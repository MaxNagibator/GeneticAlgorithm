using System;
using System.Windows.Forms;

namespace GenAlgorithm5lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var totalCount = Convert.ToInt32(uiTotalCountTextBox.Text);
            uiOutTextBox.Text = "";
            for (int i = 0; i < totalCount; i++)
            {
                uiOutTextBox.Text += "ЭКСПЕРЕМЕНТ НОМЕР " + i+Environment.NewLine;
                var personCount = Convert.ToInt32(personCountTextBox.Text);
                var tMax = Convert.ToInt32(tMaxTextBox.Text);
                var krosChanse = Convert.ToInt32(krosChanseTextBox.Text);
                var omChanse = Convert.ToInt32(omChanseTextBox.Text);
                var oiChanse = Convert.ToInt32(oiChanseTextBox.Text);

                if (uiGoldbergRadioButton.Checked)
                {
                    uiOutTextBox.Text += Golberg.Show(personCount, tMax, krosChanse, omChanse, oiChanse);
                }
                else if (uiHollandRadioButton.Checked)
                {
                    uiOutTextBox.Text += Holland.Show(personCount, tMax, krosChanse, omChanse, oiChanse);
                }
                else if (uiDevisRadioButton.Checked)
                {
                    uiOutTextBox.Text += Devis.Show(personCount, tMax, krosChanse, omChanse, oiChanse);
                }
            }
        }
    }
}
