﻿namespace WindowPowerPoint
{
    public class DrawCommand : ICommand
    {
        PowerPointModel _model;
        Shape _shape;
        public DrawCommand(PowerPointModel model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        // execute
        public void Execute()
        {
            _model.InsertShape(_shape);
        }

        // unexecute
        public void Unexecute()
        {
            _model.RemoveShape(_shape);
        }
    }
}
