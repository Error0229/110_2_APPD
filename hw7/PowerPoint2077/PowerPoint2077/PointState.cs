using System;
using System.Drawing;
using System.Windows.Forms;
namespace WindowPowerPoint
{
    public class PointState : IState
    {
        private readonly PowerPointModel _model;
        public PointState(PowerPointModel model)
        {
            _model = model;
        }

        // handle mouse down
        public void MouseDown(Point point)
        {
            MouseDownForAdjust(point);
            if (_isAdjusting)
                return;
            MouseDownForMove(point);
            if (!_isMoving)
            {
                _model.ClearSelectedShape();
            }
            _model.NotifyModelChanged(EventArgs.Empty);
        }

        // mouse down for shape adjust
        private void MouseDownForAdjust(Point point)
        {
            foreach (Shape shape in _model.GetCurrentShapes())
            {
                if (shape.TryAdjustWhenMouseDown(point, out _isAdjusting))
                {
                    _shapeFirstPoint = shape.GetFirstPoint();
                    _shapeSecondPoint = shape.GetSecondPoint();
                    break;
                }
            }
        }

        // mouse down for shape move
        private void MouseDownForMove(Point point)
        {
            _model.ClearSelectedShape();
            foreach (Shape shape in _model.GetCurrentShapes())
            {
                if (shape.IsInShape(point))
                {
                    shape.Selected = true;
                    _isMoving = true;
                    _lastPoint = point;
                    _startPoint = point;
                    break;
                }
            }
        }

        // handle mouse move
        public void MouseMove(Point point)
        {
            if (_isAdjusting)
            {
                MouseMoveWhileAdjusting(point);
                return;
            }
            if (_isMoving)
            {
                MouseMoveWhileMoving(point);
                return;
            }
            MouseMoveWhileIdle(point);
        }

        // mouse move while adjusting shape
        private void MouseMoveWhileAdjusting(Point point)
        {
            foreach (Shape shape in _model.GetCurrentShapes())
            {
                if (shape.TryAdjustWhenMouseMove(point))
                {
                    _model.NotifyModelChanged(EventArgs.Empty);
                    return;
                }
            }
        }

        // mouse move while moving shape
        private void MouseMoveWhileMoving(Point point)
        {
            foreach (Shape shape in _model.GetCurrentShapes())
            {
                if (shape.Selected)
                {
                    shape.Move(point - new Size(_lastPoint));
                    _lastPoint = point;
                    _model.NotifyModelChanged(EventArgs.Empty);
                    break;
                }
            }
        }

        // mouse move while idle
        private void MouseMoveWhileIdle(Point point)
        {
            if (_model.Pages.Count == 0)
                return;
            foreach (Shape shape in _model.GetCurrentShapes())
            {
                if (shape.Selected)
                {
                    var handleType = shape.IsCloseToHandle(point);
                    _model.ModelCursorManager.CurrentCursor = _model.HandleToCursor[handleType];
                    break;
                }
                _model.ModelCursorManager.CurrentCursor = Cursors.Default;
            }
        }

        // handle mouse up
        public void MouseUp(Point point)
        {
            if (_isAdjusting)
            {
                _model.HandleShapeResize(_shapeFirstPoint, _shapeSecondPoint);
            }
            if (_isMoving && _lastPoint != _startPoint)
            {
                _model.HandleMoveShape(point - new Size(_startPoint));
            }
            _isMoving = false;
            _isAdjusting = false;
            _model.NotifyModelChanged(EventArgs.Empty);
        }

        // handle Draw
        public void Draw(IGraphics graphics)
        {

        }

        // handle key down
        public void KeyDown(Keys keyCode)
        {
            if (keyCode == Keys.Delete)
            {
                foreach (Shape shape in _model.GetCurrentShapes())
                {
                    if (shape.Selected)
                    {
                        _model.HandleRemoveShape(shape);
                        _model.NotifyModelChanged(EventArgs.Empty);
                        break;
                    }
                }
            }
        }
        private bool _isAdjusting;
        private bool _isMoving;
        private Point _lastPoint;
        private Point _startPoint;
        private PointF _shapeFirstPoint;
        private PointF _shapeSecondPoint;
    }
}
