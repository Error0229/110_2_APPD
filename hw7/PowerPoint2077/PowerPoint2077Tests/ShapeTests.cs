using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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

        // test constructor
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

        // get info
        [TestMethod()]
        public void GetInfoTest()
        {
            Assert.AreEqual("(0, 0), (0, 0)", _shape.GetInfo());
            Assert.AreEqual("(0, 0), (0, 0)", _shape.Info);
        }

        // test get shape name
        [TestMethod()]
        public void GetShapeNameTest()
        {
            Assert.AreEqual(string.Empty, _shape.GetShapeName());
            Assert.AreEqual(string.Empty, _shape.Name);
        }

        // test set first point
        [TestMethod()]
        public void SetFirstPointTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            Assert.AreEqual(new PointF(10, 10), _privateShape.GetField("_pointFirst"));
        }

        // test set second point
        [TestMethod()]
        public void SetSecondPointTest()
        {
            _shape.SetSecondPoint(new Point(20, 20));
            Assert.AreEqual(new PointF(20, 20), _privateShape.GetField("_pointSecond"));
        }

        // test move
        [TestMethod()]
        public void MoveTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            _shape.Move(new Point(5, 5));
            Assert.AreEqual(new PointF(15, 15), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(25, 25), _privateShape.GetField("_pointSecond"));
        }

        // test adjust point
        [TestMethod()]
        public void AdjustPointsTest()
        {
            _shape.SetFirstPoint(new Point(20, 20));
            _shape.SetSecondPoint(new Point(10, 10));
            _shape.AdjustPoints();
            Assert.AreEqual(new PointF(10, 10), _privateShape.GetField("_pointFirst"));
            Assert.AreEqual(new PointF(20, 20), _privateShape.GetField("_pointSecond"));
        }

        // test is in shape
        [TestMethod()]
        public void IsInShapeTest()
        {
            _shape.SetFirstPoint(new Point(10, 10));
            _shape.SetSecondPoint(new Point(20, 20));
            Assert.IsTrue(_shape.IsInShape(new Point(15, 15)));
            Assert.IsFalse(_shape.IsInShape(new Point(23, 23)));
        }

        // test draw
        [TestMethod()]
        public void DrawTest()
        {
            _shape.Draw(null);
            Assert.IsTrue(_shape.calledAbstractMethod);
        }

        // test set canvas size
        [TestMethod()]
        public void SetCanvasSizeTest()
        {
            _shape.SetCanvasSize(new Size(100, 100));
            _shape.SetCanvasSize(new Size(200, 200));
            Assert.AreEqual(new Size(200, 200), _privateShape.GetField("_canvasSize"));
        }

        // test set 
    }
}
