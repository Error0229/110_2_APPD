using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;

namespace WindowPowerPoint.Tests
{
    [TestClass]
    public class ShapeHandleTests
    {
        class TestShape : Shape
        {
            public TestShape(ShapeType type) : base()
            {

            }

            public TestShape() : base()
            {

            }

            // draw handle
            public override void DrawHandle(IGraphics graphics)
            {
                // do something in subclasses
                calledAbstractMethod = true;
            }

            // draw
            public override void Draw(IGraphics graphic)
            {
                // do something in subclasses
                calledAbstractMethod = true;
            }

            // adjust handle
            public override void AdjustHandle()
            {
                // do something in subclasses
                _handles = new List<Handle>();
                _handles.Add(new Handle { Type = HandleType.TopLeft, Position = new Point(0, 0) });
                _handles.Add(new Handle { Type = HandleType.BottomRight, Position = new Point(100, 100) });
                calledAbstractMethod = true;
            }
            public bool calledAbstractMethod;
        }
        TestShape _shape;
        PrivateObject _privateShape;

        // initialize 
        [TestInitialize()]
        public void Initialize()
        {
            _shape = new TestShape();

            _privateShape = new PrivateObject(_shape);
        }


        // test draw handle
        [TestMethod()]
        public void DrawHandleTest()
        {
            _shape.DrawHandle(null);
            Assert.IsTrue(_shape.calledAbstractMethod);
        }

        // test adjust handle and generate handle
        [TestMethod()]
        public void AdjustHandleTest()
        {
            _shape.AdjustHandle();
            Assert.IsTrue(_shape.calledAbstractMethod);
        }

        // test the point is close to handle or not
        [TestMethod()]
        public void IsCloseToHandleTest()
        {
            _shape.AdjustHandle();
            Assert.AreEqual(HandleType.TopLeft, _shape.IsCloseToHandle(new Point(0, 0)));
            Assert.AreEqual(HandleType.None, _shape.IsCloseToHandle(new Point(50, 50)));
        }

        // test Set selected handle
        [TestMethod()]
        public void SetSelectHandleTest()
        {
            _shape.SetSelectHandle(HandleType.TopLeft);
            Assert.AreEqual(HandleType.TopLeft, _privateShape.GetField("_selectedHandleType"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByHandleTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.SetSelectHandle(HandleType.TopLeft);
            _shape.AdjustByHandle(new Point(15, 15));
            Assert.AreEqual(new PointF(15, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(20, 20), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByTopLeftTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByTopLeft(new Point(15, 15));
            Assert.AreEqual(new PointF(15, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(20, 20), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByTopTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByTop(new Point(15, 15));
            Assert.AreEqual(new PointF(10, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(20, 20), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByTopRightTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByTopRight(new Point(15, 15));
            Assert.AreEqual(new PointF(10, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(15, 20), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByLeftTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByLeft(new Point(15, 15));
            Assert.AreEqual(new PointF(15, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(20, 20), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByRightTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByRight(new Point(15, 15));
            Assert.AreEqual(new PointF(10, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(15, 20), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByBottomLeftTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByBottomLeft(new Point(15, 15));
            Assert.AreEqual(new PointF(15, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(20, 15), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByBottomTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByBottom(new Point(15, 15));
            Assert.AreEqual(new PointF(10, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(20, 15), _privateShape.GetField("_pointSecond"));
        }

        // test adjust by handle
        [TestMethod()]
        public void AdjustByBottomRightTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByBottomRight(new Point(15, 15));
            Assert.AreEqual(new PointF(10, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(15, 15), _privateShape.GetField("_pointSecond"));
        }

        // try adjust while shape selected
        [TestMethod()]
        public void TryAdjustWhenMouseDownSelectedTest()
        {
            _shape.SetFirstPoint(new Point(0, 0));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustHandle();
            _shape.Selected = true;
            bool flag;
            HandleType type;
            Assert.IsTrue(_shape.TryAdjustWhenMouseDown(new Point(0, 0), out flag, out type));
        }

        // test try adjust while shape not selected
        [TestMethod()]
        public void TryAdjustWhenMouseDownNotSelectedTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            bool flag;
            HandleType type;
            Assert.IsFalse(_shape.TryAdjustWhenMouseDown(new Point(15, 15), out flag, out type));
        }

        // test try adjust while shape selected
        [TestMethod()]
        public void TryAdjustWhenMouseMoveSelectedTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.Selected = true;
            _shape.SetSelectHandle(HandleType.TopLeft);
            Assert.IsTrue(_shape.TryAdjustWhenMouseMove(new Point(15, 15)));
        }

        // test try adjust while shape not selected
        [TestMethod()]
        public void TryAdjustWhenMouseMoveNotSelectedTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            Assert.IsFalse(_shape.TryAdjustWhenMouseMove(new Point(15, 15)));
        }
    }
}

