
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pergamon
{
    public class OptionsSubmenuViewModel : BaseViewModel
    {
        #region Singleton

        public static OptionsSubmenuViewModel Instance { get; private set; } = new OptionsSubmenuViewModel();

        #endregion

        #region Public properties

        public ObservableCollection<CultureInfo> LanguageList { get; set; }

        private CultureInfo _SelectedLanguage;
        public CultureInfo SelectedLanguage
        {
            get => _SelectedLanguage;

            set
            {
                if (_SelectedLanguage == value)
                    return;


                _SelectedLanguage = value;

                OnLanguageChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion

        private OptionsSubmenuViewModel()
        {
            var langs = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            langs.Sort((x, y) => string.Compare(x.DisplayName, y.DisplayName));
            LanguageList = new ObservableCollection<CultureInfo>(langs);
            SelectedLanguage = CultureInfo.CurrentCulture;
        }

        #region Event Handlers

        public EventHandler OnLanguageChanged;

        #endregion

        #region Public Commands

        public ICommand PerformSpellCheckCommand { get; set; }

        public ICommand ShowSearchSectionCommand { get; set; }

        #endregion

    }
}
