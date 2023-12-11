using System;
using System.Drawing;
using System.Windows.Forms;
namespace WindowPowerPoint
{
    public partial class PowerPoint : Form
    {

        private CursorManager _cursorManager;
        public PowerPoint(PowerPointPresentationModel model)
        {
            base.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.Selectable, true);
            UpdateStyles();
            _presentationModel = model;
            InitializeComponent();
            _shapeGridView.DataSource = _presentationModel.Shapes;
            _lineAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_LINE_CHECKED);
            _rectangleAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_RECTANGLE_CHECKED);
            _ellipseAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CIRCLE_CHECKED);
            _cursorButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CURSOR_CHECKED);
            _redoButton.DataBindings.Add(Constant.ENABLED, _presentationModel, Constant.IS_REDO_ENABLED);
            _undoButton.DataBindings.Add(Constant.ENABLED, _presentationModel, Constant.IS_UNDO_ENABLED);

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
            _cursorManager = new CursorManager();
            _presentationModel.SetCursorManager(_cursorManager);
            _presentationModel._cursorChanged += HandleCursorChanged;
            _presentationModel.ProcessCursorClicked();
            SizeChanged += PowerPointSizeChanged;
            PowerPointSizeChanged(this, null); // lazy resize
        }

        // handle window size changed
        private void PowerPointSizeChanged(object sender, EventArgs e)
        {
            ResizeCanvas(sender, e);
        }

        // handle model change
        private void HandleModelChanged(object sender, EventArgs e)
        {
            // Update the view
            _canvas.Invalidate();
            _shapeGridView.Invalidate();
        }

        // handle cursor changed
        private void HandleCursorChanged(object sender, EventArgs e)
        {
            Cursor = _cursorManager.CurrentCursor;
        }

        // regenerate thumbnail
        private void GenerateBrief()
        {
            if (_canvas.Width == Constant.ZERO_INTEGER || _canvas.Height == Constant.ZERO_INTEGER)
                return;
            _brief = new Bitmap(_canvas.Width, _canvas.Height);
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
            _presentationModel.ProcessCanvasReleased(e.Location);
            GenerateBrief();
        }

        // handle mouse enter canvas
        public void HandleCanvasEnter(object sender, EventArgs e)
        {
            _presentationModel.ProcessMouseEnterCanvas();
        }

        // handle mouse leave canvas
        public void HandleCanvasLeave(object sender, EventArgs e)
        {
            _presentationModel.ProcessMouseLeaveCanvas();
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
            GenerateBrief();
        }
        private Bitmap _brief;

        // handle splitter adjust
        private void SplitContainer1Adjust(object sender, SplitterEventArgs e)
        {
            // make sure the thumbnail and the slide button is updated keep the aspect ratio 16:9
            double panel1Width = _splitContainer1.Panel1.Width;
            _slide1.Width = (int)panel1Width - _splitContainer1.Panel1.Margin.Horizontal;
            _slide1.Height = (int)(_slide1.Width / Constant.SLIDE_RATIO);
            _slide1.Left = (int)((panel1Width - _slide1.Width) / Constant.PEN_THICK);
            ResizeCanvas(sender, e);
            GenerateBrief();
        }

        // handle when splitter2 move
        private void SplitContainer2Adjust(object sender, SplitterEventArgs e)
        {
            ResizeCanvas(sender, e);
        }

        // resize canvas
        private void ResizeCanvas(object sender, EventArgs e)
        {

            double panel2Width = _splitContainer2.Panel1.Width;
            double panel2Height = _splitContainer2.Panel1.Height;
            if ((panel2Width - _splitContainer2.Panel1.Margin.Horizontal) / (panel2Height - _splitContainer2.Panel1.Margin.Vertical) > Constant.SLIDE_RATIO)
            {
                _canvas.Height = (int)panel2Height - _splitContainer2.Panel1.Margin.Vertical;
                _canvas.Width = (int)(_canvas.Height * Constant.SLIDE_RATIO);
                _canvas.Location = new Point((int)((panel2Width - _canvas.Width) / Constant.PEN_THICK), (int)((panel2Height - _canvas.Height) / Constant.PEN_THICK));
            }
            else
            {
                _canvas.Width = (int)panel2Width - _splitContainer2.Panel1.Margin.Horizontal;
                _canvas.Height = (int)(_canvas.Width / Constant.SLIDE_RATIO);
                _canvas.Location = new Point((int)((panel2Width - _canvas.Width) / Constant.PEN_THICK), (int)((panel2Height - _canvas.Height) / Constant.PEN_THICK));
            }
            if (_canvas.Width != Constant.ZERO_INTEGER && _canvas.Height != Constant.ZERO_INTEGER)
                _presentationModel.SetCanvasSize(_canvas.Size);
        }

        // click redo
        private void RedoButtonClick(object sender, EventArgs e)
        {
            _presentationModel.ProcessRedo();
            ResizeCanvas(sender, e);
            GenerateBrief();
        }

        // click undo
        private void UndoButtonClick(object sender, EventArgs e)
        {
            _presentationModel.ProcessUndo();
            ResizeCanvas(sender, e);
            GenerateBrief();
        }
    }
}
