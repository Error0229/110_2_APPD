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

        // constructor
        public SaveDialog(PowerPointPresentationModel model)
        {
            InitializeComponent();
            _model = model;
        }

        // handle button save click
        private void HandleButtonSaveClick(object sender, EventArgs e)
        {
            _model.ProcessSave();
            Close();
        }

        // handle button cancel click
        private void HandleButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
