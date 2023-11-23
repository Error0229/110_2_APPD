using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using System.Runtime.CompilerServices;
namespace WindowPowerPoint
{
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE
    }
    public enum HandleType
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
        None
    }
    public struct Handle
    {
        Point _position;
        HandleType _type;
        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }
        public HandleType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

    }
    public abstract class Shape
    {
        private Dictionary<HandleType, Action<Point>> _adjustWith;
        public Shape(ShapeType type) : this()
        {
            _type = type;
        }
        public Shape()
        {
            _pointFirst = new Point();
            _pointSecond = new Point();
            _name = string.Empty;
            _selectedHandleType = HandleType.None;
            _handles = new List<Handle>();
            InitializeAdjustWith();
        }
        // get shape's info
        public virtual string GetInfo()
        {
            return FormatCoordinate(_pointFirst.X, _pointFirst.Y) + Constant.COMMA + Constant.SPACE + FormatCoordinate(_pointSecond.X, _pointSecond.Y);
        }

        // get shape's name
        public virtual string GetShapeName()
        {
            return _name;
        }

        // set first point
        public virtual void SetFirstPoint(Point point)
        {
            _pointFirst = point;
        }

        // set second point
        public virtual void SetSecondPoint(Point point)
        {
            _pointSecond = point;
        }

        // move first point
        public virtual void Move(Point offset)
        {
            _pointFirst.Offset(offset);
            _pointSecond.Offset(offset);
        }

        // adjust point to make it reasonable
        public void AdjustPoints()
        {
            var top = Math.Min(_pointFirst.X, _pointSecond.X);
            var left = Math.Min(_pointFirst.Y, _pointSecond.Y);
            var button = Math.Max(_pointFirst.X, _pointSecond.X);
            var right = Math.Max(_pointFirst.Y, _pointSecond.Y);
            _pointFirst.X = top;
            _pointFirst.Y = left;
            _pointSecond.X = button;
            _pointSecond.Y = right;
        }

        // check if point is in shape
        public virtual bool IsInShape(Point point)
        {
            return GetShapeRectangle().Contains(point);
        }

        // format coordinate
        protected string FormatCoordinate(int first, int second)
        {
            return Constant.LEFT_BRACKET + first + Constant.COMMA + Constant.SPACE + second + Constant.RIGHT_BRACKET;
        }

        // distance between two point
        protected double GetDistanceOf(Point pointFirst, Point pointSecond)
        {
            var deltaX = pointFirst.X - pointSecond.X;
            var deltaY = pointFirst.Y - pointSecond.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        // draw shape
        public abstract void Draw(IGraphics graphic);

        // draw handle
        public abstract void DrawHandle(IGraphics graphic);

        // generate handle position
        public abstract void AdjustHandle();

        // check close to handle
        public virtual HandleType IsCloseToHandle(Point cursor)
        {
            foreach (var handle in _handles)
            {
                if (GetDistanceOf(cursor, handle.Position) <= Constant.DISTANCE_THRESHOLD)
                {
                    return handle.Type;
                }
            }
            return HandleType.None;
        }

        // select handle
        public void SetSelectHandle(HandleType handle)
        {
            _selectedHandleType = handle;
        }

        // adjust by handle
        public virtual void AdjustByHandle(Point handlePosition)
        {
            _adjustWith[_selectedHandleType](handlePosition);
        }

        // adjust by top left handle
        public virtual void AdjustByTopLeft(Point handlePosition)
        {
            _pointFirst.X = handlePosition.X;
            _pointFirst.Y = handlePosition.Y;
            _selectedHandleType = _pointFirst.X < _pointSecond.X ? _selectedHandleType : HandleType.TopRight;
            _selectedHandleType = _pointFirst.Y < _pointSecond.Y ? _selectedHandleType : HandleType.BottomLeft;
        }

        // adjust by top handle
        public virtual void AdjustByTop(Point handlePosition)
        {
            _pointFirst.Y = handlePosition.Y;
            _selectedHandleType = _pointFirst.Y < _pointSecond.Y ? _selectedHandleType : HandleType.Bottom;
        }

        // adjust by top right handle
        public virtual void AdjustByTopRight(Point handlePosition)
        {

            _pointSecond.X = handlePosition.X;
            _pointFirst.Y = handlePosition.Y;
            _selectedHandleType = _pointFirst.X < _pointSecond.X ? _selectedHandleType : HandleType.TopLeft;
            _selectedHandleType = _pointFirst.Y < _pointSecond.Y ? _selectedHandleType : HandleType.BottomRight;
        }

        // adjust by left handle
        public virtual void AdjustByLeft(Point handlePosition)
        {
            _pointFirst.X = handlePosition.X;
            _selectedHandleType = _pointFirst.X < _pointSecond.X ? _selectedHandleType : HandleType.Right;
        }

        // adjust by right handle
        public virtual void AdjustByRight(Point handlePosition)
        {
            _pointSecond.X = handlePosition.X;
            _selectedHandleType = _pointFirst.X < _pointSecond.X ? _selectedHandleType : HandleType.Left;
        }

        // adjust by button left handle
        public virtual void AdjustByBottomLeft(Point handlePosition)
        {
            _pointFirst.X = handlePosition.X;
            _pointSecond.Y = handlePosition.Y;
            _selectedHandleType = _pointFirst.X < _pointSecond.X ? _selectedHandleType : HandleType.BottomRight;
            _selectedHandleType = _pointFirst.Y < _pointSecond.Y ? _selectedHandleType : HandleType.TopLeft;
        }

        // adjust by button handle
        public virtual void AdjustByBottom(Point handlePosition)
        {
            _pointSecond.Y = handlePosition.Y;
            _selectedHandleType = _pointFirst.Y < _pointSecond.Y ? _selectedHandleType : HandleType.Top;
        }

        // adjust by button right handle
        public virtual void AdjustByBottomRight(Point handlePosition)
        {
            _pointSecond.X = handlePosition.X;
            _pointSecond.Y = handlePosition.Y;
            _selectedHandleType = _pointFirst.X < _pointSecond.X ? _selectedHandleType : HandleType.BottomLeft;
            _selectedHandleType = _pointFirst.Y < _pointSecond.Y ? _selectedHandleType : HandleType.TopRight;
        }

        // initialize adjuster
        private void InitializeAdjustWith()
        {
            _adjustWith = new Dictionary<HandleType, Action<Point>>();
            _adjustWith.Add(HandleType.TopLeft, AdjustByTopLeft);
            _adjustWith.Add(HandleType.Top, AdjustByTop);
            _adjustWith.Add(HandleType.TopRight, AdjustByTopRight);
            _adjustWith.Add(HandleType.Left, AdjustByLeft);
            _adjustWith.Add(HandleType.Right, AdjustByRight);
            _adjustWith.Add(HandleType.BottomLeft, AdjustByBottomLeft);
            _adjustWith.Add(HandleType.Bottom, AdjustByBottom);
            _adjustWith.Add(HandleType.BottomRight, AdjustByBottomRight);
        }

        // initialize rectangle handle
        protected void InitializeRectangleHandle()
        {
            var two = (1 << 1);
            var count = 0;
            for (int i = 0; i < ((1 << (two + 1)) + 1/* lord forgive me 🗿*/); i++)
            {
                if (i == (1 << two))
                    continue; // skip center handle (index = 4)
                _handles.Add(new Handle
                {
                    Type = (HandleType)count++
                }); ;
            }
        }

        // adjust rectangle handles
        protected void AdjustRectangleHandle()
        {
            System.Drawing.Rectangle rectangle = GetShapeRectangle();
            var two = 1 << 1;
            var count = 0;
            // draw 8 handles
            for (int i = 0; i < ((two << two) + 1/* lord forgive me 🗿*/); i++)
            {
                if (i == (1 << two))
                    continue; // skip center handle (index = 4)
                int x = rectangle.X + (((i % (1 + two) /* lord forgive me 🗿*/) * rectangle.Width) >> 1);
                int y = rectangle.Y + (((i / (1 + two) /* lord forgive me 🗿*/) * rectangle.Height) >> 1);
                var handle = _handles[count];
                handle.Position = new Point(x, y);
                _handles[count++] = handle;
            }
        }

        // try adjust
        public virtual bool TryAdjustWhenMouseDown(Point point, out bool isAdjusting, out HandleType adjustingHandleType)
        {
            adjustingHandleType = IsCloseToHandle(point);
            if (Selected && adjustingHandleType != HandleType.None)
            {
                SetSelectHandle(adjustingHandleType);
                isAdjusting = true;
                return true;
            }
            isAdjusting = false;
            return false;
        }

        // try Move
        public virtual bool TryAdjustWhenMouseMove(Point point)
        {
            if (Selected)
            {
                AdjustByHandle(point);
                return true;
            }
            return false;
        }
        public string Name
        {
            get
            {
                return GetShapeName();
            }
        }
        public string Info
        {
            get
            {
                return GetInfo();
            }
        }
        [System.ComponentModel.Browsable(false)]
        public virtual bool Selected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }

        // get shape location
        protected Point GetShapeLocation()
        {
            var shapeLocation = new Point();
            shapeLocation.X = Math.Min(_pointFirst.X, _pointSecond.X);
            shapeLocation.Y = Math.Min(_pointFirst.Y, _pointSecond.Y);
            return shapeLocation;
        }

        // get shape rectangle
        protected System.Drawing.Rectangle GetShapeRectangle()
        {
            var shapeRectangle = new System.Drawing.Rectangle();
            shapeRectangle.Location = GetShapeLocation();
            shapeRectangle.Width = Math.Abs(_pointFirst.X - _pointSecond.X);
            shapeRectangle.Height = Math.Abs(_pointFirst.Y - _pointSecond.Y);
            return shapeRectangle;
        }
        protected ShapeType _type;
        protected bool _isSelected;
        protected Point _pointFirst;
        protected Point _pointSecond;
        protected string _name;
        protected HandleType _selectedHandleType;
        protected List<Handle> _handles;
    }
}
