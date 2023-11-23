using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Moq;
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
            Assert.AreEqual(_privateLine.GetField("_name"), Constant.LINE_CHINESE);
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
            Assert.AreEqual(_handles.Count, 2);
            Assert.AreEqual(_handles[0].Type, HandleType.TopLeft);
            Assert.AreEqual(_handles[1].Type, HandleType.BottomRight);
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
            Assert.AreEqual(new Point(11, 11), _privateLine.GetField("_pointFirst"));
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
            Assert.AreEqual(new Point(11, 11), _privateLine.GetField("_pointSecond"));
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
