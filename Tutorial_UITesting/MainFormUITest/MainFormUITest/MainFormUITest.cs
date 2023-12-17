using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
/// <summary>
/// Summary description for MainFormUITest
/// </summary>

namespace MainFormUITest
{
    [TestClass()]
    public class MainFormUITest
    {
        private Robot _robot;
        private const string APP_NAME = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        private const string CALCULATOR_TITLE = "Calculator";
        private const string EXPECTED_VALUE = "Display is 444";
        private const string RESULT_CONTROL_NAME = "CalculatorResults";

        /// <summary>
        /// Launches the Calculator
        /// </summary>
        [TestInitialize()]
        public void Initialize()
        {
            _robot = new Robot(APP_NAME, CALCULATOR_TITLE);
        }

        /// <summary>
        /// Closes the launched program
        /// </summary>
        [TestCleanup()]
        public void Cleanup()
        {
            _robot.CleanUp();
        }

        /// <summary>
        /// Runs the script: 123 + 321 =
        /// </summary>
        private void RunScriptAdd()
        {
            _robot.ClickButton("Clear");
            _robot.ClickButton("One");
            _robot.ClickButton("Two");
            _robot.ClickButton("Three");
            _robot.ClickButton("Plus");
            _robot.ClickButton("Three");
            _robot.ClickButton("Two");
            _robot.ClickButton("One");
            _robot.ClickButton("Equals");
        }

        /// <summary>
        /// Tests that the result of 123 + 321 should be 444
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            RunScriptAdd();
            _robot.AssertText(RESULT_CONTROL_NAME, EXPECTED_VALUE);
        }
    }

}