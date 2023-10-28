using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace WindowPowerPoint
{
    public interface IState
    {
        // handle mouse down
        void MouseDown(Point point);

        // handle mouse move
        void MouseMove(Point point);

        // handle mouse up
        void MouseUp(Point point);

        // handle Draw
        void Draw(IGraphics graphics);

        // handle key down
        void KeyDown(Keys keyCode);

    }
}
