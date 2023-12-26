using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace WindowPowerPoint
{
    static class Program
    {
        // set process DPI aware
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        // post message to windows
        [DllImport("user32.dll")]
        private static extern bool PostMessage(int hhwnd, uint msg, IntPtr wparam, IntPtr lparam);

        // load keyboard layout
        [DllImport("user32.dll")]
        private static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);

        private static uint WM_INPUTLANGCHANGEREQUEST = 0x0050;
        private static int HWND_BROADCAST = 0xffff;
        private static string en_US = "00000409";
        private static uint KLF_ACTIVATE = 1;

        // change language ☠️
       private static void ChangeLanguage()
       {
            PostMessage(HWND_BROADCAST, WM_INPUTLANGCHANGEREQUEST, IntPtr.Zero, LoadKeyboardLayout(en_US, KLF_ACTIVATE));
       }

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
            ChangeLanguage();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PowerPoint(presentationModel));
        }
    }
}
