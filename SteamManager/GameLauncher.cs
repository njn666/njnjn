using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SteamManager
{
    public static class GameLauncher
    {
        public static void LaunchCS2(Account account)
        {
            var args = $"-login {account.Username} {account.Password} -applaunch 730";

            var steamPath = Environment.GetEnvironmentVariable("STEAM_EXE");
            if (string.IsNullOrWhiteSpace(steamPath))
            {
                var default1 = @"C:\\Program Files (x86)\\Steam\\steam.exe";
                var default2 = @"C:\\Program Files\\Steam\\steam.exe";
                steamPath = File.Exists(default1) ? default1 : default2;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = steamPath,
                    Arguments = args,
                    UseShellExecute = false
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to launch Steam: {ex.Message}");
            }
        }
    }
}
