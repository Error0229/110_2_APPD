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

        // draw circle
        public override void Draw(Graphics graphics)
        {
            graphics.DrawEllipse(new Pen(Color.Black, Constant.PEN_THICK), GetShapeRectangle());
        }
    }
}
