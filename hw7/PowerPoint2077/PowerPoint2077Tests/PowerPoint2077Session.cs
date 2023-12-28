using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;

namespace WindowPowerPoint.Tests
{
    public class PowerPoint2077Session
    {
        protected const string WINDOWS_APPLICATION_DRIVER_URL = "http://127.0.0.1:4723";
        protected static WindowsDriver<WindowsElement> _session;

        // setup the session
        public static void Initialize(TestContext context, string targetAppPath)
        {
            if (_session == null)
            {
                var projectName = Constant.PROJECT_NAME;
                string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constant.PARENT_FOLDER_PATH));
                var options = new AppiumOptions();
                options.AddAdditionalCapability(Constant.APPLICATION, targetAppPath);
                options.AddAdditionalCapability(Constant.DEVICE_NAME, Constant.WINDOWS_PERSONAL_COMPUTER);
                options.AddAdditionalCapability(Constant.APPLICATION_WORKING_DIRECTORY, Path.Combine(solutionPath, projectName, Constant.HELP, Constant.ME));
                _session = new WindowsDriver<WindowsElement>(new Uri(WINDOWS_APPLICATION_DRIVER_URL), options);
                Assert.IsNotNull(_session);
                _session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1 + Constant.POINT_FIVE);
                _session.Manage().Window.Maximize();
            }
        }

        // teardown the session
        public static void TearDown()
        {
            // Close the application and delete the session
            if (_session != null)
            {
                CloseApp();
                _session.Quit();
                _session = null;
            }
        }

        // clear the application
        private static void CloseApp()
        {
            _session.Close();
        }
    }
}
