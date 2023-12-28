using System.ComponentModel;

namespace WindowPowerPoint
{
    public partial class PowerPointPresentationModel : INotifyPropertyChanged, ISlide
    {
        // process upload
        public async void ProcessSave()
        {
            IsSaveEnabled = false;
            IsSaveEnabled = await _model.HandleSave();
        }

        // process download
        public void ProcessLoad()
        {
            _model.HandleLoad();
        }

        // is save enabled
        public bool IsSaveEnabled
        {
            get
            {
                return _isSaveEnabled;
            }
            set
            {
                _isSaveEnabled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsSaveEnabled)));
            }
        }

        // is load enabled
        public bool IsLoadEnabled
        {
            get
            {
                return _isLoadEnabled;
            }
            set
            {
                _isLoadEnabled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsLoadEnabled)));
            }
        }

        private bool _isSaveEnabled;
        private bool _isLoadEnabled;

        // setup cursor manager
        public void SetCursorManager(CursorManager cursorManager)
        {
            _cursorManager = cursorManager;
            _model.ModelCursorManager = cursorManager;
        }
    }
}
