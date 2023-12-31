﻿using System;
using System.Collections.Generic;

namespace WindowPowerPoint
{
    public class CommandManager
    {
        public event EventHandler _undoStateChanged;
        public event EventHandler _redoStateChanged;
        private readonly Stack<ICommand> _undoStack;
        private readonly Stack<ICommand> _redoStack;

        // constructor
        public CommandManager()
        {
            _undoStack = new Stack<ICommand>();
            _redoStack = new Stack<ICommand>();
        }
        // execute command
        public virtual void Execute(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();
            StateChanged();
        }

        // undo command
        public virtual void Undo()
        {
            if (CanUndo)
            {
                ICommand command = _undoStack.Pop();
                command.Withdraw();
                _redoStack.Push(command);
                StateChanged();
            }
        }

        // redo command
        public virtual void Redo()
        {
            if (CanRedo)
            {
                ICommand command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                StateChanged();
            }
        }

        // clear command
        public virtual void Clear()
        {
            _undoStack.Clear();
            _redoStack.Clear();
            StateChanged();
        }

        // handle state change
        private void StateChanged()
        {
            if (_undoStateChanged != null)
                _undoStateChanged(this, EventArgs.Empty);
            if (_redoStateChanged != null)
                _redoStateChanged(this, EventArgs.Empty);
        }
        public virtual bool CanUndo
        {
            get
            {
                return _undoStack.Count > 0;
            }
        }

        public virtual bool CanRedo
        {
            get
            {
                return _redoStack.Count > 0;
            }
        }
    }
}
