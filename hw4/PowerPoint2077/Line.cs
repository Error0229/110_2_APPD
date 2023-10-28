namespace WindowPowerPoint
{
    class Line : Shape
    {
        public Line() : base(ShapeType.LINE)
        {
            _name = Constant.LINE_CHINESE;
        }

        // get line's info
        public override string GetInfo()
        {
            return base.GetInfo();
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

        // Draw Line Handle
        public override void DrawHandle(IGraphics graphics)
        {
            graphics.DrawLineHandle(_pointFirst, _pointSecond);
        }
    }
}
