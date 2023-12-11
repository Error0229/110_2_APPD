using System;
using System.Collections.Generic;
using System.Drawing;

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
    public abstract partial class Shape
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

        // set canvas size
        public virtual void SetCanvasSize(Size size)
        {
            if (_canvasSize != Size.Empty)
            {
                _pointFirst.X = _pointFirst.X * size.Width / _canvasSize.Width;
                _pointFirst.Y = _pointFirst.Y * size.Height / _canvasSize.Height;
                _pointSecond.X = _pointSecond.X * size.Width / _canvasSize.Width;
                _pointSecond.Y = _pointSecond.Y * size.Height / _canvasSize.Height;
            }
            _canvasSize = size;
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
            _pointFirst.X += offset.X;
            _pointFirst.Y += offset.Y;
            _pointSecond.X += offset.X;
            _pointSecond.Y += offset.Y;
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
        protected string FormatCoordinate(float first, float second)
        {
            return Constant.LEFT_BRACKET + (int)first + Constant.COMMA + Constant.SPACE + (int)second + Constant.RIGHT_BRACKET;
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
            shapeLocation.X = (int)Math.Min(_pointFirst.X, _pointSecond.X);
            shapeLocation.Y = (int)Math.Min(_pointFirst.Y, _pointSecond.Y);
            return shapeLocation;
        }

        // get shape rectangle
        protected System.Drawing.Rectangle GetShapeRectangle()
        {
            var shapeRectangle = new System.Drawing.Rectangle();
            shapeRectangle.Location = GetShapeLocation();
            shapeRectangle.Width = (int)Math.Abs(_pointFirst.X - _pointSecond.X);
            shapeRectangle.Height = (int)Math.Abs(_pointFirst.Y - _pointSecond.Y);
            return shapeRectangle;
        }

        // get point by pointF
        protected Point GetPoint(PointF point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        protected ShapeType _type;
        protected bool _isSelected;
        protected PointF _pointFirst;
        protected PointF _pointSecond;
        protected string _name;
        protected HandleType _selectedHandleType;
        protected List<Handle> _handles;
        protected Size _canvasSize;
    }
}
