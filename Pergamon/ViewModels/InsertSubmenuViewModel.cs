
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pergamon
{
    public class InsertSubmenuViewModel : BaseViewModel
    {

        #region Singleton

        public static InsertSubmenuViewModel Instance { get; set; } = new InsertSubmenuViewModel();

        #endregion

        #region Public commands

        public ICommand AttachFileCommand { get; set; }

        public ICommand InsertImageCommand { get; set; }

        public ICommand InsertLinkCommand { get; set; }

        #endregion
    }
}
