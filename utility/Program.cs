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

namespace utility
{
    class Program
    {
        static void Main(string[] args)
        {
            string excelPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\造价表2.xlsx";
            string xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\test_1124.xml";

            XMLManager.Excel2Xml(excelPath, xmlPath, "Sheet2");
            var citiesCostModelList= XMLManager.xmlParseCities(xmlPath);

            foreach (var item in citiesCostModelList)
            {
                Console.WriteLine($"1. cityName:{item.CityName}\n");
                Console.WriteLine($"2. cityId:{item.CityID}\n");
                Console.WriteLine($"3. cityOfficeHigh:{item.Office[0][0]},{item.Office[0][1]}\n");
                Console.WriteLine($"4. cityShoppingHigh:{item.Shopping[0][0]},{item.Shopping[0][1]}\n");
                Console.WriteLine($"5. cityHotelHigh:{item.Hotel[0][0]},{item.Hotel[0][1]}\n");
                Console.WriteLine($"6. cityResidentialHigh:{item.Residential[0][0]},{item.Residential[0][1]}\n");
                Console.WriteLine($"7. cityIndustrialHigh:{item.Industrial[0][0]},{item.Industrial[0][1]}\n");
                Console.WriteLine($"8. cityCarparkHigh:{item.Carpark[0][0]},{item.Carpark[0][1]}\n");
            }

            Console.WriteLine("finished");
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
    
