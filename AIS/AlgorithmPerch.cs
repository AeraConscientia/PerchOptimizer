using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIS
{
    class AlgorithmPerch
    {
        public double R1, R2;
        public double thetta1, thetta2;
        public double L1, L2;
        public double x, y;

        private Random rand = new Random();
        public Perch perch = new Perch();

        /// <summary>Размер популяции окуней </summary>
        public int population;

        /// <summary>Количество стай</summary>
        public int NumFlocks = 0;
        /// <summary>Количество окуней в стае</summary>
        public int NumPerchInFlock = 0;
        /// <summary>Количество шагов до окончания движения внутри стаи</summary>
        public int NStep = 0;
        /// <summary>Глубина продвижения внутри котла</summary>
        public double sigma = 0;

        /// <summary>Параметр распределения Леви</summary>
        public double lambda = 0;
        /// <summary>Величина шага</summary>
        public double alfa = 0;

        /// <summary>Число перекоммутаций</summary>
        public int PRmax = 0;
        /// <summary>Число шагов при перекоммутации</summary>
        public int deltapr = 0;

        /// <summary>Номер выбранной функции</summary>
        public int f;

        /// <summary>Область определения</summary>
        public double[,] D;

        /// <summary>Максимальное число итераций</summary>
        public int MaxCount { get; set; }

        /// <summary>Текущая итерация </summary>
        public int currentIteration = 0;

        /// <summary>Массив средней приспособленности</summary>
        public List<double> averageFitness = new List<double>();

        /// <summary>Массив лучшей приспособленности</summary>
        public List<double> bestFitness = new List<double>();

        /// <summary>Популяция окуней</summary>
        public List<Perch> individuals = new List<Perch>();

        public List<Perch> Pool = new List<Perch>();


        
        /// <summary>Все стаи с окунями</summary>
        public Perch[,] flock;

        public AlgorithmPerch() { }

        /// <summary>Сортировка ВСЕХ окуней</summary>
        private void Sort(List<Perch> list) // TODO: протестить на предмет правильно сортировки
        {
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list.Count - i - 1; j++)
                    if (list[j].fitness > list[j + 1].fitness)
                    {
                        Perch tmp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = tmp;
                    }
        }

        /// <summary>Сортировка всех окуней в своей стае</summary>
        private void Sort(Perch[,] perches, int flockIndex)
        {
            for (int i = 0; i < NumPerchInFlock; i++)
                for (int j = 0; j < NumPerchInFlock - i -1; j++)
                    if (perches[flockIndex, j].fitness > perches[flockIndex, j+1].fitness)
                    {
                        Perch tmp = perches[flockIndex, j];
                        perches[flockIndex, j] = perches[flockIndex, j + 1];
                        perches[flockIndex, j + 1] = tmp;
                    }
        }

        private void SortFlocks()
        {
            for (int i = 0; i < NumFlocks; i++)
            {
                for (int j = 0; j < NumFlocks - i - 1; j++)
                {
                    if (flock[j,0].fitness > flock[j + 1 , 0].fitness)
                    {
                        Perch[] tmp = new Perch[NumPerchInFlock];
                        for (int k = 0; k < NumPerchInFlock; k++)
                            tmp[k] = flock[j, k];
                        for (int k = 0; k < NumPerchInFlock; k++)
                            flock[j, k] = flock[j + 1, k];
                        for (int k = 0; k < NumPerchInFlock; k++)
                            flock[j + 1, k] = tmp[k];
                    }
                }
            }
        }

        /// <summary>Разбивка окуней на стаи</summary>
        public void MakeFlocks() 
        {
            Sort(individuals);
            flock = new Perch[NumFlocks, NumPerchInFlock];

            for (int i = 0; i < individuals.Count; i++)
            {
                int tmp;
                Math.DivRem(i, NumFlocks, out tmp);
                flock[tmp, i / (NumFlocks)] = individuals[i];
            }
            

            //flock[0][i] - перебор окуней в первой стае
            //flock[-1][i] - перебор окуней в первой стае

        }

        /// <summary>Движения каждого окуня в каждой стае</summary>
        public void MoveEPerchEFlock()
        {
            sigma = rand.NextDouble()*0.4 + 0.1; // sigma [0.1,  0.5]

            for (int i = 0; i < NumFlocks; i++)
            {
                for (int j = 0; j < NumPerchInFlock; j++)
                {
                    double x = 0;
                    double y = 0;
                    int moveCount = (int)Math.Floor(sigma * NStep);

                    List<Perch> move = new List<Perch>();
                    for (int k = 0; k < moveCount; ++k)
                    {
                        x = flock[i, j].coords[0] + k * ((flock[i, 0].coords[0] - flock[i, j].coords[0]) / (NStep));
                        y = flock[i, j].coords[1] + k * ((flock[i, 0].coords[1] - flock[i, j].coords[1]) / (NStep));
                        move.Add(new Perch(x, y, function(x, y, f)));
                    }
                    Sort(move);
                    flock[i, j] = move[0];
                }
                Sort(flock, i);
            }
            SortFlocks();
        }

        private void BestFlockSwim()
        {
            sigma = rand.NextDouble() * 0.5 + 1; // sigma [1,  1.5]
            int i = 0;
            for (int j = 0; j < NumPerchInFlock; j++)
            {
                double x = 0;
                double y = 0;
                int moveCount = (int)Math.Floor(sigma * NStep);

                List<Perch> move = new List<Perch>();
                for (int k = 0; k < moveCount; ++k)
                {
                    x = flock[i, j].coords[0] + k * ((flock[i, 0].coords[0] - flock[i, j].coords[0]) / (NStep));
                    y = flock[i, j].coords[1] + k * ((flock[i, 0].coords[1] - flock[i, j].coords[1]) / (NStep));
                    move.Add(new Perch(x, y, function(x, y, f)));
                }
                Sort(move);
                flock[i, j] = move[0];
            }
            Sort(flock, i);
        }

        private void PoorFlockSwim()
        {
            sigma = rand.NextDouble() * 0.4 + 0.1; // sigma [0.1,  0.5]
            int i = 1;


            for (int j = 0; j < NumPerchInFlock; j++)
            {
                double x = 0;
                double y = 0;
                int moveCount = (int)Math.Floor(sigma * NStep);

                List<Perch> move = new List<Perch>();
                for (int k = 0; k < moveCount; ++k)
                {
                    x = flock[i, j].coords[0] + k * ((flock[i, 0].coords[0] - flock[i, j].coords[0]) / (NStep));
                    y = flock[i, j].coords[1] + k * ((flock[i, 0].coords[1] - flock[i, j].coords[1]) / (NStep));
                    move.Add(new Perch(x, y, function(x, y, f)));
                }
                Sort(move);
                flock[i, j] = move[0];
            }
            Sort(flock, i);
        }

        /// <summary>Распределение Леви для худшей стаи</summary>
        public void Levi()
        {
            R1 = rand.Next(Convert.ToInt32(D[0,0] * 100), Convert.ToInt32(D[0,1] * 100)) / 100f;
            R2 = rand.Next(Convert.ToInt32(D[1, 0] * 100), Convert.ToInt32(D[1, 1] * 100)) / 100f;
            thetta1 = R1 * 2 * Math.PI;
            thetta2 = R2 * 2 * Math.PI;

            L1 = Math.Pow(R1, -1 / lambda);
            L2 = Math.Pow(R2, -1 / lambda);

            x = L1 * Math.Sin(thetta1);
            y = L2 * Math.Cos(thetta2); // TODO: странна, менять, если что

        }

        /// <summary>Начальное формирование популяции </summary>
        public void FormingPopulation()
        {
            for (int i = 0; i < population; i++)
            {
                x = 0;
                y = 0;

                x = D[0, 0] + rand.NextDouble() * (D[0, 1] - D[0, 0]);
                y = D[1, 0] + rand.NextDouble() * (D[1, 1] - D[1, 0]);

                Perch perch = new Perch(x, y, function(x, y, f)); // TODO: добавить iter += 1
                individuals.Add(perch);
            }
        }

        public Perch StartAlg(int MaxCount, double[,] D, int f, 
            int NumFlocks, int NumPerchInFlock, 
            int NStep, double sigma, 
            double lambda, double alfa, 
            int PRmax, int deltapr)
        {
            this.MaxCount = MaxCount;

            this.D = D;
            this.f = f;

            this.NumFlocks = NumFlocks;
            this.NumPerchInFlock = NumPerchInFlock;
            population = NumFlocks * NumPerchInFlock;

            this.NStep = NStep;
            this.sigma = sigma;
            this.lambda = lambda;
            this.alfa = alfa;
            this.PRmax = PRmax;
            this.deltapr = deltapr;

            FormingPopulation();
            MakeFlocks();
            MoveEPerchEFlock();

            return perch;
        }

        private float function(double x1, double x2, int f)
        {
            float funct = 0;
            if (f == 0)
            {
                funct = (float)(x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2))));
            }
            else if (f == 1)
            {
                funct = (float)(x1 * Math.Sin(4 * Math.PI * x1) - x2 * Math.Sin(4 * Math.PI * x2 + Math.PI) + 1);
            }
            else if (f == 2)
            {
                double[] c6 = Cpow(x1, x2, 6);
                funct = (float)(1 / (1 + Math.Sqrt((c6[0] - 1) * (c6[0] - 1) + c6[1] * c6[1])));
            }
            else if (f == 3)
            {
                funct = (float)(0.5 - (Math.Pow(Math.Sin(Math.Sqrt(x1 * x1 + x2 * x2)), 2) - 0.5) / (1 + 0.001 * (x1 * x1 + x2 * x2)));
            }
            else if (f == 4)
            {
                funct = (float)((-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(2 * Math.PI * x2)));
            }
            else if (f == 5)
            {
                funct = (float)(-Math.E + 20 * Math.Exp(-0.2 * Math.Sqrt((x1 * x1 + x2 * x2) / 2)) + Math.Exp((Math.Cos(2 * Math.PI * x1) + Math.Cos(2 * Math.PI * x2)) / 2));
            }
            else if (f == 6)
            {
                funct = (float)(Math.Pow(Math.Cos(2 * x1 * x1) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x1) - 1.2, 2) - Math.Pow(Math.Cos(2 * x2 * x2) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x2) - 1.2, 2));
            }
            else if (f == 7)
            {
                funct = (float)(-Math.Sqrt(Math.Abs(Math.Sin(Math.Sin(Math.Sqrt(Math.Abs(Math.Sin(x1 - 1))) + Math.Sqrt(Math.Abs(Math.Sin(x2 + 2))))))) + 1);
            }
            else if (f == 8)
            {
                funct = (float)(-(1 - x1) * (1 - x1) - 100 * (x2 - x1 * x1) * (x2 - x1 * x1));
            }
            else if (f == 9)
            {
                funct = (float)(-x1 * x1 - x2 * x2);
            }
            return funct;
        }
        private double[] Cpow(double x, double y, int p)
        {
            double[] Cp = new double[2];
            Cp[0] = x; Cp[1] = y;
            double x0 = 0;
            double y0 = 0;
            for (int i = 1; i < p; i++)
            {
                x0 = Cp[0] * x - Cp[1] * y;
                y0 = Cp[1] * x + Cp[0] * y;
                Cp[0] = x0; Cp[1] = y0;
            }
            return Cp;
        }
        public double AverageFitness()
        {
            double sum = 0;
            for (int i = 0; i < population; i++)
                sum += individuals[i].fitness;
            double fitness = (sum / population);
            averageFitness.Add(fitness);
            return fitness;
        }
    }
}
