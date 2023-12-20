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
        const string undoButtonName = "⬅️";
        const string redoButtonName = "➡️";
        const string canvasId = "_canvas";
        const string dataGridId = "_shapeGridView";
        private WindowsElement _canvas;
        private WindowsElement _dataGrid;

        // initialize class
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

        // absolute move
        public Interaction CreateMoveTo(PointerInputDevice device, int x, int y)
        {
            var size = _canvas.Size;
            return device.CreatePointerMove(_canvas, x - size.Width / 2, y - size.Height / 2, TimeSpan.Zero);
        }

        // draw shape
        public void DrawShape(string shapeButtonName, int startX, int startY, int endX, int endY)
        {
            ClickButton(shapeButtonName);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
            .AddAction(CreateMoveTo(device, startX, startY))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, endX, endY))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // resize shape
        public void ResizeShape(int originTop, int originLeft, int originRight, int originBottom, int targetTop, int targetLeft, int targetRight, int targetBottom)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder.AddAction(CreateMoveTo(device, (originLeft + originRight) / 2, (originTop + originBottom) / 2))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, originTop, originLeft))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetTop, targetLeft))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, originRight, originBottom))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetRight, targetBottom))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // move shape
        public void MoveShape(int originTop, int originLeft, int originRight, int originBottom, int targetX, int targetY)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder.AddAction(CreateMoveTo(device, (originLeft + originRight) / 2, (originTop + originBottom) / 2))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetX, targetY))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // test draw circle
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

        // test draw rectangle
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

        // test draw line
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

        // test draw shape undo and redo
        [TestMethod]
        public void TestDrawShapeUndoRedo()
        {
            var startX = 100;
            var startY = 100;
            var endX = 300;
            var endY = 300;
            DrawShape(circleButtonName, startX, startY, endX, endY);
            ClickButton(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickButton(redoButtonName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual($"({startX}, {startY}), ({endX}, {endY})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test resize shape undo and redo
        [TestMethod]
        public void TestResizeUndoRedo()
        {
            var startX = 100;
            var startY = 100;
            var endX = 300;
            var endY = 300;
            var targetTop = 50;
            var targetLeft = 50;
            var targetRight = 400;
            var targetButtom = 400;

            DrawShape(rectangleButtonName, startX, startY, endX, endY);
            ResizeShape(startX, startY, endX, endY, targetTop, targetLeft, targetRight, targetButtom);
            Assert.AreEqual($"({targetTop}, {targetLeft}), ({targetRight}, {targetButtom})", GetDataGridViewCellText(0, 2));
            ClickButton(undoButtonName);
            Assert.AreEqual($"({targetTop}, {targetLeft}), ({endX}, {endY})", GetDataGridViewCellText(0, 2));
            ClickButton(undoButtonName);
            Assert.AreEqual($"({startX}, {startY}), ({endX}, {endY})", GetDataGridViewCellText(0, 2));
            ClickButton(redoButtonName);
            ClickButton(redoButtonName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual($"({targetTop}, {targetLeft}), ({targetRight}, {targetButtom})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test move shape undo and redo
        [TestMethod]
        public void TestMoveUndoRedo()
        {
            var startX = 100;
            var startY = 100;
            var endX = 300;
            var endY = 300;
            var targetX = 300;
            var targetY = 300;

            DrawShape(rectangleButtonName, startX, startY, endX, endY);

            MoveShape(startX, startY, endX, endY, targetX, targetY);
            Assert.AreEqual($"({targetX - (endX - startX) / 2}, {targetY - (endY - startY) / 2}), ({targetX + (endX - startX) / 2}, {targetY + (endY - startY) / 2})", GetDataGridViewCellText(0, 2));
            ClickButton(undoButtonName);
            Assert.AreEqual($"({startX}, {startY}), ({endX}, {endY})", GetDataGridViewCellText(0, 2));
            ClickButton(redoButtonName);
            Assert.AreEqual($"({targetX - (endX - startX) / 2}, {targetY - (endY - startY) / 2}), ({targetX + (endX - startX) / 2}, {targetY + (endY - startY) / 2})", GetDataGridViewCellText(0, 2));
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
        // get data grid view cell text
        protected string GetDataGridViewCellText(int rowIndex, int columnIndex)
        {
            var rowData = _dataGrid.FindElementByName("Row " + rowIndex);
            var columnData = rowData.FindElementByName(DataGridColumnName[columnIndex] + " Row " + rowIndex);
            return columnData.Text;
        }

        // delete last insert shape
        private void DeleteLastInsertShape()
        {
            var rowCount = _dataGrid.FindElementsByName("Row").Count;
            ClickButton(DataGridColumnName[0] + " Row " + rowCount);
        }
    }
}