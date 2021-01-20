﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace UrbanX.Application
{
    public static class ToolManagers
    {
        public static void CreateKMLFile(string destPath, string savePath, Point pointMin, Point pointMax, int rowCount, int columnCount)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(destPath);
            XmlElement root = xmldoc.DocumentElement;
            XmlNode _document = root.GetElementsByTagName("Document")[0];
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            if (_document == null || _document.Attributes["xmlns"] == null)
            {
                nsmgr.AddNamespace("ns", root.Attributes["xmlns"].Value);
            }
            else
            {
                nsmgr.AddNamespace("ns", _document.Attributes["xmlns"].Value);
            }

            XmlNodeList xmlmark = root.GetElementsByTagName("Placemark");
            for (int m = 0; m < xmlmark.Count; m++)
            {
                XmlNodeList xmlmarkChilds = xmlmark[m].ChildNodes;
                for (int n = 0; n < xmlmarkChilds.Count; n++)
                {
                    XmlNode node = xmlmarkChilds[n];
                    if (node.Name == "LineString" || node.Name == "LineRing")
                    {
                        XmlNode coordsNode = node.FirstChild;
                        while (coordsNode != null && coordsNode.Name != "coordinates")
                        {
                            coordsNode = coordsNode.NextSibling;
                        }
                        if (coordsNode == null)
                            continue;

                        //添加属性
                        var inputStr= ConvertPtIntoStr(CreatePoint(pointMin, pointMax, rowCount, columnCount));
                        coordsNode.InnerXml = inputStr;
                        xmldoc.Save(savePath);
                    }
                }
            }
        }

        private static string ConvertPtIntoStr(List<Point> ptList)
        {
            StringBuilder result = new StringBuilder();
            
            for (int i = 0; i < ptList.Count; i++)
            {
                var singlePt = "";
                if (i==0)
                {
                    singlePt = string.Format("{0},{1},", ptList[i].X, ptList[i].Y);
                }
                else if (i == ptList.Count - 1)
                {
                    singlePt = string.Format("0 {0},{1},0 ", ptList[i].X, ptList[i].Y);
                }
                else
                {
                    singlePt = string.Format("0 {0},{1},", ptList[i].X, ptList[i].Y);
                }
                result.Append(singlePt);
            }
            return result.ToString() ;
        }

        private static List<Point> CreatePoint(Point ptLeftBtm, Point ptRightUp, int rowCount=10,int columnCount=10)
        {
            var xmin = ptLeftBtm.X;
            var ymin = ptLeftBtm.Y;
            var xmax = ptRightUp.X;
            var ymax = ptRightUp.Y;

            var columnStep = Math.Abs(xmax - xmin) / rowCount;
            var rowStep = Math.Abs(ymax - ymin) / columnCount;

            var totalCount = columnCount * rowCount;
            List<Point> ptResult = new List<Point>(totalCount);
            
            for (int row = 0; row < rowCount; row++)
            {
                var pointX = xmin + rowStep * row;
                for (int column = 0; column < columnCount; column++)
                {
                    var pointY = ymin + columnStep * column;
                    ptResult.Add(new Point(pointX, pointY));
                }
            }
            return ptResult;
        }

        public static XmlAttribute CreateAttribute(XmlNode node, string attributeName, string value)
        {
            try
            {
                XmlDocument doc = node.OwnerDocument;
                XmlAttribute attr = null;
                attr = doc.CreateAttribute(attributeName);
                attr.Value = value;
                node.Attributes.SetNamedItem(attr);
                return attr;
            }
            catch (Exception err)
            {
                string desc = err.Message;
                return null;
            }
        }

        private static string[] ExtractInnverText(XmlNode coordsNode)
        {
            // 思路1 ：用正则表达式去除字符串首位的制表符、换行符等符号，然后用' '来划分为string[]
            string tt = coordsNode.InnerText;
            Regex reg = new Regex("\f|\n|\r|\t");
            string modified = reg.Replace(tt, "");
            string[] ss = modified.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//清除空格
            return ss;
        }
    }
}
