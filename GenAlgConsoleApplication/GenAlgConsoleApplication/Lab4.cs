using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgConsoleApplication
{
    public class Lab4
    {
        //Селекция на основе рулетки
        //Селекция на основе заданной шкалы.
        //Элитная селекция. 
        //Турнирная селекция.

        private const int PERSON_COUNT = 8;
        private const int GEN_COUNT = 8;
        private static List<List<int>> _population;
        private static List<int> _populationSelectionKriteriy;
        private static List<int> _populationRuletkaDiaposon;
        private static List<List<int>> _populationAfterSelection;
        private static List<List<int>> _tochkiRazriva; //этот же массив хранит маску, или точки мутации, вобщем вспомогательная еденица

        public static void Show()
        {
            GenerateNotUniquePopulation(0,20);
            Worker.PopulationsShow("Селекция рулеткой!!!!! Начальные популяции: ", _population);
            SelectionRuletkoy();
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

        private static void SelectionRuletkoy()
        {
            //Простой и широко используемый в простом генетическом алгоритме (ПГА) метод.
            //При его реализации каждому элементу в популяции соответствует зона на колесе рулетки,
            //пропорционально соразмерная с величиной ЦФ. Тогда при повороте колеса рулетки каждый
            //элемент имеет некоторую вероятность выбора для селекции. Причем элемент с большим 
            //значением ЦФ имеет большую вероятность для выбора.
            _populationAfterSelection = new List<List<int>>();
            _populationSelectionKriteriy = new List<int>();
            _populationRuletkaDiaposon = new List<int>();
            //сделаю тип крутости, сумма всех значений, чем больше, тем сильнее популяция
            _populationSelectionKriteriy.Add(0);
            _populationRuletkaDiaposon.Add(0);
            Console.Write("Диапозоны на рулетке: ");
            for (int i = 0; i < PERSON_COUNT; i++)
            {
                var sum = 0;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    sum += _population[i][j];
                }
                _populationSelectionKriteriy.Add(sum);
                _populationRuletkaDiaposon.Add(_populationSelectionKriteriy[i + 1] + _populationRuletkaDiaposon[i]);
                Console.Write(_populationRuletkaDiaposon[i] + "-" + _populationRuletkaDiaposon[i + 1] + " ");
            }
            //теперь выберем половину популяций которые будут жить (чем больше диапозон, тем больше шанс выжить)
            Random rnd = new Random();
            var populationOfLifeNumbers = new List<int>();
            var positionOfLifeNumbers = new List<int>();
            int x = 0; 
            while (x < PERSON_COUNT / 2)
            {
                var numberOfLife = rnd.Next(_populationRuletkaDiaposon.Last());
                for (int j = 0; j < PERSON_COUNT; j++)
                {
                    if (_populationRuletkaDiaposon[j] < numberOfLife && _populationRuletkaDiaposon[j + 1] > numberOfLife)
                    {
                        if (!populationOfLifeNumbers.Exists(a => a == j))
                        {
                            populationOfLifeNumbers.Add(j);
                            positionOfLifeNumbers.Add(numberOfLife);
                            x++;
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.Write("Что выпало на рулетке: ");
            foreach (var positionOfLifeNumber in positionOfLifeNumbers)
            {
                Console.Write(positionOfLifeNumber + " ");
            }
            Console.WriteLine();

            foreach (var populationOfLifeNumber in populationOfLifeNumbers)
            {
                _populationAfterSelection.Add(_population[populationOfLifeNumber]);
            }
            Worker.PopulationsShow("После селекции рулеточкой: ", _populationAfterSelection);
        }
    }
}
