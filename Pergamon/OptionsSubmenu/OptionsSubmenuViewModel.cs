
using Ninject;
using Nuntium.Core;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Pergamon
{
    public class OptionsSubmenuViewModel : BaseViewModel
    {

        #region Public properties

        public ObservableCollection<CultureInfo> LanguageList { get; set; }

        private CultureInfo _SelectedCultureGroup;
        public CultureInfo SelectedCultureGroup
        {
            get => _SelectedCultureGroup;

            set
            {
                if (_SelectedCultureGroup == value)
                    return;

                _SelectedCultureGroup = value;

                IoC.Kernel.Get<CustomRichTextBox>().Language = XmlLanguage.GetLanguage(_SelectedCultureGroup.IetfLanguageTag);
            }
        }

        #endregion

        public OptionsSubmenuViewModel()
        {
            var langs = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            langs.Sort((x, y) => string.Compare(x.DisplayName, y.DisplayName));
            LanguageList = new ObservableCollection<CultureInfo>(langs);

            SelectedCultureGroup = CultureInfo.CurrentCulture;

            PerformSpellCheckCommand = new RelayCommandWithParameter((param) =>
            {
                PerformEditorSpellCheck(IoC.Kernel.Get<CustomRichTextBox>());
            });
            ShowSearchSectionCommand = new RelayCommand(() => { IoC.Kernel.Get<IEventAggregator>().GetEvent<ToggleSearchSectionVisibilityEvent>().Publish(); });
        }

        #region Public Commands

        public ICommand ShowSearchSectionCommand { get; set; }

        #endregion

        public ICommand PerformSpellCheckCommand { get; private set; }

        private void PerformEditorSpellCheck(RichTextBox editor)
        {
            var popup = new OffsetPopupFactory().CreatePopupOnPoint(editor.GetEditorPointToScreen());
            var spellCheckOptions = new SpellCheckOptions();

            popup.Child = spellCheckOptions;
            editor.SelectAll();
            for (int i = 0; i < editor.Selection.Text.Length; i++)
            {
                //Get starting insertion point
                TextPointer start = editor.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward).GetPositionAtOffset(i, LogicalDirection.Forward);

                //Check for errars at start
                SpellingError spellingError = editor.GetSpellingError(start);

                //if there is misspelling
                if (spellingError != null)
                {
                    //get range of error
                    int errRange = editor.GetSpellingErrorRange(start).Text.Length;
                    TextPointer end = editor.Document.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward).GetPositionAtOffset(i + errRange, LogicalDirection.Forward);

                    //focus editor
                    editor.Focus();

                    //select text containing error
                    editor.Selection.Select(start, end);

                    spellCheckOptions.Items.Clear();
                    foreach (string str in spellingError.Suggestions)
                    {
                        spellCheckOptions.Items.Add(str);

                        spellCheckOptions.IgnoreCommand = new RelayCommand(() =>
                        {
                            EditingCommands.IgnoreSpellingError.Execute(null, editor);
                            popup.IsOpen = false;
                            PerformEditorSpellCheck(editor);
                        });


                        spellCheckOptions.CorrectionClicked = new RelayCommandWithParameter((correction) =>
                        {
                            EditingCommands.CorrectSpellingError.Execute(correction, editor);
                            popup.IsOpen = false;
                            PerformEditorSpellCheck(editor);
                        });

                    }
                    popup.IsOpen = true;
                    return;
                }
            }

        }
    }
}
