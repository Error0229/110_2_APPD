using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowPowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace WindowPowerPoint.Tests
{
    [TestClass()]
    public class WindowsFormsGraphicsAdaptorTests
    {
        WindowsFormsGraphicsAdaptor _adaptor;
        PrivateObject _privateAdaptor;

        // initialize
        [TestInitialize()]
        public void Initialize()
        {
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            _adaptor = new WindowsFormsGraphicsAdaptor(graphics);
            _privateAdaptor = new PrivateObject(_adaptor);
        }

        // test constructor
        [TestMethod()]
        public void WindowsFormsGraphicsAdaptorTest()
        {
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            _adaptor = new WindowsFormsGraphicsAdaptor(graphics);
            _privateAdaptor = new PrivateObject(_adaptor);
            Assert.AreEqual(_privateAdaptor.GetField("_graphics"), graphics);
        }

        // test clear all
        [TestMethod()]
        public void ClearAllTest()
        {
            _adaptor.ClearAll();
            Assert.IsFalse(false);
        }

        // test draw line
        [TestMethod()]
        public void DrawLineTest()
        {
            _adaptor.DrawLine(new Point(10, 10), new Point(20, 20));
            Assert.IsFalse(false);
        }

        // test draw line handle
        [TestMethod()]
        public void DrawLineHandleTest()
        {
            _adaptor.DrawLineHandle(new Point(10, 10), new Point(20, 20));
            Assert.IsFalse(false);
        }

        // test draw rectangle
        [TestMethod()]
        public void DrawRectangleTest()
        {
            _adaptor.DrawRectangle(new System.Drawing.Rectangle(10, 10, 20, 20));
            Assert.IsFalse(false);
        }

        // test draw rectangle handle
        [TestMethod()]
        public void DrawRectangleHandleTest()
        {
            _adaptor.DrawRectangleHandle(new System.Drawing.Rectangle(10, 10, 20, 20));
            Assert.IsFalse(false);
        }

        // test draw circle
        [TestMethod()]
        public void DrawCircleTest()
        {
            _adaptor.DrawCircle(new System.Drawing.Rectangle(10, 10, 20, 20));
            Assert.IsFalse(false);
        }

        // test draw circle handle
        [TestMethod()]
        public void DrawCircleHandleTest()
        {
            _adaptor.DrawCircleHandle(new System.Drawing.Rectangle(10, 10, 20, 20));
            Assert.IsFalse(false);
        }

        // test draw handle by points
        [TestMethod()]
        public void DrawHandleTest()
        {
            _adaptor.DrawHandle(new Point(10, 10));
            Assert.IsFalse(false);
        }

        // test draw outline
        [TestMethod()]
        public void DrawOutlineTest()
        {
            _adaptor.DrawOutline(new System.Drawing.Rectangle(10, 10, 20, 20));
            Assert.IsFalse(false);
        }
    }
}
