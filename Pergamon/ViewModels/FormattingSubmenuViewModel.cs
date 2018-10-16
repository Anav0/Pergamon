using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static Pergamon.ButtonStateHelper;

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

        public bool IsMarkerColorPickerVisible { get; set; }
        public bool IsFontColorPickerVisible { get; set; }
        public bool IsFontColorPickerChecked { get; set; }

        public bool IsMarkersColorPickerChecked { get; set; }

        #endregion

        public ObservableCollection<double> FontSizes { get; set; }

        public ObservableCollection<FontFamily> FontFamilies { get; set; }

        public ObservableCollection<double> SpacingOptions { get; set; }

        private double _SelectedFontSize = 12;
        public double SelectedFontSize { get => _SelectedFontSize;
            set
            {
                if (_SelectedFontSize == value)
                    return;

                _SelectedFontSize = value;

                RaiseOnFontSizeChanged();
            }
        }

        private FontFamily _SelectedFontFamily;
        public FontFamily SelectedFontFamily { get => _SelectedFontFamily;
            set
            {
                if (_SelectedFontFamily == value)
                    return;

                _SelectedFontFamily = value;

                RaiseOnFontFamilyChanged();
            }
        }

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

        public event EventHandler OnApplyFontColorActionCalled;

        public event EventHandler OnLineSpacingChanged;

        public event EventHandler OnFontSizeChanged;

        public event EventHandler OnFontFamilyChanged;

        #endregion

        #region Event raisers

        private void RaiseOnLineSpacingChanged() => OnLineSpacingChanged?.Invoke(this, new EventArgs());

        private void RaiseOnApplyMarkerColor() => OnApplyMarkerColorActionCalled?.Invoke(this, new EventArgs());

        private void RaiseOnApplyFontColor() => OnApplyFontColorActionCalled?.Invoke(this, new EventArgs());

        private void RaiseOnFontSizeChanged() => OnFontSizeChanged?.Invoke(this, new EventArgs());

        private void RaiseOnFontFamilyChanged() => OnFontFamilyChanged?.Invoke(this, new EventArgs());

        #endregion

        public FormattingSubmenuViewModel()
        {
            ShowMarkerColorPickerCommand = new RelayCommand(ShowMarkerColorPicker);
            ShowFontColorPickerCommand = new RelayCommand(ShowFontColorPicker);

            ShowAdditionalOptionsCommand = new RelayCommand(ShowAdditionalOptions);
            ShowAdditionalAlignOptionsCommand = new RelayCommand(ShowAdditionalAlignOptions);
            ApplyMarkerColorCommand = new RelayCommand(RaiseOnApplyMarkerColor);
            ApplyFontColorCommand = new RelayCommand(RaiseOnApplyFontColor);

            FontSizes = new ObservableCollection<double>();
            FillFontSizesList();

            FontFamilies = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies.OrderBy(x => x.ToString()));
            SelectedFontFamily = FontFamilies.FirstOrDefault(x => x.Source.ToString() == "Segoe UI");

            SpacingOptions = new ObservableCollection<double>();
            FillSpacingOptions();

            ColorPickerVM = new ColorPickerViewModel();


            ColorPickerVM.OnColorSelectionChanged += ((s, e) =>
            {
                if(IsFontColorPickerVisible)
                {
                    SelectedFontColor = ColorPickerVM.SelectedStandardBrush;
                    RaiseOnApplyFontColor();
                }
                else
                {
                    SelectedMarkerColor = ColorPickerVM.SelectedStandardBrush;
                    RaiseOnApplyMarkerColor();
                }

            });
        }

        #region Public Command

        public ICommand ShowMarkerColorPickerCommand { get; set; }

        public ICommand ShowFontColorPickerCommand { get; set; }

        public ICommand ShowAdditionalOptionsCommand { get; set; }

        public ICommand ShowAdditionalAlignOptionsCommand { get; set; }

        public ICommand HideOptionsCommand { get; set; }

        public ICommand ApplyMarkerColorCommand { get; set; }

        public ICommand ApplyFontColorCommand { get; set; }

        #endregion

        #region Command methods

        private void ShowMarkerColorPicker() => IsMarkerColorPickerVisible ^= true;

        private void ShowFontColorPicker() => IsFontColorPickerVisible ^= true;

        private void ShowAdditionalOptions() => IsAdditionalOptionVisible ^= true;

        private void ShowAdditionalAlignOptions() => IsAdditionalAlignOptionVisible ^= true;

        #endregion

        #region Private methods

        public void UpdateButtonsState(TextPointer currentCaretPositon, TextSelection currentSelectedText)
        {

            IsBoldChecked = CheckDependencyPropertyState(currentCaretPositon, TextElement.FontWeightProperty, FontWeights.Bold);
            IsItalicChecked = CheckDependencyPropertyState(currentCaretPositon, TextElement.FontStyleProperty, FontStyles.Italic);
            IsUnderlineChecked = CheckDependencyPropertyState(currentCaretPositon, TextBlock.TextDecorationsProperty, TextDecorations.Underline);

            IsLeftAlignChecked = CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Left);
            IsRightAlignChecked = CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Right);
            IsCenterAlignChecked = CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Center);
            IsJustifyChecked = CheckDependencyPropertyState(currentCaretPositon, Paragraph.TextAlignmentProperty, TextAlignment.Justify);

            IsSubscriptChecked = CheckDependencyPropertyState(currentCaretPositon, Typography.VariantsProperty, FontVariants.Subscript);
            IsSuperscriptChecked = CheckDependencyPropertyState(currentCaretPositon, Typography.VariantsProperty, FontVariants.Superscript);

            IsDottedListChecked = CheckTextMarkerPropertyState(currentSelectedText, TextMarkerStyle.Disc);
            IsNumericListChecked = CheckTextMarkerPropertyState(currentSelectedText, TextMarkerStyle.Decimal);

            IsMarkersColorPickerChecked = CheckDependencyPropertyState(currentCaretPositon, TextElement.BackgroundProperty, SelectedMarkerColor);
            IsFontColorPickerChecked = CheckDependencyPropertyState(currentCaretPositon, TextElement.ForegroundProperty, SelectedFontColor);
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
