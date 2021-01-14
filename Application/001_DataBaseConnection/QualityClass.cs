using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class FuncionClass
    {
        public Dictionary<string, List<QualityClass>> funcDic;
        private string funcName { get; set; }
        private QualityClass qualityData{get; set;}

        public FuncionClass(string FuncName, QualityClass QualityData)
        {
            funcName = FuncName;
            qualityData = QualityData;
        }

        private Dictionary AddIntoDic(string FuncName, QualityClass QualityData)
        {
            funcDic.Add(FuncName, QualityData);
        }
    }
    
    public class QualityClass
    {
        public string name { get; set; }
        public int priceMax { get; set; }
        public int priceMin { get; set; }

        public QualityClass(string Name, int PriceMax, int PriceMin)
        {
            name = Name;
            priceMax = PriceMax;
            priceMin = PriceMin;
        }

    }
}
