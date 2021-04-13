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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStepPerch));
            this.buttonInitialPopulation = new System.Windows.Forms.Button();
            this.buttonMakeFlocks = new System.Windows.Forms.Button();
            this.buttonKettle = new System.Windows.Forms.Button();
            this.buttonFlocksSwim = new System.Windows.Forms.Button();
            this.buttonLeaderToPool = new System.Windows.Forms.Button();
            this.buttonCheckEndConditions = new System.Windows.Forms.Button();
            this.buttonSearchInPool = new System.Windows.Forms.Button();
            this.buttonChooseTheBest = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInitialPopulation
            // 
            this.buttonInitialPopulation.Location = new System.Drawing.Point(24, 30);
            this.buttonInitialPopulation.Name = "buttonInitialPopulation";
            this.buttonInitialPopulation.Size = new System.Drawing.Size(127, 61);
            this.buttonInitialPopulation.TabIndex = 1;
            this.buttonInitialPopulation.Text = "Генерация начальной популяции";
            this.buttonInitialPopulation.UseVisualStyleBackColor = true;
            this.buttonInitialPopulation.Click += new System.EventHandler(this.buttonInitialPopulation_Click);
            // 
            // buttonMakeFlocks
            // 
            this.buttonMakeFlocks.Location = new System.Drawing.Point(24, 118);
            this.buttonMakeFlocks.Name = "buttonMakeFlocks";
            this.buttonMakeFlocks.Size = new System.Drawing.Size(127, 61);
            this.buttonMakeFlocks.TabIndex = 1;
            this.buttonMakeFlocks.Text = "Деление популяции на стаи";
            this.buttonMakeFlocks.UseVisualStyleBackColor = true;
            this.buttonMakeFlocks.Click += new System.EventHandler(this.buttonMakeFlocks_Click);
            // 
            // buttonKettle
            // 
            this.buttonKettle.Location = new System.Drawing.Point(24, 207);
            this.buttonKettle.Name = "buttonKettle";
            this.buttonKettle.Size = new System.Drawing.Size(127, 61);
            this.buttonKettle.TabIndex = 1;
            this.buttonKettle.Text = "Реализация окуневого котла \r\nв стаях";
            this.buttonKettle.UseVisualStyleBackColor = true;
            this.buttonKettle.Click += new System.EventHandler(this.buttonKettle_Click);
            // 
            // buttonFlocksSwim
            // 
            this.buttonFlocksSwim.Location = new System.Drawing.Point(24, 296);
            this.buttonFlocksSwim.Name = "buttonFlocksSwim";
            this.buttonFlocksSwim.Size = new System.Drawing.Size(127, 61);
            this.buttonFlocksSwim.TabIndex = 1;
            this.buttonFlocksSwim.Text = "Плавание стай";
            this.buttonFlocksSwim.UseVisualStyleBackColor = true;
            this.buttonFlocksSwim.Click += new System.EventHandler(this.buttonFlocksSwim_Click);
            // 
            // buttonLeaderToPool
            // 
            this.buttonLeaderToPool.Location = new System.Drawing.Point(24, 385);
            this.buttonLeaderToPool.Name = "buttonLeaderToPool";
            this.buttonLeaderToPool.Size = new System.Drawing.Size(127, 61);
            this.buttonLeaderToPool.TabIndex = 1;
            this.buttonLeaderToPool.Text = "Помещение лидера популяции \r\nв множество Pool";
            this.buttonLeaderToPool.UseVisualStyleBackColor = true;
            this.buttonLeaderToPool.Click += new System.EventHandler(this.buttonLeaderToPool_Click);
            // 
            // buttonCheckEndConditions
            // 
            this.buttonCheckEndConditions.Location = new System.Drawing.Point(206, 208);
            this.buttonCheckEndConditions.Name = "buttonCheckEndConditions";
            this.buttonCheckEndConditions.Size = new System.Drawing.Size(117, 61);
            this.buttonCheckEndConditions.TabIndex = 1;
            this.buttonCheckEndConditions.Text = "Проверка условий завершения поиска";
            this.buttonCheckEndConditions.UseVisualStyleBackColor = true;
            this.buttonCheckEndConditions.Click += new System.EventHandler(this.buttonCheckEndConditions_Click);
            // 
            // buttonSearchInPool
            // 
            this.buttonSearchInPool.Location = new System.Drawing.Point(206, 515);
            this.buttonSearchInPool.Name = "buttonSearchInPool";
            this.buttonSearchInPool.Size = new System.Drawing.Size(117, 60);
            this.buttonSearchInPool.TabIndex = 1;
            this.buttonSearchInPool.Text = "Интенсивный поиск в Pool";
            this.buttonSearchInPool.UseVisualStyleBackColor = true;
            this.buttonSearchInPool.Click += new System.EventHandler(this.buttonSearchInPool_Click);
            // 
            // buttonChooseTheBest
            // 
            this.buttonChooseTheBest.Location = new System.Drawing.Point(24, 515);
            this.buttonChooseTheBest.Name = "buttonChooseTheBest";
            this.buttonChooseTheBest.Size = new System.Drawing.Size(127, 60);
            this.buttonChooseTheBest.TabIndex = 1;
            this.buttonChooseTheBest.Text = "Выбор наилучшего решения";
            this.buttonChooseTheBest.UseVisualStyleBackColor = true;
            this.buttonChooseTheBest.Click += new System.EventHandler(this.buttonChooseTheBest_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(744, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(490, 490);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(349, 584);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
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
            this.dataGridView3.Location = new System.Drawing.Point(390, 407);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.Size = new System.Drawing.Size(348, 189);
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
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Значения";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 195;
            // 
            // FormStepPerch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 633);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.pictureBox2);
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
            this.Text = "FormStepPerch";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}