using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SteamManager
{
    public class Account
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SteamId { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }

    public static class AccountConfig
    {
        public static List<Account> Load(string file)
        {
            if (!File.Exists(file)) return new List<Account>();
            var json = File.ReadAllText(file);
            return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();
        }
    }
}
