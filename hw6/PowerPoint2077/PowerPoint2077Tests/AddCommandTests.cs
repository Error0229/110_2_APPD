﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class AddCommandTests
    {
        AddCommand _command;
        PrivateObject _privateCommand;
        Mock<PowerPointModel> _model;
        Mock<Shape> _shape;
        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _model = new Mock<PowerPointModel>();
            _shape = new Mock<Shape>();
            _command = new AddCommand(_model.Object, _shape.Object);
            _privateCommand = new PrivateObject(_command);
        }

        [TestMethod()]
        public void AddCommandTest()
        {
            _model = new Mock<PowerPointModel>();
            _shape = new Mock<Shape>();
            _command = new AddCommand(_model.Object, _shape.Object);
            _privateCommand = new PrivateObject(_command);
            Assert.AreEqual(_privateCommand.GetField("_model"), _model.Object);
            Assert.AreEqual(_privateCommand.GetField("_shape"), _shape.Object);
        }

        [TestMethod()]
        public void ExcuteTest()
        {
            _command.Execute();
            _model.Verify(model => model.InsertShape(_shape.Object), Times.Once());
        }

        [TestMethod()]
        public void UnexcuteTest()
        {
            _command.Unexecute();
            _model.Verify(model => model.RemoveShape(_shape.Object), Times.Once());
        }
    }
}