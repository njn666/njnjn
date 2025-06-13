using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SteamManager
{
    public static class SteamApi
    {
        static HttpClient client = new HttpClient();

        public static async Task FetchLevel(Account account, Action<int> onXp)
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
                    onXp(xp.GetInt32());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API error: {ex.Message}");
            }
        }

        public static async Task FetchInventory(Account account, Action<string> onItems)
        {
            if (string.IsNullOrEmpty(account.SteamId))
                return;

            try
            {
                var url = $"https://steamcommunity.com/inventory/{account.SteamId}/730/2?l=en&count=5000";
                var json = await client.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("descriptions", out var descs))
                {
                    var items = new System.Text.StringBuilder();
                    foreach (var item in descs.EnumerateArray())
                    {
                        if (item.TryGetProperty("market_hash_name", out var name))
                            items.Append(name.GetString()).Append(", ");
                    }
                    onItems(items.ToString().TrimEnd(',', ' '));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inventory error: {ex.Message}");
            }
        }
    }
}
