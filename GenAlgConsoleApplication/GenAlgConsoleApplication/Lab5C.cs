using System;
using System.Collections.Generic;
using System.Linq;

namespace GenAlgConsoleApplication
{
    public class Lab5C
    {
        //3.1. Выполнить одно из заданий к лабораторной работе и построить на основе описаний ГА Холланда, Голдберга и Девиса ПГА вычисления min (max) функции:
        //в) max функции f(x) = 3x3 - 2x + 5 на интервале [1 - 10];
        //Размер начальной популяции — 10, формирование начальной популяции — по выбору пользователя, вероятность кроссинговера — 70%, 
        //вероятность мутации — 20%, число генераций — не менее 10.
        //3.2. Написать программу, реализующую различные схемы ПГА с возможностью задания пользователем размера популяции, числа генераций и вероятностей ОК, ОМ и ОИ.
        //3.3. Сравнить результаты работы ПГА по Голдбергу, Холланду и Девису.
        //3.4. Построить графики зависимости целевой функции от числа генераций алгоритма.

        private static int _personCount = 8;
        private const int GEN_COUNT = 4;
        private static List<List<int>> _population;
        private static List<int> _populationOfLifeNumbers;
        private static List<int> _populationFunctionValue;
        private static List<double> _populationSelectionKriteriy;
        private static int _krosChanse;

        public static void Show()
        {
            Console.WriteLine("Девис");
            //Console.WriteLine("vvedite kolichestvo popul:");
            //_personCount = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("vvedite kolichestvo generation:");
            //var tMax = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("vvedite kros shanse:");
            //_krosChanse = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("vvedite om shanse:");
            //var omChanse = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("vvedite oi shanse:");
            //var oiChanse = Convert.ToInt32(Console.ReadLine());
            _personCount = 8;
            var tMax = 9;
            _krosChanse = 70;
            var omChanse = 10;
            var rnd = new Random();
            //в) max функции f(x) = 3x3 - 2x + 5 на интервале [1 - 10];
            GenerateNotUniquePopulation(0, 1);
            var t = 0;
            Console.WriteLine("____________________");
            Console.WriteLine("step: " + t);
            Worker.PopulationsShow("популяции: ", _population);

            GetKriteriySelection(_population);

            while (t < tMax)
            {
                //selection
                var selectedPopulations = GetAfterSelection();

                //krossingover
                selectedPopulations = GetAfterCrossingover(selectedPopulations);
                var afterMuta = GetAfterMutation(selectedPopulations, omChanse);

                var deleteCOunt = _population.Count/2;
                for (int i = 0; i < deleteCOunt; i++)
                {
                    var min = _populationFunctionValue.Min();
                    var a = _populationFunctionValue.IndexOf(min);
                    _population.RemoveAt(a);
                    _populationFunctionValue.RemoveAt(a);
                }



                GetKriteriySelection(afterMuta);
                deleteCOunt = afterMuta.Count / 2;
                for (int i = 0; i < deleteCOunt; i++)
                {
                    var min = _populationFunctionValue.Min();
                    var a = _populationFunctionValue.IndexOf(min);
                    afterMuta.RemoveAt(a);
                    _populationFunctionValue.RemoveAt(a);
                }
                _population.AddRange(afterMuta);
                t++;
                Console.WriteLine("____________________");
                Console.WriteLine("step: " + t);
                Worker.PopulationsShow("популяции: ", _population);

                GetKriteriySelection(_population);
                if (_populationFunctionValue.Average(p => p) > 2900)
                {
                    break;
                }
            }
            Console.WriteLine();
        }

        private static void GetKriteriySelection(List<List<int>> population)
        {
            _populationFunctionValue = new List<int>();
            _populationSelectionKriteriy = new List<double>();
            Console.Write("Критерии отбора популяций: ");
            for (int i = 0; i < population.Count; i++)
            {
                var value = 0;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    value += population[i][j] == 1 ? (int)Math.Pow(2, j) : 0;
                }
                if (value > 10)
                {
                    _populationFunctionValue.Add(0);
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
                _populationSelectionKriteriy.Add(_populationFunctionValue[i]);
            }
        }

        private static List<List<int>> GetAfterSelection()
        {
            var parentForKrossingover = new List<List<int>>();
            var rnd = new Random();
            _populationOfLifeNumbers = new List<int>();
            int x = 0;
            while (x < _population.Count)
            {
                var numberOfLife = rnd.NextDouble() * _populationSelectionKriteriy.Sum();
                for (int j = 0; j < _population.Count; j++)
                {
                    if (_populationSelectionKriteriy[j] > numberOfLife)
                    {
                        _populationOfLifeNumbers.Add(j);
                        x++;
                        break;
                    }
                    numberOfLife -= _populationSelectionKriteriy[j];
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

            for (int i = 0; i < _personCount; i += 2)
            {
                var number1 = rnd.Next(1, parentForKrossingover.Count);
                var number2 = rnd.Next(0, number1);
                var tochkaRazriva = rnd.Next(0, GEN_COUNT + 1);
                var newGen = new List<int>();
                var newGen2 = new List<int>();

                var cr = rnd.Next(100);
                if (cr < _krosChanse)
                {
                    tochkaRazriva = GEN_COUNT + 10;
                }
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j <= tochkaRazriva)
                    {
                        newGen.Add(parentForKrossingover[number1][j]);
                        newGen2.Add(parentForKrossingover[number2][j]);
                    }
                    else
                    {
                        newGen.Add(parentForKrossingover[number2][j]);
                        newGen2.Add(parentForKrossingover[number1][j]);
                    }
                }
                populationAfterKrossingover.Add(newGen);
                populationAfterKrossingover.Add(newGen2);
                parentForKrossingover.RemoveAt(number1);
                parentForKrossingover.RemoveAt(number2);
            }
            Worker.PopulationsShow("После кроссинговера Одноточечного", populationAfterKrossingover);
            return populationAfterKrossingover;
        }

        private static List<List<int>> GetAfterMutation(List<List<int>> populationAfterKrossingover, int omChanse)
        {
            //mutat
            var populationAfterMuttation2 = new List<List<int>>();
            for (int i = 0; i < populationAfterKrossingover.Count; i++)
            {
                var rnd = new Random();
                var mut2 = rnd.Next(100);

                int tochkaRazriva = rnd.Next(1, GEN_COUNT - 1);
                var newGenMuta2 = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    newGenMuta2.Add(populationAfterKrossingover[i][j]);
                }
                if (mut2 < omChanse)
                {
                    var stakan = newGenMuta2[tochkaRazriva - 1];
                    newGenMuta2[tochkaRazriva - 1] = newGenMuta2[tochkaRazriva];
                    newGenMuta2[tochkaRazriva] = stakan;
                }
                populationAfterMuttation2.Add(newGenMuta2);

            }
            Worker.PopulationsShow("После мутации одноточечной", populationAfterMuttation2);
            return populationAfterMuttation2;
        }

        public static int GetFunctionValue(int x)
        {
            //в) max функции f(x) = 3x3 - 2x + 5 на интервале [1 - 10];
            return 3 * x * x * x - x * 2 + 5;
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
