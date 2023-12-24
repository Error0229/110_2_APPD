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
    public partial class SaveDialog : Form
    {
        PowerPointPresentationModel _model;
        public SaveDialog(PowerPointPresentationModel model)
        {
            InitializeComponent();
            _model = model;
        }

        // handle button save click
        private void _buttonSaveClick(object sender, EventArgs e)
        {
            _model.ProcessSave();
            Close();
        }

        private void _buttonCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
