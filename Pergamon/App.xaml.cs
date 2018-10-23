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
            SetupStaticVM();

            var window = new MainWindow();
            Current.MainWindow = window;
            Current.MainWindow.Show();

        }

        private void SetupStaticVM()
        {
            StaticViewModels.FormattingSubmenuVMInstance = new FormattingSubmenuViewModel();

            StaticViewModels.InsertSubmenuVMInstance = new InsertSubmenuViewModel();

            StaticViewModels.AddressSectionVMInstance = new AddressSectionViewModel();
        }
    }
}
