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
        public void AddShape(Shape shape)
        {
            Shapes.Add(shape);
        }
        public void RemoveShape(Shape shape)
        {
            Shapes.Remove(shape);
        }
        public void RemoveShape(int index)
        {
            Shapes.RemoveAt(index);
        }

        public string Encode()
        {
            var result = "";
            for (int i = 0; i < Shapes.Count; i++)
            {
                result += '{' + Shapes[i].Encode() + '}' + ", "[(i != Shapes.Count - 1) ? 0 : 1];
            }
            return result;
        }

        public void Decode(string data, Size canvasSize)
        {
            var shapes = data.Trim().Split('{');
            for (int i = 1; i < shapes.Length; i++)
            {
                var shape = shapes[i].Substring(0, shapes[i].Length - 1);
                var type = shape.Split(',')[0];
                var data2 = shape.Split('(', ')')[1];
                switch (type)
                {
                    case "LINE":
                        var line = new Line();
                        line.Decode(data2, canvasSize);
                        Shapes.Add(line);
                        break;
                    case "RECTANGLE":
                        var rectangle = new Rectangle();
                        rectangle.Decode(data2, canvasSize);
                        Shapes.Add(rectangle);
                        break;
                    case "CIRCLE":
                        var circle = new Circle();
                        circle.Decode(data2, canvasSize);
                        Shapes.Add(circle);
                        break;
                }
            }
        }
    }
}
