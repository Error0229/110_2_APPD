using System.Windows.Forms;

namespace WindowPowerPoint
{
    public class CursorManager
    {
        private Cursor _currentCursor;
        public Cursor CurrentCursor
        {
            get
            {
                return _currentCursor;
            }
            set
            {
                _currentCursor = value;
            }
        }
        public CursorManager()
        {
            _currentCursor = Cursors.Default;
        }
    }
}
