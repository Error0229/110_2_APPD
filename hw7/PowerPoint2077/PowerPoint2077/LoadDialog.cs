using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public partial class LoadDialog : Form
    {
        PowerPointPresentationModel _model;
        public LoadDialog(PowerPointPresentationModel model)
        {
            InitializeComponent();
            _model = model;
        }

        // cancel button click
        private void _cancelButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        // load button click
        private void _loadButtonClick(object sender, EventArgs e)
        {
            _model.ProcessLoad();
            Close();
        }
    }
}
