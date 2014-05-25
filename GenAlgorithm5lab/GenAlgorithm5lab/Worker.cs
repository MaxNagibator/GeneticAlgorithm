using System;
using System.Collections.Generic;

namespace GenAlgorithm5lab
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


        public static string PopulationsShow(string str, List<List<int>> population)
        {
            var textOut = str;
             textOut += Environment.NewLine;
            for (int i = 0; i < population.Count; i++)
            {
                textOut += i + ": ";
                textOut += PopulationShow(population[i]);
                textOut += Environment.NewLine;
            }
            return textOut;
        }

        public static string PopulationsShowAfterAndBeforeKrossingover(string str, List<List<int>> population, List<List<int>> populationAfterKrossingover, List<List<int>> tochkiRazriva, string razrivMessage = "точки разрыва=")
        {
            //показываю для каждой пары популяций до и после значения и точки разрыва
            var textOut = str;
            textOut += Environment.NewLine;
            for (int i = 0; i < population.Count; i += 2)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(population[i]);
                textOut+= Environment.NewLine;
                Console.Write(i + 1 + ": ");
                PopulationShow(population[i + 1]);
                textOut+= Environment.NewLine;
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
                textOut+= Environment.NewLine;
                Console.Write(i + 1 + ": ");
                PopulationShow(populationAfterKrossingover[i + 1]);
                textOut+="\r\n______________________________";
            }
            return textOut;
        }


        public static void PopulationsShowAfterAndBeforeMutation(string str, List<List<int>> population, List<List<int>> populationAfterKrossingover, List<List<int>> tochkiRazriva, string razrivMessage = "точки мутации=")
        {
            //показываю для каждой пары популяций до и после значения и точки разрыва
            var textOut = str;
            textOut += Environment.NewLine;
            for (int i = 0; i < population.Count; i++)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(population[i]);
                textOut+=Environment.NewLine;
                Console.Write("ПОСЛЕ: ");
                if (tochkiRazriva != null)
                {
                    if (tochkiRazriva[i].Count > 0)
                    {
                        Console.Write(razrivMessage);
                        for (int x = 0; x < tochkiRazriva[i].Count; x++)
                        {
                            Console.Write(" " + tochkiRazriva[i][x]);
                        }
                    }
                }
                Console.Write("\r\n" + i + ": ");
                PopulationShow(populationAfterKrossingover[i]);
                textOut+=Environment.NewLine;
                textOut+="\r\n______________________________";
            }
        }

        public static string PopulationShow(List<int> population)
        {
            var str = "";
            for (int j = 0; j < population.Count; j++)
            {
                if (population[j] != -1)
                {
                    str +=String.Format("{0,4} ",population[j]);
                }
            }
            return str;
        }
    }
}
