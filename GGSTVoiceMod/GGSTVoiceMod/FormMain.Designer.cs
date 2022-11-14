
namespace GGSTVoiceMod
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textGamePath = new System.Windows.Forms.TextBox();
            this.btnSelectGame = new System.Windows.Forms.Button();
            this.lblGamePath = new System.Windows.Forms.Label();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPatch = new System.Windows.Forms.Button();
            this.fileSelectGame = new System.Windows.Forms.OpenFileDialog();
            this.progressStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.stripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.filePrecache = new System.Windows.Forms.ToolStripMenuItem();
            this.fileUninstall = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.fileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.fileLoadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.fileLoadClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.settingCache = new System.Windows.Forms.ToolStripMenuItem();
            this.settingBundle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.helpContact = new System.Windows.Forms.ToolStripMenuItem();
            this.panelNarration = new System.Windows.Forms.Panel();
            this.checkSilence = new System.Windows.Forms.CheckBox();
            this.dropNarrChar = new System.Windows.Forms.ComboBox();
            this.dropNarrLang = new System.Windows.Forms.ComboBox();
            this.lblNarration = new System.Windows.Forms.Label();
            this.stripMain.SuspendLayout();
            this.panelNarration.SuspendLayout();
            this.SuspendLayout();
            // 
            // textGamePath
            // 
            this.textGamePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textGamePath.BackColor = System.Drawing.SystemColors.Control;
            this.textGamePath.Location = new System.Drawing.Point(56, 27);
            this.textGamePath.Name = "textGamePath";
            this.textGamePath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textGamePath.Size = new System.Drawing.Size(375, 23);
            this.textGamePath.TabIndex = 1;
            // 
            // btnSelectGame
            // 
            this.btnSelectGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectGame.Location = new System.Drawing.Point(437, 27);
            this.btnSelectGame.Name = "btnSelectGame";
            this.btnSelectGame.Size = new System.Drawing.Size(75, 23);
            this.btnSelectGame.TabIndex = 2;
            this.btnSelectGame.Text = "Browse";
            this.btnSelectGame.UseVisualStyleBackColor = true;
            this.btnSelectGame.Click += new System.EventHandler(this.btnSelectGame_Click);
            // 
            // lblGamePath
            // 
            this.lblGamePath.AutoSize = true;
            this.lblGamePath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGamePath.Location = new System.Drawing.Point(12, 30);
            this.lblGamePath.Name = "lblGamePath";
            this.lblGamePath.Size = new System.Drawing.Size(38, 15);
            this.lblGamePath.TabIndex = 3;
            this.lblGamePath.Text = "Game";
            // 
            // flowMain
            // 
            this.flowMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowMain.AutoScroll = true;
            this.flowMain.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flowMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowMain.Location = new System.Drawing.Point(12, 56);
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size(500, 489);
            this.flowMain.TabIndex = 4;
            this.flowMain.WrapContents = false;
            // 
            // btnPatch
            // 
            this.btnPatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPatch.Location = new System.Drawing.Point(437, 617);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(75, 23);
            this.btnPatch.TabIndex = 5;
            this.btnPatch.Text = "Patch";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // fileSelectGame
            // 
            this.fileSelectGame.DefaultExt = "exe";
            this.fileSelectGame.FileName = "GGST.exe";
            this.fileSelectGame.Filter = "Guilty Gear -Strive-|GGST.exe";
            // 
            // progressStatus
            // 
            this.progressStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressStatus.Location = new System.Drawing.Point(12, 646);
            this.progressStatus.Name = "progressStatus";
            this.progressStatus.Size = new System.Drawing.Size(500, 23);
            this.progressStatus.TabIndex = 6;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 621);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 7;
            // 
            // stripMain
            // 
            this.stripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuSettings,
            this.menuHelp});
            this.stripMain.Location = new System.Drawing.Point(0, 0);
            this.stripMain.Name = "stripMain";
            this.stripMain.Size = new System.Drawing.Size(524, 24);
            this.stripMain.TabIndex = 8;
            this.stripMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filePrecache,
            this.fileUninstall,
            this.fileSeparator1,
            this.fileSave,
            this.fileLoad});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // filePrecache
            // 
            this.filePrecache.Name = "filePrecache";
            this.filePrecache.Size = new System.Drawing.Size(127, 22);
            this.filePrecache.Text = "Pre-cache";
            this.filePrecache.Click += new System.EventHandler(this.filePrecache_Click);
            // 
            // fileUninstall
            // 
            this.fileUninstall.Name = "fileUninstall";
            this.fileUninstall.Size = new System.Drawing.Size(127, 22);
            this.fileUninstall.Text = "Uninstall";
            this.fileUninstall.Click += new System.EventHandler(this.fileUninstall_Click);
            // 
            // fileSeparator1
            // 
            this.fileSeparator1.Name = "fileSeparator1";
            this.fileSeparator1.Size = new System.Drawing.Size(124, 6);
            // 
            // fileSave
            // 
            this.fileSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileSaveFile,
            this.fileSaveClipboard});
            this.fileSave.Name = "fileSave";
            this.fileSave.Size = new System.Drawing.Size(127, 22);
            this.fileSave.Text = "Save";
            // 
            // fileSaveFile
            // 
            this.fileSaveFile.Name = "fileSaveFile";
            this.fileSaveFile.Size = new System.Drawing.Size(141, 22);
            this.fileSaveFile.Text = "To File";
            this.fileSaveFile.Click += new System.EventHandler(this.fileSaveFile_Click);
            // 
            // fileSaveClipboard
            // 
            this.fileSaveClipboard.Name = "fileSaveClipboard";
            this.fileSaveClipboard.Size = new System.Drawing.Size(141, 22);
            this.fileSaveClipboard.Text = "To Clipboard";
            this.fileSaveClipboard.Click += new System.EventHandler(this.fileSaveClipboard_Click);
            // 
            // fileLoad
            // 
            this.fileLoad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileLoadFile,
            this.fileLoadClipboard});
            this.fileLoad.Name = "fileLoad";
            this.fileLoad.Size = new System.Drawing.Size(127, 22);
            this.fileLoad.Text = "Load";
            // 
            // fileLoadFile
            // 
            this.fileLoadFile.Name = "fileLoadFile";
            this.fileLoadFile.Size = new System.Drawing.Size(157, 22);
            this.fileLoadFile.Text = "From File";
            this.fileLoadFile.Click += new System.EventHandler(this.fileLoadFile_Click);
            // 
            // fileLoadClipboard
            // 
            this.fileLoadClipboard.Name = "fileLoadClipboard";
            this.fileLoadClipboard.Size = new System.Drawing.Size(157, 22);
            this.fileLoadClipboard.Text = "From Clipboard";
            this.fileLoadClipboard.Click += new System.EventHandler(this.fileLoadClipboard_Click);
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingCache,
            this.settingBundle});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(61, 20);
            this.menuSettings.Text = "Settings";
            // 
            // settingCache
            // 
            this.settingCache.Name = "settingCache";
            this.settingCache.Size = new System.Drawing.Size(164, 22);
            this.settingCache.Text = "Download Cache";
            this.settingCache.Click += new System.EventHandler(this.settingCache_Click);
            // 
            // settingBundle
            // 
            this.settingBundle.Name = "settingBundle";
            this.settingBundle.Size = new System.Drawing.Size(164, 22);
            this.settingBundle.Text = "Bundle Mods";
            this.settingBundle.Click += new System.EventHandler(this.settingBundle_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAbout,
            this.helpContact});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "Help";
            // 
            // helpAbout
            // 
            this.helpAbout.Name = "helpAbout";
            this.helpAbout.Size = new System.Drawing.Size(136, 22);
            this.helpAbout.Text = "About";
            this.helpAbout.Click += new System.EventHandler(this.helpAbout_Click);
            // 
            // helpContact
            // 
            this.helpContact.Name = "helpContact";
            this.helpContact.Size = new System.Drawing.Size(136, 22);
            this.helpContact.Text = "Contact Me";
            this.helpContact.Click += new System.EventHandler(this.helpContact_Click);
            // 
            // panelNarration
            // 
            this.panelNarration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNarration.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNarration.Controls.Add(this.checkSilence);
            this.panelNarration.Controls.Add(this.dropNarrChar);
            this.panelNarration.Controls.Add(this.dropNarrLang);
            this.panelNarration.Controls.Add(this.lblNarration);
            this.panelNarration.Location = new System.Drawing.Point(12, 551);
            this.panelNarration.Name = "panelNarration";
            this.panelNarration.Size = new System.Drawing.Size(500, 60);
            this.panelNarration.TabIndex = 9;
            // 
            // checkSilence
            // 
            this.checkSilence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkSilence.AutoSize = true;
            this.checkSilence.Location = new System.Drawing.Point(339, 32);
            this.checkSilence.Name = "checkSilence";
            this.checkSilence.Size = new System.Drawing.Size(158, 19);
            this.checkSilence.TabIndex = 3;
            this.checkSilence.Text = "Silence missing narration";
            this.checkSilence.UseVisualStyleBackColor = true;
            // 
            // dropNarrChar
            // 
            this.dropNarrChar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dropNarrChar.Location = new System.Drawing.Point(185, 5);
            this.dropNarrChar.Name = "dropNarrChar";
            this.dropNarrChar.Size = new System.Drawing.Size(310, 23);
            this.dropNarrChar.TabIndex = 2;
            this.dropNarrChar.SelectedIndexChanged += new System.EventHandler(this.dropNarrChar_SelectedIndexChanged);
            // 
            // dropNarrLang
            // 
            this.dropNarrLang.Location = new System.Drawing.Point(69, 5);
            this.dropNarrLang.Name = "dropNarrLang";
            this.dropNarrLang.Size = new System.Drawing.Size(110, 23);
            this.dropNarrLang.TabIndex = 1;
            this.dropNarrLang.SelectedIndexChanged += new System.EventHandler(this.dropNarrLang_SelectedIndexChanged);
            // 
            // lblNarration
            // 
            this.lblNarration.AutoSize = true;
            this.lblNarration.Location = new System.Drawing.Point(3, 8);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Size = new System.Drawing.Size(60, 15);
            this.lblNarration.TabIndex = 0;
            this.lblNarration.Text = "Narration:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 681);
            this.Controls.Add(this.panelNarration);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressStatus);
            this.Controls.Add(this.btnPatch);
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.lblGamePath);
            this.Controls.Add(this.btnSelectGame);
            this.Controls.Add(this.textGamePath);
            this.Controls.Add(this.stripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.stripMain;
            this.MinimumSize = new System.Drawing.Size(360, 360);
            this.Name = "FormMain";
            this.Text = "GGST Voice Mod";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.stripMain.ResumeLayout(false);
            this.stripMain.PerformLayout();
            this.panelNarration.ResumeLayout(false);
            this.panelNarration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textGamePath;
        private System.Windows.Forms.Button btnSelectGame;
        private System.Windows.Forms.Label lblGamePath;
        private System.Windows.Forms.FlowLayoutPanel flowMain;
        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.OpenFileDialog fileSelectGame;
        private System.Windows.Forms.ProgressBar progressStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.MenuStrip stripMain;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem settingCache;
        private System.Windows.Forms.ToolStripMenuItem settingBundle;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem helpAbout;
        private System.Windows.Forms.ToolStripMenuItem helpContact;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem fileUninstall;
        private System.Windows.Forms.ToolStripMenuItem filePrecache;
        private System.Windows.Forms.ToolStripSeparator fileSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fileSave;
        private System.Windows.Forms.ToolStripMenuItem fileLoad;
        private System.Windows.Forms.ToolStripMenuItem fileSaveFile;
        private System.Windows.Forms.ToolStripMenuItem fileSaveClipboard;
        private System.Windows.Forms.ToolStripMenuItem fileLoadFile;
        private System.Windows.Forms.ToolStripMenuItem fileLoadClipboard;
        private System.Windows.Forms.Panel panelNarration;
        private System.Windows.Forms.Label lblNarration;
        private System.Windows.Forms.ComboBox dropNarrLang;
        private System.Windows.Forms.ComboBox dropNarrChar;
        private System.Windows.Forms.CheckBox checkSilence;
    }
}

