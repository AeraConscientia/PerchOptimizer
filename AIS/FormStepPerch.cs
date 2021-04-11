using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIS
{
    public partial class FormStepPerch : Form
    {
        public FormStepPerch(int z, double[,] obl, int MaxIteration, int NumFlocks, int NumPerchInFlock, int NStep, double sigma, double lambda, double alfa, int PRmax, int deltapr)
        {
            InitializeComponent();

            //population.this = population;

            this.z = z;
            this.obl = obl;
            this.MaxIteration = MaxIteration;

            this.NumFlocks = NumFlocks;
            this.NumPerchInFlock = NumPerchInFlock;
            this.NStep = NStep;
            this.sigma = sigma;

            this.lambda = lambda;
            this.alfa = alfa;

            this.PRmax = PRmax;
            this.deltapr = deltapr;
        }

        int population;
        int MaxIteration;

        int NumFlocks;
        int NumPerchInFlock;
        int NStep;
        double sigma;

        double lambda;
        double alfa;

        int PRmax;
        int deltapr;


        /// <summary>Номер функции</summary>
        public int z;

        public float[] Ar = new float[8];
        public bool[] flines = new bool[8];
        public float[] A = new float[8];
        public double[,] showoblbase = new double[2, 2];
        public double[,] oblbase = new double[2, 2];
        public double[,] obl;
        public int stepsCount = 8; // TODO: Что это вообще?

        public AlgorithmPerch algo = new AlgorithmPerch();

        public double[,] showobl = new double[2, 2];
        
        bool flag = false;
        /// <summary>Массив состояний</summary>
        bool[] Red = new bool[8];

        /*
        private void InitDataGridView()
        {
            dataGridViewAnswer.RowCount = 3;
            dataGridViewAnswer.Rows[0].Cells[0].Value = "x";
            dataGridViewAnswer.Rows[1].Cells[0].Value = "y";
            dataGridViewAnswer.Rows[2].Cells[0].Value = "f*";

            dataGridViewIterationInfo.RowCount = 6;
            dataGridViewIterationInfo.Rows[0].Cells[0].Value = "Номер популяции:"; // не надо
            dataGridViewIterationInfo.Rows[1].Cells[0].Value = "Размер популяции:";
            dataGridViewIterationInfo.Rows[2].Cells[0].Value = "Количество итераций:";
            dataGridViewIterationInfo.Rows[3].Cells[0].Value = "Окунь с лучшей приспособленностью:";
            dataGridViewIterationInfo.Rows[4].Cells[0].Value = "Приспособленность лучшего окуня:";
            dataGridViewIterationInfo.Rows[5].Cells[0].Value = "Средняя приспособленность популяции:";
        }
        */

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
            else if (f == 3) // Шафер
                funct = (float)(-(0.5 - (Math.Pow(Math.Sin(Math.Sqrt(x1 * x1 + x2 * x2)), 2) - 0.5) / (1 + 0.001 * (x1 * x1 + x2 * x2))));
            else if (f == 4) // Растригин
            {
                funct = (float)(-((-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(2 * Math.PI * x2))));
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pBlack = new Pen(Color.Black, 2);
            Pen pGray = new Pen(Color.Gray, 2);
            Pen pRed = new Pen(Color.Red, 2);
            Font f1 = new Font("TimesNewRoman", 12, FontStyle.Bold);

            e.Graphics.DrawLine(pBlack, 250, 250, 250, 135); // проверка -> деление  вертик 
            e.Graphics.DrawLine(pBlack, 250, 135, 140, 135); // проверка -> деление  горизонт НУЖНА СТРЕЛОЧКА

            if (Red[6] == true)
            {
                e.Graphics.DrawLine(pRed, 250, 250, 250, 135); // проверка -> деление  вертик 
                e.Graphics.DrawLine(pRed, 250, 135, 140, 135); // проверка -> деление  горизонт НУЖНА СТРЕЛОЧКА
            }

            e.Graphics.DrawLine(pBlack, 140, 405, 250, 405); // лидер Pool -> проверка горизонт

            e.Graphics.DrawLine(pBlack, 250, 405, 250, 210); // лидер Pool -> проверка вертик
            e.Graphics.DrawLine(pBlack, 250, 260, 255, 270);
            e.Graphics.DrawLine(pBlack, 249, 260, 244, 270);

            if (Red[4] == true)
            {
                e.Graphics.DrawLine(pRed, 140, 405, 250, 405); // лидер Pool -> проверка горизонт
                                     
                e.Graphics.DrawLine(pRed, 250, 405, 250, 210); // лидер Pool -> проверка вертик
                e.Graphics.DrawLine(pRed, 250, 260, 255, 270);
                e.Graphics.DrawLine(pRed, 249, 260, 244, 270);
            }

            e.Graphics.DrawLine(pBlack, 70, 300, 70, 373); // плаванье -> лидер Pool 
            e.Graphics.DrawLine(pBlack, 70, 370, 75, 360);
            e.Graphics.DrawLine(pBlack, 69, 370, 64, 360);

            if (Red[3] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 300, 70, 373); // плаванье -> лидер Pool 
                e.Graphics.DrawLine(pRed, 70, 370, 75, 360);
                e.Graphics.DrawLine(pRed, 69, 370, 64, 360);
            }

            e.Graphics.DrawLine(pBlack, 70, 210, 70, 283); // котлы -> плаванье !!!
            e.Graphics.DrawLine(pBlack, 70, 280, 75, 270);
            e.Graphics.DrawLine(pBlack, 69, 280, 64, 270);

            if (Red[2] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 210, 70, 283); // котлы -> плаванье !!!
                e.Graphics.DrawLine(pRed, 70, 280, 75, 270);
                e.Graphics.DrawLine(pRed, 69, 280, 64, 270);
            }

            e.Graphics.DrawLine(pBlack, 70, 120, 70, 193); // деление -> котлы
            e.Graphics.DrawLine(pBlack, 70, 195, 75, 185);
            e.Graphics.DrawLine(pBlack, 69, 195, 64, 185);

            if (Red[1] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 120, 70, 193); // деление -> котлы
                e.Graphics.DrawLine(pRed, 70, 195, 75, 185);
                e.Graphics.DrawLine(pRed, 69, 195, 64, 185);
            }

            e.Graphics.DrawLine(pBlack, 70, 30, 70, 105); // генерация -> деление
            e.Graphics.DrawLine(pBlack, 70, 102, 75, 92);
            e.Graphics.DrawLine(pBlack, 69, 102, 64, 92);

            if (Red[0] == true)
            {
                e.Graphics.DrawLine(pRed, 70, 30, 70, 105); // генерация -> деление
                e.Graphics.DrawLine(pRed, 70, 102, 75, 92);
                e.Graphics.DrawLine(pRed, 69, 102, 64, 92);
            }

            e.Graphics.DrawLine(pBlack, 310, 225, 340, 225); // проверка -> интенсивный поиск  горизонт
            e.Graphics.DrawLine(pBlack, 340, 225, 340, 530); // проверка -> интенсивный поиск  вертик
            e.Graphics.DrawLine(pBlack, 310, 530, 340, 530); // проверка -> интенсивный поиск  горизонт НУЖНА СТРЕЛОЧКА

            e.Graphics.DrawLine(pBlack, 70, 530, 340, 530); // интенсивный поиск -> окончание  горизонт НУЖНА СТРЕЛОЧКА
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Red[0] = true;
            for (int i = 1; i < stepsCount; i++)
                Red[i] = false;

            flag = true;
            pictureBox1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Red[3] = false;
            Red[4] = true;
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Red[0] = false;
            Red[1] = true;
            Red[6] = false;
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Red[1] = false;
            Red[2] = true;
            pictureBox1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Red[2] = false;
            Red[3] = true;
            pictureBox1.Refresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (true) 
            {
                Red[6] = true;
            }
            else 
            {
                Red[7] = true;
            }
            Red[4] = false;
            pictureBox1.Refresh();
        }
    }
}
