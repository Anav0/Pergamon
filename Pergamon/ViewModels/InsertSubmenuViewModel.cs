
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

        public event EventHandler OnAttachFileAction;

        public InsertSubmenuViewModel()
        {
            AttachFileCommand = new RelayCommand(AttachFile);
        }

        private void RaiseAttacheFilePathsChanged(string path) => OnAttachFileAction?.Invoke(this, new FilePathArgs(path));

        #region Public commands

        public ICommand AttachFileCommand { get; set; }

        #endregion

        #region Command methods

        private void AttachFile()
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Select file to attach";

            if(dialog.ShowDialog() == true)
            {
                RaiseAttacheFilePathsChanged(dialog.FileName);
            }

        }

        #endregion
    }
}
