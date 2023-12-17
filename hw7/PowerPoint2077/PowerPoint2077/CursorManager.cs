using System.Windows.Forms;

namespace WindowPowerPoint
{
    public class CursorManager
    {
        public Cursor CurrentCursor
        {
            get; set;
        }
        public CursorManager()
        {
            CurrentCursor = Cursors.Default;
        }
    }
}
