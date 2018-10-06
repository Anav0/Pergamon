using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {
        #region Public properties

        public FlowDocument Document { get; set; } = new FlowDocument();

        public ObservableCollection<double> FontSizes { get; set; }

        private TextPointer _CaretPosition;
        public TextPointer CaretPosition
        {
            get => _CaretPosition;

            set
            {
                if (_CaretPosition == value)
                    return;

                _CaretPosition = value;

                SelectedFontSize = (double)_CaretPosition.Parent.GetValue(TextElement.FontSizeProperty);
            }
        }

        private string _DestiledSelectedText;
        public string DestiledSelectedText
        {
            get => _DestiledSelectedText;
            set
            {
                if (_DestiledSelectedText == value)
                    return;

                _DestiledSelectedText = value;

                SelectedFontSize = (double)SelectedText.GetPropertyValue(TextElement.FontSizeProperty);

            }
        }


        public TextSelection SelectedText { get; set; }

        private double _SelectedFontSize;
        public double SelectedFontSize
        {
            get => _SelectedFontSize;

            set
            {
                if (_SelectedFontSize == value)
                    return;

                _SelectedFontSize = value;

                ChangeFontSize();

                if(SelectedText != null)
                    SelectedText.Select(SelectedText.Start, SelectedText.End);               }
        }

        #endregion

        public TextEditorViewModel()
        {
            ShowFontColorPickerCommand = new RelayCommand(ShowFontColorPicker);
            ShowMarkerColorPickerCommand = new RelayCommand(ShowMarkerColorPicker);
            BoldSelectedTextCommand = new RelayCommand(BoldSelectedText);
            ItalicSelectedTextCommand = new RelayCommand(ItalicSelectedText);
            UnderlineSelectedTextCommand = new RelayCommand(UnderlineSelectedText);

            AlignLeftCommand = new RelayCommand(AlignLeft);
            AlignRightCommand = new RelayCommand(AlignRight);
            AlignCenterCommand = new RelayCommand(AlignCenter);
            JustifyCommand = new RelayCommand(Justify);

            FontSizes = new ObservableCollection<double>();
            FillFontSizesList();

        }

        #region Public Commands

        public ICommand ShowFontColorPickerCommand { get; set; }
        public ICommand ShowMarkerColorPickerCommand { get; set; }

        public ICommand BoldSelectedTextCommand { get; set; }
        public ICommand ItalicSelectedTextCommand { get; set; }
        public ICommand UnderlineSelectedTextCommand { get; set; }

        public ICommand AlignLeftCommand { get; set; }
        public ICommand AlignRightCommand { get; set; }
        public ICommand AlignCenterCommand { get; set; }
        public ICommand JustifyCommand { get; set; }

        #endregion

        #region Command Methods

        private void BoldSelectedText()
        {
            if (SelectedText == null)
                return;

            var weight = (FontWeight)SelectedText.GetPropertyValue(TextElement.FontWeightProperty);

            if (weight == FontWeights.Bold)
                SelectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            else
                SelectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void ItalicSelectedText()
        {
            if (SelectedText == null)
                return;

            var style = (FontStyle)SelectedText.GetPropertyValue(TextElement.FontStyleProperty);

            if (style == FontStyles.Italic)
                SelectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            else
                SelectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        private void UnderlineSelectedText()
        {
            if (SelectedText == null)
                return;

            var decoration = (TextDecorationCollection)SelectedText.GetPropertyValue(Inline.TextDecorationsProperty);

            if (decoration == TextDecorations.Underline)
                SelectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            else
                SelectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

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

        private void Justify()
        {
            if (SelectedText == null)
                return;

            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Justify);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
        }

        private void AlignCenter()
        {
            if (SelectedText == null)
                return;

            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Center);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
        }

        private void AlignRight()
        {
            if (SelectedText == null)
                return;

            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Right);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
        }

        private void AlignLeft()
        {
            if (SelectedText == null)
                return;

            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Left);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
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

        private void ChangeFontSize()
        {
            if (!(string.IsNullOrWhiteSpace(DestiledSelectedText)))
                SelectedText.ApplyPropertyValue(TextElement.FontSizeProperty, SelectedFontSize);
        }
        #endregion
    }
}

