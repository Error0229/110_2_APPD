using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public class PowerPointPresentationModel
    {
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler _modelChanged;
        public PowerPointPresentationModel(PowerPointModel model)
        {
            this._model = model;
            _model._modelChanged += HandleModelChanged;
            _isCircleChecked = false;
            _isLineChecked = false;
            _isRectangleChecked = false;
            _isPressed = false;
            _isMoving = false;
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

        // process mouse enter canva
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

        // process mouse leave canva
        public Cursor ProcessMouseLeaveCanvas()
        {
            _isRectangleChecked = _isLineChecked = _isCircleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
            return Cursors.Default;
        }

        // Set canva coordinate
        public void SetCanvasCoordinate(Point pointFirst, Point pointSecond)
        {
            _model.SetCanvasCoordinate(pointFirst, pointSecond);
        }

        // on model changed
        protected virtual void NotifyModelChanged(EventArgs e)
        {
            if (_modelChanged != null)
                _modelChanged(this, e);
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
            _hintType = ShapeType.LINE;
            _isCircleChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // circle clicked
        public void ProcessEllipseClicked()
        {
            _isCircleChecked = true;
            _hintType = ShapeType.CIRCLE;
            _isLineChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // rectangle clicked
        public void ProcessRectangleClicked()
        {
            _isRectangleChecked = true;
            _hintType = ShapeType.RECTANGLE;
            _isLineChecked = _isCircleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // process canva pressed
        public void ProcessCanvasPressed(Point point)
        {
            if (IsDrawing())
            {
                _model.SetHint(_hintType);
                _model.SetHintFirstPoint(point);
                _isPressed = true;
            }
        }

        // process mouse moving while pressed in canva
        public void ProcessCanvasMoving(Point point)
        {
            if (_isPressed)
            {
                _isMoving = true;
                _model.SetHintSecondPoint(point);
            }
        }

        // process mouse release while drawing
        public void ProcessCanvasReleased()
        {
            if (_isPressed)
            {
                _model.AddShapeWithHint();
                _isPressed = _isMoving = false;
            }
        }

        // draw all the shape
        public void Draw(Graphics graphics)
        {
            _model.Draw(new WindowsFormsGraphicsAdaptor(graphics));
            if (_isMoving)
            {
                _model.DrawHint(new WindowsFormsGraphicsAdaptor(graphics));
            }
        }

        // is circle checked
        public bool IsCircleChecked()
        {
            return _isCircleChecked;
        }

        // is line checked
        public bool IsLineChecked()
        {
            return _isLineChecked;
        }

        // is rectangle checked
        public bool IsRectangleChecked()
        {
            return _isRectangleChecked;
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
        private bool _isPressed;
        private bool _isMoving;
        private ShapeType _hintType;
        private PowerPointModel _model;
    }
}
