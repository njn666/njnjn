using System;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace SteamManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var culture = LoadCulture();
            var accounts = AccountConfig.Load("accounts.json");
            Console.WriteLine(Resources.Strings.Welcome);
            foreach (var account in accounts)
            {
                Console.WriteLine($"{account.Username} - {account.SteamId}");
                SteamApi.FetchLevel(account).Wait();
                GameLauncher.LaunchCS2(account);
            }
        }

        static CultureInfo LoadCulture()
        {
            var lang = Environment.GetEnvironmentVariable("STEAM_MANAGER_LANG");
            if (lang == "ru")
            {
                Resources.Strings.Culture = new CultureInfo("ru-RU");
            }
            else
            {
                Resources.Strings.Culture = new CultureInfo("en-US");
            }
            return Resources.Strings.Culture;
        }
    }
}
