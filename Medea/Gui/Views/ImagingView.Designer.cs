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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.captureBox = new Gyrosoft.WinForms.DxCaptureBox();
            this.imageStatusLabel = new System.Windows.Forms.Label();
            this.imageMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageViewCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.imageSaveAsCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.imageDeleteCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlImageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.imageCmt = new System.Windows.Forms.TextBox();
            this.imagingTools = new System.Windows.Forms.ToolStrip();
            this.captureStartButton = new System.Windows.Forms.ToolStripButton();
            this.captureStopButton = new System.Windows.Forms.ToolStripButton();
            this.captureSnapshotButton = new System.Windows.Forms.ToolStripButton();
            this.previousImageBtn = new System.Windows.Forms.ToolStripButton();
            this.nextImageBtn = new System.Windows.Forms.ToolStripButton();
            this.collectionToggleButton = new System.Windows.Forms.ToolStripButton();
            this.snapshotCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.imagingMenu = new System.Windows.Forms.MenuStrip();
            this.videoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.videoDevicesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.videoResolutionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.videoResolution320x240MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoResolution640x480MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoResolution720x576MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoResolution1024x768MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.videoResolutionInterlacedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoColorDepthMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.videoColorDepth8bitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoColorDepth16bitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoColorDepth24bitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoColorDepth32bitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousImageCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.nextImageCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSelectedImageAsCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedImagesCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.collectionRefreshCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.collectionToggleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.collectionListView = new Gyrosoft.Medea.Gui.ImageCollectionListView();
            this.thumbnailUpdateWorker = new Gyrosoft.Medea.Imaging.ThumbnailUpdateWorker();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.imageMenu.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.captureBox);
            this.splitContainer.Panel1.Controls.Add(this.imageStatusLabel);
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Resize += new System.EventHandler(this.splitContainer_Panel1_Resize);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.collectionListView);
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
            // 
            // captureBox
            // 
            this.captureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.captureBox.InactiveBorderColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.captureBox, "captureBox");
            this.captureBox.Name = "captureBox";
            this.captureBox.PauseBorderColor = System.Drawing.Color.Yellow;
            this.captureBox.PlayBorderColor = System.Drawing.Color.Red;
            this.captureBox.StillBorderColor = System.Drawing.Color.Green;
            this.captureBox.PlayStateChange += new Gyrosoft.WinForms.PlayStateChangeEvent(this.captureBox_PlayStateChange);
            // 
            // imageStatusLabel
            // 
            this.imageStatusLabel.AutoEllipsis = true;
            resources.ApplyResources(this.imageStatusLabel, "imageStatusLabel");
            this.imageStatusLabel.Name = "imageStatusLabel";
            // 
            // imageMenu
            // 
            this.imageMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageViewCmd,
            this.toolStripSeparator4,
            this.imageSaveAsCmd,
            this.imageDeleteCmd,
            this.refreshToolStripMenuItem});
            this.imageMenu.Name = "imageMenu";
            resources.ApplyResources(this.imageMenu, "imageMenu");
            // 
            // imageViewCmd
            // 
            resources.ApplyResources(this.imageViewCmd, "imageViewCmd");
            this.imageViewCmd.Name = "imageViewCmd";
            this.imageViewCmd.Click += new System.EventHandler(this.imageViewCmd_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // imageSaveAsCmd
            // 
            this.imageSaveAsCmd.Name = "imageSaveAsCmd";
            resources.ApplyResources(this.imageSaveAsCmd, "imageSaveAsCmd");
            this.imageSaveAsCmd.Click += new System.EventHandler(this.imageSaveAsCmd_Click);
            // 
            // imageDeleteCmd
            // 
            this.imageDeleteCmd.Name = "imageDeleteCmd";
            resources.ApplyResources(this.imageDeleteCmd, "imageDeleteCmd");
            this.imageDeleteCmd.Click += new System.EventHandler(this.imageDeleteCmd_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapRefresh_HighColor;
            resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.collectionRefreshCmd_Click);
            // 
            // controlImageList
            // 
            this.controlImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.controlImageList, "controlImageList");
            this.controlImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.imageCmt);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // imageCmt
            // 
            resources.ApplyResources(this.imageCmt, "imageCmt");
            this.imageCmt.Name = "imageCmt";
            this.imageCmt.Enter += new System.EventHandler(this.imageCmt_Enter);
            this.imageCmt.Leave += new System.EventHandler(this.imageCmt_Leave);
            this.imageCmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.imageCmt_KeyPress);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(toolStripSeparator7, "toolStripSeparator7");
            // 
            // imagingTools
            // 
            this.imagingTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.imagingTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureStartButton,
            this.captureStopButton,
            this.captureSnapshotButton,
            toolStripSeparator1,
            this.previousImageBtn,
            this.nextImageBtn,
            toolStripSeparator7,
            this.collectionToggleButton,
            this.snapshotCountLabel});
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
            resources.ApplyResources(this.captureStopButton, "captureStopButton");
            this.captureStopButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapStop_HighColor;
            this.captureStopButton.Name = "captureStopButton";
            this.captureStopButton.Click += new System.EventHandler(this.captureStopButton_Click);
            // 
            // captureSnapshotButton
            // 
            this.captureSnapshotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.captureSnapshotButton, "captureSnapshotButton");
            this.captureSnapshotButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapCamera_HighColor;
            this.captureSnapshotButton.Name = "captureSnapshotButton";
            this.captureSnapshotButton.Click += new System.EventHandler(this.captureSnapshotButton_Click);
            // 
            // previousImageBtn
            // 
            this.previousImageBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previousImageBtn.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapGoToPrevious_HighColor;
            resources.ApplyResources(this.previousImageBtn, "previousImageBtn");
            this.previousImageBtn.Name = "previousImageBtn";
            this.previousImageBtn.Click += new System.EventHandler(this.previousImageCmd_Click);
            // 
            // nextImageBtn
            // 
            this.nextImageBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextImageBtn.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapGoToNext_HighColor;
            resources.ApplyResources(this.nextImageBtn, "nextImageBtn");
            this.nextImageBtn.Name = "nextImageBtn";
            this.nextImageBtn.Click += new System.EventHandler(this.nextImageCmd_Click);
            // 
            // collectionToggleButton
            // 
            this.collectionToggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.collectionToggleButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapPictures_HighColor;
            resources.ApplyResources(this.collectionToggleButton, "collectionToggleButton");
            this.collectionToggleButton.Name = "collectionToggleButton";
            this.collectionToggleButton.Click += new System.EventHandler(this.collectionToggleButton_Click);
            // 
            // snapshotCountLabel
            // 
            this.snapshotCountLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.snapshotCountLabel.BackColor = System.Drawing.SystemColors.Control;
            this.snapshotCountLabel.Name = "snapshotCountLabel";
            resources.ApplyResources(this.snapshotCountLabel, "snapshotCountLabel");
            // 
            // imagingMenu
            // 
            this.imagingMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoMenu,
            this.imagesToolStripMenuItem});
            resources.ApplyResources(this.imagingMenu, "imagingMenu");
            this.imagingMenu.Name = "imagingMenu";
            // 
            // videoMenu
            // 
            this.videoMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoDevicesMenu,
            this.videoResolutionMenu,
            this.videoColorDepthMenu,
            toolStripSeparator3,
            this.playToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.snapshotToolStripMenuItem});
            this.videoMenu.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.videoMenu.MergeIndex = 2;
            this.videoMenu.Name = "videoMenu";
            resources.ApplyResources(this.videoMenu, "videoMenu");
            // 
            // videoDevicesMenu
            // 
            this.videoDevicesMenu.Name = "videoDevicesMenu";
            resources.ApplyResources(this.videoDevicesMenu, "videoDevicesMenu");
            this.videoDevicesMenu.DropDownOpening += new System.EventHandler(this.videoDevicesMenu_DropDownOpening);
            // 
            // videoResolutionMenu
            // 
            this.videoResolutionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoResolution320x240MenuItem,
            this.videoResolution640x480MenuItem,
            this.videoResolution720x576MenuItem,
            this.videoResolution1024x768MenuItem,
            this.toolStripSeparator2,
            this.videoResolutionInterlacedMenuItem});
            this.videoResolutionMenu.Name = "videoResolutionMenu";
            resources.ApplyResources(this.videoResolutionMenu, "videoResolutionMenu");
            this.videoResolutionMenu.DropDownOpening += new System.EventHandler(this.videoResolutionMenu_DropDownOpening);
            // 
            // videoResolution320x240MenuItem
            // 
            this.videoResolution320x240MenuItem.Name = "videoResolution320x240MenuItem";
            resources.ApplyResources(this.videoResolution320x240MenuItem, "videoResolution320x240MenuItem");
            this.videoResolution320x240MenuItem.Click += new System.EventHandler(this.videoResolutionMenuItem_Click);
            // 
            // videoResolution640x480MenuItem
            // 
            this.videoResolution640x480MenuItem.Name = "videoResolution640x480MenuItem";
            resources.ApplyResources(this.videoResolution640x480MenuItem, "videoResolution640x480MenuItem");
            this.videoResolution640x480MenuItem.Click += new System.EventHandler(this.videoResolutionMenuItem_Click);
            // 
            // videoResolution720x576MenuItem
            // 
            this.videoResolution720x576MenuItem.Name = "videoResolution720x576MenuItem";
            resources.ApplyResources(this.videoResolution720x576MenuItem, "videoResolution720x576MenuItem");
            this.videoResolution720x576MenuItem.Click += new System.EventHandler(this.videoResolutionMenuItem_Click);
            // 
            // videoResolution1024x768MenuItem
            // 
            this.videoResolution1024x768MenuItem.Name = "videoResolution1024x768MenuItem";
            resources.ApplyResources(this.videoResolution1024x768MenuItem, "videoResolution1024x768MenuItem");
            this.videoResolution1024x768MenuItem.Click += new System.EventHandler(this.videoResolutionMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // videoResolutionInterlacedMenuItem
            // 
            this.videoResolutionInterlacedMenuItem.Name = "videoResolutionInterlacedMenuItem";
            resources.ApplyResources(this.videoResolutionInterlacedMenuItem, "videoResolutionInterlacedMenuItem");
            this.videoResolutionInterlacedMenuItem.Click += new System.EventHandler(this.videoResolutionMenuItem_Click);
            // 
            // videoColorDepthMenu
            // 
            this.videoColorDepthMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoColorDepth8bitMenuItem,
            this.videoColorDepth16bitMenuItem,
            this.videoColorDepth24bitMenuItem,
            this.videoColorDepth32bitMenuItem});
            this.videoColorDepthMenu.Name = "videoColorDepthMenu";
            resources.ApplyResources(this.videoColorDepthMenu, "videoColorDepthMenu");
            this.videoColorDepthMenu.DropDownOpening += new System.EventHandler(this.videoColorDepthMenu_DropDownOpening);
            // 
            // videoColorDepth8bitMenuItem
            // 
            this.videoColorDepth8bitMenuItem.Name = "videoColorDepth8bitMenuItem";
            resources.ApplyResources(this.videoColorDepth8bitMenuItem, "videoColorDepth8bitMenuItem");
            this.videoColorDepth8bitMenuItem.Click += new System.EventHandler(this.videoColorDepthItem_Click);
            // 
            // videoColorDepth16bitMenuItem
            // 
            this.videoColorDepth16bitMenuItem.Name = "videoColorDepth16bitMenuItem";
            resources.ApplyResources(this.videoColorDepth16bitMenuItem, "videoColorDepth16bitMenuItem");
            this.videoColorDepth16bitMenuItem.Click += new System.EventHandler(this.videoColorDepthItem_Click);
            // 
            // videoColorDepth24bitMenuItem
            // 
            this.videoColorDepth24bitMenuItem.Name = "videoColorDepth24bitMenuItem";
            resources.ApplyResources(this.videoColorDepth24bitMenuItem, "videoColorDepth24bitMenuItem");
            this.videoColorDepth24bitMenuItem.Click += new System.EventHandler(this.videoColorDepthItem_Click);
            // 
            // videoColorDepth32bitMenuItem
            // 
            this.videoColorDepth32bitMenuItem.Name = "videoColorDepth32bitMenuItem";
            resources.ApplyResources(this.videoColorDepth32bitMenuItem, "videoColorDepth32bitMenuItem");
            this.videoColorDepth32bitMenuItem.Click += new System.EventHandler(this.videoColorDepthItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapPlay_HighColor;
            resources.ApplyResources(this.playToolStripMenuItem, "playToolStripMenuItem");
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.captureStartButton_Click);
            // 
            // stopToolStripMenuItem
            // 
            resources.ApplyResources(this.stopToolStripMenuItem, "stopToolStripMenuItem");
            this.stopToolStripMenuItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapStop_HighColor;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.captureStopButton_Click);
            // 
            // snapshotToolStripMenuItem
            // 
            resources.ApplyResources(this.snapshotToolStripMenuItem, "snapshotToolStripMenuItem");
            this.snapshotToolStripMenuItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapCamera_HighColor;
            this.snapshotToolStripMenuItem.Name = "snapshotToolStripMenuItem";
            this.snapshotToolStripMenuItem.Click += new System.EventHandler(this.captureSnapshotButton_Click);
            // 
            // imagesToolStripMenuItem
            // 
            this.imagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previousImageCmd,
            this.nextImageCmd,
            this.toolStripSeparator6,
            this.saveSelectedImageAsCmd,
            this.deleteSelectedImagesCmd,
            this.collectionRefreshCmd,
            toolStripSeparator5,
            this.collectionToggleItem});
            this.imagesToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.imagesToolStripMenuItem.MergeIndex = 3;
            this.imagesToolStripMenuItem.Name = "imagesToolStripMenuItem";
            resources.ApplyResources(this.imagesToolStripMenuItem, "imagesToolStripMenuItem");
            // 
            // previousImageCmd
            // 
            this.previousImageCmd.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapGoToPrevious_HighColor;
            resources.ApplyResources(this.previousImageCmd, "previousImageCmd");
            this.previousImageCmd.Name = "previousImageCmd";
            this.previousImageCmd.Click += new System.EventHandler(this.previousImageCmd_Click);
            // 
            // nextImageCmd
            // 
            this.nextImageCmd.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapGoToNext_HighColor;
            resources.ApplyResources(this.nextImageCmd, "nextImageCmd");
            this.nextImageCmd.Name = "nextImageCmd";
            this.nextImageCmd.Click += new System.EventHandler(this.nextImageCmd_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // saveSelectedImageAsCmd
            // 
            this.saveSelectedImageAsCmd.Name = "saveSelectedImageAsCmd";
            resources.ApplyResources(this.saveSelectedImageAsCmd, "saveSelectedImageAsCmd");
            this.saveSelectedImageAsCmd.Click += new System.EventHandler(this.imageSaveAsCmd_Click);
            // 
            // deleteSelectedImagesCmd
            // 
            this.deleteSelectedImagesCmd.Name = "deleteSelectedImagesCmd";
            resources.ApplyResources(this.deleteSelectedImagesCmd, "deleteSelectedImagesCmd");
            this.deleteSelectedImagesCmd.Click += new System.EventHandler(this.imageDeleteCmd_Click);
            // 
            // collectionRefreshCmd
            // 
            this.collectionRefreshCmd.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapRefresh_HighColor;
            this.collectionRefreshCmd.Name = "collectionRefreshCmd";
            resources.ApplyResources(this.collectionRefreshCmd, "collectionRefreshCmd");
            this.collectionRefreshCmd.Click += new System.EventHandler(this.collectionRefreshCmd_Click);
            // 
            // collectionToggleItem
            // 
            this.collectionToggleItem.Name = "collectionToggleItem";
            resources.ApplyResources(this.collectionToggleItem, "collectionToggleItem");
            this.collectionToggleItem.Click += new System.EventHandler(this.collectionToggleButton_Click);
            // 
            // imageSaveDialog
            // 
            this.imageSaveDialog.DefaultExt = "jpg";
            resources.ApplyResources(this.imageSaveDialog, "imageSaveDialog");
            // 
            // collectionListView
            // 
            this.collectionListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.collectionListView.ContextMenuStrip = this.imageMenu;
            resources.ApplyResources(this.collectionListView, "collectionListView");
            this.collectionListView.LargeImageList = this.controlImageList;
            this.collectionListView.Name = "collectionListView";
            this.collectionListView.OwnerDraw = true;
            this.collectionListView.UseCompatibleStateImageBehavior = false;
            this.collectionListView.DoubleClick += new System.EventHandler(this.collectionListView_DoubleClick);
            this.collectionListView.SelectedIndexChanged += new System.EventHandler(this.collectionListView_SelectedIndexChanged);
            this.collectionListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.collectionListView_KeyUp);
            // 
            // thumbnailUpdateWorker
            // 
            this.thumbnailUpdateWorker.WorkerReportsProgress = true;
            this.thumbnailUpdateWorker.WorkerSupportsCancellation = true;
            this.thumbnailUpdateWorker.BeforeRunWorker += new Gyrosoft.Medea.Imaging.ThumbnailUpdateWorker.BeforeRunWorkerEvent(this.thumbnailUpdateWorker_BeforeRunWorker);
            this.thumbnailUpdateWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.thumbnailUpdateWorker_RunWorkerCompleted);
            this.thumbnailUpdateWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.thumbnailUpdateWorker_ProgressChanged);
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
            this.imageMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private ImageCollectionListView collectionListView;
        private Gyrosoft.Medea.Imaging.ThumbnailUpdateWorker thumbnailUpdateWorker;
        private System.Windows.Forms.ImageList controlImageList;
        private System.Windows.Forms.ToolStrip imagingTools;
        private System.Windows.Forms.MenuStrip imagingMenu;
        private System.Windows.Forms.ToolStripMenuItem videoMenu;
        private System.Windows.Forms.ToolStripMenuItem imagesToolStripMenuItem;
        private System.Windows.Forms.Label imageStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem videoDevicesMenu;
        private System.Windows.Forms.ToolStripMenuItem videoResolutionMenu;
        private System.Windows.Forms.ToolStripMenuItem videoResolution320x240MenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoResolution640x480MenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoResolution720x576MenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoResolution1024x768MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem videoResolutionInterlacedMenuItem;
        private Gyrosoft.WinForms.DxCaptureBox captureBox;
        private System.Windows.Forms.ToolStripMenuItem videoColorDepthMenu;
        private System.Windows.Forms.ToolStripMenuItem videoColorDepth8bitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoColorDepth16bitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoColorDepth24bitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoColorDepth32bitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapshotToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel snapshotCountLabel;
        private System.Windows.Forms.ToolStripMenuItem collectionToggleItem;
        private System.Windows.Forms.ContextMenuStrip imageMenu;
        private System.Windows.Forms.ToolStripMenuItem imageViewCmd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem imageSaveAsCmd;
        private System.Windows.Forms.ToolStripMenuItem imageDeleteCmd;
        private System.Windows.Forms.SaveFileDialog imageSaveDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox imageCmt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem previousImageCmd;
        private System.Windows.Forms.ToolStripMenuItem nextImageCmd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedImageAsCmd;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedImagesCmd;
        private System.Windows.Forms.ToolStripButton previousImageBtn;
        private System.Windows.Forms.ToolStripButton nextImageBtn;
        private System.Windows.Forms.ToolStripMenuItem collectionRefreshCmd;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}
