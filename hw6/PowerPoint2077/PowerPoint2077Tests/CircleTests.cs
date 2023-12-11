using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Drawing;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class CircleTests
    {
        Circle _circle;
        PrivateObject _privateCircle;

        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _circle = new Circle();
            _privateCircle = new PrivateObject(_circle);
        }

        // test constructor
        [TestMethod()]
        public void CircleTest()
        {
            _circle = new Circle();
            _privateCircle = new PrivateObject(_circle);
            Assert.AreEqual(Constant.CIRCLE_CHINESE, _privateCircle.GetField("_name"));
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            _circle.SetFirstPoint(new Point(10, 10));
            _circle.SetSecondPoint(new Point(5, 5));
            Assert.AreEqual("(5, 5), (10, 10)", _circle.GetInfo());
        }

        // test drawing
        [TestMethod()]
        public void DrawTest()
        {
            var _adapter = new Mock<IGraphics>();
            _circle.AdjustHandle();
            _circle.Selected = true;
            _circle.Draw(_adapter.Object);
            _adapter.Verify(adapter => adapter.DrawCircle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
            _adapter.Verify(adapter => adapter.DrawHandle(It.IsAny<Point>()), Times.Exactly(8));
        }

        // adjust handles
        [TestMethod()]
        public void AdjustHandleTest()
        {
            _circle.AdjustHandle();
            var _handles = (List<Handle>)_privateCircle.GetField("_handles");
            Assert.AreEqual(8, _handles.Count);
            Assert.AreEqual(HandleType.TopLeft, _handles[0].Type);
            Assert.AreEqual(HandleType.Top, _handles[1].Type);
            Assert.AreEqual(HandleType.TopRight, _handles[2].Type);
            Assert.AreEqual(HandleType.Left, _handles[3].Type);
            Assert.AreEqual(HandleType.Right, _handles[4].Type);
            Assert.AreEqual(HandleType.BottomLeft, _handles[5].Type);
            Assert.AreEqual(HandleType.Bottom, _handles[6].Type);
            Assert.AreEqual(HandleType.BottomRight, _handles[7].Type);
        }

        // adjust by handle
        [TestMethod()]
        public void DrawHandleTest()
        {
            var _adapter = new Mock<IGraphics>();
            _circle.AdjustHandle();
            _circle.Selected = true;
            _circle.DrawHandle(_adapter.Object);
            _adapter.Verify(adapter => adapter.DrawHandle(It.IsAny<Point>()), Times.Exactly(8));
        }
    }
}