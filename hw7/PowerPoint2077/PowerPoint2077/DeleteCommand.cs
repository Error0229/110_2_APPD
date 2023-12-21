namespace WindowPowerPoint
{
    public class DeleteCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        public int SlideIndex { get; set; }
        public DeleteCommand(PowerPointModel model, Shape shape, int index)
        {
            _model = model;
            _shape = shape;
            SlideIndex = index;
        }

        // execute command
        public void Execute()
        {
            _model.RemoveShape(_shape, SlideIndex);
        }

        // unexecute command
        public void Unexecute()
        {
            _model.InsertShape(_shape, SlideIndex);
        }
    }
}
