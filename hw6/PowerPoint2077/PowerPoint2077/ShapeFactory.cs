using System;

namespace WindowPowerPoint
{
    public class ShapeFactory
    {
        // Create shape
        public Shape CreateShape(string shapeName)
        {
            switch (shapeName)
            {
                case Constant.RECTANGLE_CHINESE:
                    return new Rectangle();
                case Constant.LINE_CHINESE:
                    return new Line();
                case Constant.CIRCLE_CHINESE:
                    return new Circle();
                default:
                    throw new ArgumentException(Constant.INVALID_SHAPE_NAME);
            }
        }

        // create shape by type
        public Shape CreateShape(ShapeType type)
        {
            switch (type)
            {
                case ShapeType.RECTANGLE:
                    return new Rectangle();
                case ShapeType.LINE:
                    return new Line();
                case ShapeType.CIRCLE:
                    return new Circle();
                default:
                    throw new ArgumentException(Constant.INVALID_SHAPE_TYPE);
            }
        }
    }
}
