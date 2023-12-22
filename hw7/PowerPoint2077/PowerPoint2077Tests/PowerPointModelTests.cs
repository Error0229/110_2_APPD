﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint.Tests
{
    using WindowPowerPoint;
    [TestClass()]
    public class PowerPointModelTests
    {
        PowerPointModel _model;
        PrivateObject _privateModel;
        Mock<CursorManager> _manager;
        Mock<CommandManager> _commandManager;
        Mock<IState> _state;
        int _slideIndex;
        // initialize the model
        [TestInitialize()]
        public void Initialize()
        {
            _slideIndex = 0;
            _manager = new Mock<CursorManager>();
            _commandManager = new Mock<CommandManager>();
            _model = new PowerPointModel
            {
                ModelCursorManager = _manager.Object,
                ModelCommandManager = _commandManager.Object,
                SlideIndex = _slideIndex
            };
            _state = new Mock<IState>();
            _model.SetState(_state.Object);
            _privateModel = new PrivateObject(_model);
        }

        // test set canvas size
        [TestMethod()]
        public void SetCanvasSizeTest()
        {
            _model.Pages.Clear();
            var size = new Size(600, 800);
            _model.SetCanvasSize(size);
            _model.AddPage(0, new Page());
            _model.SetCanvasSize(size);
            Assert.AreEqual(size, _model.CanvasSize);
        }

        // test CursorManager
        [TestMethod()]
        public void CursorManagerTest()
        {
            Assert.IsNotNull(_model.ModelCursorManager);
        }

        // test the model constructor
        [TestMethod()]
        public void PowerPointModelTest()
        {
            _manager = new Mock<CursorManager>();
            _state = new Mock<IState>();
            _model = new PowerPointModel
            {
                ModelCursorManager = _manager.Object
            };
            _privateModel = new PrivateObject(_model);
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_model.Pages);
            Assert.IsNotNull(_privateModel.GetField("_state"));
        }

        // test insert shape
        [TestMethod()]
        public void InsertShapeTest()
        {
            _model.AddPage(_slideIndex + 1, new Page());
            _slideIndex = 0;
            _model.InsertShape(new Circle(), _slideIndex);
            Assert.AreEqual(1, _model.Pages[_slideIndex].Shapes.Count);
        }

        // test get current shapes
        [TestMethod()]
        public void GetCurrentShapesTest()
        {
            _model.Pages[_slideIndex].Shapes.Add(new Circle());
            _model.Pages[_slideIndex].Shapes.Add(new Line());
            _model.Pages[_slideIndex].Shapes.Add(new Rectangle());
            var shapes = _model.GetCurrentShapes();
            Assert.AreEqual(3, shapes.Count);
        }

        // test handle remove shape with shape
        [TestMethod]
        public void HandleRemoveShapeTest0()
        {
            _model.HandleRemoveShape(new Circle());
            _commandManager.Verify(manager => manager.Execute(It.IsAny<DeleteCommand>()), Times.Once());
        }


        // test resize shape
        [TestMethod()]
        public void ResizeShapeTest()
        {
            var shape = new Circle();
            _model.AddPage(1, new Page());
            _slideIndex = 0;
            _model.Pages[_slideIndex].Shapes.Add(shape);
            _model.ResizeShape(shape, new PointF(100, 100), new PointF(200, 200), 0);
            Assert.AreEqual("(100, 100), (200, 200)", shape.GetInfo());
        }

        // test handle shape resize
        [TestMethod()]
        public void HandleShapeResizeTest()
        {
            var shape0 = new Circle
            {
                Selected = false
            };
            var shape1 = new Rectangle
            {
                Selected = true
            };
            _model.InsertShape(shape0, _slideIndex);
            _model.InsertShape(shape1, _slideIndex);
            _model.HandleShapeResize(new PointF(0, 0), new Point(100, 100));
            _commandManager.Verify(manager => manager.Execute(It.IsAny<ResizeCommand>()), Times.Once());
        }

        // test handle add page
        [TestMethod]
        public void HandleAddPageTest()
        {
            _model.HandleAddPage(_slideIndex);
            _commandManager.Verify(manager => manager.Execute(It.IsAny<AddPageCommand>()), Times.Once());
        }

        // test add page
        [TestMethod]
        public void AddPageTest()
        {
            _model.AddPage(_slideIndex + 1, new Page());
            Assert.AreEqual(2, _model.Pages.Count);
        }

        // test handle delete page
        [TestMethod]
        public void HandleDeletePageTest()
        {
            _model.HandleDeletePage(0);
            _commandManager.Verify(manager => manager.Execute(It.IsAny<DeletePageCommand>()), Times.Once());
        }

        // test delete page
        [TestMethod()]
        public void DeletePageTest()
        {
            Page page = new Page();
            _model.Pages.Add(page);
            _model.SlideIndex = 1;
            _model.DeletePage(0, page);
            Assert.AreEqual(0, _model.SlideIndex);
        }

        // test handle switch page
        [TestMethod()]
        public void HandleSwitchPageTest()
        {
            var pageChanged = new Mock<Action<int, Page.Action>>();
            _model._pageChanged += pageChanged.Object;
            _model.HandleSwitchPage(1);
            pageChanged.Verify(m => m(1, Page.Action.Switch), Times.Once());
        }

        // test remove shape
        [TestMethod()]
        public void RemoveShapeTest()
        {
            var shape = new Circle();
            _model.AddPage(1, new Page());
            _slideIndex = 0;
            _model.Pages[_slideIndex].Shapes.Add(shape);
            _model.RemoveShape(shape, _slideIndex);
            Assert.AreEqual(0, _model.Pages[_slideIndex].Shapes.Count);
        }

        // test model handle insert shape
        [TestMethod()]
        public void HandleInsertShapeTest()
        {
            _model.HandleInsertShape(Constant.LINE_CHINESE);
            _model.HandleInsertShape(Constant.CIRCLE_CHINESE);
            _model.HandleInsertShape(Constant.RECTANGLE_CHINESE);
            _commandManager.Verify(manager => manager.Execute(It.IsAny<AddCommand>()), Times.Exactly(3));
        }

        // test generate random number
        [TestMethod()]
        public void GeneraterandomNumberTest()
        {
            var r1 = PowerPointModel.GenerateRandomNumber(0, 10000);
            var r2 = PowerPointModel.GenerateRandomNumber(-10, 10);
            Assert.IsTrue(r1 >= 0 && r1 <= 10000);
            Assert.IsTrue(r2 >= -10 && r2 <= 10);
        }

        // test model insert shape with point
        [TestMethod()]
        public void HandleInsertShapeWithPointTest()
        {
            _model.HandleInsertShape(Constant.LINE_CHINESE, new Point(10, 10), new Point(50, 50));
            _commandManager.Verify(manager => manager.Execute(It.IsAny<AddCommand>()), Times.Once());
        }

        // test model set point state
        [TestMethod()]
        public void SetStateTest()
        {
            _model.SetState(_state.Object);
            Assert.IsNotNull(_privateModel.GetField("_state"));
        }

        // test model handle remove shape
        [TestMethod()]
        public void HandleRemoveShapeTest1()
        {
            var shape = new Circle();
            _model.Pages[_slideIndex].Shapes.Add(shape);
            _model.HandleRemoveShape(_slideIndex, 0);
            _commandManager.Verify(manager => manager.Execute(It.IsAny<DeleteCommand>()), Times.Once());
        }

        // test model set canvas size
        [TestMethod()]
        public void SetCanvasSizeTest1()
        {
            var size = new Size(600, 800);
            var shape = new Circle();
            _model.Pages[_slideIndex].Shapes.Add(shape);
            _model.SetCanvasSize(size);
            Assert.AreEqual(size, _model.CanvasSize);
        }

        // test drawing state model mouse down
        [TestMethod()]
        public void HandleMouseDownTest()
        {
            var p = new Point(0, 0);
            _model.HandleMouseDown(p);
            _state.Verify(state => state.MouseDown(p), Times.Once());
        }

        // test mouse move
        [TestMethod()]
        public void HandleMouseMoveTest()
        {
            var p = new Point(0, 0);
            _model.HandleMouseMove(p);
            _state.Verify(state => state.MouseMove(p), Times.Once());
        }

        // test mouse up
        [TestMethod()]
        public void HandleMouseUpTest()
        {
            var p = new Point(0, 0);
            _model.HandleMouseUp(p);
            _state.Verify(state => state.MouseUp(p), Times.Once());
        }

        // test key down
        [TestMethod()]
        public void HandleKeyDownTest()
        {
            _model.Pages.Clear();
            _model.HandleKeyDown(Keys.A);
            _model.AddPage(0, new Page());
            _model.HandleKeyDown(Keys.Delete);
            _state.Verify(state => state.KeyDown(Keys.Delete), Times.Once());
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(null);
            _model.Draw(mockAdaptor.Object);
            _state.Verify(state => state.Draw(mockAdaptor.Object), Times.Once());
        }

        // test move shape
        [TestMethod()]
        public void MoveShapeTest()
        {
            var shape = new Circle();
            var offset = new Point(10, 10);
            var modelChanged = new Mock<PowerPointModel.ModelChangedEventHandler>();
            _model.AddPage(1, new Page());
            _slideIndex = 0;
            _model._modelChanged += modelChanged.Object;
            _model.MoveShape(shape, offset, _slideIndex);
            modelChanged.Verify(m => m(_model, EventArgs.Empty), Times.Once());
        }

        // test redo
        [TestMethod()]
        public void RedoTest()
        {
            _model.Redo();
            _commandManager.Verify(manager => manager.Redo(), Times.Once());
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _model.Undo();
            _commandManager.Verify(manager => manager.Undo(), Times.Once());
        }

        // test draw shapes
        [TestMethod()]
        public void DrawShapesTest()
        {
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(graphics);
            _model.Pages.Clear();
            _model.DrawShapes(mockAdaptor.Object);
            _model.AddPage(0, new Page());
            _model.Pages[_slideIndex].Shapes.Add(new Circle());
            _model.Pages[_slideIndex].Shapes.Add(new Line());
            _model.Pages[_slideIndex].Shapes.Add(new Rectangle());
            _model.DrawShapes(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawCircle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawLine(It.IsAny<System.Drawing.Point>(), It.IsAny<System.Drawing.Point>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawRectangle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
        }

        // test clear selected shape
        [TestMethod()]
        public void ClearSelectedShapeTest()
        {
            _model.Pages.Clear();
            _model.ClearSelectedShape();
            _model.AddPage(0, new Page());
            var shape1 = new Mock<Shape>();
            shape1.Setup(shape => shape.Selected).Returns(true);
            _model.Pages[_slideIndex].Shapes.Add(shape1.Object);
            _model.ClearSelectedShape();
            shape1.VerifySet(shape => shape.Selected = false, Times.Once());
        }

        // test hadle move shape
        [TestMethod()]
        public void HandleMoveShapeTest()
        {
            var shape1 = new Mock<Shape>();
            var shape2 = new Mock<Shape>();
            shape1.Setup(shape => shape.Selected).Returns(true);
            shape2.Setup(shape => shape.Selected).Returns(false);
            _model.Pages[_slideIndex].Shapes.Add(shape1.Object);
            _model.Pages[_slideIndex].Shapes.Add(shape2.Object);
            _model.HandleMoveShape(new Point(10, 10));
            _commandManager.Verify(manager => manager.Execute(It.IsAny<MoveCommand>()), Times.Once());
        }


        // test draw hint
        [TestMethod()]
        public void DrawHintTest()
        {
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));
            _model.HandleMouseMove(new Point(70, 70));
            _model.SetCanvasSize(new Size(800, 600));
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(graphics);
            _model.DrawHint(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawCircle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
        }

        // test notify model changed
        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            var modelChanged = new Mock<PowerPointModel.ModelChangedEventHandler>();
            _model._modelChanged += modelChanged.Object;
            _model.NotifyModelChanged(EventArgs.Empty);
            modelChanged.Verify(m => m(_model, EventArgs.Empty), Times.Once());
        }

        // test notify page changed
        [TestMethod()]
        public void NotifyPageChangedTest()
        {
            var pageChanged = new Mock<Action<int, Page.Action>>();
            _model._pageChanged += pageChanged.Object;
            _model.NotifyPageChanged(0, Page.Action.Add);
            pageChanged.Verify(m => m(0, Page.Action.Add), Times.Once());
        }

        // test set hint
        [TestMethod()]
        public void SetHintTest()
        {
            _model.SetHint(ShapeType.CIRCLE);
            Assert.IsInstanceOfType(_privateModel.GetField("_hint"), typeof(Circle));
        }

        // test set hint first point
        [TestMethod()]
        public void SetHintFirstPointTest()
        {
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetHintFirstPoint(new Point(0, 0));
            Assert.AreEqual(new Point(0, 0), (PointF)(new PrivateObject((Circle)_privateModel.GetField("_hint"))).GetField("_pointFirst"));
        }

        // test set hint second point
        [TestMethod()]
        public void SetHintSecondPointTest()
        {
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetHintFirstPoint(new Point(0, 0));
            _model.SetHintSecondPoint(new Point(10, 10));
            Assert.AreEqual(new Point(10, 10), (PointF)(new PrivateObject((Circle)_privateModel.GetField("_hint"))).GetField("_pointSecond"));
        }

        // test add shape with hint
        [TestMethod()]
        public void AddShapeWithHintTest()
        {
            _privateModel.SetField("_hint", new Circle());
            _model.AddShapeWithHint();
            _commandManager.Verify(manager => manager.Execute(It.IsAny<DrawCommand>()), Times.Once());
        }
    }
}
