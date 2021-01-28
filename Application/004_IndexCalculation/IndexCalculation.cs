using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanXX.IO.GeoJSON;

namespace UrbanX.Application
{
    public enum QualityType
    {
        highQuality,
        lowQuality
    }
    public class IndexCalculation
    {
        private const int roundNum = 1;
        private string connectionString { get; set; }
        private string city;
        private string jsonFilePath;
        private List<double[]> pp_intervalLayer;

        private Dictionary<string, string> FunctionName = new Dictionary<string, string> {
            { "R", "residential"},
            { "H", "hotel"},
            { "O", "office"},
            { "C", "shopping_center"},
            { "M", "industrial"},
            { "W", "warehouse"},
        };
        //private Dictionary<string, InfoFromXML_BB> Con_Buildings { get; set; }
        //private Dictionary<string, InfoFromXML_BB> Con_Blocks { get; set; }
        //private Dictionary<int, InfoFromXML_Population> PopulationType { get; set; }

        private Dictionary<string, SortedList<int, BasePopulation>> populationValueDic { get; set; }
        private Dictionary<string, List<BaseEWGConsumption>> EWGConsumptionValueDic { get; set; }

        private Dictionary<string, SortedList<int, Quality>> constructionValueDic { get; set; }
        public IndexCalculation(string connectionString, string city)
        {
            //人口
            populationValueDic = ExtractData_Population(connectionString);
            pp_intervalLayer = GenerateLayerCollection(populationValueDic);


            //EWG
            EWGConsumptionValueDic = ExtractData_EWGConsumption(connectionString);

            //成本
            constructionValueDic = ExtractData_ConstructionCost(connectionString,city);


        }

        #region 功能区

        #region 001_人口计算
        /// <summary>
        /// 提取所有层高数据
        /// </summary>
        private List<double[]> GenerateLayerCollection(Dictionary<string, SortedList<int, BasePopulation>> pv_Dic, string function = "R")
        {
            var ppLayerList = new List<double[]>(pv_Dic[function].Count);

            for (int i = 0; i < pv_Dic[function].Count; i++)
            {
                var tempData = new double[] { pv_Dic[function][i].layerMin, pv_Dic[function][i].layerMax };
                ppLayerList.Add(tempData);
            }
            return ppLayerList;
        }

        /// <summary>
        /// 确定属于建筑属于哪一类型
        /// </summary>
        private int SpecifyPopulationType(int layer)
        {
            int type = -1;
            for (int i = 0; i < pp_intervalLayer.Count; i++)
            {
                if (layer >= pp_intervalLayer[i][0] && layer <= pp_intervalLayer[i][1]) { type = i; break; }
                else if (i == 4 && layer > pp_intervalLayer[i][1])
                    type = 4;
                else
                    continue;
            }
            return type;
        }

        /// <summary>
        /// 基于面积和层高，计算人口数
        /// </summary>
        public double[] CalculatePopulation(int[] layer, double[] area, string function = "R")
        {
            var len = layer.Count();
            double[] result = new double[layer.Count()];
            for (int i = 0; i < len; i++)
            {
                var type = SpecifyPopulationType(layer[i]);
                var peopleUnitValue = populationValueDic[function][type].people;
                result[i] = area[i] / peopleUnitValue;
            }
            return result;
        }

        /// <summary>
        /// 基于面积和层高，计算人口数
        /// </summary>
        public double CalculatePopulation(int layer, double area, string function = "R")
        {
            var type = SpecifyPopulationType(layer);
            var peopleUnitValue = populationValueDic[function][type].people;
            var result = area / peopleUnitValue;
            return result;
        }
        #endregion

        #region 002_地块向指标计算

        private double FAR_Block(double FAR)
        {
            double FARBLK = FAR;
            return FARBLK;
        }

        private double Density_Block(double density)
        {
            double DensityBLK = density;
            return DensityBLK;
        }

        private int[] Consumption_Block(string function, IEnumerable<int[]> Consumption_Building)
        {
            int[] Consumption = new int[3];
            var length = Consumption_Building.Count();
            int[] minGC = new int[length];
            int[] maxGC = new int[length];
            for (int i = 0; i < length; i++)
            {
                minGC[i] = Convert.ToInt32(Consumption_Building.ElementAt(i)[0]);
                maxGC[i] = Convert.ToInt32(Consumption_Building.ElementAt(i)[1]);
            }
            Consumption[0] = Sum(minGC);
            Consumption[1] = Sum(maxGC);
            Consumption[2] = (Consumption[0] + Consumption[1]) / 2;

            return Consumption;
        }
        #endregion

        #region 003_建筑向指标计算
        // TODO 更改数据来源为数据库

        private List<string[]> ReadFromDb_EWG(string connectionString)
        {
            int attrCount = 6;
            string sql = "select cv.cv_id, cv.cv_name, bf.name, bf.relative_name, cv.cv_max, cv.cv_min " +
                "from building_consumption_value cv, building_functions bf " +
                "where cv.building_type = bf.id;";

            var resultList = DB_Manager.GetData(connectionString, sql, attrCount);
            return resultList;
        }
        private int Layers_Brep(int layer)
        {
            int layerBD = layer;
            return layerBD;
        }
        private string Function_Brep(string function)
        {
            string functionBD = function;
            return functionBD;
        }

        public int[] EnergyConsumption_Brep(string function, double area)
        {
            int[] EConsumption = new int[3];
            //单位：千瓦/平方米
            double[] EConNum = Con_Buildings[function]._EConsumption;

            EConsumption[0] = Convert.ToInt32(area * EConNum[0]);
            EConsumption[1] = Convert.ToInt32(area * EConNum[1]);
            EConsumption[2] = (EConsumption[0] + EConsumption[1]) / 2;

            return EConsumption;
        }

        public int[] WaterConsumption_Brep(string function, double area)
        {
            int[] WConsumption = new int[3];
            //单位：吨/平方米
            double[] WConNum = Con_Buildings[function]._WConsumption;

            WConsumption[0] = Convert.ToInt32(area * WConNum[0]);
            WConsumption[1] = Convert.ToInt32(area * WConNum[1]);
            WConsumption[2] = (WConsumption[0] + WConsumption[1]) / 2;

            return WConsumption;
        }

        public int[] GarbageConsumption_Brep(string function, double area, int layer)
        {
            int[] GConsumption = new int[3];
            var populationCount = CalculatePopulation(layer, area);
            //单位：千克/人
            var GConNum = Con_Buildings[function]._GConsumption;

            GConsumption[0] = Convert.ToInt32(populationCount * GConNum[0]);
            GConsumption[1] = Convert.ToInt32(populationCount * GConNum[1]);
            GConsumption[2] = (GConsumption[0] + GConsumption[1]) / 2;

            return GConsumption;
        }

        public int[] Consumption_BrepSum(List<int[]> IList)
        {
            int[] GConsumption = new int[2];
            var len = IList.Count();
            var minList = new List<int>(len);
            var maxList = new List<int>(len);
            for (int i = 0; i < len; i++)
            {
                int[] valueCount = IList[i];
                minList.Add(valueCount[0]);
                maxList.Add(valueCount[1]);
            }
            var minResult = minList.Sum();
            var maxResult = maxList.Sum();

            GConsumption[0] = minResult;
            GConsumption[1] = maxResult;

            return GConsumption;
        }
        public double GetAverageValue(double[] arrayValue)
        {
            var average = (arrayValue[0] + arrayValue[1]) / 2;
            return average;
        }

        #endregion

        #region 004_成本计算
        // TODO 完善计算公式 + 数据库来源
        public ResultFromConstructionCost ConstructionCost_Brep(string funcName, int floor, double area, Dictionary<string, SortedList<int, Quality>> costPrice, QualityType qType)
        {
            var houseLevel = 10;
            Quality highQuality = new Quality();
            Quality lowQuality = new Quality();
            var min = 0;
            var max = 0;
            var average = 0;

            switch (funcName)
            {
                case "R":
                    if (floor < houseLevel)
                    {
                        highQuality = costPrice[string.Format("{0}_house", FunctionName[funcName])][0];
                        lowQuality = costPrice[string.Format("{0}_house", FunctionName[funcName])][1];
                    }
                    else
                    {
                        highQuality = costPrice[string.Format("{0}_highRise", FunctionName[funcName])][0];
                        lowQuality = costPrice[string.Format("{0}_highRise", FunctionName[funcName])][2];
                    }
                    break;
                case "H":
                    highQuality = costPrice[FunctionName[funcName]][0];
                    lowQuality = costPrice[FunctionName[funcName]][1];
                    break;
                case "O":
                    highQuality = costPrice[FunctionName[funcName]][0];
                    lowQuality = costPrice[FunctionName[funcName]][2];
                    break;
                case "C":
                    highQuality = costPrice[FunctionName[funcName]][0];
                    lowQuality = costPrice[FunctionName[funcName]][1];
                    break;
                case "M":
                    highQuality = costPrice[FunctionName[funcName]][0];
                    lowQuality = costPrice[FunctionName[funcName]][1];
                    break;
                case "W":
                    highQuality = costPrice[FunctionName[funcName]][0];
                    lowQuality = costPrice[FunctionName[funcName]][1];
                    break;
            }

            switch (qType)
            {
                case QualityType.highQuality:
                    min = Convert.ToInt32(highQuality.priceMin * area);
                    max = Convert.ToInt32(highQuality.priceMax * area);
                    average = (min + max) / 2;
                    break;
                case QualityType.lowQuality:
                    min = Convert.ToInt32(lowQuality.priceMin * area);
                    max = Convert.ToInt32(lowQuality.priceMax * area);
                    average = (min + max) / 2;
                    break;
            }
            return new ResultFromConstructionCost(funcName, min, max, average);
        }

        public Dictionary<string, int[]> ConstructionCost_SUM(List<ResultFromConstructionCost> ccList)
        {
            Dictionary<string, List<int[]>> temp_ccList = new Dictionary<string, List<int[]>>();
            List<int> minCostList = new List<int>(ccList.Count);
            List<int> averageCostList = new List<int>(ccList.Count);
            List<int> maxCostList = new List<int>(ccList.Count);

            for (int i = 0; i < ccList.Count; i++)
            {
                int[] price = new int[3];
                price[0] = ccList[i].CostMin;
                price[1] = ccList[i].CostAverage;
                price[2] = ccList[i].CostMax;
                if (temp_ccList.ContainsKey(ccList[i].Name))
                {
                    temp_ccList[ccList[i].Name].Add(price);
                }
                else
                {
                    temp_ccList.Add(ccList[i].Name, new List<int[]> { price });
                }
            }

            for (int i = 0; i < temp_ccList.Keys.Count; i++)
            {
                var keyName = temp_ccList.Keys.ElementAt(i);
                for (int j = 0; j < temp_ccList[keyName].Count; j++)
                {
                    minCostList.Add(temp_ccList[keyName][j][0]);
                    averageCostList.Add(temp_ccList[keyName][j][1]);
                    maxCostList.Add(temp_ccList[keyName][j][2]);
                }
            }

            Dictionary<string, int[]> result = new Dictionary<string, int[]>();
            for (int i = 0; i < temp_ccList.Keys.Count; i++)
            {
                result.Add(temp_ccList.Keys.ElementAt(i), new int[] { minCostList.Sum(), averageCostList.Sum(), maxCostList.Sum() });
            }
            return result;
        }

        private Dictionary<string, SortedList<int, Quality>> ExtractCostData(string connectionString, string city)
        {
            int attrCount = 9;
            string sql = string.Format("select " +
                "bf.name,qf.quality_name,cc.quality_id,c.name, cc.price_max, cc.price_min,cc.year,c.lat, c.lon " +
                "FROM construction_cost cc, cities c, building_functions bf, quality_functions qf " +
                "where cc.city_id = c.code and cc.func_id = bf.id and cc.quality_id = qf.quality_id and c.name='{0}'; ", city);

            var resultList = DB_Manager.GetData(connectionString, sql, attrCount);
            FuncionClass cityInfo = new FuncionClass(resultList);
            Dictionary<string, SortedList<int, Quality>> result = cityInfo.FuncInfoDic;

            return result;
        }
        #endregion

        #region 005_碳中和
        #endregion

        #region 098读取数据库数据

        #region 098_001_读取数据库数据 EC/WC/GC
        private Dictionary<string, List<BaseEWGConsumption>> ExtractData_EWGConsumption(string connectionString)
        {
            int attrCount = 6;
            string sql = "select cv.cv_id, cv.cv_name, bf.name, bf.relative_name, cv.cv_max, cv.cv_min " +
                "from building_consumption_value cv, building_functions bf " +
                "where cv.building_type = bf.id;";

            var temp_List = DB_Manager.GetData(connectionString, sql, attrCount);

            List<BaseEWGConsumption> valueList = new List<BaseEWGConsumption>();
            Dictionary<string, List<BaseEWGConsumption>> valueDic = new Dictionary<string, List<BaseEWGConsumption>>();
            for (int i = 0; i < temp_List.Count; i++)
            {
                var tempArray = temp_List[i];
                BaseEWGConsumption singleC = new BaseEWGConsumption(tempArray[1],tempArray[2],tempArray[3],double.Parse(tempArray[4]),double.Parse(tempArray[5]));
                if (valueDic.ContainsKey(singleC.relativeName))
                {
                    valueDic[singleC.relativeName].Add(singleC);
                }
                else
                {
                    valueDic.Add(singleC.relativeName,new List<BaseEWGConsumption> { singleC });
                }
                valueList.Add(singleC);
            }

            return valueDic;
        }

        #endregion

        #region 098_002_读取数据库数据 Population
        private Dictionary<string, SortedList<int, BasePopulation>> ExtractData_Population(string connectionString)
        {
            int attrCount = 12;
            string sql = "select " +
                "bp.bp_id, bp.bp_name, bp.bp_layer_min,bp.bp_layer_max, bp.bp_people, bp.bp_far_min, bp.bp_far_max," +
                "bp.bp_density_max,bp.bp_green_min,bp.bp_height_max,bf.name, bf.relative_name " +
                "from " +
                "building_population bp, building_functions bf " +
                "where" +
                " bp.bp_func_id = bf.id;";

            var temp_List = DB_Manager.GetData(connectionString, sql, attrCount);

            List<BasePopulation> valueList = new List<BasePopulation>();
            Dictionary<string, SortedList<int,BasePopulation>> valueDic = new Dictionary<string, SortedList<int, BasePopulation>>();
            for (int i = 0; i < temp_List.Count; i++)
            {
                var tempArray = temp_List[i];
                var singleC = new BasePopulation(tempArray[1], double.Parse(tempArray[2]), double.Parse(tempArray[3]), int.Parse(tempArray[4]), double.Parse(tempArray[5]), double.Parse(tempArray[6]),
                    double.Parse(tempArray[7]), double.Parse(tempArray[8]),double.Parse(tempArray[9]),tempArray[10],tempArray[11]
                    );
                if (valueDic.ContainsKey(singleC.relativeName))
                {
                    valueDic[singleC.relativeName].Add(int.Parse(singleC.name),singleC);
                }
                else
                {
                    valueDic.Add(singleC.relativeName, new SortedList<int, BasePopulation> { { int.Parse(singleC.name), singleC } });
                }
                valueList.Add(singleC);
            }

            return valueDic;
        }

        #endregion

        #region 098_003_读取数据库数据 成本库
        private Dictionary<string, SortedList<int, Quality>> ExtractData_ConstructionCost(string connectionString, string city)
        {
            int attrCount = 9;
            string sql = string.Format("select " +
                "bf.name,qf.quality_name,cc.quality_id,c.name, cc.price_max, cc.price_min,cc.year,c.lat, c.lon " +
                "FROM construction_cost cc, cities c, building_functions bf, quality_functions qf " +
                "where cc.city_id = c.code and cc.func_id = bf.id and cc.quality_id = qf.quality_id and c.name='{0}'; ", city);

            var resultList = DB_Manager.GetData(connectionString, sql, attrCount);
            FuncionClass cityInfo = new FuncionClass(resultList);
            var result = cityInfo.FuncInfoDic;

            return result;
        }

        #endregion
        #endregion

        #region 099_基础运算
        private double Sum(IEnumerable<double> num)
        {
            double sum = 0d;
            foreach (var item in num) { sum += item; }
            return sum;
        }

        private int Sum(IEnumerable<int> num)
        {
            int sum = 0;
            foreach (var item in num) { sum += item; }
            return sum;
        }
        #endregion
        #endregion
    }

    public class ResultFromConstructionCost
    {
        public string Name { get; set; }
        public int CostMin { get; set; }
        public int CostMax { get; set; }
        public int CostAverage { get; set; }

        public ResultFromConstructionCost(string name, int costMin, int costMax, int costAverage)
        {
            Name = name;
            CostMin = costMin;
            CostMax = costMax;
            CostAverage = costAverage;
        }
    }

    #region EWGConsumption 基础数据
    public struct BaseEWGConsumption
    {
        public string name { get; set; }
        public string  funcName { get; set; }
        public string relativeName { get; set; }
        public double c_max { get; set; }
        public double c_min { get; set; }

        public BaseEWGConsumption(string Name, string FuncName, string RelativeName, double Max, double Min)
        {
            name = Name;
            funcName = FuncName;
            relativeName = RelativeName;
            c_max = Max;
            c_min = Min;
        }
    }
    #endregion

    #region Population 基础数据
    public struct BasePopulation
    {
        public string name { get; set; }
        public double layerMin { get; set; }
        public double layerMax { get; set; }
        public int people { get; set; }
        public double farMin { get; set; }
        public double farMax { get; set; }
        public double maxDensity { get; set; }
        public double minGreen { get; set; }
        public double maxHeight { get; set; }
        public string funcName { get; set; }
        public string relativeName { get; set; }

        public BasePopulation(string Name, double LayerMin, double LayerMax, int People, double FARMin, double FARMax,
            double MaxDensity, double MinGreen, double MaxHeight, string FuncName, string RelativeName
            )
        {
            name = Name;
            layerMin = LayerMin;
            layerMax = LayerMax;
            people = People;
            farMin = FARMin;
            farMax = FARMax;
            maxDensity = MaxDensity;
            minGreen = MinGreen;
            maxHeight = MaxHeight;
            funcName = FuncName;
            relativeName = RelativeName;
        }
    }
    #endregion

    #region Construction cost 基础数据
    public class FuncionClass
    {
        public Dictionary<string, SortedList<int, Quality>> FuncInfoDic;

        public string CityName { get; set; }
        public int Year { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        private Quality qualityData { get; set; }

        public FuncionClass(List<string[]> resultList)
        {
            CityName = resultList[0][3];
            Year = int.Parse(resultList[0][6]);
            Lat = double.Parse(resultList[0][7]);
            Lon = double.Parse(resultList[0][8]);
            FuncInfoDic = DecodeList(resultList);
        }

        private Dictionary<string, SortedList<int, Quality>> DecodeList(List<string[]> resultList)
        {
            Dictionary<string, SortedList<int, Quality>> funcSortDicInside = new Dictionary<string, SortedList<int, Quality>>();
            Dictionary<string, List<string[]>> funcDic = new Dictionary<string, List<string[]>>();
            var buildingFunctionName = new string[] { "office", "shopping_center", "residential_highRise", "residential_house", "hotel", "industrial", "carpark" };

            for (int i = 0; i < resultList.Count; i++)
            {
                if (funcDic.ContainsKey(resultList[i][0]))
                {
                    funcDic[resultList[i][0]].Add(resultList[i]);
                }
                else
                {
                    funcDic.Add(resultList[i][0], new List<string[]> { resultList[i] });
                }
            }


            for (int i = 0; i < buildingFunctionName.Length; i++)
            {
                var listCount = funcDic[buildingFunctionName[i]].Count;
                SortedList<int, Quality> sortQualityList = new SortedList<int, Quality>(listCount);
                for (int j = 0; j < listCount; j++)
                {
                    var singeResultList = funcDic[buildingFunctionName[i]][j];
                    Quality singleQuality = new Quality(
                        singeResultList[1],
                        int.Parse(singeResultList[2]),
                        double.Parse(singeResultList[4]),
                        double.Parse(singeResultList[5]));

                    sortQualityList.Add(int.Parse(singeResultList[2]), singleQuality);
                }
                funcSortDicInside.Add(buildingFunctionName[i], sortQualityList);
            }
            return funcSortDicInside;
        }
    }
    public struct Quality
    {
        public string name { get; }
        public int nameId { get; }
        public double priceMax { get; }
        public double priceMin { get; }

        public Quality(string Name, int NameId, double PriceMax, double PriceMin)
        {
            name = Name;
            nameId = NameId;
            priceMax = PriceMax;
            priceMin = PriceMin;
        }

    }
    #endregion
}
