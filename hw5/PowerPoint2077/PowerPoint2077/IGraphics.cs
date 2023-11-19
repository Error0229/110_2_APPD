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

        // draw line handle
        void DrawLineHandle(Point pointFirst, Point pointSecond);

        // draw rectangle handle
        void DrawRectangleHandle(System.Drawing.Rectangle rectangle);

        // draw circle handle
        void DrawCircleHandle(System.Drawing.Rectangle rectangle);

        // draw handle
        void DrawHandle(Point point);

        // draw Outline
        void DrawOutline(System.Drawing.Rectangle rectangle);

        // clear all
        void ClearAll();
    }
}
