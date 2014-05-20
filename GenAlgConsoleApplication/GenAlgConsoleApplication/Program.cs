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
                    
                }
                if (a == "0")
                {
                    break;
                }
            }
        }
    }
}
