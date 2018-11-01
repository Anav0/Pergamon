
using System.Windows.Input;

namespace Pergamon
{
    public class SearchSectionViewModel : BaseViewModel
    {
        #region Public Properties

        public bool IsVisible { get; set; }

        public string Phrase { get; set; }

        #endregion

        public SearchSectionViewModel()
        {
            CloseCommand = new RelayCommand(() => { IsVisible = false; });
        }
        
        #region Public Command

        public ICommand ShowOptionsCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand ReplaceCommand { get; set; }

        public ICommand ReplaceAllCommand { get; set; }

        public ICommand GoToPrevCommand { get; set; }

        public ICommand GoToNextCommand { get; set; }

        #endregion

        #region Private Commands

        public ICommand CloseCommand { get; private set; }

        #endregion
    }
}
