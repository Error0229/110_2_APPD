using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowPowerPoint
{
    public enum ShapeType
    {
        LINE,
        RECTANGLE,
        CIRCLE
    }
    public abstract class Shape
    {
        public Shape(ShapeType type)
        {
            _type = type;
            _pointFirst = new Point();
            _pointSecond = new Point();
        }
        public Shape()
        {

        }
        // get shape's info
        public string GetInfo()
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

        public void AdjustPoints()
        {
            int top, left, button, right;
            top = Math.Min(_pointFirst.X, _pointSecond.X);
            left = Math.Min(_pointFirst.Y, _pointSecond.Y);
            button = Math.Max(_pointFirst.X, _pointSecond.X);
            right = Math.Max(_pointFirst.Y, _pointSecond.Y);
            _pointFirst.X = top;
            _pointFirst.Y = left;
            _pointSecond.X = button;
            _pointSecond.Y = right;
        }

        // format coordinate
        private string FormatCoordinate(int first, int second)
        {
            return Constant.LEFT_BRACKET + first + Constant.COMMA + Constant.SPACE + second + Constant.RIGHT_BRACKET;
        }

        // draw shape
        public abstract void Draw(Graphics graphic);
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
        protected ShapeType _type;

        protected Point _pointFirst;
        protected Point _pointSecond;
        protected string _name;
    }
}
