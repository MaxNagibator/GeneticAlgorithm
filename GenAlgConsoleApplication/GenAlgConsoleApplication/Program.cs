using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GenAlgConsoleApplication
{
    internal class Program
    {
        private const int PERSON_COUNT = 2;
        private const int GEN_COUNT = 8;
        private static List<List<int>> _population;
        private static List<List<int>> _populationAfterKrossingover;
        private static List<List<int>> _tochkiRazriva; //этот же массив хранит маску, или точки мутации, вобщем вспомогательная еденица
        //•	Одноточечного оператора +
        //•	Двухточечного +
        //•	Трёхточечного +
        //•	Универсального +
        //•	Упорядочивающего одно- и двухточечный операторы кроссинговера  +/-
        //•	Частично-соответствующего одно- и двухточечному операторам кроссинговера +/-
        //•	Циклического оператора +
        //•	«Жадного» оператора
        //•	Одно-, двух- и трёх точечного операторов на основе принципа «золотого сечения» и чисел Фибоначчи.

        // Написать программу, реализующую работу основных операторов мутации и их разновидностей для различных видов хромосом и схем:
        //а) простая мутация;
        //б) точечная мутация;
        //в) мутация обмена (одно- и двухточечная);+
        //г) мутация на основе принципа «золотого сечения»;
        //д) мутация на основе чисел Фибоначчи;
        //е) инверсия;+
        //ж) транслокация;+
        //з) делеция.+

        private static void Main(string[] args)
        {
            //пусть вся область всех генов у нас будет числа от 0 до 7
            //ВИД ГЕНА: числовой!
            _population = Worker.GenerateUniquePopulation(PERSON_COUNT, GEN_COUNT, 0, 7);
            Worker.PopulationsShow("Начальные популяции", _population);
            KrossingoverOdnotochechniy();
            KrossingoverDvuhtochechniy();
            KrossingoverTrehtochechniy();
            KrossingoverUporyadochenniyOdnotochechniy();
            KrossingoverChastichSootvetOdnotochechniy();
            KrossingoverCiklicheskiy();
            MutationOdnotochechniy();
            MutationDvuhtochechniy();
            MutationInversiyaOdnoTochechnaya();
            MutationInversiyaDvuhTochechnaya();
            KrossingoverMutationTranslakation();
            MutationDeleciyaDvuhTochechnaya();

            GenerateNotUniquePopulation(0, 1);
            Worker.PopulationsShow("Начальные популяции", _population);
            KrossingoverUniversal();
            KrossingoverZhadniy.Execute(2, 8);
        }

        private static void GenerateNotUniquePopulation(int minValue, int maxValue)
        {
            //берём случайные от всего решения и каждому гену присваивем случайное число
            _population = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                var gen = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    var currentValue = rnd.Next(minValue, maxValue+1);
                    gen.Add(currentValue);
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

            Worker.PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Одноточечного",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva);
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
            Worker.PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Двухточечного",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva);
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
            Worker.PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Трехточечного",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva);
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
                var b = newGen.Where(a => newGen.IndexOf(a) < tochkiRazrivaSub[0]).ToList();
                b.AddRange(newGen.Where(a => newGen.IndexOf(a) >= tochkiRazrivaSub[0]).OrderBy(g => g));
                var c = newGen2.Where(a => newGen2.IndexOf(a) < tochkiRazrivaSub[0]).ToList();
                c.AddRange(newGen2.Where(a => newGen2.IndexOf(a) >= tochkiRazrivaSub[0]).OrderBy(g => g));
                _populationAfterKrossingover.Add(b);
                _populationAfterKrossingover.Add(c);
            }
            Worker.PopulationsShowAfterAndBeforeKrossingover("После кроссинговера упорядоченного Одноточечного",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva);
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
            Worker.PopulationsShowAfterAndBeforeKrossingover(
                "После кроссинговера частично-соответствующего Одноточечного",
                _population, _populationAfterKrossingover, _tochkiRazriva);
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
            Worker.PopulationsShowAfterAndBeforeKrossingover("После кроссинговера Циклического",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva);
        }

        private static void MutationOdnotochechniy()
        {
            //При его реализации случайно выбирают ген в родительской хромосоме и, 
            //обменивая его на рядом расположенный ген, получают хромосому потомка

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i++)
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
            Worker.PopulationsShowAfterAndBeforeMutation("После мутации одноточечной",
                                                         _population, _populationAfterKrossingover, _tochkiRazriva);
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
                tochkiRazrivaSub.Add(rnd.Next(1, GEN_COUNT - 1));
                tochkiRazrivaSub.Add(rnd.Next(1, GEN_COUNT - 1));
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
            Worker.PopulationsShowAfterAndBeforeMutation("После мутации двухточечной",
                                                         _population, _populationAfterKrossingover, _tochkiRazriva);
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
                    if (j < tochkiRazrivaSub[0])
                    {
                        newGen.Add(_population[i][j]);
                    }
                    else
                    {
                        newGen.Add(_population[i][GEN_COUNT - j + tochkiRazrivaSub[0] - 1]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
            }
            Worker.PopulationsShowAfterAndBeforeMutation("После мутации инверсии одноточечной",
                                                         _population, _populationAfterKrossingover, _tochkiRazriva);
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
            Worker.PopulationsShowAfterAndBeforeMutation("После мутации инверсии двухточечной",
                                                         _population, _populationAfterKrossingover, _tochkiRazriva);
        }

        private static void KrossingoverMutationTranslakation()
        {
            //Языковая конструкция, позволяющая на основе скрещивания и инвертирования из пары 
            //родительских хромосом (или их частей) создавать две хромосомы потомков. 
            //Другими словами, он представляет собой комбинацию операторов кроссинговера и инверсии.
            //В процессе его реализации случайным образом производится один разрез в каждой хромосоме.
            //При формировании потомка Р’1 берется левая часть до разрыва из родителя Р1 и инверсия правой 
            //части до разрыва из Р2 .При создании Р’2 берется левая часть Р2 и инверсия правой части Р1. 

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT - 1));
                tochkiRazrivaSub.Sort();
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                var newGen2 = new List<int>();

                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j < tochkiRazrivaSub[0])
                    {
                        newGen.Add(_population[i][j]);
                        newGen2.Add(_population[i + 1][j]);
                    }
                    else
                    {
                        newGen.Add(_population[i + 1][GEN_COUNT - j + tochkiRazrivaSub[0] - 1]);
                        newGen2.Add(_population[i][GEN_COUNT - j + tochkiRazrivaSub[0] - 1]);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }
            Worker.PopulationsShowAfterAndBeforeKrossingover("После мутации/кроссинговера транслакация",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva);
        }

        private static void MutationDeleciyaDvuhTochechnaya()
        {
            //удаление внутреннего участка хромосомы

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i++)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(1, GEN_COUNT - 1));
                tochkiRazrivaSub.Add(rnd.Next(1, GEN_COUNT - 1));
                tochkiRazrivaSub.Sort();
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j < tochkiRazrivaSub[0] || j >= tochkiRazrivaSub[1])
                    {
                        newGen.Add(_population[i][j]);
                    }
                }
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j >= tochkiRazrivaSub[0] && j < tochkiRazrivaSub[1])
                    {
                        newGen.Add(-1);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
            }
            Worker.PopulationsShowAfterAndBeforeMutation("После мутации делеции двухточечной",
                                                         _population, _populationAfterKrossingover, _tochkiRazriva);
        }

        private static void KrossingoverUniversal()
        {
            //универсальный ОК определяют двоичную маску, длина которой равна длине заданных хромосом. 
            //Первый потомок получается сложением первого родителя с маской на основе следующих правил
            //(0+0=0, 0+1=1, 1+1=0). Второй потомок получается аналогичным образом. 

            var rnd = new Random();
            _populationAfterKrossingover = new List<List<int>>();

            _tochkiRazriva = new List<List<int>>();

            for (var i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)  //маску генерируем
                {
                    tochkiRazrivaSub.Add(rnd.Next(0, 2));
                }
                _tochkiRazriva.Add(tochkiRazrivaSub); //маску генерируем

                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (tochkiRazrivaSub[j] != _population[i][j])
                    {
                        newGen.Add(1);
                    }
                    else
                    {
                        newGen.Add(0);
                    }

                    if (tochkiRazrivaSub[j] != _population[i + 1][j])
                    {
                        newGen2.Add(1);
                    }
                    else
                    {
                        newGen2.Add(0);
                    }
                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }

            Worker.PopulationsShowAfterAndBeforeKrossingover("После кроссинговера унивесального",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva,
                                                             "маска=");
        }
    }
}
