using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace WindowPowerPoint.Tests
{
    public class PowerPoint2077Session
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static WindowsDriver<WindowsElement> session;

        public static void Setup(TestContext context, string targetAppPath)
        {
            // Launch a new instance of PowerPoint2077 application if it is not yet running
            if (session == null)
            {
                var options = new AppiumOptions();
                options.AddAdditionalCapability("app", targetAppPath);
                options.AddAdditionalCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);
                Assert.IsNotNull(session);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                // Maximize window
                session.Manage().Window.Maximize();
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                CloseApp();
                session.Quit();
                session = null;
            }
        }

        private static void CloseApp()
        {
            try
            {
                session.Close();
            }
            catch { }
        }
    }
}
