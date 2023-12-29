using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public class MessageBoxService : IMessageBox
    {
        // show message
        public void Show(string message)
        {
            MessageBox.Show(message);
        }
    }
}
