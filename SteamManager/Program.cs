using System;
using System.Globalization;
using System.Windows;

namespace SteamManager
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            LoadCulture();
            var vm = new ViewModels.MainViewModel();
            vm.LoadAccounts("accounts.json");
            var app = new App();
            var window = new Views.MainWindow { DataContext = vm };
            app.Run(window);
        }

        static void LoadCulture()
        {
            var lang = Environment.GetEnvironmentVariable("STEAM_MANAGER_LANG");
            Resources.Strings.Culture = lang == "ru" ? new CultureInfo("ru-RU") : new CultureInfo("en-US");
        }
    }
}
