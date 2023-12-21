using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace WindowPowerPoint
{
    public class Page : ISlide
    {
        public enum Action
        {
            Add,
            Remove,
            Switch
        }
        public int SlideIndex { get; set; }
        public BindingList<Shape> Shapes;
        public Page()
        {
            Shapes = new BindingList<Shape>();
        }
        public void AddShape(Shape shape)
        {
            Shapes.Add(shape);
        }
        public void RemoveShape(Shape shape)
        {
            Shapes.Remove(shape);
        }
        public void RemoveShape(int index)
        {
            Shapes.RemoveAt(index);
        }


    }
}
