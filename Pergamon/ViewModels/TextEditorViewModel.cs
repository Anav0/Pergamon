using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {
        #region Public properties

        public FlowDocument Document { get; set; } = new FlowDocument();

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
            

                if (_CaretPosition.GetTextRunLength(LogicalDirection.Backward) != 0 && !string.IsNullOrWhiteSpace(_CaretPosition.GetTextInRun(LogicalDirection.Backward)))
                {
                    StaticViewModels.FormattingSubmenuVMInstance.SelectedFontSize = (double)_CaretPosition.Parent.GetValue(TextElement.FontSizeProperty);
                    StaticViewModels.FormattingSubmenuVMInstance.SelectedFontFamily = (System.Windows.Media.FontFamily)_CaretPosition.Parent.GetValue(TextElement.FontFamilyProperty);
                }

                StaticViewModels.FormattingSubmenuVMInstance.UpdateButtonsState(CaretPosition, SelectedText);
            }
        }

        public MenuCategories SelectedMenu { get; set; }

        public double LineSpacing { get; set; }

        public FileRepListViewModel AttachedFilesListVM { get; set; } = new FileRepListViewModel();

        #endregion

        #region Public Commands

        public ICommand DisplayDiscardEmailModalBoxCommand { get;  set; }

        public ICommand SendEmailCommand { get; set; }
        #endregion

        public TextEditorViewModel()
        {

            StaticViewModels.FormattingSubmenuVMInstance.OnLineSpacingChanged += OnLineSpacingChanged;
            StaticViewModels.FormattingSubmenuVMInstance.OnApplyMarkerColorActionCalled += OnApplyMarkerAction;
            StaticViewModels.FormattingSubmenuVMInstance.OnApplyFontColorActionCalled += OnApplyFontColorAction;
            StaticViewModels.FormattingSubmenuVMInstance.OnFontFamilyChanged += OnFontFamilyChanged;
            StaticViewModels.FormattingSubmenuVMInstance.OnFontSizeChanged += OnFontSizeChanged;
            StaticViewModels.InsertSubmenuVMInstance.OnAttachFileAction += OnAttachedFilePathsChanged;
            StaticViewModels.InsertSubmenuVMInstance.OnInsertImage += OnInsertImageAction;

            SendEmailCommand = new RelayCommand(SendEmail);
            DisplayDiscardEmailModalBoxCommand = new RelayCommand(DisplayDiscardEmailModalBox);
        }

        private void DisplayDiscardEmailModalBox()
        {
        }

        private void SendEmail()
        {
        }

        private void AdjustTextSelection()
        {
            SelectedText?.Select(SelectedText.Start, SelectedText.End);
        }


        #region Event handlers

        private void OnLineSpacingChanged(object sender, EventArgs e)
        {
            if (!(sender is FormattingSubmenuViewModel formattingVM))
                return;

            this.LineSpacing = formattingVM.LineSpacing;
            AdjustTextSelection();
        }

        private void OnApplyFontColorAction(object sender, EventArgs e)
        {
            if (!(sender is FormattingSubmenuViewModel formattingVM))
                return;

            try
            {
                if (SelectedText.GetPropertyValue(TextElement.ForegroundProperty) == formattingVM.SelectedFontColor)
                    SelectedText.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Transparent));
                else
                    SelectedText.ApplyPropertyValue(TextElement.ForegroundProperty, formattingVM.SelectedFontColor);
            }
            catch (Exception ex)
            {
                //TODO: log this exception
            }
        }

        private void OnApplyMarkerAction(object sender, System.EventArgs e)
        {
            if (!(sender is FormattingSubmenuViewModel formattingVM))
                return;

            try
            {
                if (SelectedText.GetPropertyValue(TextElement.BackgroundProperty) == formattingVM.SelectedMarkerColor)
                    SelectedText.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
                else
                    SelectedText.ApplyPropertyValue(TextElement.BackgroundProperty, formattingVM.SelectedMarkerColor);
            }
            catch(Exception ex)
            {
                //TODO: log this exception
            }

            
        }

        private void OnFontSizeChanged(object sender, EventArgs e)
        {
            if (!(sender is FormattingSubmenuViewModel formattingVM))
                return;

            SelectedText.ApplyPropertyValue(TextElement.FontSizeProperty, formattingVM.SelectedFontSize);
            AdjustTextSelection();
           
        }

        private void OnFontFamilyChanged(object sender, EventArgs e)
        {
            if (!(sender is FormattingSubmenuViewModel formattingVM) || formattingVM.SelectedFontFamily==null)
                return;

            SelectedText.ApplyPropertyValue(TextElement.FontFamilyProperty, formattingVM.SelectedFontFamily);
            AdjustTextSelection();
        }

        private void OnAttachedFilePathsChanged(object sender, EventArgs e)
        {
            var args = (FilePathArgs) e;

            foreach(var file in AttachedFilesListVM.Items)
            {
                if (file.FilePath == args.FilePath)
                {
                    MessageBox.Show("File was already added","Error",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                    return;
                }
            }

            var fileVM = new FileRepViewModel
            {
                FilePath = args.FilePath,
                FileName = Path.GetFileName(args.FilePath),
                FileSize = new FileInfo(args.FilePath).Length,
                FileIcon = Icon.ExtractAssociatedIcon(args.FilePath).ToImageSource(),
            };

            fileVM.OnDeleteAction += ((s, arg) =>
            {
                if (!(s is FileRepViewModel sdr))
                    return;

                AttachedFilesListVM.Items.Remove(sdr);
            });

            fileVM.OnFileClick += ((s, arg) =>
            {
                if (!(s is FileRepViewModel sdr))
                    return;

                System.Diagnostics.Process.Start(sdr.FilePath);
            });

            AttachedFilesListVM.Items.Add(fileVM);
        }


        private void OnInsertImageAction(object sender, EventArgs e)
        {
            var args = (FilePathArgs)e;

            if (args == null || Document == null)
                return;

            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(args.FilePath, UriKind.Absolute);
            bimage.EndInit();

            var image = new System.Windows.Controls.Image { Source = bimage };
            ImageHelpers.InsertImageWithHookedEvents(image, Document);

        }

        #endregion

    }
}

