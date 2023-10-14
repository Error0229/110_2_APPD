using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowPowerPoint
{
    public enum ShapeType
    {
        LINE,
        RECTANGLE
    }
    public class Shape
    {
        public Shape(ShapeType type)
        {
            _type = type;
        }
        public Shape()
        {

        }
        // get shape's info
        public string GetInfo()
        {
            return FormatCoordinate(_top, _left) + Constant.COMMA + Constant.SPACE + FormatCoordinate(_down, _right);
        }

        // get shape's name
        public string GetShapeName()
        {
            return _name;
        }

        // set shape coordinate
        public void SetCoordinate(int top, int left, int down, int right)
        {
            _top = top;
            _left = left;
            _down = down;
            _right = right;
        }

        // format coordinate
        private string FormatCoordinate(int first, int second)
        {
            return Constant.LEFT_BRACKET + first + Constant.COMMA + Constant.SPACE + second + Constant.RIGHT_BRACKET;
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
        protected ShapeType _type;
        protected int _top;
        protected int _left;
        protected int _down;
        protected int _right;
        protected string _name;
    }
}
