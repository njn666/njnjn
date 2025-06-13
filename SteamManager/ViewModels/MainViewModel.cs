using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Globalization;

namespace SteamManager.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SteamId { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        private int _xp;
        public int Xp { get => _xp; set { _xp = value; OnPropertyChanged(nameof(Xp)); } }
        private string _drops = string.Empty;
        public string Drops { get => _drops; set { _drops = value; OnPropertyChanged(nameof(Drops)); } }
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AccountViewModel> Accounts { get; } = new();
        public ObservableCollection<string> Languages { get; } = new() { "en", "ru" };

        private string _selectedLanguage = "en";
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    Resources.Strings.Culture = new CultureInfo(value == "ru" ? "ru-RU" : "en-US");
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        public AccountViewModel? SelectedAccount { get; set; }

        public ICommand RefreshCommand { get; }
        public ICommand LaunchCommand { get; }
        public ICommand TradeCommand { get; }

        public MainViewModel()
        {
            RefreshCommand = new RelayCommand(_ => Refresh());
            LaunchCommand = new RelayCommand(_ => Launch());
            TradeCommand = new RelayCommand(_ => Trade());
        }

        public void LoadAccounts(string file)
        {
            foreach (var acc in AccountConfig.Load(file))
            {
                Accounts.Add(new AccountViewModel
                {
                    Username = acc.Username,
                    Password = acc.Password,
                    SteamId = acc.SteamId,
                    ApiKey = acc.ApiKey
                });
            }
        }

        private async void Refresh()
        {
            foreach (var acc in Accounts)
            {
                var a = new Account {
                    Username = acc.Username,
                    Password = acc.Password,
                    SteamId = acc.SteamId,
                    ApiKey = acc.ApiKey
                };
                await SteamApi.FetchLevel(a, xp => acc.Xp = xp);
                await SteamApi.FetchInventory(a, items => acc.Drops = items);
            }
        }

        private void Launch()
        {
            if (SelectedAccount != null)
            {
                var a = new Account { Username = SelectedAccount.Username, Password = SelectedAccount.Password };
                GameLauncher.LaunchCS2(a);
            }
        }

        private void Trade()
        {
            if (SelectedAccount != null)
            {
                var a = new Account { Username = SelectedAccount.Username, Password = SelectedAccount.Password };
                var trade = new TradeManager(a);
                trade.LoginAndSendOffer();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
