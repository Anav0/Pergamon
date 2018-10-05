using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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

        #region DocumentRTF attached property

        public static FlowDocument DocumentRTF(DependencyObject obj)
        {
            return (FlowDocument)obj.GetValue(DocumentRTFProperty);
        }

        public static void SetDocumentRTF(DependencyObject obj, FlowDocument value)
        {
            obj.SetValue(DocumentRTFProperty, value);
        }

        public static readonly DependencyProperty DocumentRTFProperty =
            DependencyProperty.RegisterAttached("DocumentRTF", typeof(FlowDocument), typeof(TextEditor), new PropertyMetadata(null, new PropertyChangedCallback((d, e) => {
                
                if (!(d is RichTextBox richtextBox))
                    return;

                SetDocumentRTF(d, richtextBox.Document);

            })));

        #endregion

        #region Event handlers

        private void editor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is RichTextBox richTextBox))
                return;

            SelectedText = richTextBox.Selection;
        }

        #endregion
    }
}
