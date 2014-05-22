using System;
using System.Collections.Generic;
using System.Linq;

namespace GenAlgConsoleApplication
{
    public class Lab5
    {
        //3.1. Выполнить одно из заданий к лабораторной работе и построить на основе описаний ГА Холланда, Голдберга и Девиса ПГА вычисления min (max) функции:
        //а) min функции f(x) = 5x3 — 4 на интервале [1, 2, 3, 4, 5];
        //Размер начальной популяции — 10, формирование начальной популяции — по выбору пользователя, вероятность кроссинговера — 70%, 
        //вероятность мутации — 20%, число генераций — не менее 10.
        //3.2. Написать программу, реализующую различные схемы ПГА с возможностью задания пользователем размера популяции, числа генераций и вероятностей ОК, ОМ и ОИ.
        //3.3. Сравнить результаты работы ПГА по Голдбергу, Холланду и Девису.
        //3.4. Построить графики зависимости целевой функции от числа генераций алгоритма.

        private const int PERSON_COUNT = 8;
        private const int GEN_COUNT = 5;
        private static List<List<int>> _population;
        private static List<int> _populationOfLifeNumbers;

        public static void Show()
        {
            const int mutationShance = 20;
            const int crossingShance = 70;
            var rnd = new Random();
            //а) min функции f(x) = 5x3 — 4 на интервале [1, 2, 3, 4, 5];
            GenerateNotUniquePopulation(0, 621);
            //Worker.PopulationsShow("Начальные популяции: ", _population);
            var t = 0;
            while (t < 200)
            {
                Console.WriteLine("____________________");
                Console.WriteLine("step: "+t);
                Worker.PopulationsShow("популяции: ", _population);

                //selection
                var parentForKrossingover = GetAfterSelection();

                //krossingover
                var cr = rnd.Next(100);
                var populationAfterKrossingover = parentForKrossingover;
                if (cr < crossingShance)
                {
                    populationAfterKrossingover = GetAfterCrossingover(parentForKrossingover);
                }

                var numberForMutation = rnd.Next(2);
                var newGenMuta = populationAfterKrossingover[numberForMutation];

                //mutat inverce
                var mut = rnd.Next(100);
                if (mut < mutationShance)
                {
                    newGenMuta = GetAfterMutationInverse(newGenMuta);
                }
                //mutat
                var mut2 = rnd.Next(100);
                if (mut2 < mutationShance)
                {
                    newGenMuta = GetAfterMutation(newGenMuta);
                }


                //var a1 = _population[_populationOfLifeNumbers[0]];
                //var b = _population[_populationOfLifeNumbers[1]];
                //_population.Remove(a1);
                //_population.Remove(b);
                //_population.Add(newGenMuta);
                //_population.Add(populationAfterKrossingover[numberForMutation == 0 ? 1 : 0]);
                _population[_populationOfLifeNumbers[0]]= newGenMuta;
                _population[_populationOfLifeNumbers[1]] = populationAfterKrossingover[numberForMutation == 0 ? 1 : 0];
                t++;
            }
            Console.WriteLine("____________________");
            Console.WriteLine("step: " + t);
            Worker.PopulationsShow("популяции: ", _population);
            Console.WriteLine();
        }

        private static List<List<int>> GetAfterSelection()
        {
            var populationSelectionKriteriy = new List<int>();
            Console.Write("Критерии отбора популяций: ");
            for (int i = 0; i < _population.Count; i++)
            {
                var kriteriy = 0;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    kriteriy += Math.Abs(_population[i][j] - GetFunctionValue(GetIntevalValue(j)));
                }
                populationSelectionKriteriy.Add(kriteriy);
                Console.Write(populationSelectionKriteriy[i] + " ");
            }
            Console.WriteLine("avg: " + populationSelectionKriteriy.Average(p => p));

            var parentForKrossingover = new List<List<int>>();
            var rnd = new Random();
            _populationOfLifeNumbers = new List<int>();
            var positionOfLifeNumbers = new List<int>();
            int x = 0;
            while (x < 2)
            {
                var numberOfLife = rnd.Next(populationSelectionKriteriy.Sum());
                for (int j = 0; j < _population.Count; j++)
                {
                    if (populationSelectionKriteriy[j] > numberOfLife)
                    {
                        if (!_populationOfLifeNumbers.Exists(a => a == j))
                        {
                            _populationOfLifeNumbers.Add(j);
                            positionOfLifeNumbers.Add(numberOfLife);
                            x++;
                            break;
                        }
                    }
                    else
                    {
                        numberOfLife -= populationSelectionKriteriy[j];
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
            for (int i = 0; i < PERSON_COUNT; i++)
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
