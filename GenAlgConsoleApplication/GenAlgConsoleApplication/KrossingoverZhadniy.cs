using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgConsoleApplication
{
    public class KrossingoverZhadniy
    {
        private static int _personCount = 2;
        private static int _genCount = 8;
        private static List<List<int>> _population;
        private static List<List<int>> _populationAfterKrossingover;
        private static int[][] _matricaSmezhnosti; //матрица смежности с весами
        private static List<List<int>> _tochkiRazriva; 

        public static void Execute(int personCount, int genCount) //вобщем вся суть сводиться к поиску кратчайшего пути
        {
            _personCount = personCount;
            _genCount = genCount;

            Console.WriteLine("Кроссинговер жадный: ");
            _population = Worker.GenerateUniquePopulation(_personCount, _genCount, 0, _genCount-1);
            Worker.PopulationsShow("Начальные популяции",_population);
            GenerateMatricaSmezhnosti();
            MatrixShow(_matricaSmezhnosti,"матрица смежности: ");
            Krossingover();
            Worker.PopulationsShowAfterAndBeforeMutation("После кроссинговера жадного", _population,_populationAfterKrossingover,_tochkiRazriva,"точка разрыва= ");
        }

        private static void Krossingover()
        {
            _populationAfterKrossingover = new List<List<int>>();
            _tochkiRazriva = new List<List<int>>();
            for (int x = 0; x < _personCount; x++)
            {
                Random rnd = new Random();
                var razriv = rnd.Next(0, _genCount);
                _tochkiRazriva.Add(new List<int>{razriv});
                var otvet = new List<int>();
                var uzheVmassive = new List<int>();
                uzheVmassive.Add(razriv);
                otvet.Add(_population[x][razriv]);
                for (int i = 0; i < _genCount-1; i++)
                {
                        var min = 99;
                        var indexMin = razriv;
                        for (int j = 0; j < _genCount; j++)
                        {
                            if (!uzheVmassive.Exists(e => e == j))
                            {
                                if (_matricaSmezhnosti[razriv][j] < min)
                                {
                                    min = _matricaSmezhnosti[razriv][j];
                                    indexMin = j;
                                }
                            }
                        }
                        otvet.Add(_population[x][indexMin]);
                        uzheVmassive.Add(indexMin);
                        razriv = indexMin;
                    
                }
                _populationAfterKrossingover.Add(otvet);
            }
        }

        private static void GenerateMatricaSmezhnosti()
        {
            var rnd = new Random();
            _matricaSmezhnosti = new int[_genCount][];
            
            for (int i = 0; i < _genCount; i++)
            {
                _matricaSmezhnosti[i] = new int[_genCount];
                _matricaSmezhnosti[i][i] = 0;
            }
            for (int i = 0; i < _genCount; i++)
            {
                for (int j = i + 1; j < _genCount; j++)
                {
                    var value = rnd.Next(0, 20);
                    _matricaSmezhnosti[i][j] = value;
                    _matricaSmezhnosti[j][i] = value;
                }
            }
        }

        private static void MatrixShow(int[][] matricaSmezhnosti, string str)
        {
            Console.WriteLine(str);

            Console.Write(" \\ ");
            for (int j = 0; j < _genCount; j++)
            {
                Console.Write("{0,4} ", j);
            }
            Console.WriteLine();
            for (int i = 0; i < _genCount; i++)
            {
                Console.Write(i + ": ");
                PopulationShow(matricaSmezhnosti[i]);
                Console.WriteLine();
            }
        }

        private static void PopulationShow(int[] population)
        {
            for (int j = 0; j < _genCount; j++)
            {
                Console.Write("{0,4} ", population[j]);
            }
        }
    }
}
