using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowPowerPoint
{
    public class PowerPointModel
    {
        public PowerPointModel()
        {
            _shapes = new List<Shape>();
            _factory = new ShapeFactory();
        }
        // insert shape by shape name
        public void InsertShape(string shapeName)
        {
            _shapes.Add(_factory.CreateShape(shapeName));
        }
        // remove shape by index
        public void RemoveShape(int index)
        {
            _shapes.RemoveAt(index);
        }

        private List<Shape> _shapes;
        private ShapeFactory _factory;
        public List<Shape> Shapes
        {
            get
            {
                return _shapes;
            }
        }
    }
}
