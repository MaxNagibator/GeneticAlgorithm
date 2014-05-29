using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenAlgorithmKursach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void uiExecuteButton_Click(object sender, EventArgs e)
        {
                int personCount = Convert.ToInt32(personCountTextBox.Text);
                var matricaSmezhnosti = GenerateMatricaSmezhnosti(personCount);
                var text = MatrixShow(matricaSmezhnosti, "матрица смежности: ", personCount);
                var popul = GenerateUniquePopulation(personCount, personCount, 0, personCount - 1);
                text += PopulationsShow("популяшки: ", popul);
                var krit = GetKriteriy(popul, matricaSmezhnosti);
                text += PopulationShow(krit, "kriteruii:") + Environment.NewLine;
                text += "max kriter: " + krit.Max();
                var sel = GetAfterSelection(popul, krit);
                text += PopulationsShow("После селекции рулеточкой: ", sel);
                krit = GetKriteriy(sel, matricaSmezhnosti);
                text += PopulationShow(krit, "kriteruii:") + Environment.NewLine;
                text += "max kriter: " + krit.Max();
                uiOutTextBox.Text = text;
        }

        private List<int> GetKriteriy(List<List<int>> popul, int[][] matricaSmezhnosti)
        {
            var krit = new List<int>();
            for (int i = 0; i < popul.Count; i++)
            {
                var krit1 = 1;
                for (int j = 1; j < popul[i].Count; j++)
                {
                    var allRight = true;
                    for (int k = j-1; k >= 0; k--)
                    {
                        var x = popul[i][k];
                        var y = popul[i][j];
                        if (matricaSmezhnosti[x][y] != 1)
                        {
                            allRight = false;
                            break;
                        }
                    }
                    if (allRight)
                    {
                        krit1++;
                    }
                    else
                    {
                        break;
                    }
                }
                krit.Add(krit1);
            }
            return krit;
        }

        private static int[][] GenerateMatricaSmezhnosti(int personCount)
        {
            var rnd = new Random();
            var matricaSmezhnosti = new int[personCount][];

            for (int i = 0; i < personCount; i++)
            {
                matricaSmezhnosti[i] = new int[personCount];
                matricaSmezhnosti[i][i] = 0;
            }
            for (int i = 0; i < personCount; i++)
            {
                for (int j = i + 1; j < personCount; j++)
                {
                    var value = rnd.Next(0, 2);
                    matricaSmezhnosti[i][j] = value;
                    matricaSmezhnosti[j][i] = value;
                }
            }
            return matricaSmezhnosti;
        }

        private static string MatrixShow(int[][] matricaSmezhnosti, string str, int personCount)
        {
            str += Environment.NewLine + " \\ ";
            for (int j = 0; j < personCount; j++)
            {
                str += String.Format("{0,4} ", j);
            }
            str += Environment.NewLine;
            for (int i = 0; i < personCount; i++)
            {
                str += i + ": ";
                str += PopulationShow(matricaSmezhnosti[i].ToList());
                str += Environment.NewLine;
            }
            return str;
        }

        public static string PopulationsShow(string str, List<List<int>> population)
        {
            var textOut = str;
            textOut += Environment.NewLine;
            for (int i = 0; i < population.Count; i++)
            {
                textOut += i + ": ";
                textOut += PopulationShow(population[i]);
                textOut += Environment.NewLine;
            }
            return textOut;
        }

        public static string PopulationShow(List<int> population, string str ="")
        {
            for (int j = 0; j < population.Count; j++)
            {
                if (population[j] != -1)
                {
                    str += String.Format("{0,4} ", population[j]);
                }
            }
            return str;
        }

        public static List<List<int>> GenerateUniquePopulation(int personCount, int genCount, int minValue, int maxValue)
        {
            //берём случайные от всего решения и каждому гену присваивем случайное число
            var population = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < personCount; i++)
            {
                var gen = new List<int>();
                for (int j = 0; j < genCount; j++)
                {
                    while (true)
                    {
                        var currentValue = rnd.Next(minValue, maxValue + 1);
                        if (!gen.Exists(g => g == currentValue))
                        {
                            gen.Add(currentValue);
                            break;
                        }
                    }
                }
                population.Add(gen);
            }
            return population;
        }

        private List<List<int>> GetAfterSelection(List<List<int>> popul, List<int> krit)
        {
            var parentForKrossingover = new List<List<int>>();
            var rnd = new Random();
            var populationOfLifeNumbers = new List<int>();
            int x = 0;
            while (x < popul.Count)
            {
                var numberOfLife = rnd.NextDouble() * krit.Sum();
                for (int j = 0; j < popul.Count; j++)
                {
                    if (krit[j] > numberOfLife)
                    {
                        populationOfLifeNumbers.Add(j);
                        x++;
                        break;
                    }
                    numberOfLife -= krit[j];
                }
            }
            foreach (var populationOfLifeNumber in populationOfLifeNumbers)
            {
                parentForKrossingover.Add(popul[populationOfLifeNumber]);
            }
            return parentForKrossingover;
        }
    }
}
