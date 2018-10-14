using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Pergamon
{
    public class FormattingSubmenuViewModel : BaseViewModel
    {

        #region Public properties

        #region bools

        public bool IsBoldChecked { get; set; }
        public bool IsItalicChecked { get; set; }
        public bool IsUnderlineChecked { get; set; }
        public bool IsLeftAlignChecked { get; set; }
        public bool IsRightAlignChecked { get; set; }
        public bool IsCenterAlignChecked { get; set; }
        public bool IsJustifyChecked { get; set; }
        public bool IsDottedListChecked { get; set; }
        public bool IsNumericListChecked { get; set; }

        public bool IsAdditionalOptionVisible { get; set; }
        public bool IsAdditionalAlignOptionVisible { get; set; }

        public bool IsSuperscriptChecked { get; set; }
        public bool IsSubscriptChecked { get; set; }

        public bool IsMarkersColorPickerVisible { get; set; }

        public bool IsMarkersColorPickerChecked { get; set; }

        #endregion

        public ObservableCollection<double> FontSizes { get; set; }

        public ObservableCollection<FontFamily> FontFamilies { get; set; }

        public ObservableCollection<double> SpacingOptions { get; set; }

        public double SelectedFontSize { get; set; } = 12;

        public FontFamily SelectedFontFamily { get; set; }

        public double LineSpacing { get; set; } = 0.5d;

        private double _SelectedSpacing;
        public double SelectedSpacing
        {
            get => _SelectedSpacing;
            set
            {
                if (_SelectedSpacing == value)
                    return;

                if (!(SpacingOptions.Contains(value)))
                    SpacingOptions.Add(value);

                _SelectedSpacing = value;

                LineSpacing = _SelectedSpacing * SelectedFontSize;
                RaiseOnLineSpacingChanged();
            }
        }

        public ColorPickerViewModel ColorPickerVM { get; set; }

        public Brush SelectedMarkerColor { get; set; } = new SolidColorBrush(Colors.Yellow);

        public Brush SelectedFontColor { get; set; } = new SolidColorBrush(Colors.Black);

        #endregion

        #region Event handlers

        public event EventHandler OnApplyMarkerColorActionCalled;

        public event EventHandler OnLineSpacingChanged;

        #endregion

        #region Event raisers

        private void RaiseOnLineSpacingChanged() => OnLineSpacingChanged?.Invoke(this, new EventArgs());

        private void RaiseOnApplyMarkerColor()   => OnApplyMarkerColorActionCalled?.Invoke(this, new EventArgs());

        #endregion

        public FormattingSubmenuViewModel()
        {
            ShowFontColorPickerCommand = new RelayCommand(ShowFontColorPicker);
            ShowMarkerColorPickerCommand = new RelayCommand(ShowMarkerColorPicker);
            ShowAdditionalOptionsCommand = new RelayCommand(ShowAdditionalOptions);
            ShowAdditionalAlignOptionsCommand = new RelayCommand(ShowAdditionalAlignOptions);
            ApplyMarkerColorCommand = new RelayCommand(RaiseOnApplyMarkerColor);

            FontSizes = new ObservableCollection<double>();
            FillFontSizesList();

            FontFamilies = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies.OrderBy(x => x.ToString()));
            SelectedFontFamily = FontFamilies.FirstOrDefault(x => x.Source.ToString() == "Segoe UI");

            SpacingOptions = new ObservableCollection<double>();
            FillSpacingOptions();

            ColorPickerVM = new ColorPickerViewModel();

            ColorPickerVM.OnSelectedStandardColorChanged += ((s, e) =>
            {
                SelectedMarkerColor = ColorPickerVM.SelectedStandardBrush;
                RaiseOnApplyMarkerColor();
            });
        }

        #region Public Command

        public ICommand ShowFontColorPickerCommand { get; set; }

        public ICommand ShowMarkerColorPickerCommand { get; set; }

        public ICommand ShowAdditionalOptionsCommand { get; set; }

        public ICommand ShowAdditionalAlignOptionsCommand { get; set; }

        public ICommand HideOptionsCommand { get; set; }

        public ICommand ApplyMarkerColorCommand { get; set; }

        #endregion

        #region Command methods

        private void ShowMarkerColorPicker() => IsMarkersColorPickerVisible ^= true;

        private void ShowFontColorPicker()
        {
        }

        private void ShowAdditionalOptions() => IsAdditionalOptionVisible ^= true;

        private void ShowAdditionalAlignOptions() => IsAdditionalAlignOptionVisible ^= true;

        #endregion

        #region Private methods

        public void UpdateButtonsState(TextPointer currentCaretPositon, TextSelection currentSelectedText)
        {

            IsBoldChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, TextElement.FontWeightProperty, FontWeights.Bold);
            IsItalicChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, TextElement.FontStyleProperty, FontStyles.Italic);
            IsUnderlineChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, TextBlock.TextDecorationsProperty, TextDecorations.Underline);

            IsLeftAlignChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Left);
            IsRightAlignChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Right);
            IsCenterAlignChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Center);
            IsJustifyChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Justify);

            IsSubscriptChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, Typography.VariantsProperty, FontVariants.Subscript);
            IsSuperscriptChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, Typography.VariantsProperty, FontVariants.Superscript);

            IsDottedListChecked = ButtonStateHelper.CheckTextMarkerPropertyState(currentSelectedText, TextMarkerStyle.Disc);
            IsNumericListChecked = ButtonStateHelper.CheckTextMarkerPropertyState(currentSelectedText, TextMarkerStyle.Decimal);

            IsMarkersColorPickerChecked = ButtonStateHelper.CheckDependencyPropertyState(currentCaretPositon, TextElement.BackgroundProperty, SelectedMarkerColor);
        }

        private void FillFontSizesList()
        {
            FontSizes.Add(8);
            FontSizes.Add(9);
            FontSizes.Add(10);
            FontSizes.Add(11);

            for (int i = 12; i < 28; i += 2)
            {
                FontSizes.Add(i);
            }
            FontSizes.Add(36);
            FontSizes.Add(48);
            FontSizes.Add(72);
        }

        private void FillSpacingOptions()
        {
            SpacingOptions.Add(0.5d);
            SpacingOptions.Add(1.0d);
            SpacingOptions.Add(1.15d);
            SpacingOptions.Add(1.50d);
            SpacingOptions.Add(2.0d);
            SpacingOptions.Add(2.5d);
            SpacingOptions.Add(3.0d);
        }

        #endregion
    }
}
