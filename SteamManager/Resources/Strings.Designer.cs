namespace SteamManager.Resources {
    using System;
    using System.Reflection;
    using System.Resources;
    using System.Globalization;

    public static class Strings {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        public static ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    var temp = new ResourceManager("SteamManager.Resources.Strings", typeof(Strings).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        public static CultureInfo Culture {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        public static string Welcome {
            get { return ResourceManager.GetString("Welcome", resourceCulture); }
        }

        public static string Launch {
            get { return ResourceManager.GetString("Launch", resourceCulture); }
        }

        public static string Trade {
            get { return ResourceManager.GetString("Trade", resourceCulture); }
        }

        public static string Refresh {
            get { return ResourceManager.GetString("Refresh", resourceCulture); }
        }

        public static string Drops {
            get { return ResourceManager.GetString("Drops", resourceCulture); }
        }

        public static string Username {
            get { return ResourceManager.GetString("Username", resourceCulture); }
        }

        public static string Language {
            get { return ResourceManager.GetString("Language", resourceCulture); }
        }

        public static string Password {
            get { return ResourceManager.GetString("Password", resourceCulture); }
        }

        public static string SteamId {
            get { return ResourceManager.GetString("SteamId", resourceCulture); }
        }

        public static string ApiKey {
            get { return ResourceManager.GetString("ApiKey", resourceCulture); }
        }

        public static string AddAccount {
            get { return ResourceManager.GetString("AddAccount", resourceCulture); }
        }
    }
}
