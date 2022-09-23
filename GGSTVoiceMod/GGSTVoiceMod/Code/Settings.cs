using System.IO;

namespace GGSTVoiceMod
{
    public static class Settings
    {
        #region Properties

        public static bool? UseCache   { get; set; }
        public static bool? BundleMods { get; set; }

        #endregion

        #region Constructor

        static Settings()
        {
            Load();
        }

        #endregion

        #region Methods

        public static void Load()
        {
            if (!File.Exists(Paths.Settings))
                return;

            string[] lines = File.ReadAllLines(Paths.Settings);

            // This is a pretty simple and loose "ini" style settings format, nothing fancy just basic variables
            // It will attempt for interpret anything in the format "[name]=[value]", extra '=' are ignored and improperly formatted lines are skipped 
            for (int i = 0; i < lines.Length; ++i)
            {
                string[] parts = lines[i].Split('=');

                if (parts.Length < 2)
                    continue;

                string name  = parts[0].Trim().ToLower();
                string value = parts[1].Trim().ToLower();

                switch (name)
                {
                    case "cache":
                        if (bool.TryParse(value, out bool cache))
                            UseCache = cache;
                        break;
                    case "bundle":
                        if (bool.TryParse(value, out bool bundle))
                            BundleMods = bundle;
                        break;
                }
            }
        }

        public static void Save()
        {
            using StreamWriter writer = File.CreateText(Paths.Settings);

            if (UseCache != null)
                writer.WriteLine($"cache={UseCache}");
            if (BundleMods != null)
                writer.WriteLine($"bundle={BundleMods}");
        }

        #endregion
    }
}
