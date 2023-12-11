using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PowerPointPresentationModelTests
    {
        PowerPointPresentationModel _presentationModel;
        Mock<PowerPointModel> _model;
        PrivateObject _privatePresentationModel;

        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            _presentationModel = new PowerPointPresentationModel(_model.Object);
            _presentationModel.SetCursorManager(new CursorManager());
            _privatePresentationModel = new PrivateObject(_presentationModel);
        }

        // test constructor
        [TestMethod()]
        public void PowerPointPresentationModelTest()
        {
            _model = new Mock<PowerPointModel>();
            _presentationModel = new PowerPointPresentationModel(_model.Object);
            _presentationModel.SetCursorManager(new CursorManager());
            _privatePresentationModel = new PrivateObject(_presentationModel);
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
            Assert.IsNotNull(_privatePresentationModel.GetField("_model"));
        }

        // test insert shape
        [TestMethod()]
        public void ProcessInsertShapeTest()
        {
            _presentationModel.ProcessInsertShape(Constant.CIRCLE_CHINESE);
            _model.Verify(model => model.HandleInsertShape(Constant.CIRCLE_CHINESE), Times.Once());
        }

        // test remove shape
        [TestMethod()]
        public void ProcessRemoveShapeTest()
        {
            _presentationModel.ProcessInsertShape(Constant.CIRCLE_CHINESE);
            _presentationModel.ProcessRemoveShape(0, 0);
            _model.Verify(model => model.HandleRemoveShape(0), Times.Once());
        }

        // test mouse enter canvas while drawing
        [TestMethod()]
        public void ProcessMouseEnterCanvasWhileDrawingTest()
        {

            _presentationModel.ProcessLineClicked();
            _presentationModel.ProcessMouseEnterCanvas();
            Assert.AreEqual(((CursorManager)_privatePresentationModel.GetField("_cursorManager")).CurrentCursor, Cursors.Cross);
        }

        // test mouse enter canvas while idle
        [TestMethod()]
        public void ProcessMouseEnterCanvasWhileIdleTest()
        {
            _presentationModel.ProcessMouseEnterCanvas();
            Assert.AreEqual(((CursorManager)_privatePresentationModel.GetField("_cursorManager")).CurrentCursor, Cursors.Default);
        }

        // test mouse leave canvas
        [TestMethod()]
        public void ProcessMouseLeaveCanvasTest()
        {
            _presentationModel.ProcessLineClicked();
            _presentationModel.ProcessMouseEnterCanvas();
            _presentationModel.ProcessMouseLeaveCanvas();
            Assert.AreEqual(((CursorManager)_privatePresentationModel.GetField("_cursorManager")).CurrentCursor, Cursors.Default);
        }

        // test set canvas size
        [TestMethod()]
        public void SetCanvasSizeTest()
        {
            var size = new Size(1600, 900);
            _presentationModel.SetCanvasSize(size);
            _model.Verify(model => model.SetCanvasSize(size), Times.Once());
        }

        // test Line button clicked
        [TestMethod()]
        public void ProcessLineClickedTest()
        {
            _presentationModel.ProcessLineClicked();
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
        }

        // test Circle button clicked
        [TestMethod()]
        public void ProcessEllipseClickedTest()
        {
            _presentationModel.ProcessEllipseClicked();
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isCircleChecked"));
        }

        // test Rectangle button clicked
        [TestMethod()]
        public void ProcessRectangleClickedTest()
        {
            _presentationModel.ProcessRectangleClicked();
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
        }

        // test Cursor button clicked
        [TestMethod()]
        public void ProcessCursorClickedTest()
        {
            _presentationModel.ProcessCursorClicked();
            Assert.IsTrue((bool)_privatePresentationModel.GetField("_isSelecting"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isRectangleChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isLineChecked"));
            Assert.IsFalse((bool)_privatePresentationModel.GetField("_isCircleChecked"));
        }

        // test key down
        [TestMethod()]
        public void ProcessKeyDownTest()
        {
            var key = Keys.Delete;
            _presentationModel.ProcessKeyDown(key);
            _model.Verify(model => model.HandleKeyDown(key), Times.Once());
        }

        // test canvas pressed
        [TestMethod()]
        public void ProcessCanvasPressedTest()
        {
            var point = new Point(0, 0);
            _presentationModel.ProcessCursorClicked();
            _presentationModel.ProcessCanvasPressed(point);
            _model.Verify(model => model.HandleMouseDown(point), Times.Once());
        }

        // test canvas mouse moving
        [TestMethod()]
        public void ProcessCanvasMovingTest()
        {
            var point = new Point(50, 50);
            _presentationModel.ProcessCanvasMoving(point);
            _model.Verify(model => model.HandleMouseMove(point), Times.Once());
        }

        // test process redo 
        [TestMethod()]
        public void ProcessRedoTest()
        {
            _presentationModel.ProcessRedo();
            _model.Verify(model => model.Redo(), Times.Once());
        }

        // test process undo
        [TestMethod()]
        public void ProcessUndoTest()
        {
            _presentationModel.ProcessUndo();
            _model.Verify(model => model.Undo(), Times.Once());
        }

        // test canvas mouse release in drawing state
        [TestMethod()]
        public void ProcessCanvasReleasedDrawTest()
        {
            var point = new Point(50, 50);
            _presentationModel.ProcessEllipseClicked();
            _presentationModel.ProcessCanvasPressed(new Point(0, 0));
            _presentationModel.ProcessCanvasReleased(point);
            Assert.AreEqual(((CursorManager)_privatePresentationModel.GetField("_cursorManager")).CurrentCursor, Cursors.Default);
        }

        // test canvas mouse release in point state
        [TestMethod()]
        public void ProcessCanvasReleasedPointTest()
        {
            var point = new Point(50, 50);
            _presentationModel.ProcessCursorClicked();
            _presentationModel.ProcessCanvasPressed(new Point(0, 0));
            _presentationModel.ProcessCanvasReleased(point);
            Assert.AreEqual(((CursorManager)_privatePresentationModel.GetField("_cursorManager")).CurrentCursor, Cursors.Default);
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            var adaptor = new WindowsFormsGraphicsAdaptor(Graphics.FromImage(new Bitmap(800, 600)));
            _presentationModel.Draw(adaptor);
            _model.Verify(model => model.Draw(adaptor), Times.Once());
        }

        // test is drawing
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

        // test notify model changed
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

        // test notify cursor changed
        [TestMethod()]
        public void NotifyCursorChangedTest()
        {
            var cursorChanged = new Mock<PowerPointPresentationModel.ModelChangedEventHandler>();
            _presentationModel._cursorChanged += cursorChanged.Object;
            _presentationModel.NotifyCursorChanged(EventArgs.Empty);
            cursorChanged.Verify(m => m(_presentationModel, EventArgs.Empty), Times.Once());
        }

        // test handle model changed
        [TestMethod()]
        public void HandleModelChangedTest()
        {
            var modelChanged = new Mock<PowerPointPresentationModel.ModelChangedEventHandler>();
            _presentationModel._modelChanged += modelChanged.Object;
            _presentationModel.HandleModelChanged(this, EventArgs.Empty);
            modelChanged.Verify(m => m(_presentationModel, EventArgs.Empty), Times.Once());
        }

        // test get shape
        [TestMethod()]
        public void ShapesTest()
        {
            _ = _presentationModel.Shapes;
            _model.Verify(model => model.Shapes, Times.Once());
        }

        // test is undo/redo enabled
        [TestMethod()]
        public void IsUndoRedoEnabledTest()
        {
            var propertyChanged = new Mock<PropertyChangedEventHandler>();
            _presentationModel.PropertyChanged += propertyChanged.Object;
            _ = _presentationModel.IsUndoEnabled;
            _ = _presentationModel.IsRedoEnabled;
            _presentationModel.IsUndoEnabled = true;
            _presentationModel.IsRedoEnabled = true;
            propertyChanged.Verify(p => p(_presentationModel, It.IsAny<PropertyChangedEventArgs>()), Times.Exactly(2));
        }

    }
}
