using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
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
        const string saveButtonName = "💾";
        const string loadButtonName = "📂";
        const string insertCircleColumnName = "圓形";
        const string insertRectangleColumnName = "矩形";
        const string insertLineColumnName = "線";
        const string insertShapeButtonName = "新增";
        const string addSlideButtonName = "Add Slide";
        const string insertShapeComboBox = "_shapeComboBox";
        const string canvasId = "_canvas";
        const string dataGridId = "_shapeGridView";
        const string slidePanel = "_flowLayoutPanel";
        const string slideButtonClass = "WindowsForms10.BUTTON.app.0.141b42a_r8_ad1";
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
        public void DrawShape(string shapeButtonName, ShapeCoordinate coordinate)
        {
            ClickByElementName(shapeButtonName);
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder
            .AddAction(CreateMoveTo(device, coordinate.Left, coordinate.Top))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, coordinate.Right, coordinate.Bottom))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // draw Suspicious character
        public void DrawSuspicious()
        {
            // Define dimensions and positions for the Among Us character
            // Body dimensions and position
            ShapeCoordinate body = new ShapeCoordinate(50, 80, 150, 200);

            // Backpack dimensions and position
            ShapeCoordinate backpack = new ShapeCoordinate(30, 100, 70, 160);

            // Left Leg dimensions and position
            ShapeCoordinate leftLeg = new ShapeCoordinate(60, 200, 80, 240);

            // Right Leg dimensions and position
            ShapeCoordinate rightLeg = new ShapeCoordinate(120, 200, 140, 240);

            // Glass Visor dimensions and position
            ShapeCoordinate visor = new ShapeCoordinate(95, 110, 160, 140);

            // Head made of lines - approximate a semi-circle or oval
            ShapeCoordinate[] headLines = new ShapeCoordinate[]
            {
                new ShapeCoordinate(50, 80, 75, 60), // Upper left line
                new ShapeCoordinate(75, 60, 125, 60), // Upper line
                new ShapeCoordinate(125, 60, 150, 80), // Upper right line
            };

            // Draw the shapes
            // Draw body
            DrawShape(rectangleButtonName, body);

            // Draw backpack
            DrawShape(rectangleButtonName, backpack);

            // Draw left leg
            DrawShape(rectangleButtonName, leftLeg);

            // Draw right leg
            DrawShape(rectangleButtonName, rightLeg);

            // Draw glass visor
            DrawShape(circleButtonName, visor);

            // Draw head using lines
            foreach (var line in headLines)
            {
                DrawShape(lineButtonName, line);
            }
        }



        // resize shape
        public void ResizeShape(ShapeCoordinate originCoordinate, ShapeCoordinate targetCoordinate)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder.AddAction(CreateMoveTo(device, (originCoordinate.Left + originCoordinate.Right) / 2, (originCoordinate.Top + originCoordinate.Bottom) / 2))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, originCoordinate.Left, originCoordinate.Top))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetCoordinate.Left, targetCoordinate.Top))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, originCoordinate.Right, originCoordinate.Bottom))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetCoordinate.Right, targetCoordinate.Bottom))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // move shape
        public void MoveShape(ShapeCoordinate coordinate, int targetX, int targetY)
        {
            ActionBuilder actionBuilder = new ActionBuilder();
            PointerInputDevice device = new PointerInputDevice(PointerKind.Pen);
            actionBuilder.AddAction(CreateMoveTo(device, (coordinate.Left + coordinate.Right) / 2, (coordinate.Top + coordinate.Bottom) / 2))
            .AddAction(device.CreatePointerDown(PointerButton.LeftMouse))
            .AddAction(CreateMoveTo(device, targetX, targetY))
            .AddAction(device.CreatePointerUp(PointerButton.LeftMouse));
            session.PerformActions(actionBuilder.ToActionSequenceList());
        }

        // get slides
        public IReadOnlyCollection<AppiumWebElement> GetSlides()
        {
            return _slidePanel.FindElementsByAccessibilityId("Slide");
        }

        // add page
        public void AddPage()
        {
            ClickByElementName(addSlideButtonName);
        }

        // delete page
        public void DeletePage()
        {
            Actions actions = new Actions(session);
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
        public string GetCoordinateString(ShapeCoordinate coordinate)
        {
            return $"({coordinate.Left}, {coordinate.Top}), ({coordinate.Right}, {coordinate.Bottom})";
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
            coordinate.Right = GenerateRandomNumber(coordinate.Left + 1, size.Width);
            coordinate.Bottom = GenerateRandomNumber(coordinate.Top + 1, size.Height);
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
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            ClickByElementName(circleButtonName);
            Assert.IsTrue(IsButtonChecked(circleButtonName));
            Assert.IsFalse(IsButtonChecked(rectangleButtonName));
            Assert.IsFalse(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            // why not use elegant Actions(session)? due to the Pure action give a 250ms duration which somehow makes the moving unstable
            DrawShape(circleButtonName, coordinate);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test draw rectangle
        [TestMethod]
        public void TestDrawRectangle()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            ClickByElementName(rectangleButtonName);
            Assert.IsFalse(IsButtonChecked(circleButtonName));
            Assert.IsTrue(IsButtonChecked(rectangleButtonName));
            Assert.IsFalse(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            DrawShape(rectangleButtonName, coordinate);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test draw line
        [TestMethod]
        public void TestDrawLine()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            ClickByElementName(lineButtonName);
            Assert.IsFalse(IsButtonChecked(circleButtonName));
            Assert.IsFalse(IsButtonChecked(rectangleButtonName));
            Assert.IsTrue(IsButtonChecked(lineButtonName));
            Assert.IsFalse(IsButtonChecked(selectButtonName));
            DrawShape(lineButtonName, coordinate);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
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

        // test draw circle undo and redo
        [TestMethod]
        public void TestDrawCircleUndoRedo()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            DrawShape(circleButtonName, coordinate);
            ClickByElementName(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test draw rectangle undo and redo
        [TestMethod]
        public void TestDrawRectangleUndoRedo()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            DrawShape(rectangleButtonName, coordinate);
            ClickByElementName(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test draw line undo and redo
        [TestMethod]
        public void TestDrawLineUndoRedo()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            DrawShape(lineButtonName, coordinate);
            ClickByElementName(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test resize rectangle undo and redo
        [TestMethod]
        public void TestResizeRectangleUndoRedo()
        {
            var originCoordinate = new ShapeCoordinate(100, 100, 300, 300);
            var targetCoordinate = new ShapeCoordinate(50, 50, 400, 400);

            DrawShape(rectangleButtonName, originCoordinate);
            ResizeShape(originCoordinate, targetCoordinate);
            Assert.AreEqual(GetCoordinateString(targetCoordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual($"({targetCoordinate.Top}, {targetCoordinate.Left}), ({originCoordinate.Bottom}, {originCoordinate.Right})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(originCoordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(targetCoordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test resize circle undo and redo
        [TestMethod]
        public void TestResizeCircleUndoRedo()
        {
            var originCoordinate = new ShapeCoordinate(100, 100, 300, 300);
            var targetCoordinate = new ShapeCoordinate(50, 50, 400, 400);

            DrawShape(circleButtonName, originCoordinate);
            ResizeShape(originCoordinate, targetCoordinate);
            Assert.AreEqual(GetCoordinateString(targetCoordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual($"({targetCoordinate.Top}, {targetCoordinate.Left}), ({originCoordinate.Bottom}, {originCoordinate.Right})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(originCoordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(targetCoordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test resize line undo and redo
        [TestMethod]
        public void TestResizeLineUndoRedo()
        {
            var originCoordinate = new ShapeCoordinate(100, 100, 300, 300);
            var targetCoordinate = new ShapeCoordinate(50, 50, 400, 400);

            DrawShape(lineButtonName, originCoordinate);
            ResizeShape(originCoordinate, targetCoordinate);
            Assert.AreEqual(GetCoordinateString(targetCoordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual($"({targetCoordinate.Top}, {targetCoordinate.Left}), ({originCoordinate.Bottom}, {originCoordinate.Right})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(originCoordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(targetCoordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test move rectangle undo and redo
        [TestMethod]
        public void TestMoveRectangleUndoRedo()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            var targetX = 300;
            var targetY = 300;

            DrawShape(rectangleButtonName, coordinate);
            MoveShape(coordinate, targetX, targetY);
            Assert.AreEqual($"({targetX - (coordinate.Right - coordinate.Left) / 2}, {targetY - (coordinate.Bottom - coordinate.Top) / 2}), ({targetX + (coordinate.Right - coordinate.Left) / 2}, {targetY + (coordinate.Bottom - coordinate.Top) / 2})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            Assert.AreEqual($"({targetX - (coordinate.Right - coordinate.Left) / 2}, {targetY - (coordinate.Bottom - coordinate.Top) / 2}), ({targetX + (coordinate.Right - coordinate.Left) / 2}, {targetY + (coordinate.Bottom - coordinate.Top) / 2})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test move circle undo and redo
        [TestMethod]
        public void TestMoveCircleUndoRedo()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            var targetX = 300;
            var targetY = 300;

            DrawShape(circleButtonName, coordinate);
            MoveShape(coordinate, targetX, targetY);
            Assert.AreEqual($"({targetX - (coordinate.Right - coordinate.Left) / 2}, {targetY - (coordinate.Bottom - coordinate.Top) / 2}), ({targetX + (coordinate.Right - coordinate.Left) / 2}, {targetY + (coordinate.Bottom - coordinate.Top) / 2})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            Assert.AreEqual($"({targetX - (coordinate.Right - coordinate.Left) / 2}, {targetY - (coordinate.Bottom - coordinate.Top) / 2}), ({targetX + (coordinate.Right - coordinate.Left) / 2}, {targetY + (coordinate.Bottom - coordinate.Top) / 2})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test move line undo and redo
        [TestMethod]
        public void TestMoveLineUndoRedo()
        {
            var coordinate = new ShapeCoordinate(100, 100, 300, 300);
            var targetX = 300;
            var targetY = 300;

            DrawShape(lineButtonName, coordinate);
            MoveShape(coordinate, targetX, targetY);
            Assert.AreEqual($"({targetX - (coordinate.Right - coordinate.Left) / 2}, {targetY - (coordinate.Bottom - coordinate.Top) / 2}), ({targetX + (coordinate.Right - coordinate.Left) / 2}, {targetY + (coordinate.Bottom - coordinate.Top) / 2})", GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            Assert.AreEqual($"({targetX - (coordinate.Right - coordinate.Left) / 2}, {targetY - (coordinate.Bottom - coordinate.Top) / 2}), ({targetX + (coordinate.Right - coordinate.Left) / 2}, {targetY + (coordinate.Bottom - coordinate.Top) / 2})", GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test insert circle undo and redo
        [TestMethod]
        public void TestInsertCircleUndoRedo()
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

        // test insert rectangle undo and redo
        [TestMethod]
        public void TestInsertRectangleUndoRedo()
        {
            var coordinate = InsertShape(insertRectangleColumnName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }

        // test insert line undo and redo
        [TestMethod]
        public void TestInsertLineUndoRedo()
        {
            var coordinate = InsertShape(insertLineColumnName);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(undoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
            ClickByElementName(redoButtonName);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            DeleteLastInsertShape();
        }


        // test delete circle redo and undo
        [TestMethod]
        public void TestDeleteCircleUndoRedo()
        {
            var coordinate = InsertShape(insertCircleColumnName);
            DeleteLastInsertShape();
            ClickByElementName(undoButtonName);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
        }

        // test delete rectangle redo and undo
        [TestMethod]
        public void TestDeleteRectangleUndoRedo()
        {
            var coordinate = InsertShape(insertRectangleColumnName);
            DeleteLastInsertShape();
            ClickByElementName(undoButtonName);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, GetDataGridViewCellText(0, 1));
            Assert.AreEqual(GetCoordinateString(coordinate), GetDataGridViewCellText(0, 2));
            ClickByElementName(redoButtonName);
            Assert.AreEqual("0", _dataGrid.GetAttribute("Grid.RowCount"));
        }

        // test delete line redo and undo
        [TestMethod]
        public void TestDeleteLineUndoRedo()
        {
            var coordinate = InsertShape(insertLineColumnName);
            DeleteLastInsertShape();
            ClickByElementName(undoButtonName);
            Assert.AreEqual(Constant.LINE_CHINESE, GetDataGridViewCellText(0, 1));
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

        // test save
        [TestMethod]
        public void TestSaveLoad()
        {
            DrawSuspicious();
            ClickByElementName(saveButtonName);
            var dialogBox = session.FindElementByName("SaveDialog");
            dialogBox.FindElementByName("Save").Click();
            ClickByElementName(circleButtonName);
            Assert.IsFalse(IsButtonEnabled(saveButtonName));

        }

        // test load
        [TestMethod]
        public void TestLoad()
        {
            ClickByElementName(loadButtonName);
            var dialogBox = session.FindElementByName("LoadDialog");
            dialogBox.FindElementByName("Load").Click();
            try
            {
                dialogBox.FindElementByName("Cancel").Click();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        // click by element name
        protected void ClickByElementName(string buttonName)
        {
            session.FindElementByName(buttonName).Click();
        }

        // click by element id
        public void ClickByElementID(string buttonID)
        {
            session.FindElementByAccessibilityId(buttonID).Click();
        }

        // is button checked
        protected bool IsButtonChecked(string buttonName)
        {
            string legacyStateValue = session.FindElementByName(buttonName).GetAttribute("LegacyState");
            AccessibleStates stateFlags = (AccessibleStates)Enum.Parse(typeof(AccessibleStates), legacyStateValue);
            return (stateFlags & AccessibleStates.Checked) == AccessibleStates.Checked;
        }

        // is button enabled
        protected bool IsButtonEnabled(string buttonName)
        {
            string legacyStateValue = session.FindElementByName(buttonName).GetAttribute("LegacyState");
            AccessibleStates stateFlags = (AccessibleStates)Enum.Parse(typeof(AccessibleStates), legacyStateValue);
            return (stateFlags & AccessibleStates.Unavailable) != AccessibleStates.Unavailable;
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