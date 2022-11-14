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

        public static string VoiceLangID = null; // The current active voice language ID (e.g. ENG, JPN, etc.)
        public static string VoiceCharID = null; // The current active voice character ID (e.g. RAM, BGT, etc.)

        public static string NarrLangID  = null; // The current active narration language ID (e.g. ENG, JPN)
        public static string NarrCharID  = null; // The current active narration character ID (e.g. RAM, SIN, etc.)

        public static string UpdateAgent => $"{ExecutableRoot}/UpdateAgent.exe";

        // <|| GitHub ||>
        public static string GitHubURL        => "https://github.com"; // Root URL for GitHub
        public static string GitHubUser       => "Unit-03"; // Name of my GitHub account
        public static string RepoName         => "GGSTVoiceMod"; // Name of the repository for this application
        public static string RepoURL          => $"{GitHubURL}/{GitHubUser}/{RepoName}"; // The full URL for the repository
        public static string LatestReleaseURL => $"{RepoURL}/releases/latest"; // The full URL for retrieving the latest release of this repository

        // <|| Assets ||>
        public static string AssetDownloadURL => $"{RepoURL}/raw/main/Assets";
        public static string AssetCacheRoot   => $"{ExecutableRoot}/cache";

        public static string VoiceAssetDownloadURL => $"{AssetDownloadURL}/{VoiceLangID}/{VoiceCharID}.zip"; // The URL that cooked VO assets will be downloaded from
        public static string VoiceAssetCache       => $"{AssetCacheRoot}/{VoiceLangID}/{VoiceCharID}.zip"; // The directory that downloaded VO assets will be cached at (if the user has caching enabled)

        public static string NarrAssetDownloadURL => $"{AssetDownloadURL}/Narration/{NarrLangID}/{NarrCharID}.zip"; // The URL that cooked narration assets will be downloaded from
        public static string NarrAssetCache       => $"{AssetCacheRoot}/Narration/{NarrLangID}/{NarrCharID}.zip"; // The directory that downloaded narration assets will be cached at (if the user has caching enabled)

        // <|| UnrealPak ||>
        public static string UPRoot         => $"{ExecutableRoot}/UnrealPak"; // Root directory that UnrealPak.exe and it's libraries are stored in
        public static string UPFileList     => $"filelist.txt"; // Name of the text file that UnrealPak uses to find folder to create paks from
        public static string UPFileListPath => $"{UPRoot}/{UPFileList}"; // Text file that stores the root directory of the files to create a pak from
        public static string UPExecutable   => $"{UPRoot}/UnrealPak.exe"; // Where the UnrealPak executable is located so we can build the mods at runtime

        // <|| Generator ||>
        public static string GenTemp => $"{ExecutableRoot}/~temp"; // Root directory for storing temporary files during mod generation

        public static string VoiceGenUnpack  => $"{GenTemp}/{VoiceCharID}_{VoiceLangID}"; // Full temporary directory for unpacking voice asset archives into
        public static string VoiceGenPakFile => $"{VoiceGenUnpack}.pak"; // The output path for a generated pak for a single voice mod

        public static string NarrGenUnpack  => $"{GenTemp}/NARR_{NarrLangID}_{NarrCharID}"; // Full temporary directory for unpacking narration asset archives into
        public static string NarrGenPakFile => $"{NarrGenUnpack}.pak"; // The output path for a generated pak for a narration mod

        public static string GenBundleName    => $"VOBundle"; // The name used for bundled mod generation
        public static string GenBundleUnpack  => $"{GenTemp}/{GenBundleName}"; // Full temporary directory for unpacking
        public static string GenBundlePakFile => $"{GenBundleUnpack}.pak"; // The output path for the pak file for bundled mods

        // <|| Config ||>
        public static string SettingsFile => $"{ExecutableRoot}/settings.ini"; // Settings file to store the user's preference for things like caching, bundling, etc.

        public static string GamePaks => $"{GameRoot}/RED/Content/Paks"; // Where the game's pak and sig files are located
        public static string GameSig  => $"{GamePaks}/pakchunk0-WindowsNoEditor.sig"; // The signature file for the game's pak, we duplicate this for the generated mods
        
        public static string ModRoot     => $"{GamePaks}/~mods"; // Mod installation directory
        public static string ModInstall  => $"{ModRoot}/IVOMod"; // Sub-directory within the mods folder that GGSTVoiceMod mods will be installed into
        public static string ModManifest => $"{ModInstall}/manifest.txt";  // File that stores the current state of the installed mods so the program can launch with the previous settings and avoid re-installing existing mods
    }
}
