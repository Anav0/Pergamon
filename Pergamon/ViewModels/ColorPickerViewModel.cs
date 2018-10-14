
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Pergamon
{
    public class ColorPickerViewModel : BaseViewModel
    {

        #region Public properties

        public ObservableCollection<SolidColorBrush> StandardColors { get; set; }

        public SolidColorBrush SelectedStandardBrush { get; set; }

        #endregion

        public ColorPickerViewModel()
        {
            StandardColorClickedCommand = new RelayCommandWithParameter((param) => StandardColorClicked(param));

            StandardColors = new ObservableCollection<SolidColorBrush>();

            FillStandardColorsList();
        }

        #region Public Commands

        public ICommand StandardColorClickedCommand { get; set; }

        #endregion

        #region Event handlers

        public event EventHandler OnSelectedStandardColorChanged;

        #endregion

        #region Event raisers

        private void RaiseOnSelectedStandardColorChanged() => OnSelectedStandardColorChanged?.Invoke(this, new EventArgs());

        #endregion

        #region Command methods

        private void StandardColorClicked(object param)
        {

            if (!(param is SolidColorBrush selectedBursh))
                return;

            try
            {
                if (SelectedStandardBrush == selectedBursh)
                    return;

                SelectedStandardBrush = selectedBursh;
                RaiseOnSelectedStandardColorChanged();
            }
            catch(IndexOutOfRangeException exp)
            {
                MessageBox.Show(exp.ToString());
            }


        }

        #endregion

        #region Private methods

        private void FillStandardColorsList()
        {
            StandardColors.Add(new SolidColorBrush(Colors.DarkRed));
            StandardColors.Add(new SolidColorBrush(Colors.Red));
            StandardColors.Add(new SolidColorBrush(Colors.Orange));
            StandardColors.Add(new SolidColorBrush(Colors.Yellow));
            StandardColors.Add(new SolidColorBrush(Colors.LightGreen));
            StandardColors.Add(new SolidColorBrush(Colors.ForestGreen));
            StandardColors.Add(new SolidColorBrush(Colors.LightSkyBlue));
            StandardColors.Add(new SolidColorBrush(Colors.RoyalBlue));
            StandardColors.Add(new SolidColorBrush(Colors.DarkSlateBlue));
            StandardColors.Add(new SolidColorBrush(Colors.BlueViolet));
        }

        #endregion
    }
}
