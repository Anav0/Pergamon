
using System;
using System.Windows.Media.Imaging;

namespace Pergamon
{
    public class FileRepDesign : FileRepViewModel
    {
        public static FileRepDesign Instance => new FileRepDesign();


        public FileRepDesign()
        {
            FileName = "flying_cat.psd";
            FileSize = 25000000;

            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("Assets/fileIcon.png", UriKind.Relative);
            src.EndInit();

            FileIcon = src;
        }
    }
}
