﻿using System.ComponentModel;
namespace WindowPowerPoint
{
    public class Page
    {
        public enum Action
        {
            Add,
            Remove,
            Switch
        }
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
