using Nuntium.Core;
using Prism.Events;
using System.Windows;

namespace Pergamon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetupIoC();

            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }

        private void SetupIoC()
        {
            IoC.Kernel.Bind<IEventAggregator>().ToConstant(new EventAggregator());

            IoC.Kernel.Bind<TextEditorViewModel>().ToConstant(new TextEditorViewModel());
            
            IoC.Kernel.Bind<AddressSectionViewModel>().ToConstant(new AddressSectionViewModel());

            IoC.Kernel.Bind<AttachmentsSectionViewModel>().ToConstant(new AttachmentsSectionViewModel());

            IoC.Kernel.Bind<SearchSectionViewModel>().ToConstant(new SearchSectionViewModel());
        }
    }
}
