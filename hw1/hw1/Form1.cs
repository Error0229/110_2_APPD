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
        private CalculatorModel _model;
        public CalculatorForm(CalculatorModel model)
        {
            this._model = model;
            InitializeComponent();
            _resultBox.Text = model.Text;
        }

        // Load form
        private void LoadCalculatorForm(object sender, EventArgs e)
        {
        }

        // click 0
        private void ButtonZeroClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.ZERO);
            _resultBox.Text = _model.Text;
        }

        // click 1
        private void ButtonOneClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.ONE);
            _resultBox.Text = _model.Text;
        }

        // click 2
        private void ButtonTwoClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.TWO);
            _resultBox.Text = _model.Text;
        }

        // click 3
        private void ButtonThreeClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.THREE);
            _resultBox.Text = _model.Text;
        }

        // click 4
        private void ButtonFourClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.FOUR);
            _resultBox.Text = _model.Text;
        }

        // click 5
        private void ButtonFiveClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.FIVE);
            _resultBox.Text = _model.Text;
        }

        // click 6
        private void ButtonSixClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.SIX);
            _resultBox.Text = _model.Text;
        }

        // click 7
        private void ButtonSevenClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.SEVEN);
            _resultBox.Text = _model.Text;
        }

        // click 8
        private void ButtonEightClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.EIGHT);
            _resultBox.Text = _model.Text;
        }

        // click 9
        private void ButtonNineClick(object sender, EventArgs e)
        {
            _model.ProcessNumber(Constants.NINE);
            _resultBox.Text = _model.Text;
        }

        // click .
        private void ButtonDotClick(object sender, EventArgs e)
        {
            _model.ClickDot();
            _resultBox.Text = _model.Text;
        }

        // click =
        private void ButtonEqualClick(object sender, EventArgs e)
        {
            _model.ClickOperatorButton(Constants.EQUAL);
            _resultBox.Text = _model.Text;
        }

        // click /
        private void ButtonDivideClick(object sender, EventArgs e)
        {
            _model.ClickOperatorButton(Constants.DIVISION);
            _resultBox.Text = _model.Text;
        }

        // click *
        private void ButtonMultiplyClick(object sender, EventArgs e)
        {
            _model.ClickOperatorButton(Constants.MULTIPLY);
            _resultBox.Text = _model.Text;
        }

        // click -
        private void ButtonSubstractClick(object sender, EventArgs e)
        {
            _model.ClickOperatorButton(Constants.SUBSTRACT);
            _resultBox.Text = _model.Text;
        }

        // click +
        private void ButtonPlusClick(object sender, EventArgs e)
        {
            _model.ClickOperatorButton(Constants.ADDITION);
            _resultBox.Text = _model.Text;
        }

        // click C
        private void ButtonClearClick(object sender, EventArgs e)
        {
            _model.Clear();
            _resultBox.Text = _model.Text;
        }

        // click CE
        private void ButtonClearErrorClick(object sender, EventArgs e)
        {
            _model.ClearError();
            _resultBox.Text = _model.Text;
        }
    }
}
