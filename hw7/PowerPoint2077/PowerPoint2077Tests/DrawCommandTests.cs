using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class DrawCommandTests
    {
        DrawCommand _command;
        PrivateObject _privateCommand;
        Mock<PowerPointModel> _model;
        Mock<Shape> _shape;
        int _slideIndex;
        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _slideIndex = 0;
            _model = new Mock<PowerPointModel>();
            _shape = new Mock<Shape>();
            _command = new DrawCommand(_model.Object, _shape.Object, _slideIndex);
            _privateCommand = new PrivateObject(_command);
        }
        [TestMethod()]
        public void DrawCommandTest()
        {
            _model = new Mock<PowerPointModel>();
            _shape = new Mock<Shape>();
            _command = new DrawCommand(_model.Object, _shape.Object, _slideIndex);
            _privateCommand = new PrivateObject(_command);
            Assert.AreEqual(_privateCommand.GetField("_model"), _model.Object);
            Assert.AreEqual(_privateCommand.GetField("_shape"), _shape.Object);
        }


        [TestMethod()]
        public void ExcuteTest()
        {
            _command.Execute();
            _model.Verify(model => model.InsertShape(_shape.Object, _slideIndex), Times.Once());
        }

        [TestMethod()]
        public void UnexcuteTest()
        {
            _command.Withdraw();
            _model.Verify(model => model.RemoveShape(_shape.Object, _slideIndex), Times.Once());
        }
    }
}