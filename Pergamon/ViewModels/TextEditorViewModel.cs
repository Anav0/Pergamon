
using System;
using System.Windows.Input;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {
        #region Public properties



        #endregion


        public TextEditorViewModel()
        {
            ShowFontColorPickerCommand = new RelayCommand(ShowFontColorPicker);
            ShowMarkerColorPickerCommand = new RelayCommand(ShowMarkerColorPicker);

        }

        #region Command Methods

        private void ShowMarkerColorPicker()
        {
        }

        private void ShowFontColorPicker()
        {
        }

        #endregion

        #region Public Commands

        public ICommand ShowFontColorPickerCommand { get; set; }

        public ICommand ShowMarkerColorPickerCommand { get; set; }

        #endregion
    }
}
