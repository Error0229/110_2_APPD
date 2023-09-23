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
            resultBox.Text = model.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(1);
            resultBox.Text = model.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(2);
            resultBox.Text = model.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(3);
            resultBox.Text = model.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(4);
            resultBox.Text = model.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(5);
            resultBox.Text = model.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(6);
            resultBox.Text = model.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(7);
            resultBox.Text = model.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(8);
            resultBox.Text = model.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            model.ClickNumberButton(9);
            resultBox.Text = model.Text;
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            model.ClickDot();
            resultBox.Text = model.Text;
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            model.ClickOperatorButton('=');
            resultBox.Text = model.Text;
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            model.ClickOperatorButton('/');
            resultBox.Text = model.Text;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            model.ClickOperatorButton('*');
            resultBox.Text = model.Text;
        }

        private void buttonSubstract_Click(object sender, EventArgs e)
        {
            model.ClickOperatorButton('-');
            resultBox.Text = model.Text;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            model.ClickOperatorButton('+');
            resultBox.Text = model.Text;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            model.Clear();
            resultBox.Text = model.Text;
        }

        private void buttonClearError_Click(object sender, EventArgs e)
        {
            model.ClearError();
            resultBox.Text = model.Text;
        }
    }
}
