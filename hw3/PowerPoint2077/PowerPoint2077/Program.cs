using System;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var model = new PowerPointModel();
            var presentationModel = new PowerPointPresentationModel(model);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PowerPoint(presentationModel));
        }
    }
}
