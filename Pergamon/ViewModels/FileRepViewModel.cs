
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pergamon
{
    public class FileRepViewModel : BaseViewModel
    {

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public ImageSource FileIcon { get; set; }


        public FileRepViewModel()
        {
            DeleteFileCommand = new RelayCommand(DeleteFile);
        }


        public ICommand DeleteFileCommand { get; set; }

        private void DeleteFile()
        {
        }
    }
}
