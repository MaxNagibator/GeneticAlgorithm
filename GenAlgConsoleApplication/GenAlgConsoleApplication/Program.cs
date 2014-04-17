using System;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private const int PERSON_COUNT = 10;
        private const int GEN_COUNT = 8;
        private static int[,] _population;

        private static void Main(string[] args)
        {
            //пусть вся область у нас будет числа от 0 до 255
            GenerateOdeyaloPopulation();
            PopulationShow("Odeyalo");
            GenerateDrobovikPopulation();
            PopulationShow("Drobovik");
            GenerateFocusPopulation();
            PopulationShow("Focus");
        }

        private static void GenerateOdeyaloPopulation()
        {
            //некоторая заданная область будет у нас от 15 до (15 + PERSON_COUNT)
            _population = new int[PERSON_COUNT, GEN_COUNT];
            const int startValue = 15;
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                var currentValue = startValue + i;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    _population[i, j] = currentValue%2;
                    currentValue = currentValue/2;
                }
            }
        }

        private static void GenerateDrobovikPopulation()
        {
            //берём случайные от всего решения
            _population = new int[PERSON_COUNT, GEN_COUNT];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                var currentValue = rnd.Next(0, 255);
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    _population[i, j] = currentValue%2;
                    currentValue = currentValue/2;
                }
            }
        }

        private static void GenerateFocusPopulation()
        {
            //берём случайные в выбранном диапозоне от 50 до 100
            _population = new int[PERSON_COUNT, GEN_COUNT];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                var currentValue = rnd.Next(50, 100);
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    _population[i, j] = currentValue%2;
                    currentValue = currentValue/2;
                }
            }
        }

        private static void PopulationShow(string str)
        {
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    Console.Write(_population[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
