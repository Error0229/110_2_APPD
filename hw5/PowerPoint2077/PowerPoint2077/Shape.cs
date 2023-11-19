using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
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
        TOP_LEFT,
        TOP,
        TOP_RIGHT,
        LEFT,
        CENTER,
        RIGHT,
        BUTTON_LEFT,
        BUTTON,
        BUTTON_RIGHT,
        NONE
    }
    public struct Handle
    {
        public Point Position;
        public HandleType Type;
    }
    public abstract class Shape
    {
        public Shape(ShapeType type) : this()
        {
            _type = type;
        }
        public Shape()
        {
            _pointFirst = new Point();
            _pointSecond = new Point();
            _name = string.Empty;
            _selectedHandleType = HandleType.NONE;
            _handles = new List<Handle>();
        }
        // get shape's info
        public virtual string GetInfo()
        {
            return FormatCoordinate(_pointFirst.X, _pointFirst.Y) + Constant.COMMA + Constant.SPACE + FormatCoordinate(_pointSecond.X, _pointSecond.Y);
        }

        // get shape's name
        public string GetShapeName()
        {
            return _name;
        }

        // set first point
        public void SetFirstPoint(Point point)
        {
            _pointFirst = point;
        }

        // set second point
        public void SetSecondPoint(Point point)
        {
            _pointSecond = point;
        }

        // move first point
        public void Move(Point offset)
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
        public bool IsInShape(Point point)
        {
            return GetShapeRectangle().Contains(point);
        }

        // format coordinate
        protected string FormatCoordinate(int first, int second)
        {
            return Constant.LEFT_BRACKET + first + Constant.COMMA + Constant.SPACE + second + Constant.RIGHT_BRACKET;
        }

        // distance bewtween two point
        protected double DistanceOf(Point pointFirst, Point pointSecond)
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
        public Handle IsCloseToHandle(Point cursor)
        {
            foreach (var handle in _handles)
            {
                if (DistanceOf(cursor, handle.Position) <= Constant.DISTANCE_THRESHOLD)
                {
                    return handle;
                }
            }
            return new Handle { Type = HandleType.NONE };
        }

        // select handle
        public void SelectHandle(HandleType handle)
        {
            _selectedHandleType = handle;
        }

        // adjust by handle
        public virtual void AdjustByHandle(Point handlePosition)
        {
            switch (_selectedHandleType)
            {
                case HandleType.TOP_LEFT:
                    AdjustByTopLeft(handlePosition);
                    break;
                case HandleType.TOP:
                    AdjustByTop(handlePosition);
                    break;
                case HandleType.TOP_RIGHT:
                    AdjustByTopRight(handlePosition);
                    break;
                case HandleType.LEFT:
                    AdjustByLeft(handlePosition);
                    break;
                case HandleType.RIGHT:
                    AdjustByRight(handlePosition);
                    break;
                case HandleType.BUTTON_LEFT:
                    AdjustByButtonLeft(handlePosition);
                    break;
                case HandleType.BUTTON:
                    AdjustByButton(handlePosition);
                    break;
                case HandleType.BUTTON_RIGHT:
                    AdjustByButtonRight(handlePosition);
                    break;
            }
        }

        // adjust by top left handle
        public virtual void AdjustByTopLeft(Point handlePosition)
        {
            _pointFirst.X = handlePosition.X;
            _pointFirst.Y = handlePosition.Y;
            if (_pointFirst.X >= _pointSecond.X)
            {
                _selectedHandleType = HandleType.TOP_RIGHT;
            }
            else if (_pointFirst.Y >= _pointSecond.Y)
            {
                _selectedHandleType = HandleType.BUTTON_LEFT;
            }
        }

        // adjust by top handle
        public virtual void AdjustByTop(Point handlePosition)
        {
            _pointFirst.Y = handlePosition.Y;
            if (_pointFirst.Y >= _pointSecond.Y)
            {
                _selectedHandleType = HandleType.BUTTON;
            }
        }

        // adjust by top right handle
        public virtual void AdjustByTopRight(Point handlePosition)
        {

            _pointSecond.X = handlePosition.X;
            _pointFirst.Y = handlePosition.Y;
            if (_pointFirst.X >= _pointSecond.X)
            {
                _selectedHandleType = HandleType.TOP_LEFT;
            }
            else if (_pointFirst.Y >= _pointSecond.Y)
            {
                _selectedHandleType = HandleType.BUTTON_RIGHT;
            }
        }

        // adjust by left handle
        public virtual void AdjustByLeft(Point handlePosition)
        {

            _pointFirst.X = handlePosition.X;
            if (_pointFirst.X >= _pointSecond.X)
            {
                _selectedHandleType = HandleType.RIGHT;
            }
        }

        // adjust by right handle
        public virtual void AdjustByRight(Point handlePosition)
        {

            _pointSecond.X = handlePosition.X;
            if (_pointFirst.X >= _pointSecond.X)
            {
                _selectedHandleType = HandleType.LEFT;
            }
        }

        // adjust by button left handle
        public virtual void AdjustByButtonLeft(Point handlePosition)
        {

            _pointFirst.X = handlePosition.X;
            _pointSecond.Y = handlePosition.Y;
            if (_pointFirst.X >= _pointSecond.X)
            {
                _selectedHandleType = HandleType.BUTTON_RIGHT;
            }
            else if (_pointFirst.Y >= _pointSecond.Y)
            {
                _selectedHandleType = HandleType.TOP_LEFT;
            }
        }

        // adjust by button handle
        public virtual void AdjustByButton(Point handlePosition)
        {

            _pointSecond.Y = handlePosition.Y;
            if (_pointFirst.Y >= _pointSecond.Y)
            {
                _selectedHandleType = HandleType.TOP;
            }
        }

        // adjust by button right handle
        public virtual void AdjustByButtonRight(Point handlePosition)
        {

            _pointSecond.X = handlePosition.X;
            _pointSecond.Y = handlePosition.Y;
            if (_pointFirst.X >= _pointSecond.X)
            {
                _selectedHandleType = HandleType.BUTTON_LEFT;
            }
            else if (_pointFirst.Y >= _pointSecond.Y)
            {
                _selectedHandleType = HandleType.TOP_RIGHT;
            }
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
        public bool Selected
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
