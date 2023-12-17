using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WindowPowerPoint;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace PowerPoint2077Tests
{
    [TestClass]
    public class UITests : PowerPoint2077Session
    {
        const string circleButtonName = "⭕";
        const string rectangleButtonName = "🔲";
        const string lineButtonName = "━";
        const string selectButtonName = "🖰";
        const string canvasId = "_canvas";
        const string dataGridId = "_shapeGridView";
        private WindowsElement _canvas;
        private WindowsElement _dataGrid;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var projectName = "PowerPoint2077";
            string solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var targetAppPath = Path.Combine(solutionPath, projectName, "bin", "Debug", "PowerPoint2077.exe");
            Setup(context, targetAppPath);
            session.SwitchTo().Window(session.CurrentWindowHandle);

        }
        [ClassCleanup]
        public static void ClassCleanUp()
        {
            TearDown();
        }
        [TestInitialize]
        public void TestInitialize()
        {
            _canvas = session.FindElementByAccessibilityId(canvasId);
            _dataGrid = session.FindElementByAccessibilityId(dataGridId);
        }
        public void DrawShape(string shapeButtonName, int startX, int startY, int endX, int endY)
        {
            ClickButton(shapeButtonName);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            var size = _canvas.Size;
            actionBuilder.AddAction(device.CreatePointerMove(_canvas, -size.Width / 2, -size.Height / 2, TimeSpan.Zero));
            actionBuilder.AddAction(device.CreatePointerMove(CoordinateOrigin.Pointer, startX, startY, TimeSpan.Zero));
            actionBuilder.AddAction(device.CreatePointerDown(PointerButton.LeftMouse));
            actionBuilder.AddAction(device.CreatePointerMove(CoordinateOrigin.Pointer, endX - startX, endY - startY, TimeSpan.Zero));
            actionBuilder.AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }
        [TestMethod]
        public void TestDrawCircle()
        {
            var startX = 100;
            var startY = 100;
            var endX = 200;
            var endY = 200;
            ClickButton(circleButtonName);
            Assert.IsTrue(IsButtonChecked(circleButtonName));
            Assert.IsFalse(IsButtonChecked(rectangleButtonName));
            Assert.IsFalse(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            // why not use elegant Actions(session)? due to the Pure action give a 250ms duration which somehow makes the moving unstable
            DrawShape(circleButtonName, startX, startY, endX, endY);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual($"({startX}, {startY}), ({endX}, {endY})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        [TestMethod]
        public void TestDrawRectangle()
        {
            var startX = 100;
            var startY = 100;
            var endX = 200;
            var endY = 200;
            ClickButton(rectangleButtonName);
            Assert.IsFalse(IsButtonChecked(circleButtonName));
            Assert.IsTrue(IsButtonChecked(rectangleButtonName));
            Assert.IsFalse(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            DrawShape(rectangleButtonName, startX, startY, endX, endY);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual($"({startX}, {startY}), ({endX}, {endY})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }
        [TestMethod]
        public void TestDrawLine()
        {
            var startX = 100;
            var startY = 100;
            var endX = 200;
            var endY = 200;
            ClickButton(lineButtonName);
            Assert.IsFalse(IsButtonChecked(circleButtonName));
            Assert.IsFalse(IsButtonChecked(rectangleButtonName));
            Assert.IsTrue(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            DrawShape(lineButtonName, startX, startY, endX, endY);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual($"({startX}, {startY}), ({endX}, {endY})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }
        protected void ClickButton(string buttonName)
        {
            session.FindElementByName(buttonName).Click();
        }

        protected bool IsButtonChecked(string buttonName)
        {
            string legacyStateValue = session.FindElementByName(buttonName).GetAttribute("LegacyState");
            AccessibleStates stateFlags = (AccessibleStates)Enum.Parse(typeof(AccessibleStates), legacyStateValue);
            return (stateFlags & AccessibleStates.Checked) == AccessibleStates.Checked;
        }
        public static Dictionary<int, string> DataGridColumnName = new Dictionary<int, string>()
        {
            { 0, "刪除" },
            { 1, "形狀" },
            { 2, "資訊" }
        };
        protected string GetDataGridViewCellText(int rowIndex, int columnIndex)
        {
            var rowData = _dataGrid.FindElementByName("Row " + rowIndex);
            var columnData = rowData.FindElementByName(DataGridColumnName[columnIndex] + " Row " + rowIndex);
            return columnData.Text;
        }
        private void DeleteLastInsertShape()
        {
            var rowCount = _dataGrid.FindElementsByName("Row").Count;
            ClickButton(DataGridColumnName[0] + " Row " + rowCount);
        }
    }
}