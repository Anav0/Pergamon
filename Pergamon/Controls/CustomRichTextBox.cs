
using System;
using System.Drawing;
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
            var data = e.SourceDataObject.GetData(DataFormats.Bitmap);

            if (data != null)
            {
                var image = new System.Windows.Controls.Image { Source = (ImageSource)data, };

                ImageHelpers.InsertImageWithHookedEvents(image, Document);

                //Cancel so we can add image with hooked events instead
                e.CancelCommand();
            }
            
        }
    }
}
