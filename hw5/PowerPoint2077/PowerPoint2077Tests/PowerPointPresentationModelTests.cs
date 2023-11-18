using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Moq;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PowerPointPresentationModelTests
    {
        PowerPointPresentationModel _presentationModel;
        Mock<PowerPointModel> _model;
        PrivateObject _privatePresentationModel;
        
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            _presentationModel = new PowerPointPresentationModel(_model.Object);
            _privatePresentationModel = new PrivateObject(_presentationModel);
        }
        [TestMethod()]
        public void PowerPointPresentationModelTest()
        {
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
            Assert.IsNotNull(_privatePresentationModel.GetField("_model"));
        }

        [TestMethod()]
        public void ProcessInsertShapeTest()
        {
            _presentationModel.ProcessInsertShape(Constant.CIRCLE_CHINESE);
            _model.Verify(model => model.InsertShape(Constant.CIRCLE_CHINESE), Times.Once());
        }

        [TestMethod()]
        public void ProcessRemoveShapeTest()
        {
            _presentationModel.ProcessInsertShape(Constant.CIRCLE_CHINESE);
            _presentationModel.ProcessRemoveShape(0, 0);
            _model.Verify(model => model.RemoveShape(0), Times.Once());
        }

        [TestMethod()]
        public void ProcessMouseEnterCanvasWhileDrawingTest()
        {
            _presentationModel.ProcessLineClicked();
            var cursor = _presentationModel.ProcessMouseEnterCanvas();
            Assert.AreEqual(cursor, Cursors.Cross);
        }

        [TestMethod()]
        public void ProcessMouseEnterCanvasWhileIdleTest()
        {
            var cursor = _presentationModel.ProcessMouseEnterCanvas();
            Assert.AreEqual(cursor, Cursors.Default);
        }

        [TestMethod()]
        public void ProcessMouseLeaveCanvasTest()
        {
            _presentationModel.ProcessLineClicked();
            _presentationModel.ProcessMouseEnterCanvas();
            var cursor = _presentationModel.ProcessMouseLeaveCanvas();
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
            Assert.AreEqual(cursor, Cursors.Default);
        }

        [TestMethod()]
        public void SetCanvasCoordinateTest()
        {
            var firstPoint = new Point(0, 0);
            var secondPoint = new Point(600, 800);
            _presentationModel.SetCanvasCoordinate(firstPoint, secondPoint);
            _model.Verify(model => model.SetCanvasCoordinate(firstPoint, secondPoint), Times.Once());
        }

        [TestMethod()]
        public void ProcessLineClickedTest()
        {
            _presentationModel.ProcessLineClicked();
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
        }

        [TestMethod()]
        public void ProcessEllipseClickedTest()
        {
            _presentationModel.ProcessEllipseClicked();
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isCircleChecked"));
        }

        [TestMethod()]
        public void ProcessRectangleClickedTest()
        {
            _presentationModel.ProcessRectangleClicked();
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
        }

        [TestMethod()]
        public void ProcessCursorClickedTest()
        {
            _presentationModel.ProcessCursorClicked();
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));

        }

        [TestMethod()]
        public void ProcessKeyDownTest()
        {
            var key = Keys.Delete;
            _presentationModel.ProcessKeyDown(key);
            _model.Verify(model => model.HandleKeyDown(key), Times.Once());
        }

        [TestMethod()]
        public void ProcessCanvasPressedTest()
        {
            var point = new Point(0, 0);
            _presentationModel.ProcessCursorClicked();
            _presentationModel.ProcessCanvasPressed(point);
            _model.Verify(model => model.HandleMouseDown(point), Times.Once());
        }

        [TestMethod()]
        public void ProcessCanvasMovingTest()
        {
            var point = new Point(50, 50);
            _presentationModel.ProcessCanvasMoving(point);
            _model.Verify(model => model.HandleMouseMove(point), Times.Once());
        }

        [TestMethod()]
        public void ProcessCanvasReleasedDrawTest()
        {
            var point = new Point(50, 50);
            _presentationModel.ProcessEllipseClicked();
            _presentationModel.ProcessCanvasPressed(new Point(0, 0));
            Assert.AreEqual(Cursors.Default, _presentationModel.ProcessCanvasReleased(point));
        }
        [TestMethod()]
        public void ProcessCanvasReleasedPointTest()
        {
            var point = new Point(50, 50);
            _presentationModel.ProcessCursorClicked();
            _presentationModel.ProcessCanvasPressed(new Point(0, 0));
            Assert.AreEqual(Cursors.Default, _presentationModel.ProcessCanvasReleased(point));
        }

        [TestMethod()]
        public void DrawTest()
        {
            var adaptor = new WindowsFormsGraphicsAdaptor(Graphics.FromImage(new Bitmap(800, 600)));
            _presentationModel.Draw(adaptor);
            _model.Verify(model => model.Draw(adaptor), Times.Once());
        }

        [TestMethod()]
        public void IsDrawingTest()
        {
            _presentationModel.ProcessCursorClicked();
            _presentationModel.ProcessEllipseClicked();
            _presentationModel.ProcessLineClicked();
            _presentationModel.ProcessRectangleClicked();
            Assert.IsFalse(_presentationModel.IsCursorChecked);
            Assert.IsFalse(_presentationModel.IsCircleChecked);
            Assert.IsFalse(_presentationModel.IsLineChecked);
            Assert.IsTrue(_presentationModel.IsRectangleChecked);
            Assert.IsTrue(_presentationModel.IsDrawing());
        }
        [TestMethod()]
        public void NotifyModelChangedTest()
        {
            var modelChanged = new Mock<PowerPointPresentationModel.ModelChangedEventHandler>();
            var propertyChanged = new Mock<PropertyChangedEventHandler>();
            _presentationModel._modelChanged += modelChanged.Object;
            _presentationModel.PropertyChanged += propertyChanged.Object;
            _presentationModel.NotifyModelChanged(EventArgs.Empty);
            modelChanged.Verify(m => m(_presentationModel, EventArgs.Empty), Times.Once());
            propertyChanged.Verify(p => p(_presentationModel, It.IsAny<PropertyChangedEventArgs>()), Times.Exactly(4));
        }
        [TestMethod()]
        public void HandleModelChangedTest()
        {
            var modelChanged = new Mock<PowerPointPresentationModel.ModelChangedEventHandler>();
            _presentationModel._modelChanged += modelChanged.Object;
            _presentationModel.HandleModelChanged(this, EventArgs.Empty);
            modelChanged.Verify(m => m(_presentationModel, EventArgs.Empty), Times.Once());
        }
        [TestMethod]
        public void ShapesTest()
        {
            _ = _presentationModel.Shapes;
            _model.Verify(model => model.Shapes, Times.Once());
        }

    }
}