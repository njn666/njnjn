namespace SteamManager.Resources {
    using System;
    using System.Reflection;
    using System.Resources;
    using System.Globalization;

    internal static class Strings {
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
            get {
                return ResourceManager.GetString("Welcome", resourceCulture);
            }
        }
    }
}
