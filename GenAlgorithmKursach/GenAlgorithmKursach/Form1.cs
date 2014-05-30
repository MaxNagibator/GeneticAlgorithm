using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            var tMax = Convert.ToInt32(tMaxTextBox.Text);
            var krosChanse = Convert.ToInt32(krosChanseTextBox.Text);
            var linkChanse = Convert.ToInt32(linkChanseTextBox.Text);
            var omChanse = Convert.ToInt32(omChanseTextBox.Text);
            var oiChanse = Convert.ToInt32(oiChanseTextBox.Text);
            var genCount = Convert.ToInt32(uiGenCountTextBox.Text);
            var matricaSmezhnosti = GenerateMatricaSmezhnosti(genCount, linkChanse);
            var text = MatrixShow(matricaSmezhnosti, "матрица смежности: ", genCount);
            var popul = GenerateUniquePopulation(personCount, genCount, 0, genCount - 1);
            text += PopulationsShow("популяшки: ", popul);
            var krit = GetKriteriy(popul, matricaSmezhnosti);
            text += PopulationShow(krit, "kriteruii:") + Environment.NewLine;
            text += "max kriter: " + krit.Max() + Environment.NewLine;
            var startMax = krit.Max();

            var maxKriteriy = new List<int>();
            var avgKriteriy = new List<double>();
            maxKriteriy.Add(krit.Max());
            avgKriteriy.Add(krit.Average());
            var t = 0;
            while (t < tMax)
            {
                popul = GetAfterSelection2(popul, krit);
                text += PopulationsShow("После селекции рулеточкой: ", popul);
                krit = GetKriteriy(popul, matricaSmezhnosti);
                text += PopulationShow(krit, "kriteruii:") + Environment.NewLine;
                text += "max kriter: " + krit.Max() + "avg:" + krit.Average() + Environment.NewLine;
                maxKriteriy.Add(krit.Max());
                avgKriteriy.Add(krit.Average());
                string razrivStr;
                popul = GetAfterCrossingover2(popul, krosChanse, out razrivStr);
                //text += PopulationsShow("После кроссинговера Одноточечного " + razrivStr, popul);
                string mutStr;
                popul = GetAfterMutation(popul, omChanse, out mutStr);
                //text += PopulationsShow("После мутации одноточечной " + mutStr, popul);
                string mutInvStr;
                popul = GetAfterMutationInverse(popul, oiChanse, out mutInvStr);
                //text += PopulationsShow("После мутации инверсией " + mutInvStr, popul);
                uiOutTextBox.Text = text;
                t++;
            }
            
            var endMax = krit.Max();
            for(int i = 0; i< krit.Count;i++)
            {
                if(krit[i]==endMax)
                {
                    uiOutTextBox.Text+= Environment.NewLine+PopulationsShow("лучшая популяция ", new List<List<int>>{popul[i]});
                    uiOutTextBox.Text += " с критерием = " + endMax;
                    break;
                }
            }

            uiKriteriyTextBox.Text = startMax + " " + endMax + Environment.NewLine + Environment.NewLine;
            for (int i = 0; i < maxKriteriy.Count; i++)
            {
                uiKriteriyTextBox.Text += maxKriteriy[i] + " " + avgKriteriy[i] + Environment.NewLine;
            }
        }


        private static List<List<int>> GetAfterMutationInverse(List<List<int>> popul, int oiChanse, out string mutInvStr)
        {
            mutInvStr = "";
            var genCount = popul[0].Count;
            var populationAfterMuttation = new List<List<int>>();
            var rnd = new Random();
            for (int i = 0; i < popul.Count; i++)
            {
                var mut = rnd.Next(100);
                if (mut < oiChanse)
                {
                    var tochkaRazriva = rnd.Next(0, genCount - 1);
                    var newGenMuta = new List<int>();
                    mutInvStr += i + "-" + tochkaRazriva + "; ";
                    for (var j = 0; j < genCount; j++)
                    {
                        if (j < tochkaRazriva)
                        {
                            newGenMuta.Add(popul[i][j]);
                        }
                        else
                        {
                            newGenMuta.Add(popul[i][genCount - j + tochkaRazriva - 1]);
                        }
                    }
                    populationAfterMuttation.Add(newGenMuta);
                }else
                {
                    populationAfterMuttation.Add(popul[i]);
                }
            }
            return populationAfterMuttation;
        }

        private static List<List<int>> GetAfterMutation(List<List<int>> populationAfterKrossingover, int omChanse, out string mutStr)
        {
            mutStr = "";
            var rnd = new Random();
            var genCount = populationAfterKrossingover[0].Count;
            var populationAfterMuttation2 = new List<List<int>>();
            for (int i = 0; i < populationAfterKrossingover.Count; i++)
            {
                var mut2 = rnd.Next(100);

                int tochkaRazriva = rnd.Next(1, genCount - 1);
                var newGenMuta2 = new List<int>();
                for (var j = 0; j < genCount; j++)
                {
                    newGenMuta2.Add(populationAfterKrossingover[i][j]);
                }
                if (mut2 < omChanse)
                {
                    mutStr += i + "-" + tochkaRazriva+"; ";
                    var stakan = newGenMuta2[tochkaRazriva - 1];
                    newGenMuta2[tochkaRazriva - 1] = newGenMuta2[tochkaRazriva];
                    newGenMuta2[tochkaRazriva] = stakan;
                }
                populationAfterMuttation2.Add(newGenMuta2);
            }
            return populationAfterMuttation2;
        }

        private static List<List<int>> GetAfterCrossingover2(List<List<int>> parentForKrossingover, int krosChanse, out string razrivStr)
        {
            razrivStr = "tr: ";
            var populationAfterKrossingover = new List<List<int>>();
            var rnd = new Random();
            var personCount = parentForKrossingover.Count;
            var genCount = parentForKrossingover[0].Count;
            for (int i = 0; i < personCount; i += 2)
            {
                var tr = rnd.Next(0, genCount + 1);
                razrivStr += tr + " ";
                var newGen = new List<int>();
                var newGen2 = new List<int>();
                for (int j = 0; j < genCount; j++)
                {
                    if (j >= tr)
                    {
                        for (int x = 0; x < genCount; x++)
                            //поиск элемента из (популяции 2) которого ещё нету в популяции один, для добавления в (новую популяцию 1)
                        {
                            if (!newGen.Exists(g => g == parentForKrossingover[i + 1][x]))
                            {
                                newGen.Add(parentForKrossingover[i + 1][x]);
                                break;
                            }
                        }
                        for (int x = 0; x < genCount; x++)
                            //поиск элемента из (популяции 1) которого ещё нету в популяции один, для добавления в (новую популяцию 2)
                        {
                            if (!newGen2.Exists(g => g == parentForKrossingover[i][x]))
                            {
                                newGen2.Add(parentForKrossingover[i][x]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        newGen.Add(parentForKrossingover[i][j]);
                        newGen2.Add(parentForKrossingover[i + 1][j]);
                    }
                }
                //это сортировка как и заказывали
                //var b = newGen.Where(a => newGen.IndexOf(a) < tochkiRazrivaSub[0]).ToList();
                //b.AddRange(newGen.Where(a => newGen.IndexOf(a) >= tochkiRazrivaSub[0]).OrderBy(g => g));
                //var c = newGen2.Where(a => newGen2.IndexOf(a) < tochkiRazrivaSub[0]).ToList();
               // c.AddRange(newGen2.Where(a => newGen2.IndexOf(a) >= tochkiRazrivaSub[0]).OrderBy(g => g));
                //_populationAfterKrossingover.Add(b);
                //_populationAfterKrossingover.Add(c);
                populationAfterKrossingover.Add(newGen);
                populationAfterKrossingover.Add(newGen2);
            }
            return populationAfterKrossingover;
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

        private static int[][] GenerateMatricaSmezhnosti(int personCount, int linkChanse)
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
                    var k = rnd.Next(0, 100);
                    var value = k<linkChanse ? 1 : 0;
                    matricaSmezhnosti[i][j] = value;
                    matricaSmezhnosti[j][i] = value;
                }
            }
            return matricaSmezhnosti;
        }

        private static string MatrixShow(int[][] matricaSmezhnosti, string str, int personCount)
        {
            //str += Environment.NewLine + " \\ ";
            //for (int j = 0; j < personCount; j++)
            //{
            //    str += String.Format("{0,4} ", j);
            //}
            str += Environment.NewLine;
            for (int i = 0; i < personCount; i++)
            {
                str += String.Format("{0,4} ", i + ": ");
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
                textOut += String.Format("{0,4} ", i + ": ");
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

        private List<List<int>> GetAfterSelection2(List<List<int>> popul, List<int> newKriter)
        {
            var krit = new List<int>();
            foreach (var k in newKriter)
            {
                krit.Add(k * k*k);
            }
            var parentForKrossingover = new List<List<int>>();
            var rnd = new Random();
            var populationOfLifeNumbers = new List<int>();
            int x = 0;
            while (x < popul.Count)
            {
                var numberOfLife = rnd.Next(krit.Sum());
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

        private List<List<int>> GetAfterSelection(List<List<int>> population, List<int> populationSelectionKriteriy)
        {
            var populationAfterSelection = new List<List<int>>();

            var populationOfLifeKriteriy = populationSelectionKriteriy.OrderBy(p => p).ToList();
            populationOfLifeKriteriy.RemoveRange(0, populationOfLifeKriteriy.Count/2);
            for (int i = 0; i < population.Count; i++)
            {
                if (populationOfLifeKriteriy.Exists(k => k == populationSelectionKriteriy[i]))
                {
                    populationAfterSelection.Add(population[i]);
                }
            }
            return populationAfterSelection;
        }
    }
}
