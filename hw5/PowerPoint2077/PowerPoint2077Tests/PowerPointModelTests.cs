using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        PrivateObject _private_model;
        [TestInitialize()]
        public void Initialize()
        {
            _model = new PowerPointModel();
            _private_model = new PrivateObject(_model);
        }
        [TestMethod()]
        public void PowerPointModelTest()
        {
            Assert.IsNotNull(_model);
            Assert.IsNotNull(_private_model.GetField("_shapes"));
            Assert.IsNotNull(_private_model.GetField("_state"));
        }

        [TestMethod()]
        public void InsertShapeTest()
        {
            _model.InsertShape(Constant.LINE_CHINESE);
            _model.InsertShape(Constant.CIRCLE_CHINESE);
            _model.InsertShape(Constant.RECTANGLE_CHINESE);
            Assert.AreEqual(_model.Shapes.Count, 3);
        }

        [TestMethod()]
        public void InsertShapeWithPointTest()
        {
            _model.InsertShape(Constant.LINE_CHINESE, new Point(10, 10), new Point(50, 50));
            Assert.IsTrue(_model.Shapes[0].IsInShape(new Point(30, 30)));
        }

        [TestMethod()]
        public void SetIdleStateTest()
        {
            _model.SetState(new IdleState());
            Assert.IsInstanceOfType(_private_model.GetField("_state"), typeof(IdleState));    
        }

        [TestMethod()]
        public void SetPointStateTest()
        {
            _model.SetState(new PointState(_model));
            Assert.IsInstanceOfType(_private_model.GetField("_state"), typeof(PointState));
        }

        [TestMethod()]
        public void SetDrawingStateTest()
        {
            _model.SetState(new DrawingState(_model));
            Assert.IsInstanceOfType(_private_model.GetField("_state"), typeof(DrawingState));
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
            _model.SetCanvasCoordinate(new Point(0, 0), new Point(600, 800));
            Assert.AreEqual(_private_model.GetField("_canvasTopLeft"), new Point(0, 0));
            Assert.AreEqual(_private_model.GetField("_canvasButtonRight"), new Point(600, 800));
        }

        [TestMethod()]
        public void HandleDrawingStateMouseDownTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));

            var _private_state = new PrivateObject(_private_model.GetField("_state"));
            Assert.AreEqual(_private_state.GetField("_isDrawing"), true);
        }

        [TestMethod()]
        public void HandlePointStateMouseDownTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.HandleMouseDown(new Point(30, 30));
            Assert.IsFalse(_model.Shapes[0].Selected);
            Assert.IsTrue(_model.Shapes[1].Selected);
        }

        [TestMethod()]
        public void HandleDrawingStateMouseMoveTest()
        {
            _model.SetState(new DrawingState(_model));
            _model.SetHint(ShapeType.CIRCLE);
            _model.HandleMouseDown(new Point(50, 50));
            _model.HandleMouseMove(new Point(60, 60));
            Assert.AreEqual(((Shape)_private_model.GetField("_hint")).Info, "(50, 50), (60, 60)");
        }

        [TestMethod()]
        public void HandlePointStateMouseMoveTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.CIRCLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(65, 65));
            _model.HandleMouseMove(new Point(70, 70));
            Assert.AreEqual(_model.Shapes[0].Info, "(65, 65), (75, 75)");
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
            Assert.IsFalse((bool)(new PrivateObject((PointState)_private_model.GetField("_state"))).GetField("_isAdjusting"));
        }

        [TestMethod()]
        public void HandleKeyDownTest()
        {
            _model.SetState(new PointState(_model));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(10, 10), new Point(50, 50));
            _model.InsertShape(Constant.RECTANGLE_CHINESE, new Point(60, 60), new Point(70, 70));
            _model.HandleMouseDown(new Point(30, 30));
            _model.HandleKeyDown(Keys.Delete);

            Assert.AreEqual(_model.Shapes.Count, 0);
        }

        [TestMethod()]
        public void DrawTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DrawShapesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ClearSelectedShapeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DrawHintTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetHintTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetHintFirstPointTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SetHintSecondPointTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddShapeWithHintTest()
        {
            Assert.Fail();
        }
    }
}