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
        public BindingList<Shape> Shapes;
        public Page()
        {
            Shapes = new BindingList<Shape>();
        }

        // add shape
        public void AddShape(Shape shape)
        {
            Shapes.Add(shape);
        }

        // remove shape
        public void RemoveShape(Shape shape)
        {
            Shapes.Remove(shape);
        }

        // remove shape by index
        public void RemoveShape(int index)
        {
            Shapes.RemoveAt(index);
        }

        // encode page
        public string Encode()
        {
            var result = string.Empty;
            for (int i = 0; i < Shapes.Count; i++)
            {
                result += '{' + Shapes[i].Encode() + '}' + ", "[(i != Shapes.Count - 1) ? 0 : 1];
            }
            return result;
        }

        // decide page
        public void Decode(string data, Size canvasSize)
        {
            var shapes = data.Trim().Split('{');
            for (int i = 1; i < shapes.Length; i++)
            {
                var shapeInfo = shapes[i].Substring(0, shapes[i].Length - 1);
                var type = shapeInfo.Split(',')[0];
                var coordinate = shapeInfo.Split('(', ')')[1];
                Shape shape;
                switch (type)
                {
                    case "LINE":
                        shape = new Line();
                        break;
                    case "RECTANGLE":
                        shape = new Rectangle();
                        break;
                    case "CIRCLE":
                        shape = new Circle();
                        break;
                    default:
                        shape = new Circle();
                        break;
                }
                shape.Decode(coordinate, canvasSize);
                Shapes.Add(shape);
            }
        }
    }
}
