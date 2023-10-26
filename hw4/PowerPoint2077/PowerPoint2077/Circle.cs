using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowPowerPoint
{
    class Circle : Shape
    {
        public Circle() : base(ShapeType.CIRCLE)
        {
            _name = Constant.CIRCLE_CHINESE;
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
        }
    }
}
