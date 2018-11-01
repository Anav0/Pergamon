using System.Windows;
using System.Windows.Controls;

namespace Pergamon
{
    /// <summary>
    /// Interaction logic for InsertSubmenu.xaml
    /// </summary>
    public partial class OptionsSubmenu : UserControl
    {
        public OptionsSubmenu()
        {
            InitializeComponent();
        }

        public Control ActionTarget
        {
            get { return (Control)GetValue(ActionTargetProperty); }
            set { SetValue(ActionTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionTargetProperty =
            DependencyProperty.Register("ActionTarget", typeof(Control), typeof(OptionsSubmenu), new PropertyMetadata(null));
    }
}
