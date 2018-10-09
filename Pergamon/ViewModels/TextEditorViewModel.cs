using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {

        #region Public properties

        public FlowDocument Document { get; set; } = new FlowDocument();

        public ObservableCollection<double> FontSizes { get; set; }

        public ObservableCollection<FontFamily> FontFamilies { get; set; }

        public bool IsBoldChecked { get; set; }
        public bool IsItalicChecked { get; set; }
        public bool IsUnderlineChecked { get; set; }
        public bool IsLeftAlignChecked { get; set; }
        public bool IsRightAlignChecked { get; set; }
        public bool IsCenterAlignChecked { get; set; }
        public bool IsJustifyChecked { get; set; }
        public bool IsDottedListChecked { get; set; }
        public bool IsNumericListChecked { get; set; }

        public TextSelection SelectedText { get; set; }

        private TextPointer _CaretPosition;
        public TextPointer CaretPosition
        {
            get => _CaretPosition;

            set
            {
                if (_CaretPosition == value)
                    return;

                _CaretPosition = value;

                UpdateButtonsState();

                if (_CaretPosition.GetTextRunLength(LogicalDirection.Backward) != 0 && !string.IsNullOrWhiteSpace(_CaretPosition.GetTextInRun(LogicalDirection.Backward)))
                {
                    SelectedFontSize = (double)_CaretPosition.Parent.GetValue(TextElement.FontSizeProperty);
                    SelectedFontFamily = (FontFamily)_CaretPosition.Parent.GetValue(TextElement.FontFamilyProperty);
                }
            }
        }

        private double _SelectedFontSize = 12;
        public double SelectedFontSize
        {
            get => _SelectedFontSize;

            set
            {
                if (_SelectedFontSize == value)
                    return;

                _SelectedFontSize = value;

                if (SelectedText == null)
                    return;

                SelectedText.ApplyPropertyValue(TextElement.FontSizeProperty, _SelectedFontSize);
                SelectedText.Select(SelectedText.Start, SelectedText.End);
            }
        }

        private FontFamily _SelectedFontFamily;
        public FontFamily SelectedFontFamily
        {
            get => _SelectedFontFamily;

            set
            {
                if (_SelectedFontFamily == value)
                    return;

                _SelectedFontFamily = value;

                if (SelectedText == null)
                    return;

                SelectedText.ApplyPropertyValue(TextElement.FontFamilyProperty, _SelectedFontFamily);
                SelectedText.Select(SelectedText.Start, SelectedText.End);
            }
        }
        #endregion

        public TextEditorViewModel()
        {
           ShowFontColorPickerCommand = new RelayCommand(ShowFontColorPicker);
           ShowMarkerColorPickerCommand = new RelayCommand(ShowMarkerColorPicker);

           FontSizes = new ObservableCollection<double>();
           FillFontSizesList();

           FontFamilies = new ObservableCollection<FontFamily>(Fonts.SystemFontFamilies.OrderBy(x=>x.ToString()));
           SelectedFontFamily = FontFamilies.FirstOrDefault(x => x.Source.ToString() == "Noto Sans");
        }

        #region Public Commands

        public ICommand ShowFontColorPickerCommand { get; set; }

        public ICommand ShowMarkerColorPickerCommand { get; set; }

        #endregion

        #region Command Methods

        private void ShowMarkerColorPicker()
        {
        }

        private void ShowFontColorPicker()
        {
            if (SelectedText == null)
                return;


        }

        private void SaveToXmlFile()
        {
            TextRange range;
            FileStream fStream;
            range = new TextRange(Document.ContentStart, Document.ContentEnd);
            fStream = new FileStream("test.xml", FileMode.Create);
            range.Save(fStream, DataFormats.Xaml);
            fStream.Close();
        }

        #endregion

        #region Private methods

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

        private void UpdateButtonsState()
        {
            IsBoldChecked = UpdateState(TextElement.FontWeightProperty, FontWeights.Bold);
            IsItalicChecked = UpdateState(TextElement.FontStyleProperty, FontStyles.Italic);
            IsUnderlineChecked = UpdateState(TextBlock.TextDecorationsProperty, TextDecorations.Underline);

            IsLeftAlignChecked = UpdateState(Paragraph.TextAlignmentProperty, TextAlignment.Left);
            IsRightAlignChecked = UpdateState(Paragraph.TextAlignmentProperty, TextAlignment.Right);
            IsCenterAlignChecked = UpdateState(Paragraph.TextAlignmentProperty, TextAlignment.Center);
            IsJustifyChecked = UpdateState(Paragraph.TextAlignmentProperty, TextAlignment.Justify);

            UpdateSelectionListType();
        }

        private bool UpdateState(DependencyProperty formattingProperty, object expectedValue)
        {
            object currentValue = CaretPosition.Parent.GetValue(formattingProperty);

            if (currentValue == null || currentValue == DependencyProperty.UnsetValue)
                return false;

            return currentValue.Equals(expectedValue);
        }

        private void UpdateSelectionListType()
        {

            if (SelectedText == null)
                return;

            var startParagraph = SelectedText.Start.Paragraph;
            var endParagraph = SelectedText.End.Paragraph;

            if (startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
            {
                TextMarkerStyle markerStyle = ((ListItem)startParagraph.Parent).List.MarkerStyle;
                if (markerStyle == TextMarkerStyle.Disc) //bullets
                {
                    IsDottedListChecked = true;
                }
                else if (markerStyle == TextMarkerStyle.Decimal) //numbers
                {
                    IsNumericListChecked = true;
                }
            }
            else
            {
                IsDottedListChecked = false;
                IsNumericListChecked = false;
            }
        }

        #endregion
    }
}

