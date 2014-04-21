using System;
using System.Collections.Generic;
using System.Globalization;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private const int PERSON_COUNT = 2;
        private const int GEN_COUNT = 8;
        private static List<List<int>> _population;
        private static List<List<int>> _populationAfterKrossingover;
        private static List<List<int>> _tochkiRazriva;
        //•	Одноточечного оператора +
        //•	Двухточечного +
        //•	Трёхточечного +
        //•	Универсального -
        //•	Упорядочивающего одно- и двухточечный операторы кроссинговера  +/-
        //•	Частично-соответствующего одно- и двухточечному операторам кроссинговера +/-
        //•	Циклического оператора +
        //•	«Жадного» оператора
        //•	Одно-, двух- и трёх точечного операторов на основе принципа «золотого сечения» и чисел Фибоначчи.

        // Написать программу, реализующую работу основных операторов мутации и их разновидностей для различных видов хромосом и схем:
        //а) простая мутация;
        //б) точечная мутация;
        //в) мутация обмена (одно- и двухточечная);
        //г) мутация на основе принципа «золотого сечения»;
        //д) мутация на основе чисел Фибоначчи;
        //е) инверсия;
        //ж) транслокация;
        //з) делеция.


        private static void Main(string[] args)
        {
            //пусть вся область всех генов у нас будет числа от 0 до 8
            //ВИД ГЕНА: числовой!
            GenerateDrobovikPopulation();
            PopulationsShow("Начальная популяция");
            KrossingoverOdnotochechniy();
            PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Одноточечного",1);
            KrossingoverDvuhtochechniy();
            PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Двухточечного",2);
            KrossingoverTrehtochechniy();
            PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Трехточечного", 3);
            KrossingoverUporyadochenniyOdnotochechniy();
            PopulationsShowAfterAndBeforeKrossingover("После кроссинговера упорядоченного Одноточечного",1);
            KrossingoverChastichSootvetOdnotochechniy();
            PopulationsShowAfterAndBeforeKrossingover("После кроссинговера частично-соответствующего Одноточечного",1);
            KrossingoverCiklicheskiy();
            PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Циклического",0);
            MutationOdnotochechniy();
            PopulationsShowAfterAndBeforeMutation("После мутации одноточечной", 1);
            MutationDvuhtochechniy();
            PopulationsShowAfterAndBeforeMutation("После мутации двухточечной", 2);
            MutationInversiyaOdnoTochechnaya();
            PopulationsShowAfterAndBeforeMutation("После мутации инверсии одноточечной", 1);
            MutationInversiyaDvuhTochechnaya();
            PopulationsShowAfterAndBeforeMutation("После мутации инверсии двухточечной", 2);
        }

        private static void GenerateDrobovikPopulation()
        {
            //берём случайные от всего решения и каждому гену присваивем случайное число
            _population = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                var gen = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    while (true)
                    {
                        var currentValue = rnd.Next(0, 8);
                        if (!gen.Exists(g => g == currentValue))
                        {
                            gen.Add(currentValue);
                            break;
                        }
                    }
                }
                _population.Add(gen);
            }
        }

        private static void KrossingoverOdnotochechniy()
        {
            //Перед началом работы одноточечного оператора кроссинговера определяется так называемая точка ОК, 
            //или разрезающая точка ОК, которая обычно определяется случайно.
            //Эта точка определяет место в двух хромосомах, где они должны быть «разрезаны».

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j >= tochkiRazrivaSub[0])
                    {
                        newGen.Add(_population[i + 1][j]);
                        newGen2.Add(_population[i][j]);
                    }
                    else
                    {
                        newGen.Add(_population[i][j]);
                        newGen2.Add(_population[i + 1][j]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }
        }

        private static void KrossingoverDvuhtochechniy()
        {
            //В каждой хромосоме определяются две точки ОК,
            //и хромосомы обмениваются участками, расположенными между двумя точками ОК. 
            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                tochkiRazrivaSub.Sort();
                _tochkiRazriva.Add(tochkiRazrivaSub);

                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (j >= tochkiRazrivaSub[0] && j < tochkiRazrivaSub[1])
                    {
                        newGen.Add(_population[i + 1][j]);
                        newGen2.Add(_population[i][j]);
                    }
                    else
                    {
                        newGen.Add(_population[i][j]);
                        newGen2.Add(_population[i + 1][j]);
                    }
                    _populationAfterKrossingover.Add(newGen);
                    _populationAfterKrossingover.Add(newGen2);
                }
            }
            Console.WriteLine();
        }

        private static void KrossingoverTrehtochechniy()
        {
            //В каждой хромосоме определяются две точки ОК,
            //и хромосомы обмениваются участками, расположенными между двумя точками ОК. 
            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                tochkiRazrivaSub.Sort();
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if ((j >= tochkiRazrivaSub[0] && j < tochkiRazrivaSub[1]) || j >= tochkiRazrivaSub[2])
                    {
                        newGen.Add(_population[i + 1][j]);
                        newGen2.Add(_population[i][j]);
                    }
                    else
                    {
                        newGen.Add(_population[i][j]);
                        newGen2.Add(_population[i + 1][j]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }
            Console.WriteLine();
        }

        private static void KrossingoverUporyadochenniyOdnotochechniy()
        {
            //Здесь «разрезающая» точка также выбирается случайно. Далее происходит копирование левого сегмента Р1 в Р'1. 
            //Остальные позиции в Р'1 берутся из Р2 в упорядоченном виде слева направо, 
            //исключая элементы, уже попавшие в Р'1. 
            //Получение Р'2 может выполняться различными способами. Наиболее распространенный метод копирования левого сегмента из Р2, 
            //а далее анализ Р1 методом, указанным выше. .
            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (j >= tochkiRazrivaSub[0])
                    {
                        for (int x = 0; x < GEN_COUNT; x++)
                            //поиск элемента из (популяции 2) которого ещё нету в популяции один, для добавления в (новую популяцию 1)
                        {
                            if (!newGen.Exists(g => g == _population[i + 1][x]))
                            {
                                newGen.Add(_population[i + 1][x]);
                                break;
                            }
                        }
                        for (int x = 0; x < GEN_COUNT; x++)
                            //поиск элемента из (популяции 1) которого ещё нету в популяции один, для добавления в (новую популяцию 2)
                        {
                            if (!newGen2.Exists(g => g == _population[i][x]))
                            {
                                newGen2.Add(_population[i][x]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        newGen.Add(_population[i][j]);
                        newGen2.Add(_population[i + 1][j]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }
            Console.WriteLine();
        }

        private static void KrossingoverChastichSootvetOdnotochechniy()
        {
            //Здесь также случайно выбирается «разрезающая» точка или точка ОК. 
            //Дальше анализируются сегменты (части) в обеих хромосомах и устанавливается 
            //частичное соответствие между элементами первого и второго родителей с формированием потомков. 
            //При этом правый сегмент P2 переносится в P'1, левый сегмент Р1 переносится в P'1 
            //с заменой повторяющихся генов на отсутствующие гены, находящиеся в частичном соответствии. 

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    if (j < tochkiRazrivaSub[0])
                    {
                        newGen.Add(-1);
                        newGen2.Add(-1);
                    }
                    else
                    {
                        newGen.Add(_population[i + 1][j]);
                        newGen2.Add(_population[i][j]);
                    }
                }
                for (int j = 0; j < tochkiRazrivaSub[0]; j++) //тут вобщем, надо языком лучше объяснить
                {
                    var index = j;
                    while (true)
                    {
                        if (!newGen.Exists(g => g == _population[i][index]))
                        {
                            newGen[j] = _population[i][index];
                            break;
                        }
                        index = newGen.IndexOf(_population[i][index]);
                    }
                }
                for (int j = 0; j < tochkiRazrivaSub[0]; j++)
                {
                    var index = j;
                    while (true)
                    {
                        if (!newGen2.Exists(g => g == _population[i + 1][index]))
                        {
                            newGen2[j] = _population[i + 1][index];
                            break;
                        }
                        index = newGen2.IndexOf(_population[i + 1][index]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }
        }

        private static void KrossingoverCiklicheskiy()
        {
            //Перед началом работы одноточечного оператора кроссинговера определяется так называемая точка ОК, 
            //или разрезающая точка ОК, которая обычно определяется случайно.
            //Эта точка определяет место в двух хромосомах, где они должны быть «разрезаны».

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT + 1));
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    newGen.Add(-1);
                    newGen2.Add(-1);
                }
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    newGen[_population[i][j]] = _population[i + 1][j];
                    newGen2[_population[i + 1][j]] = _population[i][j];
                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }
        }

        private static void MutationOdnotochechniy()
        {
            //При его реализации случайно выбирают ген в родительской хромосоме и, 
            //обменивая его на рядом расположенный ген, получают хромосому потомка

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i ++)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(1, GEN_COUNT));
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                        newGen.Add(_population[i][j]);
                }
                var stakan = newGen[tochkiRazrivaSub[0] - 1];
                newGen[tochkiRazrivaSub[0] - 1] = newGen[tochkiRazrivaSub[0]];
                newGen[tochkiRazrivaSub[0]] = stakan;

                _populationAfterKrossingover.Add(newGen);
            }
        }

        private static void MutationDvuhtochechniy()
        {
            //При реализации двухточечного ОМ случайным или направленным образом выбираются две точки разреза.
            //Затем производится перестановка генов между собой, расположенных справа от точек разреза. 

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i++)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(1, GEN_COUNT-1));
                tochkiRazrivaSub.Add(rnd.Next(1, GEN_COUNT-1));
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    newGen.Add(_population[i][j]);
                }
                var stakan = newGen[tochkiRazrivaSub[1]];
                newGen[tochkiRazrivaSub[1]] = newGen[tochkiRazrivaSub[0]];
                newGen[tochkiRazrivaSub[0]] = stakan;

                _populationAfterKrossingover.Add(newGen);
            }
        }

        private static void MutationInversiyaOdnoTochechnaya()
        {
            //Языковая конструкция, позволяющая на основе инвертирования родительской хромосомы 
            //(или ее части) создавать хромосому потомка. При его реализации случайным образом 
            //определяется одна или несколько точек разреза (инверсии), внутри которых элементы инвертируются.
            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i++)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT - 1));
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();

                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if(j<tochkiRazrivaSub[0])
                    {
                        newGen.Add(_population[i][j]);
                    }
                    else
                    {
                         newGen.Add(_population[i][GEN_COUNT-j+tochkiRazrivaSub[0]-1]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
            }
        }

        private static void MutationInversiyaDvuhTochechnaya()
        {
            //Языковая конструкция, позволяющая на основе инвертирования родительской хромосомы 
            //(или ее части) создавать хромосому потомка. При его реализации случайным образом 
            //определяется одна или несколько точек разреза (инверсии), внутри которых элементы инвертируются.
            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i++)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT - 1));
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT - 1));
                tochkiRazrivaSub.Sort();
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();

                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j < tochkiRazrivaSub[0] || j > tochkiRazrivaSub[1])
                    {
                        newGen.Add(_population[i][j]);
                    }
                    else
                    {
                        newGen.Add(_population[i][tochkiRazrivaSub[1] - j + tochkiRazrivaSub[0]]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
            }
        }

        private static void PopulationsShow(string str)
        {
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                Console.Write(i + ": ");
                PopulationShow(_population[i]);
                Console.WriteLine();
            }
        }

        private static void PopulationsShowAfterAndBeforeKrossingover(string str, int razrivCount)
        {
            //показываю для каждой пары популяций до и после значения и точки разрыва
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i += 2)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(_population[i]);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(_population[i + 1]);
                Console.WriteLine();
                Console.Write("ПОСЛЕ: ");
                if(razrivCount>0)
                {
                    Console.Write("точки разрыва=");
                    for (int x = 0; x < razrivCount; x++)
                    {
                        Console.Write(" " + _tochkiRazriva[i / 2][x]);
                    }
                }
                Console.Write("\r\n" + i + ": ");
                PopulationShow(_populationAfterKrossingover[i]);
                Console.WriteLine();
                Console.Write(i + 1 + ": ");
                PopulationShow(_populationAfterKrossingover[i + 1]);
                Console.WriteLine("\r\n______________________________");
            }
        }

        private static void PopulationsShowAfterAndBeforeMutation(string str, int mutationCount)
        {
            //показываю для каждой пары популяций до и после значения и точки разрыва
            Console.WriteLine(str);
            for (int i = 0; i < PERSON_COUNT; i ++)
            {
                Console.Write("ДО: \r\n" + i + ": ");
                PopulationShow(_population[i]);
                Console.WriteLine();
                Console.Write("ПОСЛЕ: ");
                if (mutationCount > 0)
                {
                    Console.Write("точки мутации=");
                    for (int x = 0; x < mutationCount; x++)
                    {
                        Console.Write(" " + _tochkiRazriva[i][x]);
                    }
                }
                Console.Write("\r\n" + i + ": ");
                PopulationShow(_populationAfterKrossingover[i]);
                Console.WriteLine();
                Console.WriteLine("\r\n______________________________");
            }
        }

        private static void PopulationShow(List<int> population)
        {
            for (int j = 0; j < GEN_COUNT; j++)
            {
                Console.Write("{0,4} ", population[j]);
            }
        }
    }
}
