using System.Drawing;

namespace WindowPowerPoint
{
    class WindowsFormsGraphicsAdaptor : IGraphics
    {
        Graphics _graphics;
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            this._graphics = graphics;
        }

        // clear all
        public void ClearAll()
        {
            // OnPaint時會自動清除畫面，因此不需實作
        }

        // draw line
        public void DrawLine(Point pointFirst, Point pointSecond)
        {
            _graphics.DrawLine(Pens.Black, pointFirst, pointSecond);
        }

        // draw rectangle
        public void DrawRectangle(System.Drawing.Rectangle rectangle)
        {
            _graphics.DrawRectangle(Pens.Black, rectangle);
        }

        // draw circle
        public void DrawCircle(System.Drawing.Rectangle rectangle)
        {
            _graphics.DrawEllipse(Pens.Black, rectangle);
        }

    }
}