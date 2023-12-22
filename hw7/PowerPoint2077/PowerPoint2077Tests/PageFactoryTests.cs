using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class PageFactoryTests
    {
        PageFactory _pageFactory;
        [TestInitialize]
        public void Initialize()
        {
            _pageFactory = new PageFactory();
        }
        [TestMethod()]
        public void GetPageTest()
        {

        }
    }
}