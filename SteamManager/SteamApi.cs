using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SteamManager
{
    public static class SteamApi
    {
        static HttpClient client = new HttpClient();

        public static async Task FetchLevel(Account account)
        {
            if (string.IsNullOrEmpty(account.ApiKey) || string.IsNullOrEmpty(account.SteamId))
            {
                Console.WriteLine($"{account.Username}: API key or Steam ID missing");
                return;
            }
            try
            {
                var url = $"https://api.steampowered.com/IPlayerService/GetBadges/v1/?key={account.ApiKey}&steamid={account.SteamId}";
                var json = await client.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("response", out var resp) && resp.TryGetProperty("player_xp", out var xp))
                {
                    Console.WriteLine($"{account.Username}: XP {xp.GetInt32()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API error: {ex.Message}");
            }
        }
    }
}
