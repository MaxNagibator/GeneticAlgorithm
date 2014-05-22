using System;
using System.Collections.Generic;
using System.Linq;

namespace GenAlgConsoleApplication
{
    public class Lab5A
    {
        //3.1. Выполнить одно из заданий к лабораторной работе и построить на основе описаний ГА Холланда, Голдберга и Девиса ПГА вычисления min (max) функции:
        //а) min функции f(x) = 5x3 — 4 на интервале [1, 2, 3, 4, 5];
        //Размер начальной популяции — 10, формирование начальной популяции — по выбору пользователя, вероятность кроссинговера — 70%, 
        //вероятность мутации — 20%, число генераций — не менее 10.
        //3.2. Написать программу, реализующую различные схемы ПГА с возможностью задания пользователем размера популяции, числа генераций и вероятностей ОК, ОМ и ОИ.
        //3.3. Сравнить результаты работы ПГА по Голдбергу, Холланду и Девису.
        //3.4. Построить графики зависимости целевой функции от числа генераций алгоритма.

        private static int _personCount = 8;
        private const int GEN_COUNT = 3;
        private static List<List<int>> _population;
        private static List<int> _populationOfLifeNumbers;
        private static List<int> _populationFunctionValue;
        private static List<double> _populationSelectionKriteriy;

        public static void Show()
        {
            Console.WriteLine("vvedite kolichestvo popul:");
            _personCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("vvedite kolichestvo generation:");
            var tMax = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("vvedite kros shanse:");
            var krosChanse = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("vvedite om shanse:");
            var omChanse = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("vvedite oi shanse:");
            var oiChanse = Convert.ToInt32(Console.ReadLine());
            var rnd = new Random();
            //а) min функции f(x) = 5x3 — 4 на интервале [1, 2, 3, 4, 5];
            GenerateNotUniquePopulation(0, 1);
            //Worker.PopulationsShow("Начальные популяции: ", _population);
            var t = 0;
            Console.WriteLine("____________________");
            Console.WriteLine("step: " + t);
            Worker.PopulationsShow("популяции: ", _population);

            GetKriteriySelection();

            while (t < tMax)
            {
                //selection
                var selectedPopulations = GetAfterSelection();

                //krossingover
                var cr = rnd.Next(100);
                if (cr < krosChanse)
                {
                    selectedPopulations = GetAfterCrossingover(selectedPopulations);
                }

                var numberForMutation = rnd.Next(2);
                var newGenMuta = selectedPopulations[numberForMutation];

                //mutat inverce
                var mut = rnd.Next(100);
                if (mut < oiChanse)
                {
                    newGenMuta = GetAfterMutationInverse(newGenMuta);
                }
                //mutat
                var mut2 = rnd.Next(100);
                if (mut2 < omChanse)
                {
                    newGenMuta = GetAfterMutation(newGenMuta);
                }
                for (int i = 0; i < 1; i++)
                {
                    var max = _populationFunctionValue.Max();
                    var a = _populationFunctionValue.IndexOf(max);
                    _population.RemoveAt(a);
                    _populationFunctionValue.RemoveAt(a);
                }
                _population.Add(newGenMuta);
                //_population.Add(selectedPopulations[numberForMutation == 0 ? 1 : 0]);
                t++;

                Console.WriteLine("____________________");
                Console.WriteLine("step: " + t);
                Worker.PopulationsShow("популяции: ", _population);

                GetKriteriySelection();
                if (_populationFunctionValue.Average(p => p) < 2)
                {
                    break;
                }
            }
            Console.WriteLine();
        }

        private static void GetKriteriySelection()
        {
            _populationFunctionValue = new List<int>();
            _populationSelectionKriteriy = new List<double>();
            Console.Write("Критерии отбора популяций: ");
            for (int i = 0; i < _population.Count; i++)
            {
                var value = 0;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    value += _population[i][j] == 1 ? (int) Math.Pow(2, j) : 0;
                }
                if (value > 5 || value < 1)
                {
                    _populationFunctionValue.Add(999);
                }
                else
                {
                    _populationFunctionValue.Add(GetFunctionValue(value));
                }
                Console.Write(_populationFunctionValue[i] + " ");
            }
            Console.WriteLine("avg: " + _populationFunctionValue.Average(p => p));
            for (int i = 0; i < _populationFunctionValue.Count; i++)
            {
                _populationSelectionKriteriy.Add(1/(double) (_populationFunctionValue[i]));
            }
        }

        private static List<List<int>> GetAfterSelection()
        {
        var parentForKrossingover = new List<List<int>>();
            var rnd = new Random();
            _populationOfLifeNumbers = new List<int>();
            int x = 0;
            while (x < 2)
            {
                var numberOfLife = rnd.NextDouble() * _populationSelectionKriteriy.Sum();
                for (int j = 0; j < _population.Count; j++)
                {
                    if (_populationSelectionKriteriy[j] > numberOfLife)
                    {
                        if (!_populationOfLifeNumbers.Exists(a => a == j))
                        {
                            _populationOfLifeNumbers.Add(j);
                            x++;
                            break;
                        }
                    }
                    else
                    {
                        numberOfLife -= _populationSelectionKriteriy[j];
                    }
                }
            }
            foreach (var populationOfLifeNumber in _populationOfLifeNumbers)
            {
                parentForKrossingover.Add(_population[populationOfLifeNumber]);
            }
            Worker.PopulationsShow("После селекции рулеточкой: ", parentForKrossingover);
            return parentForKrossingover;
        }

        private static List<List<int>> GetAfterCrossingover(List<List<int>> parentForKrossingover)
        {
            var rnd = new Random();
            var populationAfterKrossingover = new List<List<int>>();
            var tochkaRazriva = 0;
            tochkaRazriva = rnd.Next(0, GEN_COUNT + 1);
            var newGen = new List<int>();
            var newGen2 = new List<int>();
            for (var j = 0; j < GEN_COUNT; j++)
            {
                if (j >= tochkaRazriva)
                {
                    newGen.Add(parentForKrossingover[1][j]);
                    newGen2.Add(parentForKrossingover[0][j]);
                }
                else
                {
                    newGen.Add(parentForKrossingover[0][j]);
                    newGen2.Add(parentForKrossingover[1][j]);
                }
            }
            populationAfterKrossingover.Add(newGen);
            populationAfterKrossingover.Add(newGen2);

            Console.WriteLine("tochkaRazriva: " + tochkaRazriva);
            Worker.PopulationsShow("После кроссинговера Одноточечного", populationAfterKrossingover);
            return populationAfterKrossingover;
        }

        private static List<int> GetAfterMutationInverse(List<int> popul)
        {
            var rnd = new Random();
            var tochkaRazriva = rnd.Next(0, GEN_COUNT - 1);
            var newGenMuta = new List<int>();
            for (var j = 0; j < GEN_COUNT; j++)
            {
                if (j < tochkaRazriva)
                {
                    newGenMuta.Add(popul[j]);
                }
                else
                {
                    newGenMuta.Add(popul[GEN_COUNT - j + tochkaRazriva - 1]);
                }
            }
            var populationAfterMuttation = new List<List<int>> {newGenMuta};
            Console.WriteLine("tochka invers mutat: " + tochkaRazriva);
            Worker.PopulationsShow("После мутации инверсии Одноточечной", populationAfterMuttation);
            return newGenMuta;
        }

        private static List<int> GetAfterMutation(List<int> populationAfterMuttation)
        {
            var rnd = new Random();
            var populationAfterMuttation2 = new List<List<int>>();

            int tochkaRazriva = rnd.Next(1, GEN_COUNT - 1);

            var newGenMuta2 = new List<int>();
            for (var j = 0; j < GEN_COUNT; j++)
            {
                newGenMuta2.Add(populationAfterMuttation[j]);
            }
            var stakan = newGenMuta2[tochkaRazriva - 1];
            newGenMuta2[tochkaRazriva - 1] = newGenMuta2[tochkaRazriva];
            newGenMuta2[tochkaRazriva] = stakan;

            populationAfterMuttation2.Add(newGenMuta2);

            Console.WriteLine("tochka mutat: " + tochkaRazriva);
            Worker.PopulationsShow("После мутации одноточечной", populationAfterMuttation2);
            return newGenMuta2;
        }

        public static int GetIntevalValue(int number)
        {
            //на интервале [1, 2, 3, 4, 5];
            var a = new[] { 1, 2, 3, 4, 5 };
            return a[number];
        }

        public static int GetFunctionValue(int x)
        {
            //функции f(x) = 5x3 — 4;
            return x*x*x*5 - 4;
        }

        private static void GenerateNotUniquePopulation(int minValue, int maxValue)
        {
            //берём случайные от всего решения и каждому гену присваивем случайное число
            _population = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < _personCount; i++)
            {
                var gen = new List<int>();
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    var currentValue = rnd.Next(minValue, maxValue + 1);
                    gen.Add(currentValue);
                }
                _population.Add(gen);
            }
        }
    }
}
