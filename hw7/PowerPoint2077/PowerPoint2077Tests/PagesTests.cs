using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PagesTests
    {
        Pages _pages;


        // initialize
        [TestInitialize]
        public void Initialize()
        {
            _pages = new Pages();
        }

        // test constructor
        [TestMethod()]
        public void PagesTest()
        {
            _pages = new Pages();
            Assert.IsNotNull(_pages);
        }

        [TestMethod()]
        public void InsertTest()
        {
            var _page = new Mock<Page>();
            _pages.Insert(0, _page.Object);
            Assert.AreEqual(1, _pages.Count);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            var _page = new Mock<Page>();
            _pages.Insert(0, _page.Object);
            _pages.Remove(_page.Object);
            Assert.AreEqual(0, _pages.Count);
        }

        [TestMethod()]
        public void AddTest()
        {
            var _page = new Mock<Page>();
            _pages.Add(_page.Object);
            Assert.AreEqual(1, _pages.Count);
        }

        [TestMethod()]
        public void ConvertTest()
        {
            var _page = new Mock<Page>();
            _page.Setup(x => x.Convert()).Returns("test");
            _pages.Add(_page.Object);
            Assert.AreEqual("test\n", _pages.Convert());
        }
    }
}