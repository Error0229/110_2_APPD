namespace WindowPowerPoint
{
    public class Circle : Shape
    {
        public Circle() : base(ShapeType.CIRCLE)
        {
            _name = Constant.CIRCLE_CHINESE;
            InitializeRectangleHandle();
        }

        // get circle's info
        public override string GetInfo()
        {
            AdjustPoints();
            return base.GetInfo();
        }

        // draw circle
        public override void Draw(IGraphics graphic)
        {
            graphic.DrawCircle(GetShapeRectangle());
            if (_isSelected)
            {
                DrawHandle(graphic);
            }
        }

        // draw circle handles 
        public override void AdjustHandle()
        {
            AdjustRectangleHandle();
        }

        // draw circle handle
        public override void DrawHandle(IGraphics graphic)
        {
            graphic.DrawOutline(GetShapeRectangle());
            AdjustHandle();
            foreach (var handle in _handles)
            {
                graphic.DrawHandle(handle.Position);
            }
        }
    }
}
