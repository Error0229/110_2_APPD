using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WindowPowerPoint
{
    class PointState : IState
    {
        private PowerPointModel _model;
        constructor(PowerPointModel model)
        {
            _model = model;
        }
    }
}
