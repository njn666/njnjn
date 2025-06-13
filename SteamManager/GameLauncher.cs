using System.Diagnostics;

namespace SteamManager
{
    public static class GameLauncher
    {
        public static void LaunchCS2(Account account)
        {
            var args = $"-login {account.Username} {account.Password} -applaunch 730";
            Process.Start("steam", args);
        }
    }
}
