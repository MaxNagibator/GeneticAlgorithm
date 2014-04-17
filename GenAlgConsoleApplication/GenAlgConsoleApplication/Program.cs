using System;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private const int PERSON_COUNT = 10;
        private const int GEN_COUNT = 2;
        private static int[,] _population;

        private static void Main(string[] args)
        {
            //пусть вся область первого гена у нас будет числа от 0 до 255
            //значение второго гена у нас будет от 50 до 100
            GenerateOdeyaloPopulation();
            PopulationShow("Odeyalo");
            GenerateDrobovikPopulation();
            PopulationShow("Drobovik");
            GenerateFocusPopulation()   ;
            PopulationShow("Focus");
        }

        /* 1. Выбрать диапазон десятичных значений (напр. - от 15 до 25)
            2. Создать цикл. Длина цикла - количество этих значений (особей).
               В каждой итерации цикла закодировать это десятичное число, представляющее особь,
               в двоичном формате. Т.о., получаем двоичный вектор, являющийся геномом особи.
         * 
         *  УТОЧНИТЬ: 
         *  Делается выборка непрерывного диапазона или можно из разных частей?
         *  Можно брать выборку не всех значений?
         */
        private static void GenerateOdeyaloPopulation()
        {
            //некоторая заданная область будет у нас от 15 до (15 + PERSON_COUNT)
            _population = new int[PERSON_COUNT,GEN_COUNT];
            const int startValue = 15;
            const int start2GenValue = 65; //значение второго гена от 65 до 75
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                // особь из 2-х генов. Если их должно быть больше, соответственно, изменить колич. процедур.
                // в данный момент получаем десятичные значения. Если нужно кодировать (1-й вариант), 
                // - преобразовать в двоичный формат.
                _population[i, 0] = startValue + i;
                _population[i, 1] = start2GenValue + i;
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
                var current2GenValue = rnd.Next(50, 100);
                _population[i, 0] = currentValue;
                _population[i, 1] = current2GenValue;
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
                var current2GenValue = rnd.Next(70, 93);
                _population[i, 0] = currentValue;
                _population[i, 1] = current2GenValue;
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
                    Console.Write(_population[i, j]+" ");
                }
                Console.WriteLine();
            }
        }
    }
}
