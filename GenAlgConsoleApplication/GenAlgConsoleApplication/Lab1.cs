using System;

namespace GenAlgConsoleApplication
{
    public class Lab1
    {
        private const int PERSON_COUNT = 1;
        private const int GEN_COUNT = 8;
        private static int[,] _population;

        public static void Show()
        {
            Console.WriteLine("chislovie:");
            GenerateOdeyaloPopulation();
            PopulationShow("Odeyalo");
            Console.WriteLine();
            GenerateDrobovikPopulation();
            PopulationShow("Drobovik");
            Console.WriteLine();
            GenerateFocusPopulation();
            PopulationShow("Focus");
            Console.WriteLine();
            Lab1PopulationTypeGeneration.Show();
            Console.ReadLine();
        }

        private static void GenerateOdeyaloPopulation()
        {
            _population = new int[PERSON_COUNT, GEN_COUNT];
            var interval = 100;
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    _population[i, j] = interval / (2 * PERSON_COUNT) + interval / PERSON_COUNT * j;
                }
            }
        }

        private static void GenerateDrobovikPopulation()
        {
            _population = new int[PERSON_COUNT, GEN_COUNT];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    var currentValue = rnd.Next(0, 255);
                    _population[i, j] = currentValue;
                }
            }
        }

        private static void GenerateFocusPopulation()
        {
            _population = new int[PERSON_COUNT, GEN_COUNT];
            var rnd = new Random();
            const int t = 100;
            var a = (int)(t * 0.25 + rnd.Next((int)(t * 0.5)));
            var k = a / (2 * GEN_COUNT);
            for (int x = 0; x < GEN_COUNT / 2; x++)
            {
                var y = 2 * x + k;
                if ((a + y) < t)
                {
                    _population[0, x] = a + y;
                }
            }
            for (int x = GEN_COUNT / 2; x < GEN_COUNT; x++)
            {
                var y = 2 * (x - GEN_COUNT / 2 + 1) + k;
                if ((a - y) >= 0)
                {
                    _population[0, x] = a - y;
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
                    Console.Write(_population[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
