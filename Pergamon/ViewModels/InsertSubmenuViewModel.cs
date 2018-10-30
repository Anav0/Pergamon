
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Pergamon
{
    public class InsertSubmenuViewModel : BaseViewModel
    {

        #region Singleton

        public static InsertSubmenuViewModel Instance { get; set; } = new InsertSubmenuViewModel();

        #endregion

        public event EventHandler OnAttachFileAction;

        public event EventHandler OnInsertImage;

        public event EventHandler OnInsertLink;

        private InsertSubmenuViewModel()
        {
            AttachFileCommand = new RelayCommand(AttachFile);
            InsertImageCommand = new RelayCommand(InsertImage);
            InsertLinkCommand = new RelayCommand(InsertLink);
        }
       

        private void RaiseOnAttachFile(string path) => OnAttachFileAction?.Invoke(this, new FilePathArgs(path));

        private void RaiseOnInsertImage(string path) => OnInsertImage?.Invoke(this, new FilePathArgs(path));

        private void RaiseOnInsertLink() => OnInsertLink?.Invoke(this, new EventArgs());

        #region Public commands

        public ICommand AttachFileCommand { get; set; }

        public ICommand InsertImageCommand { get; set; }

        public ICommand InsertLinkCommand { get; set; }

        #endregion

        #region Command methods

        private void AttachFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select file to attach";

            if(dialog.ShowDialog() == true)
            {
                RaiseOnAttachFile(dialog.FileName);
            }

        }
        private void InsertImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select image to insert";
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == true)
            {
                RaiseOnInsertImage(dialog.FileName);
            }
            
        }

        private void InsertLink()
        {
            RaiseOnInsertLink();
        }

        #endregion
    }
}
