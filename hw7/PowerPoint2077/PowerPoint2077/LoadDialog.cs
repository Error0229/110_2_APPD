using System;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public partial class LoadDialog : Form
    {
        PowerPointPresentationModel _model;

        // constructor
        public LoadDialog(PowerPointPresentationModel model)
        {
            InitializeComponent();
            _model = model;
        }

        // cancel button click
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        // load button click
        private void HandleLoadButtonClick(object sender, EventArgs e)
        {
            _model.ProcessLoad();
            Close();
        }
    }
}
