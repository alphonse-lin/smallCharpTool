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

namespace utility
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 读取excel数据，转为citiesCostModel 类
            //string excelPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\造价表2.xlsx";
            //string xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\test_1124.xml";

            //XMLManager.Excel2Xml(excelPath, xmlPath, "Sheet2");
            //var citiesCostModelList= XMLManager.xmlParseCities(xmlPath);

            //foreach (var item in citiesCostModelList)
            //{
            //    Console.WriteLine($"1. cityName:{item.CityName}\n");
            //    Console.WriteLine($"2. cityId:{item.CityID}\n");
            //    Console.WriteLine($"3. cityOfficeHigh:{item.Office[0][0]},{item.Office[0][1]}\n");
            //    Console.WriteLine($"4. cityShoppingHigh:{item.Shopping[0][0]},{item.Shopping[0][1]}\n");
            //    Console.WriteLine($"5. cityHotelHigh:{item.Hotel[0][0]},{item.Hotel[0][1]}\n");
            //    Console.WriteLine($"6. cityResidentialHigh:{item.Residential[0][0]},{item.Residential[0][1]}\n");
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
            var cs = "Host=127.0.0.1;Username=postgres;Password=admin;Database=test";
            var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT * FROM cars";
            var cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine($"{rdr.GetName(0),-4} {rdr.GetName(1),-10} {rdr.GetName(2),10}");

            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetInt32(0),-4}{rdr.GetString(1),-10}{rdr.GetInt32(2),10}");
            }
            #endregion
            Console.ReadLine();

        }
        #region 数据结构测试
        private static void CreateStructuredXml(string testPath)
        {
            var doc = new XDocument(
            new XElement("Cities",
                new XElement("City", new XAttribute("Name", "HongKong"),
                    new XElement("Offices",
                        new XElement("HighQuaility", "21700,30900"),
                        new XElement("MediumQuaility", "17900,21800"),
                        new XElement("OrdinaryQuaility", "15300,19200")),
                    new XElement("Hotel",
                        new XElement("HighQuaility", "21700,30900"),
                        new XElement("MediumQuaility", "17900,21800"),
                        new XElement("OrdinaryQuaility", "15300,19200")
            )),
                new XElement("City", new XAttribute("Name", "北京"),
                    new XElement("Offices",
                        new XElement("HighQuaility", "21700,30900"),
                        new XElement("MediumQuaility", "17900,21800"),
                        new XElement("OrdinaryQuaility", "15300,19200")
            ))));
            doc.Save(testPath);
        }
        #endregion
    }
}
    
