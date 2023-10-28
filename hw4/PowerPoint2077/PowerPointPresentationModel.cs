using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Accessibility;

namespace WindowPowerPoint
{
    public class PowerPointPresentationModel : INotifyPropertyChanged
    {
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler _modelChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public PowerPointPresentationModel(PowerPointModel model)
        {
            this._model = model;
            _model._modelChanged += HandleModelChanged;
            _isCircleChecked = false;
            _isLineChecked = false;
            _isRectangleChecked = false;
            _isSelecting = false;
            // _isPressed = false;
            // _isMoving = false;
        }

        // insert shape
        public void ProcessInsertShape(string shapeName)
        {
            if (shapeName != string.Empty)
            {
                _model.InsertShape(shapeName);
            }
        }

        // get shape
        public void ProcessRemoveShape(int columnIndex, int index)
        {
            if (columnIndex == 0 && index >= 0)
            {
                _model.RemoveShape(index);
            }
        }

        // process mouse enter canvas
        public Cursor ProcessMouseEnterCanvas()
        {
            if (IsDrawing())
            {
                return Cursors.Cross;
            }
            else
            {
                return Cursors.Default;
            }
        }

        // process mouse leave canvas
        public Cursor ProcessMouseLeaveCanvas()
        {
            _isSelecting = _isRectangleChecked = _isLineChecked = _isCircleChecked = false;
            _model.SetState(new IdleState());
            _model.ClearSelectedShape();
            NotifyModelChanged(EventArgs.Empty);
            return Cursors.Default;
        }

        // Set canvas coordinate
        public void SetCanvasCoordinate(Point pointFirst, Point pointSecond)
        {
            _model.SetCanvasCoordinate(pointFirst, pointSecond);
        }

        // on model changed
        protected virtual void NotifyModelChanged(EventArgs e)
        {
            if (_modelChanged != null)
                _modelChanged(this, e);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_LINE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_CIRCLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_RECTANGLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_CURSOR_CHECKED));
            }
        }

        // handle model changed
        private void HandleModelChanged(object sender, EventArgs e)
        {
            NotifyModelChanged(e);
        }

        // line clicked 
        public void ProcessLineClicked()
        {
            _isLineChecked = true;
            _model.ClearSelectedShape();
            _model.SetHint(ShapeType.LINE);
            _model.SetState(new DrawingState(_model));
            _isSelecting = _isCircleChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // circle clicked
        public void ProcessEllipseClicked()
        {
            _isCircleChecked = true;
            _model.ClearSelectedShape();
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetState(new DrawingState(_model));
            _isSelecting = _isLineChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // rectangle clicked
        public void ProcessRectangleClicked()
        {
            _isRectangleChecked = true;
            _model.ClearSelectedShape();
            _model.SetHint(ShapeType.RECTANGLE);
            _model.SetState(new DrawingState(_model));
            _isSelecting = _isLineChecked = _isCircleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // cursor icon clicked
        public void ProcessCursorClicked()
        {
            _isSelecting = true;
            _model.SetState(new PointState(_model));
            _isLineChecked = _isCircleChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // process key down
        public void ProcessKeyDown(Keys keyCode)
        {
            _model.HandleKeyDown(keyCode);
        }

        // process canvas pressed
        public void ProcessCanvasPressed(Point point)
        {
            if (IsDrawing() || IsCursorChecked)
            {
                // _model.SetHintFirstPoint(point);
                _model.HandleMouseDown(point);
                // _isPressed = true;
            }
        }

        // process mouse moving while pressed in canvas
        public void ProcessCanvasMoving(Point point)
        {
            // if (_isPressed)
            // {
            //     _isMoving = true;
            //     // _model.SetHintSecondPoint(point);
            // }
            _model.HandleMouseMove(point);
        }

        // process mouse release while drawing
        public Cursor ProcessCanvasReleased(Point point)
        {
            _model.HandleMouseUp(point);
            if (IsDrawing())
            {
                // _model.AddShapeWithHint();
                return ProcessMouseLeaveCanvas();
            }
            return Cursors.Default;
        }

        // draw all the shape
        public void Draw(IGraphics graphics)
        {
            _model.Draw(graphics);
        }

        // is circle checked
        public bool IsCircleChecked
        {
            get
            {
                return _isCircleChecked;
            }
        }

        // is line checked
        public bool IsLineChecked
        {
            get
            {
                return _isLineChecked;
            }
        }

        // is rectangle checked
        public bool IsRectangleChecked
        {
            get
            {
                return _isRectangleChecked;
            }
        }

        // is cursor checked
        public bool IsCursorChecked
        {
            get
            {
                return _isSelecting;
            }
        }

        // is drawing
        public bool IsDrawing()
        {
            return _isRectangleChecked || _isLineChecked || _isCircleChecked;
        }

        public BindingList<Shape> Shapes
        {
            get
            {
                return _model.Shapes;
            }
        }

        private bool _isLineChecked;
        private bool _isCircleChecked;
        private bool _isRectangleChecked;
        private bool _isSelecting;
        private PowerPointModel _model;
    }
}
