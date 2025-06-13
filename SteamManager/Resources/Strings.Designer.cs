namespace SteamManager.Resources {
    using System;
    using System.Reflection;
    using System.Resources;
    using System.Globalization;

    public static class Strings {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal static ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    var temp = new ResourceManager("SteamManager.Resources.Strings", typeof(Strings).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        internal static CultureInfo Culture {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        internal static string Welcome {
            get { return ResourceManager.GetString("Welcome", resourceCulture); }
        }

        internal static string Launch {
            get { return ResourceManager.GetString("Launch", resourceCulture); }
        }

        internal static string Trade {
            get { return ResourceManager.GetString("Trade", resourceCulture); }
        }

        internal static string Refresh {
            get { return ResourceManager.GetString("Refresh", resourceCulture); }
        }

        internal static string Drops {
            get { return ResourceManager.GetString("Drops", resourceCulture); }
        }

        internal static string Username {
            get { return ResourceManager.GetString("Username", resourceCulture); }
        }

        internal static string Language {
            get { return ResourceManager.GetString("Language", resourceCulture); }
        }
    }
}
