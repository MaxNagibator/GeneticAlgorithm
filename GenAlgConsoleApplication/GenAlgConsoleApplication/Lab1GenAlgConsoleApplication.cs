using System;

namespace GenAlgConsoleApplication
{
    internal class Lab1PopulationTypeGeneration
    {
        private const int PERSON_COUNT = 2;
        private const int GEN_COUNT = 6;
        private const int VEKTOR_LENGTH = 3;
        private static int[,] _population;
        private static int[, ,] _populationVektor;

        public static void Show()
        {
            GenerateBinaryPopulation();
            PopulationShow("binarnie");
            GenerateChisloviePopulation();
            PopulationShow("chislovie");
            GenerateVektorniePopulation();
            PopulationVektorShow("vektornie");
        }

        private static void GenerateBinaryPopulation()
        {
            _population = new int[PERSON_COUNT, GEN_COUNT];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    var currentValue = rnd.Next(0, 2);
                    _population[i, j] = currentValue;
                }
            }
        }

        private static void GenerateChisloviePopulation()
        {
            _population = new int[PERSON_COUNT, GEN_COUNT];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    var currentValue = rnd.Next(0, 99);
                    _population[i, j] = currentValue;
                }
            }
        }

        private static void PopulationShow(string str)
        {
            Console.WriteLine();
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    Console.Write(_population[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static void GenerateVektorniePopulation() //Генерация векторных популяций
        {
            _populationVektor = new int[PERSON_COUNT, GEN_COUNT, VEKTOR_LENGTH];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    for (int k = 0; k < VEKTOR_LENGTH; k++)
                    {
                        var currentValue = rnd.Next(0, 99);
                        _populationVektor[i, j, k] = currentValue;
                    }
                }
            }
        }

        private static void PopulationVektorShow(string str)
        {
            Console.WriteLine();
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    Console.Write("(");
                    for (int k = 0; k < VEKTOR_LENGTH; k++)
                    {
                        Console.Write(_populationVektor[i, j, k]);
                        if (k < VEKTOR_LENGTH - 1)
                        {
                            Console.Write(";");
                        }
                    }
                    Console.Write(") ");
                }
                Console.WriteLine();
            }
        }
    }
}
