using System.IO;

namespace GGSTVoiceMod
{
    public static class Settings
    {
        #region Constants

        private const string CACHE_ID     = "cache";
        private const string BUNDLE_ID    = "bundle";
        private const string GAME_ROOT_ID = "gameRoot";

        #endregion

        #region Properties

        public static bool? UseCache {
            get => _useCache;
            set => _useCache = value;
        }

        public static bool? BundleMods {
            get => _bundleMods;
            set => _bundleMods = value;
        }

        public static string GamePath {
            get => _gamePath;
            set {
                _gamePath = value;
                Paths.GameRoot = Path.GetDirectoryName(_gamePath);
            }
        }

        #endregion

        #region Fields

        // These could just be auto-properties but for consistency and potential changes to their functionality later I'm keeping them like this
        private static bool? _useCache;
        private static bool? _bundleMods;
        private static string _gamePath;

        #endregion

        #region Methods

        public static void Load()
        {
            if (!File.Exists(Paths.SettingsFile))
                return;

            string[] lines = File.ReadAllLines(Paths.SettingsFile);

            // This is a pretty simple and loose "ini" style settings format, nothing fancy just basic variables
            // It will attempt for interpret anything in the format "[name]=[value]", extra '=' are ignored and improperly formatted lines are skipped 
            for (int i = 0; i < lines.Length; ++i)
            {
                string[] parts = lines[i].Split('=');

                if (parts.Length < 2)
                    continue;

                string name  = parts[0].Trim();
                string value = parts[1].Trim();

                switch (name)
                {
                    case CACHE_ID:
                        if (bool.TryParse(value, out bool cache))
                            UseCache = cache;
                        break;
                    case BUNDLE_ID:
                        if (bool.TryParse(value, out bool bundle))
                            BundleMods = bundle;
                        break;
                    case GAME_ROOT_ID:
                        if (File.Exists(value))
                            GamePath = value;
                        break;
                }
            }
        }

        public static void Save()
        {
            using StreamWriter writer = File.CreateText(Paths.SettingsFile);

            if (UseCache != null)
                writer.WriteLine($"{CACHE_ID}={UseCache}");
            if (BundleMods != null)
                writer.WriteLine($"{BUNDLE_ID}={BundleMods}");
            if (GamePath != null)
                writer.WriteLine($"{GAME_ROOT_ID}={GamePath}");
        }

        #endregion
    }
}
