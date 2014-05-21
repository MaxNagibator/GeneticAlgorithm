using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Vvedite 2, 3 ili 4, i nazhmite 'enter', pokazhetsya labaratornaya sootvetstvuyush'aya etoy zadache");
            while (true)
            {
                var a = Console.ReadLine();
                Console.Clear();
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
                if (a == "0")
                {
                    break;
                }
            }
        }
    }
}
