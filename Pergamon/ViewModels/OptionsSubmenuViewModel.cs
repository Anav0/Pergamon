
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

                RaiseOnLanguageChanged();
            }
        }

        public EventHandler OnLanguageChanged;
        public EventHandler OnPerformSpellCheck;

        public ICommand PerformSpellCheckCommand { get; set; }

        private void RaiseOnLanguageChanged() => OnLanguageChanged?.Invoke(this, new EventArgs());

        private OptionsSubmenuViewModel()
        {
            var langs = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            langs.Sort((x, y) => string.Compare(x.DisplayName, y.DisplayName));
            LanguageList = new ObservableCollection<CultureInfo>(langs);
            SelectedLanguage = CultureInfo.CurrentCulture;

            PerformSpellCheckCommand = new RelayCommandWithParameter((param) => { PerformSpellCheck(param); });
        }

        private void PerformSpellCheck(object actionTarget)
        {
            if (!(actionTarget is Control contr))
                return;

            OnPerformSpellCheck?.Invoke(this, new ControlEventArgs(contr));
        }

    }
}
