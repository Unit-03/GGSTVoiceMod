
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.settingCache = new System.Windows.Forms.ToolStripMenuItem();
            this.settingBundle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
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
            this.flowMain.Size = new System.Drawing.Size(500, 555);
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
            this.lblStatus.Size = new System.Drawing.Size(59, 15);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Standby...";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(524, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(524, 681);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressStatus);
            this.Controls.Add(this.btnPatch);
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.lblGamePath);
            this.Controls.Add(this.btnSelectGame);
            this.Controls.Add(this.textGamePath);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(360, 360);
            this.Name = "FormMain";
            this.Text = "GGST Voice Mod";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem settingCache;
        private System.Windows.Forms.ToolStripMenuItem settingBundle;
    }
}

