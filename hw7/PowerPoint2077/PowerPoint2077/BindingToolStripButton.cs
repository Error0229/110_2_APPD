using System.Windows.Forms;
using System.Drawing;
namespace WindowPowerPoint
{
    public class BindingToolStripButton : ToolStripButton, IBindableComponent
    {
        public BindingToolStripButton()
        {
            _dataBindings = new ControlBindingsCollection(this);
            BindingContext = new BindingContext();
        }
        private readonly ControlBindingsCollection _dataBindings;
        public ControlBindingsCollection DataBindings
        {
            get
            {
                return _dataBindings;
            }
        }
        public BindingContext BindingContext
        {
            get; set;
        }
    }
}
