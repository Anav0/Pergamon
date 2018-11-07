using System.Windows.Documents;
using System.Windows.Input;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {
        #region Public properties

        public MenuCategories SelectedMenu { get; set; }

        public bool IsSearchSectionVisible { get; set; }

        public double LineSpacing { get; set; }

        #endregion

        public TextEditorViewModel()
        {
            SendEmailCommand = new RelayCommand(SendEmail);
            DisplayDiscardEmailModalBoxCommand = new RelayCommand(DisplayDiscardEmailModalBox);
        }

        #region Public Commands

        public ICommand DisplayDiscardEmailModalBoxCommand { get; set; }

        public ICommand SendEmailCommand { get; set; }

        #endregion

        #region Command Methods

        private void DisplayDiscardEmailModalBox()
        {
        }

        private void SendEmail()
        {
        }

        #endregion

    }
}

