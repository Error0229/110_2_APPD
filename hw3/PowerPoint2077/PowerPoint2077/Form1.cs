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
            _presentationModel.SetCanvasCoordinate(new Point(Constant.ZERO_INTEGER, Constant.ZERO_INTEGER), new Point(_canvas.Width, _canvas.Height));
            _canvas.Paint += HandleCanvasPaint;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoving;
            _canvas.MouseEnter += HandleCanvasEnter;
            _canvas.MouseLeave += HandleCanvasLeave;

            _presentationModel._modelChanged += HandleModelChanged;
        }

        // handle model change
        private void HandleModelChanged(object sender, EventArgs e)
        {
            // Update the view
            _bindingSource.ResetBindings(false);
            _canvas.Invalidate();
            _lineAddButton.Checked = _presentationModel.IsLineChecked();
            _rectangleAddButton.Checked = _presentationModel.IsRectangleChecked();
            _ellipseAddButton.Checked = _presentationModel.IsCircleChecked();
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
            _presentationModel.ProcessCanvasPressed(e.Location);
        }

        // handle mouse moving on canva
        public void HandleCanvasMoving(object sender, MouseEventArgs e)
        {
            _presentationModel.ProcessCanvasMoving(e.Location);
        }

        // handle mouse release on canva
        public void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            _presentationModel.ProcessCanvasReleased();
        }

        // handle mouse enter canva
        public void HandleCanvasEnter(object sender, EventArgs e)
        {
            Cursor = _presentationModel.ProcessMouseEnterCanvas();
        }

        // handle mouse leave canva
        public void HandleCanvasLeave(object sender, EventArgs e)
        {
            Cursor = _presentationModel.ProcessMouseLeaveCanvas();
        }

        // click add line
        private void ClickAddLineButton(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessLineClicked();
        }

        // click add rect
        private void ClickAddRectangleButton(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessRectangleClicked();
        }

        // click arr ellipse
        private void ClickAddEllipseButton(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessEllipseClicked();
        }

    }
}
