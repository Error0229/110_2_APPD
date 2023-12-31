﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace WindowPowerPoint
{
    public partial class PowerPointModel : ISlide
    {
        public event Action<int, Page.Action> _pageChanged;
        public delegate void ModelChangedEventHandler(object sender, EventArgs e);
        public event ModelChangedEventHandler _modelChanged;

        private IState _state;
        private readonly Dictionary<HandleType, Cursor> _handleToCursor;
        public Dictionary<HandleType, Cursor> HandleToCursor
        {
            get
            {
                return _handleToCursor;
            }
        }

        // constructor
        public PowerPointModel()
        {
            _drive = new GoogleDriveService(Constant.PROJECT_NAME, Constant.SECRET_FILE_NAME, new MessageBoxService());
            CheckSavesExist();
            _factory = new ShapeFactory();
            _pageFactory = new PageFactory();
            _slides = new Pages();
            _slides.Add(_pageFactory.GetPage());
            _state = new PointState(this);
            CanvasSize = new Size(0, 0);
            _messageBox = new MessageBoxService();
            _handleToCursor = new Dictionary<HandleType, Cursor>();
            _handleToCursor.Add(HandleType.TopLeft, Cursors.SizeNWSE);
            _handleToCursor.Add(HandleType.Top, Cursors.SizeNS);
            _handleToCursor.Add(HandleType.TopRight, Cursors.SizeNESW);
            _handleToCursor.Add(HandleType.Left, Cursors.SizeWE);
            _handleToCursor.Add(HandleType.Right, Cursors.SizeWE);
            _handleToCursor.Add(HandleType.BottomLeft, Cursors.SizeNESW);
            _handleToCursor.Add(HandleType.Bottom, Cursors.SizeNS);
            _handleToCursor.Add(HandleType.BottomRight, Cursors.SizeNWSE);
            _handleToCursor.Add(HandleType.None, Cursors.Default);
            // don't fuck me, fuck 🧑🏿‍⚕️smell
        }

        // notify page change
        public void NotifyPageChanged(int index, Page.Action operation)
        {
            if (_pageChanged != null)
            {
                _pageChanged.Invoke(index, operation);
            }
        }

        // insert shape by shape name
        public virtual void HandleInsertShape(string shapeName)
        {
            Shape shape = _factory.CreateShape(shapeName);
            shape.SetFirstPoint(new Point(GenerateRandomNumber(0, CanvasSize.Width), GenerateRandomNumber(0, CanvasSize.Height)));
            shape.SetSecondPoint(new Point(GenerateRandomNumber(0, CanvasSize.Width), GenerateRandomNumber(0, CanvasSize.Height)));
            _commandManager.Execute(new AddCommand(this, shape, SlideIndex));
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert shape by shape name and coordinate
        public virtual void HandleInsertShape(string shapeName, Point firstPoint, Point secondPoint)
        {
            Shape shape = _factory.CreateShape(shapeName);
            shape.SetFirstPoint(firstPoint);
            shape.SetSecondPoint(secondPoint);
            shape.AdjustHandle();
            _commandManager.Execute(new AddCommand(this, shape, SlideIndex));
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert shape by shape for command pattern
        public virtual void InsertShape(Shape shape, int actionIndex)
        {
            if (actionIndex != SlideIndex)
            {
                SlideIndex = actionIndex;
                NotifyPageChanged(actionIndex, Page.Action.Switch);
            }
            shape.SetCanvasSize(CanvasSize);
            _slides[SlideIndex].AddShape(shape);
            NotifyModelChanged(EventArgs.Empty);
        }

        // set state 
        public virtual void SetState(IState state)
        {
            _state = state;
        }

        // handle remove shape
        public virtual void HandleRemoveShape(Shape shape)
        {
            _commandManager.Execute(new DeleteCommand(this, shape, SlideIndex));
            NotifyModelChanged(EventArgs.Empty);
        }

        // handle remove shape
        public virtual void HandleRemoveShape(int slideIndex, int index)
        {
            _commandManager.Execute(new DeleteCommand(this, _slides[slideIndex].Shapes[index], SlideIndex));
            NotifyModelChanged(EventArgs.Empty);
        }

        // remove shape by shape
        public virtual void RemoveShape(Shape shape, int actionIndex)
        {
            if (actionIndex != SlideIndex)
            {
                SlideIndex = actionIndex;
                NotifyPageChanged(actionIndex, Page.Action.Switch);
            }
            _slides[SlideIndex].Shapes.Remove(shape);
            NotifyModelChanged(EventArgs.Empty);
        }

        // set canvas coordinate
        public virtual void SetCanvasSize(Size canvasSize)
        {
            CanvasSize = canvasSize;
            foreach (Shape shape in _slides[SlideIndex].Shapes)
            {
                shape.SetCanvasSize(canvasSize);
            }
            NotifyModelChanged(EventArgs.Empty);
        }

        // handle move shape
        public virtual void HandleMoveShape(Point offset)
        {
            foreach (Shape shape in _slides[SlideIndex].Shapes)
            {
                if (shape.Selected)
                {
                    _commandManager.Execute(new MoveCommand(this, shape, offset, CanvasSize, SlideIndex));
                }
            }
            NotifyModelChanged(EventArgs.Empty);
        }

        // handle resize shape
        public virtual void HandleShapeResize(PointF firstPoint, PointF secondPoint)
        {
            foreach (Shape shape in _slides[SlideIndex].Shapes)
            {
                if (shape.Selected)
                {
                    _commandManager.Execute(new ResizeCommand(this, shape, firstPoint, secondPoint, SlideIndex));
                }
            }
            NotifyModelChanged(EventArgs.Empty);
        }

        // resize shape
        public virtual void ResizeShape(Shape shape, PointF firstPoint, PointF secondPoint, int commandSlideIndex)
        {
            if (commandSlideIndex != SlideIndex)
            {
                SlideIndex = commandSlideIndex;
                NotifyPageChanged(commandSlideIndex, Page.Action.Switch);
            }
            shape.SetFirstPoint(Point.Round(firstPoint));
            shape.SetSecondPoint(Point.Round(secondPoint));
            shape.AdjustHandle();
            NotifyModelChanged(EventArgs.Empty);
        }

        // move shape
        public virtual void MoveShape(Shape shape, Point offset, int commandIndex)
        {
            if (commandIndex != SlideIndex)
            {
                SlideIndex = commandIndex;
                NotifyPageChanged(commandIndex, Page.Action.Switch);
            }
            shape.Move(offset);
            NotifyModelChanged(EventArgs.Empty);
        }

        // Handle mouse down
        public virtual void HandleMouseDown(Point point)
        {
            _state.MouseDown(point);
        }

        // Handle mouse move
        public virtual void HandleMouseMove(Point point)
        {
            _state.MouseMove(point);
        }

        // Handle mouse up
        public virtual void HandleMouseUp(Point point)
        {
            _state.MouseUp(point);
        }

        // handle Key down
        public virtual void HandleKeyDown(Keys keyCode)
        {
            var oldCount = _slides[SlideIndex].Shapes.Count;
            _state.KeyDown(keyCode);
            if (oldCount == _slides[SlideIndex].Shapes.Count && keyCode == Keys.Delete)
            {
                _commandManager.Execute(new DeletePageCommand(this, SlideIndex, _slides[SlideIndex]));
            }
        }

        // draw shapes
        public virtual void Draw(IGraphics graphics)
        {
            DrawShapes(graphics);
            _state.Draw(graphics);
        }

        // draw all shapes
        public virtual void DrawShapes(IGraphics graphics)
        {
            foreach (Shape shape in _slides[SlideIndex].Shapes)
            {
                shape.Draw(graphics);
            }
        }

        // clear selected shape
        public virtual void ClearSelectedShape()
        {
            foreach (Shape shape in _slides[SlideIndex].Shapes)
            {
                shape.Selected = false;
            }
        }

        // draw hint
        public virtual void DrawHint(IGraphics graphics)
        {
            _hint.Draw(graphics);
        }

        // model change function
        public virtual void NotifyModelChanged(EventArgs e)
        {
            if (_modelChanged != null)
                _modelChanged(this, e);
        }

        // set hint type
        public virtual void SetHint(ShapeType type)
        {
            _hint = _factory.CreateShape(type);
        }

        // set hint's first point
        public virtual void SetHintFirstPoint(Point point)
        {
            _hint.SetFirstPoint(point);
        }

        // set hint's second point
        public virtual void SetHintSecondPoint(Point point)
        {
            _hint.SetSecondPoint(point);
            NotifyModelChanged(EventArgs.Empty);
        }

        // insert user drew shape by hint
        public virtual void AddShapeWithHint()
        {
            _commandManager.Execute(new DrawCommand(this, _hint, SlideIndex));
            NotifyModelChanged(EventArgs.Empty);
        }

        // handle add page
        public virtual void HandleAddPage(int newSlideIndex)
        {
            _commandManager.Execute(new AddPageCommand(this, newSlideIndex, _pageFactory.GetPage()));
            NotifyModelChanged(EventArgs.Empty);
        }

        // handle remove page
        public virtual void HandleDeletePage(int deletedSlideIndex)
        {
            _commandManager.Execute(new DeletePageCommand(this, deletedSlideIndex, _slides[deletedSlideIndex]));
        }

        // Add page
        public virtual void AddPage(int newSlideIndex, Page page)
        {
            _slides.Insert(newSlideIndex, page);
            SlideIndex = newSlideIndex;
        }

        // remove page
        public virtual void DeletePage(int deleteIndex, Page page)
        {
            if (SlideIndex == _slides.Count - 1)
            {
                SlideIndex--;
            }
            _slides.Remove(page);
        }

        // handle switch page
        public virtual void HandleSwitchPage(int newSlideIndex)
        {
            SlideIndex = newSlideIndex;
            NotifyPageChanged(newSlideIndex, Page.Action.Switch);
            NotifyModelChanged(EventArgs.Empty);
        }

        // redo
        public virtual void Redo()
        {
            _commandManager.Redo();
            NotifyModelChanged(EventArgs.Empty);
        }

        // undo
        public virtual void Undo()
        {
            _commandManager.Undo();
            NotifyModelChanged(EventArgs.Empty);
        }

        // get current shapes
        public virtual BindingList<Shape> GetCurrentShapes()
        {
            return _slides[SlideIndex].Shapes;
        }

        private readonly ShapeFactory _factory;
        private readonly PageFactory _pageFactory;
        private Pages _slides;
        private Shape _hint;
    }
}
