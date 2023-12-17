using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Drawing;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class RectangleTests
    {
        Rectangle _rectangle;
        PrivateObject _privateRectangle;

        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _rectangle = new Rectangle();
            _privateRectangle = new PrivateObject(_rectangle);
        }

        // test constructor
        [TestMethod()]
        public void RectangleTest()
        {
            _rectangle = new Rectangle();
            _privateRectangle = new PrivateObject(_rectangle);
            Assert.AreEqual(Constant.RECTANGLE_CHINESE, _privateRectangle.GetField("_name"));
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            _rectangle.SetFirstPoint(new Point(10, 10));
            _rectangle.SetSecondPoint(new Point(5, 5));
            Assert.AreEqual("(5, 5), (10, 10)", _rectangle.GetInfo());
        }

        // test drawing
        [TestMethod()]
        public void DrawTest()
        {
            var _adapter = new Mock<IGraphics>();
            _rectangle.AdjustHandle();
            _rectangle.Selected = true;
            _rectangle.Draw(_adapter.Object);
            _adapter.Verify(adapter => adapter.DrawRectangle(It.IsAny<System.Drawing.Rectangle>()), Times.Once());
            _adapter.Verify(adapter => adapter.DrawHandle(It.IsAny<Point>()), Times.Exactly(8));
        }

        // adjust handles
        [TestMethod()]
        public void AdjustHandleTest()
        {
            _rectangle.SetFirstPoint(new Point(10, 10));
            _rectangle.SetSecondPoint(new Point(5, 5));
            _rectangle.AdjustHandle();
            var _handles = (List<Handle>)_privateRectangle.GetField("_handles");
            Assert.AreEqual(8, _handles.Count);
            Assert.AreEqual(new Point(5, 5), _handles[0].Position);
            Assert.AreEqual(new Point(7, 5), _handles[1].Position);
            Assert.AreEqual(new Point(10, 5), _handles[2].Position);
            Assert.AreEqual(new Point(5, 7), _handles[3].Position);
            Assert.AreEqual(new Point(10, 7), _handles[4].Position);
            Assert.AreEqual(new Point(5, 10), _handles[5].Position);
            Assert.AreEqual(new Point(7, 10), _handles[6].Position);
            Assert.AreEqual(new Point(10, 10), _handles[7].Position);
        }

        // draw handle
        [TestMethod()]
        public void DrawHandleTest()
        {
            var _adapter = new Mock<IGraphics>();
            _rectangle.AdjustHandle();
            _rectangle.Selected = true;
            _rectangle.DrawHandle(_adapter.Object);
            _adapter.Verify(adapter => adapter.DrawHandle(It.IsAny<Point>()), Times.Exactly(8));
        }
    }
}