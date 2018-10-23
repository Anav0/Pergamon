
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

        private InsertSubmenuViewModel()
        {
            AttachFileCommand = new RelayCommand(AttachFile);
            InsertImageCommand = new RelayCommand(InsertImage);
        }

        private void RaiseAttachFilePathsChanged(string path) => OnAttachFileAction?.Invoke(this, new FilePathArgs(path));

        private void RaiseInsertImageAction(string path) => OnInsertImage?.Invoke(this, new FilePathArgs(path));

        #region Public commands

        public ICommand AttachFileCommand { get; set; }

        public ICommand InsertImageCommand { get; set; }

        #endregion

        #region Command methods

        private void AttachFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select file to attach";

            if(dialog.ShowDialog() == true)
            {
                RaiseAttachFilePathsChanged(dialog.FileName);
            }

        }
        private void InsertImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select image to insert";
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (dialog.ShowDialog() == true)
            {
                RaiseInsertImageAction(dialog.FileName);
            }
            else
            {

            }
        }

        #endregion
    }
}
