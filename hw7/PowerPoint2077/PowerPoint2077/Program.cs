using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace WindowPowerPoint
{
    static class Program
    {
        // set process DPI aware
        [DllImport("user32.dll")]

        private static extern bool SetProcessDPIAware();

        // post message to windows
        [DllImport("user32.dll")]

        private static extern bool PostMessage(int handle, uint message, IntPtr widthParameter, IntPtr lengthParameter);

        // load keyboard layout
        [DllImport("user32.dll")]

        private static extern IntPtr LoadKeyboardLayout(string layoutID, uint flags);

        private const uint INPUT_LANGUAGE_CHANGE_REQUEST = 0x0050;
        private const int HANDLE_BROADCAST = 0xffff;
        private const string ENGLISH_UNITED_STATES = "00000409";
        private const uint KEYBOARD_ACTIVATE = 1;

        // change language ☠️
        private static void ChangeLanguage()
        {
            PostMessage(HANDLE_BROADCAST, INPUT_LANGUAGE_CHANGE_REQUEST, IntPtr.Zero, LoadKeyboardLayout(ENGLISH_UNITED_STATES, KEYBOARD_ACTIVATE));
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
