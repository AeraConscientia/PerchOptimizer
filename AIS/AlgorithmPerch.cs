using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace AIS
{
    public class AlgorithmPerch
    {
        public double R1, R2;
        public double thetta1, thetta2;
        public double L1, L2;
        public double x, y;

        private Random rand = new Random();
        public Perch perch = new Perch();
        public Perch best = new Perch();
        public Perch result = new Perch();
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


        public ulong functionCalls = 0;
        /// <summary>Область определения</summary>
        public double[,] D;

        /// <summary>Максимальное число итераций</summary>
        public int MaxCount { get; set; }

        /// <summary>Текущая итерация </summary>
        public int currentIteration = 1;

        /// <summary>Массив средней приспособленности</summary>
        public List<double> averageFitness = new List<double>();

        /// <summary>Массив лучшей приспособленности</summary>
        public List<double> bestFitness = new List<double>();

        /// <summary>Популяция окуней</summary>
        public List<Perch> individuals = new List<Perch>();

        public List<Perch> Pool = new List<Perch>();

        public bool flagCreate = false;
        
        /// <summary>Все стаи с окунями</summary>
        public Perch[,] flock;

        public AlgorithmPerch() { }

        /// <summary>Сортировка ВСЕХ окуней</summary>
        
        private void Sort(List<Perch> list)
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
            //Sort(individuals);
            individuals = individuals.OrderBy(s => s.fitness).ToList();
            flock = new Perch[NumFlocks, NumPerchInFlock];

            for (int i = 0; i < individuals.Count; i++)
            {
                int tmp;
                Math.DivRem(i, NumFlocks, out tmp);
                flock[tmp, i / (NumFlocks)] = individuals[i];
                //if (Double.IsNaN(flock[tmp, i / (NumFlocks)].coords[0]) || Double.IsNaN(flock[tmp, i / (NumFlocks)].coords[1]))
                //    throw new Exception();
                //
                //if (Double.IsInfinity(flock[tmp, i / (NumFlocks)].coords[0]) || Double.IsInfinity(flock[tmp, i / (NumFlocks)].coords[1]))
                //    throw new Exception();
            }

            //for (int m = 0; m < NumFlocks; m++)
            //{
            //    for (int p = 0; p < NumPerchInFlock; p++)
            //    {
            //        if (Double.IsInfinity(flock[m, p].coords[0]) || Double.IsInfinity(flock[m, p].coords[1]))
            //            throw new Exception();
            //    }
            //}
            //flock[0][i] - перебор окуней в первой стае
            //flock[-1][i] - перебор окуней в первой стае

        }

        /// <summary>Движения каждого окуня в каждой стае, создание котлов</summary>
        public void MoveEPerchEFlock() // пересчет функции: 0.5 * Nstep * population
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

                        if (x < D[0,0] || x > D[0,1] || y < D[1,0] || y > D[1,1]) // если окуни вышли на границы, оставляем их прежние позиции
                        {
                            x = flock[i, j].coords[0];
                            y = flock[i, j].coords[1];
                        }

                        //if (Double.IsNaN(x) || Double.IsNaN(y))
                        //    throw new Exception();
                        move.Add(new Perch(x, y, function(x, y, f)));
                    }
                    //Sort(move);
                    move = move.OrderBy(s => s.fitness).ToList();
                    flock[i, j] = move[0];
                }
                Sort(flock, i);
            }
            SortFlocks();
            //if (Double.IsNaN(flock[NumFlocks - 1, 0].coords[0]) || Double.IsNaN(flock[NumFlocks - 1, 0].coords[1]))
            //    throw new Exception();

            //for (int m = 0; m < NumFlocks; m++)
            //{
            //    for (int p = 0; p < NumPerchInFlock; p++)
            //    {
            //        if (Double.IsNaN(flock[m, p].coords[0]) || Double.IsNaN(flock[m, p].coords[1]))
            //            throw new Exception();
            //    }
            //}
        }

        private void BestFlockSwim() // пересчет функции: 1.5 * NStep * перчей в одной стае
        {
            for (int m = 0; m < NumFlocks; m++)
            {
                for (int p = 0; p < NumPerchInFlock; p++)
                {
                    if (Double.IsNaN(flock[m, p].coords[0]) || Double.IsNaN(flock[m, p].coords[1]))
                        throw new Exception();
                }
            }
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

                    if (x < D[0,0] || x > D[0,1] || y < D[1,0] || y > D[1,1]) // если окуни вышли за границы, оставляем их прежние позиции
                    {
                        x = flock[i, j].coords[0];
                        y = flock[i, j].coords[1];
                    }
                    //if (Double.IsNaN(x) || Double.IsNaN(y))
                    //    throw new Exception();
                    move.Add(new Perch(x, y, function(x, y, f)));
                }
                //for (int m = 0; m < NumFlocks; m++)
                //{
                //    for (int p = 0; p < NumPerchInFlock; p++)
                //    {
                //        if (Double.IsNaN(flock[m, p].coords[0]) || Double.IsNaN(flock[m, p].coords[1]))
                //            throw new Exception();
                //    }
                //}
                //Sort(move);
                move = move.OrderBy(s => s.fitness).ToList();
                flock[i, j] = move[0];
                //if (Double.IsNaN(flock[i, j].coords[0]) || Double.IsNaN(flock[i, j].coords[1]))
                //    throw new Exception();
            }
            Sort(flock, i);
            //if (Double.IsNaN(flock[NumFlocks - 1, 0].coords[0]) || Double.IsNaN(flock[NumFlocks - 1, 0].coords[1]))
            //    throw new Exception();
            //for (int m = 0; m < NumFlocks; m++)
            //{
            //    for (int p = 0; p < NumPerchInFlock; p++)
            //    {
            //        if (Double.IsNaN(flock[m, p].coords[0]) || Double.IsNaN(flock[m, p].coords[1]))
            //            throw new Exception();
            //    }
            //}
        }

        /// <summary>Движение средних окуней</summary>
        private void AverFlockSwim() // пересчет функции: 0.8 * NStep * все окуни, кроме лучших и худших
        {
            sigma = rand.NextDouble() / 20 + 0.6; // sigma [0.6,  0.8] 

            for (int l = 1; l < NumFlocks - 1; l++) // если не изменяет память, передвижение лидеров средних стай
            {
                double x = 0;
                double y = 0;
                int moveCount = (int)Math.Floor(sigma * NStep);

                List<Perch> move = new List<Perch>();
                for (int k = 0; k < moveCount; ++k)
                {
                    x = flock[l, 0].coords[0] + k * ((flock[0, 0].coords[0] - flock[l, 0].coords[0]) / (NStep));
                    y = flock[l, 0].coords[1] + k * ((flock[0, 0].coords[1] - flock[l, 0].coords[1]) / (NStep));
                    //if (Double.IsNaN(x) || Double.IsNaN(y))
                    //    throw new Exception();

                    if (x < D[0, 0] || x > D[0, 1] || y < D[1, 0] || y > D[1, 1]) // если лидеры вышли за границу, оставляем их прежние позиции
                    {
                        x = flock[l, 0].coords[0];
                        y = flock[l, 0].coords[1];
                    }
                    move.Add(new Perch(x, y, function(x, y, f)));
                }

                //Sort(move);
                move = move.OrderBy(s => s.fitness).ToList();
                flock[l, 0] = move[0];


                for (int j = 0; j < NumPerchInFlock; j++)
                {
                    double x1 = 0;
                    double y1 = 0;
                    int moveCount1 = (int)Math.Floor(sigma * NStep);

                    List<Perch> move1 = new List<Perch>();
                    for (int k = 0; k < moveCount1; ++k)
                    {
                        x1 = flock[l, j].coords[0] + k * ((flock[l, 0].coords[0] - flock[l, j].coords[0]) / (NStep));
                        y1 = flock[l, j].coords[1] + k * ((flock[l, 0].coords[1] - flock[l, j].coords[1]) / (NStep));
                        //if (Double.IsNaN(x1) || Double.IsNaN(y1))
                        //    throw new Exception();
                        if (x1 < D[0, 0] || x1 > D[0, 1] || y1 < D[1, 0] || y1 > D[1, 1]) // если не-лидеры средних стай вышли за гранмцы, оставляем их прежние позиции
                        {
                            x1 = flock[l, j].coords[0];
                            y1 = flock[l, j].coords[1];
                        }
                        move1.Add(new Perch(x1, y1, function(x1, y1, f)));
                    }
                    //Sort(move1);
                    move1 = move1.OrderBy(s => s.fitness).ToList();
                    flock[l, j] = move1[0];
                }
                Sort(flock, l);
            }
            SortFlocks();
            //if (Double.IsNaN(flock[NumFlocks - 1, 0].coords[0]) || Double.IsNaN(flock[NumFlocks - 1, 0].coords[1]))
            //    throw new Exception();
            //
            //for (int i = 0; i < NumFlocks; i++)
            //{
            //    for (int j = 0; j < NumPerchInFlock; j++)
            //    {
            //        if (Double.IsNaN(flock[i, j].coords[0]) || Double.IsNaN(flock[i, j].coords[1]))
            //            throw new Exception();
            //    }
            //}
        }

        private void PoorFlockSwim() // пересчет функции: 0.5 * NStep * окуней в одной стае (худшей)
        {
            //for (int d = 0; d < NumFlocks; d++)
            //{
            //    for (int p = 0; p < NumPerchInFlock; p++)
            //    {
            //        if (Double.IsNaN(flock[d, p].coords[0]) || Double.IsNaN(flock[d, p].coords[1]))
            //            throw new Exception();
            //    }
            //}
            double xPoorLeader = flock[NumFlocks - 1, 0].coords[0];
            double yPoorLeader = flock[NumFlocks - 1, 0].coords[1];

            PoorLeaderSwim();
            //int numTries = 10;
            //
            if (flock[NumFlocks - 1, 0].coords[0] > D[0, 1] || flock[NumFlocks - 1, 0].coords[0] < D[0, 0]
                || flock[NumFlocks - 1, 0].coords[1] > D[1, 1] || flock[NumFlocks - 1, 0].coords[1] < D[1, 0])
                flock[NumFlocks - 1, 0].coords[0] = xPoorLeader;
                flock[NumFlocks - 1, 0].coords[1] = yPoorLeader;
            //for (int d = 0; d < NumFlocks; d++)
            //{
            //    for (int p = 0; p < NumPerchInFlock; p++)
            //    {
            //        if (Double.IsNaN(flock[d, p].coords[0]) || Double.IsNaN(flock[d, p].coords[1]))
            //            throw new Exception();
            //    }
            //}

            sigma = rand.NextDouble() * 0.4 + 0.1; // sigma [0.1,  0.5]

            double xMin = Math.Min((flock[NumFlocks - 1, 0].coords[0] - D[0, 0]), (D[0, 1] - flock[NumFlocks - 1, 0].coords[0]));
            double yMin = Math.Min((flock[NumFlocks - 1, 0].coords[1] - D[1, 0]), (D[1, 1] - flock[NumFlocks - 1, 0].coords[1]));

            for (int j = 1; j < NumPerchInFlock; j++)
            {
                double x1 = (flock[NumFlocks - 1, 0].coords[0] - xMin);
                double x2 = (flock[NumFlocks - 1, 0].coords[0] + xMin);
                double y1 = (flock[NumFlocks - 1, 0].coords[1] - yMin);
                double y2 = (flock[NumFlocks - 1, 0].coords[1] + yMin);
                double xRes = 0;
                double yRes = 0;
                //do // TODO: Добавлены ограничения на x,y
                //{ // спутся пару месяцев оказалось, что это неправильно работает

                    xRes = x1 + rand.NextDouble() * (x2 - x1);
                    yRes = y1 + rand.NextDouble() * (y2 - y1);

                    //x = ((rand.NextDouble()) * 2 - 1) * (flock[NumFlocks - 1, 0].coords[0] - xMin);
                    //y = ((rand.NextDouble()) * 2 - 1) * (flock[NumFlocks - 1, 0].coords[1] - yMin);
                //} while (xRes > D[0, 1] || xRes < D[0, 0] || yRes > D[1, 1] || yRes < D[1, 0]);
                
                //if (Double.IsNaN(x) || Double.IsNaN(y))
                //    throw new Exception();
                flock[NumFlocks - 1, j] = new Perch(xRes, yRes, function(xRes, yRes, f));
            }

            int i = 1;


            for (int j = 0; j < NumPerchInFlock; j++) // всех окуней из худших двигаем к лидеру худшей стаи
            {
                double x = 0;
                double y = 0;
                int moveCount = (int)Math.Floor(sigma * NStep);

                List<Perch> move = new List<Perch>();
                for (int k = 0; k < moveCount; ++k)
                {
                    x = flock[i, j].coords[0] + k * ((flock[i, 0].coords[0] - flock[i, j].coords[0]) / (NStep));
                    y = flock[i, j].coords[1] + k * ((flock[i, 0].coords[1] - flock[i, j].coords[1]) / (NStep));
                    if (x < D[0, 0] || x > D[0, 1] || y < D[1, 0] || y > D[1, 1]) // если окуни вышли за границы, оставляем их прежние позиции
                    {
                        x = flock[i, j].coords[0];
                        y = flock[i, j].coords[1];
                    }    
                    //if (Double.IsNaN(x) || Double.IsNaN(y))
                    //    throw new Exception();
                    move.Add(new Perch(x, y, function(x, y, f)));
                }
                //Sort(move);
                move = move.OrderBy(s => s.fitness).ToList();
                flock[i, j] = move[0];
            }
            Sort(flock, i);
        }

        /// <summary>Новые координаты лидера худшей стаи</summary>
        public void PoorLeaderSwim()
        {

            //TODO: ПОЛУЧАЕТСЯ NAN, ПРОБЛЕМА С ВОЗВЕДЕНИЕМ В ДРОБНУЮ СТЕПЕНЬ ОТРИЦАТЕЛЬНОГО ЧИСЛА. Вроде решено
            double a = LeviX();
            double b = LeviY();
            flock[NumFlocks - 1, 0].coords[0] = flock[NumFlocks - 1, 0].coords[0] + (alfa / currentIteration) * a;
            flock[NumFlocks - 1, 0].coords[1] = flock[NumFlocks - 1, 0].coords[1] + (alfa / currentIteration) * b;
            //for (int d = 0; d < NumFlocks; d++)
            //{
            //    for (int p = 0; p < NumPerchInFlock; p++)
            //    {
            //        if (Double.IsNaN(flock[d, p].coords[0]) || Double.IsNaN(flock[d, p].coords[1]))
            //            throw new Exception();
            //    }
            //}
        }

        /// <summary>Распределение Леви, координата х,  для худшей стаи</summary>
        public double LeviX()
        {
            R1 = rand.Next(Convert.ToInt32(0), Convert.ToInt32((D[0,1] - D[0, 0]) * 100)) / 100f; // (0, b1-a1)
            thetta1 = R1 * 2 * Math.PI;
            L1 = Math.Pow(R1 + 0.0001f, -1 / lambda);

            x = L1 * Math.Sin(thetta1);
            return x;
        }

        /// <summary>Распределение Леви, координата y,  для худшей стаи</summary>
        public double LeviY()
        {
            R2 = rand.Next(Convert.ToInt32(0), Convert.ToInt32((D[1, 1] - D[1, 0]) * 100)) / 100f; // (0, b2-a2)
            thetta2 = R2 * 2 * Math.PI;
            L2 = Math.Pow(R2 + 0.0001f, -1 / lambda);   //!

            y = L2 * Math.Cos(thetta2);
            return y;
        }

        /// <summary>Начальное формирование популяции </summary>
        public void FormingPopulation() // пересчет функции: Количество окуней в популяции
        {
            for (int i = 0; i < population; i++)
            {
                x = 0;
                y = 0;

                x = D[0, 0] + rand.NextDouble() * (D[0, 1] - D[0, 0]);
                y = D[1, 0] + rand.NextDouble() * (D[1, 1] - D[1, 0]);
                //if (Double.IsNaN(x) || Double.IsNaN(y))
                //    throw new Exception();
                Perch perch = new Perch(x, y, function(x, y, f)); // TODO: добавить iter += 1
                individuals.Add(perch);
            }
        }

        public void BestAnswer()
        {
            for (int pr  = 0; pr < PRmax; pr++)
            {
                List<Perch> answers = new List<Perch>();
                for (int i = 0; i < 3; i++)
                {
                    int randomIndex = rand.Next(0, Pool.Count());
                    answers.Add(Pool[randomIndex]);
                }
                // TODO: добавить конец перекоммутации. Ну он появился
            }
        }

        public Perch StartAlg(int MaxCount, double[,] D, int f, 
            int NumFlocks, int NumPerchInFlock, 
            int NStep,
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
            this.lambda = lambda;
            this.alfa = alfa;
            this.PRmax = PRmax;
            this.deltapr = deltapr;
            functionCalls = 0;


            FormingPopulation();


            for (int currentIteration = 1; currentIteration < MaxCount; currentIteration++)
            {
                MakeFlocks();
                MoveEPerchEFlock();
                FlocksSwim();
                this.currentIteration++;
            }
            Recommutation();
            //perch = individuals[0];
            perch = Pool[0];
            Console.WriteLine("Запусков целевой функции: {0}", functionCalls);
            return perch;
        }

        public void FlocksSwim()
        {

            BestFlockSwim();
            PoorFlockSwim();
            AverFlockSwim();

            individuals = new List<Perch>();

            for (int i = 0; i < NumFlocks; i++)
            {
                for (int j = 0; j < NumPerchInFlock; j++)
                {
                    individuals.Add(flock[i, j]);
                }
            }
            Pool.Add(flock[0, 0]);
            //if (Double.IsNaN(flock[0, 0].coords[0]) || Double.IsNaN(flock[0, 0].coords[1]))
            //    throw new Exception();
            //for (int m = 0; m < NumFlocks; m++)
            //{
            //    for (int p = 0; p < NumPerchInFlock; p++)
            //    {
            //        if (Double.IsNaN(flock[m, p].coords[0]) || Double.IsNaN(flock[m, p].coords[1]))
            //            throw new Exception();
            //    }
            //}

        }

        private float function(double x1, double x2, int f)
        {
            float funct = 0;
            functionCalls += 1;
            if (f == 0) // Швефель
            {
                funct = (float)(-(x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2)))));
            }
            else if (f == 1) // Мульти
                funct = (float)(-(x1 * Math.Sin(4 * Math.PI * x1) - x2 * Math.Sin(4 * Math.PI * x2 + Math.PI) + 1));
            else if (f == 2) // корневая
            {
                double[] c6 = Cpow(x1, x2, 6);
                funct = (float)(-1 / (1 + Math.Sqrt((c6[0] - 1) * (c6[0] - 1) + c6[1] * c6[1])));
            }
            else if (f == 3) // Шафер
                funct = (float)(-(0.5 - (Math.Pow(Math.Sin(Math.Sqrt(x1 * x1 + x2 * x2)), 2) - 0.5) / (1 + 0.001 * (x1 * x1 + x2 * x2))));
            else if (f == 4) // Растригин
            {
                funct = (float)(-(-20 + (-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(2 * Math.PI * x2))));
            }
            else if (f == 5) // Эклея
            {
                funct = (float)(-(-Math.E + 20 * Math.Exp(-0.2 * Math.Sqrt((x1 * x1 + x2 * x2) / 2)) + Math.Exp((Math.Cos(2 * Math.PI * x1) + Math.Cos(2 * Math.PI * x2)) / 2)));
            }
            else if (f == 6) // skin
            {
                funct = (float)(-(Math.Pow(Math.Cos(2 * x1 * x1) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x1) - 1.2, 2) - Math.Pow(Math.Cos(2 * x2 * x2) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x2) - 1.2, 2)));
            }
            else if (f == 7) //Trapfall
            {
                funct = (float)(-(-Math.Sqrt(Math.Abs(Math.Sin(Math.Sin(Math.Sqrt(Math.Abs(Math.Sin(x1 - 1))) + Math.Sqrt(Math.Abs(Math.Sin(x2 + 2))))))) + 1));
            }
            else if (f == 8) // Розенброк
            {
                funct = (float)(-(-(1 - x1) * (1 - x1) - 100 * (x2 - x1 * x1) * (x2 - x1 * x1)));
            }
            else if (f == 9) // Параболическая
            {
                funct = (float)(x1 * x1 + x2 * x2);
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
            for (int i = 0; i < NumFlocks; i++)
                for (int j = 0; j < NumPerchInFlock; j++)
                {
                    sum += flock[i, j].fitness;
                }

            double fitness = (sum / population);
            averageFitness.Add(fitness);
            return fitness;
        }

        

        public void Recommutation()
        {
            int p, q, r;

            for (int pr = 0; pr < PRmax; pr++)
            {
                    Perch perchResult = new Perch();
                    Perch Xp_pool = new Perch();
                    Perch Xq_pool = new Perch();
                    Perch Xr_pool = new Perch(); ;
                    p = rand.Next(0, MaxCount - 1);
                    q = rand.Next(0, MaxCount - 1);
                    while(q == p)
                    {
                        q = rand.Next(0, MaxCount - 1);
                    }
                    r = rand.Next(0, MaxCount - 1);
                    while(r == p || r == q)
                    {
                        r = rand.Next(0, MaxCount - 1);
                    }

                    Xp_pool = Pool[p];
                    Xq_pool = Pool[q];
                    Xr_pool = Pool[r];

                    double min = 1000;
                    for (int i = 0; i < deltapr - 1; i++)
                    {
                        double x = Xq_pool.coords[0] + i * (Xq_pool.coords[0] - Xq_pool.coords[0]) / deltapr;
                        double y = Xq_pool.coords[1] + i * (Xq_pool.coords[1] - Xq_pool.coords[1]) / deltapr;
                        double res = function(x, y, f);
                        if (res < min)
                        {
                            perchResult = new Perch(x, y, res);
                            min = res;
                        }
                    }

                    min = 1000;
                    Perch inPool = new Perch();
                    for (int i = 0; i < deltapr - 1; i++)
                    {
                        double x = perchResult.coords[0] + i * (Xr_pool.coords[0] - perchResult.coords[0]) / deltapr;
                        double y = perchResult.coords[1] + i * (Xr_pool.coords[1] - perchResult.coords[1]) / deltapr;
                    //    if (Double.IsNaN(x) || Double.IsNaN(y))
                    //        throw new Exception();

                    double res = function(x, y, f);
                        if (res < min)
                        {
                            inPool = new Perch(x, y, res);
                            min = res;
                        }
                    }
                    Pool.Add(inPool);
            }

            //Sort(Pool);
            Pool = Pool.OrderBy(s => s.fitness).ToList();
            result = Pool[0];
            //if (Double.IsNaN(result.coords[0]) || Double.IsNaN(result.coords[1]))
            //    throw new Exception();
        }
    }
}
