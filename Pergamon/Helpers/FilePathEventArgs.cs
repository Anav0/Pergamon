
using System;

namespace Pergamon
{
    public class FilePathEventArgs : EventArgs
    {

        public string FilePath { get; set; }

        public FilePathEventArgs(string FilePath) => this.FilePath = FilePath;

    }
}
