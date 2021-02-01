using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Xml.Linq;
using Npgsql;
using UrbanX.Calculation;
using UrbanXX.IO.GeoJSON;

namespace UrbanX.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 读取excel数据，转为citiesCostModel 类
            //string excelPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\造价表2.xlsx";
            //string xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\test_1124.xml";

            //XMLManager.Excel2Xml(excelPath, xmlPath, "Sheet2");
            //var citiesCostModelList = XMLManager.xmlParseCities(xmlPath);

            //foreach (var item in citiesCostModelList)
            //{
            //    Console.WriteLine($"1. cityName:{item.CityName}\n");
            //    Console.WriteLine($"2. cityId:{item.CityID}\n");
            //    Console.WriteLine($"3. cityOfficeHigh:{item.Office[0][0]},{item.Office[0][1]}\n");
            //    Console.WriteLine($"4. cityShoppingHigh:{item.Shopping[0][0]},{item.Shopping[0][1]}\n");
            //    Console.WriteLine($"5. cityHotelHigh:{item.Hotel[0][0]},{item.Hotel[0][1]}\n");
            //    Console.WriteLine($"6. cityResidentialHigh:{item.Residential_HighRise[0][0]},{item.Residential_HighRise[0][1]}\n");
            //    Console.WriteLine($"7. cityIndustrialHigh:{item.Industrial[0][0]},{item.Industrial[0][1]}\n");
            //    Console.WriteLine($"8. cityCarparkHigh:{item.Carpark[0][0]},{item.Carpark[0][1]}\n");
            //}

            //Console.WriteLine("finished");
            //Console.ReadLine();
            #endregion

            #region 第一次连接数据库PGSQL
            //var cs = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";

            //var con = new NpgsqlConnection(cs);
            //con.Open();

            //var sql = "SELECT version()";

            //var cmd = new NpgsqlCommand(sql, con);

            //var version = cmd.ExecuteScalar().ToString();
            //Console.WriteLine($"PostgreSQL version: {version}");
            //Console.ReadKey();
            #endregion

            #region 第二次链接数据库 创建表格, 插入数据
            //var cs = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //var con = new NpgsqlConnection(cs);
            //con.Open();

            //var cmd = new NpgsqlCommand();
            //cmd.Connection = con;

            //cmd.CommandText = @"DROP TABLE IF EXISTS cars";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = @"CREATE TABLE CARS(ID SERIAL PRIMARY KEY,
            //      name VARCHAR(255), price INT)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText="INSERT INTO cars(name, price) VALUES('Audi', 52642)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
            //cmd.ExecuteNonQuery();

            //Console.WriteLine("Table cars created");
            #endregion

            #region 第三次链接数据库 prepared statement
            //var cs = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //var con = new NpgsqlConnection(cs);
            //con.Open();

            //var sql = "INSERT INTO cars(name, price) VALUES(@name, @price)";
            //var cmd = new NpgsqlCommand(sql, con);

            //cmd.Parameters.AddWithValue("name", "BMW");
            //cmd.Parameters.AddWithValue("price", 36600);
            //cmd.Prepare();

            //cmd.ExecuteNonQuery();
            //Console.WriteLine("row inserted");

            #endregion

            #region 第四次链接数据库 读取数据
            //var cs = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //var con = new NpgsqlConnection(cs);
            //con.Open();

            //var sql = "SELECT * FROM cars";
            //var cmd = new NpgsqlCommand(sql, con);

            //NpgsqlDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())
            //{
            //    Console.WriteLine("{0} {1} {2}",
            //        rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2));
            //}
            #endregion

            #region 第五次链接数据库 column headers
            //var cs = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //var con = new NpgsqlConnection(cs);
            //con.Open();

            //var sql = "SELECT * FROM cars";
            //var cmd = new NpgsqlCommand(sql, con);
            //NpgsqlDataReader rdr = cmd.ExecuteReader();
            //Console.WriteLine($"{rdr.GetName(0),-4} {rdr.GetName(1),-10} {rdr.GetName(2),10}");

            //while (rdr.Read())
            //{
            //    Console.WriteLine($"{rdr.GetInt32(0),-4}{rdr.GetString(1),-10}{rdr.GetInt32(2),10}");
            //}
            #endregion

            #region 第六次正式运行，录入城市数据
            //IDBHelper dbHelper = new PostgreHelper();
            ////string connectionString = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";

            //var createDB = "create table if not exists cities(id serial, code char(20) primary key, name char(20), lat numeric(9,6), lon numeric(9,6)) ";
            //dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, createDB);

            //string sql = "insert into [cities]([code],[name],[lat],[lon])values(@code, @name, @lat, @lon)";

            ////var cityNames = new string[] { "北京", "成都", "广州", "上海", "深圳", "天津", "武汉", "无锡", "西安", "珠海" };
            //var cityNames = new string[] { "杭州", "厦门", "东莞", "重庆", "福州", "泉州", "太原", "大同", "漳州", "佛山" };
            //var cityInfoList = new List<GeocodeResult>(cityNames.Length);
            //for (int i = 0; i < cityNames.Length; i++)
            //    cityInfoList.Add(GaodeLocation.DecodeResult(cityNames[i]));

            //for (int i = 0; i < cityInfoList.Count; i++)
            //{
            //    var _params = new NpgsqlParameter[] {
            //    new NpgsqlParameter("@code", cityInfoList[i].adcode),
            //    new NpgsqlParameter("@name", cityNames[i]),
            //    new NpgsqlParameter("@lat", cityInfoList[i].latitude),
            //    new NpgsqlParameter("@lon", cityInfoList[i].lontitude),
            //    };
            //    int r = dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql, _params);
            //}


            #endregion

            #region 第七次正式运行，录入类型数据
            //IDBHelper dbHelper = new PostgreHelper();
            ////string connectionString = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";

            ////var createDB = "create table if not exists building_functions(id serial primary key, name char(40)) ";
            ////dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, createDB);

            //string sql = "insert into [building_functions]([name],[relative_name])values(@name,@relative_name)";

            ////string excelPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\造价表2.xlsx";
            ////string xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\test_1124.xml";

            ////XMLManager.Excel2Xml(excelPath, xmlPath, "Sheet2");
            ////var citiesCostModelList = XMLManager.xmlParseCities(xmlPath);

            //var buildingFunctionName = new string[] { "office", "shopping_center", "residential_highRise", "residential_house", "hotel", "industrial", "carpark" };

            //for (int i = 0; i < buildingFunctionName.Length; i++)
            //{
            //    var _params = new NpgsqlParameter[] {
            //    new NpgsqlParameter("@name", buildingFunctionName[i]),
            //    };
            //    int r = dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql, _params);
            //}

            ////Console.WriteLine("完成");
            #endregion

            #region 第八次录入数据库 成本库
            //IDBHelper dbHelper = new PostgreHelper();
            ////string connectionString = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";

            //var createDB = "create table if not exists construction_cost(" +
            //    "id serial,\n " +
            //    "func_id int,\n" +
            //    "quality_id int,\n" +
            //    "city_id char(20),\n" +
            //    "price_max numeric(9,2),\n" +
            //    "price_min numeric(9,2),\n" +
            //    "year int,\n" +



            //    "Primary Key(id),\n" +

            //    "Constraint fk_city_id\n" +
            //    "Foreign Key(city_id)\n" +
            //    "References cities(code),\n" +

            //    "Constraint fk_func_id\n" +
            //    "Foreign Key(func_id)\n" +
            //    "References building_functions(id)," +

            //    "Constraint fk_quality_id\n" +
            //    "Foreign Key(quality_id)\n" +
            //    "References quality_functions(quality_id)" +
            //    ") ";
            //dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, createDB);

            //string sql = "insert into [construction_cost]([year],[city_id],[func_id],[price_min],[price_max],[quality_id])values(@year,@city_id,@func_id,@price_min,@price_max,@quality_id)";

            //string excelPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\造价表2.xlsx";
            //string xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\test_1124.xml";

            //XMLManager.Excel2Xml(excelPath, xmlPath, "Sheet2");
            //var citiesCostModelList = XMLManager.xmlParseCities(xmlPath);


            //for (int i = 0; i < citiesCostModelList.Count; i++)
            //{
            //    var resultList = DB_Manager.ExtractCityConstructionCost(citiesCostModelList[i]);
            //    for (int j = 0; j < resultList.Count; j++)
            //    {
            //        int r = dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql, resultList[j]);
            //    }
            //};

            //Console.WriteLine("完成");
            #endregion

            #region 第九次 读取数据库 成本库
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";
            //string city = "北京";
            //int attrCount = 9;
            //string sql = string.Format("select " +
            //    "bf.name,qf.quality_name,cc.quality_id,c.name, cc.price_max, cc.price_min,cc.year,c.lat, c.lon " +
            //    "FROM construction_cost cc, cities c, building_functions bf, quality_functions qf " +
            //    "where cc.city_id = c.code and cc.func_id = bf.id and cc.quality_id = qf.quality_id and c.name='{0}'; ", city);

            //var resultList = DB_Manager.GetData(connectionString, sql, attrCount);
            //FuncionClass cityInfo = new FuncionClass(resultList);
            //var result = cityInfo.FuncInfoDic;

            //Console.WriteLine("城市为{0}, 经度为{1},纬度{2}", cityInfo.CityName, cityInfo.Lat.ToString(), cityInfo.Lon.ToString());
            //Console.WriteLine("完成");
            #endregion

            #region 玩真的 标准计算数据

            #region 录入数据库数据 EC/WC/GC
            //    IDBHelper dbHelper = new PostgreHelper();
            //    //string connectionString = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //    string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";

            //    var xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\UrbanX\Data\indexCalculation.xml";
            //    var indexData=new IndexCalculation(xmlPath);

            //    string sql = "insert into [building_consumption_value]([cv_name],[building_type],[cv_min],[cv_max])values(@cv_name,@building_type,@cv_min,@cv_max)";
            //    string tablename = "building_consumption_value";
            //    string[] attrName = new string[] {"cv_name","building_type", "cv_max", "cv_min" };

            //    Dictionary<string, int> FunctionName = new Dictionary<string, int> {
            //    { "R", 3},
            //    { "H", 5},
            //    { "O", 1},
            //    { "C", 2},
            //    { "M", 6},
            //    { "W", 8},
            //    { "S", 9},
            //    { "GIC", 10},
            //};

            //    string[][] insertValue = new string[3][];
            //    for (int i = 0; i < indexData.Con_Buildings.Keys.Count; i++)
            //    {
            //        var keyName = indexData.Con_Buildings.Keys.ElementAt(i);
            //        insertValue[0] = new string[4];
            //        insertValue[0][3] = indexData.Con_Buildings[keyName]._EConsumption[0].ToString();
            //        insertValue[0][2] = indexData.Con_Buildings[keyName]._EConsumption[1].ToString();
            //        insertValue[0][1] = FunctionName[keyName].ToString();
            //        insertValue[0][0] = "EConsumption";

            //        insertValue[1] = new string[4];
            //        insertValue[1][3] = indexData.Con_Buildings[keyName]._WConsumption[0].ToString();
            //        insertValue[1][2] = indexData.Con_Buildings[keyName]._WConsumption[1].ToString();
            //        insertValue[1][1] = FunctionName[keyName].ToString();
            //        insertValue[1][0] = "WConsumption";

            //        insertValue[2] = new string[4];
            //        insertValue[2][3] = indexData.Con_Buildings[keyName]._GConsumption[0].ToString();
            //        insertValue[2][2] = indexData.Con_Buildings[keyName]._GConsumption[1].ToString();
            //        insertValue[2][1] = FunctionName[keyName].ToString();
            //        insertValue[2][0] = "GConsumption";
            //        DB_Manager.InsertData(connectionString, sql, tablename, attrName, insertValue);
            //    }
            #endregion

            #region 录入数据库数据 Population
            //IDBHelper dbHelper = new PostgreHelper();
            ////string connectionString = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";

            //var xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\UrbanX\Data\indexCalculation.xml";
            //var indexData = new IndexCalculation(xmlPath);

            //var createDB = "create table if not exists building_population(" +
            //    "bp_id serial,\n " +
            //    "bp_name char(20),\n" +
            //    "bp_layer_min int,\n" +
            //    "bp_layer_max int,\n" +
            //    "bp_people int,\n" +
            //    "bp_FAR_min numeric(4,2),\n" +
            //    "bp_FAR_max numeric(4,2),\n" +
            //    "bp_density_max numeric(3,2),\n" +
            //    "bp_green_min numeric(3,2),\n" +
            //    "bp_height_max numeric(3,1),\n" +
            //    "bp_func_id int,\n" +

            //    "Primary Key(bp_id),\n" +

            //    "Constraint fk_func_id\n" +
            //    "Foreign Key(bp_func_id)\n" +
            //    "References building_functions(id)\n" +
            //    ") ";
            //dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, createDB);

            //string sql = "insert into [building_population](" +
            //    "[bp_name],[bp_layer_min],[bp_layer_max],[bp_people],[bp_far_min],[bp_far_max]," +
            //    "[bp_density_max],[bp_green_min],[bp_height_max],[bp_func_id])" +
            //    "values(@bp_name,@bp_layer_min,@bp_layer_max,@bp_people,@bp_far_min,@bp_far_max" +
            //    ",@bp_density_max,@bp_green_min,@bp_height_max,@bp_func_id)";

            //string tablename = "building_population";
            //string[] attrName = new string[] { 
            //    "bp_name", "bp_layer_min", "bp_layer_max", "bp_people", "bp_FAR_min" , "bp_FAR_max",
            //    "bp_density_max","bp_green_min","bp_height_max","bp_func_id"
            //};

            //Dictionary<string, int> FunctionName = new Dictionary<string, int> {
            //    { "R", 3},
            //    { "H", 5},
            //    { "O", 1},
            //    { "C", 2},
            //    { "M", 6},
            //    { "W", 8},
            //    { "S", 9},
            //    { "GIC", 10},
            //};

            //int NodeCount = indexData.PopulationType.Keys.Count;
            //string[][] insertValue = new string[NodeCount][];
            //for (int i = 0; i < NodeCount; i++)
            //{
            //    var keyName = i;
            //    insertValue[i] = new string[10];
            //    insertValue[i][0] = string.Format("type_{0}", i);
            //    insertValue[i][1] = indexData.PopulationType[keyName]._layer[0].ToString();
            //    insertValue[i][2] = indexData.PopulationType[keyName]._layer[1].ToString();
            //    insertValue[i][3] = indexData.PopulationType[keyName]._people.ToString();
            //    insertValue[i][4] = indexData.PopulationType[keyName]._FAR[0].ToString();
            //    insertValue[i][5] = indexData.PopulationType[keyName]._FAR[1].ToString();
            //    insertValue[i][6] = indexData.PopulationType[keyName]._maxDensity.ToString();
            //    insertValue[i][7] = indexData.PopulationType[keyName]._minGreen.ToString();
            //    insertValue[i][8] = indexData.PopulationType[keyName]._maxHeight.ToString();
            //    insertValue[i][9] = FunctionName["R"].ToString();
            //}
            //DB_Manager.InsertData(connectionString, sql, tablename, attrName, insertValue);
            #endregion

            #region 读取数据库数据 EC/WC/GC
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";
            //int attrCount = 6;
            //string sql = "select cv.cv_id, cv.cv_name, bf.name, bf.relative_name, cv.cv_max, cv.cv_min " +
            //    "from building_consumption_value cv, building_functions bf " +
            //    "where cv.building_type = bf.id;";

            //var resultList = DB_Manager.GetData(connectionString, sql, attrCount);
            #endregion

            #region 读取数据库数据 Population
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";
            //int attrCount = 12;
            //string sql = "select " +
            //    "bp.bp_id, bp.bp_name, bp.bp_layer_min,bp.bp_layer_max, bp.bp_people, bp.bp_far_min, bp.bp_far_max," +
            //    "bp.bp_density_max,bp.bp_green_min,bp.bp_height_max,bf.name, bf.relative_name " +
            //    "from " +
            //    "building_population bp, building_functions bf " +
            //    "where" +
            //    " bp.bp_func_id = bf.id;";

            //var resultList = DB_Manager.GetData(connectionString, sql, attrCount);
            #endregion

            #region 读取数据库数据 成本库
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";
            //string city = "北京";
            //int attrCount = 9;
            //string sql = string.Format("select " +
            //    "bf.name,qf.quality_name,cc.quality_id,c.name, cc.price_max, cc.price_min,cc.year,c.lat, c.lon " +
            //    "FROM construction_cost cc, cities c, building_functions bf, quality_functions qf " +
            //    "where cc.city_id = c.code and cc.func_id = bf.id and cc.quality_id = qf.quality_id and c.name='{0}'; ", city);

            //var resultList = DB_Manager.GetData(connectionString, sql, attrCount);
            //FuncionClass cityInfo = new FuncionClass(resultList);
            //var result = cityInfo.FuncInfoDic;

            //Console.WriteLine("城市为{0}, 经度为{1},纬度{2}", cityInfo.CityName, cityInfo.Lat.ToString(), cityInfo.Lon.ToString());
            #endregion

            #region 读取geojson
            //var jsonFilePath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\geojson\building.geojson";
            //StreamReader sr = File.OpenText(jsonFilePath);

            //var feactureCollection = GeoJsonReader.GetFeatureCollectionFromJson(sr.ReadToEnd());

            //for (int i = 0; i < feactureCollection.Count; i++)
            //{
            //    var jsonDic = feactureCollection[i].Attributes["function"];
            //    Console.WriteLine(jsonDic.ToString());
            //}
            #endregion

            #region 测试计算整体内容
            string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlab_db";
            string city = "北京";
            string jsonFile = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\geojson\building.geojson";
            double hqratio = 0.5;

            Sustainable_calculation calc = new Sustainable_calculation(connectionString, city, jsonFile,hqratio);

            //Console.WriteLine("城市为{0}, 经度为{1},纬度{2}", cityInfo.CityName, cityInfo.Lat.ToString(), cityInfo.Lon.ToString());
            #endregion

            Console.WriteLine("完成");




            #endregion

            #region 其他测试

            #region 001_根据节点，读取xml数据
            //string readPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\kmlTest\test001.kml";
            //string savePath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\kmlTest\testOutput_4.kml";
            //Point pointMin = new Point(110.9880, 36.7666);
            //Point pointMax = new Point(113.7515, 38.7294);

            //ToolManagers.CreateKMLFile(readPath, savePath, pointMin, pointMax, 150, 150);
            //Console.WriteLine("完成");
            #endregion

            #region 002_输出分布函数
            //List<double> resultList = new List<double>();
            //int count = 100;
            //var result = StatisticsModel.NormalDistribution(10, 0.1, count, 1);

            //foreach (var item in resultList)
            //    Console.WriteLine(item);
            #endregion

            #region 003_地理位置编码
            //var data = GaodeLocation.DecodeResult("成都");
            //Console.WriteLine(data.adcode +"\n"+data.latitude+ "\n"+data.lontitude);
            #endregion

            #region 004_批量上传shp数据进数据库
            //string host = "39.107.177.223";
            //string user = "postgres";
            //string pwd = "admin";
            //string db = "urbanxlab_db";
            //string schema = "geometry_data";
            //string table = "beijing";


            //string shpfile = @"E:\114_temp\014_data\geojson\北京市.shp";
            //var result=DB_Manager.OutputSHPtoPSGL(shpfile, host, user, pwd, db, schema, true, table);
            //Console.WriteLine(result);
            #endregion

            #endregion



            Console.WriteLine("完成");
            Console.ReadLine();
        }

    }
}

