using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public partial class InsertShapeDialog : Form
    {
        Size _canvasSize;
        PowerPointPresentationModel _model;
        string _shapeName;
        int _top;
        int _left;
        int _right;
        int _bottom;

        // constructor
        public InsertShapeDialog()
        {
            InitializeComponent();
        }

        // setup dialog 
        public void InitializeDialog(Size canvasSize, string shapeName, PowerPointPresentationModel model)
        {
            _canvasSize = canvasSize;
            _model = model;
            _shapeName = shapeName;
            _buttonOK.Enabled = false;
        }

        // handle click button ok
        private void ClickButtonInsert(object sender, EventArgs e)
        {
            _model.ProcessInsertShape(_shapeName, new Point(_left, _top), new Point(_right, _bottom));
            _textBox1.Text = "";
            _textBox2.Text = "";
            _textBox3.Text = "";
            _textBox4.Text = "";
            Close();
        }

        // handle button cancel click
        private void ClickButtonCancel(object sender, EventArgs e)
        {
            Close();
        }

        // text box 1 text changed
        private void HandleTextBox1TextChanged(object sender, EventArgs e)
        {
            try
            {
                _left = Convert.ToInt32(_textBox1.Text);
            }
            catch (Exception)
            {
                _left = 0;
            }
            NotifyTextBoxChanged();
        }

        // text box 2 text changed
        private void HandleTextBox2TextChanged(object sender, EventArgs e)
        {
            try
            {
                _top = Convert.ToInt32(_textBox2.Text);
            }
            catch (Exception)
            {
                _top = 0;
            }
            NotifyTextBoxChanged();
        }

        // text box 3 text changed

        private void HandleTextBox3TextChanged(object sender, EventArgs e)
        {
            try
            {
                _right = Convert.ToInt32(_textBox3.Text);
            }
            catch (Exception)
            {
                _right = 0;
            }
            NotifyTextBoxChanged();
        }

        // text box 4 text changed
        private void HandleTextBox4TextChanged(object sender, EventArgs e)
        {
            try
            {
                _bottom = Convert.ToInt32(_textBox4.Text);
            }
            catch (Exception)
            {
                _bottom = 0;
            }
            NotifyTextBoxChanged();
        }

        // notify text box changed
        private void NotifyTextBoxChanged()
        {
            if (_left >= 0 && _left <= _canvasSize.Width &&
                _top >= 0 && _top <= _canvasSize.Height &&
                _right >= 0 && _right <= _canvasSize.Width &&
                _bottom >= 0 && _bottom <= _canvasSize.Height &&
                _left < _right && _top < _bottom)
            {
                _buttonOK.Enabled = true;
            }
            else
            {
                _buttonOK.Enabled = false;
            }
        }
    }
}
