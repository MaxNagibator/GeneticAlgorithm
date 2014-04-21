using System;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private const int PERSON_COUNT = 10;
        private const int GEN_COUNT = 8;
        private static int[,] _population;
        private static int[,] _populationAfterKrossingover;
        private static int[] _tochkiRazriva;
        //•	Одноточечного оператора
        //•	Двухточечного
        //•	Трёхточечного
        //•	Универсального
        //•	Упорядочивающего одно- и двухточечный операторы кроссинговера
        //•	Частично-соответствующего одно- и двухточечному операторам кроссинговера
        //•	Циклического оператора
        //•	«Жадного» оператора
        //•	Одно-, двух- и трёх точечного операторов на основе принципа «золотого сечения» и чисел Фибоначчи.

        private static void Main(string[] args)
        {
            //пусть вся область всех генов у нас будет числа от 0 до 255
            //ВИД ГЕНА: числовой!
            GenerateDrobovikPopulation();
            PopulationsShow("Начальная популяция");
            KrossingoverOdnotochechniy();
            PopulationsShowAfterKrossingover("После кроссинговера Одноточечного");
            KrossingoverDvuhtochechniy();
            PopulationsShowAfterKrossingoverDvuhtochechniy("После кроссинговера Двухточечного");
        }

        private static void GenerateDrobovikPopulation()
        {
            //берём случайные от всего решения и каждому гену присваивем случайное число
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

        private static void KrossingoverOdnotochechniy()
        {
            //Перед началом работы одноточечного оператора кроссинговера определяется так называемая точка ОК, 
            //или разрезающая точка ОК, которая обычно определяется случайно.
            //Эта точка определяет место в двух хромосомах, где они должны быть «разрезаны».
            _populationAfterKrossingover = new int[PERSON_COUNT, GEN_COUNT];
            _tochkiRazriva = new int[PERSON_COUNT/2];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkaRazriva = rnd.Next(0, GEN_COUNT + 1);
                _tochkiRazriva[i/2] = tochkaRazriva;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (tochkaRazriva > j)
                    {
                        _populationAfterKrossingover[i + 1, j] = _population[i, j];
                        _populationAfterKrossingover[i, j] = _population[i + 1, j];
                    }
                    else
                    {
                        _populationAfterKrossingover[i, j] = _population[i, j];
                        _populationAfterKrossingover[i + 1, j] = _population[i + 1, j];
                    }
                }
            }
            Console.WriteLine();
        }

        private static void KrossingoverDvuhtochechniy()
        {
            //В каждой хромосоме определяются две точки ОК,
            //и хромосомы обмениваются участками, расположенными между двумя точками ОК. 
            _populationAfterKrossingover = new int[PERSON_COUNT, GEN_COUNT];
            _tochkiRazriva = new int[PERSON_COUNT];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkaRazriva1 = rnd.Next(0, GEN_COUNT + 1);
                var tochkaRazriva2 = rnd.Next(0, GEN_COUNT + 1);
                if (tochkaRazriva1 > tochkaRazriva2)
                {
                    var stakan = tochkaRazriva2;
                    tochkaRazriva2 = tochkaRazriva1;
                    tochkaRazriva1 = stakan;
                }

                _tochkiRazriva[i] = tochkaRazriva1;
                _tochkiRazriva[i + 1] = tochkaRazriva2;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (tochkaRazriva2 > j && j >= tochkaRazriva1)
                    {
                        _populationAfterKrossingover[i + 1, j] = _population[i, j];
                        _populationAfterKrossingover[i, j] = _population[i + 1, j];
                    }
                    else
                    {
                        _populationAfterKrossingover[i, j] = _population[i, j];
                        _populationAfterKrossingover[i + 1, j] = _population[i + 1, j];
                    }
                }
            }
            Console.WriteLine();
        }

        private static void PopulationsShow(string str)
        {
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    Console.Write("{0,4} ", _population[i, j]); // {0,4} - это для отступов, чтобы красиво отображалось
                }
                Console.WriteLine();
            }
        }

        private static void PopulationsShowAfterKrossingover(string str)
        {
            //показываю для каждой пары популяций до и после значения и точку разрыва
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(i, _population);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(i + 1, _population);
                Console.WriteLine();
                Console.Write("ПОСЛЕ: точка разрыва=" + _tochkiRazriva[i/2] + "\r\n" + i + ": ");
                PopulationShow(i, _populationAfterKrossingover);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(i + 1, _populationAfterKrossingover);
                Console.WriteLine("\r\n______________________________");
            }
        }

        private static void PopulationsShowAfterKrossingoverDvuhtochechniy(string str)
        {
            //показываю для каждой пары популяций до и после значения и точки разрыва
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(i, _population);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(i + 1, _population);
                Console.WriteLine();
                Console.Write("ПОСЛЕ: точки разрыва=" + _tochkiRazriva[i] + " " + _tochkiRazriva[i + 1] + "\r\n" + i +
                              ": ");
                PopulationShow(i, _populationAfterKrossingover);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(i + 1, _populationAfterKrossingover);
                Console.WriteLine("\r\n______________________________");
            }
        }

        private static void PopulationShow(int i, int[,] population)
        {
            for (int j = 0; j < GEN_COUNT; j++)
            {
                Console.Write("{0,4} ", population[i, j]);
            }
        }
    }
}
