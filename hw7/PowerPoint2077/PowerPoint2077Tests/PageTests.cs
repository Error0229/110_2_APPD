using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Drawing;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PageTests
    {
        Page _page;

        // initialize
        [TestInitialize]
        public void Initialize()
        {
            _page = new Page();
        }

        // test constructor
        [TestMethod()]
        public void PageTest()
        {
            _page = new Page();
            Assert.IsNotNull(_page.Shapes);
        }

        // test add shape
        [TestMethod()]
        public void AddShapeTest()
        {
            var shape = new Mock<Shape>();
            _page.AddShape(shape.Object);
            Assert.AreEqual(1, _page.Shapes.Count);
        }

        // test remove shape
        [TestMethod()]
        public void RemoveShapeTest()
        {
            var shape = new Mock<Shape>();
            _page.AddShape(shape.Object);
            _page.RemoveShape(shape.Object);
            Assert.AreEqual(0, _page.Shapes.Count);
        }

        // test remove shape by index
        [TestMethod()]
        public void RemoveShapeTest1()
        {
            var shape = new Mock<Shape>();
            _page.AddShape(shape.Object);
            _page.RemoveShape(0);
            Assert.AreEqual(0, _page.Shapes.Count);
        }

        // test encode page
        [TestMethod()]
        public void ConvertTest()
        {
            var circle = new Circle();
            circle.SetFirstPoint(new Point(0, 0));
            circle.SetSecondPoint(new Point(50, 50));
            circle.CanvasSize = new Size(100, 100);
            _page.AddShape(circle);
            Assert.AreEqual("{CIRCLE,(0,0,0.5,0.5)} ", _page.Convert());
        }

        // test decode page
        [TestMethod()]
        public void InterpretTest()
        {
            string data = "{CIRCLE,(0,0,0,0)} ";
            _page.Interpret(data, new Size(100, 100));
            Assert.AreEqual(1, _page.Shapes.Count);
        }

        // test interpret circle
        [TestMethod()]
        public void InterpretCircleTest()
        {
            var type = Constant.CIRCLE;
            var coordinate = "0,0,0.5,0.5";
            var canvasSize = new Size(100, 100);
            var shape = _page.InterpretShape(type, coordinate, canvasSize);
            Assert.AreEqual(new PointF(0, 0), shape.GetFirstPoint());
            Assert.AreEqual(new PointF(50, 50), shape.GetSecondPoint());
        }

        // test interpret rectangle
        [TestMethod()]
        public void InterpretRectangleTest()
        {
            var type = Constant.RECTANGLE;
            var coordinate = "0,0,0.5,0.5";
            var canvasSize = new Size(100, 100);
            var shape = _page.InterpretShape(type, coordinate, canvasSize);
            Assert.AreEqual(new PointF(0, 0), shape.GetFirstPoint());
            Assert.AreEqual(new PointF(50, 50), shape.GetSecondPoint());
        }

        // test interpret line
        [TestMethod()]
        public void InterpretLineTest()
        {
            var type = Constant.LINE;
            var coordinate = "0,0,0.5,0.5";
            var canvasSize = new Size(100, 100);
            var shape = _page.InterpretShape(type, coordinate, canvasSize);
            Assert.AreEqual(new PointF(0, 0), shape.GetFirstPoint());
            Assert.AreEqual(new PointF(50, 50), shape.GetSecondPoint());
        }

        // test interpret exception
        [TestMethod()]
        public void InterpretExceptionTest()
        {
            var type = "test";
            var coordinate = "0,0,0.5,0.5";
            var canvasSize = new Size(100, 100);
            Assert.ThrowsException<System.Exception>(() => _page.InterpretShape(type, coordinate, canvasSize));
        }
    }
}