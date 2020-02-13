
using System.Windows;
using WPFCaching.ViewModels;

namespace WPFCaching
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            CoreNavigation core = new CoreNavigation();

            core.Startup(new AppViewModel(), new MainPageViewModel(), true);
            DataTemplateManager manager = new DataTemplateManager();
            manager.LoadDataTemplateByConvention();
        }
    }
}
