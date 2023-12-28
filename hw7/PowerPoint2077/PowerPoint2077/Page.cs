using System.ComponentModel;
using System.Drawing;

namespace WindowPowerPoint
{
    public class Page
    {
        public enum Action
        {
            Add,
            Remove,
            Switch
        }
        private BindingList<Shape> _shapes;
        public BindingList<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
            set
            {
                _shapes = value;
            }
        }
        public Page()
        {
            Shapes = new BindingList<Shape>();
        }

        // add shape
        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        // remove shape
        public void RemoveShape(Shape shape)
        {
            _shapes.Remove(shape);
        }

        // remove shape by index
        public void RemoveShape(int index)
        {
            _shapes.RemoveAt(index);
        }

        // encode page
        public virtual string Convert()
        {
            var result = string.Empty;
            for (int i = 0; i < _shapes.Count; i++)
            {
                result += Constant.LEFT_BRACE + _shapes[i].GetConvert() + Constant.RIGHT_BRACE + (Constant.COMMA + Constant.SPACE)[(i != _shapes.Count - 1) ? 0 : 1];
            }
            return result;
        }

        // decide page
        public void Interpret(string data, Size canvasSize)
        {
            var shapes = data.Trim().Split(Constant.LEFT_BRACE[0]);
            for (int i = 1; i < shapes.Length; i++)
            {
                var shapeInfo = shapes[i].Substring(0, shapes[i].Length - 1);
                var type = shapeInfo.Split(Constant.COMMA[0])[0];
                var coordinate = shapeInfo.Split(Constant.LEFT_BRACKET[0], Constant.RIGHT_BRACKET[0])[1];
                _shapes.Add(InterpretShape(type, coordinate, canvasSize));
            }
        }

        // interpret shape
        public Shape InterpretShape(string type, string coordinate, Size canvasSize)
        {
            Shape shape;
            switch (type)
            {
                case Constant.LINE:
                    shape = new Line();
                    break;
                case Constant.RECTANGLE:
                    shape = new Rectangle();
                    break;
                case Constant.CIRCLE:
                    shape = new Circle();
                    break;
                default:
                    throw new System.Exception(Constant.INVALID_SHAPE_TYPE);
            }
            shape.Interpret(coordinate, canvasSize);
            return shape;
        }
    }
}
