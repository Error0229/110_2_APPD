using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowPowerPoint
{
    public class Rectangle : Shape
    {
        public Rectangle() : base (ShapeType.RECTANGLE)
        {
            _name = Constant.RECTANGLE_CHINESE;
        }

        // Draw Rectangle
        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(new Pen(Color.Black, Constant.PEN_THICK), GetShapeRectangle());
        }
    }
}
