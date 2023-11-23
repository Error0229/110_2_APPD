using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Moq;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PointStateTests
    {
        Mock<PowerPointModel> _model;
        PointState _state;
        PrivateObject _privateState;

        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            _state = new PointState(_model.Object);
            _privateState = new PrivateObject(_state);
        }

        [TestMethod()]
        public void PointStateTest()
        {
            _model = new Mock<PowerPointModel>();
            _state = new PointState(_model.Object);
            _privateState = new PrivateObject(_state);
            Assert.IsNotNull(_privateState.GetField("_model"));
        }

        [TestMethod()]
        public void MouseDownTest()
        {
            var p = new Point(0, 0);
            _state.MouseDown(p);
            Assert.IsTrue((bool)_privateState.GetField("_isDrawing"));
            _model.Verify(model => model.SetHintFirstPoint(p), Times.Once());
        }

        [TestMethod()]
        public void MouseMoveTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MouseUpTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DrawTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void KeyDownTest()
        {
            Assert.Fail();
        }
    }
}