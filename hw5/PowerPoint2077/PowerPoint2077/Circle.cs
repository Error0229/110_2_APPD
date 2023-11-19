using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
namespace WindowPowerPoint
{
    public class Circle : Shape
    {
        public Circle() : base(ShapeType.CIRCLE)
        {
            _name = Constant.CIRCLE_CHINESE;
            InitializeRectangleHandle();
        }

        // get circle's info
        public override string GetInfo()
        {
            AdjustPoints();
            return base.GetInfo();
        }

        // draw circle
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawCircle(GetShapeRectangle());
            if (_isSelected)
            {
                DrawHandle(graphics);
            }
        }

        // draw circle handles 
        public override void AdjustHandle()
        {
            AdjustRectangleHandle();
        }

        // draw circle handle
        public override void DrawHandle(IGraphics graphics)
        {
            graphics.DrawOutline(GetShapeRectangle());
            AdjustHandle();
            foreach (var handle in _handles)
            {
                graphics.DrawHandle(handle.Position);
            }
        }
    }
}
