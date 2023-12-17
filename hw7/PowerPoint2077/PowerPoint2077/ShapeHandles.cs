using System;
using System.Collections.Generic;
using System.Drawing;

namespace WindowPowerPoint
{
    // logic avout shape handle
    public abstract partial class Shape
    {
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
            if (handlePosition.X > _pointSecond.X)
            {
                _selectedHandleType = HandleType.TopRight;
                _pointFirst.X = _pointSecond.X;
                _pointSecond.X = handlePosition.X;
            }
            else
                _pointFirst.X = handlePosition.X;
            if (handlePosition.Y > _pointSecond.Y)
            {
                _selectedHandleType = HandleType.BottomLeft;
                _pointFirst.Y = _pointSecond.Y;
                _pointSecond.Y = handlePosition.Y;
            }
            else
                _pointFirst.Y = handlePosition.Y;
        }

        // adjust by top handle
        public virtual void AdjustByTop(Point handlePosition)
        {
            if (handlePosition.Y > _pointSecond.Y)
            {
                _selectedHandleType = HandleType.Bottom;
                _pointFirst.Y = _pointSecond.Y;
                _pointSecond.Y = handlePosition.Y;
            }
            else
                _pointFirst.Y = handlePosition.Y;
        }

        // adjust by top right handle
        public virtual void AdjustByTopRight(Point handlePosition)
        {
            if (handlePosition.X < _pointFirst.X)
            {
                _selectedHandleType = HandleType.TopLeft;
                _pointSecond.X = _pointFirst.X;
                _pointFirst.X = handlePosition.X;
            }
            else
                _pointSecond.X = handlePosition.X;
            if (handlePosition.Y > _pointSecond.Y)
            {
                _selectedHandleType = HandleType.BottomRight;
                _pointFirst.Y = _pointSecond.Y;
                _pointSecond.Y = handlePosition.Y;
            }
            else
                _pointFirst.Y = handlePosition.Y;
        }

        // adjust by left handle
        public virtual void AdjustByLeft(Point handlePosition)
        {
            if (handlePosition.X > _pointSecond.X)
            {
                _selectedHandleType = HandleType.Right;
                _pointFirst.X = _pointSecond.X;
                _pointSecond.X = handlePosition.X;
            }
            else
                _pointFirst.X = handlePosition.X;
        }

        // adjust by right handle
        public virtual void AdjustByRight(Point handlePosition)
        {
            if (handlePosition.X < _pointFirst.X)
            {
                _selectedHandleType = HandleType.Left;
                _pointSecond.X = _pointFirst.X;
                _pointFirst.X = handlePosition.X;
            }
            else
                _pointSecond.X = handlePosition.X;
        }

        // adjust by button left handle
        public virtual void AdjustByBottomLeft(Point handlePosition)
        {
            if (handlePosition.X > _pointSecond.X)
            {
                _selectedHandleType = HandleType.BottomRight;
                _pointFirst.X = _pointSecond.X;
                _pointSecond.X = handlePosition.X;
            }
            else
                _pointFirst.X = handlePosition.X;
            if (handlePosition.Y < _pointFirst.Y)
            {
                _selectedHandleType = HandleType.TopLeft;
                _pointSecond.Y = _pointFirst.Y;
                _pointFirst.Y = handlePosition.Y;
            }
            else
                _pointSecond.Y = handlePosition.Y;
        }

        // adjust by button handle
        public virtual void AdjustByBottom(Point handlePosition)
        {
            if (handlePosition.Y < _pointFirst.Y)
            {
                _selectedHandleType = HandleType.Top;
                _pointSecond.Y = _pointFirst.Y;
                _pointFirst.Y = handlePosition.Y;
            }
            else
                _pointSecond.Y = handlePosition.Y;
        }

        // adjust by button right handle
        public virtual void AdjustByBottomRight(Point handlePosition)
        {
            if (handlePosition.X < _pointFirst.X)
            {
                _selectedHandleType = HandleType.BottomLeft;
                _pointSecond.X = _pointFirst.X;
                _pointFirst.X = handlePosition.X;
            }
            else
                _pointSecond.X = handlePosition.X;
            if (handlePosition.Y < _pointFirst.Y)
            {
                _selectedHandleType = HandleType.TopRight;
                _pointSecond.Y = _pointFirst.Y;
                _pointFirst.Y = handlePosition.Y;
            }
            else
                _pointSecond.Y = handlePosition.Y;
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
        public virtual bool TryAdjustWhenMouseDown(Point point, out bool isAdjusting)
        {
            var adjustingHandleType = IsCloseToHandle(point);
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
    }
}
