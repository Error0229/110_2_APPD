using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
namespace WindowPowerPoint
{
    public class DrawingState : IState
    {
        private PowerPointModel _model;
        public DrawingState(PowerPointModel model)
        {
            _model = model;
        }

        // handle mouse down
        public void MouseDown(Point point)
        {
            _model.SetHintFirstPoint(point);
            _isDrawing = true;
        }

        // handle mouse move
        public void MouseMove(Point point)
        {
            if (_isDrawing)
            {
                _model.SetHintSecondPoint(point);
            }
        }

        // handle mouse up
        public void MouseUp(Point point)
        {
            if (_isDrawing)
            {
                _model.AddShapeWithHint();
                _isDrawing = false;
            }
        }

        // draw
        public void Draw(IGraphics graphics)
        {
            if (_isDrawing)
            {
                _model.DrawHint(graphics);
            }
        }

        // handle key down
        public void KeyDown(System.Windows.Forms.Keys keyCode)
        {
        }
        private bool _isDrawing;
    }
}
