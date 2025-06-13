using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Win32;

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

        public Resources.LocalizedStrings Loc { get; } = new Resources.LocalizedStrings();

        private string _selectedLanguage = "en";
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    Loc.UpdateCulture(value);
                    OnPropertyChanged(nameof(SelectedLanguage));
                }
            }
        }

        public AccountViewModel? SelectedAccount { get; set; }

        public ICommand RefreshCommand { get; }
        public ICommand LaunchCommand { get; }
        public ICommand TradeCommand { get; }
        public ICommand AddAccountCommand { get; }
        public ICommand BrowseSteamCommand { get; }

        private string _steamExePath = string.Empty;
        public string SteamExePath
        {
            get => _steamExePath;
            set { _steamExePath = value; OnPropertyChanged(nameof(SteamExePath)); }
        }

        public string NewUsername { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string NewSteamId { get; set; } = string.Empty;
        public string NewApiKey { get; set; } = string.Empty;

        public MainViewModel()
        {
            RefreshCommand = new RelayCommand(_ => Refresh());
            LaunchCommand = new RelayCommand(_ => Launch());
            TradeCommand = new RelayCommand(_ => Trade());
            AddAccountCommand = new RelayCommand(_ => AddAccount());
            BrowseSteamCommand = new RelayCommand(_ => BrowseSteam());
            SteamExePath = GameLauncher.DetectSteamPath();
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
                GameLauncher.LaunchCS2(a, SteamExePath);
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

        private void AddAccount()
        {
            var vm = new AccountViewModel
            {
                Username = NewUsername,
                Password = NewPassword,
                SteamId = NewSteamId,
                ApiKey = NewApiKey
            };
            Accounts.Add(vm);

            var list = new List<Account>();
            foreach (var a in Accounts)
            {
                list.Add(new Account
                {
                    Username = a.Username,
                    Password = a.Password,
                    SteamId = a.SteamId,
                    ApiKey = a.ApiKey
                });
            }
            AccountConfig.Save("accounts.json", list);

            NewUsername = NewPassword = NewSteamId = NewApiKey = string.Empty;
            OnPropertyChanged(nameof(NewUsername));
            OnPropertyChanged(nameof(NewPassword));
            OnPropertyChanged(nameof(NewSteamId));
            OnPropertyChanged(nameof(NewApiKey));
        }

        private void BrowseSteam()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Steam (steam.exe)|steam.exe|Executable (*.exe)|*.exe",
                FileName = "steam.exe"
            };
            if (dialog.ShowDialog() == true)
            {
                SteamExePath = dialog.FileName;
                Environment.SetEnvironmentVariable("STEAM_EXE", SteamExePath);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
