using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowPowerPoint
{
    interface IState
    {
        // handle mouse down
        void mouseDown();

        // handle mouse move
        void mouseMove();

        // handle mouse up
        void mouseUp();


    }
}
