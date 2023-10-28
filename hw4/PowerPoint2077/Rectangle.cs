namespace WindowPowerPoint
{
    class Rectangle : Shape
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

        // Draw Rectangle Handle
        public override void DrawHandle(IGraphics graphics)
        {
            graphics.DrawRectangleHandle(GetShapeRectangle());
        }
    }
}
