using System.IO;
using System.Reflection;

namespace GGSTVoiceMod
{
    public static class Paths
    {
        // Gotta be honest, not a huge fan of this but it's MY code so I get to do it MY way >:(

        // Just storing the executable directory at launch for use in paths later
        public static readonly string ExecutableRoot = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GameRoot = null; // The root directory for Guilty Gear -Strive-
        public static string LanguageID = null; // The current active language ID (e.g. ENG, JPN, etc.)
        public static string CharacterID = null; // The current active character ID (e.g. RAM, BGT, etc.)

        public static string UpdateAgent => $"{ExecutableRoot}/UpdateAgent.exe";

        public static string GitHubURL        => "https://github.com"; // Root URL for GitHub
        public static string GitHubUser       => "Unit-03"; // Name of my GitHub account
        public static string RepoName         => "GGSTVoiceMod"; // Name of the repository for this application
        public static string RepoURL          => $"{GitHubURL}/{GitHubUser}/{RepoName}"; // The full URL for the repository
        public static string LatestReleaseURL => $"{RepoURL}/releases/latest"; // The full URL for retrieving the latest release of this repository
        public static string AssetDownloadURL => $"{RepoURL}/raw/main/Assets/{LanguageID}/{CharacterID}.zip"; // The URL that cooked VO assets will be downloaded from
        public static string AssetCache       => $"{ExecutableRoot}/cache/{LanguageID}/{CharacterID}.zip"; // The directory that downloaded VO assets will be cached at (if the user has caching enabled)

        public static string UPRoot         => $"{ExecutableRoot}/UnrealPak"; // Root directory that UnrealPak.exe and it's libraries are stored in
        public static string UPFileList     => $"filelist.txt"; // Name of the text file that UnrealPak uses to find folder to create paks from
        public static string UPFileListPath => $"{UPRoot}/{UPFileList}"; // Text file that stores the root directory of the files to create a pak from
        public static string UPExecutable   => $"{UPRoot}/UnrealPak.exe"; // Where the UnrealPak executable is located so we can build the mods at runtime

        public static string GenTemp          => $"{ExecutableRoot}/~temp"; // Root directory for storing temporary files during mod generation
        public static string GenUnpack        => $"{GenTemp}/{CharacterID}_{LanguageID}"; // Full temporary directory for unpacking asset archives into
        public static string GenPakFile       => $"{GenUnpack}.pak"; // The output path for a generated pak for a single mod
        public static string GenBundleName    => $"VOBundle"; // The name used for bundled mod generation
        public static string GenBundleUnpack  => $"{GenTemp}/{GenBundleName}"; // Full temporary directory for unpacking
        public static string GenBundlePakFile => $"{GenBundleUnpack}.pak"; // The output path for the pak file for bundled mods

        public static string SettingsFile => $"{ExecutableRoot}/settings.ini"; // Settings file to store the user's preference for things like caching, bundling, etc.

        public static string GamePaks => $"{GameRoot}/RED/Content/Paks"; // Where the game's pak and sig files are located
        public static string GameSig  => $"{GamePaks}/pakchunk0-WindowsNoEditor.sig"; // The signature file for the game's pak, we duplicate this for the generated mods
        
        public static string ModRoot     => $"{GamePaks}/~mods"; // Mod installation directory
        public static string ModInstall  => $"{ModRoot}/IVOMod"; // Sub-directory within the mods folder that GGSTVoiceMod mods will be installed into
        public static string ModManifest => $"{ModInstall}/manifest.txt";  // File that stores the current state of the installed mods so the program can launch with the previous settings and avoid re-installing existing mods
    }
}
