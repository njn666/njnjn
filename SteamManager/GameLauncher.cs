using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SteamManager
{
    public static class GameLauncher
    {
        public static string DetectSteamPath()
        {
            var env = Environment.GetEnvironmentVariable("STEAM_EXE");
            if (!string.IsNullOrWhiteSpace(env))
                return env;

            var default1 = @"C:\\Program Files (x86)\\Steam\\steam.exe";
            var default2 = @"C:\\Program Files\\Steam\\steam.exe";
            return File.Exists(default1) ? default1 : default2;
        }

        public static void LaunchCS2(Account account, string? steamPath)
        {
            var args = $"-login {account.Username} {account.Password} -applaunch 730";

            if (string.IsNullOrWhiteSpace(steamPath))
            {
                steamPath = DetectSteamPath();
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
