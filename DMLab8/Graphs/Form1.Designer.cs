namespace Graphs
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.IsOriented = new System.Windows.Forms.CheckBox();
            this.button11 = new System.Windows.Forms.Button();
            this.joint = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._bfs = new System.Windows.Forms.RadioButton();
            this._dfs = new System.Windows.Forms.RadioButton();
            this.button10 = new System.Windows.Forms.Button();
            this.coloring = new System.Windows.Forms.RadioButton();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.cylesButton = new System.Windows.Forms.RadioButton();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.distances = new System.Windows.Forms.RadioButton();
            this.connectivity = new System.Windows.Forms.RadioButton();
            this.redact = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button13 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button15 = new System.Windows.Forms.Button();
            this.IsLetters = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(530, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Опции";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "graph.txt";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(139, 48);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "граф из файла";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(230, 26);
            this.button3.TabIndex = 1;
            this.button3.Text = "Очистить граф";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(230, 26);
            this.button2.TabIndex = 0;
            this.button2.Text = "Показать матрицу смежности";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "10";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button15);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.button11);
            this.groupBox2.Controls.Add(this.joint);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.button10);
            this.groupBox2.Controls.Add(this.coloring);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.cylesButton);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.distances);
            this.groupBox2.Controls.Add(this.connectivity);
            this.groupBox2.Controls.Add(this.redact);
            this.groupBox2.Location = new System.Drawing.Point(530, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 313);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Режим карты";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.IsLetters);
            this.groupBox5.Controls.Add(this.IsOriented);
            this.groupBox5.Location = new System.Drawing.Point(88, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(77, 70);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "тип графа";
            // 
            // IsOriented
            // 
            this.IsOriented.AutoSize = true;
            this.IsOriented.Location = new System.Drawing.Point(6, 20);
            this.IsOriented.Name = "IsOriented";
            this.IsOriented.Size = new System.Drawing.Size(78, 17);
            this.IsOriented.TabIndex = 0;
            this.IsOriented.Text = "Ориентир.";
            this.IsOriented.UseVisualStyleBackColor = true;
            this.IsOriented.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(171, 208);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(62, 23);
            this.button11.TabIndex = 15;
            this.button11.Text = "таблица";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // joint
            // 
            this.joint.AutoSize = true;
            this.joint.Location = new System.Drawing.Point(6, 211);
            this.joint.Name = "joint";
            this.joint.Size = new System.Drawing.Size(72, 17);
            this.joint.TabIndex = 14;
            this.joint.Text = "Шарниры";
            this.joint.UseVisualStyleBackColor = true;
            this.joint.CheckedChanged += new System.EventHandler(this.joint_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this._bfs);
            this.groupBox3.Controls.Add(this._dfs);
            this.groupBox3.Location = new System.Drawing.Point(165, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(77, 70);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "тип обхода";
            // 
            // _bfs
            // 
            this._bfs.AutoSize = true;
            this._bfs.Location = new System.Drawing.Point(6, 19);
            this._bfs.Name = "_bfs";
            this._bfs.Size = new System.Drawing.Size(45, 17);
            this._bfs.TabIndex = 11;
            this._bfs.Text = "BFS";
            this._bfs.UseVisualStyleBackColor = true;
            this._bfs.CheckedChanged += new System.EventHandler(this._bfs_CheckedChanged);
            // 
            // _dfs
            // 
            this._dfs.AutoSize = true;
            this._dfs.Checked = true;
            this._dfs.Location = new System.Drawing.Point(6, 42);
            this._dfs.Name = "_dfs";
            this._dfs.Size = new System.Drawing.Size(46, 17);
            this._dfs.TabIndex = 12;
            this._dfs.TabStop = true;
            this._dfs.Text = "DFS";
            this._dfs.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Enabled = false;
            this.button10.Location = new System.Drawing.Point(6, 159);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(108, 23);
            this.button10.TabIndex = 10;
            this.button10.Text = "найти цикл (BFS)";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // coloring
            // 
            this.coloring.AutoSize = true;
            this.coloring.Location = new System.Drawing.Point(6, 188);
            this.coloring.Name = "coloring";
            this.coloring.Size = new System.Drawing.Size(89, 17);
            this.coloring.TabIndex = 9;
            this.coloring.Text = "2-Раскраска";
            this.coloring.UseVisualStyleBackColor = true;
            this.coloring.CheckedChanged += new System.EventHandler(this.coloring_CheckedChanged);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(98, 133);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(25, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "<";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(125, 133);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(25, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = ">";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(174, 133);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(62, 23);
            this.button7.TabIndex = 6;
            this.button7.Text = "таблица";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // cylesButton
            // 
            this.cylesButton.AutoSize = true;
            this.cylesButton.Location = new System.Drawing.Point(6, 136);
            this.cylesButton.Name = "cylesButton";
            this.cylesButton.Size = new System.Drawing.Size(87, 17);
            this.cylesButton.TabIndex = 5;
            this.cylesButton.Text = "циклы (DFS)";
            this.cylesButton.UseVisualStyleBackColor = true;
            this.cylesButton.CheckedChanged += new System.EventHandler(this.cylesButton_CheckedChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(174, 75);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(62, 23);
            this.button6.TabIndex = 4;
            this.button6.Text = "таблица";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(174, 104);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(62, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "таблица";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // distances
            // 
            this.distances.AutoSize = true;
            this.distances.Location = new System.Drawing.Point(6, 107);
            this.distances.Name = "distances";
            this.distances.Size = new System.Drawing.Size(84, 17);
            this.distances.TabIndex = 2;
            this.distances.Text = "расстояния";
            this.distances.UseVisualStyleBackColor = true;
            this.distances.CheckedChanged += new System.EventHandler(this.distances_CheckedChanged);
            // 
            // connectivity
            // 
            this.connectivity.AutoSize = true;
            this.connectivity.Location = new System.Drawing.Point(6, 75);
            this.connectivity.Name = "connectivity";
            this.connectivity.Size = new System.Drawing.Size(144, 17);
            this.connectivity.TabIndex = 1;
            this.connectivity.Text = "компоненты связности";
            this.connectivity.UseVisualStyleBackColor = true;
            this.connectivity.CheckedChanged += new System.EventHandler(this.connectivity_CheckedChanged);
            // 
            // redact
            // 
            this.redact.AutoSize = true;
            this.redact.Checked = true;
            this.redact.Location = new System.Drawing.Point(6, 29);
            this.redact.Name = "redact";
            this.redact.Size = new System.Drawing.Size(50, 17);
            this.redact.TabIndex = 0;
            this.redact.TabStop = true;
            this.redact.Text = "граф";
            this.redact.UseVisualStyleBackColor = true;
            this.redact.CheckedChanged += new System.EventHandler(this.redact_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button12);
            this.groupBox4.Controls.Add(this.button14);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.button13);
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Location = new System.Drawing.Point(530, 128);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(242, 102);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Загрузка / сохранение";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(95, 73);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(138, 23);
            this.button12.TabIndex = 6;
            this.button12.Text = "открыть файл графов";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(150, 44);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(83, 23);
            this.button14.TabIndex = 5;
            this.button14.Text = "сохранить";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(138, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "graph_name";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(151, 19);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(82, 23);
            this.button13.TabIndex = 2;
            this.button13.Text = "удалить";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(138, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button15
            // 
            this.button15.Enabled = false;
            this.button15.Location = new System.Drawing.Point(6, 237);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(230, 23);
            this.button15.TabIndex = 16;
            this.button15.Text = "топологическая сортировка орграфа";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // IsLetters
            // 
            this.IsLetters.AutoSize = true;
            this.IsLetters.Location = new System.Drawing.Point(6, 43);
            this.IsLetters.Name = "IsLetters";
            this.IsLetters.Size = new System.Drawing.Size(58, 17);
            this.IsLetters.TabIndex = 1;
            this.IsLetters.Text = "Буквы";
            this.IsLetters.UseVisualStyleBackColor = true;
            this.IsLetters.CheckedChanged += new System.EventHandler(this.IsLetters_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Графы";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton distances;
        private System.Windows.Forms.RadioButton connectivity;
        private System.Windows.Forms.RadioButton redact;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.RadioButton cylesButton;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.RadioButton coloring;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.RadioButton _dfs;
        private System.Windows.Forms.RadioButton _bfs;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton joint;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox IsOriented;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.CheckBox IsLetters;
    }
}

