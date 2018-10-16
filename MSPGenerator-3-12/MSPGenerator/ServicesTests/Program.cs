
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicesTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing");

            /* //Testy pouziti sluzeb simulatoru
               //Console.WriteLine("Simulation TESTS:");
               //ServicesTests.SimulationService.SimulationTester.AlgortihmInfoTest();//test zjistovani informaci o implementovanych algoritmech
               
             */ ///SimulationTester.SimulationCreateSenateTests("INS-tester-1",true,50,20);//test vytvareni senatu na klientovi
               ServicesTests.SimulationService.SimulationTester.ExampleUsage(); //ukazkove pouziti

              //paralelni test-simulace behu vice klientu najednou
              /*Parallel.For(0, 10, (i) =>
              {
                var tester = new ServicesTests.SimulationService.SimulationParallelTester();
                tester.ClientRun("USER"+i.ToString());
             }
             );
            */
            
            //Testy použití služby generovani
            //ServicesTests.GeneratingService.GeneratorTester.ExampleUsage();
            //paralelni test-simulace behu vice klientu najednou
       /*   Parallel.For(0, 5, (i) =>
            {
                var tester = new ServicesTests.GeneratingService.GeneratorParallelTester();
                tester.ClientRun("USER" + i.ToString());
            });
           */ 
            Console.WriteLine("Stiskni klavesu");
            Console.ReadLine();
        }

       
    }

   
  

}
