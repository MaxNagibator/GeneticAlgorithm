using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgConsoleApplication
{
    public class Lab3
    {
        // Написать программу, реализующую работу основных операторов мутации и их разновидностей для различных видов хромосом и схем:
        //а) простая мутация; //не нашёл
        //б) точечная мутация;
        //в) мутация обмена (одно- и двухточечная);+
        //г) мутация на основе принципа «золотого сечения»; +/?
        //д) мутация на основе чисел Фибоначчи; /?
        //е) инверсия;+
        //ж) транслокация;+
        //з) делеция.+

        private const int PERSON_COUNT = 2;
        private const int GEN_COUNT = 8;
        private static List<List<int>> _population;
        private static List<List<int>> _populationAfterKrossingover;
        private static List<List<int>> _tochkiRazriva; //этот же массив хранит маску, или точки мутации, вобщем вспомогательная еденица

        public static void Show()
        {
            _population = Worker.GenerateUniquePopulation(PERSON_COUNT, GEN_COUNT, 0, GEN_COUNT - 1);
            Worker.PopulationsShow("Начальные популяции", _population);
            MutationOdnotochechniy();
            MutationDvuhtochechniy();
            MutationInversiyaOdnoTochechnaya();
            MutationInversiyaDvuhTochechnaya();
            OperatorTranslokation();
            KrossingoverMutationTranslakation();
            MutationDeleciyaDvuhTochechnaya();
            MutationZolotoeSechenie();
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

        private static void OperatorTranslokation()
        {
            // эт я короче не то сделал
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
            Worker.PopulationsShowAfterAndBeforeKrossingover("Оператор транслокации (надо не надо?)",
                                                             _population, _populationAfterKrossingover, _tochkiRazriva);
        }

        private static void KrossingoverMutationTranslakation()
        {
            //Межхромосомные перестройки часто называют транслокациями. 
            //При этом участок хромосомы перемещается (транслоцируется) на другое место хромосомы. 
            //Выделяют следующие типы транслокаций.
            //Реципрокные – взаимный обмен участками негомологичных хромосом. 
            //В отличие от кроссинговера, при транслокации происходит обмен участков хромосом различной длины. Например,
            //P1: AB|CDEFGH
            //P2: MNO|PQR
            //P : MNOCDEFGH
            //P : ABPQR.

            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            var rnd = new Random();
            for (var i = 0; i < PERSON_COUNT; i += 2)
            {
                var tochkiRazrivaSub = new List<int>();
                tochkiRazrivaSub.Add(rnd.Next(0, GEN_COUNT - 1));
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
                    }
                }
                for (var j = tochkiRazrivaSub[1]; j < GEN_COUNT; j++)
                {
                    newGen.Add(_population[i + 1][j]);

                }
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j < tochkiRazrivaSub[1])
                    {
                        newGen2.Add(_population[i + 1][j]);
                    }
                }
                for (var j = tochkiRazrivaSub[0]; j < GEN_COUNT; j++)
                {
                    newGen2.Add(_population[i][j]);

                }
                _populationAfterKrossingover.Add(newGen);
                _populationAfterKrossingover.Add(newGen2);
            }
            Worker.PopulationsShowAfterAndBeforeKrossingover("После мутации/кроссинговера транслакация реципрокная",
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

        private static void MutationZolotoeSechenie()
        {
            //информации не нашёл, поэтому логически сделал, как и одноточечную мутацию, 
            //только точка является золотым сечение http://ru.wikipedia.org/wiki/%D0%97%D0%BE%D0%BB%D0%BE%D1%82%D0%BE%D0%B5_%D1%81%D0%B5%D1%87%D0%B5%D0%BD%D0%B8%D0%B5
            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();

            var zolotoeSechenieProportion = (Math.Sqrt(5) - 1) / 2;
            for (var i = 0; i < PERSON_COUNT; i++)
            {
                var zolotoeSechenie = (int)((GEN_COUNT + 1) * zolotoeSechenieProportion);
                var tochkiRazrivaSub = new List<int>();
                _tochkiRazriva.Add(tochkiRazrivaSub);
                var newGen = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    newGen.Add(_population[i][j]);
                }
                while (true)
                {
                    var c = newGen.Where(x => newGen.IndexOf(x) < zolotoeSechenie).ToList();
                    for (int x = newGen.Count - 1; x >= zolotoeSechenie; x--)
                    {
                        c.Add(newGen[x]);
                    }
                    newGen = c;
                    tochkiRazrivaSub.Add(zolotoeSechenie);
                    //Console.WriteLine("Точка сечения: " + zolotoeSechenie);
                    //Worker.PopulationShow(newGen);
                    //Console.WriteLine();
                    //Console.WriteLine("\r\n______________________________"); //можно раскоментрировать и посмотреть этапы мутации
                    if (zolotoeSechenie == 1)
                    {
                        break;
                    }
                    zolotoeSechenie = (int)Math.Round(zolotoeSechenie * zolotoeSechenieProportion);
                }
                _populationAfterKrossingover.Add(newGen);
            }
            Worker.PopulationsShowAfterAndBeforeMutation("После мутации одноточечной(золотое сечение)",
                                                         _population, _populationAfterKrossingover, _tochkiRazriva);
        }

    }
}
