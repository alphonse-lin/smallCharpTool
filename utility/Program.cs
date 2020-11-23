using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.IO;
using System.Data.OleDb;

namespace utility
{
    class Program
    {
        static void Main(string[] args)
        {
            string excelPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\造价表2.xlsx";
            string xmlPath = @"E:\114_temp\008_代码集\002_extras\smallCharpTool\utility\data\造价表2_temp.xml";
            //var ds= ExcelToDS(excelPath);

            ReadExcel2Xml(excelPath, xmlPath);
            Console.WriteLine("finished");
            Console.ReadLine();
        }

        private static void ReadExcel2XmlCfgAndDoExchange(string filePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            if (xmlDoc == null)
            {
                Console.WriteLine("Read Excel2Xml Failed!");
                return;
            }
            XmlNodeList resList = xmlDoc.GetElementsByTagName("item");
            for (int i = 0; i < resList.Count; i++)
            {
                XmlNode node = resList.Item(i);
                string ExcelPath = node.Attributes["excelPath"].Value;
                string XmlPath = node.Attributes["xmlPath"].Value;
                XmlDocument outXmlDoc = Excel2Xml(ExcelPath);
                CreateXml(outXmlDoc, XmlPath);
            }
        }

        private static void ReadExcel2Xml(string ExcelPath, string XmlPath)
        {
            XmlDocument outXmlDoc = Excel2Xml(ExcelPath);
            CreateXml(outXmlDoc, XmlPath);
        }

        private static void CreateXml(XmlDocument xmlDoc, string xmlName)
        {
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.NewLineHandling = NewLineHandling.Entitize;
            ws.Encoding = System.Text.Encoding.UTF8;
            ws.Indent = true;
            XmlWriter writer = XmlWriter.Create(xmlName, ws);
            writer.WriteStartElement("root");
            XmlNodeList resList = xmlDoc.GetElementsByTagName("item");
            for (int i = 0; i < resList.Count; ++i)
            {
                XmlNode node = resList.Item(i);
                //写入子节点
                writer.WriteStartElement("item");
                System.Collections.IEnumerator erator = node.GetEnumerator();
                while (erator.MoveNext())
                {
                    //写入属性
                    writer.WriteAttributeString(((XmlNode)erator.Current).Name, ((XmlNode)erator.Current).InnerText);
                }
                writer.WriteEndElement();
            }
            writer.Close();
        }

        private static XmlDocument Excel2Xml(string excelFilePath)
        {
            XmlDocument excelData = new XmlDocument();
            DataSet excelTableDataSet = new DataSet();
            StreamReader excelContent = new StreamReader(excelFilePath, System.Text.Encoding.Default);
            string stringConnectToExcelFile = string.Format("provider=Microsoft.Jet.OLEDB.4.0;data source={0};Extended Properties=Excel 8.0;", excelFilePath);
            System.Data.OleDb.OleDbConnection oleConnectionToExcelFile = new System.Data.OleDb.OleDbConnection(stringConnectToExcelFile);
            System.Data.OleDb.OleDbDataAdapter oleDataAdapterForGetExcelTable = new System.Data.OleDb.OleDbDataAdapter(
                string.Format("select * from [Sheet2$]"), oleConnectionToExcelFile);
            try
            {
                oleDataAdapterForGetExcelTable.Fill(excelTableDataSet, "item");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("error:" + ex.Message);
            }
            string excelOutputXml = "tmp.xml";
            excelTableDataSet.WriteXml(excelOutputXml);

            excelData.Load(excelOutputXml);
            File.Delete(excelOutputXml);
            return excelData;
        }

        private static DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }
    }
}
