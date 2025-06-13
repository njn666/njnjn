using System;
using System.Windows;

namespace SteamManager
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoadCulture();
            var vm = new ViewModels.MainViewModel();
            vm.LoadAccounts("accounts.json");
            var window = new Views.MainWindow { DataContext = vm };
            MainWindow = window;
            window.Show();
        }

        private static void LoadCulture()
        {
            var lang = Environment.GetEnvironmentVariable("STEAM_MANAGER_LANG");
            SteamManager.Resources.Strings.Culture = lang == "ru" ?
                new System.Globalization.CultureInfo("ru-RU") :
                new System.Globalization.CultureInfo("en-US");
        }
    }
}
