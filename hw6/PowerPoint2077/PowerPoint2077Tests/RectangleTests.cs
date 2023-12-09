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
            Assert.AreEqual(_privateRectangle.GetField("_name"), Constant.RECTANGLE_CHINESE);
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            _rectangle.SetFirstPoint(new Point(10, 10));
            _rectangle.SetSecondPoint(new Point(5, 5));
            Assert.AreEqual(_rectangle.GetInfo(), "(5, 5), (10, 10)");
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
            Assert.AreEqual(_handles.Count, 8);
            Assert.AreEqual(_handles[0].Position, new Point(5, 5));
            Assert.AreEqual(_handles[1].Position, new Point(7, 5));
            Assert.AreEqual(_handles[2].Position, new Point(10, 5));
            Assert.AreEqual(_handles[3].Position, new Point(5, 7));
            Assert.AreEqual(_handles[4].Position, new Point(10, 7));
            Assert.AreEqual(_handles[5].Position, new Point(5, 10));
            Assert.AreEqual(_handles[6].Position, new Point(7, 10));
            Assert.AreEqual(_handles[7].Position, new Point(10, 10));
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