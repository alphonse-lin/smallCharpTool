//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace utility._001_DataBaseConnection
//{
//    enum FunctionType
//    {
//        office,
//        shopping_center,
//        residential_highrise,
//        residential_house,
//        hotel,
//        industrial,
//        carpark
//    }
//    public class CityInfoIntoPG
//    {
//        private ConstructionCostClass _cityInfo;

//        private string _name;

//        public string Name
//        {
//            get { return _name; }
//            set { _name = value; }
//        }

//        private int _year;

//        public int Year
//        {
//            get { return _year; }
//            set { _year = value; }
//        }

//        private string _city_id;

//        public string CityId
//        {
//            get { return _city_id; }
//            set { _city_id = value; }
//        }

//        private int _func_id;

//        public int FuncId
//        {
//            get { return _func_id; }
//            set { _func_id = value; }
//        }

//        private double _price_min;

//        public double PriceMin
//        {
//            get { return _price_min; }
//            set { _price_min = value; }
//        }

//        private double _price_max;
//        public double PriceMax
//        {
//            get { return _price_max; }
//            set { _price_max = value; }
//        }

//        public CityInfoIntoPG(ConstructionCostClass cityInfo)
//        {
//            _cityInfo = cityInfo;
//        }

//        public CityInfoIntoPG Parse(ConstructionCostClass cityInfo, FunctionType func_type)
//        {
//            switch (func_type)
//            {
//                case FunctionType.office:

//                    break;
//            }
//        }


//    }
//}
