using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AIS
{
    public partial class FormMain : Form
    {
        //Мое

        private AlgorithmPerch algPerch;

        private int MaxIteration = 0;
        private Perch resultBest;
        private double[,] obl = new double[2, 2];

        List<Vector> exactPoints = new List<Vector>();

        /// <summary>Количество стай</summary>
        public int NumFlocks = 0;
        /// <summary>Количество окуней в стае</summary>
        public int NumPerchInFlock = 0;
        /// <summary>Количество шагов до окончания движения внутри стаи</summary>
        public int NStep = 0;
        /// <summary>Глубина продвижения внутри котла</summary>
        public double sigma = 0;

        public int functionCount = 0;
        public ulong functionCallsAverage = 0;

        /// <summary>Параметр распределения Леви</summary>
        public double lambda = 0;
        /// <summary>Величина шага</summary>
        public double alfa = 0;

        /// <summary>Число перекоммутаций</summary>
        public int PRmax = 0;
        /// <summary>Число шагов при перекоммутации</summary>
        public int deltapr = 0;

        //private int population = 0;
        public int population = 0;

        //Не мое
        private bool[] flines = new bool[8];
        private float k = 1;
        /// <summary>Константы для линий уровня. Тут - для минимума функции сделаны. Нужно переделать под максимум - умножить все коэффициенты на -1</summary>
        private float[] Ar = new float[8];
        private double[,] showobl = new double[2, 2];
        private bool flag = false;
        private bool flag2 = false;
        /// <summary>Точное значение функции в минимуме. Нужно переделать на максимум - домножить на -1</summary>
        private double exact = 0;

        public FormMain()
        {
            InitializeComponent();
            InitDataGridView();

            if (File.Exists("protocol.txt"))
                File.Delete("protocol.txt");

            FileStream fs = new FileStream("protocol.txt", FileMode.Append, FileAccess.Write);

            StreamWriter r = new StreamWriter(fs);
            r.Write($"+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+\r\n" +
                    $"|  Номер  |   Размер    | Кол-во |   Кол-во окуней |  Кол-во  | NStep | PRmax | delta | lambda | alfa  | Cреднее значение отклонения  |  Наименьшее значение   | Среднеквадратическое  |   Кол-во   |\r\n" +
                    $"| функции |  популяции  |  стай  |       в стае    | итераций |       |       |   pr  |        |       |    от точного решения        |       отклонения       |      отклонение       |  успехов   |\r\n" +
                    $"+---------+-------------+--------+-----------------+----------+-------+-------+-------+--------+-------+------------------------------+------------------------+-----------------------+------------+\r\n");
            r.Close();
            fs.Close();

        }

        private void InitDataGridView()
        {
            dataGridView1.RowCount = 2;
            dataGridView1.Rows[0].Cells[0].Value = "x";
            dataGridView1.Rows[1].Cells[0].Value = "y";

            dataGridView2.RowCount = 6;
            dataGridView2.Rows[0].Cells[0].Value = "Кол-во шагов до окончания движения";
            dataGridView2.Rows[0].Cells[1].Value = 100;

            dataGridView2.Rows[1].Cells[0].Value = "Maximum iteration count";// "Максимальное количество итераций";
            dataGridView2.Rows[1].Cells[1].Value = 4;

            dataGridView2.Rows[2].Cells[0].Value = "Number of flocks";//"Количество стай"; 
            dataGridView2.Rows[2].Cells[1].Value = 4;

            dataGridView2.Rows[3].Cells[0].Value = "Number of perches";//"Количество окуней в стае";
            dataGridView2.Rows[3].Cells[1].Value = 3;

            dataGridView2.Rows[4].Cells[0].Value = "Число перекоммутаций";
            dataGridView2.Rows[4].Cells[1].Value = 10;

            dataGridView2.Rows[5].Cells[0].Value = "Число шагов в перекоммутации";
            dataGridView2.Rows[5].Cells[1].Value = 5;

            dataGridView3.RowCount = 4;
            dataGridView3.Rows[0].Cells[0].Value = "x";
            dataGridView3.Rows[1].Cells[0].Value = "y";
            dataGridView3.Rows[2].Cells[0].Value = "f*";
            dataGridView3.Rows[3].Cells[0].Value = "Exact value of f";

            dataGridView4.RowCount = 2;
            dataGridView4.Rows[0].Cells[0].Value = "Distribution parameter";//"Параметр распределения";
            dataGridView4.Rows[0].Cells[1].Value = (1.5).ToString();

            dataGridView4.Rows[1].Cells[0].Value = "Величина шага";
            dataGridView4.Rows[1].Cells[1].Value = (0.6).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                //создать начальную популяцию
                if ((comboBox1.SelectedIndex != -1) )
                {
                    int z = comboBox1.SelectedIndex;

                    // область определения
                    obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                    obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                    obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                    obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

                    NStep           =   Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
                    MaxIteration    =   Convert.ToInt32(dataGridView2.Rows[1].Cells[1].Value) + 1;

                    NumFlocks       =   Convert.ToInt32(dataGridView2.Rows[2].Cells[1].Value);
                    NumPerchInFlock =   Convert.ToInt32(dataGridView2.Rows[3].Cells[1].Value);

                    lambda          =   Convert.ToDouble(dataGridView4.Rows[0].Cells[1].Value);
                    alfa            =   Convert.ToDouble(dataGridView4.Rows[1].Cells[1].Value);

                    PRmax           =   Convert.ToInt32(dataGridView2.Rows[4].Cells[1].Value);
                    deltapr         =   Convert.ToInt32(dataGridView2.Rows[5].Cells[1].Value);

                    algPerch = new AlgorithmPerch();

                    resultBest = algPerch.StartAlg(MaxIteration, obl, z, NumFlocks, NumPerchInFlock, NStep, lambda, alfa, PRmax, deltapr);
                    flag2 = true;


                    //result = algPerch.StartAlg(population, MaxIteration, obl, z, param);
                    dataGridView3.Rows[0].Cells[1].Value = string.Format($"{resultBest.coords[0]:F8}");
                    dataGridView3.Rows[1].Cells[1].Value = string.Format($"{resultBest.coords[1]:F8}");
                    dataGridView3.Rows[2].Cells[1].Value = string.Format($"{resultBest.fitness:F8}");
                    dataGridView3.Rows[3].Cells[1].Value = string.Format($"{exact:F8}");
                    //flag2 = true;
                    pictureBox1.Refresh();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedIndex != -1) )
            {
                buttonAnswer.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;

            }

            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-500";
                dataGridView1.Rows[0].Cells[2].Value = "500";
                dataGridView1.Rows[1].Cells[1].Value = "-500";
                dataGridView1.Rows[1].Cells[2].Value = "500";
                exact = -837.9658;

                Ar[0] = 200;
                Ar[1] = 1;
                Ar[2] = -300;
                Ar[3] = -600;
                Ar[4] = -800;
                exactPoints.Add(new Vector(420.9687, 420.9687));
                flag = true;
                pictureBox2.Image = Properties.Resources.ШвефельМин;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-2";
                dataGridView1.Rows[0].Cells[2].Value = "2";
                dataGridView1.Rows[1].Cells[1].Value = "-2";
                dataGridView1.Rows[1].Cells[2].Value = "2";
                exact = -4.253888;

                exactPoints.Add(new Vector(-1.6288, -1.6288));
                exactPoints.Add(new Vector(1.6288, 1.6288));
                exactPoints.Add(new Vector(-1.6288, 1.6288));
                exactPoints.Add(new Vector(1.6288, -1.6288));

                Ar[0] = 0;
                Ar[1] = -1;
                Ar[2] = -2;
                Ar[3] = -3;
                Ar[4] = -4;
                flag = true;
                pictureBox2.Image = Properties.Resources.МультиМин;

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-2";
                dataGridView1.Rows[0].Cells[2].Value = "2";
                dataGridView1.Rows[1].Cells[1].Value = "-2";
                dataGridView1.Rows[1].Cells[2].Value = "2";
                exact = -1;

                exactPoints.Add(new Vector(0.5, -0.866));
                exactPoints.Add(new Vector(-0.5, 0.866));
                exactPoints.Add(new Vector(0.5, 0.866));
                exactPoints.Add(new Vector(-0.5, -0.866));
                exactPoints.Add(new Vector(1, 0));
                exactPoints.Add(new Vector(-1, 0));

                Ar[0] = -0.2F;
                Ar[1] = -0.45F;
                Ar[2] = -0.499999F;//0.5000001F;
                Ar[3] = -0.6F;
                Ar[4] = -0.9F;
                flag = true;
                pictureBox2.Image = Properties.Resources.КорневаяМин;

            }
            else if (comboBox1.SelectedIndex == 3)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-10";
                dataGridView1.Rows[0].Cells[2].Value = "10";
                dataGridView1.Rows[1].Cells[1].Value = "-10";
                dataGridView1.Rows[1].Cells[2].Value = "10";
                exact = -1;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = -0.2F;
                Ar[1] = -0.4F;
                Ar[2] = -0.6F;//0.5000001F;
                Ar[3] = -0.8F;
                Ar[4] = -0.99F;
                flag = true;
                pictureBox2.Image = Properties.Resources.ШаферМин;

            }
            else if (comboBox1.SelectedIndex == 4)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = 20F;
                Ar[1] = 10F;
                Ar[2] = -0F;//0.5000001F;
                Ar[3] = -10F;
                Ar[4] = -19F;
                flag = true;
                pictureBox2.Image = Properties.Resources.РастригинМин;

            }
            else if (comboBox1.SelectedIndex == 5)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-10";
                dataGridView1.Rows[0].Cells[2].Value = "10";
                dataGridView1.Rows[1].Cells[1].Value = "-10";
                dataGridView1.Rows[1].Cells[2].Value = "10";
                exact = -20;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = -4F;
                Ar[1] = -7F;
                Ar[2] = -10F;//0.5000001F;
                Ar[3] = -14F;
                Ar[4] = -19F;
                flag = true;
                pictureBox2.Image = Properties.Resources.ЭклеяМин;
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = -14.060606;

                exactPoints.Add(new Vector(-3.3157, -3.0725));

                Ar[0] = -2F;
                Ar[1] = -8F;
                Ar[2] = -10F;//0.5000001F;
                Ar[3] = -12F;
                Ar[4] = -14F;
                flag = true;
                pictureBox2.Image = Properties.Resources.SkinMin;
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = -1;

                exactPoints.Add(new Vector(1, -2));

                Ar[0] = -0.1F;
                Ar[1] = -0.15F;
                Ar[2] = -0.2F;//0.5000001F;
                Ar[3] = -0.3F;
                Ar[4] = -0.5F;
                flag = true;
                pictureBox2.Image = Properties.Resources.TrapfallMin;
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-3";
                dataGridView1.Rows[0].Cells[2].Value = "3";
                dataGridView1.Rows[1].Cells[1].Value = "-1";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;

                exactPoints.Add(new Vector(1, 1));

                Ar[0] = 350F;
                Ar[1] = 180F;
                Ar[2] = 30F;//0.5000001F;
                Ar[3] = 4F;
                Ar[4] = 0.5F;
                flag = true;
                pictureBox2.Image = Properties.Resources.РозенброкМин;
            }
            else if (comboBox1.SelectedIndex == 9)
            {
                dataGridView1.Rows[0].Cells[1].Value = "-5";
                dataGridView1.Rows[0].Cells[2].Value = "5";
                dataGridView1.Rows[1].Cells[1].Value = "-5";
                dataGridView1.Rows[1].Cells[2].Value = "5";
                exact = 0;

                exactPoints.Add(new Vector(0, 0));

                Ar[0] = 7F;
                Ar[1] = 4F;
                Ar[2] = 2F;//0.5000001F;
                Ar[3] = 0.8F;
                Ar[4] = 0.1F;
                flag = true;
                pictureBox2.Image = Properties.Resources.ПараболическаяМин;
            }
            Ar[5] = 0;
            Ar[6] = 0;
            Ar[7] = 0;
            for (int i = 0; i < 5; i++)
                flines[i] = true;
            flines[5] = false;
            flines[6] = false;
            flines[7] = false;

            flag2 = false;

            showobl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
            showobl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
            showobl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
            showobl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

            pictureBox1.Refresh();
            dataGridView1.Refresh();
        }

        /// <summary>Отрисовка линий уровня. НЕ ТРОГАТЬ</summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            float w = pictureBox1.Width;
            float h = pictureBox1.Height;
            float x0 = w/2;
            float y0 = h/2;
            float a = 30;
            
            List<PointF> points = new List<PointF>();

            Pen p1 = new Pen(Color.PaleGreen, 1);
            Pen p2 = new Pen(Color.GreenYellow, 1);
            Pen p3 = new Pen(Color.YellowGreen, 1);
            Pen p4 = new Pen(Color.Yellow, 1);
            Pen p5 = new Pen(Color.Orange, 1);
            Pen p6 = new Pen(Color.OrangeRed, 1);
            Pen p7 = new Pen(Color.Red, 1);
            Pen p8 = new Pen(Color.Brown, 1);
            Pen p9 = new Pen(Color.Maroon, 1);
            Pen p10 = new Pen(Color.Black, 1);
            Pen p11 = new Pen(Color.Blue, 4);

            Font font1 = new Font("TimesNewRoman", 10, FontStyle.Bold | FontStyle.Italic);
            Font font2 = new Font("TimesNewRoman", 8);
            
            pictureBox1.BackColor = Color.White;
            if (flag)
            {
                double x1 = showobl[0, 0];
                double x2 = showobl[0, 1];
                double y1 = showobl[1, 0];
                double y2 = showobl[1, 1];

                int z = comboBox1.SelectedIndex;
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
                double dxy = dx-dy;


                double bxy = Math.Max(dx, dy);
                double step;
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

                if (dxy>0)
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

                                if (((f2 < a1) || (f3 < a1) || (f4 < a1) || (f5 < a1) || (f6 < a1) || (f7 < a1) || (f8 < a1) || (f9 < a1)) && (f > a1)&&(flines[4]==true)) e.Graphics.FillRectangle(Brushes.PaleGreen, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a3) || (f3 < a3) || (f4 < a3) || (f5 < a3) || (f6 < a3) || (f7 < a3) || (f8 < a3) || (f9 < a3)) && (f > a3)&&(flines[3]==true)) e.Graphics.FillRectangle(Brushes.YellowGreen, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a5) || (f3 < a5) || (f4 < a5) || (f5 < a5) || (f6 < a5) || (f7 < a5) || (f8 < a5) || (f9 < a5)) && (f > a5)&&(flines[2]==true)) e.Graphics.FillRectangle(Brushes.Orange, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a7) || (f3 < a7) || (f4 < a7) || (f5 < a7) || (f6 < a7) || (f7 < a7) || (f8 < a7) || (f9 < a7)) && (f > a7)&&(flines[1]==true)) e.Graphics.FillRectangle(Brushes.Red, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a9) || (f3 < a9) || (f4 < a9) || (f5 < a9) || (f6 < a9) || (f7 < a9) || (f8 < a9) || (f9 < a9)) && (f > a9)&&(flines[0]==true)) e.Graphics.FillRectangle(Brushes.Maroon, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a10) || (f3 < a10) || (f4 < a10) || (f5 < a10) || (f6 < a10) || (f7 < a10) || (f8 < a10) || (f9 < a10)) && (f > a10) && (flines[5] == true)) e.Graphics.FillRectangle(Brushes.Pink, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a11) || (f3 < a11) || (f4 < a11) || (f5 < a11) || (f6 < a11) || (f7 < a11) || (f8 < a11) || (f9 < a11)) && (f > a11) && (flines[6] == true)) e.Graphics.FillRectangle(Brushes.Violet, (float)(ii), (float)(h - jj), 1, 1);
                                else if (((f2 < a12) || (f3 < a12) || (f4 < a12) || (f5 < a12) || (f6 < a12) || (f7 < a12) || (f8 < a12) || (f9 < a12)) && (f > a12) && (flines[7] == true)) e.Graphics.FillRectangle(Brushes.MediumOrchid, (float)(ii), (float)(h - jj), 1, 1);

                            }

                        //Отрисовка результата работы алгоритма
                        if (flag2 == true)
                        {
                            //for (int i = 0; i < NumPerchInFlock; i++) // раскраска лучших окуней
                            //    e.Graphics.FillEllipse(Brushes.Red, (float)((algPerch.flock[0,i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algPerch.flock[0, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);

                            for (int i = 0; i < NumPerchInFlock; i++) // раскраска худших окуней
                                e.Graphics.FillEllipse(Brushes.DarkGreen, (float)((algPerch.flock[NumFlocks - 1, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algPerch.flock[NumFlocks - 1, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                            for (int j = 1; j < NumFlocks-1; j++) // раскраска остальных окуней
                            {

                                for (int i = 0; i < NumPerchInFlock; i++)
                                    e.Graphics.FillEllipse(Brushes.Blue, (float)((algPerch.flock[j, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algPerch.flock[j, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);
                            }

                            for (int i = 0; i < NumPerchInFlock; i++) // раскраска лучших окуней
                                e.Graphics.FillEllipse(Brushes.Red, (float)((algPerch.flock[0, i].coords[0] * k - x1) * w / (x2 - x1) - 3), (float)(h - (algPerch.flock[0, i].coords[1] * k - y1) * h / (y2 - y1) - 3), 6, 6);

                    //e.Graphics.FillEllipse(Brushes.Red, (float)((algPerch.alfa.coords.vector[0] * k - x1) * w / (x2 - x1) - 4), (float)(h - (algPerch.alfa.coords.vector[1] * k - y1) * h / (y2 - y1) - 4), 8, 8);
                }

                //отрисовка Осей
                for (int i = -6; i < 12; i++)
                        {
                            e.Graphics.DrawLine(p10, (float)((x1 - i*step) * w / (x1 - x2)), h - a - 5, (float)((x1 - i*step) * w / (x1 - x2)), h - a + 5);
                            e.Graphics.DrawLine(p10, a - 5, (float)(h - (y1 - i*step) * h / (y1 - y2)), a + 5, (float)(h - (y1 - i*step) * h / (y1 - y2)));
                            e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, (float)((x1 - i * step) * w / (x1 - x2)), h - a + 5);
                            e.Graphics.DrawString((i * step).ToString(), font2, Brushes.Black, a - 30, (float)(h -5- (y1 - i * step) * h / (y1 - y2)));
                        }
            }
            
            //Стрелочки абцисс и ординат
            p10.EndCap = LineCap.ArrowAnchor;
            e.Graphics.DrawLine(p10, 0, h - a, w - 10, h - a);
            e.Graphics.DrawLine(p10, a, h, a, 0);
            e.Graphics.DrawString("x", font1, Brushes.Black, w - 20, h - a + 5);
            e.Graphics.DrawString("y", font1, Brushes.Black, a - 20, 1);
        }

        /// <summary>Все тестовые функции</summary>
        private float function(double x1, double x2, int f)
        { 
            float funct = 0;
            if (f == 0)             // Швефель
                funct = (float) (-(x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2)))));
            else if (f == 1)        // Мульти
                funct = (float)(-(x1 * Math.Sin(4 * Math.PI * x1) - x2 * Math.Sin(4 * Math.PI * x2 + Math.PI) + 1));
            else if (f == 2)        // Корневая
            { 
                double[] c6 = Cpow(x1,x2,6);
                funct = (float)(-1 / (1 + Math.Sqrt((c6[0] - 1) * (c6[0] - 1) + c6[1] * c6[1])));
            }
            else if (f == 3)        // Шафер
                funct = (float)(-(0.5-(Math.Pow(Math.Sin(Math.Sqrt(x1*x1+x2*x2)),2)-0.5)/(1+0.001*(x1*x1+x2*x2))));
            else if (f == 4)        // Растригин
                funct = (float)(-(-20 + (-x1 * x1 + 10 * Math.Cos(2 * Math.PI * x1)) + (-x2 * x2 + 10 * Math.Cos(2*Math.PI * x2))));
            else if (f == 5)        // Эклея
                funct = (float)(-(-Math.E + 20 * Math.Exp(-0.2 * Math.Sqrt((x1 * x1 + x2 * x2) / 2)) + Math.Exp((Math.Cos(2 * Math.PI * x1) + Math.Cos(2 * Math.PI * x2)) / 2)));
            else if (f == 6)        // skin
                funct = (float)(-(Math.Pow(Math.Cos(2 * x1 * x1) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x1) - 1.2, 2) - Math.Pow(Math.Cos(2 * x2 * x2) - 1.1, 2) + Math.Pow(Math.Sin(0.5 * x2) - 1.2, 2)));
            else if (f == 7)        // Trapfall
                funct = (float)(-(-Math.Sqrt(Math.Abs(Math.Sin(Math.Sin(Math.Sqrt(Math.Abs(Math.Sin(x1-1)))+Math.Sqrt(Math.Abs(Math.Sin(x2+2)))))))+1));
            else if (f == 8)        // Розенброк
                funct = (float)(-(-(1 - x1) * (1 - x1) - 100 * (x2 - x1 * x1) * (x2 - x1 * x1)));
            else if (f == 9)        // Параболическая
                funct = (float)(x1 * x1 + x2 * x2);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxSelectParams_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ((comboBox1.SelectedIndex != -1))
            {
                buttonAnswer.Enabled = true;
                button1.Enabled = true;
            }
        }

        /// <summary>Вызов pdf файла с алгоритмом</summary>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Process.Start("HelpPerchMethod.pdf");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            obl = new double[2, 2];

            obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
            obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
            obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
            obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);

            NStep           = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
            MaxIteration    = Convert.ToInt32(dataGridView2.Rows[1].Cells[1].Value) + 1;

            NumFlocks       = Convert.ToInt32(dataGridView2.Rows[2].Cells[1].Value);
            NumPerchInFlock = Convert.ToInt32(dataGridView2.Rows[3].Cells[1].Value);
            population      = NumFlocks * NumPerchInFlock;
            
            lambda          = Convert.ToDouble(dataGridView4.Rows[0].Cells[1].Value);
            alfa            = Convert.ToDouble(dataGridView4.Rows[1].Cells[1].Value);

            PRmax           = Convert.ToInt32(dataGridView2.Rows[4].Cells[1].Value);
            deltapr         = Convert.ToInt32(dataGridView2.Rows[5].Cells[1].Value);
           
            FormStepPerch formPerch = new FormStepPerch(comboBox1.SelectedIndex, obl, MaxIteration, NumFlocks, NumPerchInFlock, NStep, lambda, alfa, PRmax, deltapr, exact)
            {
                flines = flines,
                showobl = showobl,
                Ar = Ar
            };
            formPerch.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                if (dataGridView1.Rows[0].Cells[1].Value != null &&
                    dataGridView1.Rows[0].Cells[2].Value != null &&
                    dataGridView1.Rows[1].Cells[1].Value != null &&
                    dataGridView1.Rows[1].Cells[2].Value != null)
                {
                    if (comboBox1.SelectedIndex != -1)
                    {
                        obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                        obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                        obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                        obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);


                        functionCallsAverage = 0;
                        for (int p = 0; p < 1; p++)
                        {


                            List<double> averFuncDeviation = new List<double>();
                            double minDeviation = 0;
                            int successCount = 0;
                            double eps = Math.Max(Math.Abs(obl[0, 0] - obl[0, 1]), Math.Abs(obl[1, 0] - obl[1, 1])) / 1000f;
                            double averDer = 0;
                            double normalDerivation = 0;
                            int z = comboBox1.SelectedIndex;

                            NStep           = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value);
                            MaxIteration    = Convert.ToInt32(dataGridView2.Rows[1].Cells[1].Value) + 1;

                            NumFlocks       = Convert.ToInt32(dataGridView2.Rows[2].Cells[1].Value);
                            NumPerchInFlock = Convert.ToInt32(dataGridView2.Rows[3].Cells[1].Value);
                            population      = NumFlocks * NumPerchInFlock;

                            PRmax           = Convert.ToInt32(dataGridView2.Rows[4].Cells[1].Value);
                            deltapr         = Convert.ToInt32(dataGridView2.Rows[5].Cells[1].Value);

                            lambda          = Convert.ToDouble(dataGridView4.Rows[0].Cells[1].Value);
                            alfa            = Convert.ToDouble(dataGridView4.Rows[1].Cells[1].Value);

                            for (int i = 0; i < 1000; i++)
                            {
                                algPerch = new AlgorithmPerch();

                                Perch result = algPerch.StartAlg(MaxIteration, obl, z, NumFlocks, NumPerchInFlock, NStep, lambda, alfa, PRmax, deltapr);
                                functionCallsAverage += algPerch.functionCalls;

                                foreach (Vector item in exactPoints)
                                {
                                    if ((Math.Abs(result.coords[0] - item[0]) < eps) && (Math.Abs(result.coords[1] - item[1]) < eps))
                                    {
                                        successCount++;
                                        break;
                                    }
                                }

                                averFuncDeviation.Add(Math.Abs(result.fitness - exact));
                            }
                            Console.WriteLine(functionCallsAverage / 1000);

                            double deltaSum = 0;
                            for (int i = 0; i < 1000; i++)
                                deltaSum += averFuncDeviation[i];

                            // СК отлонение?
                            averDer = deltaSum / 1000f;

                            averFuncDeviation.Sort();/////////////////////////////////////////////////
                            minDeviation = averFuncDeviation[0];

                            double dispersion = 0;
                            for (int i = 0; i < 1000; i++)
                                dispersion += Math.Pow(averFuncDeviation[i] - averDer, 2);
                            normalDerivation = Math.Sqrt((dispersion / 1000f));

                            FileStream fs = new FileStream("protocol.txt", FileMode.Append, FileAccess.Write);
                            StreamWriter r = new StreamWriter(fs);
                            r.Write(String.Format(@"|{0, 5}    |  {1, 6}     |{2, 5}   |    {3, 5}        |{4, 5}     |{5, 5}  |{6, 5}  |{7, 4}   |  {8,5:f3} | {9,5:f3} |    {10, 14:f6}            |{11, 17:f6}       |{12, 15:f6}        |{13, 7}     |  
|---------+-------------+--------+-----------------+----------+-------+-------+-------+--------+-------+------------------------------+------------------------+-----------------------+------------|", z + 1, population, NumFlocks, NumPerchInFlock, MaxIteration-1, NStep, PRmax, deltapr, lambda, alfa, averDer, minDeviation, normalDerivation, successCount));
                            r.Write("\r\n");

                            r.Close();

                            fs.Close();
                        }
                        Process.Start("protocol.txt");
                    }
                }
                else
                    MessageBox.Show("Введите корректные параметры", "Ошибка при запуске алгоритма", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}