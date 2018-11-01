
using System.Collections.ObjectModel;

namespace Pergamon
{
    public class FileRepListViewModel : BaseViewModel
    {
        public ObservableCollection<FileRepViewModel> Items { get; set; } = new ObservableCollection<FileRepViewModel>();
    }
}
