using System.Windows.Forms;

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
            _shapeGridView.DataSource = _bindingSource;
        }

        // insert shape
        private void ButtonInsertShapeClick(object sender, System.EventArgs e)
        {
            if (_shapeComboBox.Text == string.Empty)
            {
                return;
            }
            _model.InsertShape(_shapeComboBox.Text);
            _bindingSource.ResetBindings(false);
        }
        private readonly PowerPointModel _model;

        // delete shape
        private void ShapeGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                _model.RemoveShape(e.RowIndex);
                _bindingSource.ResetBindings(false);
            }
        }
    }
}
