using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class MoveCommandTests
    {
        MoveCommand _command;
        PrivateObject _privateCommand;
        Mock<PowerPointModel> _model;
        Mock<Shape> _shape;
        Point _offset;
        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            _shape = new Mock<Shape>();
            _offset = new Point(10, 5);
            _command = new MoveCommand(_model.Object, _shape.Object, _offset);
            _privateCommand = new PrivateObject(_command);
        }
        [TestMethod()]
        public void MoveCommandTest()
        {
            _command = new MoveCommand(_model.Object, _shape.Object, _offset);
            _privateCommand = new PrivateObject(_command);
            Assert.AreEqual(_privateCommand.GetField("_model"), _model.Object);
            Assert.AreEqual(_privateCommand.GetField("_shape"), _shape.Object);
        }

        [TestMethod()]
        public void ExcuteTest()
        {
            _command.Execute();
            _command.Execute();
            _model.Verify(model => model.MoveShape(_shape.Object, _offset), Times.Once());
        }

        [TestMethod()]
        public void UnexcuteTest()
        {
            _command.Unexecute();
            _model.Verify(model => model.MoveShape(_shape.Object, new Point(-_offset.X, -_offset.Y)), Times.Once());
        }
    }
}