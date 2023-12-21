namespace WindowPowerPoint
{
    public class DrawCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        public int SlideIndex { get; set; }
        public DrawCommand(PowerPointModel model, Shape shape, int index)
        {
            _model = model;
            _shape = shape;
            SlideIndex = index;
        }

        // execute
        public void Execute()
        {
            _model.InsertShape(_shape, SlideIndex);
        }

        // unexecute
        public void Unexecute()
        {
            _model.RemoveShape(_shape, SlideIndex);
        }
    }
}
