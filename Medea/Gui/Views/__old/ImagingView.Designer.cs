namespace Gyrosoft.Medea.Gui
{
    partial class ImagingView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagingView));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.imageHost = new System.Windows.Forms.PictureBox();
            this.collectionListView = new Gyrosoft.Medea.Gui.ImageCollectionListView();
            this.controlImageList = new System.Windows.Forms.ImageList(this.components);
            this.imagingTools = new System.Windows.Forms.ToolStrip();
            this.captureStartButton = new System.Windows.Forms.ToolStripButton();
            this.captureStopButton = new System.Windows.Forms.ToolStripButton();
            this.captureSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.collectionToggleButton = new System.Windows.Forms.ToolStripButton();
            this.imagingMenu = new System.Windows.Forms.MenuStrip();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thumbnailUpdateWorker = new Gyrosoft.Medea.Imaging.ThumbnailUpdateWorker();
            this.capturedevicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x240ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x480ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x576ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x768ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interlacedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageHost)).BeginInit();
            this.imagingTools.SuspendLayout();
            this.imagingMenu.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.imagePanel);
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Resize += new System.EventHandler(this.splitContainer_Panel1_Resize);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.collectionListView);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // imagePanel
            // 
            this.imagePanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.imagePanel.Controls.Add(this.imageHost);
            resources.ApplyResources(this.imagePanel, "imagePanel");
            this.imagePanel.Name = "imagePanel";
            // 
            // imageHost
            // 
            this.imageHost.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.imageHost, "imageHost");
            this.imageHost.Name = "imageHost";
            this.imageHost.TabStop = false;
            // 
            // collectionListView
            // 
            this.collectionListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.collectionListView, "collectionListView");
            this.collectionListView.LargeImageList = this.controlImageList;
            this.collectionListView.Name = "collectionListView";
            this.collectionListView.OwnerDraw = true;
            this.collectionListView.UseCompatibleStateImageBehavior = false;
            this.collectionListView.DoubleClick += new System.EventHandler(this.collectionListView_DoubleClick);
            // 
            // controlImageList
            // 
            this.controlImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.controlImageList, "controlImageList");
            this.controlImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imagingTools
            // 
            this.imagingTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.imagingTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureStartButton,
            this.captureStopButton,
            this.captureSnapshotButton,
            toolStripSeparator1,
            this.collectionToggleButton});
            resources.ApplyResources(this.imagingTools, "imagingTools");
            this.imagingTools.Name = "imagingTools";
            // 
            // captureStartButton
            // 
            this.captureStartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.captureStartButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapPlay_HighColor;
            resources.ApplyResources(this.captureStartButton, "captureStartButton");
            this.captureStartButton.Name = "captureStartButton";
            this.captureStartButton.Click += new System.EventHandler(this.captureStartButton_Click);
            // 
            // captureStopButton
            // 
            this.captureStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.captureStopButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapStop_HighColor;
            resources.ApplyResources(this.captureStopButton, "captureStopButton");
            this.captureStopButton.Name = "captureStopButton";
            this.captureStopButton.Click += new System.EventHandler(this.captureStopButton_Click);
            // 
            // captureSnapshotButton
            // 
            this.captureSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.captureSnapshotButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapCamera_HighColor;
            resources.ApplyResources(this.captureSnapshotButton, "captureSnapshotButton");
            this.captureSnapshotButton.Name = "captureSnapshotButton";
            this.captureSnapshotButton.Click += new System.EventHandler(this.captureSnapshotButton_Click);
            // 
            // collectionToggleButton
            // 
            this.collectionToggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collectionToggleButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapPictures_HighColor;
            resources.ApplyResources(this.collectionToggleButton, "collectionToggleButton");
            this.collectionToggleButton.Name = "collectionToggleButton";
            this.collectionToggleButton.Click += new System.EventHandler(this.collectionToggleButton_Click);
            // 
            // imagingMenu
            // 
            this.imagingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoToolStripMenuItem,
            this.imagesToolStripMenuItem});
            resources.ApplyResources(this.imagingMenu, "imagingMenu");
            this.imagingMenu.Name = "imagingMenu";
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.capturedevicesToolStripMenuItem,
            this.resolutionToolStripMenuItem});
            this.videoToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.videoToolStripMenuItem.MergeIndex = 2;
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            resources.ApplyResources(this.videoToolStripMenuItem, "videoToolStripMenuItem");
            // 
            // imagesToolStripMenuItem
            // 
            this.imagesToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.imagesToolStripMenuItem.MergeIndex = 3;
            this.imagesToolStripMenuItem.Name = "imagesToolStripMenuItem";
            resources.ApplyResources(this.imagesToolStripMenuItem, "imagesToolStripMenuItem");
            // 
            // thumbnailUpdateWorker
            // 
            this.thumbnailUpdateWorker.WorkerReportsProgress = true;
            this.thumbnailUpdateWorker.WorkerSupportsCancellation = true;
            this.thumbnailUpdateWorker.BeforeRunWorker += new Gyrosoft.Medea.Imaging.ThumbnailUpdateWorker.BeforeRunWorkerEvent(this.thumbnailUpdateWorker_BeforeRunWorker);
            this.thumbnailUpdateWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.thumbnailUpdateWorker_RunWorkerCompleted);
            this.thumbnailUpdateWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.thumbnailUpdateWorker_ProgressChanged);
            // 
            // capturedevicesToolStripMenuItem
            // 
            this.capturedevicesToolStripMenuItem.Name = "capturedevicesToolStripMenuItem";
            resources.ApplyResources(this.capturedevicesToolStripMenuItem, "capturedevicesToolStripMenuItem");
            // 
            // resolutionToolStripMenuItem
            // 
            this.resolutionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x240ToolStripMenuItem,
            this.x480ToolStripMenuItem,
            this.x576ToolStripMenuItem,
            this.x768ToolStripMenuItem,
            this.toolStripSeparator2,
            this.interlacedToolStripMenuItem});
            this.resolutionToolStripMenuItem.Name = "resolutionToolStripMenuItem";
            resources.ApplyResources(this.resolutionToolStripMenuItem, "resolutionToolStripMenuItem");
            // 
            // x240ToolStripMenuItem
            // 
            this.x240ToolStripMenuItem.Name = "x240ToolStripMenuItem";
            resources.ApplyResources(this.x240ToolStripMenuItem, "x240ToolStripMenuItem");
            // 
            // x480ToolStripMenuItem
            // 
            this.x480ToolStripMenuItem.Name = "x480ToolStripMenuItem";
            resources.ApplyResources(this.x480ToolStripMenuItem, "x480ToolStripMenuItem");
            // 
            // x576ToolStripMenuItem
            // 
            this.x576ToolStripMenuItem.Name = "x576ToolStripMenuItem";
            resources.ApplyResources(this.x576ToolStripMenuItem, "x576ToolStripMenuItem");
            // 
            // x768ToolStripMenuItem
            // 
            this.x768ToolStripMenuItem.Name = "x768ToolStripMenuItem";
            resources.ApplyResources(this.x768ToolStripMenuItem, "x768ToolStripMenuItem");
            // 
            // interlacedToolStripMenuItem
            // 
            this.interlacedToolStripMenuItem.Name = "interlacedToolStripMenuItem";
            resources.ApplyResources(this.interlacedToolStripMenuItem, "interlacedToolStripMenuItem");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // ImagingView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imagingMenu);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.imagingTools);
            this.Name = "ImagingView";
            this.Load += new System.EventHandler(this.ImagingControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageHost)).EndInit();
            this.imagingTools.ResumeLayout(false);
            this.imagingTools.PerformLayout();
            this.imagingMenu.ResumeLayout(false);
            this.imagingMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripButton captureStartButton;
        private System.Windows.Forms.ToolStripButton captureStopButton;
        private System.Windows.Forms.ToolStripButton captureSnapshotButton;
        private System.Windows.Forms.ToolStripButton collectionToggleButton;
        private System.Windows.Forms.Panel imagePanel;
        private ImageCollectionListView collectionListView;
        private Gyrosoft.Medea.Imaging.ThumbnailUpdateWorker thumbnailUpdateWorker;
        private System.Windows.Forms.ImageList controlImageList;
        private System.Windows.Forms.ToolStrip imagingTools;
        private System.Windows.Forms.MenuStrip imagingMenu;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagesToolStripMenuItem;
        private System.Windows.Forms.PictureBox imageHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem capturedevicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x240ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x480ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x576ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x768ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem interlacedToolStripMenuItem;
    }
}
