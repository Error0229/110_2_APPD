using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace WindowPowerPoint
{
    public class PowerPointPresentationModel
    {
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler ModelChanged;
        public PowerPointPresentationModel(PowerPointModel model)
        {
            this._model = model;
            _model.ModelChanged += HandleModelChanged;
            _isEllipseChecked = false;
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
            if (columnIndex == 0)
            {
                _model.RemoveShape(index);
            }
        }

        // process mouse enter canva
        public Cursor ProcessMouseEnterCanva()
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
        public Cursor ProcessMouseLeaveCanva()
        {
            _isRectangleChecked = _isLineChecked = _isEllipseChecked = false;
            OnModelChanged(EventArgs.Empty);
            return Cursors.Default;
        }


        // Set canva coordinate
        public void SetCanvaCoordinate(Point pointFirst, Point pointSecond)
        {
            _model.SetCanvaCoordinate(pointFirst, pointSecond);
        }

        // on model changed
        protected virtual void OnModelChanged(EventArgs e)
        {
            ModelChanged?.Invoke(this, e);
        }
        private void HandleModelChanged(object sender, EventArgs e)
        {
            OnModelChanged(e);
        }


        public void ProcessLineClicked()
        {
            _isLineChecked = true;
            _hintType = ShapeType.LINE;
            _isEllipseChecked = _isRectangleChecked = false;
            OnModelChanged(EventArgs.Empty);
        }
        public void ProcessEllipseClicked()
        {
            _isEllipseChecked = true;
            _hintType = ShapeType.CIRCLE;
            _isLineChecked = _isRectangleChecked = false;
            OnModelChanged(EventArgs.Empty);
        }
        public void ProcessRectangleClicked()
        {
            _isRectangleChecked = true;
            _hintType = ShapeType.RECTANGLE;
            _isLineChecked = _isEllipseChecked = false;
            OnModelChanged(EventArgs.Empty);
        }
        // process canva pressed
        public void ProcessCanvaPressed(Point point)
        {
            if (IsDrawing())
            {
                _model.SetHint(_hintType);
                _model.SetHintFirstPoint(point);
                _isPressed = true;
            }
        }
        // process mouse moving while pressed in canva
        public void ProcessCanvaMoving(Point point)
        {
            if (_isPressed)
            {
                _isMoving = true;
                _model.SetHintSecondPoint(point);
            }
        }

        // process mouse release while drawing
        public void ProcessCanvaReleased(Point point)
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
            _model.Draw(graphics);
            if (_isMoving)
            {
                _model.DrawHint(graphics);
            }
        }

        public bool IsEliipseChecked()
        {
            return _isEllipseChecked;
        }
        public bool IsLineChecked()
        {
            return _isLineChecked;
        }
        public bool IsRectangleChecked()
        {
            return _isRectangleChecked;
        }

        // is drawing
        public bool IsDrawing()
        {
            return _isRectangleChecked || _isLineChecked || _isEllipseChecked;
        }
        public List<Shape> Shapes
        {
            get
            {
                return _model.Shapes;
            }
        }
        private bool _isLineChecked;
        private bool _isEllipseChecked;
        private bool _isRectangleChecked;
        private bool _isPressed;
        private bool _isMoving;
        private ShapeType _hintType;
        private PowerPointModel _model;
    }
}
