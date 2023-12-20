namespace WindowPowerPoint
{
    public class AddCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        public int SlideNumber { get; set; }
        public AddCommand(PowerPointModel model, Shape shape)
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
