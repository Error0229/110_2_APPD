using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moq;
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
            Assert.AreEqual(_privateCircle.GetField("_name"), Constant.CIRCLE_CHINESE);
        }

        // test get info
        [TestMethod()]
        public void GetInfoTest()
        {
            _circle.SetFirstPoint(new Point(10, 10));
            _circle.SetSecondPoint(new Point(5, 5));
            Assert.AreEqual(_circle.GetInfo(), "(5, 5), (10, 10)");
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
            Assert.AreEqual(_handles.Count, 8);
            Assert.AreEqual(_handles[0].Type, HandleType.TopLeft);
            Assert.AreEqual(_handles[1].Type, HandleType.Top);
            Assert.AreEqual(_handles[2].Type, HandleType.TopRight);
            Assert.AreEqual(_handles[3].Type, HandleType.Left);
            Assert.AreEqual(_handles[4].Type, HandleType.Right);
            Assert.AreEqual(_handles[5].Type, HandleType.BottomLeft);
            Assert.AreEqual(_handles[6].Type, HandleType.Bottom);
            Assert.AreEqual(_handles[7].Type, HandleType.BottomRight);
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
