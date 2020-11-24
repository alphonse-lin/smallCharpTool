using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace utility
{
    public class ExcelToXml
    {
        public static void Excel2Xml(string excelFilePath, string exportXmlPath,string sheetTitle)
        {
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
        }
        
        private static void CreateNewXElement(DataSet execelDataset, string testPath)
        {
            var xElement = new List<XElement>();//city
            var valueXElement = new List<XElement>();//value

            var rowTotalCount = execelDataset.Tables[0].Rows.Count;
            var columnData = execelDataset.Tables[0].Columns;
            var columnList = new List<string>();

            for (int rowId = 0; rowId < rowTotalCount; rowId++)
            {
                var idRecord = 0;
                var rowList = execelDataset.Tables[0].Rows[rowId].ItemArray.ToList();
                var typeXElement = new List<XElement>();
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
                        if (tempName.Contains("$")) { name = tempName.Replace("Sheet2$.", ""); }
                        var value= tempValue.ToString().Replace(",", "");
                        var singleXElement = new XElement(name, (value.Replace(" - ",",")));
                        valueXElement.Add(singleXElement);//value
                    }
                }
                var singleCity = new XElement("City", new XAttribute("Name", rowList[0]), typeXElement);
                xElement.Add(singleCity);
            }
            var doc = new XDocument(new XElement("Cities", xElement));
            doc.Save(testPath);
        }
    }
}
