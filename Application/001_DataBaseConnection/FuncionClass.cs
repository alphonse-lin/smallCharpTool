using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class FuncionClass
    {
        public Dictionary<string, SortedList<int, Quality>> FuncInfoDic;
        
        public string CityName { get; set; }
        public int Year { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        private Quality qualityData { get; set ; }

        public FuncionClass(List<string[]> resultList)
        {
            CityName = resultList[0][3];
            Year = int.Parse(resultList[0][6]);
            Lat = double.Parse(resultList[0][7]);
            Lon = double.Parse(resultList[0][8]);
            FuncInfoDic = DecodeList(resultList);
        }

        private Dictionary<string, SortedList<int, Quality>>  DecodeList(List<string[]> resultList)
        {
            Dictionary<string, SortedList<int, Quality>> funcSortDicInside = new Dictionary<string, SortedList<int, Quality>>();
            Dictionary<string, List<string[]>> funcDic =new Dictionary<string, List<string[]>>();
            var buildingFunctionName = new string[] { "office", "shopping_center", "residential_highRise", "residential_house", "hotel", "industrial", "carpark" };
            
            for (int i = 0; i < resultList.Count; i++)
            {
                if (funcDic.ContainsKey(resultList[i][0]))
                {
                    funcDic[resultList[i][0]].Add(resultList[i]);
                }
                else
                {
                    funcDic.Add(resultList[i][0],new List<string[]>{ resultList[i] });
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
    }
    
    public struct Quality
    {
        public string name { get; }
        public int nameId { get; }
        public double priceMax { get; }
        public double priceMin { get;}

        public Quality(string Name, int NameId, double PriceMax, double PriceMin)
        {
            name = Name;
            nameId = NameId;
            priceMax = PriceMax;
            priceMin = PriceMin;
        }

    }
