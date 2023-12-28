namespace WindowPowerPoint
{
    public class DeletePageCommand : ICommand
    {
        readonly PowerPointModel _model;
        Page _page;
        public int SlideIndex 
        { 
            get; set; 
        }
        public DeletePageCommand(PowerPointModel model, int slideIndex, Page page)
        {
            _model = model;
            SlideIndex = slideIndex;
            _page = page;
        }

        // execute
        public void Execute()
        {
            _model.DeletePage(SlideIndex, _page);
            _model.NotifyPageChanged(SlideIndex, Page.Action.Remove);
        }

        // unexecute
        public void Withdraw()
        {
            _model.AddPage(SlideIndex, _page);
            _model.NotifyPageChanged(SlideIndex, Page.Action.Add);
        }
    }
}
