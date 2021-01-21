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
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlabdb";

            //var createDB = "create table if not exists cities(id serial, code char(20) primary key, name char(20), lat numeric(9,6), lon numeric(9,6)) ";
            //dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, createDB);

            //string sql = "insert into [cities]([code],[name],[lat],[lon])values(@code, @name, @lat, @lon)";

            //var cityNames = new string[] { "北京", "成都", "广州", "上海", "深圳", "天津", "武汉","无锡","西安","珠海"};
            ////var cityNames = new string[] { "杭州", "厦门", "东莞", "重庆", "福州", "泉州", "太原", "大同", "漳州", "佛山" };
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
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlabdb";

            //var createDB = "create table if not exists building_functions(id serial primary key, name char(40)) ";
            //dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, createDB);

            //string sql = "insert into [building_functions]([name])values(@name)";

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
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlabdb";

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
            //    var resultList = ExtractCityConstructionCost(citiesCostModelList[i]);
            //    for (int j = 0; j < resultList.Count; j++)
            //    {
            //        int r = dbHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql, resultList[j]);
            //    }
            //};

            //Console.WriteLine("完成");
            #endregion

            #region 第九次 读取数据库 成本库
            //string connectionString = "Host=39.107.177.223;Username=postgres;Password=admin;Database=urbanxlabdb";
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

            #region 其他测试

            #region 根据节点，读取xml数据
            //string readPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\kmlTest\test001.kml";
            //string savePath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\kmlTest\testOutput_4.kml";
            //Point pointMin = new Point(110.9880, 36.7666);
            //Point pointMax = new Point(113.7515, 38.7294);

            //ToolManagers.CreateKMLFile(readPath, savePath, pointMin, pointMax, 150, 150);
            //Console.WriteLine("完成");
            #endregion

            #region 输出分布函数
            //List<double> resultList = new List<double>();
            //int count = 100;
            //var result = StatisticsModel.NormalDistribution(10, 0.1, count, 1);

            //foreach (var item in resultList)
            //    Console.WriteLine(item);
            #endregion

            #region 地理位置编码
            //var data = GaodeLocation.DecodeResult("成都");
            //Console.WriteLine(data.adcode +"\n"+data.latitude+ "\n"+data.lontitude);
            #endregion

            #endregion

            #region 读取geojson
            var jsonFilePath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\Application\data\geojson\building.geojson";
            StreamReader sr = File.OpenText(jsonFilePath);
            GeoJsonReader testRead = new GeoJsonReader();
            var result = testRead.Read<string>(sr.ReadToEnd());

            Console.WriteLine(result);

            #endregion
            Console.ReadLine();
        }
    }
}

