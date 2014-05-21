using System;
using System.Collections.Generic;
using System.Linq;

namespace GenAlgConsoleApplication
{
    public class Lab5
    {
        //Селекция на основе рулетки +
        //Селекция на основе заданной шкалы.
        //Элитная селекция. +
        //Турнирная селекция. +

        private const int PERSON_COUNT = 8;
        private const int GEN_COUNT = 5;
        private static List<List<int>> _population;
        private static List<int> _populationSelectionKriteriy;

        public static void Show()
        {
            //а) min функции f(x) = 5x3 — 4 на интервале [1, 2, 3, 4, 5];
            GenerateNotUniquePopulation(0, 621);
            //Worker.PopulationsShow("Начальные популяции: ", _population);
            var t = 0;
            while (t < 20)
            {
                Console.WriteLine("____________________");
                Console.WriteLine("step: "+t);
                Worker.PopulationsShow("популяции: ", _population);

                _populationSelectionKriteriy = new List<int>();
                Console.Write("Критерии отбора популяций: ");
                for (int i = 0; i < _population.Count; i++)
                {
                    var kriteriy = 0;
                    for (int j = 0; j < GEN_COUNT; j++)
                    {
                        kriteriy += Math.Abs(_population[i][j] - GetFunctionValue(GetIntevalValue(j)));
                    }
                    _populationSelectionKriteriy.Add(kriteriy);
                    Console.Write(_populationSelectionKriteriy[i] + " ");
                }
                Console.WriteLine("avg: " + _populationSelectionKriteriy.Average(p => p));
                var parentForKrossingover = new List<List<int>>();

                Random rnd = new Random();
                var populationOfLifeNumbers = new List<int>();
                var positionOfLifeNumbers = new List<int>();
                int x = 0;
                while (x < 2)
                {
                    var numberOfLife = rnd.Next(_populationSelectionKriteriy.Sum());
                    for (int j = 0; j < _population.Count; j++)
                    {
                        if (_populationSelectionKriteriy[j] > numberOfLife)
                        {
                            if (!populationOfLifeNumbers.Exists(a => a == j))
                            {
                                populationOfLifeNumbers.Add(j);
                                positionOfLifeNumbers.Add(numberOfLife);
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
                foreach (var populationOfLifeNumber in populationOfLifeNumbers)
                {
                    parentForKrossingover.Add(_population[populationOfLifeNumber]);
                }
                Worker.PopulationsShow("После селекции рулеточкой: ", parentForKrossingover);

                //krossingover
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

                var numberForMutation = rnd.Next(2);
                var populationAfterMuttation = new List<List<int>>();
                tochkaRazriva = rnd.Next(0, GEN_COUNT - 1);

                var newGenMuta = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    if (j < tochkaRazriva)
                    {
                        newGenMuta.Add(populationAfterKrossingover[numberForMutation][j]);
                    }
                    else
                    {
                        newGenMuta.Add(populationAfterKrossingover[numberForMutation][GEN_COUNT - j + tochkaRazriva - 1]);
                    }
                }
                populationAfterMuttation.Add(newGenMuta);

                Console.WriteLine("tochka invers mutat: " + tochkaRazriva);
                Worker.PopulationsShow("После мутации инверсии Одноточечной", populationAfterMuttation);

                var populationAfterMuttation2 = new List<List<int>>();

                tochkaRazriva = rnd.Next(1, GEN_COUNT - 1);

                var newGenMuta2 = new List<int>();
                for (var j = 0; j < GEN_COUNT; j++)
                {
                    newGenMuta2.Add(populationAfterMuttation[0][j]);
                }
                var stakan = newGenMuta2[tochkaRazriva - 1];
                newGenMuta2[tochkaRazriva - 1] = newGenMuta2[tochkaRazriva];
                newGenMuta2[tochkaRazriva] = stakan;

                populationAfterMuttation2.Add(newGenMuta2);

                Console.WriteLine("tochka mutat: " + tochkaRazriva);
                Worker.PopulationsShow("После мутации одноточечной", populationAfterMuttation2);


                var a1 = _population[populationOfLifeNumbers[0]];
                var b = _population[populationOfLifeNumbers[1]];
                _population.Remove(a1);
                _population.Remove(b);
                _population.Add(newGenMuta2);
                _population.Add(populationAfterKrossingover[numberForMutation == 0 ? 1 : 0]);
                t++;
            }
            Console.WriteLine();
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
