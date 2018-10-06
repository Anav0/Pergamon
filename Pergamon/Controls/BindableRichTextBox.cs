
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
            DependencyProperty.Register("Document", typeof(FlowDocument), typeof(BindableRichTextBox), new PropertyMetadata(null, new PropertyChangedCallback((d, e) =>
            {

            })));



    }
}
