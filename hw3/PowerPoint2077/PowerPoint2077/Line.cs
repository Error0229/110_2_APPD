using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowPowerPoint
{
    class Line : Shape
    {
        public Line() : base(ShapeType.LINE)
        {
            _name = Constant.LINE_CHINESE;
        }

        // Draw Line
        public override void Draw(Graphics graphics)
        {
            graphics.DrawLine(new Pen(Color.Black, 2), _pointFirst, _pointSecond);
        }
    }
}
