using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace UrbanX.Calculation
{
    class Program
    {
        static void Main(string[] args)
        {
            var mean = 10d;
            var min = -10d;
            var max = 40d;
            var count = 20;
            var seed = DateTime.Now.Second;
            var result=StatisticsModel.NormalDistribution(mean, min, max, count, seed);

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            
            Console.WriteLine("Mean = {0}\nSum = {1}", result.Average(), result.Sum());
            Console.ReadLine();
        }
    }
}

