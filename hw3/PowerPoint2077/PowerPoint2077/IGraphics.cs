using System.Drawing;

namespace WindowPowerPoint
{
    public interface IGraphics
    {
        // draw line
        void DrawLine(Point pointFirst, Point pointSecond);

        // draw rectangle
        void DrawRectangle(System.Drawing.Rectangle rectangle);

        // draw circle
        void DrawCircle(System.Drawing.Rectangle rectangle);

        // clear all
        void ClearAll();
    }
}
