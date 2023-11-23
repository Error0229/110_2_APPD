using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class DrawingStateTests
    {
        Mock<PowerPointModel> _model;
        DrawingState _state;
        PrivateObject _privateState;

        // set up
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            _state = new DrawingState(_model.Object);
            _privateState = new PrivateObject(_state);
        }

        // test constructor
        [TestMethod()]
        public void DrawingStateTest()
        {
            _model = new Mock<PowerPointModel>();
            _state = new DrawingState(_model.Object);
            _privateState = new PrivateObject(_state);
            Assert.IsNotNull(_privateState.GetField("_model"));
        }

        // test mouse down 🦥
        [TestMethod()]
        public void MouseDownTest()
        {
            var p = new Point(0, 0);
            _state.MouseDown(p);
            Assert.IsTrue((bool)_privateState.GetField("_isDrawing"));
            _model.Verify(model => model.SetHintFirstPoint(p), Times.Once());
        }

        // test mouse move 🙃
        [TestMethod()]
        public void MouseMoveTest()
        {
            _privateState.SetField("_isDrawing", true);
            var p = new Point(0, 0);
            _state.MouseMove(p);
            _model.Verify(model => model.SetHintSecondPoint(p), Times.Once());
        }

        // test mouse up 🐇
        [TestMethod()]
        public void MouseUpTest()
        {
            _privateState.SetField("_isDrawing", true);
            var p = new Point(0, 0);
            _state.MouseUp(p);
            _model.Verify(model => model.AddShapeWithHint(), Times.Once());
            Assert.IsFalse((bool)_privateState.GetField("_isDrawing"));
        }

        // test draw 🫏
        [TestMethod()]
        public void DrawTest()
        {
            _privateState.SetField("_isDrawing", true);
            var graphicsMock = new Mock<IGraphics>();
            _state.Draw(graphicsMock.Object);
            _model.Verify(model => model.DrawHint(graphicsMock.Object), Times.Once());
        }

        // test key down 🙈
        [TestMethod()]
        public void KeyDownTest()
        {
            _state.KeyDown(Keys.A);
            Assert.AreNotEqual(";", ";"); // how do i know 🐇
        }
    }
}
