using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public class PowerPointModel
    {
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler _modelChanged;

        private IState _state;
        public PowerPointModel()
        {
            _shapes = new BindingList<Shape>();
            _factory = new ShapeFactory();
            _state = new IdleState();
        }

        // insert shape by shape name
        public virtual void InsertShape(string shapeName)
        {
            var random = new Random();
            Shape shape = _factory.CreateShape(shapeName);
            shape.SetFirstPoint(new Point(random.Next(_canvasTopLeft.X, _canvasButtonRight.X), random.Next(_canvasTopLeft.Y, _canvasButtonRight.Y)));
            shape.SetSecondPoint(new Point(random.Next(_canvasTopLeft.X, _canvasButtonRight.X), random.Next(_canvasTopLeft.Y, _canvasButtonRight.Y)));
            _shapes.Add(shape);
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert shape by shape name and coordinate
        public virtual void InsertShape(string shapeName, Point firstPoint, Point secondPoint)
        {
            var random = new Random();
            Shape shape = _factory.CreateShape(shapeName);
            shape.SetFirstPoint(firstPoint);
            shape.SetSecondPoint(secondPoint);
            _shapes.Add(shape);
            NotifyModelChanged(EventArgs.Empty);
        }

        // set state 
        public virtual void SetState(IState state)
        {
            _state = state;
        }

        // remove shape by index
        public virtual void RemoveShape(int index)
        {
            _shapes.RemoveAt(index);
            NotifyModelChanged(EventArgs.Empty);
        }

        // set canvas coordinate
        public virtual void SetCanvasCoordinate(Point pointTopLeft, Point pointButtonRight)
        {
            _canvasTopLeft = pointTopLeft;
            _canvasButtonRight = pointButtonRight;
        }

        // Handle mouse down
        public virtual void HandleMouseDown(Point point)
        {
            _state.MouseDown(point);
        }

        // Handle mouse move
        public virtual void HandleMouseMove(Point point)
        {
            _state.MouseMove(point);
        }

        // Handle mouse up
        public virtual void HandleMouseUp(Point point)
        {
            _state.MouseUp(point);
        }

        // handle Key down
        public virtual void HandleKeyDown(Keys keyCode)
        {
            _state.KeyDown(keyCode);
        }

        // draw shapes
        public virtual void Draw(IGraphics graphics)
        {
            DrawShapes(graphics);
            _state.Draw(graphics);
        }

        // draw all shapes
        public void DrawShapes(IGraphics graphics)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Draw(graphics);
            }
        }

        // clear selected shape
        public void ClearSelectedShape()
        {
            foreach (Shape shape in _shapes)
            {
                shape.Selected = false;
            }
        }

        // draw hint
        public void DrawHint(IGraphics graphics)
        {
            _hint.Draw(graphics);
        }

        // model change function
        public virtual void NotifyModelChanged(EventArgs e)
        {
            if (_modelChanged != null)
                _modelChanged(this, e);
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
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert user drew shape by hint
        public void AddShapeWithHint()
        {
            _shapes.Add(_hint);
            NotifyModelChanged(EventArgs.Empty);
        }

        private Shape _hint;
        private Point _canvasTopLeft;
        private Point _canvasButtonRight;
        private readonly ShapeFactory _factory;
        private readonly BindingList<Shape> _shapes;
        public BindingList<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }
    }
}
