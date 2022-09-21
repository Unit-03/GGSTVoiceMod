using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace GGSTVoiceMod
{
    public partial class FormMain : Form
    {
        #region Fields

        private const int ENTRY_WIDTH = 100;
        private const int ENTRY_HEIGHT = 25;

        private Size entrySize = new Size(ENTRY_WIDTH, ENTRY_HEIGHT);
        private Size rowSize = new Size(ENTRY_WIDTH * (Constants.LANGUAGE_IDS.Length + 1), ENTRY_HEIGHT);

        private Dictionary<string, LanguageSettings> languageSettings = new Dictionary<string, LanguageSettings>();

        #endregion

        #region Constructor

        public FormMain()
        {
            InitializeComponent();

            RetrieveLanguageSettings();
            SetupLanguageControls();
        }

        #endregion

        #region Setup

        private void RetrieveLanguageSettings()
        {
            foreach (string charId in Constants.CHARACTER_IDS)
                languageSettings.Add(charId, new LanguageSettings(charId));
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

        #region Callbacks

        private void OnLanguageChanged(string charId, string langId, string selectedLang)
        {
            languageSettings[charId][langId] = selectedLang;
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

                    textGamePath.Text = fileSelectGame.FileName;
                }
            }
            while (!exit);
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
