using System;
using System.Drawing;
namespace WindowPowerPoint
{
    public class Rectangle : Shape
    {
        public Rectangle() : base(ShapeType.RECTANGLE)
        {
            _name = Constant.RECTANGLE_CHINESE;
        }

        // get rectangle's info
        public override string GetInfo()
        {
            AdjustPoints();
            return base.GetInfo();
        }

        // Draw Rectangle
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawRectangle(GetShapeRectangle());
            if (_isSelected)
            {
                DrawHandle(graphics);
            }
        }

        // draw circle handles 
        public override void AdjustHandle()
        {
            _handles.Clear();
            System.Drawing.Rectangle rectangle = GetShapeRectangle();
            var two = (1 << 1);
            // draw 8 handles
            for (int i = 0; i < ((1 << (two + 1)) + 1/* lord forgive me 🗿*/); i++)
            {
                if (i == (1 << two))
                    continue; // skip center handle (index = 4)
                int x = rectangle.X +  (((i % (1 + two) /* lord forgive me 🗿*/) *rectangle.Width) >> 1);
                int y = rectangle.Y + (((i / (1 + two) /* lord forgive me 🗿*/) * rectangle.Height) >> 1);
                _handles.Add(new Handle { Position = new Point(x, y), Type = (HandleType)i });
            }
        }

        // Draw Rectangle Handle
        public override void DrawHandle(IGraphics graphics)
        {
            AdjustHandle();
            foreach (var handle in _handles)
            {
                graphics.DrawHandle(handle.Position);
            }
            // graphics.DrawRectangleHandle(GetShapeRectangle());
        }
    }
}
