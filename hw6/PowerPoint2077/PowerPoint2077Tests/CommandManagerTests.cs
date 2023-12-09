using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class CommandManagerTests
    {
        CommandManager _commandManager;
        PrivateObject _privateCommandManager;
        Mock<ICommand> _command;
        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _command = new Mock<ICommand>();
            _commandManager = new CommandManager();
            _commandManager._undoStateChanged += (sender, e) => { };
            _commandManager._redoStateChanged += (sender, e) => { };
            _privateCommandManager = new PrivateObject(_commandManager);
        }

        // test constructor
        [TestMethod()]
        public void CommandManagerTest()
        {
            _commandManager = new CommandManager();
            _privateCommandManager = new PrivateObject(_commandManager);
            Assert.IsNotNull(_privateCommandManager.GetField("_undoStack"));
            Assert.IsNotNull(_privateCommandManager.GetField("_redoStack"));
        }

        // test excute
        [TestMethod()]
        public void ExcuteTest()
        {
            _commandManager.Execute(_command.Object);
            var _undoStack = (Stack<ICommand>)_privateCommandManager.GetField("_undoStack");
            var _redoStack = (Stack<ICommand>)_privateCommandManager.GetField("_redoStack");
            Assert.AreEqual(_undoStack.Count, 1);
            Assert.AreEqual(_redoStack.Count, 0);
            _command.Verify(command => command.Execute(), Times.Once());
        }

        // test undo
        [TestMethod()]
        public void UndoTest()
        {
            _commandManager.Execute(_command.Object);
            _commandManager.Undo();
            var _undoStack = (Stack<ICommand>)_privateCommandManager.GetField("_undoStack");
            var _redoStack = (Stack<ICommand>)_privateCommandManager.GetField("_redoStack");
            Assert.AreEqual(_undoStack.Count, 0);
            Assert.AreEqual(_redoStack.Count, 1);
            _command.Verify(command => command.Unexecute(), Times.Once());
            _undoStack.Clear();
            _redoStack.Clear();
            _commandManager.Undo();
            Assert.AreEqual(_undoStack.Count, 0);
            Assert.AreEqual(_redoStack.Count, 0);
            _commandManager.Redo();
            Assert.AreEqual(_undoStack.Count, 0);
            Assert.AreEqual(_redoStack.Count, 0);
            _commandManager.Undo();
            _commandManager.Execute(_command.Object);
            _commandManager.Undo();
            _commandManager.Execute(_command.Object);
            _commandManager.Execute(_command.Object);
            _commandManager.Undo();
            _commandManager.Undo();
            _commandManager.Undo();
            Assert.AreEqual(_undoStack.Count, 0);
            Assert.AreEqual(_redoStack.Count, 2);
            _commandManager.Redo();
            Assert.AreEqual(_undoStack.Count, 1);
            Assert.AreEqual(_redoStack.Count, 1);
        }

        // test redo
        [TestMethod()]
        public void RedoTest()
        {
            _commandManager.Execute(_command.Object);
            _commandManager.Undo();
            _commandManager.Redo();
            var _undoStack = (Stack<ICommand>)_privateCommandManager.GetField("_undoStack");
            var _redoStack = (Stack<ICommand>)_privateCommandManager.GetField("_redoStack");
            Assert.AreEqual(_undoStack.Count, 1);
            Assert.AreEqual(_redoStack.Count, 0);
            _command.Verify(command => command.Execute(), Times.Exactly(2));
            _undoStack.Clear();
            _redoStack.Clear();
            _commandManager.Redo();
            Assert.AreEqual(_undoStack.Count, 0);
            Assert.AreEqual(_redoStack.Count, 0);
            _commandManager.Undo();
            _commandManager.Execute(_command.Object);
            _commandManager.Undo();
            _commandManager.Execute(_command.Object);
            _commandManager.Execute(_command.Object);
            _commandManager.Undo();
            _commandManager.Undo();
            _commandManager.Undo();
            Assert.AreEqual(_undoStack.Count, 0);
            Assert.AreEqual(_redoStack.Count, 2);
            _commandManager.Redo();
            _commandManager.Redo();
            _commandManager.Redo();
            Assert.AreEqual(_undoStack.Count, 2);
            Assert.AreEqual(_redoStack.Count, 0);
        }

        // test state changed
        [TestMethod()]
        public void StateChangedTest()
        {
            _commandManager.Execute(_command.Object);
            _commandManager.Undo();
            _commandManager.Redo();
            Assert.IsNotNull(_privateCommandManager.GetField("_undoStateChanged"));
            Assert.IsNotNull(_privateCommandManager.GetField("_redoStateChanged"));
        }
    }
}