using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GGSTVoiceMod
{
    public partial class FormMain : Form
    {
        // TODO:
        // - On startup check for an internet connection! this tool doesn't work without it :$
        // - On startup check if there's a manifest in the mods directory and update the list accordingly
        // - Download assets when the user clicks "Patch" (checking the cache first if it exists)
        // - Inform the user about caching and if they choose "yes" then cache the assets
        // - Inform the user about mod-bundling and branch from there
        // -> If the user chooses mod-bundling then move all necessary assets to UnrealPak and generate the mod
        // --> Clear the mods folder and install the new mod
        // -> If the user doesn't want mod-bundling then build the mods one at a time and move them over to the mods folder
        // -> Generate a manifest listing which assets are installed

        #region Fields

        // should these be in Constants.cs? yes. why aren't they? idk get off my back debra
        private const int ENTRY_WIDTH  = 100;
        private const int ENTRY_HEIGHT = 25;

        private Size entrySize = new Size(ENTRY_WIDTH, ENTRY_HEIGHT);
        private Size rowSize   = new Size(ENTRY_WIDTH * (Constants.LANGUAGE_IDS.Length + 1), ENTRY_HEIGHT);

        private Dictionary<string, LanguageSettings> previousLanguages = new Dictionary<string, LanguageSettings>();
        private Dictionary<string, LanguageSettings> currentLanguages  = new Dictionary<string, LanguageSettings>();

        #endregion

        #region Constructor

        public FormMain()
        {
            InitializeComponent();

            Enabled = false;
            Invalidate();

            textGamePath.Enabled = false;
            btnPatch.Enabled = false;

            RetrieveLanguageSettings();
            SetupLanguageControls();

            Enabled = true;
            Invalidate();
        }

        #endregion

        #region Setup

        private void RetrieveLanguageSettings()
        {
            previousLanguages = new Dictionary<string, LanguageSettings>();

            foreach (string charId in Constants.CHARACTER_IDS)
                previousLanguages.Add(charId, new LanguageSettings(charId));

            if (File.Exists(Paths.Manifest))
            {
                string[] lines = File.ReadAllLines(Paths.Manifest);

                for (int i = 0; i < lines.Length; ++i)
                {
                    string[] parts = lines[i].Split('=');

                    if (parts.Length < 2)
                        continue;

                    string[] keys = parts[0].Split('_');

                    string charId = keys[0].Trim().ToUpper();
                    string langId = keys[1].Trim().ToUpper();
                    string useId = parts[1].Trim().ToUpper();

                    if (previousLanguages.ContainsKey(charId))
                    {
                        if (Constants.LANGUAGE_IDS.Contains(langId) && Constants.LANGUAGE_IDS.Contains(useId))
                            previousLanguages[charId][langId] = useId;
                    }
                }
            }

            currentLanguages = new Dictionary<string, LanguageSettings>(previousLanguages.Count);

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

            foreach (var langId in Constants.LANGUAGE_IDS)
            {
                headerFlow.Controls.Add(new Label() {
                    Size = entrySize,
                    Text = Constants.Languages[langId].FullName,
                    TextAlign = ContentAlignment.BottomCenter
                });
            }

            flowMain.Controls.Add(headerFlow);

            foreach (var charId in Constants.CHARACTER_IDS)
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
                Text = Constants.Characters[charId].ShortName,
                TextAlign = ContentAlignment.MiddleLeft
            });

            foreach (string langId in Constants.LANGUAGE_IDS)
            {
                ComboBox dropdown = new ComboBox() {
                    Size = entrySize,
                    Name = $"drop_{charId}_{langId}"
                };

                dropdown.Items.AddRange(Constants.LANGUAGE_IDS);
                dropdown.SelectedItem = langId;
                dropdown.SelectedIndexChanged += (obj, args) => OnLanguageChanged(charId, langId, dropdown.SelectedItem.ToString());

                flow.Controls.Add(dropdown);
            }

            return flow;
        }

        #endregion

        #region Utility

        private void SetStatus(string text, int progressSize)
        {
            lblStatus.Text = text;
            progressStatus.Maximum = progressSize;
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

        private async Task<(bool result, long downloadSize)> ValidateAssetData(PatchInfo patch)
        {
            SetStatus("Validating asset data", patch.Count - 1);

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
                        using WebResponse response = await DownloadManager.GetHeaderData(patch[i].Character, patch[i].UseLang);
                        downloadSize += response.ContentLength;

                        assetsChecked.Add(assetCode);
                    }
                }
                catch (WebException)
                {
                    failedPatches.Add(patch[i]);
                    patch.RemovePatch(i);
                }

                IncrementProgress();
            }

            ClearStatus();

            if (failedPatches.Count > 0)
            {
                string failedList = string.Join("\n", failedPatches.Select(patch => $"{Constants.Characters[patch.Character].ShortName} - {patch.OverLang} to {patch.UseLang}")
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
                                    $"This is probably an issue!! Reach out to the developer (that's me! my details are under the 'Help' tab) if you need assistance ^-^",
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
                                                  $"This will require approximately {cacheMBs:N2}MBs of storage space\n\n" +
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

                    Paths.GameRoot = filePath;
                    textGamePath.Text = fileSelectGame.FileName;

                    btnPatch.Enabled = true;
                }
            }
            while (!exit);
        }

        private async void btnPatch_Click(object sender, EventArgs e)
        {
            PatchInfo prev = new PatchInfo(previousLanguages);
            PatchInfo curr = new PatchInfo(currentLanguages);

            PatchInfo diff = curr.Diff(prev);

            if (diff.Count == 0)
                return;

            (bool validated, long downloadSize) = await ValidateAssetData(diff);

            if (!validated)
                return;

            if (Settings.UseCache == null)
                Settings.UseCache = DisplayCachePrompt(downloadSize);
            if (Settings.BundleMods == null)
                Settings.BundleMods = DisplayBundlePrompt();
        }

        #endregion
    }
}
