using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Pergamon
{
    /// <summary>
    /// Interaction logic for TextEditor.xaml
    /// </summary>
    public partial class TextEditor : UserControl
    {
        public TextEditor()
        {
            InitializeComponent();
            var vm = new TextEditorViewModel();
            DataContext = vm;
        }

        #region BarBackground

        public Brush BarBackground
        {
            get { return (Brush)GetValue(BarBackgroundProperty); }
            set { SetValue(BarBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BarBackgroundProperty =
            DependencyProperty.Register("BarBackground", typeof(Brush), typeof(TextEditor), new PropertyMetadata(new Control().FindResource("DefaultTabBackgroundColorBrush")));

        #endregion

        #region BarForeground

        public Brush BarForeground
        {
            get { return (Brush)GetValue(BarForegroundProperty); }
            set { SetValue(BarForegroundProperty, value); }
        }

        public static readonly DependencyProperty BarForegroundProperty =
            DependencyProperty.Register("BarForeground", typeof(Brush), typeof(TextEditor), new PropertyMetadata(new Control().FindResource("DefaultTabForegroundColorBrush")));

        #endregion

        #region BarPlacement

        public Dock BarPlacement
        {
            get { return (Dock)GetValue(BarPlacementProperty); }
            set
            {
                SetValue(BarPlacementProperty, value);
            }
        }
        public static readonly DependencyProperty BarPlacementProperty =
            DependencyProperty.Register("BarPlacement", typeof(Dock), typeof(TextEditor), new PropertyMetadata(Dock.Top));

        #endregion

        private void editor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is RichTextBox editor))
                return;

            var vm = DataContext as TextEditorViewModel;

            if (vm == null)
                return;

            vm.CaretPosition = editor.CaretPosition;
            vm.SelectedText = editor.Selection;

        }
    }
}
