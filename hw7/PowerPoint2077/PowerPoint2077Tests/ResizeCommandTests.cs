using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class ResizeCommandTests
    {
        Mock<PowerPointModel> _model;
        PointF _firstPoint;
        PointF _secondPoint;
        int _slideIndex;
        Shape _shape;
        ResizeCommand _command;
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            _firstPoint = new PointF(0, 0);
            _secondPoint = new PointF(100, 100);
            _slideIndex = 0;
            _shape = new Circle();
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _command = new ResizeCommand(_model.Object, _shape, _firstPoint, _secondPoint, _slideIndex);
        }

        [TestMethod()]
        public void ResizeCommandTest()
        {
            Assert.IsNotNull(_command);
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            _command.Execute();
            _command.Execute();
            _model.Verify(model => model.ResizeShape(_shape, new Point(10, 10), new Point(20, 20), _slideIndex), Times.Once());
        }

        [TestMethod()]
        public void UnexecuteTest()
        {
            _command.Unexecute();
            _model.Verify(model => model.ResizeShape(_shape, new Point(0, 0), new Point(100, 100), _slideIndex), Times.Once());
        }
    }
}