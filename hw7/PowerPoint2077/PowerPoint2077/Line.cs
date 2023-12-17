using System.Drawing;
namespace WindowPowerPoint
{
    public class Line : Shape
    {
        public Line() : base(ShapeType.LINE)
        {
            _name = Constant.LINE_CHINESE;
        }

        // Draw Line
        public override void Draw(IGraphics graphic)
        {
            graphic.DrawLine(GetPoint(_pointFirst), GetPoint(_pointSecond));
            if (_isSelected)
            {
                DrawHandle(graphic);
            }
        }

        // generate handle
        public override void AdjustHandle()
        {
            _handles.Clear();
            _handles.Add(new Handle
            {
                Position = GetPoint(_pointFirst),
                Type = HandleType.TopLeft
            });
            _handles.Add(new Handle
            {
                Position = GetPoint(_pointSecond),
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
        public override void DrawHandle(IGraphics graphic)
        {
            AdjustHandle();
            foreach (var handle in _handles)
            {
                graphic.DrawHandle(handle.Position);
            }
        }
    }
}
