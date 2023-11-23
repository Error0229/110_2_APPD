using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class ShapeTests
    {
        class TestShape : Shape
        {
            public TestShape(ShapeType type) : base()
            {

            }
            public TestShape() : base()
            {

            }
            public override void DrawHandle(IGraphics graphics)
            {
                // do something in subclasses
                _calledAbstractMethod = true;
            }
            public override void Draw(IGraphics graphic)
            {
                // do something in subclasses
                _calledAbstractMethod = true;
            }
            public override void AdjustHandle()
            {
                // do something in subclasses
                _handles = new List<Handle>();
                _handles.Add(new Handle { Type = HandleType.TopLeft, Position = new Point(0, 0) });
                _calledAbstractMethod = true;
            }
            private bool _calledAbstractMethod = false;
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

        // test constructoe
        [TestMethod()]
        public void ShapeTest()
        {
            _shape = new TestShape();
            _privateShape = new PrivateObject(_shape);
            Assert.IsNotNull(_privateShape.GetField("_pointFirst"));
            Assert.IsNotNull(_privateShape.GetField("_pointSecond"));
            Assert.IsNotNull(_privateShape.GetField("_name"));
            Assert.IsNotNull(_privateShape.GetField("_selectedHandleType"));
        }

        // test constructor with type
        [TestMethod()]
        public void ShapeTestWithType()
        {
            _shape = new TestShape(ShapeType.LINE);
            _privateShape = new PrivateObject(_shape);
            Assert.AreEqual(ShapeType.LINE, _privateShape.GetField("_type"));
        }


        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.AreEqual("(0, 0), (0, 0)", _shape.GetInfo());
        }

        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(string.Empty, _shape.GetShapeName());
        }

        [TestMethod()]
        public void SetFirstPointTest()
        {
            var p = new Point(10, 10);
            _shape.SetFirstPoint(p);
            Assert.AreEqual(p, _privateShape.GetField("_pointFirst"));
        }

        [TestMethod()]
        public void SetSecondPointTest()
        {
            var p = new Point(20, 20);
            _shape.SetSecondPoint(p);
            Assert.AreEqual(p, _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void MoveTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.Move(new Point(5, 5));
            Assert.AreEqual(new Point(15, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(25, 25), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustPointsTest()
        {
            var p1 = new Point(10, 10);
            var p2 = new Point(20, 20);
            _shape.SetFirstPoint(p2);
            _shape.SetSecondPoint(p1);
            _shape.AdjustPoints();
            Assert.AreEqual(p1, _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(p2, _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void IsInShapeTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            Assert.IsTrue(_shape.IsInShape(new Point(15, 15)));
            Assert.IsFalse(_shape.IsInShape(new Point(23, 23)));
        }

        [TestMethod()]
        public void DrawTest()
        {
            _shape.Draw(null);
            Assert.IsTrue((bool)_privateShape.GetField("_calledAbstractMethod"));
        }

        [TestMethod()]
        public void DrawHandleTest()
        {
            _shape.DrawHandle(null);
            Assert.IsTrue((bool)_privateShape.GetField("_calledAbstractMethod"));
        }

        [TestMethod()]
        public void AdjustHandleTest()
        {
            _shape.AdjustHandle();
            Assert.IsTrue((bool)_privateShape.GetField("_calledAbstractMethod"));
        }

        [TestMethod()]
        public void IsCloseToHandleTest()
        {
            _shape.AdjustHandle();
            Assert.AreEqual(HandleType.TopLeft, _shape.IsCloseToHandle(new Point(0, 0)));
        }

        [TestMethod()]
        public void SetSelectHandleTest()
        {
            _shape.SetSelectHandle(HandleType.TopLeft);
            Assert.AreEqual(HandleType.TopLeft, _privateShape.GetField("_selectedHandleType"));
        }

        [TestMethod()]
        public void AdjustByHandleTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.SetSelectHandle(HandleType.TopLeft);
            _shape.AdjustByHandle(new Point(15, 15));
            Assert.AreEqual(new Point(15, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(20, 20), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByTopLeftTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByTopLeft(new Point(15, 15));
            Assert.AreEqual(new Point(15, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(20, 20), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByTopTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByTop(new Point(15, 15));
            Assert.AreEqual(new Point(10, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(20, 20), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByTopRightTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByTopRight(new Point(15, 15));
            Assert.AreEqual(new Point(10, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(15, 20), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByLeftTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByLeft(new Point(15, 15));
            Assert.AreEqual(new Point(15, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(20, 20), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByRightTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByRight(new Point(15, 15));
            Assert.AreEqual(new Point(10, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(15, 20), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByBottomLeftTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByBottomLeft(new Point(15, 15));
            Assert.AreEqual(new Point(15, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(20, 15), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByBottomTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByBottom(new Point(15, 15));
            Assert.AreEqual(new Point(10, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(20, 15), _privateShape.GetField("_pointSecond"));
        }

        [TestMethod()]
        public void AdjustByBottomRightTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.AdjustByBottomRight(new Point(15, 15));
            Assert.AreEqual(new Point(10, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new Point(15, 15), _privateShape.GetField("_pointSecond"));
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
