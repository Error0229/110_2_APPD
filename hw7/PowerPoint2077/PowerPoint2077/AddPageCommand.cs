namespace WindowPowerPoint
{
    public class AddPageCommand : ICommand
    {
        readonly PowerPointModel _model;
        Page _page;
        public int SlideIndex { get; set; }
        public AddPageCommand(PowerPointModel model,int slideIndex, Page page)
        {
            _page = page;
            _model = model;
            SlideIndex = slideIndex;
        }

        // executes
        public void Execute()
        {
            _model.AddPage(SlideIndex, _page);
            _model.NotifyPageChanged(SlideIndex, Page.Action.Add);
        }

        // unexecute
        public void Unexecute()
        {
            _model.DeletePage(SlideIndex, _page);
            _model.NotifyPageChanged(SlideIndex, Page.Action.Remove);
        }
    }
}
