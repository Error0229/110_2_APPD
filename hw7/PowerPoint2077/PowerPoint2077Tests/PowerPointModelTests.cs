using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        // initialize the model
        [TestInitialize()]
        public void Initialize()
        {
            _manager = new Mock<CursorManager>();
            _commandManager = new Mock<CommandManager>();
            _model = new PowerPointModel
            {
                ModelCursorManager = _manager.Object,
                ModelCommandManager = _commandManager.Object
            };
            _state = new Mock<IState>();
            _model.SetState(_state.Object);
            _privateModel = new PrivateObject(_model);
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
            _privateModel = new PrivateObject(_model); Assert.IsNotNull(_model);
            Assert.IsNotNull(_privateModel.GetField("_shapes"));
            Assert.IsNotNull(_privateModel.GetField("_state"));
        }

        // test insert shape
        [TestMethod()]
        public void InsertShapeTest()
        {
            _model.InsertShape(new Circle());
            Assert.AreEqual(1, _model.Shapes.Count);
        }

        // test remove shape
        [TestMethod()]
        public void RemoveShapeTest()
        {
            var shape = new Circle();
            _model.Shapes.Add(shape);
            _model.RemoveShape(shape);
            Assert.AreEqual(0, _model.Shapes.Count);
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
        public void HandleRemoveShapeTest()
        {
            _model.Shapes.Add(new Circle());
            _model.HandleRemoveShape(0);
            _commandManager.Verify(manager => manager.Execute(It.IsAny<DeleteCommand>()), Times.Once());
        }

        // test model set canvas size
        [TestMethod()]
        public void SetCanvasSizeTest()
        {
            var size = new Size(600, 800);
            var shape = new Circle();
            _model.Shapes.Add(shape);
            _model.SetCanvasSize(size);
            Assert.AreEqual(size, _privateModel.GetField("_canvasSize"));
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
            _model._modelChanged += modelChanged.Object;
            _model.MoveShape(shape, offset);
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
            _model.Shapes.Add(new Circle());
            _model.Shapes.Add(new Line());
            _model.Shapes.Add(new Rectangle());
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(graphics);
            _model.DrawShapes(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawCircle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawLine(It.IsAny<System.Drawing.Point>(), It.IsAny<System.Drawing.Point>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawRectangle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
        }

        // test clear selected shape
        [TestMethod()]
        public void ClearSelectedShapeTest()
        {
            var shape1 = new Mock<Shape>();
            shape1.Setup(shape => shape.Selected).Returns(true);
            _model.Shapes.Add(shape1.Object);
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
            _model.Shapes.Add(shape1.Object);
            _model.Shapes.Add(shape2.Object);
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
