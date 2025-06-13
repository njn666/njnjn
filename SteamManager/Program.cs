using System;
using System.Windows;

namespace SteamManager
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            // App.xaml does not declare any resources, so the generated
            // InitializeComponent method is empty. Calling it causes a
            // build error on systems without the WPF build tasks, so
            // the application can start directly.
            app.Run();
        }
    }
}
