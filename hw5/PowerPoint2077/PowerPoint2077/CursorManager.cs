using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
