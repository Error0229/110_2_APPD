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
            Point pointTopLeft = new Point(Math.Min(_pointFirst.X, _pointSecond.X), Math.Min(_pointFirst.Y, _pointSecond.Y));
            Size rectangleSize = new Size(Math.Abs(_pointSecond.X - _pointFirst.X), Math.Abs(_pointSecond.Y - _pointFirst.Y));
            graphics.DrawRectangle(new Pen(Color.Black, 2), new System.Drawing.Rectangle(pointTopLeft, rectangleSize));
        }
    }
}
