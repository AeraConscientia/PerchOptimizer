namespace AIS
{
    partial class FormStepPerch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStepPerch));
            this.buttonInitialPopulation = new System.Windows.Forms.Button();
            this.buttonMakeFlocks = new System.Windows.Forms.Button();
            this.buttonKettle = new System.Windows.Forms.Button();
            this.buttonFlocksSwim = new System.Windows.Forms.Button();
            this.buttonLeaderToPool = new System.Windows.Forms.Button();
            this.buttonCheckEndConditions = new System.Windows.Forms.Button();
            this.buttonSearchInPool = new System.Windows.Forms.Button();
            this.buttonChooseTheBest = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAnswer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chartGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numericUpDownNIteration = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSavePictures = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxLevelLines = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNIteration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLevelLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInitialPopulation
            // 
            this.buttonInitialPopulation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonInitialPopulation.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonInitialPopulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInitialPopulation.Location = new System.Drawing.Point(24, 30);
            this.buttonInitialPopulation.Name = "buttonInitialPopulation";
            this.buttonInitialPopulation.Size = new System.Drawing.Size(127, 61);
            this.buttonInitialPopulation.TabIndex = 1;
            this.buttonInitialPopulation.Text = "Генерация начальной популяции";
            this.buttonInitialPopulation.UseVisualStyleBackColor = false;
            this.buttonInitialPopulation.Click += new System.EventHandler(this.buttonInitialPopulation_Click);
            // 
            // buttonMakeFlocks
            // 
            this.buttonMakeFlocks.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonMakeFlocks.Enabled = false;
            this.buttonMakeFlocks.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonMakeFlocks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMakeFlocks.Location = new System.Drawing.Point(24, 118);
            this.buttonMakeFlocks.Name = "buttonMakeFlocks";
            this.buttonMakeFlocks.Size = new System.Drawing.Size(127, 61);
            this.buttonMakeFlocks.TabIndex = 1;
            this.buttonMakeFlocks.Text = "Деление популяции на стаи";
            this.buttonMakeFlocks.UseVisualStyleBackColor = false;
            this.buttonMakeFlocks.Click += new System.EventHandler(this.buttonMakeFlocks_Click);
            // 
            // buttonKettle
            // 
            this.buttonKettle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonKettle.Enabled = false;
            this.buttonKettle.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonKettle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonKettle.Location = new System.Drawing.Point(24, 207);
            this.buttonKettle.Name = "buttonKettle";
            this.buttonKettle.Size = new System.Drawing.Size(127, 61);
            this.buttonKettle.TabIndex = 1;
            this.buttonKettle.Text = "Реализация окуневого котла в стаях";
            this.buttonKettle.UseVisualStyleBackColor = false;
            this.buttonKettle.Click += new System.EventHandler(this.buttonKettle_Click);
            // 
            // buttonFlocksSwim
            // 
            this.buttonFlocksSwim.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonFlocksSwim.Enabled = false;
            this.buttonFlocksSwim.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonFlocksSwim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFlocksSwim.Location = new System.Drawing.Point(24, 296);
            this.buttonFlocksSwim.Name = "buttonFlocksSwim";
            this.buttonFlocksSwim.Size = new System.Drawing.Size(127, 61);
            this.buttonFlocksSwim.TabIndex = 1;
            this.buttonFlocksSwim.Text = "Плавание стай";
            this.buttonFlocksSwim.UseVisualStyleBackColor = false;
            this.buttonFlocksSwim.Click += new System.EventHandler(this.buttonFlocksSwim_Click);
            // 
            // buttonLeaderToPool
            // 
            this.buttonLeaderToPool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonLeaderToPool.Enabled = false;
            this.buttonLeaderToPool.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonLeaderToPool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLeaderToPool.Location = new System.Drawing.Point(24, 385);
            this.buttonLeaderToPool.Name = "buttonLeaderToPool";
            this.buttonLeaderToPool.Size = new System.Drawing.Size(127, 61);
            this.buttonLeaderToPool.TabIndex = 1;
            this.buttonLeaderToPool.Text = "Перемещение лидера стай в множество Pool";
            this.buttonLeaderToPool.UseVisualStyleBackColor = false;
            this.buttonLeaderToPool.Click += new System.EventHandler(this.buttonLeaderToPool_Click);
            // 
            // buttonCheckEndConditions
            // 
            this.buttonCheckEndConditions.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonCheckEndConditions.Enabled = false;
            this.buttonCheckEndConditions.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonCheckEndConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCheckEndConditions.Location = new System.Drawing.Point(206, 208);
            this.buttonCheckEndConditions.Name = "buttonCheckEndConditions";
            this.buttonCheckEndConditions.Size = new System.Drawing.Size(117, 61);
            this.buttonCheckEndConditions.TabIndex = 1;
            this.buttonCheckEndConditions.Text = "Проверка условий завершения поиска";
            this.buttonCheckEndConditions.UseVisualStyleBackColor = false;
            this.buttonCheckEndConditions.Click += new System.EventHandler(this.buttonCheckEndConditions_Click);
            // 
            // buttonSearchInPool
            // 
            this.buttonSearchInPool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSearchInPool.Enabled = false;
            this.buttonSearchInPool.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonSearchInPool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearchInPool.Location = new System.Drawing.Point(206, 515);
            this.buttonSearchInPool.Name = "buttonSearchInPool";
            this.buttonSearchInPool.Size = new System.Drawing.Size(117, 60);
            this.buttonSearchInPool.TabIndex = 1;
            this.buttonSearchInPool.Text = "Интенсивный поиск в множестве Pool";
            this.buttonSearchInPool.UseVisualStyleBackColor = false;
            this.buttonSearchInPool.Click += new System.EventHandler(this.buttonSearchInPool_Click);
            // 
            // buttonChooseTheBest
            // 
            this.buttonChooseTheBest.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonChooseTheBest.Enabled = false;
            this.buttonChooseTheBest.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonChooseTheBest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChooseTheBest.Location = new System.Drawing.Point(24, 515);
            this.buttonChooseTheBest.Name = "buttonChooseTheBest";
            this.buttonChooseTheBest.Size = new System.Drawing.Size(127, 60);
            this.buttonChooseTheBest.TabIndex = 1;
            this.buttonChooseTheBest.Text = "Выбор наилучшего решения";
            this.buttonChooseTheBest.UseVisualStyleBackColor = false;
            this.buttonChooseTheBest.Click += new System.EventHandler(this.buttonChooseTheBest_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView3.Location = new System.Drawing.Point(11, 30);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView3.Size = new System.Drawing.Size(331, 135);
            this.dataGridView3.TabIndex = 31;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Характеристика";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 195;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Значение";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 136;
            // 
            // buttonAnswer
            // 
            this.buttonAnswer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonAnswer.Enabled = false;
            this.buttonAnswer.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.buttonAnswer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAnswer.Location = new System.Drawing.Point(12, 591);
            this.buttonAnswer.Name = "buttonAnswer";
            this.buttonAnswer.Size = new System.Drawing.Size(89, 53);
            this.buttonAnswer.TabIndex = 34;
            this.buttonAnswer.Text = "Получить ответ";
            this.buttonAnswer.UseVisualStyleBackColor = false;
            this.buttonAnswer.Click += new System.EventHandler(this.buttonAnswer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(388, 474);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 188);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о текущей популяции";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(385, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 30);
            this.label1.TabIndex = 36;
            this.label1.Text = "График изменения средней (зеленый) и наилучшей (синий) \r\nприспособленности популя" +
    "ции:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(755, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(377, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "Графическое изображение популяции и линий уровня функции ";
            // 
            // chartGraph
            // 
            chartArea1.AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisX.Title = "Итерация";
            chartArea1.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.SharpTriangle;
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisY.Title = "f";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 81F;
            chartArea1.InnerPlotPosition.Width = 81.7F;
            chartArea1.InnerPlotPosition.X = 10F;
            chartArea1.InnerPlotPosition.Y = 7F;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 94F;
            chartArea1.Position.Width = 94F;
            chartArea1.Position.X = 5F;
            chartArea1.Position.Y = 3F;
            this.chartGraph.ChartAreas.Add(chartArea1);
            this.chartGraph.Location = new System.Drawing.Point(388, 48);
            this.chartGraph.Name = "chartGraph";
            this.chartGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.RoyalBlue;
            series1.Name = "Series0";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Green;
            series2.Name = "Series1";
            this.chartGraph.Series.Add(series1);
            this.chartGraph.Series.Add(series2);
            this.chartGraph.Size = new System.Drawing.Size(348, 410);
            this.chartGraph.TabIndex = 38;
            this.chartGraph.Text = "chartGraph";
            this.chartGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.ChartGraph_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 599);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 39;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(206, 591);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 30);
            this.button1.TabIndex = 40;
            this.button1.Text = "Выполнить N итераций";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numericUpDownNIteration
            // 
            this.numericUpDownNIteration.Location = new System.Drawing.Point(315, 624);
            this.numericUpDownNIteration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNIteration.Name = "numericUpDownNIteration";
            this.numericUpDownNIteration.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownNIteration.TabIndex = 41;
            this.numericUpDownNIteration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 627);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "N = ";
            // 
            // buttonSavePictures
            // 
            this.buttonSavePictures.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.buttonSavePictures.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OliveDrab;
            this.buttonSavePictures.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonSavePictures.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSavePictures.Image = global::AIS.Properties.Resources.Save;
            this.buttonSavePictures.Location = new System.Drawing.Point(1212, 11);
            this.buttonSavePictures.Name = "buttonSavePictures";
            this.buttonSavePictures.Size = new System.Drawing.Size(36, 37);
            this.buttonSavePictures.TabIndex = 54;
            this.buttonSavePictures.UseVisualStyleBackColor = true;
            this.buttonSavePictures.Click += new System.EventHandler(this.buttonSavePictures_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox3.Image = global::AIS.Properties.Resources.FlockColor2;
            this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
            this.pictureBox3.Location = new System.Drawing.Point(758, 563);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(490, 81);
            this.pictureBox3.TabIndex = 32;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBoxLevelLines
            // 
            this.pictureBoxLevelLines.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxLevelLines.Location = new System.Drawing.Point(758, 68);
            this.pictureBoxLevelLines.Name = "pictureBoxLevelLines";
            this.pictureBoxLevelLines.Size = new System.Drawing.Size(490, 490);
            this.pictureBoxLevelLines.TabIndex = 2;
            this.pictureBoxLevelLines.TabStop = false;
            this.pictureBoxLevelLines.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(349, 573);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // FormStepPerch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1262, 667);
            this.Controls.Add(this.buttonSavePictures);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownNIteration);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chartGraph);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonAnswer);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBoxLevelLines);
            this.Controls.Add(this.buttonChooseTheBest);
            this.Controls.Add(this.buttonSearchInPool);
            this.Controls.Add(this.buttonCheckEndConditions);
            this.Controls.Add(this.buttonLeaderToPool);
            this.Controls.Add(this.buttonFlocksSwim);
            this.Controls.Add(this.buttonKettle);
            this.Controls.Add(this.buttonMakeFlocks);
            this.Controls.Add(this.buttonInitialPopulation);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStepPerch";
            this.Text = "Метод окуней. Работа по шагам";
            this.Load += new System.EventHandler(this.FormStepPerch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNIteration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLevelLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonInitialPopulation;
        private System.Windows.Forms.Button buttonMakeFlocks;
        private System.Windows.Forms.Button buttonKettle;
        private System.Windows.Forms.Button buttonFlocksSwim;
        private System.Windows.Forms.Button buttonLeaderToPool;
        private System.Windows.Forms.Button buttonCheckEndConditions;
        private System.Windows.Forms.Button buttonSearchInPool;
        private System.Windows.Forms.Button buttonChooseTheBest;
        private System.Windows.Forms.PictureBox pictureBoxLevelLines;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button buttonAnswer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGraph;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDownNIteration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button buttonSavePictures;
    }
}