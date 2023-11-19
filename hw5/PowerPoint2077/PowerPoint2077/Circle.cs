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
            var two = (1 << 1);
            for (int i = 0; i < ((1 << (two + 1)) + 1/* lord forgive me 🗿*/); i++)
            {
                _handles.Add(new Handle { Type = (HandleType)i });
            }
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
            System.Drawing.Rectangle rectangle = GetShapeRectangle();
            var two = 1 << 1;
            // draw 8 handles
            for (int i = 0; i < ((two << two) + 1/* lord forgive me 🗿*/); i++)
            {
                if (i == (1 << two))
                    continue; // skip center handle (index = 4)
                int x = rectangle.X + (((i % (1 + two) /* lord forgive me 🗿*/) * rectangle.Width) >> 1);
                int y = rectangle.Y + (((i / (1 + two) /* lord forgive me 🗿*/) * rectangle.Height) >> 1);
                var handle = _handles[i];
                handle.Position = new Point(x, y);
                _handles[i] = handle;
            }
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
            // graphics.DrawCircleHandle(GetShapeRectangle());
        }
    }
}
