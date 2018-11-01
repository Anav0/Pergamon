
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Pergamon
{
    public class SearchSectionViewModel : BaseViewModel
    {
        #region Public Properties

        public string Phrase { get; set; }

        #endregion

        public SearchSectionViewModel()
        {
            SearchCommand = new RelayCommandWithParameter((param) => { Search((Control)param); });
        }

        #region Private Command

        public ICommand ShowOptionsCommand { get; private set; }

        public ICommand ReplaceCommand { get; private set; }

        public ICommand ReplaceAllCommand { get; private set; }

        public ICommand GoToPrevCommand { get; private set; }

        public ICommand GoToNextCommand { get; private set; }

        public ICommand SearchCommand { get; private set; }

        #endregion

        #region Command Methods

        private void Search(Control cntrl)
        {

            if (!(cntrl is RichTextBox editor) || string.IsNullOrWhiteSpace(Phrase))
                return;

            TextPointer start = editor.Document.ContentStart;

            while (start != null)
            {
                string textInRun = start.GetTextInRun(LogicalDirection.Forward);

                if (!string.IsNullOrWhiteSpace(textInRun))
                {
                    int index = textInRun.IndexOf(Phrase);

                    if (index != -1)
                    {
                        TextPointer selectionStart = start.GetPositionAtOffset(index, LogicalDirection.Forward);
                        TextPointer selectionEnd = selectionStart.GetPositionAtOffset(Phrase.Length, LogicalDirection.Forward);

                        //TODO: highlight selected
                    }
                }
                start = start.GetNextContextPosition(LogicalDirection.Forward);
            }

        }

        #endregion
    }
}
