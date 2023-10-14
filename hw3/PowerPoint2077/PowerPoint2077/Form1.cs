using System.Windows.Forms;

namespace WindowPowerPoint
{
    public partial class PowerPoint : Form
    {

        private readonly BindingSource _bindingSource = new BindingSource();
        public PowerPoint(PowerPointPresentationModel model)
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
        private readonly PowerPointPresentationModel _model;

        // delete shape
        private void ShapeGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                _model.RemoveShape(e.RowIndex);
                _bindingSource.ResetBindings(false);
            }
        }

        private void _lineAddButtonClick(object sender, System.EventArgs e)
        {
            _model.ProcessLineClicked();
            _updateCheckBoxStatus();
        }

        private void _rectangleAddButtonClick(object sender, System.EventArgs e)
        {
            _model.ProcessRectangleClicked();
            _updateCheckBoxStatus();
        }

        private void _ellipseAddButtonClick(object sender, System.EventArgs e)
        {
            _model.ProcessEllipseClicked();
            _updateCheckBoxStatus();
        }
        private void _updateCheckBoxStatus()
        {
            _lineAddButton.Checked = _model.IsLineChecked();
            _rectangleAddButton.Checked = _model.IsRectangleChecked();
            _ellipseAddButton.Checked = _model.IsEliipseChecked();
        }
    }
}
