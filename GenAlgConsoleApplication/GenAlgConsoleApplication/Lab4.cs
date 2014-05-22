using System;
using System.Collections.Generic;
using System.Linq;

namespace GenAlgConsoleApplication
{
    public class Lab4
    {
        //Селекция на основе рулетки +
        //Селекция на основе заданной шкалы.
        //Элитная селекция. +
        //Турнирная селекция. +

        private const int PERSON_COUNT = 8;
        private const int GEN_COUNT = 8;
        private static List<List<int>> _population;
        private static List<List<int>> _populationTemp;
        private static List<int> _populationSelectionKriteriy;
        private static List<int> _populationRuletkaDiaposon;
        private static List<List<int>> _populationAfterSelection;

        public static void Show()
        {
            //•	бессознательный отбор, при котором в процессе эволюции сохраняют лучшие экземпляры;
            Console.WriteLine(
                "это всё бессознательный отбор - сохраняем лучшие экземпляры(сумма чисел, чем больше тем лучше):");
            SelectionRuletkoy();
            SelectionTurnir();
            SelectionElita();

            //•	естественный отбор, вызывающий изменения, связанные с приспособлением к новым условиям;;
            Console.WriteLine();
            Console.WriteLine("это всё естественный отбор - приспосабливаемся к новым условиям");
            SelectionElitaEstestvenniy();

            Console.WriteLine();
            Console.WriteLine("это всё сам придумал");
            SelectionSamPridumal();
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

            GenerateNotUniquePopulation(0, 20);
            Worker.PopulationsShow("Селекция рулеткой!!!!! Начальная популяция: ", _population);

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
            while (x < PERSON_COUNT/2)
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

        private static void SelectionTurnir()
        {
            //При этом некоторое число элементов (согласно размеру «турнира») 
            //выбирается случайно или направленно из популяции, и лучшие элементы 
            //в этой группе на основе заданного турнира определяются
            //для дальнейшего эволюционного поиска.

            GenerateNotUniquePopulation(0, 20);
            Console.WriteLine();
            Console.WriteLine();
            Worker.PopulationsShow("Селекция турнир!!!!! Начальная популяция: ", _population);
            _populationTemp = new List<List<int>>();
            _populationAfterSelection = new List<List<int>>();
            _populationSelectionKriteriy = new List<int>();
            var rnd = new Random();
            var tempCol = rnd.Next(PERSON_COUNT);
            if (tempCol == PERSON_COUNT || tempCol == 0)
            {
                tempCol = PERSON_COUNT/2;
            }
            if (tempCol%2 == 1)
            {
                tempCol++;
            }
            for (int i = 0; i < tempCol; i++)
            {
                _populationTemp.Add(_population[i]);
            }

            Worker.PopulationsShow("Экземпляры выбранные для турнира: ", _populationTemp);
            Console.Write("Критерий отбора турнирная популяция: ");
            for (int i = 0; i < _populationTemp.Count; i++)
            {
                var sum = 0;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    sum += _populationTemp[i][j];
                }
                _populationSelectionKriteriy.Add(sum);
                Console.Write(_populationSelectionKriteriy[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < _populationTemp.Count; i += 2)
            {
                if (_populationSelectionKriteriy[i] >= _populationSelectionKriteriy[i + 1])
                {
                    _populationAfterSelection.Add(_population[i]);
                }
                else
                {
                    _populationAfterSelection.Add(_population[i + 1]);
                }
            }
            Worker.PopulationsShow("После селекции турнирной: ", _populationAfterSelection);
        }

        private static void SelectionElita()
        {
            //В этом случае выбираются лучшие (элитные) элементы на основе 
            //сравнения значений ЦФ. Далее они вступают в различные преобразования, 
            //после которых снова выбираются элитные элементы. Процесс продолжается 
            //аналогично до тех пор, пока продолжают появляться элитные элементы.

            GenerateNotUniquePopulation(0, 20);
            Console.WriteLine();
            Console.WriteLine();
            Worker.PopulationsShow("Селекция элитная!!!!! Начальная популяция: ", _population);
            _populationAfterSelection = new List<List<int>>();
            _populationSelectionKriteriy = new List<int>();

            Console.Write("Критерии отбора Экземпляров: ");
            for (int i = 0; i < _population.Count; i++)
            {
                var sum = 0;
                for (int j = 0; j < GEN_COUNT; j++)
                {
                    sum += _population[i][j];
                }
                _populationSelectionKriteriy.Add(sum);
                Console.Write(_populationSelectionKriteriy[i] + " ");
            }
            var populationOfLifeKriteriy = _populationSelectionKriteriy.OrderBy(p => p).ToList();
            populationOfLifeKriteriy.RemoveRange(0, populationOfLifeKriteriy.Count/2);

            Console.WriteLine();
            Console.Write("Лучшие критерии отбора экземпляров: ");
            for (int i = 0; i < populationOfLifeKriteriy.Count; i++)
            {
                Console.Write(populationOfLifeKriteriy[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < _population.Count; i++)
            {
                if (populationOfLifeKriteriy.Exists(k => k == _populationSelectionKriteriy[i]))
                {
                    _populationAfterSelection.Add(_population[i]);
                }
            }
            Worker.PopulationsShow("После селекции элитной: ", _populationAfterSelection);
        }

        private static void SelectionElitaEstestvenniy()
        {
            //В этом случае выбираются лучшие (элитные) элементы на основе 
            //сравнения значений ЦФ. Далее они вступают в различные преобразования, 
            //после которых снова выбираются элитные элементы. Процесс продолжается 
            //аналогично до тех пор, пока продолжают появляться элитные элементы.

            var maxValue = 20;
            GenerateNotUniquePopulation(0, maxValue);
            Console.WriteLine();
            Worker.PopulationsShow("Естественный отбор!!!!! Начальные популяции: ", _population);
            _populationAfterSelection = new List<List<int>>();
            _populationSelectionKriteriy = new List<int>();

            Console.WriteLine();
            Console.WriteLine("Критерии отбора популяций: первый > " + maxValue/2 + ", последний < " + maxValue/2);

            for (int i = 0; i < _population.Count; i++)
            {
                if (_population[i].First() > maxValue/2 && _population[i].Last() < maxValue/2)
                {
                    _populationAfterSelection.Add(_population[i]);
                }
            }
            Worker.PopulationsShow("После естественного отбора: ", _populationAfterSelection);
        }

        private static void SelectionSamPridumal()
        {
            //На основе полученных знаний предложить новые модификации операторов селекции и отбора. 
            //Написать программу, реализующую разработанные схемы. Продемонстрировать работу программы на примерах.

            // вобщем селекция у нас будет называться "везунчики", будут взяты со случайного места половина популяции
            // вобщем отбор у нас будет называться "первый крупнее последнего", будут отобраны лишь те, у кого первый больше последнего

            GenerateNotUniquePopulation(0, 20);
            Console.WriteLine();
            Console.WriteLine();
            Worker.PopulationsShow("Селекция моя и отбор мой!!!!! Начальные популяции: ", _population);
            _populationAfterSelection = new List<List<int>>();
            var otobrannie = new List<List<int>>();

            for (int i = 0; i < _population.Count; i++)
            {
                if (_population[i].First() > _population[i].Last())
                {
                    otobrannie.Add(_population[i]);
                }
            }
            Worker.PopulationsShow("После отбора 'первый крупнее последнего': ", otobrannie);

            Random rnd = new Random();
            var number = rnd.Next(otobrannie.Count);
            var count = otobrannie.Count%2 == 1 ? otobrannie.Count/2 + 1 : otobrannie.Count/2;

            for (int i = 0; i < count; i++)
            {
                if (number >= otobrannie.Count)
                {
                    number = 0;
                }
                _populationAfterSelection.Add(otobrannie[number]);
                number++;
            }
            Worker.PopulationsShow("После селекции 'везунчики': ", _populationAfterSelection);
        }
    }
}
