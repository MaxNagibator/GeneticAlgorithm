using System;
using System.Globalization;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private const int PERSON_COUNT = 10;
        private const int GEN_COUNT = 8;
        private static int[,] _population;
        private static int[,] _populationAfterKrossingover;
        private static int[,] _tochkiRazriva;
        //•	Одноточечного оператора +
        //•	Двухточечного +
        //•	Трёхточечного +
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
            PopulationsShowAfterKrossingoverOdnotochechniy("После кроссинговера Одноточечного");
            KrossingoverDvuhtochechniy();
            PopulationsShowAfterKrossingoverDvuhtochechniy("После кроссинговера Двухточечного");
            KrossingoverTrehtochechniy();
            PopulationsShowAfterKrossingoverTrehtochechniy("После кроссинговера Трехточечного");
            KrossingoverUporyadochenniyOdnotochechniy();
            PopulationsShowAfterKrossingoverOdnotochechniy("После кроссинговера упорядоченного Одноточечного");
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
            _tochkiRazriva = new int[PERSON_COUNT/2,1];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkaRazriva = rnd.Next(0, GEN_COUNT + 1);
                _tochkiRazriva[i/2,0] = tochkaRazriva;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (j >= tochkaRazriva)
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
            _tochkiRazriva = new int[PERSON_COUNT/2, 2];
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
                _tochkiRazriva[i/2, 0] = tochkaRazriva1;
                _tochkiRazriva[i/2, 1] = tochkaRazriva2;

                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (j >= tochkaRazriva1 && j < tochkaRazriva2)
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

        private static void KrossingoverTrehtochechniy()
        {
            //В каждой хромосоме определяются две точки ОК,
            //и хромосомы обмениваются участками, расположенными между двумя точками ОК. 
            _populationAfterKrossingover = new int[PERSON_COUNT, GEN_COUNT];
            _tochkiRazriva = new int[PERSON_COUNT, 3];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                _tochkiRazriva[i/2, 0] = rnd.Next(0, GEN_COUNT + 1);
                _tochkiRazriva[i/2, 1] = rnd.Next(0, GEN_COUNT + 1);
                _tochkiRazriva[i/2, 2] = rnd.Next(0, GEN_COUNT + 1);
                _tochkiRazriva = SortArray(_tochkiRazriva, i/2);
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if ((j >= _tochkiRazriva[i/2, 0] && j < _tochkiRazriva[i/2, 1]) || j >= _tochkiRazriva[i/2, 2])
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

        private static int[,] SortArray(int[,] array, int i)
        {
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2 - x; y++)
                {
                    if (array[i, y] > array[i, y + 1])
                    {
                        var stakan = _tochkiRazriva[i, y];
                        array[i, y] = array[i, y + 1];
                        array[i, y + 1] = stakan;
                    }
                }
            } // сортирую точки разрыва пузырьком 
            return array;
        }

        private static void KrossingoverUporyadochenniyOdnotochechniy()
        {
            //Здесь «разрезающая» точка также выбирается случайно. Далее происходит копирование левого сегмента Р1 в Р'1. 
            //Остальные позиции в Р'1 берутся из Р2 в упорядоченном виде слева направо, 
            //исключая элементы, уже попавшие в Р'1. 
            //Получение Р'2 может выполняться различными способами. Наиболее распространенный метод копирования левого сегмента из Р2, 
            //а далее анализ Р1 методом, указанным выше. Тогда имеем P'2 : (G A B E | C D F H).
            _populationAfterKrossingover = new int[PERSON_COUNT, GEN_COUNT];
            _tochkiRazriva = new int[PERSON_COUNT/2, 1];
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkaRazriva = rnd.Next(0, GEN_COUNT + 1);
                _tochkiRazriva[i/2, 0] = tochkaRazriva;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (j >= tochkaRazriva)
                    {
                        for (int x = 0; x < j; x++) //поиск элемента из (популяции 2) которого ещё нету в популяции один, для добавления в (новую популяцию 1)
                        {
                            var isExists = false;
                            for (int y = 0; y < j; y++)
                            {
                                if (_populationAfterKrossingover[i, y] == _population[i + 1, x])
                                {
                                    isExists = true;
                                }
                            }
                            if (isExists == false)
                            {
                                _populationAfterKrossingover[i, j] = _population[i + 1, x];
                                break;
                            }
                        }
                        for (int x = 0; x < j; x++) //поиск элемента из (популяции 1) которого ещё нету в популяции один, для добавления в (новую популяцию 2)
                        {
                            var isExists = false;
                            for (int y = 0; y < j; y++)
                            {
                                if (_populationAfterKrossingover[i + 1, y] == _population[i, x])
                                {
                                    isExists = true;
                                }
                            }
                            if (isExists == false)
                            {
                                _populationAfterKrossingover[i + 1, j] = _population[i, x];
                                break;
                            }
                        }
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

        private static void PopulationsShowAfterKrossingoverOdnotochechniy(string str)
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
                Console.Write("ПОСЛЕ: точка разрыва=" + _tochkiRazriva[i/2,0] + "\r\n" + i + ": ");
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
                Console.Write("ПОСЛЕ: точки разрыва=" + _tochkiRazriva[i/2,0] + " " + _tochkiRazriva[i/2,1] + "\r\n" + i +
                              ": ");
                PopulationShow(i, _populationAfterKrossingover);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(i + 1, _populationAfterKrossingover);
                Console.WriteLine("\r\n______________________________");
            }
        }

        private static void PopulationsShowAfterKrossingoverTrehtochechniy(string str)
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
                Console.Write("ПОСЛЕ: точки разрыва=" + _tochkiRazriva[i / 2, 0] + " " + _tochkiRazriva[i / 2, 1] + " " + _tochkiRazriva[i / 2, 2] + "\r\n" + i +
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
