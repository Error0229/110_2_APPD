using System.Drawing;

namespace WindowPowerPoint
{
    public class ResizeCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        PointF _oldShapeFirstPoint;
        PointF _oldShapeSecondPoint;
        PointF _newShapeFirstPoint;
        PointF _newShapeSecondPoint;
        // ensure that the first excute will not move the shape
        bool _firstResize;
        public int SlideNumber { get; set; }
        public ResizeCommand(PowerPointModel model, Shape shape, PointF firstPoint, PointF secondPoint)
        {
            _model = model;
            _shape = shape;
            _oldShapeFirstPoint = firstPoint;
            _oldShapeSecondPoint = secondPoint;
            _newShapeFirstPoint = shape.GetFirstPoint();
            _newShapeSecondPoint = shape.GetSecondPoint();
            _firstResize = true;
        }

        // execute command
        public void Execute()
        {
            if (_firstResize)
            {
                _firstResize = false;
                return;
            }
            _model.ResizeShape(_shape, _newShapeFirstPoint, _newShapeSecondPoint);
        }

        // unexecute commmand
        public void Unexecute()
        {
            _model.ResizeShape(_shape, _oldShapeFirstPoint, _oldShapeSecondPoint);
        }

    }
}
