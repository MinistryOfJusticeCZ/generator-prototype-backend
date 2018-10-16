using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SelectionsAlgorithms;


namespace tester1
{
    class Program
    {
        static void Main(string[] args)
        {
            //test funkcnosti iteratoru pres algoritmy
            foreach (var item in AlgorithmFactory.Algorithms)
                item.AssignCase();
            //test funkcnosti iteratoru pres popisy algoritmu
            foreach(var item in AlgorithmFactory.Descriptions)
                Console.WriteLine(item);
            try
            {
                AlgorithmFactory.GetSelectionAlgorithm(1).AssignCase();
                AlgorithmFactory.GetSelectionAlgorithm(10).AssignCase();
            }
            catch(KeyNotFoundException e)
            {
                Console.WriteLine("Exception OK: {0}", e.Message);
            }
             
          
            Console.ReadKey();
        }
    }
}
