
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Pergamon
{
    public class BindableRichTextBox : RichTextBox
    {

        public new FlowDocument Document
        {
            get { return (FlowDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Document.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(FlowDocument), typeof(BindableRichTextBox));


        //TODO: Violating MVVM principals
        //protected override void OnTextInput(TextCompositionEventArgs e)
        //{
        //    var vm = DataContext as TextEditorViewModel;

        //    if (vm == null)
        //    {
        //        base.OnTextInput(e);
        //        return;
        //    }

        //    Selection.Text = "";

        //    Run run = new Run(e.Text, CaretPosition.DocumentEnd);

        //    if(vm.SelectedFontFamily != null)
        //        run.FontFamily = vm.SelectedFontFamily;

        //    run.FontSize = vm.SelectedFontSize;

        //    CaretPosition = run.ElementEnd;
        //}

    }
}
