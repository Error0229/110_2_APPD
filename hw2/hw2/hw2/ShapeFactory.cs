﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowPowerPoint
{
    class ShapeFactory
    {
        // Create shape
        public Shape CreateShape(string shapeName)
        {
            switch (shapeName)
            {
                case Constant.RECTANGLE_CHINESE:
                    return new Rectangle();
                case Constant.LINE_CHINESE:
                    return new Line();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
