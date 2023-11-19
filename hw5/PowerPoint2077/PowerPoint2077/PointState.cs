using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace WindowPowerPoint
{
    public class PointState : IState
    {
        private PowerPointModel _model;
        public PointState(PowerPointModel model)
        {
            _model = model;
            _adjustingHandleType = HandleType.None;
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
            foreach (Shape shape in _model.Shapes)
            {
                if (shape.TryAdjustWhenMouseDown(point, out _isAdjusting, out _adjustingHandleType))
                {
                    break;
                }
            }
        }

        // mouse down for shape move
        private void MouseDownForMove(Point point)
        {
            _model.ClearSelectedShape();
            foreach (Shape shape in _model.Shapes)
            {
                if (shape.IsInShape(point))
                {
                    shape.Selected = true;
                    _isMoving = true;
                    _lastPoint = point;
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
            foreach (Shape shape in _model.Shapes)
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
            foreach (Shape shape in _model.Shapes)
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
            foreach (Shape shape in _model.Shapes)
            {
                if (shape.Selected)
                {
                    var handleType = shape.IsCloseToHandle(point);
                    _model.Manager.CurrentCursor = _model.HandleToCursor[handleType];
                    break;
                }
                _model.Manager.CurrentCursor = Cursors.Default;
            }
        }

        // handle mouse up
        public void MouseUp(Point point)
        {
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
                foreach (Shape shape in _model.Shapes)
                {
                    if (shape.Selected)
                    {
                        _model.RemoveShape(_model.Shapes.IndexOf(shape));
                        _model.NotifyModelChanged(EventArgs.Empty);
                        break;
                    }
                }
            }
        }
        private HandleType _adjustingHandleType;
        private bool _isAdjusting;
        private bool _isMoving;
        private Point _lastPoint;
    }
}
