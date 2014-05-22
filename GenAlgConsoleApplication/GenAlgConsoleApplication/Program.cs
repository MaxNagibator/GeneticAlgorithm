using System;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Lab5.Show();
            Console.WriteLine("Vvedite 1, 2, 3 ili 4, i nazhmite 'enter', pokazhetsya labaratornaya sootvetstvuyush'aya etoy zadache");
            while (true)
            {
                var a = Console.ReadLine();
                Console.Clear();
                if (a == "1")
                {
                    Lab1.Show();
                }
                if (a == "2")
                {
                    Lab2.Show();
                }
                if (a == "3")
                {
                    Lab3.Show();
                }
                if (a == "4")
                {
                    Lab4.Show();
                }
                if (a == "5")
                {
                    Lab5A.Show();
                }
                if (a == "0")
                {
                    break;
                }
            }
        }
    }
}
