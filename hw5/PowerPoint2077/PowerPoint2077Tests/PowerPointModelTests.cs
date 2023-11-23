using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Mock<IState> _state;
        // initialize the model
        [TestInitialize()]
        public void Initialize()
        {
            _manager = new Mock<CursorManager>();
            _state = new Mock<IState>();
            _model = new PowerPointModel
            {
                Manager = _manager.Object
            };
            _privateModel = new PrivateObject(_model);
        }

        // test the model constructor
        [TestMethod()]
        public void PowerPointModelTest()
        {
            _manager = new Mock<CursorManager>();
            _state = new Mock<IState>();
            _model = new PowerPointModel
            {
                Manager = _manager.Object
            };
            _privateModel = new PrivateObject(_model); Assert.IsNotNull(_model);
            Assert.IsNotNull(_privateModel.GetField("_shapes"));
            Assert.IsNotNull(_privateModel.GetField("_state"));
        }

        // test model insert shape
        [TestMethod()]
        public void InsertShapeTest()
        {
            _model.InsertShape(Constant.LINE_CHINESE);
            _model.InsertShape(Constant.CIRCLE_CHINESE);
            _model.InsertShape(Constant.RECTANGLE_CHINESE);
            Assert.AreEqual(_model.Shapes.Count, 3);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, _model.Shapes[1].Name);
        }

        // test model insert shape with point
        [TestMethod()]
        public void InsertShapeWithPointTest()
        {
            _model.InsertShape(Constant.LINE_CHINESE, new Point(10, 10), new Point(50, 50));
            Assert.IsTrue(_model.Shapes[0].IsInShape(new Point(30, 30)));
        }

        // test model set point state
        [TestMethod()]
        public void SetStateTest()
        {
            _model.SetState(_state.Object);
            Assert.IsNotNull(_privateModel.GetField("_state"));
        }

        // test model remove shape
        [TestMethod()]
        public void RemoveShapeTest()
        {
            _model.InsertShape(Constant.LINE_CHINESE);
            _model.InsertShape(Constant.CIRCLE_CHINESE);
            _model.InsertShape(Constant.RECTANGLE_CHINESE);

            _model.RemoveShape(0);

            Assert.AreEqual(_model.Shapes.Count, 2);
            Assert.IsInstanceOfType(_model.Shapes[0], typeof(Circle));
            Assert.IsInstanceOfType(_model.Shapes[1], typeof(Rectangle));
        }

        // test model set canvas coordinate
        [TestMethod()]
        public void SetCanvasCoordinateTest()
        {
            Point pointFirst = new Point(0, 0);
            Point pointSecond = new Point(800, 600);
            _model.SetCanvasCoordinate(pointFirst, pointSecond);
            Assert.AreEqual(pointFirst, _privateModel.GetField("_canvasTopLeft"));
            Assert.AreEqual(pointSecond, _privateModel.GetField("_canvasButtonRight"));
        }

        // test drawing state model mouse down
        [TestMethod()]
        public void HandleMouseDownTest()
        {
            var p = new Point(0, 0);
            _model.HandleMouseDown(p);
            _state.Verify(state => state.MouseDown(p), Times.Once());
        }

        // test point state model mouse down for move shape
        [TestMethod()]
        public void HandlePointStateMouseDownForMovingTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.HandleMouseDown(new Point(30, 30));
            Assert.IsFalse(_model.Shapes[0].Selected);
            Assert.IsTrue(_model.Shapes[1].Selected);
        }

        // test point state model mouse down for clear shape
        [TestMethod()]
        public void HandlePointStateMouseDownForClearingTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.HandleMouseDown(new Point(30, 30));
            _model.HandleMouseUp(new Point(30, 30));
            _model.HandleMouseDown(new Point(0, 0));
            Assert.IsFalse((bool)(new PrivateObject((PointState)_privateModel.GetField("_state"))).GetField("_isAdjusting"));
            Assert.IsFalse(_model.Shapes[0].Selected);
        }

        // test point state model mouse down for adjust shape
        [TestMethod()]
        public void HandlePointStateMouseDownForAdjustTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(60, 60));
            _model.HandleMouseDown(new Point(60, 60));
            Assert.IsTrue((bool)(new PrivateObject((PointState)_privateModel.GetField("_state"))).GetField("_isAdjusting"));
        }

        // test drawing state model mouse down for move shape
        [TestMethod()]
        public void HandleDrawingStateMouseMoveTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));
            _model.HandleMouseMove(new Point(60, 60));
            Assert.AreEqual(((Shape)_privateModel.GetField("_hint")).Info, "(50, 50), (60, 60)");
        }

        // test point state model mouse move for move shape
        [TestMethod()]
        public void HandlePointStateMouseMoveForMoveTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(30, 30), new Point(40, 40));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(65, 65));
            _model.HandleMouseMove(new Point(70, 70));
            Assert.AreEqual(_model.Shapes[1].Info, "(65, 65), (75, 75)");
        }

        // test point state model mouse move for adjust shape
        [TestMethod()]
        public void HandlePointStateMouseMoveForAdjustTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(90, 90), new Point(100, 100));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(60, 60));
            _model.HandleMouseDown(new Point(60, 60));
            _model.HandleMouseMove(new Point(50, 50));
            Assert.AreEqual(_model.Shapes[1].Info, "(50, 50), (70, 70)");
        }

        // test point state model mouse move when close to handle
        [TestMethod()]
        public void HandlePointStateMouseMoveCloseToHandleTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(90, 90), new Point(100, 100));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(65, 65));
            _model.HandleMouseUp(new Point(65, 65));
            _model.HandleMouseMove(new Point(60, 60));
            Assert.AreEqual(Cursors.SizeNWSE, _model.Manager.CurrentCursor);
        }

        // test drawing state model mouse up
        [TestMethod()]
        public void HandleDrawingStateMouseUpTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));
            _model.HandleMouseMove(new Point(70, 70));
            _model.HandleMouseUp(new Point(70, 70));
            Assert.AreEqual(_model.Shapes[0].Info, "(50, 50), (70, 70)");
        }

        // test point state model mouse up
        [TestMethod()]
        public void HandlePointStateMouseUpTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(65, 65));
            _model.HandleMouseMove(new Point(70, 70));
            _model.HandleMouseUp(new Point(70, 70));
            Assert.IsFalse((bool)(new PrivateObject((PointState)_privateModel.GetField("_state"))).GetField("_isMoving"));
        }

        // test key down for point state
        [TestMethod()]
        public void HandleKeyDownPointStateTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleKeyDown(Keys.Delete);
            _model.HandleMouseDown(new Point(30, 30));
            _model.HandleKeyDown(Keys.Delete);

            Assert.AreEqual(_model.Shapes.Count, 1);
        }

        // test key down for drawing state
        [TestMethod()]
        public void HandleKeyDownDrawingStateTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.HandleKeyDown(Keys.Delete);
            Assert.AreNotEqual(";", ";"); // what do I know 🗿
        }

        // test draw rectangle
        [TestMethod()]
        public void DrawRectangleTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
            _model.HandleMouseDown(new Point(40, 40));
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(graphics);
            _model.Draw(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawRectangle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawHandle(It.IsAny<Point>()), Times.Exactly(8));
        }

        // test draw circle
        [TestMethod()]
        public void DrawCircleTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
            _model.HandleMouseDown(new Point(40, 40));
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(graphics);
            _model.Draw(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawCircle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawHandle(It.IsAny<Point>()), Times.Exactly(8));
        }

        // test draw line
        [TestMethod()]
        public void DrawLineTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.LINE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
            _model.HandleMouseDown(new Point(40, 40));
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(graphics);
            _model.Draw(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawLine(It.IsAny<System.Drawing.Point>(), It.IsAny<System.Drawing.Point>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawHandle(It.IsAny<Point>()), Times.Exactly(2));
        }

        // test draw shapes
        [TestMethod()]
        public void DrawShapesTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.InsertShape(Constant.LINE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
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
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
            _model.HandleMouseDown(new Point(40, 40));
            _model.ClearSelectedShape();
            Assert.IsFalse(_model.Shapes[0].Selected);
        }

        // test draw while drawing
        [TestMethod()]
        public void DrawWhileDrawingTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));
            _model.HandleMouseMove(new Point(70, 70));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            var mockAdaptor = new Mock<WindowsFormsGraphicsAdaptor>(graphics);
            _model.Draw(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawCircle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
        }

        // test draw hint
        [TestMethod()]
        public void DrawHintTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));
            _model.HandleMouseMove(new Point(70, 70));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
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
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetHintFirstPoint(new Point(0, 0));
            Assert.AreEqual((Point)(new PrivateObject((Circle)_privateModel.GetField("_hint"))).GetField("_pointFirst"), new Point(0, 0));
        }

        // test set hint second point
        [TestMethod()]
        public void SetHintSecondPointTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetHintFirstPoint(new Point(0, 0));
            _model.SetHintSecondPoint(new Point(10, 10));
            Assert.AreEqual((Point)(new PrivateObject((Circle)_privateModel.GetField("_hint"))).GetField("_pointSecond"), new Point(10, 10));
        }

        // test add shape with hint
        [TestMethod()]
        public void AddShapeWithHintTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetHintFirstPoint(new Point(0, 0));
            _model.SetHintSecondPoint(new Point(10, 10));
            _model.AddShapeWithHint();
            Assert.AreEqual(_model.Shapes.Count, 1);
        }
    }
}
