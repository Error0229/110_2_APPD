using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Tutorial
{
    class MainEntry
    {
       static void Main(string[] args)
        {
            Form form = new ElfinForm();
            Application.Run(form);
        }
    }
}
