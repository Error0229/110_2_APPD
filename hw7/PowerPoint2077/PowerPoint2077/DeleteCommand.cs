namespace WindowPowerPoint
{
    public class DeleteCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        public DeleteCommand(PowerPointModel model, Shape shape)
        {
            _model = model;
            _shape = shape;
        }

        // execute command
        public void Execute()
        {
            _model.RemoveShape(_shape);
        }

        // unexecute command
        public void Unexecute()
        {
            _model.InsertShape(_shape);
        }
    }
}
