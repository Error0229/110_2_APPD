using System.Drawing;

namespace WindowPowerPoint
{
    public class WindowsFormsGraphicsAdaptor : IGraphics
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
        public virtual void DrawLine(Point pointFirst, Point pointSecond)
        {
            _graphics.DrawLine(Pens.Black, pointFirst, pointSecond);
        }

        // draw line handle
        public virtual void DrawLineHandle(Point pointFirst, Point pointSecond)
        {
            _graphics.DrawEllipse(Pens.Gray, pointFirst.X - (Constant.HANDLE_SIZE >> 1), pointFirst.Y - (Constant.HANDLE_SIZE >> 1), Constant.HANDLE_SIZE, Constant.HANDLE_SIZE);
            _graphics.DrawEllipse(Pens.Gray, pointSecond.X - (Constant.HANDLE_SIZE >> 1), pointSecond.Y - (Constant.HANDLE_SIZE >> 1), Constant.HANDLE_SIZE, Constant.HANDLE_SIZE);
        }

        // draw rectangle
        public virtual void DrawRectangle(System.Drawing.Rectangle rectangle)
        {
            _graphics.DrawRectangle(Pens.Black, rectangle);
        }

        // draw rectangle handle
        public virtual void DrawRectangleHandle(System.Drawing.Rectangle rectangle)
        {
            // draw out rectangle
            _graphics.DrawRectangle(Pens.Gray, rectangle);
            var two = (1 << 1);
            // draw 8 handles
            for (int i = 0; i < ((1 << (two + 1)) + 1/* lord forgive me 🗿*/); i++)
            {
                if (i == (1 << two))
                    continue; // skip center handle (index = 4)
                int x = rectangle.X - (Constant.HANDLE_SIZE >> 1) + (i % (1 + two) /* lord forgive me 🗿*/) * (rectangle.Width >> 1);
                int y = rectangle.Y - (Constant.HANDLE_SIZE >> 1) + (i / (1 + two) /* lord forgive me 🗿*/) * (rectangle.Height >> 1);
                _graphics.DrawEllipse(Pens.Gray, x, y, Constant.HANDLE_SIZE, Constant.HANDLE_SIZE);
            }
        }

        // draw circle
        public virtual void DrawCircle(System.Drawing.Rectangle rectangle)
        {
            _graphics.DrawEllipse(Pens.Black, rectangle);

        }

        // draw circle handle
        public virtual void DrawCircleHandle(System.Drawing.Rectangle rectangle)
        {
            DrawRectangleHandle(rectangle);
        }
    }
}
