using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public partial class Dialog : Form
    {
        Size _canvasSize;
        PowerPointPresentationModel _model;
        string _shapeName;
        int _top;
        int _left;
        int _right;
        int _buttom;
        public Dialog()
        {
            InitializeComponent();
        }
        public void SetupDialog(Size canvasSize, string shapeName, PowerPointPresentationModel model)
        {
            _canvasSize = canvasSize;
            _model = model;
            _shapeName = shapeName;
            buttonOK.Enabled = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _model.ProcessInsertShape(_shapeName, new Point(_left, _top), new Point(_right, _buttom));
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _left = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                _left = 0;
            }
            notifyTextBoxChanged();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _top = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception)
            {
                _top = 0;
            }
            notifyTextBoxChanged();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _right = Convert.ToInt32(textBox3.Text);
            }
            catch (Exception)
            {
                _right = 0;
            }
            notifyTextBoxChanged();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _buttom = Convert.ToInt32(textBox4.Text);
            }
            catch (Exception)
            {
                _buttom = 0;
            }
            notifyTextBoxChanged();
        }

        private void notifyTextBoxChanged()
        {
            if (_left >= 0 && _left <= _canvasSize.Width &&
                _top >= 0 && _top <= _canvasSize.Height &&
                _right >= 0 && _right <= _canvasSize.Width &&
                _buttom >= 0 && _buttom <= _canvasSize.Height &&
                _left < _right && _top < _buttom)
            {
                buttonOK.Enabled = true;
            }
            else
            {
                buttonOK.Enabled = false;
            }
        }
    }
}
