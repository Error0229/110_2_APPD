namespace WindowPowerPoint
{
    public class AddCommand : ICommand
    {
        readonly PowerPointModel _model;
        readonly Shape _shape;
        public int SlideIndex 
        { 
            get; set; 
        }

        // constructor
        public AddCommand(PowerPointModel model, Shape shape, int currentIndex)
        {
            SlideIndex = currentIndex;
            _model = model;
            _shape = shape;
        }

        // execute
        public void Execute()
        {
            _model.InsertShape(_shape, SlideIndex);
        }

        // unexecute
        public void Withdraw()
        {
            _model.RemoveShape(_shape, SlideIndex);
        }
    }
}
