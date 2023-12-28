using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace WindowPowerPoint
{
    public partial class PowerPoint : Form, ISlide
    {

        public int SlideIndex
        {
            get; set;
        }
        private CursorManager _cursorManager;
        private InsertShapeDialog _insertDialog;
        private SaveDialog _saveDialog;
        private LoadDialog _loadDialog;
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
            _lineAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_LINE_CHECKED);
            _rectangleAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_RECTANGLE_CHECKED);
            _ellipseAddButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CIRCLE_CHECKED);
            _cursorButton.DataBindings.Add(Constant.CHECKED, _presentationModel, Constant.IS_CURSOR_CHECKED);
            _redoButton.DataBindings.Add(Constant.ENABLED, _presentationModel, Constant.IS_REDO_ENABLED);
            _undoButton.DataBindings.Add(Constant.ENABLED, _presentationModel, Constant.IS_UNDO_ENABLED);
            _saveButton.DataBindings.Add(Constant.ENABLED, _presentationModel, nameof(_presentationModel.IsSaveEnabled));
            _canvas.Paint += HandleCanvasPaint;
            _canvas.MouseDown += HandleCanvasPressed;
            _canvas.MouseUp += HandleCanvasReleased;
            _canvas.MouseMove += HandleCanvasMoving;
            _canvas.MouseEnter += HandleCanvasEnter;
            _canvas.MouseLeave += HandleCanvasLeave;
            _flowLayoutPanel.Layout += _flowLayoutPanelLayout;
            KeyPreview = true;
            KeyDown += HandleKeyDown;
            _brief = new Bitmap(_canvas.Width, _canvas.Height);
            _presentationModel._modelChanged += HandleModelChanged;
            _cursorManager = new CursorManager();
            _presentationModel.SetCursorManager(_cursorManager);
            _presentationModel._cursorChanged += HandleCursorChanged;
            _presentationModel._pageChanged += ProcessPageChanged;
            _presentationModel.ProcessCursorClicked();
            SizeChanged += PowerPointSizeChanged;
            PowerPointSizeChanged(this, null); // lazy resize
            InitializeSlide(); // lazy add page
            SplitContainer1Adjust(this, null);
            _shapeGridView.DataSource = _presentationModel.Shapes;
            _insertDialog = new InsertShapeDialog();
            _saveDialog = new SaveDialog(_presentationModel);
            _loadDialog = new LoadDialog(_presentationModel);
        }
        private void _flowLayoutPanelLayout(object sender, LayoutEventArgs e)
        {
            bool verticalScrollVisible = _flowLayoutPanel.Height < _flowLayoutPanel.DisplayRectangle.Height;
            int scrollbarWidth = SystemInformation.VerticalScrollBarWidth;

            foreach (Control control in _flowLayoutPanel.Controls)
            {
                if (verticalScrollVisible)
                {
                    control.Width = _flowLayoutPanel.Width - scrollbarWidth - (_flowLayoutPanel.Padding.Horizontal + control.Margin.Horizontal);
                    control.Height = (int)(control.Width / Constant.SLIDE_RATIO);
                }
                else
                {
                    control.Width = _flowLayoutPanel.Width - (_flowLayoutPanel.Padding.Horizontal + control.Margin.Horizontal);
                    control.Height = (int)(control.Width / Constant.SLIDE_RATIO);
                }
            }
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
            if (_flowLayoutPanel.Controls.Count == 0 || _canvas.Width == Constant.ZERO_INTEGER || _canvas.Height == Constant.ZERO_INTEGER)
                return;
            _brief = new Bitmap(_canvas.Width, _canvas.Height);
            _canvas.DrawToBitmap(_brief, new System.Drawing.Rectangle(Constant.ZERO_INTEGER, Constant.ZERO_INTEGER, _canvas.Width, _canvas.Height));
            Button button = ((Button)(_flowLayoutPanel.Controls[SlideIndex]));
            button.BackgroundImage = new Bitmap(_brief, button.Size);
        }

        // insert shape
        private void ButtonInsertShapeClick(object sender, EventArgs e)
        {
            // _presentationModel.ProcessInsertShape(_shapeComboBox.Text);
            // if the dialog box not open, open it
            _insertDialog.SetupDialog(_canvas.Size, _shapeComboBox.Text, _presentationModel);
            _insertDialog.ShowDialog();
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
            if (SlideIndex != -1)
                GenerateBrief();
        }
        private Bitmap _brief;

        // handle splitter adjust
        private void SplitContainer1Adjust(object sender, SplitterEventArgs e)
        {
            // make sure the thumbnail and the slide button is updated keep the aspect ratio 16:9
            double panel1Width = _flowLayoutPanel.Width;
            foreach (Button slideButton in _flowLayoutPanel.Controls)
            {
                slideButton.Width = (int)panel1Width - (_flowLayoutPanel.Padding.Horizontal + slideButton.Margin.Horizontal);
                slideButton.Height = (int)(slideButton.Width / Constant.SLIDE_RATIO);
            }
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
                _canvas.Height += (_canvas.Height & 1); // make sure the height is even
                _canvas.Width = (int)(_canvas.Height * Constant.SLIDE_RATIO);
                _canvas.Width += (_canvas.Width & 1); // make sure the width is even
                _canvas.Location = new Point((int)((panel2Width - _canvas.Width) / Constant.PEN_THICK), (int)((panel2Height - _canvas.Height) / Constant.PEN_THICK));
            }
            else
            {
                _canvas.Width = (int)panel2Width - _splitContainer2.Panel1.Margin.Horizontal;
                _canvas.Width += (_canvas.Width & 1); // make sure the width is even
                _canvas.Height = (int)(_canvas.Width / Constant.SLIDE_RATIO);
                _canvas.Height += (_canvas.Height & 1); // make sure the height is even
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

        // add page button click
        private void AddPageButtonClick(object sender, EventArgs e)
        {
            _presentationModel.ProcessAddPage(SlideIndex + 1);
        }

        // handle page changed
        private void ProcessPageChanged(int index, Page.Action action)
        {
            switch (action)
            {
                case Page.Action.Add:
                    if (SlideIndex == -1)
                    {
                        _canvas.BackColor = Color.White;
                        _canvas.Enabled = true;
                        _shapeGridView.Enabled = true;
                        _lineAddButton.Enabled = true;
                        _rectangleAddButton.Enabled = true;
                        _ellipseAddButton.Enabled = true;
                        _buttonInsertShape.Enabled = true;
                        _cursorButton.Enabled = true;
                        _cursorButton.Checked = true;
                    }
                    SlideIndex = index;
                    Button button = new Button();
                    double panel1Width = _flowLayoutPanel.Width;
                    button.Width = (int)panel1Width - (_flowLayoutPanel.Padding.Horizontal + button.Margin.Horizontal);
                    button.Height = (int)(button.Width / Constant.SLIDE_RATIO);
                    button.BackColor = Color.White;
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                    button.Name = "Slide";
                    button.Click += HandleSlideButtonClick;
                    _flowLayoutPanel.Controls.Add(button);
                    _flowLayoutPanel.Controls.SetChildIndex(button, index);
                    button.Focus();
                    _presentationModel.SlideIndex = SlideIndex;
                    _shapeGridView.DataSource = _presentationModel.Shapes;
                    break;
                case Page.Action.Remove:
                    if (SlideIndex == _flowLayoutPanel.Controls.Count - 1)
                        SlideIndex--;
                    _flowLayoutPanel.Controls.RemoveAt(index);
                    _presentationModel.SlideIndex = SlideIndex;
                    if (SlideIndex == -1)
                    {
                        _canvas.BackColor = Color.Gray;
                        _canvas.Enabled = false;
                        _shapeGridView.Enabled = false;
                        _lineAddButton.Enabled = false;
                        _rectangleAddButton.Enabled = false;
                        _ellipseAddButton.Enabled = false;
                        _cursorButton.Enabled = false;
                        _cursorButton.Checked = false;
                        _buttonInsertShape.Enabled = false;
                        _shapeGridView.DataSource = new BindingList<Shape>();
                        return;
                    }
                    _flowLayoutPanel.Controls[SlideIndex].Focus();
                    _shapeGridView.DataSource = _presentationModel.Shapes;
                    break;
                case Page.Action.Switch:
                    SlideIndex = index;
                    _presentationModel.SlideIndex = SlideIndex;
                    _shapeGridView.DataSource = _presentationModel.Shapes;
                    _flowLayoutPanel.Controls[SlideIndex].Focus();
                    break;
            }
            _flowLayoutPanel.PerformLayout();
            HandleModelChanged(this, null);
            GenerateBrief();
        }

        // initialize first page
        private void InitializeSlide()
        {
            SlideIndex = 0;
            Button button = new Button();
            double panel1Width = _flowLayoutPanel.Width;
            button.Width = (int)panel1Width - _flowLayoutPanel.Margin.Horizontal;
            button.Height = (int)(button.Width / Constant.SLIDE_RATIO);
            button.Click += HandleSlideButtonClick;
            button.BackColor = Color.White;
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.Name = "Slide";
            _flowLayoutPanel.Controls.Add(button);
            _flowLayoutPanel.Controls.SetChildIndex(button, SlideIndex);
            _presentationModel.SlideIndex = 0;
            GenerateBrief();
        }
        // handle slide buttin click
        private void HandleSlideButtonClick(object sender, EventArgs e)
        {
            var index = _flowLayoutPanel.Controls.GetChildIndex((Button)sender);
            // change the current page
            _presentationModel.ProcessSwitchPage(index);
            _shapeGridView.DataSource = _presentationModel.Shapes;
            GenerateBrief();
        }

        // handle save button click
        private void _saveButtonClick(object sender, EventArgs e)
        {
            _saveDialog.ShowDialog();
        }

        // handle load button click
        private void _loadButtonClick(object sender, EventArgs e)
        {
            _loadDialog.ShowDialog();
        }
    }
}
