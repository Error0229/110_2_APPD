﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowPowerPoint
{
    public partial class PowerPointPresentationModel : INotifyPropertyChanged, ISlide
    {
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler _modelChanged;
        public event ModelChangedEventHandler _cursorChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<int, Page.Action> _pageChanged;
        private CursorManager _cursorManager;
        private readonly CommandManager _commandManager;
        public int SlideIndex 
        { 
            get; set; 
        }
        public PowerPointPresentationModel(PowerPointModel model)
        {
            _commandManager = new CommandManager();
            _commandManager._undoStateChanged += (sender, e) => IsUndoEnabled = _commandManager.CanUndo;
            _commandManager._redoStateChanged += (sender, e) => IsRedoEnabled = _commandManager.CanRedo;
            _model = model;
            _model._modelChanged += HandleModelChanged;
            _model._pageChanged += HandlePageChanged;
            _model.ModelCommandManager = _commandManager;
            _isCircleChecked = false;
            _isLineChecked = false;
            _isRectangleChecked = false;
            _isSelecting = false;
            _isUndoEnabled = false;
            _isRedoEnabled = false;
            _isSaveEnabled = true;
            _isLoadEnabled = true;
        }

        // handle add page
        public void HandlePageChanged(int index, Page.Action operation)
        {
            if (_pageChanged != null)
            {
                _pageChanged.Invoke(index, operation);
            }
        }

        // insert shape with coordinate
        public void ProcessInsertShape(string shapeName, Point firstPoint, Point secondPoint)
        {
            if (shapeName != string.Empty)
            {
                _model.HandleInsertShape(shapeName, firstPoint, secondPoint);
            }
        }

        // insert shape
        public void ProcessInsertShape(string shapeName)
        {
            if (shapeName != string.Empty)
            {
                _model.HandleInsertShape(shapeName);
            }
        }

        // get shape
        public void ProcessRemoveShape(int columnIndex, int index)
        {
            if (columnIndex == 0 && index >= 0)
            {
                _model.HandleRemoveShape(SlideIndex, index);
            }
        }

        // process mouse enter canvas
        public void ProcessMouseEnterCanvas()
        {
            if (IsDrawing())
            {
                _cursorManager.CurrentCursor = Cursors.Cross;
            }
            else
            {
                _cursorManager.CurrentCursor = Cursors.Default;
            }
            NotifyCursorChanged(EventArgs.Empty);
        }

        // process mouse leave canvas
        public void ProcessMouseLeaveCanvas()
        {
            _cursorManager.CurrentCursor = Cursors.Default;
            NotifyCursorChanged(EventArgs.Empty);
        }

        // Set canvas coordinate
        public void SetCanvasSize(Size canvasSize)
        {
            if (SlideIndex < 0)
            {
                return;
            }
            _model.SetCanvasSize(canvasSize);
        }

        // on model changed
        public virtual void NotifyModelChanged(EventArgs e)
        {
            if (_modelChanged != null)
                _modelChanged(this, e);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_LINE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_CIRCLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_RECTANGLE_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_CURSOR_CHECKED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_UNDO_ENABLED));
                PropertyChanged(this, new PropertyChangedEventArgs(Constant.IS_REDO_ENABLED));
            }
        }

        // on cursor changed
        public virtual void NotifyCursorChanged(EventArgs e)
        {
            if (_cursorChanged != null)
                _cursorChanged(this, e);
        }

        // handle model changed
        public void HandleModelChanged(object sender, EventArgs e)
        {
            NotifyModelChanged(e);
        }

        // line clicked 
        public void ProcessLineClicked()
        {
            _isLineChecked = true;
            _model.ClearSelectedShape();
            _model.SetHint(ShapeType.LINE);
            _model.SetState(new DrawingState(_model));
            _isSelecting = _isCircleChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // circle clicked
        public void ProcessEllipseClicked()
        {
            _isCircleChecked = true;
            _model.ClearSelectedShape();
            _model.SetHint(ShapeType.CIRCLE);
            _model.SetState(new DrawingState(_model));
            _isSelecting = _isLineChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // rectangle clicked
        public void ProcessRectangleClicked()
        {
            _isRectangleChecked = true;
            _model.ClearSelectedShape();
            _model.SetHint(ShapeType.RECTANGLE);
            _model.SetState(new DrawingState(_model));
            _isSelecting = _isLineChecked = _isCircleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // cursor icon clicked
        public void ProcessCursorClicked()
        {
            _isSelecting = true;
            _model.SetState(new PointState(_model));
            _isLineChecked = _isCircleChecked = _isRectangleChecked = false;
            NotifyModelChanged(EventArgs.Empty);
        }

        // process key down
        public void ProcessKeyDown(Keys keyCode)
        {
            if (SlideIndex < 0)
            {
                return;
            }
            _model.HandleKeyDown(keyCode);
        }

        // process canvas pressed
        public void ProcessCanvasPressed(Point point)
        {
            if (IsDrawing() || IsCursorChecked)
            {
                _model.HandleMouseDown(point);
            }
        }

        // process mouse moving while pressed in canvas
        public void ProcessCanvasMoving(Point point)
        {
            if (SlideIndex < 0)
            {
                return;
            }
            _model.HandleMouseMove(point);
            NotifyCursorChanged(EventArgs.Empty);
        }

        // process mouse release while drawing
        public void ProcessCanvasReleased(Point point)
        {
            _model.HandleMouseUp(point);
            if (IsDrawing())
            {
                ProcessCursorClicked();
            }
            _cursorManager.CurrentCursor = Cursors.Default;
            NotifyCursorChanged(EventArgs.Empty);
        }

        // process add page
        public void ProcessAddPage(int newSlideIndex)
        {
            _model.HandleAddPage(newSlideIndex);
        }

        // process delete page
        public void ProcessDeletePage(int deletedSlideIndex)
        {
            _model.HandleDeletePage(deletedSlideIndex);
        }

        // process change page
        public void ProcessSwitchPage(int newSlideIndex)
        {
            _model.HandleSwitchPage(newSlideIndex);
        }

        // process redo
        public void ProcessRedo()
        {
            _model.Redo();
        }

        // process undo
        public void ProcessUndo()
        {
            _model.Undo();
        }

        // draw all the shape
        public void Draw(IGraphics graphics)
        {
            if (SlideIndex < 0)
            {
                return;
            }
            _model.Draw(graphics);
        }
        public bool IsCircleChecked
        {
            get
            {
                return _isCircleChecked;
            }
        }
        public bool IsLineChecked
        {
            get
            {
                return _isLineChecked;
            }
        }
        public bool IsRectangleChecked
        {
            get
            {
                return _isRectangleChecked;
            }
        }
        public bool IsCursorChecked
        {
            get
            {
                return _isSelecting;
            }
        }
        public bool IsUndoEnabled
        {
            get
            {
                return _isUndoEnabled;
            }
            set
            {
                _isUndoEnabled = value;
                NotifyModelChanged(EventArgs.Empty);
            }
        }
        public bool IsRedoEnabled
        {
            get
            {
                return _isRedoEnabled;
            }
            set
            {
                _isRedoEnabled = value;
                NotifyModelChanged(EventArgs.Empty);
            }
        }

        // is drawing
        public bool IsDrawing()
        {
            return _isRectangleChecked || _isLineChecked || _isCircleChecked;
        }

        public BindingList<Shape> Shapes
        {
            get
            {
                return _model.GetCurrentShapes();
            }
        }

        private bool _isLineChecked;
        private bool _isCircleChecked;
        private bool _isRectangleChecked;
        private bool _isSelecting;
        private bool _isUndoEnabled;
        private bool _isRedoEnabled;
        private readonly PowerPointModel _model;
    }
}
