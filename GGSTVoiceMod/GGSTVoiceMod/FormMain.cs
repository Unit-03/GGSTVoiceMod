using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace GGSTVoiceMod
{
    // This file is a little messy and maybe one day I'll refactor it into something better but it works for now!!
    public partial class FormMain : Form
    {
        #region Fields

        // should these be in Constants.cs? yes. why aren't they? idk get off my back debra
        private const int ENTRY_WIDTH  = 100;
        private const int ENTRY_HEIGHT = 25;

        private Size entrySize = new Size(ENTRY_WIDTH, ENTRY_HEIGHT);
        private Size rowSize   = new Size(ENTRY_WIDTH * (Constants.VOICE_LANG_IDS.Length + 1), ENTRY_HEIGHT);

        private Dictionary<string, LanguageSettings> previousLanguages = new Dictionary<string, LanguageSettings>();
        private Dictionary<string, LanguageSettings> currentLanguages  = new Dictionary<string, LanguageSettings>();

        private Dictionary<string, Dictionary<string, ComboBox>> languageControls = new Dictionary<string, Dictionary<string, ComboBox>>();

        #endregion

        #region Constructor

        public FormMain()
        {
            InitializeComponent();
            Setup();
        }

        #endregion

        #region Setup

        private async void Setup()
        {
            Enabled = false;
            Invalidate();

            Settings.Load();

            BasicControlsSetup();
            RetrieveLanguageSettings();
            SetupLanguageControls();

            if (CheckInternetConnection())
            {
                if (await CheckForNewRelease())
                {
                    Application.Exit();
                }
                else
                {
                    Enabled = true;
                    Invalidate();
                }
            }
        }

        private bool CheckInternetConnection()
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send("8.8.8.8", 1000, new byte[32]);

            if (reply.Status != IPStatus.Success)
            {
                MessageBox.Show("It seems like you aren't connected to the internet, the tool will be unable to download voice assets to generate mods.\n" +
                                "If you've already pre-cached assets then you can safely ignore this warning",
                                "Internet Connection Failed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async Task<bool> CheckForNewRelease()
        {
            (bool hasNewer, SemVersion version) = await DownloadManager.HasNewRelease();

            if (hasNewer)
            {
                DialogResult result = MessageBox.Show("A new version of this tool is available!\n" +
                                                      "Would you like to download and install it now?", 
                                                      "New Version", 
                                                      MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    Process activeProc = Process.GetCurrentProcess();
                    string arguments = $"{activeProc.Id} {version}";

                    ProcessStartInfo startInfo = new ProcessStartInfo() {
                        Arguments = arguments,
                        CreateNoWindow = true,
                        FileName = Paths.UpdateAgent,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                    };

                    Process.Start(startInfo);
                    return true;
                }
            }

            return false;
        }

        private void BasicControlsSetup()
        {
            textGamePath.Enabled = false;
            textGamePath.Text = Settings.GamePath;
            btnPatch.Enabled  = !string.IsNullOrEmpty(Settings.GamePath);

            settingCache .Checked = Settings.UseCache   ?? false;
            settingBundle.Checked = Settings.BundleMods ?? false;
        }

        private void RetrieveLanguageSettings()
        {
            previousLanguages = ReadManifest();
            currentLanguages  = new Dictionary<string, LanguageSettings>(previousLanguages.Count);

            foreach (var pair in previousLanguages)
                currentLanguages.Add(pair.Key, pair.Value.Clone());
        }

        private void SetupLanguageControls()
        {
            FlowLayoutPanel headerFlow = new FlowLayoutPanel() {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            headerFlow.Controls.Add(new Label() { 
                Size = entrySize
            });

            foreach (var langId in Constants.VOICE_LANG_IDS)
            {
                headerFlow.Controls.Add(new Label() {
                    Size = entrySize,
                    Text = Constants.VoiceLanguages[langId].FullName,
                    TextAlign = ContentAlignment.BottomCenter
                });
            }

            flowMain.Controls.Add(headerFlow);

            languageControls = new Dictionary<string, Dictionary<string, ComboBox>>();

            foreach (var charId in Constants.VOICE_CHAR_IDS)
            {
                FlowLayoutPanel charFlow = SetupCharacterRow(charId);
                flowMain.Controls.Add(charFlow);
            }

            Invalidate();
        }

        private FlowLayoutPanel SetupCharacterRow(string charId)
        {
            FlowLayoutPanel flow = new FlowLayoutPanel() {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            flow.Controls.Add(new Label() {
                Size = entrySize,
                Text = Constants.VoiceCharacters[charId].ShortName,
                TextAlign = ContentAlignment.MiddleLeft
            });

            languageControls.Add(charId, new Dictionary<string, ComboBox>());

            foreach (string langId in Constants.VOICE_LANG_IDS)
            {
                ComboBox dropdown = new ComboBox() {
                    Size = entrySize,
                    Name = $"drop_{charId}_{langId}"
                };

                dropdown.Items.AddRange(Constants.VOICE_LANG_IDS);
                dropdown.SelectedIndexChanged += (obj, args) => SetLanguage(charId, langId, dropdown.SelectedItem.ToString(), true);
                flow.Controls.Add(dropdown);

                languageControls[charId].Add(langId, dropdown);

                SetLanguage(charId, langId, currentLanguages[charId][langId]);
            }

            return flow;
        }

        #endregion

        #region Utility

        private Dictionary<string, LanguageSettings> ReadManifest()
        {
            Dictionary<string, LanguageSettings> manifest = new Dictionary<string, LanguageSettings>();

            foreach (string charId in Constants.VOICE_CHAR_IDS)
                manifest.Add(charId, new LanguageSettings(charId));

            if (File.Exists(Paths.ModManifest))
            {
                string[] lines = File.ReadAllLines(Paths.ModManifest);

                for (int i = 0; i < lines.Length; ++i)
                {
                    string[] parts = lines[i].Split('=');

                    if (parts.Length < 2)
                        continue;

                    string[] keys = parts[0].Split('_');

                    string charId = keys[0].Trim().ToUpper();
                    string langId = keys[1].Trim().ToUpper();
                    string useId = parts[1].Trim().ToUpper();

                    if (manifest.ContainsKey(charId))
                    {
                        if (Constants.VOICE_LANG_IDS.Contains(langId) && Constants.VOICE_LANG_IDS.Contains(useId))
                            manifest[charId][langId] = useId;
                    }
                }
            }

            return manifest;
        }

        private void WriteManifest(Dictionary<string, LanguageSettings> settings)
        {
            string manifestDir = Path.GetDirectoryName(Paths.ModManifest);

            if (!Directory.Exists(manifestDir))
                Directory.CreateDirectory(manifestDir);

            using StreamWriter writer = File.CreateText(Paths.ModManifest);

            foreach (string charId in settings.Keys)
            {
                foreach (string langId in Constants.VOICE_LANG_IDS)
                {
                    // No point writing languages that haven't been changed
                    if (langId == settings[charId][langId])
                        continue;

                    writer.WriteLine($"{charId}_{langId}={settings[charId][langId]}");
                }
            }

            previousLanguages.Clear();

            foreach (var pair in settings)
                previousLanguages.Add(pair.Key, pair.Value.Clone());
        }

        private void SetLanguage(string charId, string langId, string value, bool fromDropdown = false)
        {
            if (!fromDropdown)
            {
                languageControls[charId][langId].SelectedItem = value;
            }
            else
            {
                currentLanguages[charId][langId] = value;
                languageControls[charId][langId].BackColor = Constants.VoiceLanguages[value].Colour;
            }
        }

        private void SetStatus(string text, int progressSize)
        {
            lblStatus.Text = text;
            progressStatus.Maximum = progressSize > 0 ? progressSize : 0;
            progressStatus.Value = 0;
        }

        private void ClearStatus()
        {
            lblStatus.Text = string.Empty;
            progressStatus.Maximum = 100;
            progressStatus.Value = 0;
        }

        private void IncrementProgress(int amount = 1)
        {
            if (progressStatus.Value + amount > progressStatus.Maximum)
                amount = progressStatus.Maximum - progressStatus.Value;

            progressStatus.Value += amount;
        }

        private bool ValidateUnrealPak()
        {
            if (!File.Exists(Paths.UPExecutable))
            {
                MessageBox.Show("UnrealPak.exe could not be located, without it this tool cannot function\n" +
                                "Try re-downloading GGSTVoiceMod and trying again\n" +
                                "If it still doesn't work then please contact me!! (the developer, me, hi! my info is in the 'Help' tab)",
                                "UnrealPak Missing",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private async Task<(bool result, long downloadSize)> ValidateAssetData(PatchInfo patch)
        {
            SetStatus("Validating asset data", patch.Count);

            long downloadSize = 0;
            List<string> assetsChecked = new List<string>();
            
            List<PatchInfo.LangPatch> failedPatches = new List<PatchInfo.LangPatch>();

            for (int i = patch.Count - 1; i >= 0; --i)
            {
                try
                {
                    string assetCode = $"{patch[i].Character}_{patch[i].UseLang}";

                    if (!assetsChecked.Contains(assetCode))
                    {
                        downloadSize += await DownloadManager.GetDownloadSize(patch[i].Character, patch[i].UseLang);
                        assetsChecked.Add(assetCode);
                    }
                }
                catch
                {
                    failedPatches.Add(patch[i]);
                    patch.RemovePatch(i);
                }

                IncrementProgress();
            }

            ClearStatus();

            if (failedPatches.Count > 0)
            {
                string failedList = string.Join("\n", failedPatches.Select(patch => $"{Constants.VoiceCharacters[patch.Character].ShortName} - {patch.OverLang} to {patch.UseLang}")
                                                                   .OrderBy(str => str));

                if (patch.Count > 0)
                {
                    DialogResult result = MessageBox.Show($"The following {failedPatches.Count} asset bundles failed to validate:\n" +
                                                          $"{failedList}\n\n" +
                                                          $"Would you like to continue patching without them?",
                                                          "Validation Failure",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return (false, 0);
                }
                else
                {
                    MessageBox.Show($"Failed to validate any of the required assets, the game can't be patched\n" +
                                    $"This is probably an issue!! Reach out to the developer if you need assistance (that's me! my details are under the 'Help' tab) ^-^",
                                    "Validation Failure",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return (false, 0);
                }
            }

            return (true, downloadSize);
        }

        private bool DisplayCachePrompt(long downloadSize)
        {
            double cacheMBs = downloadSize / 1_000_000d;

            DialogResult result = MessageBox.Show("Would you like to cache the downloaded assets so you don't have to download them again next time?\n" +
                                                  $"For this run it will require approximately {cacheMBs:N2}MBs of storage space\n\n" +
                                                  $"You can change this setting from the 'Settings' tab at any time",
                                                  "Download Cache",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        private bool DisplayBundlePrompt()
        {
            DialogResult result = MessageBox.Show("Would you like to install your voice pack as a single bundled mod instead of separately?\n" +
                                                  "This will save space and improve load times slightly but you will have to rebuild the entire mod next time you change voice settings\n" +
                                                  "If you choose not to bundle the mods then it will be quicker to install/uninstall voices in the future\n\n" +
                                                  "You can change this setting from the 'Settings' tab at any time",
                                                  "Bundle Mods",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        #endregion

        #region Callbacks

        private void OnLanguageChanged(string charId, string langId, string selectedLang)
        {
            currentLanguages[charId][langId] = selectedLang;
            languageControls[charId][langId].BackColor = Constants.VoiceLanguages[selectedLang].Colour;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
        }

        private void btnSelectGame_Click(object sender, EventArgs e)
        {
            bool exit = false;

            do
            {
                exit = true;

                if (fileSelectGame.ShowDialog() == DialogResult.OK)
                {
                    string filePath = Path.GetDirectoryName(fileSelectGame.FileName);
                    string fileName = Path.GetFileName(fileSelectGame.FileName);

                    bool validFile = false;

                    Shell32.Folder folder = new Shell32.Shell().NameSpace(filePath);

                    foreach (Shell32.FolderItem2 item in folder.Items())
                    {
                        if (item.Name == fileName)
                        {
                            validFile = folder.GetDetailsOf(item, 2)   == "Application"           &&
                                        folder.GetDetailsOf(item, 33)  == "Epic Games, Inc."      &&
                                        folder.GetDetailsOf(item, 34)  == "BootstrapPackagedGame" &&
                                        folder.GetDetailsOf(item, 166) == "4.25.0.0"              &&
                                        folder.GetDetailsOf(item, 190) == "GUILTY GEAR STRIVE";
                            break;
                        }
                    }

                    if (!validFile) 
                    {
                        DialogResult result = MessageBox.Show("The selected file doesn't seem like a valid Guilty Gear -Strive- executable (GGST.exe)\n\nIf you're sure it is then ignore this warning", "Invalid GGST.exe",
                                                              MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                        switch (result)
                        {
                            case DialogResult.Abort:
                                exit = true;
                                continue;
                            case DialogResult.Retry:
                                exit = false;
                                continue;
                        }
                    }

                    Settings.GamePath = fileSelectGame.FileName;
                    textGamePath.Text = Settings.GamePath;

                    btnPatch.Enabled = true;
                }
            }
            while (!exit);
        }

        private async void btnPatch_Click(object sender, EventArgs e)
        {
            if (!ValidateUnrealPak())
                return;

            Enabled = false;

            PatchInfo prev = new PatchInfo(previousLanguages);
            PatchInfo curr = new PatchInfo(currentLanguages);

            // First we gotta go through and uninstall any old mods that the user didn't set this time
            if (Directory.Exists(Paths.ModInstall))
            {
                PatchInfo uninstallDiff = prev.Diff(curr);

                if (uninstallDiff.Count > 0)
                {
                    SetStatus("Uninstalling old mods", uninstallDiff.Count);

                    foreach (var patch in uninstallDiff)
                    {
                        string modRoot = Path.Combine(Paths.ModInstall, patch.Character);

                        if (Directory.Exists(modRoot))
                            Directory.Delete(modRoot, true);

                        IncrementProgress();
                    }

                    ClearStatus();
                }
            }

            // Now we can install the new stuff!
            PatchInfo installDiff = curr.Diff(prev);

            if (installDiff.Count > 0)
            {
                (bool validated, long downloadSize) = await ValidateAssetData(installDiff);

                if (validated)
                {
                    if (Settings.UseCache == null)
                    {
                        Settings.UseCache = DisplayCachePrompt(downloadSize);
                        settingCache.Checked = Settings.UseCache ?? false;
                    }
                    if (Settings.BundleMods == null)
                    {
                        Settings.BundleMods = DisplayBundlePrompt();
                        settingBundle.Checked = Settings.BundleMods ?? false;
                    }

                    SetStatus("Generating mods", installDiff.Count);
                    await ModGenerator.Generate(installDiff, () => IncrementProgress());
                    ClearStatus();
                }
            }

            WriteManifest(currentLanguages);
            Enabled = true;
        }

        private async void filePrecache_Click(object sender, EventArgs e)
        {
            Enabled = false;

            Settings.UseCache = true;

            int assetCount = Constants.VOICE_CHAR_IDS.Length * Constants.VOICE_LANG_IDS.Length;
            long downloadSize = 0;

            SetStatus("Calculating download size", assetCount);

            foreach (string charId in Constants.VOICE_CHAR_IDS)
            {
                foreach (string langId in Constants.VOICE_LANG_IDS)
                {
                    downloadSize += await DownloadManager.GetDownloadSize(charId, langId);
                    IncrementProgress();
                }
            }

            ClearStatus();

            double megabytes = downloadSize / 1_000_000d;

            DialogResult result = MessageBox.Show($"Pre-caching all assets will require {megabytes:N2}MBs of space, are you sure you want to pre-cache?",
                                                  "Pre-cache Assets", 
                                                  MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                SetStatus("Pre-caching assets", assetCount);

                foreach (string charId in Constants.VOICE_CHAR_IDS)
                {
                    foreach (string langId in Constants.VOICE_LANG_IDS)
                    {
                        await DownloadManager.DownloadAsset(charId, langId);
                        IncrementProgress();
                    }
                }

                ClearStatus();
            }

            Enabled = true;
        }

        private void fileUninstall_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Paths.ModInstall))
            {
                DialogResult result = MessageBox.Show("Are you sure you want to uninstall all the voice mods?\n" +
                                                      "You won't be able to recover them and will have to generate them again later",
                                                      "Uninstall All",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    Directory.Delete(Paths.ModInstall, true);
            }
            else
            {
                MessageBox.Show("You haven't installed anything yet!!", "ahhh, etto...bwehhhh _/xwx\\_");
            }
        }

        private void fileSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog() {
                AddExtension = true,
                DefaultExt = "ivo",
                Filter = "Individual Voice Options|*.ivo",
                OverwritePrompt = true,
                InitialDirectory = Paths.ExecutableRoot,
                RestoreDirectory = true
            };

            if (file.ShowDialog() == DialogResult.OK)
                new PatchInfo(currentLanguages).ToFile(file.FileName);
        }

        private void fileSaveClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(new PatchInfo(currentLanguages).ToBase64());
        }

        private void fileLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog() {
                AddExtension = true,
                CheckFileExists = true,
                DefaultExt = "ivo",
                Filter = "Individual Voice Options|*.ivo",
                InitialDirectory = Paths.ExecutableRoot,
                RestoreDirectory = true
            };

            if (file.ShowDialog() == DialogResult.OK)
            {
                PatchInfo patch = new PatchInfo();

                if (patch.FromFile(file.FileName))
                {
                    foreach (var lang in patch)
                        SetLanguage(lang.Character, lang.OverLang, lang.UseLang);
                }
                else
                {
                    MessageBox.Show("Something went wrong loading the file!\n" +
                                    "If you're sure the file is correct then please contact me ^-^",
                                    "Load Failed");
                }
            }
        }

        private void fileLoadClipboard_Click(object sender, EventArgs e)
        {
            string base64 = Clipboard.GetText();
            PatchInfo patch = new PatchInfo();

            if (patch.FromBase64(base64))
            {
                foreach (var lang in patch)
                    SetLanguage(lang.Character, lang.OverLang, lang.UseLang);
            }
            else
            {
                MessageBox.Show("Looks like there's something wrong with the text in your clipboard!\n" +
                                "Please double check that you copied the right text, if the issue persists then contact me ^-^",
                                "Load Failed");
            }
        }

        private void settingCache_Click(object sender, EventArgs e)
        {
            Settings.UseCache = !(Settings.UseCache ?? false);
            settingCache.Checked = (bool)Settings.UseCache;
        }

        private void settingBundle_Click(object sender, EventArgs e)
        {
            Settings.BundleMods = !(Settings.BundleMods ?? false);
            settingBundle.Checked = (bool)Settings.BundleMods;
        }

        private void helpAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GGSTVoiceMod is a tool created by 'android ♥' to allow players to set individual voice languages for each character!\n\n" +
                            "The tool works by generating mods for each language setting you choose and installing them directly to the game for you. " +
                            "You can choose to either install the mods separately, or bundle them all into a single mod to save on space and load times!\n\n" +
                            "GGSTVoiceMod requires internet access to function as it has to download the voice assets, " +
                            "however you can use the 'Pre-cache' button in the 'File' menu to download everything ahead of time.\n\n" +
                            "I hope the tool is easy to use and does what you want it to, but if you have any problems you can contact me anytime! Enjoy!! uwu\n\n" +
                            $"Version: {SemVersion.Current}",
                            "About");
        }

        private void helpContact_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("If you have any issues or are confused about the tool, please contact me!!\n\n" +
                                                  "Discord: android <3#0003\n" +
                                                  "Twitter: @drone_03\n\n" +
                                                  "Would you like to submit a bug report on GitHub?",
                                                  "Contact Me!",
                                                  MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Process.Start("explorer", "https://github.com/Unit-03/GGSTVoiceMod/issues/new");
        }

        #endregion
    }
}
