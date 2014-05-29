namespace GenAlgorithmKursach
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.uiTotalCountTextBox = new System.Windows.Forms.TextBox();
            this.uiExecuteButton = new System.Windows.Forms.Button();
            this.personCountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tMaxTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.krosChanseTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.oiChanseTextBox = new System.Windows.Forms.TextBox();
            this.omChanseTextBox = new System.Windows.Forms.TextBox();
            this.uiOutTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(570, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "сколько раз выполнить?";
            // 
            // uiTotalCountTextBox
            // 
            this.uiTotalCountTextBox.Location = new System.Drawing.Point(573, 26);
            this.uiTotalCountTextBox.Name = "uiTotalCountTextBox";
            this.uiTotalCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.uiTotalCountTextBox.TabIndex = 19;
            this.uiTotalCountTextBox.Text = "5";
            // 
            // uiExecuteButton
            // 
            this.uiExecuteButton.Location = new System.Drawing.Point(773, 26);
            this.uiExecuteButton.Name = "uiExecuteButton";
            this.uiExecuteButton.Size = new System.Drawing.Size(158, 67);
            this.uiExecuteButton.TabIndex = 12;
            this.uiExecuteButton.Text = "execute";
            this.uiExecuteButton.UseVisualStyleBackColor = true;
            this.uiExecuteButton.Click += new System.EventHandler(this.uiExecuteButton_Click);
            // 
            // personCountTextBox
            // 
            this.personCountTextBox.Location = new System.Drawing.Point(132, 34);
            this.personCountTextBox.Name = "personCountTextBox";
            this.personCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.personCountTextBox.TabIndex = 0;
            this.personCountTextBox.Text = "8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "количество в популяции";
            // 
            // tMaxTextBox
            // 
            this.tMaxTextBox.Location = new System.Drawing.Point(132, 73);
            this.tMaxTextBox.Name = "tMaxTextBox";
            this.tMaxTextBox.Size = new System.Drawing.Size(100, 20);
            this.tMaxTextBox.TabIndex = 6;
            this.tMaxTextBox.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "максимальное количество итераций";
            // 
            // krosChanseTextBox
            // 
            this.krosChanseTextBox.Location = new System.Drawing.Point(278, 29);
            this.krosChanseTextBox.Name = "krosChanseTextBox";
            this.krosChanseTextBox.Size = new System.Drawing.Size(100, 20);
            this.krosChanseTextBox.TabIndex = 8;
            this.krosChanseTextBox.Text = "70";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "шанс мутации";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "шанс кроссинговера";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.uiTotalCountTextBox);
            this.panel1.Controls.Add(this.uiExecuteButton);
            this.panel1.Controls.Add(this.personCountTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.oiChanseTextBox);
            this.panel1.Controls.Add(this.tMaxTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.krosChanseTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.omChanseTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 119);
            this.panel1.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(401, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "шанс мутации инверсией";
            // 
            // oiChanseTextBox
            // 
            this.oiChanseTextBox.Location = new System.Drawing.Point(401, 72);
            this.oiChanseTextBox.Name = "oiChanseTextBox";
            this.oiChanseTextBox.Size = new System.Drawing.Size(100, 20);
            this.oiChanseTextBox.TabIndex = 17;
            this.oiChanseTextBox.Text = "5";
            // 
            // omChanseTextBox
            // 
            this.omChanseTextBox.Location = new System.Drawing.Point(401, 33);
            this.omChanseTextBox.Name = "omChanseTextBox";
            this.omChanseTextBox.Size = new System.Drawing.Size(100, 20);
            this.omChanseTextBox.TabIndex = 10;
            this.omChanseTextBox.Text = "5";
            // 
            // uiOutTextBox
            // 
            this.uiOutTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiOutTextBox.Location = new System.Drawing.Point(0, 119);
            this.uiOutTextBox.Multiline = true;
            this.uiOutTextBox.Name = "uiOutTextBox";
            this.uiOutTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.uiOutTextBox.Size = new System.Drawing.Size(989, 434);
            this.uiOutTextBox.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 553);
            this.Controls.Add(this.uiOutTextBox);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox uiTotalCountTextBox;
        private System.Windows.Forms.Button uiExecuteButton;
        private System.Windows.Forms.TextBox personCountTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tMaxTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox krosChanseTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox oiChanseTextBox;
        private System.Windows.Forms.TextBox omChanseTextBox;
        private System.Windows.Forms.TextBox uiOutTextBox;
    }
}

