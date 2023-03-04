namespace Gyrosoft.Medea.Gui
{
    partial class AppMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppMainForm));
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.viewHost1 = new Gyrosoft.Medea.Gui.ViewHost();
            this.viewHost2 = new Gyrosoft.Medea.Gui.ViewHost();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuViewSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.personalizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelpHomePage = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelpTechSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.viewStateTimer = new System.Windows.Forms.Timer(this.components);
            this.layoutHostPanel = new System.Windows.Forms.Panel();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.layoutHostPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.viewHost1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.viewHost2);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.TabStop = false;
            // 
            // viewHost1
            // 
            this.viewHost1.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.viewHost1, "viewHost1");
            this.viewHost1.Name = "viewHost1";
            this.viewHost1.SuppressCanClose = false;
            // 
            // viewHost2
            // 
            this.viewHost2.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.viewHost2, "viewHost2");
            this.viewHost2.Name = "viewHost2";
            this.viewHost2.SuppressCanClose = false;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFile,
            this.mainMenuView,
            this.mainMenuTools,
            this.mainMenuHelp});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // mainMenuFile
            // 
            this.mainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFileExit});
            this.mainMenuFile.Name = "mainMenuFile";
            resources.ApplyResources(this.mainMenuFile, "mainMenuFile");
            // 
            // mainMenuFileExit
            // 
            this.mainMenuFileExit.Name = "mainMenuFileExit";
            resources.ApplyResources(this.mainMenuFileExit, "mainMenuFileExit");
            this.mainMenuFileExit.Click += new System.EventHandler(this.mainMenuFileExit_Click);
            // 
            // mainMenuView
            // 
            this.mainMenuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuViewSplit});
            this.mainMenuView.Name = "mainMenuView";
            resources.ApplyResources(this.mainMenuView, "mainMenuView");
            this.mainMenuView.DropDownOpening += new System.EventHandler(this.mainMenuView_DropDownOpening);
            // 
            // mainMenuViewSplit
            // 
            resources.ApplyResources(this.mainMenuViewSplit, "mainMenuViewSplit");
            this.mainMenuViewSplit.Name = "mainMenuViewSplit";
            this.mainMenuViewSplit.Click += new System.EventHandler(this.mainMenuViewSplit_Click);
            // 
            // mainMenuTools
            // 
            this.mainMenuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.personalizationToolStripMenuItem});
            this.mainMenuTools.Name = "mainMenuTools";
            resources.ApplyResources(this.mainMenuTools, "mainMenuTools");
            // 
            // personalizationToolStripMenuItem
            // 
            this.personalizationToolStripMenuItem.Name = "personalizationToolStripMenuItem";
            resources.ApplyResources(this.personalizationToolStripMenuItem, "personalizationToolStripMenuItem");
            this.personalizationToolStripMenuItem.Click += new System.EventHandler(this.personalizationToolStripMenuItem_Click);
            // 
            // mainMenuHelp
            // 
            this.mainMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuHelpContents,
            toolStripSeparator1,
            this.mainMenuHelpHomePage,
            this.mainMenuHelpTechSupport,
            toolStripSeparator2,
            this.mainMenuHelpAbout});
            this.mainMenuHelp.Name = "mainMenuHelp";
            resources.ApplyResources(this.mainMenuHelp, "mainMenuHelp");
            this.mainMenuHelp.DropDownOpening += new System.EventHandler(this.mainMenuHelp_DropDownOpening);
            // 
            // mainMenuHelpContents
            // 
            this.mainMenuHelpContents.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapHelp_HighColor;
            resources.ApplyResources(this.mainMenuHelpContents, "mainMenuHelpContents");
            this.mainMenuHelpContents.Name = "mainMenuHelpContents";
            // 
            // mainMenuHelpHomePage
            // 
            this.mainMenuHelpHomePage.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapHome_HighColor;
            resources.ApplyResources(this.mainMenuHelpHomePage, "mainMenuHelpHomePage");
            this.mainMenuHelpHomePage.Name = "mainMenuHelpHomePage";
            // 
            // mainMenuHelpTechSupport
            // 
            this.mainMenuHelpTechSupport.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapDial_HighColor;
            resources.ApplyResources(this.mainMenuHelpTechSupport, "mainMenuHelpTechSupport");
            this.mainMenuHelpTechSupport.Name = "mainMenuHelpTechSupport";
            // 
            // mainMenuHelpAbout
            // 
            this.mainMenuHelpAbout.Name = "mainMenuHelpAbout";
            resources.ApplyResources(this.mainMenuHelpAbout, "mainMenuHelpAbout");
            this.mainMenuHelpAbout.Click += new System.EventHandler(this.mainMenuHelpAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMsg,
            this.statusProgress});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            // 
            // statusMsg
            // 
            this.statusMsg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusMsg.Name = "statusMsg";
            resources.ApplyResources(this.statusMsg, "statusMsg");
            this.statusMsg.Spring = true;
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            resources.ApplyResources(this.statusProgress, "statusProgress");
            // 
            // viewStateTimer
            // 
            this.viewStateTimer.Interval = 10;
            this.viewStateTimer.Tick += new System.EventHandler(this.viewStateTimer_Tick);
            // 
            // layoutHostPanel
            // 
            this.layoutHostPanel.Controls.Add(this.splitContainer);
            resources.ApplyResources(this.layoutHostPanel, "layoutHostPanel");
            this.layoutHostPanel.Name = "layoutHostPanel";
            // 
            // AppMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutHostPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "AppMainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Closed);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AppMainForm_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.layoutHostPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelp;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelpAbout;
        private System.Windows.Forms.ToolStripStatusLabel statusMsg;
        private System.Windows.Forms.ToolStripMenuItem mainMenuTools;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFile;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelpHomePage;
        private System.Windows.Forms.ToolStripMenuItem mainMenuHelpTechSupport;
        private System.Windows.Forms.ToolStripMenuItem mainMenuView;
        private System.Windows.Forms.ToolStripMenuItem mainMenuViewSplit;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.Timer viewStateTimer;
        private System.Windows.Forms.Panel layoutHostPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private ViewHost viewHost1;
        private ViewHost viewHost2;
        private System.Windows.Forms.ToolStripMenuItem personalizationToolStripMenuItem;

    }
}