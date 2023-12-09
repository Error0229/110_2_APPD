namespace WindowPowerPoint
{
    public class Rectangle : Shape
    {
        public Rectangle() : base(ShapeType.RECTANGLE)
        {
            _name = Constant.RECTANGLE_CHINESE;
            InitializeRectangleHandle();
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
            AdjustRectangleHandle();
        }

        // Draw Rectangle Handle
        public override void DrawHandle(IGraphics graphics)
        {
            AdjustHandle();
            foreach (var handle in _handles)
            {
                graphics.DrawHandle(handle.Position);
            }
        }
    }
}
