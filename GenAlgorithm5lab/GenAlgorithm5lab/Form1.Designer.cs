namespace GenAlgorithm5lab
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
            this.personCountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tMaxTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.krosChanseTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.omChanseTextBox = new System.Windows.Forms.TextBox();
            this.uiExecuteButton = new System.Windows.Forms.Button();
            this.uiGoldbergRadioButton = new System.Windows.Forms.RadioButton();
            this.uiHollandRadioButton = new System.Windows.Forms.RadioButton();
            this.uiDevisRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.oiChanseTextBox = new System.Windows.Forms.TextBox();
            this.uiOutTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // personCountTextBox
            // 
            this.personCountTextBox.Location = new System.Drawing.Point(-1, 30);
            this.personCountTextBox.Name = "personCountTextBox";
            this.personCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.personCountTextBox.TabIndex = 0;
            this.personCountTextBox.Text = "8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "количество в популяции";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "максимальное количество итераций";
            // 
            // tMaxTextBox
            // 
            this.tMaxTextBox.Location = new System.Drawing.Point(-1, 69);
            this.tMaxTextBox.Name = "tMaxTextBox";
            this.tMaxTextBox.Size = new System.Drawing.Size(100, 20);
            this.tMaxTextBox.TabIndex = 6;
            this.tMaxTextBox.Text = "20";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "шанс кроссинговера";
            // 
            // krosChanseTextBox
            // 
            this.krosChanseTextBox.Location = new System.Drawing.Point(203, 68);
            this.krosChanseTextBox.Name = "krosChanseTextBox";
            this.krosChanseTextBox.Size = new System.Drawing.Size(100, 20);
            this.krosChanseTextBox.TabIndex = 8;
            this.krosChanseTextBox.Text = "70";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(334, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "шанс мутации";
            // 
            // omChanseTextBox
            // 
            this.omChanseTextBox.Location = new System.Drawing.Point(334, 30);
            this.omChanseTextBox.Name = "omChanseTextBox";
            this.omChanseTextBox.Size = new System.Drawing.Size(100, 20);
            this.omChanseTextBox.TabIndex = 10;
            this.omChanseTextBox.Text = "5";
            // 
            // uiExecuteButton
            // 
            this.uiExecuteButton.Location = new System.Drawing.Point(487, 22);
            this.uiExecuteButton.Name = "uiExecuteButton";
            this.uiExecuteButton.Size = new System.Drawing.Size(311, 67);
            this.uiExecuteButton.TabIndex = 12;
            this.uiExecuteButton.Text = "execute";
            this.uiExecuteButton.UseVisualStyleBackColor = true;
            this.uiExecuteButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // uiGoldbergRadioButton
            // 
            this.uiGoldbergRadioButton.AutoSize = true;
            this.uiGoldbergRadioButton.Checked = true;
            this.uiGoldbergRadioButton.Location = new System.Drawing.Point(6, 16);
            this.uiGoldbergRadioButton.Name = "uiGoldbergRadioButton";
            this.uiGoldbergRadioButton.Size = new System.Drawing.Size(72, 17);
            this.uiGoldbergRadioButton.TabIndex = 13;
            this.uiGoldbergRadioButton.TabStop = true;
            this.uiGoldbergRadioButton.Text = "Голдберг";
            this.uiGoldbergRadioButton.UseVisualStyleBackColor = true;
            // 
            // uiHollandRadioButton
            // 
            this.uiHollandRadioButton.AutoSize = true;
            this.uiHollandRadioButton.Location = new System.Drawing.Point(6, 37);
            this.uiHollandRadioButton.Name = "uiHollandRadioButton";
            this.uiHollandRadioButton.Size = new System.Drawing.Size(68, 17);
            this.uiHollandRadioButton.TabIndex = 14;
            this.uiHollandRadioButton.TabStop = true;
            this.uiHollandRadioButton.Text = "Холланд";
            this.uiHollandRadioButton.UseVisualStyleBackColor = true;
            // 
            // uiDevisRadioButton
            // 
            this.uiDevisRadioButton.AutoSize = true;
            this.uiDevisRadioButton.Location = new System.Drawing.Point(6, 60);
            this.uiDevisRadioButton.Name = "uiDevisRadioButton";
            this.uiDevisRadioButton.Size = new System.Drawing.Size(58, 17);
            this.uiDevisRadioButton.TabIndex = 15;
            this.uiDevisRadioButton.TabStop = true;
            this.uiDevisRadioButton.Text = "Девис";
            this.uiDevisRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.uiGoldbergRadioButton);
            this.groupBox1.Controls.Add(this.uiDevisRadioButton);
            this.groupBox1.Controls.Add(this.uiHollandRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(-111, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(104, 91);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "тип";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(334, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "шанс мутации инверсией";
            // 
            // oiChanseTextBox
            // 
            this.oiChanseTextBox.Location = new System.Drawing.Point(334, 69);
            this.oiChanseTextBox.Name = "oiChanseTextBox";
            this.oiChanseTextBox.Size = new System.Drawing.Size(100, 20);
            this.oiChanseTextBox.TabIndex = 17;
            this.oiChanseTextBox.Text = "5";
            // 
            // uiOutTextBox
            // 
            this.uiOutTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiOutTextBox.Location = new System.Drawing.Point(0, 119);
            this.uiOutTextBox.Multiline = true;
            this.uiOutTextBox.Name = "uiOutTextBox";
            this.uiOutTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.uiOutTextBox.Size = new System.Drawing.Size(1011, 340);
            this.uiOutTextBox.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiExecuteButton);
            this.panel1.Controls.Add(this.personCountTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.oiChanseTextBox);
            this.panel1.Controls.Add(this.tMaxTextBox);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.krosChanseTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.omChanseTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 119);
            this.panel1.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 459);
            this.Controls.Add(this.uiOutTextBox);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox personCountTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tMaxTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox krosChanseTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox omChanseTextBox;
        private System.Windows.Forms.Button uiExecuteButton;
        private System.Windows.Forms.RadioButton uiGoldbergRadioButton;
        private System.Windows.Forms.RadioButton uiHollandRadioButton;
        private System.Windows.Forms.RadioButton uiDevisRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox oiChanseTextBox;
        private System.Windows.Forms.TextBox uiOutTextBox;
        private System.Windows.Forms.Panel panel1;
    }
}

