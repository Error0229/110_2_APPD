using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
namespace WindowPowerPoint
{
    public class PowerPointModel
    {
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler ModelChanged;
        public PowerPointModel()
        {
            _shapes = new List<Shape>();
            _factory = new ShapeFactory();
        }
        // insert shape by shape name
        public void InsertShape(string shapeName)
        {
            var rand = new Random();
            Shape shape = _factory.CreateShape(shapeName);
            shape.SetFirstPoint(new Point(rand.Next(_canvaTopLeft.X, _canvaButtonRight.X), rand.Next(_canvaTopLeft.Y, _canvaButtonRight.Y)));
            shape.SetSecondPoint(new Point(rand.Next(_canvaTopLeft.X, _canvaButtonRight.X), rand.Next(_canvaTopLeft.Y, _canvaButtonRight.Y)));
            _shapes.Add(shape);
            OnModelChanged(EventArgs.Empty);

        }

        // remove shape by index
        public void RemoveShape(int index)
        {
            _shapes.RemoveAt(index);
            OnModelChanged(EventArgs.Empty);
        }

        // set canva coordinate
        public void SetCanvaCoordinate(Point pointTopLeft, Point pointButtonRight)
        {
            _canvaTopLeft = pointTopLeft;
            _canvaButtonRight = pointButtonRight;
        }

        // draw shapes
        public void Draw(Graphics graphics)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Draw(graphics);
            }
        }

        // draw hint
        public void DrawHint(Graphics graphics)
        {
            _hint.Draw(graphics);
        }
        // model change function
        protected virtual void OnModelChanged(EventArgs e)
        {
           
            ModelChanged?.Invoke(this, e);
        }

        // set hint type
        public void SetHint(ShapeType type)
        {
            _hint = _factory.CreateShape(type);
        }

        // set hint's first point
        public void SetHintFirstPoint(Point point)
        {
            _hint.SetFirstPoint(point);
        }

        // set hint's second point
        public void SetHintSecondPoint(Point point)
        {
            _hint.SetSecondPoint(point);
            OnModelChanged(EventArgs.Empty);
        }

        // insert user drew shape by hint
        public void AddShapeWithHint()
        {
            _shapes.Add(_hint);
            OnModelChanged(EventArgs.Empty);
        }
        private Shape _hint;
        private Point _canvaTopLeft;
        private Point _canvaButtonRight;
        private readonly List<Shape> _shapes;
        private readonly ShapeFactory _factory;
        public List<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }
    }
}
