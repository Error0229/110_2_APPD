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
        public Shape (ShapeType type)
        {
            _type = type;
        }
        public Shape()
        {

        }
        // get shape's info
        public string GetInfo()
        {
            return "(" + _top + ", " + _left + "), (" + _right + ", " + _down + ")";
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
        protected ShapeType _type;
        protected int _top;
        protected int _left;
        protected int _down;
        protected int _right;
        protected string _name;
    }
}
