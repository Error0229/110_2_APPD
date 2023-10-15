using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System;

namespace WindowPowerPoint
{
    public partial class PowerPoint : Form
    {

        private readonly BindingSource _bindingSource = new BindingSource();
        public PowerPoint(PowerPointPresentationModel model)
        {
            // DoubleBuffered = true;
            base.DoubleBuffered = true;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            _presentationModel = model;
            InitializeComponent();
            _bindingSource.DataSource = _presentationModel.Shapes;
            _shapeGridView.DataSource = _bindingSource;
            _presentationModel.SetCanvaCoordinate(new Point(Constant.ZERO_INT, Constant.ZERO_INT), new Point(_canva.Width, _canva.Height));
            _canva.Paint += HandleCanvasPaint;
            _canva.MouseDown += HandleCanvasPressed;
            _canva.MouseUp += HandleCanvasReleased;
            _canva.MouseMove += HandleCanvasMoving;
            _canva.MouseEnter += HandleCanvasEnter;
            _canva.MouseLeave += HandleCanvasLeave;

            _presentationModel.ModelChanged += HandleModelChanged;
        }

        // handle model change
        private void HandleModelChanged(object sender, EventArgs e)
        {
            // Update the view
            _bindingSource.ResetBindings(false);
            _canva.Invalidate();
            _lineAddButton.Checked = _presentationModel.IsLineChecked();
            _rectangleAddButton.Checked = _presentationModel.IsRectangleChecked();
            _ellipseAddButton.Checked = _presentationModel.IsEliipseChecked();
        }

        // insert shape
        private void ButtonInsertShapeClick(object sender, EventArgs e)
        {
            _presentationModel.ProcessInsertShape(_shapeComboBox.Text);
        }
        private readonly PowerPointPresentationModel _presentationModel;

        // delete shape
        private void ShapeGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _presentationModel.ProcessRemoveShape(e.ColumnIndex, e.RowIndex);
        }

        // handle panel paint
        public void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            _presentationModel.Draw(e.Graphics);
        }

        // handle canva press
        public void HandleCanvasPressed(object sender, MouseEventArgs e)
        {
            _presentationModel.ProcessCanvaPressed(e.Location);
        }

        // handle mouse moving on canva
        public void HandleCanvasMoving(object sender, MouseEventArgs e)
        {
            _presentationModel.ProcessCanvaMoving(e.Location);
        }

        // handle mouse release on canva
        public void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            _presentationModel.ProcessCanvaReleased(e.Location);
        }

        // handle mouse enter canva
        public void HandleCanvasEnter(object sender, EventArgs e)
        {
            Cursor = _presentationModel.ProcessMouseEnterCanva();
        }

        // handle mouse leave canva
        public void HandleCanvasLeave(object sender, EventArgs e)
        {
            Cursor = _presentationModel.ProcessMouseLeaveCanva();
        }
        private void _lineAddButtonClick(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessLineClicked();
        }

        private void _rectangleAddButtonClick(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessRectangleClicked();
        }

        private void _ellipseAddButtonClick(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessEllipseClicked();
        }

    }
}
