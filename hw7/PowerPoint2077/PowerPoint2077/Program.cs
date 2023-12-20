using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace WindowPowerPoint
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var model = new PowerPointModel();
            var presentationModel = new PowerPointPresentationModel(model);
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PowerPoint(presentationModel));
        }
    }
}
