﻿using Ninject;
using Nuntium.Core;
using Prism.Events;
using System.Windows.Documents;
using System.Windows.Input;

namespace Pergamon
{
    public class TextEditorViewModel : BaseViewModel
    {
        #region Public properties

        public MenuCategories SelectedMenu { get; set; }

        public bool IsSearchSectionVisible { get; set; }

        public double LineSpacing { get; set; }

        #endregion

        public TextEditorViewModel()
        {
            SendEmailCommand = new RelayCommand(SendEmail);
            DisplayDiscardEmailModalBoxCommand = new RelayCommand(DisplayDiscardEmailModalBox);

            IoC.Kernel.Get<IEventAggregator>().GetEvent<LineSpacingChangedEvent>().Subscribe((value) =>
            {
                LineSpacing = value;
            });
        }

        #region Public Commands

        public ICommand DisplayDiscardEmailModalBoxCommand { get; set; }

        public ICommand SendEmailCommand { get; set; }

        #endregion

        #region Command Methods

        private void DisplayDiscardEmailModalBox()
        {
        }

        private void SendEmail()
        {
            IMarkupConverter markupConverter = new MarkupConverterImpl();
            var editor = IoC.Kernel.Get<CustomRichTextBox>();

            var html = markupConverter.ConvertRtfToHtml(editor.Document);
        }

        #endregion

    }
}

