using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GGSTVoiceMod
{
    public static class ModGenerator
    {
        #region Methods

        public static Task GenerateNarrationMod(string langId, string charId, bool silent)
        {
            return GenerateNarration(langId, charId, silent);
        }

        private static async Task GenerateNarration(string langId, string charId, bool silent)
        {
            Paths.NarrLangID = langId;
            Paths.NarrCharID = charId;

            // Make sure the temp folder is clear before generating or it could break things
            if (Directory.Exists(Paths.GenTemp))
                Directory.Delete(Paths.GenTemp, true);

            using Stream stream = await DownloadManager.DownloadAsset(Paths.NarrAssetDownloadURL, Paths.NarrAssetCache);
            using ZipArchive zip = new ZipArchive(stream);

            Directory.CreateDirectory(Paths.NarrGenUnpack);

            // Unpack the asset bundle into a temporary directory
            await Task.Run(() => zip.ExtractToDirectory(Paths.NarrGenUnpack));

            // Either rename or delete the silent audio files depending on the user's choice
            await Task.Run(() => ProcessEmptyAudio(Paths.NarrGenUnpack, silent));

            // Generate the mod with UnrealPak
            await Task.Run(() => CreatePak(Paths.NarrGenUnpack, Paths.NarrGenPakFile));

            // Move the generated mod to the mods folder and copy the signature file over
            string modRoot = $"{Paths.ModInstall}/Narration";

            if (!Directory.Exists(modRoot))
                Directory.CreateDirectory(modRoot);

            string modFileName = Path.Combine(modRoot, $"{langId}_{charId}");

            File.Move(Paths.NarrGenPakFile, $"{modFileName}.pak", true);
            File.Copy(Paths.GameSig,        $"{modFileName}.sig", true);

            // Clear out the temp files
            Directory.Delete(Paths.GenTemp, true);
        }

        private static void ProcessEmptyAudio(string root, bool silence)
        {
            string[] files = Directory.GetFiles(root, "*_empty.*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; ++i)
            {
                if (silence)
                    File.Move(files[i], files[i].Replace("_empty", ""), true);
                else
                    File.Delete(files[i]);
            }
        }

        public static Task GenerateVoiceMod(PatchInfo patch, Action iterationCallback = null)
        {
            if (Settings.BundleMods == true)
                return GenerateBundled(patch, iterationCallback);
            else
                return GenerateIndividual(patch, iterationCallback);
        }

        private static async Task GenerateIndividual(PatchInfo patch, Action iterationCallback)
        {
            // Make sure the temp folder is clear before generating or it could break things
            if (Directory.Exists(Paths.GenTemp))
                Directory.Delete(Paths.GenTemp, true);

            foreach (var langPatch in patch)
            {
                Paths.VoiceCharID = langPatch.Character;
                Paths.VoiceLangID = langPatch.UseLang;

                using Stream stream = await DownloadManager.DownloadAsset(Paths.VoiceAssetDownloadURL, Paths.VoiceAssetCache);
                using ZipArchive zip = new ZipArchive(stream);

                Directory.CreateDirectory(Paths.VoiceGenUnpack);

                // Unpack the asset bundle into a temporary directory
                await Task.Run(() => zip.ExtractToDirectory(Paths.VoiceGenUnpack));

                // Rename the relevant directories to the new language
                await Task.Run(() => RecursiveRename(Paths.VoiceGenUnpack, langPatch.UseLang, langPatch.OverLang));

                // Generate the mod with UnrealPak
                await Task.Run(() => CreatePak(Paths.VoiceGenUnpack, Paths.VoiceGenPakFile));

                // Move the generated mod to the mods folder and copy the signature file over
                string modRoot = $"{Paths.ModInstall}/{langPatch.Character}";

                if (!Directory.Exists(modRoot))
                    Directory.CreateDirectory(modRoot);

                string modFileName = Path.Combine(modRoot, $"{langPatch.UseLang} over {langPatch.OverLang}");

                File.Move(Paths.VoiceGenPakFile, $"{modFileName}.pak", true);
                File.Copy(Paths.GameSig,         $"{modFileName}.sig", true);

                // Invoke the callback so we can do updates inbetween iterations
                iterationCallback?.Invoke();
            }

            // Clear out the temp files
            Directory.Delete(Paths.GenTemp, true);
        }

        private static async Task GenerateBundled(PatchInfo patch, Action iterationCallback)
        {
            // Make sure the temp folder is clear before generating or it could break things
            if (Directory.Exists(Paths.GenTemp))
                Directory.Delete(Paths.GenTemp, true);

            // If we're doing a bundled mod then any already installed mods should be deleted first
            if (Directory.Exists(Paths.ModInstall))
                Directory.Delete(Paths.ModInstall, true);

            foreach (var langPatch in patch)
            {
                Paths.VoiceCharID = langPatch.Character;
                Paths.VoiceLangID = langPatch.UseLang;

                using Stream stream = await DownloadManager.DownloadAsset(Paths.VoiceAssetDownloadURL, Paths.VoiceAssetCache);
                using ZipArchive zip = new ZipArchive(stream);

                Directory.CreateDirectory(Paths.VoiceGenUnpack);

                // Unpack the asset bundle into a temporary directory
                await Task.Run(() => zip.ExtractToDirectory(Paths.VoiceGenUnpack));

                // Rename the relevant directories to the new language
                await Task.Run(() => RecursiveRename(Paths.VoiceGenUnpack, langPatch.UseLang, langPatch.OverLang));

                // Move the renamed assets into the bundle directory
                await Task.Run(() => MoveSubDirectories(Paths.VoiceGenUnpack, Paths.GenBundleUnpack));

                // Invoke the callback so we can do updates inbetween iterations
                iterationCallback?.Invoke();
            }

            // Generate the mod with UnrealPak
            await Task.Run(() => CreatePak(Paths.GenBundleUnpack, Paths.GenBundlePakFile));

            // Move the generated mod to the mods folder and copy the signature file over
            string modRoot = $"{Paths.ModInstall}";

            if (!Directory.Exists(modRoot))
                Directory.CreateDirectory(modRoot);

            string modFileName = Path.Combine(modRoot, Paths.GenBundleName);

            File.Move(Paths.GenBundlePakFile, $"{modFileName}.pak", true);
            File.Copy(Paths.GameSig,          $"{modFileName}.sig", true);

            // Clear out the temp files
            Directory.Delete(Paths.GenTemp, true);
        }

        private static void RecursiveRename(string root, string oldName, string newName)
        {
            string[] subDirs = Directory.GetDirectories(root);

            for (int i = 0; i < subDirs.Length; ++i)
            {
                string subDirName = Path.GetRelativePath(root, subDirs[i]);

                if (subDirName == oldName)
                {
                    string newDir = Path.Combine(root, newName);
                    Directory.Move(subDirs[i], newDir);

                    subDirs[i] = newDir;
                }
            }

            for (int i = 0; i < subDirs.Length; ++i)
                RecursiveRename(subDirs[i], oldName, newName);
        }

        private static void MoveSubDirectories(string root, string destination)
        {
            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            string[] files = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; ++i)
            {
                string relativePath = Path.GetRelativePath(root, files[i]);
                string newPath      = Path.Combine(destination, relativePath);
                string fileDir      = Path.GetDirectoryName(newPath);

                if (!Directory.Exists(fileDir))
                    Directory.CreateDirectory(fileDir);

                File.Move(files[i], newPath);
            }
        }

        private static void CreatePak(string root, string destination)
        {
            string filelist = $"\"{root}/*.*\" \"../../../*.*\""; // I'll be honest I don't really know what this is but it works :>
            File.WriteAllText(Paths.UPFileListPath, filelist);

            string arguments = $"\"{destination}\" -create={Paths.UPFileList} -compress";

            ProcessStartInfo startInfo = new ProcessStartInfo() {
                Arguments = arguments,
                CreateNoWindow = true,
                FileName = Paths.UPExecutable,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            Process unrealPak = Process.Start(startInfo);
            unrealPak.WaitForExit();
        }

        #endregion
    }
}
