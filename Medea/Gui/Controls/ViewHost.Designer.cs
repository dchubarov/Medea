namespace Gyrosoft.Medea.Gui
{
    partial class ViewHost
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewHost));
            this.viewTools = new System.Windows.Forms.ToolStrip();
            this.navBackButton = new System.Windows.Forms.ToolStripSplitButton();
            this.navForwardButton = new System.Windows.Forms.ToolStripSplitButton();
            this.navHomeButton = new System.Windows.Forms.ToolStripButton();
            this.hostedToolsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.hostPanel = new System.Windows.Forms.Panel();
            this.captionPanel = new System.Windows.Forms.Panel();
            this.captionBox = new System.Windows.Forms.GroupBox();
            this.captionLabel = new System.Windows.Forms.Label();
            this.viewTools.SuspendLayout();
            this.captionPanel.SuspendLayout();
            this.captionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewTools
            // 
            resources.ApplyResources(this.viewTools, "viewTools");
            this.viewTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.viewTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navBackButton,
            this.navForwardButton,
            this.navHomeButton,
            this.hostedToolsSeparator});
            this.viewTools.Name = "viewTools";
            this.viewTools.Click += new System.EventHandler(this.captionArea_Click);
            // 
            // navBackButton
            // 
            this.navBackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navBackButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapNavBack_HighColor;
            resources.ApplyResources(this.navBackButton, "navBackButton");
            this.navBackButton.Name = "navBackButton";
            this.navBackButton.ButtonClick += new System.EventHandler(this.navigationCmd_Click);
            // 
            // navForwardButton
            // 
            this.navForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navForwardButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapNavForward_HighColor;
            resources.ApplyResources(this.navForwardButton, "navForwardButton");
            this.navForwardButton.Name = "navForwardButton";
            this.navForwardButton.ButtonClick += new System.EventHandler(this.navigationCmd_Click);
            // 
            // navHomeButton
            // 
            this.navHomeButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapTask_HighColor;
            resources.ApplyResources(this.navHomeButton, "navHomeButton");
            this.navHomeButton.Name = "navHomeButton";
            this.navHomeButton.Click += new System.EventHandler(this.navigationHomeCmd_Click);
            // 
            // hostedToolsSeparator
            // 
            this.hostedToolsSeparator.Name = "hostedToolsSeparator";
            resources.ApplyResources(this.hostedToolsSeparator, "hostedToolsSeparator");
            // 
            // hostPanel
            // 
            this.hostPanel.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.hostPanel, "hostPanel");
            this.hostPanel.Name = "hostPanel";
            // 
            // captionPanel
            // 
            this.captionPanel.Controls.Add(this.captionBox);
            resources.ApplyResources(this.captionPanel, "captionPanel");
            this.captionPanel.Name = "captionPanel";
            // 
            // captionBox
            // 
            resources.ApplyResources(this.captionBox, "captionBox");
            this.captionBox.BackColor = System.Drawing.SystemColors.Window;
            this.captionBox.Controls.Add(this.captionLabel);
            this.captionBox.Name = "captionBox";
            this.captionBox.TabStop = false;
            // 
            // captionLabel
            // 
            resources.ApplyResources(this.captionLabel, "captionLabel");
            this.captionLabel.Name = "captionLabel";
            // 
            // ViewHost
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.hostPanel);
            this.Controls.Add(this.viewTools);
            this.Controls.Add(this.captionPanel);
            this.Name = "ViewHost";
            this.Load += new System.EventHandler(this.ViewHost_Load);
            this.viewTools.ResumeLayout(false);
            this.viewTools.PerformLayout();
            this.captionPanel.ResumeLayout(false);
            this.captionBox.ResumeLayout(false);
            this.captionBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip viewTools;
        private System.Windows.Forms.ToolStripSplitButton navBackButton;
        private System.Windows.Forms.ToolStripSplitButton navForwardButton;
        private System.Windows.Forms.ToolStripSeparator hostedToolsSeparator;
        private System.Windows.Forms.ToolStripButton navHomeButton;
        private System.Windows.Forms.Panel hostPanel;
        private System.Windows.Forms.Panel captionPanel;
        private System.Windows.Forms.GroupBox captionBox;
        private System.Windows.Forms.Label captionLabel;

    }
}
