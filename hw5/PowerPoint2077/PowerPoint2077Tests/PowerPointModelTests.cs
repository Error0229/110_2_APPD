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
        [TestInitialize()]
        public void Initialize()
        {
            _model = new PowerPointModel();
            _model.cursorManager = new CursorManager();
            _privateModel = new PrivateObject(_model);
        }
        [TestMethod()]
        public void PowerPointModelTest()
        {
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_privateModel.GetField("_shapes"));
            Assert.IsNotNull(_privateModel.GetField("_state"));
        }

        [TestMethod()]
        public void InsertShapeTest()
        {
            _model.InsertShape(Constant.LINE_CHINESE);
            _model.InsertShape(Constant.CIRCLE_CHINESE);
            _model.InsertShape(Constant.RECTANGLE_CHINESE);
            Assert.AreEqual(_model.Shapes.Count, 3);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, _model.Shapes[1].Name);
        }

        [TestMethod()]
        public void InsertShapeWithPointTest()
        {
            _model.InsertShape(Constant.LINE_CHINESE, new Point(10, 10), new Point(50, 50));
            Assert.IsTrue(_model.Shapes[0].IsInShape(new Point(30, 30)));
        }

        [TestMethod()]
        public void SetPointStateTest()
        {
            _model.SetState(new PointState(_model));
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(PointState));
        }

        [TestMethod()]
        public void SetDrawingStateTest()
        {
            _model.SetState(new DrawingState(_model));
            Assert.IsInstanceOfType(_privateModel.GetField("_state"), typeof(DrawingState));
        }

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

        [TestMethod()]
        public void SetCanvasCoordinateTest()
        {
            Point pointFirst = new Point(0, 0);
            Point pointSecond = new Point(800, 600);
            _model.SetCanvasCoordinate(pointFirst, pointSecond);
            Assert.AreEqual(pointFirst, _privateModel.GetField("_canvasTopLeft"));
            Assert.AreEqual(pointSecond, _privateModel.GetField("_canvasButtonRight"));
        }

        [TestMethod()]
        public void HandleDrawingStateMouseDownTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));

            var _private_state = new PrivateObject(_privateModel.GetField("_state"));
            Assert.IsTrue((bool)_private_state.GetField("_isDrawing"));
        }

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

        [TestMethod()]
        public void HandlePointStateMouseDownForClearingTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.HandleMouseDown(new Point(30, 30));
            _model.HandleMouseUp(new Point(30, 30));
            _model.HandleMouseDown(new Point(0, 0));
            Assert.IsFalse(_model.Shapes[0].Selected);
        }
        [TestMethod()]
        public void HandlePointStateMouseDownForAdjustTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(60, 60));
            _model.HandleMouseDown(new Point(60, 60));
            Assert.IsTrue((bool)(new PrivateObject((PointState)_privateModel.GetField("_state"))).GetField("_isAdjusting"));
        }

        [TestMethod()]
        public void HandleDrawingStateMouseMoveTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));
            _model.HandleMouseMove(new Point(60, 60));
            Assert.AreEqual(((Shape)_privateModel.GetField("_hint")).Info, "(50, 50), (60, 60)");
        }

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

        [TestMethod()]
        public void HandlePointStateMouseMoveCloseToHandleTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(90, 90), new Point(100, 100));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(65, 65));
            _model.HandleMouseUp(new Point(65, 65));
            _model.HandleMouseMove(new Point(60, 60));
            Assert.AreEqual(Cursors.SizeNWSE, _model.cursorManager.CurrentCursor);
        }

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

        [TestMethod()]
        public void HandleKeyDownDrawingStateTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.HandleKeyDown(Keys.Delete);
            Assert.AreNotEqual(";", ";"); // what do I know 🗿
        }

        [TestMethod()]
        public void DrawRecatangleTest()
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
            mockAdaptor.Verify(adaptor => adaptor.DrawHandle(It.IsAny<Point>()), Times.Exactly(9));
        }

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
            _model.Draw(mockAdaptor.Object);
            mockAdaptor.Verify(adaptor => adaptor.DrawCircle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawLine(It.IsAny<System.Drawing.Point>(), It.IsAny<System.Drawing.Point>()), Times.Once());
            mockAdaptor.Verify(adaptor => adaptor.DrawRectangle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
        }

        [TestMethod()]
        public void ClearSelectedShapeTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
            _model.HandleMouseDown(new Point(40, 40));
            _model.HandleMouseDown(new Point(60, 60));
            Assert.IsFalse(_model.Shapes[0].Selected);
        }

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

        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            var modelChanged = new Mock<PowerPointModel.ModelChangedEventHandler>();
            _model._modelChanged += modelChanged.Object;
            _model.NotifyModelChanged(EventArgs.Empty);
            modelChanged.Verify(m => m(_model, EventArgs.Empty), Times.Once());
        }

        [TestMethod()]
        public void SetHintTest()
        {
            _model.SetHint(ShapeType.CIRCLE);
            Assert.IsInstanceOfType(_privateModel.GetField("_hint"), typeof(Circle));
        }

        [TestMethod()]
        public void SetHintFirstPointTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetHintFirstPoint(new Point(0, 0));
            Assert.AreEqual((Point)(new PrivateObject((Circle)_privateModel.GetField("_hint"))).GetField("_pointFirst"), new Point(0, 0));
        }

        [TestMethod()]
        public void SetHintSecondPointTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetHintFirstPoint(new Point(0, 0));
            _model.SetHintSecondPoint(new Point(10, 10));
            Assert.AreEqual((Point)(new PrivateObject((Circle)_privateModel.GetField("_hint"))).GetField("_pointSecond"), new Point(10, 10));
        }

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
