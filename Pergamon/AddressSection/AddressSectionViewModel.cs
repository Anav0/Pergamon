
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Pergamon
{
    public class AddressSectionViewModel : BaseViewModel
    {
        #region Singleton

        public static AddressSectionViewModel Instance { get; set; } = new AddressSectionViewModel();

        #endregion

        public ObservableCollection<Address> Addresses { get; set; } = new ObservableCollection<Address>();

        public bool IsCCandBCCVisible { get; set; }

        public string Topic { get; set; }

        public ICommand ShowCCandBCCFieldsCommand { get; set; }

        private AddressSectionViewModel()
        {
            ShowCCandBCCFieldsCommand = new RelayCommand(ShowCCandBCCFields);
        }

        private void ShowCCandBCCFields() => IsCCandBCCVisible ^= true;
    }
}
