using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowPowerPoint
{
    enum ShapeType
    {
        LINE,
        RECTANGLE
    }
    public class Shape
    {
        Shape (ShapeType type)
        {
            _type = type;
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
        private ShapeType _type;
        private int _top;
        private int _left;
        private int _down;
        private int _right;
        private string _name;
    }
}
