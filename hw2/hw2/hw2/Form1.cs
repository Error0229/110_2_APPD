using System.Windows.Forms;
using System.Collections.Generic;

namespace WindowPowerPoint
{
    public partial class PowerPoint : Form
    {

        private readonly BindingSource _bindingSource = new BindingSource();
        public PowerPoint(PowerPointModel model)
        {
            _model = model;
            InitializeComponent();
            _bindingSource.DataSource = _model.Shapes;
            ShapeGridView.DataSource = _bindingSource;
        }

        // insert shape
        private void ButtonInsertShapeClick(object sender, System.EventArgs e)
        {
            if (ShapeComboBox.Text == string.Empty)
            {
                return;
            }
            _model.InsertShape(ShapeComboBox.Text);
            _bindingSource.ResetBindings(false);
        }
        private PowerPointModel _model;

        // delete shape
        private void ShapeGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                _model.RemoveShape(e.RowIndex);
                _bindingSource.ResetBindings(false);
            }
        }
    }
}
