using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks.Dataflow;
namespace WindowPowerPoint
{
    public class PointState : IState
    {
        private PowerPointModel _model;
        public PointState(PowerPointModel model)
        {
            _model = model;
        }
        
        // handle mouse down
        public void MouseDown(Point point)
        {
            _model.ClearSelectedShape();
            foreach (Shape shape in _model.Shapes)
            {

                if (shape.IsInShape(point))
                {
                    shape.Selected = true;
                    _isAdjusting = true;
                    _lastPoint = point;
                    break;
                }
            }
            if (!_isAdjusting)
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
        }

        // handle mouse up
        public void MouseUp(Point point)
        {
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
        private bool _isAdjusting;
        private Point _lastPoint;
    }
}
