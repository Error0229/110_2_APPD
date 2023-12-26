using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace WindowPowerPoint.Tests
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
        const string insertCircleColumnName = "圓形";
        const string insertRectangleColumnName = "矩形";
        const string insertLineColumnName = "線";
        const string insertShapeButtonName = "新增";
        const string addSlideButtonName = "Add Slide";
        const string insertShapeComboBox = "_shapeComboBox";
        const string canvasId = "_canvas";
        const string dataGridId = "_shapeGridView";
        const string slidePanel = "_flowLayoutPanel";
        const string slideButtonClass = "WindowsForms10.BUTTON.app.0.17ad52b_r8_ad1";
        // WindowsForms10.BUTTON.app.0.2c908d5_r9_ad1
        private WindowsElement _canvas;
        private WindowsElement _dataGrid;
        private WindowsElement _slidePanel;
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
            _slidePanel = session.FindElementByAccessibilityId(slidePanel);
        }

        // absolute move
        public Interaction CreateMoveTo(PointerInputDevice device, int x, int y)
        {
            var size = _canvas.Size;
            return device.CreatePointerMove(_canvas, x - size.Width / 2, y - size.Height / 2, TimeSpan.Zero);
        }

        // draw shape
        public void DrawShape(string shapeButtonName, ShapeCoordinate coor)
        {
            ClickByElementName(shapeButtonName);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
            .AddAction(CreateMoveTo(device, coor.Left, coor.Top))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, coor.Right, coor.Bottom))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // resize shape
        public void ResizeShape(ShapeCoordinate originCoor, ShapeCoordinate targetCoor)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder.AddAction(CreateMoveTo(device, (originCoor.Left + originCoor.Right) / 2, (originCoor.Top + originCoor.Bottom) / 2))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, originCoor.Left, originCoor.Top))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetCoor.Left, targetCoor.Top))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, originCoor.Right, originCoor.Bottom))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetCoor.Right, targetCoor.Bottom))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // move shape
        public void MoveShape(ShapeCoordinate coor, int targetX, int targetY)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder.AddAction(CreateMoveTo(device, (coor.Left + coor.Right) / 2, (coor.Top + coor.Bottom) / 2))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetX, targetY))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // get slides
        public ReadOnlyCollection<AppiumWebElement> GetSlides()
        {
            return _slidePanel.FindElementsByClassName(slideButtonClass);
        }

        // add page
        public void AddPage()
        {
            ClickByElementName(addSlideButtonName);
        }

        // delete page
        public void DeletePage()
        {
            // 建立 Actions 類的實例
            Actions actions = new Actions(session);
            // 按下刪除鍵
            actions.SendKeys(OpenQA.Selenium.Keys.Delete).Perform();
        }


        public struct ShapeCoordinate
        {
            public int Top { get; set; }
            public int Left { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }

            public ShapeCoordinate(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }
        public string GetCoordinateString(ShapeCoordinate coor)
        {
            return $"({coor.Left}, {coor.Top}), ({coor.Right}, {coor.Bottom})";
        }

        public ShapeCoordinate InsertShape(string shapeName)
        {
            ClickByElementID(insertShapeComboBox);
            ClickByElementName(shapeName);
            ClickByElementName(insertShapeButtonName);
            var dialogBox = session.FindElementByAccessibilityId("Dialog");
            var textBox1 = dialogBox.FindElementByAccessibilityId("textBox1");
            var textBox2 = dialogBox.FindElementByAccessibilityId("textBox2");
            var textBox3 = dialogBox.FindElementByAccessibilityId("textBox3");
            var textBox4 = dialogBox.FindElementByAccessibilityId("textBox4");
            var buttonOK = dialogBox.FindElementByAccessibilityId("buttonOK");
            var size = _canvas.Size;
            var coordinate = new ShapeCoordinate
            {
                Left = GenerateRandomNumber(0, size.Width),
                Top = GenerateRandomNumber(0, size.Height)
            };
            coordinate.Right = GenerateRandomNumber(coordinate.Left, size.Width);
            coordinate.Bottom = GenerateRandomNumber(coordinate.Top, size.Height);
            textBox1.SendKeys(coordinate.Left.ToString());
            textBox2.SendKeys(coordinate.Top.ToString());
            textBox3.SendKeys(coordinate.Right.ToString());
            textBox4.SendKeys(coordinate.Bottom.ToString());
            buttonOK.Click();
            return coordinate;
        }

        // random shape
        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[Constant.INTEGER32_BYTES];
                rng.GetBytes(randomNumber);
                int generatedValue = BitConverter.ToInt32(randomNumber, 0);
                return Math.Abs(generatedValue % (maxValue - minValue + 1)) + minValue;
            }
        }

        // resize window 
        public void ResizeWindow(int width, int height)
        {
            session.Manage().Window.Size = new System.Drawing.Size(width, height);
        }

        // test draw circle
        [TestMethod]
        public void TestDrawCircle()
        {
            var coor = new ShapeCoordinate(100, 100, 300, 300);
            ClickByElementName(circleButtonName);
            Assert.IsTrue(IsButtonChecked(circleButtonName));
            Assert.IsFalse(IsButtonChecked(rectangleButtonName));
            Assert.IsFalse(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            // why not use elegant Actions(session)? due to the Pure action give a 250ms duration which somehow makes the moving unstable
            DrawShape(circleButtonName, coor);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coor), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test draw rectangle
        [TestMethod]
        public void TestDrawRectangle()
        {
            var coor = new ShapeCoordinate(100, 100, 300, 300);
            ClickByElementName(rectangleButtonName);
            Assert.IsFalse(IsButtonChecked(circleButtonName));
            Assert.IsTrue(IsButtonChecked(rectangleButtonName));
            Assert.IsFalse(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            DrawShape(rectangleButtonName, coor);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coor), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test draw line
        [TestMethod]
        public void TestDrawLine()
        {
            var coor = new ShapeCoordinate(100, 100, 300, 300);
            ClickByElementName(lineButtonName);
            Assert.IsFalse(IsButtonChecked(circleButtonName));
            Assert.IsFalse(IsButtonChecked(rectangleButtonName));
            Assert.IsTrue(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            DrawShape(lineButtonName, coor);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coor), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test insert circle
        [TestMethod]
        public void TestInsertCircle()
        {
            var coordinate = InsertShape(insertCircleColumnName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test insert rectangle
        [TestMethod]
        public void TestInsertRectangle()
        {
            var coordinate = InsertShape(insertRectangleColumnName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test insert line
        [TestMethod]
        public void TestInsertLine()
        {
            var coordinate = InsertShape(insertLineColumnName);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test draw shape undo and redo
        [TestMethod]
        public void TestDrawShapeUndoRedo()
        {
            var coor = new ShapeCoordinate(100, 100, 300, 300);
            DrawShape(circleButtonName, coor);
            ClickByElementName(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coor), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test resize shape undo and redo
        [TestMethod]
        public void TestResizeUndoRedo()
        {
            var originCoor = new ShapeCoordinate(100, 100, 300, 300);
            var targetCoor = new ShapeCoordinate(50, 50, 400, 400);

            DrawShape(rectangleButtonName, originCoor);
            ResizeShape(originCoor, targetCoor);
            Assert.AreEqual(GetCoordinateString(targetCoor), GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual($"({targetCoor.Top}, {targetCoor.Left}), ({originCoor.Bottom}, {originCoor.Right})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(originCoor), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(targetCoor), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test move shape undo and redo
        [TestMethod]
        public void TestMoveUndoRedo()
        {
            var coor = new ShapeCoordinate(100, 100, 300, 300);
            var targetX = 300;
            var targetY = 300;

            DrawShape(rectangleButtonName, coor);
            MoveShape(coor, targetX, targetY);
            Assert.AreEqual($"({targetX - (coor.Right - coor.Left) / 2}, {targetY - (coor.Bottom - coor.Top) / 2}), ({targetX + (coor.Right - coor.Left) / 2}, {targetY + (coor.Bottom - coor.Top) / 2})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(coor), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            Assert.AreEqual($"({targetX - (coor.Right - coor.Left) / 2}, {targetY - (coor.Bottom - coor.Top) / 2}), ({targetX + (coor.Right - coor.Left) / 2}, {targetY + (coor.Bottom - coor.Top) / 2})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test insert shape undo and redo
        [TestMethod]
        public void TestInsertShapeUndoRedo()
        {
            var coordinate = InsertShape(insertCircleColumnName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test delete shape page redo and undo
        [TestMethod]
        public void TestDeleteShapeUndoRedo()
        {
            var coordinate = InsertShape(insertCircleColumnName);
            DeleteLastInsertShape();
            ClickByElementName(undoButtonName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
        }

        // test add page redo and undo
        [TestMethod]
        public void TestAddPageUndoRedo()
        {
            AddPage();
            ClickByElementName(undoButtonName);
            Assert.AreEqual(1, GetSlides().Count);
            ClickByElementName(redoButtonName);
            Assert.AreEqual(2, GetSlides().Count);
            DeletePage();
        }

        // test delete page redo and undo
        [TestMethod]
        public void TestDeletePageUndoRedo()
        {
            DeletePage();
            ClickByElementName(undoButtonName);
            Assert.AreEqual(1, GetSlides().Count);
            ClickByElementName(redoButtonName);
            Assert.AreEqual(0, GetSlides().Count);
            AddPage();
        }

        // test add page
        [TestMethod]
        public void TestAddPage()
        {
            AddPage();
            Assert.AreEqual(2, GetSlides().Count);
            DeletePage();
        }

        // test delete page
        [TestMethod]
        public void TestDeletePage()
        {
            DeletePage();
            Assert.AreEqual(0, GetSlides().Count);
            AddPage();
        }

        // test move shape
        [TestMethod]
        public void TestMoveShape()
        {
            var coor = new ShapeCoordinate(100, 100, 300, 300);
            var targetX = 300;
            var targetY = 300;

            DrawShape(rectangleButtonName, coor);
            MoveShape(coor, targetX, targetY);
            Assert.AreEqual($"({targetX - (coor.Right - coor.Left) / 2}, {targetY - (coor.Bottom - coor.Top) / 2}), ({targetX + (coor.Right - coor.Left) / 2}, {targetY + (coor.Bottom - coor.Top) / 2})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }
        // test resize window
        [TestMethod]
        public void TestResizeWindow()
        {
            var width = 800;
            var height = 600;
            var slides = GetSlides();
            Assert.AreEqual(1, slides.Count);
            ResizeWindow(width, height);
            Assert.IsTrue(Math.Abs(_canvas.Size.Width / _canvas.Size.Height - 16 / 9) < 0.01);
            foreach (var slide in slides)
            {
                Assert.IsTrue(Math.Abs(slide.Size.Width / slide.Size.Height - 16 / 9) < 0.01);
            }
        }
        protected void ClickByElementName(string buttonName)
        {
            session.FindElementByName(buttonName).Click();
        }

        public void ClickByElementID(string buttonID)
        {
            session.FindElementByAccessibilityId(buttonID).Click();
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
            ClickByElementName(DataGridColumnName[0] + " Row " + rowCount);
        }
    }
}