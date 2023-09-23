using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsCalculator
{
    public partial class CalculatorForm : Form
    { 
        private CalculatorModel model;
        public CalculatorForm(CalculatorModel model)
        {
            this.model = model;
            InitializeComponent();
        }
        private void CalculatorFormLoad(object sender, EventArgs e)
        {
        }

        private void button0_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(9);
        }
    }
}
