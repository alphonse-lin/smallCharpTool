using System;
using System.Collections.Generic;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using MicroPPt=Microsoft.Office.Interop.PowerPoint;

namespace Application.Office
{
    public class PowerPointHelper
    {
        public static string ReplacePowerPoint(string filePath, Dictionary<string,string>keyValues)
        {
            string path = "";
            MicroPPt.Application pptApp;
            Presentations presentations = null;
            Presentation presentation = null;
            pptApp = new MicroPPt.Application();
            try
            {
                presentations = pptApp.Presentations;
                //打开ppt
                presentation = presentations.Open(filePath, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                //替换模板ppt中的文本
                foreach (MicroPPt.Slide slide in presentation.Slides)
                {
                    foreach (MicroPPt.Shape shape in slide.Shapes)
                    {
                        ReplaceShapeText();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 替换PowerPoint TextBox的内容
        /// </summary>
        public static void ReplaceShapeText()
        {
            throw new NotImplementedException();
        }
    }
}
