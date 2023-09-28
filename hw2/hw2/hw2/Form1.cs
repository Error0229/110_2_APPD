using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowPowerPoint
{
    public partial class PowerPoint : Form
    {

        public PowerPoint(PowerPointModel model)
        {
            _model = model;
            InitializeComponent();
        }

        // insert shape
        private void ButtonInsertShapeClick(object sender, System.EventArgs e)
        {
            _model.InsertShape(ShapeComboBox.Text);
            // add a new row to the grid view
            // the grid view have 3 columns, first one is delete button, second one is shape name, third one is shape's coordinates
            ShapeGridView.Rows.Add("Delete", ShapeComboBox.Text, "(0, 0), (1, 1)");
        }
        private PowerPointModel _model;

        private void ShapeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                _model.RemoveShape(e.RowIndex);
                ShapeGridView.Rows.RemoveAt(e.RowIndex);
            }
        }
    }
}
