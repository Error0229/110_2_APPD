using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class AddPageCommandTests
    {
        Mock<Page> _page;
        Mock<PowerPointModel> _model;
        int _slideIndex;
        AddPageCommand _command;
        Mock<Action<int, Page.Action>> _action;
        // initialize
        [TestInitialize]
        public void Initialize()
        {
            _action = new Mock<Action<int, Page.Action>>();
            _page = new Mock<Page>();
            _model = new Mock<PowerPointModel>();
            _model.Object._pageChanged += _action.Object;
            _slideIndex = 0;
            _command = new AddPageCommand(_model.Object, _slideIndex, _page.Object);
        }

        // test constructor
        [TestMethod()]
        public void AddPageCommandTest()
        {
            Assert.IsNotNull(_command);
        }

        // test execute
        [TestMethod()]
        public void ExecuteTest()
        {
            _command.Execute();
            _model.Verify(model => model.AddPage(_slideIndex, _page.Object), Times.Once());
            _action.Verify(action => action(_slideIndex, Page.Action.Add), Times.Once());
        }

        // test unexecute
        [TestMethod()]
        public void UnexecuteTest()
        {
            _command.Unexecute();
            _model.Verify(model => model.DeletePage(_slideIndex, _page.Object), Times.Once());
            _action.Verify(action => action(_slideIndex, Page.Action.Remove), Times.Once());
        }
    }
}