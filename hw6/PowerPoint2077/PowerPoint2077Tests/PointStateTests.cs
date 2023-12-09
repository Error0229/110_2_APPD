using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.ComponentModel;
using System.Drawing;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PointStateTests
    {
        Mock<PowerPointModel> _model;
        PointState _state;
        PrivateObject _privateState;

        // set up
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            // set up _model's public properties
            _model.SetupGet(model => model.ModelCursorManager).Returns(new CursorManager());
            _model.SetupGet(model => model.Shapes).Returns(new BindingList<Shape>());
            _state = new PointState(_model.Object);
            _privateState = new PrivateObject(_state);
        }

        // test constructor
        [TestMethod()]
        public void PointStateTest()
        {
            _model = new Mock<PowerPointModel>();
            _state = new PointState(_model.Object);
            _privateState = new PrivateObject(_state);
            Assert.IsNotNull(_privateState.GetField("_model"));
        }

        // test mouse down
        [TestMethod()]
        public void MouseDownTest()
        {
            var p = new Point(0, 0);
            _privateState.SetField("_isAdjusting", true);
            _state.MouseDown(p);
            _privateState.SetField("_isAdjusting", false);
            _state.MouseDown(p);

            _model.Verify(model => model.ClearSelectedShape(), Times.Exactly(2));
            _model.Verify(model => model.NotifyModelChanged(EventArgs.Empty), Times.Once());
        }

        // test mouse down for adjust
        [TestMethod()]
        public void MouseDownForAdjustTest()
        {
            // insert shape into _model.Shapes
            var shapeMock0 = new Mock<Shape>();
            var shapeMock = new Mock<Shape>();
            shapeMock0.Setup(shape => shape.TryAdjustWhenMouseDown(It.IsAny<Point>(), out It.Ref<bool>.IsAny, out It.Ref<HandleType>.IsAny)).Returns(false);
            shapeMock.Setup(shape => shape.TryAdjustWhenMouseDown(It.IsAny<Point>(), out It.Ref<bool>.IsAny, out It.Ref<HandleType>.IsAny)).Returns(true);
            _model.Object.Shapes.Add(shapeMock0.Object);
            _model.Object.Shapes.Add(shapeMock.Object);
            _privateState.Invoke("MouseDownForAdjust", new Point(0, 0));
            shapeMock.Verify(shape => shape.TryAdjustWhenMouseDown(It.IsAny<Point>(), out It.Ref<bool>.IsAny, out It.Ref<HandleType>.IsAny), Times.Once());
        }

        // test mouse down for move
        [TestMethod()]
        public void MouseDownForMoveTest()
        {
            // insert shape into _model.Shapes
            var shapeMock = new Mock<Shape>();
            var shapeMock2 = new Mock<Shape>();
            shapeMock.Setup(shape => shape.IsInShape(It.IsAny<Point>())).Returns(false);
            shapeMock2.Setup(shape => shape.IsInShape(It.IsAny<Point>())).Returns(true);
            _model.Object.Shapes.Add(shapeMock.Object);
            _model.Object.Shapes.Add(shapeMock2.Object);
            _privateState.Invoke("MouseDownForMove", new Point(0, 0));
            shapeMock.Verify(shape => shape.IsInShape(It.IsAny<Point>()), Times.Once());
            shapeMock2.Verify(shape => shape.IsInShape(It.IsAny<Point>()), Times.Once());
        }

        // test mouse move
        [TestMethod()]
        public void MouseMoveTest()
        {
            _state.MouseMove(new Point(0, 0));
            _privateState.SetField("_isMoving", true);
            _state.MouseMove(new Point(0, 0));
            _privateState.SetField("_isAdjusting", true);
            _state.MouseMove(new Point(0, 0));
            Assert.IsTrue((bool)_privateState.GetField("_isMoving"));
            Assert.IsTrue((bool)_privateState.GetField("_isAdjusting"));
        }

        // test mouse move while adjusting
        [TestMethod()]
        public void MouseMoveWhileAdjustingTest()
        {
            // insert shape into _model.Shapes
            var shapeMock = new Mock<Shape>();
            var shapeMock2 = new Mock<Shape>();
            shapeMock.Setup(shape => shape.TryAdjustWhenMouseMove(It.IsAny<Point>())).Returns(false);
            shapeMock2.Setup(shape => shape.TryAdjustWhenMouseMove(It.IsAny<Point>())).Returns(true);
            _model.Object.Shapes.Add(shapeMock.Object);
            _model.Object.Shapes.Add(shapeMock2.Object);
            _privateState.Invoke("MouseMoveWhileAdjusting", new Point(0, 0));
            shapeMock.Verify(shape => shape.TryAdjustWhenMouseMove(It.IsAny<Point>()), Times.Once());
            shapeMock2.Verify(shape => shape.TryAdjustWhenMouseMove(It.IsAny<Point>()), Times.Once());
        }

        // test mouse move while moving
        [TestMethod()]
        public void MouseMoveWhileMovingTest()
        {
            // insert shape into _model.Shapes
            var shapeMock0 = new Mock<Shape>();
            var shapeMock = new Mock<Shape>();
            shapeMock0.Setup(shape => shape.Selected).Returns(false);
            shapeMock.Setup(shape => shape.Move(It.IsAny<Point>()));
            shapeMock.Setup(shape => shape.Selected).Returns(true);
            _model.Object.Shapes.Add(shapeMock0.Object);
            _model.Object.Shapes.Add(shapeMock.Object);
            _privateState.Invoke("MouseMoveWhileMoving", new Point(0, 0));
            _state.MouseMove(new Point(0, 0));
            shapeMock.Verify(shape => shape.Move(It.IsAny<Point>()), Times.Once());
        }

        // test mouse move while idle
        [TestMethod()]
        public void MouseMoveWhileIdleTest()
        {
            // insert shape into _model.Shapes
            var shapeMock0 = new Mock<Shape>();
            var shapeMock = new Mock<Shape>();
            shapeMock0.Setup(shape => shape.Selected).Returns(false);
            shapeMock.Setup(shape => shape.IsCloseToHandle(It.IsAny<Point>())).Returns(HandleType.None);
            shapeMock.Setup(shape => shape.Selected).Returns(true);
            _model.Object.Shapes.Add(shapeMock0.Object);
            _model.Object.Shapes.Add(shapeMock.Object);
            _privateState.Invoke("MouseMoveWhileIdle", new Point(0, 0));
            shapeMock.Verify(shape => shape.IsCloseToHandle(It.IsAny<Point>()), Times.Once());
        }

        // test mouse up
        [TestMethod()]
        public void MouseUpTest()
        {
            _privateState.SetField("_isAdjusting", true);
            _privateState.SetField("_isMoving", true);
            _privateState.SetField("_startPoint", new Point(0, 0));
            _privateState.SetField("_lastPoint", new Point(10, 10));
            _state.MouseUp(new Point(10, 10));
            Assert.IsFalse((bool)_privateState.GetField("_isAdjusting"));
            Assert.IsFalse((bool)_privateState.GetField("_isMoving"));
            _model.Verify(model => model.NotifyModelChanged(EventArgs.Empty), Times.Once());
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            var graphicsMock = new Mock<IGraphics>();
            _state.Draw(graphicsMock.Object);
            Assert.AreNotEqual(";", ";"); // how do i know ☠️
        }

        // test key down
        [TestMethod()]
        public void KeyDownTest()
        {
            var shapeMock0 = new Mock<Shape>();
            var shapeMock = new Mock<Shape>();
            shapeMock.Setup(shape => shape.Selected).Returns(true);
            _model.Object.Shapes.Add(shapeMock0.Object);
            _model.Object.Shapes.Add(shapeMock.Object);
            _state.KeyDown(System.Windows.Forms.Keys.D);
            _state.KeyDown(System.Windows.Forms.Keys.Delete);
            shapeMock.Verify(shape => shape.Selected, Times.Once());
            _model.Verify(model => model.NotifyModelChanged(EventArgs.Empty), Times.Once());
        }
    }
}
