using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowPowerPoint
{
    public class PowerPointPresentationModel
    {
        public PowerPointPresentationModel(PowerPointModel model)
        {
            this._model = model;
            _isEllipseChecked = false;
            _isLineChecked = false;
            _isRectangleChecked = false;
        }
        // insert shape
        public void InsertShape(string shapeName)
        {
            _model.InsertShape(shapeName);
        }

        // get shape
        public void RemoveShape(int index)
        {
            _model.RemoveShape(index);
        }
    
        public void ProcessLineClicked()
        {
            _isLineChecked = true;
            _isEllipseChecked = _isRectangleChecked = false;
        }
        public void ProcessEllipseClicked()
        {
            _isEllipseChecked = true;
            _isLineChecked = _isRectangleChecked = false;
        }
        public void ProcessRectangleClicked()
        {
            _isRectangleChecked = true;
            _isLineChecked = _isEllipseChecked = false;
        }
        public bool IsEliipseChecked()
        {
            return _isEllipseChecked;
        }
        public bool IsLineChecked()
        {
            return _isLineChecked;
        }
        public bool IsRectangleChecked()
        {
            return _isRectangleChecked;
        }
        public List<Shape> Shapes {
            get
            {
                return _model.Shapes;
            }
        }
        private bool _isLineChecked;
        private bool _isEllipseChecked;
        private bool _isRectangleChecked;
        private PowerPointModel _model;
    }
}
