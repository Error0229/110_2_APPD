using System.Drawing;
using System.Collections.Generic;
namespace WindowPowerPoint
{
    public class Line : Shape
    {
        public Line() : base(ShapeType.LINE)
        {
            _name = Constant.LINE_CHINESE;
        }

        // Draw Line
        public override void Draw(IGraphics graphics)
        {
            graphics.DrawLine(_pointFirst, _pointSecond);
            if (_isSelected)
            {
                DrawHandle(graphics);
            }
        }

        // generate handle
        public override void AdjustHandle()
        {
            _handles.Clear();
            _handles.Add(new Handle
            {
                Position = _pointFirst,
                Type = HandleType.TopLeft
            });
            _handles.Add(new Handle
            {
                Position = _pointSecond,
                Type = HandleType.BottomRight
            });
        }

        // adjust by handle
        public override void AdjustByHandle(Point handlePosition)
        {
            switch (_selectedHandleType)
            {
                case HandleType.TopLeft:
                    _pointFirst = handlePosition;
                    break;
                case HandleType.BottomRight:
                    _pointSecond = handlePosition;
                    break;
            }
        }

        // Draw Line Handle
        public override void DrawHandle(IGraphics graphics)
        {
            AdjustHandle();
            foreach (var handle in _handles)
            {
                graphics.DrawHandle(handle.Position);
            }
            // graphics.DrawLineHandle(_pointFirst, _pointSecond);
        }
    }
}
