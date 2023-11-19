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
            _draggingHandle = new Handle();
        }
        
        // handle mouse down
        public void MouseDown(Point point)
        {
            foreach (Shape shape in _model.Shapes)
            {
                _draggingHandle = shape.IsCloseToHandle(point);
                if (shape.Selected && _draggingHandle.Type != HandleType.NONE)
                {
                    shape.SelectHandle(_draggingHandle.Type);
                    _isAdjusting = true;
                    return;
                }
            }
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
            if (!_isMoving)
            {
                _model.ClearSelectedShape();
            }

            _model.NotifyModelChanged(EventArgs.Empty);
        }

        // handle mouse move
        public void MouseMove(Point point)
        {
            if (_isAdjusting)
            {
                _draggingHandle.Position = point;
                foreach (Shape shape in _model.Shapes)
                {
                    if (shape.Selected)
                    {
                        shape.AdjustByHandle(point);
                        _model.NotifyModelChanged(EventArgs.Empty);
                        break;
                    }
                }
                return;
            }
            if (_isMoving)
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
                return;
            }
            foreach(Shape shape in _model.Shapes)
            {
                if (shape.Selected)
                {
                    var handle = shape.IsCloseToHandle(point);
                    _model.cursorManager.CurrentCursor = PowerPointModel.handleToCursor[handle.Type];
                    break;
                }
                _model.cursorManager.CurrentCursor = Cursors.Default;
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
        private Handle _draggingHandle;
        private bool _isAdjusting;
        private bool _isMoving;
        private Point _lastPoint;
    }
}
