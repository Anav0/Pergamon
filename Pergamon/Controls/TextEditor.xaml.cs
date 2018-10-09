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
            DataContext = new TextEditorViewModel();
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

        #region SelectedText

        public TextSelection SelectedText
        {
            get { return (TextSelection)GetValue(SelectedTextProperty); }
            set { SetValue(SelectedTextProperty, value); }
        }

        public static readonly DependencyProperty SelectedTextProperty =
            DependencyProperty.Register("SelectedText", typeof(TextSelection), typeof(TextEditor), new PropertyMetadata(null));

        #endregion

        #region DestiledSelectedText

        public string  DestiledSelectedText
        {
            get { return (string)GetValue(DestiledSelectedTextproperty); }
            set { SetValue(DestiledSelectedTextproperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedTextOnlyText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DestiledSelectedTextproperty =
            DependencyProperty.Register("DestiledSelectedText", typeof(string ), typeof(TextEditor), new PropertyMetadata(""));

        #endregion

        #region PlacholderText

        public string PlacholderText
        {
            get { return (string)GetValue(PlacholderTextProperty); }
            set { SetValue(PlacholderTextProperty, value); }
        }

        public static readonly DependencyProperty PlacholderTextProperty =
            DependencyProperty.Register("PlacholderText", typeof(string), typeof(TextEditor), new PropertyMetadata("Write your message..."));

        #endregion

        #region CaretPosition

        public TextPointer CaretPosition
        {
            get { return (TextPointer)GetValue(CaretPositionProperty); }
            set { SetValue(CaretPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaretPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaretPositionProperty =
            DependencyProperty.Register("CaretPosition", typeof(TextPointer), typeof(TextEditor), new PropertyMetadata(null));

        #endregion

        #region Event handlers

        private void editor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is RichTextBox richTextBox))
                return;

            SelectedText = richTextBox.Selection;
            CaretPosition = richTextBox.CaretPosition;
            DestiledSelectedText = SelectedText.Text;
        }

        #endregion

    }
}
