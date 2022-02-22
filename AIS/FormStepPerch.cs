using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIS
{
    public partial class FormStepPerch : Form
    {
        public FormStepPerch(int z, double[,] obl, int MaxIteration, int NumFlocks, int NumPerchInFlock, int NStep, double lambda, double alfa, int PRmax, int deltapr, double exact)
        {
            InitializeComponent();

            this.z = z;
            this.obl = obl;
            this.MaxIteration = MaxIteration;

            this.NumFlocks = NumFlocks;
            this.NumPerchInFlock = NumPerchInFlock;
            this.NStep = NStep;

            this.lambda = lambda;
            this.alfa = alfa;

            this.PRmax = PRmax;
            this.deltapr = deltapr;
            int population = NumFlocks * NumPerchInFlock;
            this.exact = exact;
        }

        
        int MaxIteration;

        int NumFlocks;
        int NumPerchInFlock;
        int population;
        int NStep;

        double lambda;
        double alfa;

        int PRmax;
        int deltapr;

        double exact;


        /// <summary>Номер функции</summary>
        public int z;

        public float[] Ar = new float[8];
        public bool[] flines = new bool[8];
        public float[] A = new float[8];
        public double[,] showoblbase = new double[2, 2];
        public double[,] oblbase = new double[2, 2];
        public double[,] obl;
        public int stepsCount = 9; // TODO: Что это вообще?

        public AlgorithmPerch algo = new AlgorithmPerch();

        public double[,] showobl = new double[2, 2];
        
        bool flag = false;
        /// <summary>Массив состояний</summary>
        bool[] Red = new bool[9];

        public int iterationGraph = 0;

        private float function(double x1, double x2, int f)
        {
            float funct = 0;
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
            else if (f == 3) // Шаффер
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

        /// <summary>Степень для комплексного числа</summary>
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

        /// <summary>Отрисовка стрелочек</summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pBlack = new Pen(Color.Black, 2);
            Pen pGray  = new Pen(Color.Gray, 2);
            Pen pRed   = new Pen(Color.Red, 2);
            Font f1    = new Font("TimesNewRoman", 12, FontStyle.Bold);

            e.Graphics.DrawLine(pBlack, 250, 250, 250, 135);        // проверка -> деление  вертик 
            e.Graphics.DrawLine(pBlack, 250, 135, 135, 135);        // проверка -> деление  горизонт
            e.Graphics.DrawLine(pBlack, 135+2, 134, 145+3, 130-1);          // Верхнее крыло повернутой стрелочки
            e.Graphics.DrawLine(pBlack, 135+2, 134, 145+3, 140);            // Нижнее крыло повернутой стрелочки

            if (Red[6] == true)
            {
                e.Graphics.DrawLine(pRed, 250, 250, 250, 135);      // проверка -> деление  вертик 
                e.Graphics.DrawLine(pRed, 250, 135, 140, 135);      // проверка -> деление  горизонт
                e.Graphics.DrawLine(pRed, 135 + 2, 134, 145 + 3, 130 - 1);
                e.Graphics.DrawLine(pRed, 135 + 2, 134, 145 + 3, 140);
            }

            e.Graphics.DrawLine(pBlack, 140, 405, 250, 405);        // лидер Pool -> проверка горизонт

            e.Graphics.DrawLine(pBlack, 250, 405, 250, 210);        // лидер Pool -> проверка вертик
            e.Graphics.DrawLine(pBlack, 250, 258, 255, 268);
            e.Graphics.DrawLine(pBlack, 249, 258, 244, 268);

            if (Red[4] == true)
            {
                e.Graphics.DrawLine(pRed, 140, 405, 250, 405);      // лидер Pool -> проверка горизонт
                                     
                e.Graphics.DrawLine(pRed, 250, 405, 250, 210);      // лидер Pool -> проверка вертик
                e.Graphics.DrawLine(pRed, 250, 258, 255, 268);
                e.Graphics.DrawLine(pRed, 249, 258, 244, 268);
            }

            e.Graphics.DrawLine(pBlack, 70, 300, 70, 373);          // плаванье -> лидер Pool 
            e.Graphics.DrawLine(pBlack, 70, 370, 75, 360);
            e.Graphics.DrawLine(pBlack, 69, 370, 64, 360);

            if (Red[3] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 300, 70, 373);        // плаванье -> лидер Pool 
                e.Graphics.DrawLine(pRed, 70, 370, 75, 360);
                e.Graphics.DrawLine(pRed, 69, 370, 64, 360);
            }

            e.Graphics.DrawLine(pBlack, 70, 210, 70, 283);          // котлы -> плаванье !!!
            e.Graphics.DrawLine(pBlack, 70, 280, 75, 270);
            e.Graphics.DrawLine(pBlack, 69, 280, 64, 270);

            if (Red[2] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 210, 70, 283);        // котлы -> плаванье !!!
                e.Graphics.DrawLine(pRed, 70, 280, 75, 270);
                e.Graphics.DrawLine(pRed, 69, 280, 64, 270);
            }

            e.Graphics.DrawLine(pBlack, 70, 120, 70, 193);          // деление -> котлы
            e.Graphics.DrawLine(pBlack, 70, 195, 75, 185);
            e.Graphics.DrawLine(pBlack, 69, 195, 64, 185);

            if (Red[1] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 120, 70, 193);        // деление -> котлы
                e.Graphics.DrawLine(pRed, 70, 195, 75, 185);
                e.Graphics.DrawLine(pRed, 69, 195, 64, 185);
            }

            e.Graphics.DrawLine(pBlack, 70, 30, 70, 105);           // генерация -> деление
            e.Graphics.DrawLine(pBlack, 70, 102, 75, 92);
            e.Graphics.DrawLine(pBlack, 69, 102, 64, 92);

            if (Red[0] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 30, 70, 105);         // генерация -> деление
                e.Graphics.DrawLine(pRed, 70, 102, 75, 92);
                e.Graphics.DrawLine(pRed, 69, 102, 64, 92);
            }


            e.Graphics.DrawLine(pBlack, 310, 225, 340, 225);        // проверка -> интенсивный поиск  горизонт
            e.Graphics.DrawLine(pBlack, 340, 225, 340, 530);        // проверка -> интенсивный поиск  вертик
            e.Graphics.DrawLine(pBlack, 310, 530, 340, 530);        // проверка -> интенсивный поиск  горизонт
            e.Graphics.DrawLine(pBlack, 310, 305+224, 321, 300+224);        // Верхнее крыло повернутой стрелочки
            e.Graphics.DrawLine(pBlack, 310, 305+224, 321, 311+225);        // Нижнее крыло повернутой стрелочки
            
            if (Red[7] == true)
            {
                e.Graphics.DrawLine(pRed, 310, 225, 340, 225);      // проверка -> интенсивный поиск  горизонт
                e.Graphics.DrawLine(pRed, 340, 225, 340, 530);      // проверка -> интенсивный поиск  вертик
                e.Graphics.DrawLine(pRed, 310, 530, 340, 530);      // проверка -> интенсивный поиск  горизонт
                e.Graphics.DrawLine(pRed, 310, 305 + 224, 321, 300 + 224);  // Верхнее крыло повернутой стрелочки
                e.Graphics.DrawLine(pRed, 310, 305 + 224, 321, 311 + 225);  // Нижнее крыло повернутой стрелочки 
            }

            e.Graphics.DrawLine(pBlack, 70, 530, 310, 530);         // интенсивный поиск -> окончание  горизонт НУЖНА СТРЕЛОЧКА
            e.Graphics.DrawLine(pBlack, 137, 305 + 224, 148, 300 + 224);    // Верхнее крыло повернутой стрелочки 
            e.Graphics.DrawLine(pBlack, 137, 305 + 224, 148, 311 + 225);    // Нижнее крыло повернутой стрелочки 
            if (Red[8] == true) 
            {
                e.Graphics.DrawLine(pRed, 70, 530, 310, 530);       // интенсивный поиск -> окончание  горизонт НУЖНА СТРЕЛОЧКА
                e.Graphics.DrawLine(pRed, 137, 305 + 224, 148, 300 + 224);  // Верхнее крыло повернутой стрелочки 
                e.Graphics.DrawLine(pRed, 137, 305 + 224, 148, 311 + 225);  // Нижнее крыло повернутой стрелочки 
            }

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox2.Width;
            float h = pictureBox2.Height;
            float x0 = w / 2;
            float y0 = h / 2;
            float a = 30;
            float k = 1;

            List<PointF> points = new List<PointF>();
            Pen p1  = new Pen(Color.PaleGreen, 1);
            Pen p2  = new Pen(Color.GreenYellow, 1);
            Pen p3  = new Pen(Color.YellowGreen, 1);
            Pen p4  = new Pen(Color.Yellow, 1);
            Pen p5  = new Pen(Color.Orange, 1);
            Pen p6  = new Pen(Color.OrangeRed, 1);
            Pen p7  = new Pen(Color.Red, 1);
            Pen p8  = new Pen(Color.Brown, 1);
            Pen p9  = new Pen(Color.Maroon, 1);
            Pen p10 = new Pen(Color.Black, 1);
            Pen p11 = new Pen(Color.Blue, 4);

            Pen p12 = new Pen(Color.DarkOrange, 2);
            Pen p13 = new Pen(Color.DarkGreen, 2);
            Pen p14 = new Pen(Color.Red, 2);

            Font font1 = new Font("TimesNewRoman", 10, FontStyle.Bold | FontStyle.Italic);
            Font font2 = new Font("TimesNewRoman", 8);

            pictureBox2.BackColor = Color.White;

            double x1 = showobl[0, 0];
            double x2 = showobl[0, 1];
            double y1 = showobl[1, 0];
            double y2 = showobl[1, 1];

            double a1 = Ar[0];//5
            double a3 = Ar[1];//4
            double a5 = Ar[2];//3
            double a7 = Ar[3];//2
            double a9 = Ar[4];//1

            double a10 = Ar[5];//6  
            double a11 = Ar[6];//7
            double a12 = Ar[7];//8

            double dx = x2 - x1;
            double dy = y2 - y1;
            double dxy = dx - dy;

            double bxy = Math.Max(dx, dy);
            double step = 0;
            if (bxy < 1.1) step = 0.1;
            else if (bxy < 2.1) step = 0.2;
            else if (bxy < 5.1) step = 0.5;
            else if (bxy < 10.1) step = 1;
            else if (bxy < 20.1) step = 2;
            else if (bxy < 50.1) step = 5;
            else if (bxy < 100.1) step = 10;
            else if (bxy < 200.1) step = 20;
            else if (bxy < 500.1) step = 50;
            else if (bxy < 1000.1) step = 100;
            else if (bxy < 2000.1) step = 200;
            else step = 1000;

            if (dxy > 0)
            {
                y1 = y1 - dxy / 2;
                y2 = y2 + dxy / 2;
            }
            else if (dxy < 0)
            {
                x1 = x1 - Math.Abs(dxy) / 2;
                x2 = x2 + Math.Abs(dxy) / 2;
            }
            x1 = x1 - 0.05 * Math.Abs(x2 - x1);
            x2 = x2 + 0.05 * Math.Abs(x2 - x1);
            y1 = y1 - 0.05 * Math.Abs(y2 - y1);
            y2 = y2 + 0.05 * Math.Abs(y2 - y1);

            float mw = k * (w) / ((float)(Math.Max(x2 - x1, y2 - y1)));
            float mh = k * (h) / ((float)(Math.Max(x2 - x1, y2 - y1)));
            for (int ii = 0; ii < w; ii++)
                for (int jj = 0; jj < h; jj++)
                {
                    double i = (ii * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                    double j = (jj * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                    double i1 = ((ii + 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                    double j1 = ((jj + 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                    double i0 = ((ii - 1) * (Math.Max(x2 - x1, y2 - y1)) / w + x1) / k;
                    double j0 = ((jj - 1) * (Math.Max(x2 - x1, y2 - y1)) / h + y1) / k;
                    double f = function(i, j, z);
                    double f2 = function(i0, j, z);
                    double f3 = function(i, j0, z);
                    double f4 = function(i1, j, z);
                    double f5 = function(i, j1, z);
                    double f6 = function(i1, j1, z);
                    double f7 = function(i0, j1, z);
                    double f8 = function(i1, j0, z);
                    double f9 = function(i0, j0, z);

                    if (((f2 < a1) || (f3 < a1) || (f4 < a1) || (f5 < a1) || (f6 < a1) || (f7 < a1) || (f8 < a1) || (f9 < a1)) && (f > a1) && (flines[4] == true)) e.Graphics.FillRectangle(Brushes.PaleGreen, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a3) || (f3 < a3) || (f4 < a3) || (f5 < a3) || (f6 < a3) || (f7 < a3) || (f8 < a3) || (f9 < a3)) && (f > a3) && (flines[3] == true)) e.Graphics.FillRectangle(Brushes.YellowGreen, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a5) || (f3 < a5) || (f4 < a5) || (f5 < a5) || (f6 < a5) || (f7 < a5) || (f8 < a5) || (f9 < a5)) && (f > a5) && (flines[2] == true)) e.Graphics.FillRectangle(Brushes.Orange, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a7) || (f3 < a7) || (f4 < a7) || (f5 < a7) || (f6 < a7) || (f7 < a7) || (f8 < a7) || (f9 < a7)) && (f > a7) && (flines[1] == true)) e.Graphics.FillRectangle(Brushes.Red, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a9) || (f3 < a9) || (f4 < a9) || (f5 < a9) || (f6 < a9) || (f7 < a9) || (f8 < a9) || (f9 < a9)) && (f > a9) && (flines[0] == true)) e.Graphics.FillRectangle(Brushes.Maroon, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a10) || (f3 < a10) || (f4 < a10) || (f5 < a10) || (f6 < a10) || (f7 < a10) || (f8 < a10) || (f9 < a10)) && (f > a10) && (flines[5] == true)) e.Graphics.FillRectangle(Brushes.Pink, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a11) || (f3 < a11) || (f4 < a11) || (f5 < a11) || (f6 < a11) || (f7 < a11) || (f8 < a11) || (f9 < a11)) && (f > a11) && (flines[6] == true)) e.Graphics.FillRectangle(Brushes.Violet, (float)(ii), (float)(h - jj), 1, 1);
                    else if (((f2 < a12) || (f3 < a12) || (f4 < a12) || (f5 < a12) || (f6 < a12) || (f7 < a12) || (f8 < a12) || (f9 < a12)) && (f > a12) && (flines[7] == true)) e.Graphics.FillRectangle(Brushes.MediumOrchid, (float)(ii), (float)(h - jj), 1, 1);
                }

            if (flag) 
            {
                if ((Red[1] == true) || (Red[2] == true) || (Red[3] == true))
                {

                    //for (int i = 0; i < NumPerchInFlock; i++) // раскраска лучших окуней
                    //    e.Graphics.FillEllipse(Brushes.Red, (float)((algo.flock[0, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algo.flock[0, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);

                    for (int i = 0; i < NumPerchInFlock; i++) // раскраска худших окуней
                        e.Graphics.FillEllipse(Brushes.DarkGreen, (float)((algo.flock[NumFlocks - 1, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algo.flock[NumFlocks - 1, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                    for (int j = 1; j < NumFlocks - 1; j++) // раскраска остальных окуней
                    {

                        for (int i = 0; i < NumPerchInFlock; i++)
                            e.Graphics.FillEllipse(Brushes.Blue, (float)((algo.flock[j, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algo.flock[j, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                    }

                    for (int i = 0; i < NumPerchInFlock; i++) // раскраска лучших окуней
                        e.Graphics.FillEllipse(Brushes.Red, (float)((algo.flock[0, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algo.flock[0, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                }
                else
                {
                    if (algo.flock != null)
                        for (int j = 0; j < NumFlocks; j++) // раскраска остальных окуней
                            for (int i = 0; i < NumPerchInFlock; i++)
                                e.Graphics.FillEllipse(Brushes.Blue, (float)((algo.flock[j, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algo.flock[j, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                    else
                        for (int i = 0; i < algo.individuals.Count; i++)
                            e.Graphics.FillEllipse(Brushes.Blue, (float)((algo.individuals[i].coords.vector[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algo.individuals[i].coords.vector[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                }
            }

            for (int i = -6; i < 12; i++)
            {
                e.Graphics.DrawLine(p10, (float)((x1 - i * step) * w / (x1 - x2)), h - a - 5, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                e.Graphics.DrawLine(p10, a - 5, (float)(h - (y1 - i * step) * h / (y1 - y2)), a + 5, (float)(h - (y1 - i * step) * h / (y1 - y2)));
                e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, a - 30, (float)(h - 5 - (y1 - i * step) * h / (y1 - y2)));
            }

            //Стрелочки абцисс и ординат
            e.Graphics.DrawLine(p10, 0, h - a, w, h - a);
            e.Graphics.DrawLine(p10, a, h, a, 0);
            e.Graphics.DrawLine(p10, a, 0, a - 5, 10);
            e.Graphics.DrawLine(p10, a, 0, a + 5, 10);
            e.Graphics.DrawLine(p10, w - 5, h - a, w - 15, h - a - 5);
            e.Graphics.DrawLine(p10, w - 5, h - a, w - 15, h - a + 5);
            e.Graphics.DrawString("x", font1, Brushes.Black, w - 20, h - a + 5);
            e.Graphics.DrawString("y", font1, Brushes.Black, a - 20, 1);

        }

        /// <summary>Создание начальной популяции</summary>
        private void buttonInitialPopulation_Click(object sender, EventArgs e)
        {
            iterationGraph = 0;
            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            if (!flag)
            {
                Red[0] = true;
                for (int i = 1; i < stepsCount; i++)
                    Red[i] = false;

                flag = true;                   //Начало работы алгоритма

                algo = new AlgorithmPerch 
                {
                    MaxCount = MaxIteration,
                    NumFlocks = NumFlocks,
                    NumPerchInFlock = NumPerchInFlock,
                    population = NumFlocks * NumPerchInFlock,
                    f = z,
                    D = obl,
                    NStep = NStep,
                    lambda = lambda,
                    alfa = alfa,
                    PRmax = PRmax,
                    deltapr = deltapr
                };
                algo.FormingPopulation();
                buttonMakeFlocks.Enabled = true;
                buttonInitialPopulation.Enabled = false;


                pictureBox1.Refresh();
                pictureBox2.Refresh();
            }
        }

        /// <summary>Разбивка на стаи</summary>
        private void buttonMakeFlocks_Click(object sender, EventArgs e)
        {
            Red[0] = false;
            Red[1] = true;
            Red[6] = false;
            algo.MakeFlocks();

            buttonMakeFlocks.Enabled = false;
            buttonKettle.Enabled = true;

            dataGridView3.RowCount = 1;
            dataGridView3.Rows[0].Cells[0].Value = "Current iteration";

            dataGridView3.Rows[0].Cells[1].Value = string.Format($"{algo.currentIteration}");

            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        /// <summary>Помещение лидера в множество Pool</summary>
        private void buttonLeaderToPool_Click(object sender, EventArgs e)
        {

            Red[3] = false;
            Red[4] = true;
            this.buttonAnswer.Enabled = true;
            button1.Enabled = true;

            buttonLeaderToPool.Enabled = false;
            buttonCheckEndConditions.Enabled = true;

            pictureBox1.Refresh();
        }

        /// <summary>Реализация котлов</summary>
        private void buttonKettle_Click(object sender, EventArgs e)
        {

            Red[1] = false;
            Red[2] = true;
            algo.MoveEPerchEFlock();

            buttonFlocksSwim.Enabled = true;
            buttonKettle.Enabled = false;

            pictureBox1.Refresh();
            pictureBox2.Refresh();
        }

        /// <summary>Плавание стай</summary>
        private void buttonFlocksSwim_Click(object sender, EventArgs e)
        {

            Red[2] = false;
            Red[3] = true;
            algo.FlocksSwim();

            algo.best = algo.flock[0, 0];
            algo.bestFitness.Add(algo.best.fitness);
            algo.AverageFitness();

            buttonFlocksSwim.Enabled = false;
            buttonLeaderToPool.Enabled = true;

            //pictureBoxGraph.Refresh();
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            this.chart1.Series[0].Points.AddXY(iterationGraph + 1, algo.bestFitness[algo.bestFitness.Count - 1]);
            this.chart1.Series[1].Points.AddXY(iterationGraph + 1, algo.averageFitness[algo.averageFitness.Count - 1]);
            
            algo.currentIteration++;
            iterationGraph++;
            this.numericUpDown1.Maximum = algo.MaxCount - algo.currentIteration;
        }

        /// <summary>Проверка условий окончания</summary>
        private void buttonCheckEndConditions_Click(object sender, EventArgs e)
        {
            this.buttonAnswer.Enabled = false;
            buttonCheckEndConditions.Enabled = false;
            button1.Enabled = false;

            if (algo.currentIteration < algo.MaxCount) 
            {
                Red[6] = true;
                buttonMakeFlocks.Enabled = true;
            }
            else 
            {
                Red[7] = true;
                buttonSearchInPool.Enabled = true;
            }
            Red[4] = false;
            dataGridView3.RowCount = 5;
            dataGridView3.Rows[0].Cells[0].Value = "Current iteration";
            dataGridView3.Rows[1].Cells[0].Value = "Положение лучшего окуня";
            dataGridView3.Rows[2].Cells[0].Value = "f* of the best perch";
            dataGridView3.Rows[3].Cells[0].Value = "f* average";
            dataGridView3.Rows[4].Cells[0].Value = "Exact value of f";

            if (algo.currentIteration == algo.MaxCount)
            {
                dataGridView3.Rows[0].Cells[1].Value = string.Format($"{algo.currentIteration - 1}");
            }
            else
            {
                dataGridView3.Rows[0].Cells[1].Value = string.Format($"{algo.currentIteration - 1}");
            }
            dataGridView3.Rows[1].Cells[1].Value = string.Format($"({algo.best.coords[0]:F4}, {algo.best.coords[1]:F4})");
            dataGridView3.Rows[2].Cells[1].Value = string.Format($"{algo.best.fitness:F8}");
            dataGridView3.Rows[3].Cells[1].Value = string.Format($"{algo.averageFitness[algo.averageFitness.Count-1]:F8}");
            dataGridView3.Rows[4].Cells[1].Value = string.Format($"{exact:F8}");
            pictureBox1.Refresh();
        }

        /// <summary>Поиск самого лучшего окуня</summary>
        private void buttonSearchInPool_Click(object sender, EventArgs e)
        {
            if (Red[7] == true)
            {
                Red[7] = false;
                Red[8] = true;
            }

            buttonSearchInPool.Enabled = false;
            buttonChooseTheBest.Enabled = true;
            algo.Recommutation();
            pictureBox1.Refresh();
            //pictureBox2.Refresh(); // вот зачем это тут было
        }

        /// <summary>Выбор самого лучшего окуня</summary>
        private void buttonChooseTheBest_Click(object sender, EventArgs e)
        {
            if (Red[8] == true) 
            {
                pictureBox1.Refresh();
                flag = false;


                dataGridView3.RowCount = 3;
                dataGridView3.Rows[0].Cells[0].Value = "Положение лучшего окуня";
                dataGridView3.Rows[1].Cells[0].Value = "f*";
                dataGridView3.Rows[2].Cells[0].Value = "Exact value of f";

                dataGridView3.Rows[0].Cells[1].Value = string.Format($"({algo.Pool[0].coords[0]:F4}, {algo.Pool[0].coords[1]:F4})");
                dataGridView3.Rows[1].Cells[1].Value = string.Format($"{algo.Pool[0].fitness:F8}");
                dataGridView3.Rows[2].Cells[1].Value = string.Format($"{exact:F8}");

                buttonChooseTheBest.Enabled = false;
                buttonInitialPopulation.Enabled = true;
            }
        }

        private void ChartGraph_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void buttonAnswer_Click(object sender, EventArgs e)
        {
            this.buttonAnswer.Enabled = false;
            button1.Enabled = false;
            buttonInitialPopulation.Enabled = true;
            button1.Enabled = false;
            for (int i = algo.currentIteration; i < algo.MaxCount; i++)
            {
                algo.MakeFlocks();
                algo.MoveEPerchEFlock();
                algo.FlocksSwim();
                algo.currentIteration++;

                algo.best = algo.flock[0, 0];
                algo.bestFitness.Add(algo.best.fitness);
                algo.AverageFitness();
                this.chart1.Series[0].Points.AddXY(iterationGraph + 1, algo.bestFitness[algo.bestFitness.Count - 1]);
                this.chart1.Series[1].Points.AddXY(iterationGraph + 1, algo.averageFitness[algo.averageFitness.Count - 1]);
                iterationGraph++;
            }

            buttonCheckEndConditions.Enabled = false;
            Red[1] = true; Red[2] = true; Red[3] = true;
            pictureBox1.Refresh();
            pictureBox2.Refresh();

            flag = false;

            dataGridView3.RowCount = 3;
            dataGridView3.Rows[0].Cells[0].Value = "Положение лучшего окуня";
            dataGridView3.Rows[1].Cells[0].Value = "f*";
            dataGridView3.Rows[2].Cells[0].Value = "Exact value of f";

            dataGridView3.Rows[0].Cells[1].Value = string.Format($"({algo.best.coords[0]:F4}, {algo.best.coords[1]:F4})");
            dataGridView3.Rows[1].Cells[1].Value = string.Format($"{algo.best.fitness:F8}");
            dataGridView3.Rows[2].Cells[1].Value = string.Format($"{exact:F8}");

            dataGridView3.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tmp = algo.currentIteration;
            bool tmpRed0, tmpRed1, tmpRed2, tmpRed3, tmpRed4, tmpRed5, tmpRed6, tmpRed7; // так сделано, потому что я запуталась, когда какое состояние
            tmpRed0 = Red[0];
            tmpRed1 = Red[1];
            tmpRed2 = Red[2];
            tmpRed3 = Red[3];
            tmpRed4 = Red[4];
            tmpRed5 = Red[5];
            tmpRed6 = Red[6];
            tmpRed7 = Red[7];


            bool tmp2Flag = flag;
            flag = true;
            for (int i = algo.currentIteration; i < tmp + numericUpDown1.Value; i++)
            {
                //Red[1] = true; Red[2] = true; Red[3] = true;
                if (algo.currentIteration == MaxIteration)
                    break;
                if (algo.currentIteration < algo.MaxCount)
                {
                    Red[6] = true;
                    //buttonMakeFlocks.Enabled = true;
                }
                else
                {
                    Red[7] = true;
                    //buttonSearchInPool.Enabled = true;
                }
                Red[4] = false;
                Red[0] = false;
                Red[1] = true;
                Red[6] = false;
                algo.MakeFlocks();
                pictureBox2.Refresh();

                Red[1] = false;
                Red[2] = true;
                algo.MoveEPerchEFlock();
                pictureBox2.Refresh();

                Red[2] = false;
                Red[3] = true;
                algo.FlocksSwim();
                algo.currentIteration++;

                algo.best = algo.flock[0, 0];
                algo.bestFitness.Add(algo.best.fitness);
                algo.AverageFitness();
                pictureBox2.Refresh();

                Red[3] = false;
                Red[4] = true;

                this.chart1.Series[0].Points.AddXY(iterationGraph+1, algo.bestFitness[algo.bestFitness.Count - 1]);
                this.chart1.Series[1].Points.AddXY(iterationGraph+1, algo.averageFitness[algo.averageFitness.Count - 1]);
                iterationGraph++;
                pictureBox1.Refresh();
                //pictureBox2.Refresh();
                
            }
            flag = tmp2Flag;
            Red[0] = tmpRed0;
            Red[1] = tmpRed1;
            Red[2] = tmpRed2;
            Red[3] = tmpRed3;
            Red[4] = tmpRed4;
            Red[5] = tmpRed5;
            Red[6] = tmpRed6;
            Red[7] = tmpRed7;
            pictureBox1.Refresh();
            //pictureBox2.Refresh();

            //flag = false;

            dataGridView3.RowCount = 5;
            dataGridView3.Rows[0].Cells[0].Value = "Current iteration";
            dataGridView3.Rows[1].Cells[0].Value = "Положение лучшего окуня";
            dataGridView3.Rows[2].Cells[0].Value = "f* of the best perch";
            dataGridView3.Rows[3].Cells[0].Value = "f* average";
            dataGridView3.Rows[4].Cells[0].Value = "Exact value of f";


            dataGridView3.Rows[0].Cells[1].Value = string.Format($"{algo.currentIteration - 1}");
            dataGridView3.Rows[1].Cells[1].Value = string.Format($"({algo.best.coords[0]:F4}, {algo.best.coords[1]:F4})");
            dataGridView3.Rows[2].Cells[1].Value = string.Format($"{algo.best.fitness:F8}");
            dataGridView3.Rows[3].Cells[1].Value = string.Format($"{algo.averageFitness[algo.averageFitness.Count - 1]:F8}");
            dataGridView3.Rows[4].Cells[1].Value = string.Format($"{exact:F8}");

            dataGridView3.Refresh();
            this.numericUpDown1.Maximum = numericUpDown1.Maximum - numericUpDown1.Value;
            if (numericUpDown1.Value < 0)
            {
                numericUpDown1.Value = 0;
            }
        }

        private void FormStepPerch_Load(object sender, EventArgs e)
        {

        }
    }
}
