using System.Drawing;

namespace WindowPowerPoint
{
    public class MoveCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        public int SlideIndex { get; set; }
        Point _offset;
        // ensure that the first excute will not move the shape
        bool _firstMove;
        public MoveCommand(PowerPointModel model, Shape shape, Point offset, int index)
        {
            _model = model;
            _shape = shape;
            _offset = offset;
            _firstMove = true;
            SlideIndex = index;
        }

        // execute command
        public void Execute()
        {
            if (_firstMove)
            {
                _firstMove = false;
                return;
            }
            _model.MoveShape(_shape, _offset, SlideIndex);
        }

        // unexecute commmand
        public void Unexecute()
        {
            _model.MoveShape(_shape, new Point(-_offset.X, -_offset.Y), SlideIndex);
        }
    }
}
