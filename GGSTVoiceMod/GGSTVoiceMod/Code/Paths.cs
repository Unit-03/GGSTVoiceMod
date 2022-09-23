using System.IO;
using System.Reflection;

namespace GGSTVoiceMod
{
    public static class Paths
    {
        // Gotta be honest, not a huge fan of this but it's MY code so I get to do it MY way >:(

        // Just storing the executable directory at launch for use in paths later
        public static readonly string ExecutableRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GameRoot    = null; // Root directory for Guilty Gear -Strive- (set by the user)
        public static string LanguageID  = null;  // The current active language ID (e.g. ENG, JPN, etc.)
        public static string CharacterID = null; // The current active character ID (e.g. RAM, BGT, etc.)

        // The URL that cooked VO assets will be downloaded from (why does VS highlight links, this isn't a web browser...)
        public static string AssetDownloadURL => $"https://github.com/Unit-03/GGSTVoiceMod/raw/main/Assets/{LanguageID}/{CharacterID}.zip";

        public static string UnrealPak  => $"{ExecutableRoot}/UnrealPak/UnrealPak.exe"; // Where the UnrealPak executable is located so we can build the mods at runtime
        public static string AssetCache => $"{ExecutableRoot}/cache"; // The directory that downloaded VO assets will be cached at (if the user has caching enabled)
        public static string Settings   => $"{ExecutableRoot}/settings.ini"; // Settings file to store the user's preference for things like caching, bundling, etc.

        public static string Mods     => $"{GameRoot}/RED/Content/Paks/~mods"; // Mod installation directory
        public static string Install  => $"{Mods}/VoiceMod"; // Sub-directory within the mods folder that GGSTVoiceMod mods will be installed into
        public static string Manifest => $"{Install}/manifest.txt";  // File that stores the current state of the installed mods so the program can launch with the previous settings and avoid re-installing existing mods
    }
}
