using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace utility
{
    public class XMLManager
    {
        static XMLManager() { }
        public static void xmlParse(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode xn = xmlDoc.SelectSingleNode("Cities");
            XmlNodeList xnls = xn.ChildNodes;

            var citiesCostModelList = new List<ConstructionCostClass>();

            for (int i = 0; i < xnls.Count; i++)
            {
                var cityCostModel = new ConstructionCostClass();

                XmlElement xe_1 = (XmlElement)xnls[i];
                XmlNodeList xnl_1 = xe_1.ChildNodes;//type of building-office-hotel level

                cityCostModel.CityName = xnl_1.Item(0).InnerText;
                cityCostModel.CityID = xe_1.GetAttribute("Id").ToString();

                cityCostModel.Office = XmlNodeParse(xnl_1, 1);
                cityCostModel.Shopping = XmlNodeParse(xnl_1, 2);
                cityCostModel.Residential = XmlNodeParse(xnl_1, 3);
                cityCostModel.Hotel = XmlNodeParse(xnl_1, 4);
                cityCostModel.Industrial = XmlNodeParse(xnl_1, 5);
                cityCostModel.Carpark = XmlNodeParse(xnl_1, 6);
                
                citiesCostModelList.Add(cityCostModel);
            }

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
        }

        private static string[][] XmlNodeParse(XmlNodeList xnl_1, int index)
        {
            XmlNodeList xnl_2 = xnl_1.Item(index).ChildNodes;//high qulity-medium-ordinary level
            string[][] value = new string[xnl_2.Count][];
            for (int k = 0; k < xnl_2.Count; k++)
            {
                if (xnl_2.Item(k).InnerText.Contains("N/A"))
                {
                    value[k] = new string[2];
                    for (int i = 0; i < 2; i++)
                    {
                        value[k][i] = "N/A";
                    }
                }
                else
                {
                    value[k] = cleanString(xnl_2.Item(k).InnerText.Split(','));
                }
            }
            return value;
        }

        private static string[][][] XmlNodeParse_2(XmlNodeList xnl_1, int index)
        {
            XmlNodeList xnl_2 = xnl_1.Item(index).ChildNodes;//high qulity-medium-ordinary level
            string[][][] value = new string[xnl_2.Count][][];
            for (int i = 0; i < xnl_2.Count; i++)
            {
                XmlNodeList xnl_3 = xnl_2.Item(i).ChildNodes;
                for (int j = 0; j < xnl_3.Count; j++)
                {
                    value[i][j] = xnl_3.Item(i).InnerText.Split(',');
                }
            }
            return value;
        }

        private static string cleanString(string newStr)
        {
            if (newStr.Contains("\n")|| newStr.Contains("\r"))
            {
                var tempStr = newStr.Replace("\n", "");
                return tempStr.Replace("\r", "");
            }
            return newStr;
        }
        private static string[] cleanString(string[] newStr)
        {
            var resultString = new string[newStr.Length];
            for (int i = 0; i < newStr.Length; i++)
            {
                resultString[i]=cleanString(newStr[i]);
            }
            return resultString;
        }
    }
}
