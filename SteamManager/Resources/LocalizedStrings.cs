using System.ComponentModel;
using System.Globalization;

namespace SteamManager.Resources
{
    public class LocalizedStrings : INotifyPropertyChanged
    {
        public string Welcome => Strings.Welcome;
        public string Launch => Strings.Launch;
        public string Trade => Strings.Trade;
        public string Refresh => Strings.Refresh;
        public string Drops => Strings.Drops;
        public string Username => Strings.Username;
        public string Language => Strings.Language;
        public string Password => Strings.Password;
        public string SteamId => Strings.SteamId;
        public string ApiKey => Strings.ApiKey;
        public string AddAccount => Strings.AddAccount;
        public string SteamPath => Strings.SteamPath;
        public string Browse => Strings.Browse;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void UpdateCulture(string lang)
        {
            Strings.Culture = new CultureInfo(lang == "ru" ? "ru-RU" : "en-US");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Welcome)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Launch)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trade)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Refresh)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drops)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Language)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SteamId)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ApiKey)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddAccount)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SteamPath)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Browse)));
        }
    }
}
