using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class FuncionClass
    {
        public string funcName { get; set; }
        public QualityClass qualityData{get; set;}

        public FuncionClass(string FuncName, QualityClass QualityData)
        {
            funcName = FuncName;
            qualityData = QualityData;
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
