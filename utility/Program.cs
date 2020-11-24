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

            Excel2Xml(excelPath,xmlPath, "Sheet2");
            Console.WriteLine("finished");
            Console.ReadLine();
        }

        private static XmlDocument Excel2Xml(string excelFilePath, string exportXmlPath,string sheetTitle)
        {
            XmlDocument excelData = new XmlDocument();
            DataSet excelTableDataSet = new DataSet();
            string stringConnectToExcelFile = string.Format("provider=Microsoft.Jet.OLEDB.4.0;data source={0};Extended Properties=Excel 8.0;", excelFilePath);
            OleDbConnection oleConnectionToExcelFile = new OleDbConnection(stringConnectToExcelFile);
            OleDbDataAdapter oleDataAdapterForGetExcelTable = new OleDbDataAdapter(
                string.Format($"select * from [{sheetTitle}$]"), oleConnectionToExcelFile);
            try
            {
                oleDataAdapterForGetExcelTable.Fill(excelTableDataSet, "city");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("error:" + ex.Message);
            }

            CreateNewXElement(excelTableDataSet, exportXmlPath);

            string excelOutputXml = "tmp.xml";
            excelTableDataSet.WriteXml(excelOutputXml);

            excelData.Load(excelOutputXml);
            File.Delete(excelOutputXml);
            return excelData;
        }
        
        private static void CreateNewXElement(DataSet execelDataset, string testPath)
        {
            var XElements = new List<XElement>();//cities
            var XElement = new List<XElement>();//city
            var valueXElement = new List<XElement>();//value

            var rowLength = new List<int>();
            var rowTotalCount = execelDataset.Tables[0].Rows.Count;
            var columnData = execelDataset.Tables[0].Columns;
            var columnList = new List<string>();

            for (int rowId = 0; rowId < rowTotalCount; rowId++)
            {

                var idRecord = 0;
                var rowList = execelDataset.Tables[0].Rows[rowId].ItemArray.ToList();

                var typeXElement = new List<XElement>();//type

                for (int columnId = 0; columnId < columnData.Count; columnId++)
                {
                    columnList.Add(columnData[columnId].ColumnName.Replace(" ", ""));
                    if (rowList[columnId].ToString().Length == 0)
                    {
                        var singleTypeXElement = new XElement(columnList[idRecord], valueXElement); //type->office
                        typeXElement.Add(singleTypeXElement);

                        valueXElement = new List<XElement>();
                        idRecord = columnId;
                        continue;
                    }
                    else
                    {
                        var tempName = columnList[columnId];
                        var tempValue = rowList[columnId];
                        var name = tempName;
                        if (tempName.Contains("$"))
                        {
                            name = tempName.Replace("Sheet2$.", "");
                        }
                        var singleXElement = new XElement(name, tempValue);
                        valueXElement.Add(singleXElement);//value
                    }
                }
                var singleCity = new XElement("City", new XAttribute("Name", rowList[0]), typeXElement);
                XElement.Add(singleCity);
            }

            var doc = new XDocument(new XElement("Cities", XElement));
            doc.Save(testPath);
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
    
