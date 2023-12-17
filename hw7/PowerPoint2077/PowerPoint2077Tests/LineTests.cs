using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Drawing;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class LineTests
    {
        Line _line;
        PrivateObject _privateLine;
        // initialize 
        [TestInitialize()]
        public void Initialize()
        {
            _line = new Line();
            _privateLine = new PrivateObject(_line);
        }

        // test constructor
        [TestMethod()]
        public void LineTest()
        {
            _line = new Line();
            _privateLine = new PrivateObject(_line);
            Assert.AreEqual(Constant.LINE_CHINESE, _privateLine.GetField("_name"));
        }

        // test drawing
        [TestMethod()]
        public void DrawTest()
        {
            var _adapter = new Mock<IGraphics>();
            _line.AdjustHandle();
            _line.Selected = true;
            _line.Draw(_adapter.Object);
            _adapter.Verify(adapter => adapter.DrawLine(It.IsAny<Point>(), It.IsAny<Point>()), Times.Once());
            _adapter.Verify(adapter => adapter.DrawHandle(It.IsAny<Point>()), Times.Exactly(2));
        }

        // test generate handle
        [TestMethod()]
        public void AdjustHandleTest()
        {
            _line.AdjustHandle();
            var _handles = (List<Handle>)_privateLine.GetField("_handles");
            Assert.AreEqual(2, _handles.Count);
            Assert.AreEqual(HandleType.TopLeft, _handles[0].Type);
            Assert.AreEqual(HandleType.BottomRight, _handles[1].Type);
        }

        // test adjust by top left
        [TestMethod()]
        public void AdjustByHandleTopLeftTest()
        {
            _line.SetFirstPoint(new Point(10, 10));
            _line.SetSecondPoint(new Point(20, 20));
            _line.AdjustHandle();
            _line.SetSelectHandle(HandleType.TopLeft);
            _line.AdjustByHandle(new Point(11, 11));
            Assert.AreEqual(new PointF(11, 11), _privateLine.GetField("_pointFirst"));
        }

        // test adjust by bottom right
        [TestMethod()]
        public void AdjustByHandleBottomRightTest()
        {
            _line.SetFirstPoint(new Point(10, 10));
            _line.SetSecondPoint(new Point(20, 20));
            _line.AdjustHandle();
            _line.SetSelectHandle(HandleType.BottomRight);
            _line.AdjustByHandle(new Point(11, 11));
            Assert.AreEqual(new PointF(11, 11), _privateLine.GetField("_pointSecond"));
        }

        // test draw handle
        [TestMethod()]
        public void DrawHandleTest()
        {
            var _adapter = new Mock<IGraphics>();
            _line.AdjustHandle();
            _line.DrawHandle(_adapter.Object);
            _adapter.Verify(adapter => adapter.DrawHandle(It.IsAny<Point>()), Times.Exactly(2));
        }
    }
}
