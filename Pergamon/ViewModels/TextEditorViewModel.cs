
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {
        #region Public properties

        public FlowDocument Document { get; set; } = new FlowDocument();

        public TextSelection SelectedText { get; set; }

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
            var weight = (FontWeight)SelectedText.GetPropertyValue(TextElement.FontWeightProperty);

            if (weight == FontWeights.Bold)
                SelectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            else
                SelectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void ItalicSelectedText()
        {
            var style = (FontStyle)SelectedText.GetPropertyValue(TextElement.FontStyleProperty);

            if (style == FontStyles.Italic)
                SelectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            else
                SelectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
        }

        private void UnderlineSelectedText()
        {
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
            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Justify);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
        }

        private void AlignCenter()
        {
            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Center);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
        }

        private void AlignRight()
        {
            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Right);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
        }

        private void AlignLeft()
        {
            SelectedText.ApplyPropertyValue(Block.TextAlignmentProperty, TextAlignment.Left);
            SelectedText.Select(SelectedText.Start, SelectedText.End);
        }

        #endregion
    }
}

