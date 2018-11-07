using Microsoft.Win32;
using Ninject;
using Nuntium.Core;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Pergamon
{
    public class InsertSubmenuViewModel : BaseViewModel
    {
        public InsertSubmenuViewModel()
        {
            InsertImageCommand = new RelayCommandWithParameter((param) => { InsertImage((RichTextBox)param); });
            InsertLinkCommand = new RelayCommandWithParameter((param) => { InsertLink((RichTextBox)param); });
            InsertFileCommand = new RelayCommand(() => { IoC.Kernel.Get<AttachmentsSectionViewModel>().InsertFileFromDialog(); });
        }

        #region Public commands

        public ICommand InsertFileCommand { get; private set; }

        public ICommand InsertImageCommand { get; private set; }

        public ICommand InsertLinkCommand { get; private set; }

        #endregion

        private void InsertImage(RichTextBox editor)
        {
            if (editor.Document == null)
                return;

            var dialog = new OpenFileDialog();
            dialog.Title = "Select image to insert";
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == true)
            {
                BitmapImage bimage = new BitmapImage();
                bimage.BeginInit();
                bimage.UriSource = new Uri(dialog.FileName, UriKind.Absolute);
                bimage.EndInit();

                var image = new System.Windows.Controls.Image { Source = bimage };
                editor.Document.InsertAdornedImage(image);
            }
        }

        private void InsertLink(RichTextBox editor)
        {
            var insertLinkPopup = new InsertLinkPopup();

            var existingLinks = editor.Selection.GetHyperlinksFromSelection();

            if (existingLinks == null || existingLinks.Count > 1)
                return;

            if (existingLinks.Count == 1)
                insertLinkPopup.Link = existingLinks[0]?.NavigateUri?.ToString();

            insertLinkPopup.TextToDisplay = editor.Selection.Text;

            var popup = new OffsetPopupFactory().CreatePopupOnPoint(editor.GetEditorPointToScreen());

            insertLinkPopup.AcceptCommand = new RelayCommand(() =>
            {
                if (string.IsNullOrEmpty(insertLinkPopup.Link) || string.IsNullOrEmpty(insertLinkPopup.TextToDisplay))
                    return;

                editor.Selection.Text = insertLinkPopup.TextToDisplay;

                var link = new BasicHyperLinkFactory().CreateHyperLinkOnTopOfSelectedText(editor.Selection, insertLinkPopup.Link);

                popup.IsOpen = false;

            });

            popup.Child = insertLinkPopup;
            popup.IsOpen = true;
        }

    }
}
