using System.Windows;
using System.Windows.Controls;

namespace Pergamon
{
    /// <summary>
    /// Interaction logic for SearchSection.xaml
    /// </summary>
    public partial class SearchSection : UserControl
    {
        public SearchSection()
        {
            //TODO: new vm every time...
            DataContext = new SearchSectionViewModel();
            InitializeComponent();
        }

        public Control ActionTarget
        {
            get { return (Control)GetValue(ActionTargetProperty); }
            set { SetValue(ActionTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionTargetProperty =
            DependencyProperty.Register("ActionTarget", typeof(Control), typeof(SearchSection), new PropertyMetadata(null));
    }
}
