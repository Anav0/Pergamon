using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {

        #region Public properties
        
        public TextSelection SelectedText { get; set; }

        private TextPointer _CaretPosition;
        public TextPointer CaretPosition
        {
            get => _CaretPosition;

            set
            {
                //TODO: fix line spacing
                if (_CaretPosition == value)
                    return;

                _CaretPosition = value;


                StaticViewModels.FormattingSubMenuVMInstance.UpdateButtonsState(CaretPosition, SelectedText);

                if (_CaretPosition.GetTextRunLength(LogicalDirection.Backward) != 0 && !string.IsNullOrWhiteSpace(_CaretPosition.GetTextInRun(LogicalDirection.Backward)))
                {
                    StaticViewModels.FormattingSubMenuVMInstance.SelectedFontSize = (double)_CaretPosition.Parent.GetValue(TextElement.FontSizeProperty);
                    StaticViewModels.FormattingSubMenuVMInstance.SelectedFontFamily = (FontFamily)_CaretPosition.Parent.GetValue(TextElement.FontFamilyProperty);
                }
            }
        }

        public MenuCategories SelectedMenu { get; set; }

        public double LineSpacing { get; set; }

        #endregion

        public TextEditorViewModel()
        {
            StaticViewModels.FormattingSubMenuVMInstance.OnApplyMarkerColorActionCalled += OnApplyMarkerAction;

            StaticViewModels.FormattingSubMenuVMInstance.OnLineSpacingChanged += OnLineSpacingChanged;
        }

        #region Event handlers

        private void OnLineSpacingChanged(object sender, EventArgs e)
        {
            if (!(sender is FormattingSubmenuViewModel formattingVM))
                return;

            this.LineSpacing = formattingVM.LineSpacing;
        }

        private void OnApplyMarkerAction(object sender, System.EventArgs e)
        {
            if (!(sender is FormattingSubmenuViewModel formattingVM))
                return;

            try
            {
                var element = (TextElement)CaretPosition.Parent;

                if (element.Background == formattingVM.SelectedMarkerColor)
                    element.Background = new SolidColorBrush(Colors.Transparent);
                else
                    element.Background = formattingVM.SelectedMarkerColor;
            }
            catch(Exception ex)
            {
                //TODO: log this exception
            }

            
        }

        #endregion

    }
}

