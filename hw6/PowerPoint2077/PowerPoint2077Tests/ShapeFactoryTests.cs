using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class ShapeFactoryTests
    {
        ShapeFactory _shapeFactory;
        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            _shapeFactory = new ShapeFactory();
        }

        // test create shape
        [TestMethod()]
        public void CreateShapeTest()
        {
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(Constant.RECTANGLE_CHINESE), typeof(Rectangle));
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(Constant.LINE_CHINESE), typeof(Line));
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(Constant.CIRCLE_CHINESE), typeof(Circle));
            Assert.ThrowsException<ArgumentException>(() => _shapeFactory.CreateShape("Triangle"));
        }

        // test create shape with type
        [TestMethod()]
        public void CreateShapeWithTypeTest()
        {
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(ShapeType.RECTANGLE), typeof(Rectangle));
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(ShapeType.LINE), typeof(Line));
            Assert.IsInstanceOfType(_shapeFactory.CreateShape(ShapeType.CIRCLE), typeof(Circle));
            Assert.ThrowsException<ArgumentException>(() => _shapeFactory.CreateShape((ShapeType)3));
        }
    }
}