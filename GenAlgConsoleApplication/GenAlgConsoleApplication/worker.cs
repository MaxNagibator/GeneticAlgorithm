using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgConsoleApplication
{
    public class Worker
    {
        public static List<List<int>> GenerateUniquePopulation(int personCount, int genCount, int minValue, int maxValue)
        {
            //берём случайные от всего решения и каждому гену присваивем случайное число
            var population = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < personCount; i++)
            {
                var gen = new List<int>();
                for (int j = 0; j < genCount; j++)
                {
                    while (true)
                    {
                        var currentValue = rnd.Next(minValue, maxValue + 1);
                        if (!gen.Exists(g => g == currentValue))
                        {
                            gen.Add(currentValue);
                            break;
                        }
                    }
                }
                population.Add(gen);
            }
            return population;
        }


        public static void PopulationsShow(string str, List<List<int>> population)
        {
            Console.WriteLine(str);
            for (int i = 0; i < population.Count; i++)
            {
                Console.Write(i + ": ");
                PopulationShow(population[i]);
                Console.WriteLine();
            }
        }

        public static void PopulationsShowAfterAndBeforeKrossingover(string str, List<List<int>> population, List<List<int>> populationAfterKrossingover, List<List<int>> tochkiRazriva, string razrivMessage = "точки разрыва=")
        {
            //показываю для каждой пары популяций до и после значения и точки разрыва
            Console.WriteLine(str);
            for (int i = 0; i < population.Count; i += 2)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(population[i]);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(population[i + 1]);
                Console.WriteLine();
                Console.Write("ПОСЛЕ: ");
                if (tochkiRazriva[i / 2].Count > 0)
                {
                    Console.Write(razrivMessage);
                    for (int x = 0; x < tochkiRazriva[i / 2].Count; x++)
                    {
                        Console.Write(" " + tochkiRazriva[i / 2][x]);
                    }
                }
                Console.Write("\r\n" + i + ": ");
                PopulationShow(populationAfterKrossingover[i]);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(populationAfterKrossingover[i + 1]);
                Console.WriteLine("\r\n______________________________");
            }
        }


        public static void PopulationsShowAfterAndBeforeMutation(string str, List<List<int>> population, List<List<int>> populationAfterKrossingover, List<List<int>> tochkiRazriva, string razrivMessage = "точки мутации=")
        {
            //показываю для каждой пары популяций до и после значения и точки разрыва
            Console.WriteLine(str);
            for (int i = 0; i < population.Count; i++)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(population[i]);
                Console.WriteLine();
                Console.Write("ПОСЛЕ: ");
                if (tochkiRazriva[i].Count > 0)
                {
                    Console.Write(razrivMessage);
                    for (int x = 0; x < tochkiRazriva[i].Count; x++)
                    {
                        Console.Write(" " + tochkiRazriva[i][x]);
                    }
                }
                Console.Write("\r\n" + i + ": ");
                PopulationShow(populationAfterKrossingover[i]);
                Console.WriteLine();
                Console.WriteLine("\r\n______________________________");
            }
        }

        public static void PopulationShow(List<int> population)
        {
            for (int j = 0; j < population.Count; j++)
            {
                Console.Write("{0,4} ", population[j]);
            }
        }

    }
}
