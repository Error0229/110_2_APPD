using System.Drawing;

namespace WindowPowerPoint
{
    public class MoveCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        public int SlideIndex 
        { 
            get; set; 
        }
        Point _offset;
        Size _canvasSize;
        // ensure that the first excute will not move the shape
        bool _firstMove;
        public MoveCommand(PowerPointModel model, Shape shape, Point offset, Size canvasSize, int index)
        {
            _model = model;
            _shape = shape;
            _offset = offset;
            _firstMove = true;
            _canvasSize = canvasSize;
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
            Point offset = new Point(_offset.X * _model.CanvasSize.Width / _canvasSize.Width, _offset.Y * _model.CanvasSize.Height / _canvasSize.Height);
            _model.MoveShape(_shape, offset, SlideIndex);
        }

        // unexecute commmand
        public void Withdraw()
        {

            Point offset = new Point(-_offset.X * _model.CanvasSize.Width / _canvasSize.Width, -_offset.Y * _model.CanvasSize.Height / _canvasSize.Height);
            _model.MoveShape(_shape, offset, SlideIndex);
        }
    }
}
