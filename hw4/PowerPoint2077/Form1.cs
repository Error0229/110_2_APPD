using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
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
            _shapeGridView.DataSource = _presentationModel.Shapes;
            _lineAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_LINE_CHECKED);
            _rectangleAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_RECTANGLE_CHECKED);
            _ellipseAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CIRCLE_CHECKED);
            _cursorButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CURSOR_CHECKED);

            _presentationModel.SetCanvasCoordinate(new Point(Constant.ZERO_INTEGER, Constant.ZERO_INTEGER), new Point(_canvas.Width, _canvas.Height));
            _canvas.Paint += HandleCanvasPaint;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoving;
            _canvas.MouseEnter += HandleCanvasEnter;
            _canvas.MouseLeave += HandleCanvasLeave;
            KeyPreview = true;
            KeyDown += HandleKeyDown;
            _brief = new Bitmap(_canvas.Width, _canvas.Height);
            _presentationModel._modelChanged += HandleModelChanged;
        }

        // handle model change
        private void HandleModelChanged(object sender, EventArgs e)
        {
            // Update the view
            _canvas.Invalidate();
            _shapeGridView.Invalidate();
            // _lineAddButton.Checked = _presentationModel.IsLineChecked();
            // _rectangleAddButton.Checked = _presentationModel.IsRectangleChecked();
            // _ellipseAddButton.Checked = _presentationModel.IsCircleChecked();
            // _cursorButton.Checked = _presentationModel.IsCursorChecked();
        }

        // regenerate thumbnail
        private void GenerateBrief()
        {
            _canvas.DrawToBitmap(_brief, new System.Drawing.Rectangle(Constant.ZERO_INTEGER, Constant.ZERO_INTEGER, _canvas.Width, _canvas.Height));
            _slide1.Image = new Bitmap(_brief, _slide1.Size);
        }

        // insert shape
        private void ButtonInsertShapeClick(object sender, EventArgs e)
        {
            _presentationModel.ProcessInsertShape(_shapeComboBox.Text);
            GenerateBrief();
        }
        private readonly PowerPointPresentationModel _presentationModel;

        // delete shape
        private void ShapeGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _presentationModel.ProcessRemoveShape(e.ColumnIndex, e.RowIndex);
            GenerateBrief();
        }

        // handle panel paint
        public void HandleCanvasPaint(object sender, PaintEventArgs e)
        {
            _presentationModel.Draw(new WindowsFormsGraphicsAdaptor(e.Graphics));
        }

        // handle canvas press
        public void HandleCanvasPressed(object sender, MouseEventArgs e)
        {
            _presentationModel.ProcessCanvasPressed(e.Location);
        }

        // handle mouse moving on canvas
        public void HandleCanvasMoving(object sender, MouseEventArgs e)
        {
            _presentationModel.ProcessCanvasMoving(e.Location);
        }

        // handle mouse release on canvas
        public void HandleCanvasReleased(object sender, MouseEventArgs e)
        {
            Cursor = _presentationModel.ProcessCanvasReleased(e.Location);
            GenerateBrief();
        }

        // handle mouse enter canvas
        public void HandleCanvasEnter(object sender, EventArgs e)
        {
            Cursor = _presentationModel.ProcessMouseEnterCanvas();
        }

        // handle mouse leave canvas
        public void HandleCanvasLeave(object sender, EventArgs e)
        {
            Cursor = _presentationModel.ProcessMouseLeaveCanvas();
            GenerateBrief();
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

        // click add ellipse
        private void ClickAddEllipseButton(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessEllipseClicked();
        }

        // click cursor
        private void ClickCursorButton(object sender, System.EventArgs e)
        {
            _presentationModel.ProcessCursorClicked();
        }

        // handle key down

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {

            _presentationModel.ProcessKeyDown(e.KeyCode);
        }
        private Bitmap _brief;
    }
}
