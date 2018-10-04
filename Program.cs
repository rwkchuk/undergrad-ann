//Robert Kowalchuk
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ann_2
{
    class Program
    {
        //Main file
        static void Main(string[] args)
        {
            //one Read_From_Example_File per file
            Read_From_Example_File TestFile = new Read_From_Example_File("Test.txt", 65);
            Read_From_Example_File TranFile = new Read_From_Example_File("Train.txt", 65);
            //create Netwrok 
            Network Dan = new Network(2, 64, 10, 16, 0.2f);
            
            TestFile.ReadFile();

            TranFile.ReadFile();

            Console.WriteLine("Test 0 no training " + " ---------------------------------------------------------------- ");
            Dan.Forward_Propagation(TestFile.Examples);

            int Num_Trains = 3;
            for(int i=0; i<Num_Trains; i++)
            {
                Console.WriteLine("Test " + (i+1) + " ---------------------------------------------------------------- ");
                Dan.Forward_Back_Propagation(TranFile.Examples);
                Dan.Forward_Propagation(TestFile.Examples);
            }
        }
    }
}
