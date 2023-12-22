using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PageTests
    {
        Page _page;
        [TestInitialize]
        public void Initialize()
        {
            _page = new Page();
        }
        [TestMethod()]
        public void PageTest()
        {
            _page = new Page();
            Assert.IsNotNull(_page.Shapes);
        }

        [TestMethod()]
        public void AddShapeTest()
        {
            var shape = new Mock<Shape>();
            _page.AddShape(shape.Object);
            Assert.AreEqual(1, _page.Shapes.Count);
        }

        [TestMethod()]
        public void RemoveShapeTest()
        {
            var shape = new Mock<Shape>();
            _page.AddShape(shape.Object);
            _page.RemoveShape(shape.Object);
            Assert.AreEqual(0, _page.Shapes.Count);
        }

        [TestMethod()]
        public void RemoveShapeTest1()
        {
            var shape = new Mock<Shape>();
            _page.AddShape(shape.Object);
            _page.RemoveShape(0);
            Assert.AreEqual(0, _page.Shapes.Count);
        }
    }
}