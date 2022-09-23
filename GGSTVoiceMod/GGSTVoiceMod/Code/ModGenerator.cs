using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace GGSTVoiceMod
{
    public static class ModGenerator
    {
        #region Methods

        public static Task Generate(PatchInfo patch, Action iterationCallback = null)
        {
            if (Settings.BundleMods == true)
                return null;// GenerateBundled(patch);
            else
                return GenerateIndividual(patch, iterationCallback);
        }

        private static async Task GenerateIndividual(PatchInfo patch, Action iterationCallback)
        {
            foreach (var langPatch in patch)
            {
                using Stream stream = await DownloadManager.DownloadAsset(langPatch.Character, langPatch.UseLang);
                using ZipArchive zip = new ZipArchive(stream);

                Paths.CharacterID = langPatch.Character;
                Paths.LanguageID  = langPatch.UseLang;

                Directory.CreateDirectory(Paths.GenUnpack);

                // Unpack the asset bundle into a temporary directory
                await Task.Run(() => zip.ExtractToDirectory(Paths.GenUnpack));

                // Rename the relevant directories to the new language
                await Task.Run(() => RecursiveRename(Paths.GenUnpack, langPatch.UseLang, langPatch.OverLang));

                // Generate the mod with UnrealPak
                await Task.Run(CreatePak);

                // Move the generated mod to the mods folder and copy the signature file over
                string modRoot = $"{Paths.ModInstall}/{langPatch.Character}";

                if (!Directory.Exists(modRoot))
                    Directory.CreateDirectory(modRoot);

                string modFileName = $"{modRoot}/{langPatch.UseLang} over {langPatch.OverLang}";

                File.Move(Paths.GenPakFile, $"{modFileName}.pak");
                File.Copy(Paths.GameSig,    $"{modFileName}.sig");

                // Invoke the callback so we can do updates inbetween iterations
                iterationCallback?.Invoke();
            }

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

        private static void CreatePak()
        {
            string filelist = $"\"{Paths.GenUnpack}/*.*\" \"../../../*.*\""; // I'll be honest I don't really know what this is but it works :>
            File.WriteAllText(Paths.UPFileListPath, filelist);

            string arguments = $"\"{Paths.GenPakFile}\" -create={Paths.UPFileList} -compress";

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

        //private static async Task GenerateBundled(PatchInfo patch)
        //{

        //}

        #endregion
    }
}
