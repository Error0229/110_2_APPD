using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public class PowerPointModel
    {
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler _modelChanged;
        private CursorManager _cursorManager;
        public virtual CursorManager ModelCursorManager
        {
            get
            {
                return _cursorManager;
            }
            set
            {
                _cursorManager = value;
            }
        }
        private CommandManager _commandManager;
        public virtual CommandManager ModelCommandManager
        {
            set
            {
                _commandManager = value;
            }
        }
        private IState _state;
        private readonly Dictionary<HandleType, Cursor> _handleToCursor;
        public Dictionary<HandleType, Cursor> HandleToCursor
        {
            get
            {
                return _handleToCursor;
            }
        }
        public PowerPointModel()
        {
            _shapes = new BindingList<Shape>();
            _factory = new ShapeFactory();
            _state = new PointState(this);
            _canvasSize = new Size(0, 0);
            _handleToCursor = new Dictionary<HandleType, Cursor>();
            _handleToCursor.Add(HandleType.TopLeft, Cursors.SizeNWSE);
            _handleToCursor.Add(HandleType.Top, Cursors.SizeNS);
            _handleToCursor.Add(HandleType.TopRight, Cursors.SizeNESW);
            _handleToCursor.Add(HandleType.Left, Cursors.SizeWE);
            _handleToCursor.Add(HandleType.Right, Cursors.SizeWE);
            _handleToCursor.Add(HandleType.BottomLeft, Cursors.SizeNESW);
            _handleToCursor.Add(HandleType.Bottom, Cursors.SizeNS);
            _handleToCursor.Add(HandleType.BottomRight, Cursors.SizeNWSE);
            _handleToCursor.Add(HandleType.None, Cursors.Default);
            // don't fuck me, fuck 🧑🏿‍⚕️smell
        }

        // insert shape by shape name
        public virtual void HandleInsertShape(string shapeName)
        {
            var random = new Random();
            Shape shape = _factory.CreateShape(shapeName);
            shape.SetFirstPoint(new Point(random.Next(0, _canvasSize.Width), random.Next(0, _canvasSize.Height)));
            shape.SetSecondPoint(new Point(random.Next(0, _canvasSize.Width), random.Next(0, _canvasSize.Height)));
            _commandManager.Execute(new AddCommand(this, shape));
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert shape by shape name and coordinate
        public virtual void HandleInsertShape(string shapeName, Point firstPoint, Point secondPoint)
        {
            Shape shape = _factory.CreateShape(shapeName);
            shape.SetFirstPoint(firstPoint);
            shape.SetSecondPoint(secondPoint);
            shape.AdjustHandle();
            _commandManager.Execute(new AddCommand(this, shape));
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert shape by shape for command pattern
        public virtual void InsertShape(Shape shape)
        {
            shape.CanvasSize = _canvasSize;
            _shapes.Add(shape);
            NotifyModelChanged(EventArgs.Empty);
        }

        // set state 
        public virtual void SetState(IState state)
        {
            _state = state;
        }

        // handle remove shape
        public virtual void HandleRemoveShape(int index)
        {
            _commandManager.Execute(new DeleteCommand(this, _shapes[index]));
            NotifyModelChanged(EventArgs.Empty);
        }

        // remove shape by shape
        public virtual void RemoveShape(Shape shape)
        {
            shape.Selected = false;
            _shapes.Remove(shape);
            NotifyModelChanged(EventArgs.Empty);
        }

        // set canvas coordinate
        public virtual void SetCanvasSize(Size canvasSize)
        {
            _canvasSize = canvasSize;
            foreach (Shape shape in _shapes)
            {
                shape.UpdateCanvasSize(canvasSize);
            }
            NotifyModelChanged(EventArgs.Empty);
        }

        // handle move shape
        public virtual void HandleMoveShape(Point offset)
        {
            foreach (Shape shape in _shapes)
            {
                if (shape.Selected)
                {
                    _commandManager.Execute(new MoveCommand(this, shape, offset));
                }
            }
            NotifyModelChanged(EventArgs.Empty);
        }

        // move shape
        public virtual void MoveShape(Shape shape, Point offset)
        {
            shape.Move(offset);
            NotifyModelChanged(EventArgs.Empty);
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
        public virtual void DrawShapes(IGraphics graphics)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Draw(graphics);
            }
        }

        // clear selected shape
        public virtual void ClearSelectedShape()
        {
            foreach (Shape shape in _shapes)
            {
                shape.Selected = false;
            }
        }

        // draw hint
        public virtual void DrawHint(IGraphics graphics)
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
        public virtual void SetHint(ShapeType type)
        {
            _hint = _factory.CreateShape(type);
        }

        // set hint's first point
        public virtual void SetHintFirstPoint(Point point)
        {
            _hint.SetFirstPoint(point);
        }

        // set hint's second point
        public virtual void SetHintSecondPoint(Point point)
        {
            _hint.SetSecondPoint(point);
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert user drew shape by hint
        public virtual void AddShapeWithHint()
        {
            _commandManager.Execute(new DrawCommand(this, _hint));
            NotifyModelChanged(EventArgs.Empty);
        }

        // redo
        public virtual void Redo()
        {
            _commandManager.Redo();
            NotifyModelChanged(EventArgs.Empty);
        }

        // undo
        public virtual void Undo()
        {
            _commandManager.Undo();
            NotifyModelChanged(EventArgs.Empty);
        }

        private Shape _hint;
        private Size _canvasSize;
        private readonly ShapeFactory _factory;
        private BindingList<Shape> _shapes;
        public virtual BindingList<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }
    }
}
