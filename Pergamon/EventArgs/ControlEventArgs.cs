
using System;
using System.Windows.Controls;

namespace Pergamon
{
    public class ControlEventArgs : EventArgs
    {
        public Control control;

        public ControlEventArgs(Control control)
        {
            this.control = control;
        }
    }
}
