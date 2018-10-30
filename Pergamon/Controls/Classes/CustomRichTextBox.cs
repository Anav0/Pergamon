
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Pergamon
{
    public class CustomRichTextBox : RichTextBox
    {
        public CustomRichTextBox()
        {
            DataObject.AddPastingHandler(this, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            var data = Clipboard.GetImage();//e.SourceDataObject.GetData(DataFormats.Bitmap);

            if (data != null)
            {
                var image = new System.Windows.Controls.Image { Source = data, };

                Document.InsertAdornedImage(image);

                //Cancel so we can add image with hooked events instead
                e.CancelCommand();
            }
            
            
        }

        public static readonly DependencyProperty SelectedWordProperty =
       DependencyProperty.Register(
               "SelectedWord",
               typeof(string),
               typeof(CustomRichTextBox),
               new PropertyMetadata("")
               );

        public string SelectedWord
        {
            get { return (string)GetValue(SelectedWordProperty); }
            set { SetValue(SelectedWordProperty, value); }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            string wordBeforeCursor = CaretPosition.GetTextInRun(LogicalDirection.Backward).Split().Last();
            string wordAfterCursor = CaretPosition.GetTextInRun(LogicalDirection.Forward).Split().First();

            string text = wordBeforeCursor + wordAfterCursor;

            SelectedWord = string.Join("", text
                .Where(c => char.IsLetter(c))
                .ToArray());

            base.OnMouseUp(e);
        }
    }
}

