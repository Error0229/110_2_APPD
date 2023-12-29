using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace WindowPowerPoint.Tests
{
    using System.Threading.Tasks;
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
            var size = new Size(600, 800);
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
            Assert.IsNotNull(_privateModel.GetField("_state"));
        }

        // test insert shape
        [TestMethod()]
        public void InsertShapeTest()
        {
            _model.AddPage(_slideIndex + 1, new Page());
            _slideIndex = 0;
            _model.InsertShape(new Circle(), _slideIndex);
            Assert.AreEqual(1, GetShapesCount());
        }

        // test get current shapes
        [TestMethod()]
        public void GetCurrentShapesTest()
        {
            _model.InsertShape(new Circle(), _slideIndex);
            _model.InsertShape(new Line(), _slideIndex);
            _model.InsertShape(new Rectangle(), _slideIndex);
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
            _model.InsertShape(shape, _slideIndex);
            _slideIndex = 0;
            _model.ResizeShape(shape, new PointF(100, 100), new PointF(200, 200), 1);
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
            _model.AddPage(_slideIndex, new Page());
            Assert.AreEqual(2, (_privateModel.GetField("_slides") as Pages).Count);
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
            _model.AddPage(_slideIndex, page);
            _model.SlideIndex = 1;
            _model.DeletePage(0, page);
            Assert.AreEqual(1, (_privateModel.GetField("_slides") as Pages).Count);
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
            _model.InsertShape(shape, 1);
            _model.SlideIndex = 0;
            _model.RemoveShape(shape, 1);
            Assert.AreEqual(0, GetShapesCount());
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
            _model.InsertShape(shape, _slideIndex);
            _model.HandleRemoveShape(_slideIndex, 0);
            _commandManager.Verify(manager => manager.Execute(It.IsAny<DeleteCommand>()), Times.Once());
        }

        // test model set canvas size
        [TestMethod()]
        public void SetCanvasSizeTest1()
        {
            var size = new Size(600, 800);
            var shape = new Circle();
            _model.InsertShape(shape, _slideIndex);
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
            _model.InsertShape(new Circle(), _slideIndex);
            _model.InsertShape(new Line(), _slideIndex);
            _model.InsertShape(new Rectangle(), _slideIndex);
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
            _model.InsertShape(shape1.Object, _slideIndex);
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
            _model.InsertShape(shape1.Object, _slideIndex);
            _model.InsertShape(shape2.Object, _slideIndex);
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

        // handle save test
        [TestMethod]
        public async Task HandleSaveTest()
        {
            var mockDrive = new Mock<GoogleDriveService>("PowePoint2077Test", "credential", new Mock<IMessageBox>().Object);
            _privateModel.SetField("_drive", mockDrive.Object);
            _privateModel.SetField("_fileID", null);
            mockDrive.Setup(service => service.Save(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(string.Empty);
            Assert.IsFalse(await _model.HandleSave());
            _privateModel.SetField("_fileID", "file ID");
            mockDrive.Setup(service => service.UpdateFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            Assert.IsFalse(await _model.HandleSave());
            mockDrive.Setup(service => service.UpdateFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            Assert.IsTrue(await _model.HandleSave());
        }

        // handle load test
        [TestMethod]
        public void HandleLoadTest()
        {

            var mockDrive = new Mock<GoogleDriveService>("PowePoint2077Test", "credential", new Mock<IMessageBox>().Object);
            _privateModel.SetField("_drive", mockDrive.Object);
            _privateModel.SetField("_fileID", null);
            _privateModel.SetField("_messageBox", new Mock<IMessageBox>().Object);
            mockDrive.Setup(service => service.Load(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            _model.HandleLoad();
            System.IO.File.WriteAllText(Constant.LOAD_FILE_NAME, "{RECTANGLE,(0.3143275,0.4012987,0.6681287,0.7155844)} \n");
            mockDrive.Setup(service => service.Load(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _model.SlideIndex = -1;
            _model.HandleLoad();
        }

        // check saves exist test
        [TestMethod]
        public void CheckSavesExistTest()
        {
            var mockDrive = new Mock<GoogleDriveService>("PowePoint2077Test", "credential", new Mock<IMessageBox>().Object);
            _privateModel.SetField("_drive", mockDrive.Object);
            mockDrive.Setup(service => service.SearchFile(It.IsAny<string>())).ReturnsAsync(new System.Collections.Generic.List<Google.Apis.Drive.v3.Data.File>());
            _model.CheckSavesExist();
            Assert.AreEqual(_privateModel.GetField("_fileID"), string.Empty);
            mockDrive.Setup(service => service.SearchFile(It.IsAny<string>())).ReturnsAsync(new System.Collections.Generic.List<Google.Apis.Drive.v3.Data.File>() { new Google.Apis.Drive.v3.Data.File() });
            _model.CheckSavesExist();
            Assert.AreNotEqual(_privateModel.GetField("_fileID"), string.Empty);
        }

        // test clear slide
        [TestMethod]
        public void ClearSlideTest()
        {
            _model.AddPage(0, new Page());
            _model.AddPage(1, new Page());
            // set command manager when call delete page make _model.SlideIndex-=1;
            _commandManager.Setup(manager => manager.Execute(It.IsAny<DeletePageCommand>())).Callback(() => _model.SlideIndex -= 1);
            _model.ClearSlide();
            Assert.AreEqual(-1, _model.SlideIndex);
        }

        // interpret pages test
        [TestMethod]
        public void InterpretPagesTest()
        {
            var data = "{RECTANGLE,(0.3143275,0.4012987,0.6681287,0.7155844)} \n";
            var result = _privateModel.Invoke("InterpretPages", data) as System.Collections.Generic.List<string>;
            Assert.AreEqual(2, result.Count);
        }

        // check save file exist test
        [TestMethod]
        public void CheckSaveFileExistTest()
        {
            var mockDrive = new Mock<GoogleDriveService>("PowePoint2077Test", "credential", new Mock<IMessageBox>().Object);
            _privateModel.SetField("_drive", mockDrive.Object);
            mockDrive.Setup(service => service.SearchFile(It.IsAny<string>())).ReturnsAsync(new System.Collections.Generic.List<Google.Apis.Drive.v3.Data.File>());
            _model.CheckSavesExist();
            Assert.AreEqual(_privateModel.GetField("_fileID"), string.Empty);
            mockDrive.Setup(service => service.SearchFile(It.IsAny<string>())).ReturnsAsync(new System.Collections.Generic.List<Google.Apis.Drive.v3.Data.File>() { new Google.Apis.Drive.v3.Data.File() });
            _model.CheckSavesExist();
            Assert.AreNotEqual(_privateModel.GetField("_fileID"), string.Empty);
        }

        // get shapes count
        private int GetShapesCount()
        {
            return _model.GetCurrentShapes().Count;
        }
    }
}
