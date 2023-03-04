namespace Gyrosoft.Medea.Gui
{
    partial class ExaminationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExaminationView));
            System.Windows.Forms.ToolStripSeparator exmMenuSeparator;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            this.exmTools = new System.Windows.Forms.ToolStrip();
            this.saveCmdButton = new System.Windows.Forms.ToolStripButton();
            this.printCmdButton = new System.Windows.Forms.ToolStripButton();
            this.snapshotsCmdButton = new System.Windows.Forms.ToolStripButton();
            this.exmTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addCategoryCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.addIndicationDefCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.indicationSelectCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.indicationUnselectCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.indicationEditCommentCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.indicationClearCommentCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.renameNodeCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNodeCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTreeCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.treeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exmTreeImages = new System.Windows.Forms.ImageList(this.components);
            this.exmTree = new System.Windows.Forms.TreeView();
            this.kindLabel = new System.Windows.Forms.Label();
            this.exmMenu = new System.Windows.Forms.MenuStrip();
            this.examinationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exmKindEdit = new System.Windows.Forms.TextBox();
            this.exmKindLookupButton = new System.Windows.Forms.Button();
            this.patientEdit = new System.Windows.Forms.TextBox();
            this.patientLookupBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.conclusionEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.indCmt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.physicianEdit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.performDatePicker = new System.Windows.Forms.DateTimePicker();
            this.physicianLookupBtn = new System.Windows.Forms.Button();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            exmMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exmTools.SuspendLayout();
            this.exmTreeMenu.SuspendLayout();
            this.exmMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // exmMenuSeparator
            // 
            exmMenuSeparator.Name = "exmMenuSeparator";
            resources.ApplyResources(exmMenuSeparator, "exmMenuSeparator");
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // exmTools
            // 
            this.exmTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.exmTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCmdButton,
            this.printCmdButton,
            this.snapshotsCmdButton});
            resources.ApplyResources(this.exmTools, "exmTools");
            this.exmTools.Name = "exmTools";
            // 
            // saveCmdButton
            // 
            this.saveCmdButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapSave_HighColor;
            resources.ApplyResources(this.saveCmdButton, "saveCmdButton");
            this.saveCmdButton.Name = "saveCmdButton";
            this.saveCmdButton.Click += new System.EventHandler(this.saveCmd_Click);
            // 
            // printCmdButton
            // 
            this.printCmdButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapPrint_HighColor;
            resources.ApplyResources(this.printCmdButton, "printCmdButton");
            this.printCmdButton.Name = "printCmdButton";
            this.printCmdButton.Click += new System.EventHandler(this.printCmdButton_Click);
            // 
            // snapshotsCmdButton
            // 
            this.snapshotsCmdButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapPictures_HighColor;
            resources.ApplyResources(this.snapshotsCmdButton, "snapshotsCmdButton");
            this.snapshotsCmdButton.Name = "snapshotsCmdButton";
            this.snapshotsCmdButton.Click += new System.EventHandler(this.snapshotsCmd_Click);
            // 
            // exmTreeMenu
            // 
            this.exmTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCategoryCmd,
            this.addIndicationDefCmd,
            toolStripSeparator2,
            this.indicationSelectCmd,
            this.indicationUnselectCmd,
            toolStripSeparator3,
            this.indicationEditCommentCmd,
            this.indicationClearCommentCmd,
            exmMenuSeparator,
            this.renameNodeCmd,
            this.deleteNodeCmd,
            toolStripSeparator1,
            this.refreshTreeCmd});
            this.exmTreeMenu.Name = "exmTreeMenu";
            this.exmTreeMenu.OwnerItem = this.treeToolStripMenuItem;
            resources.ApplyResources(this.exmTreeMenu, "exmTreeMenu");
            this.exmTreeMenu.Opening += new System.ComponentModel.CancelEventHandler(this.exmMenu_Opening);
            // 
            // addCategoryCmd
            // 
            this.addCategoryCmd.Name = "addCategoryCmd";
            resources.ApplyResources(this.addCategoryCmd, "addCategoryCmd");
            this.addCategoryCmd.Click += new System.EventHandler(this.addCategoryCmd_Click);
            // 
            // addIndicationDefCmd
            // 
            this.addIndicationDefCmd.Name = "addIndicationDefCmd";
            resources.ApplyResources(this.addIndicationDefCmd, "addIndicationDefCmd");
            this.addIndicationDefCmd.Click += new System.EventHandler(this.addIndicationDefCmd_Click);
            // 
            // indicationSelectCmd
            // 
            this.indicationSelectCmd.Name = "indicationSelectCmd";
            resources.ApplyResources(this.indicationSelectCmd, "indicationSelectCmd");
            this.indicationSelectCmd.Click += new System.EventHandler(this.indicationSelectCmd_Click);
            // 
            // indicationUnselectCmd
            // 
            this.indicationUnselectCmd.Name = "indicationUnselectCmd";
            resources.ApplyResources(this.indicationUnselectCmd, "indicationUnselectCmd");
            this.indicationUnselectCmd.Click += new System.EventHandler(this.indicationUnselectCmd_Click);
            // 
            // indicationEditCommentCmd
            // 
            this.indicationEditCommentCmd.Name = "indicationEditCommentCmd";
            resources.ApplyResources(this.indicationEditCommentCmd, "indicationEditCommentCmd");
            // 
            // indicationClearCommentCmd
            // 
            this.indicationClearCommentCmd.Name = "indicationClearCommentCmd";
            resources.ApplyResources(this.indicationClearCommentCmd, "indicationClearCommentCmd");
            // 
            // renameNodeCmd
            // 
            this.renameNodeCmd.Name = "renameNodeCmd";
            resources.ApplyResources(this.renameNodeCmd, "renameNodeCmd");
            this.renameNodeCmd.Click += new System.EventHandler(this.renameNodeCmd_Click);
            // 
            // deleteNodeCmd
            // 
            this.deleteNodeCmd.Name = "deleteNodeCmd";
            resources.ApplyResources(this.deleteNodeCmd, "deleteNodeCmd");
            this.deleteNodeCmd.Click += new System.EventHandler(this.deleteNodeCmd_Click);
            // 
            // refreshTreeCmd
            // 
            this.refreshTreeCmd.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapRefresh_HighColor;
            resources.ApplyResources(this.refreshTreeCmd, "refreshTreeCmd");
            this.refreshTreeCmd.Name = "refreshTreeCmd";
            this.refreshTreeCmd.Click += new System.EventHandler(this.refreshTreeCmd_Click);
            // 
            // treeToolStripMenuItem
            // 
            this.treeToolStripMenuItem.DropDown = this.exmTreeMenu;
            this.treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            resources.ApplyResources(this.treeToolStripMenuItem, "treeToolStripMenuItem");
            // 
            // exmTreeImages
            // 
            this.exmTreeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("exmTreeImages.ImageStream")));
            this.exmTreeImages.TransparentColor = System.Drawing.Color.Magenta;
            this.exmTreeImages.Images.SetKeyName(0, "CategoryClosed");
            this.exmTreeImages.Images.SetKeyName(1, "CategoryOpen");
            this.exmTreeImages.Images.SetKeyName(2, "Indication");
            this.exmTreeImages.Images.SetKeyName(3, "IndicationSelected");
            // 
            // exmTree
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.exmTree, 4);
            this.exmTree.ContextMenuStrip = this.exmTreeMenu;
            resources.ApplyResources(this.exmTree, "exmTree");
            this.exmTree.HideSelection = false;
            this.exmTree.ImageList = this.exmTreeImages;
            this.exmTree.ItemHeight = 20;
            this.exmTree.LabelEdit = true;
            this.exmTree.Name = "exmTree";
            this.exmTree.ShowRootLines = false;
            this.exmTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.exmTree_NodeMouseDoubleClick);
            this.exmTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.exmTree_AfterLabelEdit);
            this.exmTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.exmTree_AfterSelect);
            this.exmTree.KeyUp += new System.Windows.Forms.KeyEventHandler(this.exmTree_KeyUp);
            this.exmTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.exmTree_BeforeLabelEdit);
            this.exmTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.exmTree_MouseDown);
            // 
            // kindLabel
            // 
            resources.ApplyResources(this.kindLabel, "kindLabel");
            this.kindLabel.Name = "kindLabel";
            // 
            // exmMenu
            // 
            this.exmMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.examinationToolStripMenuItem});
            resources.ApplyResources(this.exmMenu, "exmMenu");
            this.exmMenu.Name = "exmMenu";
            // 
            // examinationToolStripMenuItem
            // 
            this.examinationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripSeparator5,
            this.treeToolStripMenuItem});
            this.examinationToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.examinationToolStripMenuItem.MergeIndex = 2;
            this.examinationToolStripMenuItem.Name = "examinationToolStripMenuItem";
            resources.ApplyResources(this.examinationToolStripMenuItem, "examinationToolStripMenuItem");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapSave_HighColor;
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapPrint_HighColor;
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printCmdButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // exmKindEdit
            // 
            resources.ApplyResources(this.exmKindEdit, "exmKindEdit");
            this.exmKindEdit.Name = "exmKindEdit";
            this.exmKindEdit.ReadOnly = true;
            // 
            // exmKindLookupButton
            // 
            resources.ApplyResources(this.exmKindLookupButton, "exmKindLookupButton");
            this.exmKindLookupButton.Name = "exmKindLookupButton";
            this.exmKindLookupButton.UseVisualStyleBackColor = true;
            this.exmKindLookupButton.Click += new System.EventHandler(this.exmKindLookupButton_Click);
            // 
            // patientEdit
            // 
            resources.ApplyResources(this.patientEdit, "patientEdit");
            this.patientEdit.Name = "patientEdit";
            this.patientEdit.ReadOnly = true;
            // 
            // patientLookupBtn
            // 
            resources.ApplyResources(this.patientLookupBtn, "patientLookupBtn");
            this.patientLookupBtn.Name = "patientLookupBtn";
            this.patientLookupBtn.UseVisualStyleBackColor = true;
            this.patientLookupBtn.Click += new System.EventHandler(this.patientLookupBtn_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.exmTree, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.conclusionEdit, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.indCmt, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.kindLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.exmKindEdit, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.exmKindLookupButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.patientEdit, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.patientLookupBtn, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.physicianEdit, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.performDatePicker, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.physicianLookupBtn, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // conclusionEdit
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.conclusionEdit, 4);
            resources.ApplyResources(this.conclusionEdit, "conclusionEdit");
            this.conclusionEdit.Name = "conclusionEdit";
            this.conclusionEdit.TextChanged += new System.EventHandler(this.conclusionEdit_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // indCmt
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.indCmt, 4);
            resources.ApplyResources(this.indCmt, "indCmt");
            this.indCmt.Name = "indCmt";
            this.indCmt.Enter += new System.EventHandler(this.indCmt_Enter);
            this.indCmt.Leave += new System.EventHandler(this.indCmt_Leave);
            this.indCmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.indCmt_KeyPress);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // physicianEdit
            // 
            resources.ApplyResources(this.physicianEdit, "physicianEdit");
            this.physicianEdit.Name = "physicianEdit";
            this.physicianEdit.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // performDatePicker
            // 
            resources.ApplyResources(this.performDatePicker, "performDatePicker");
            this.performDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.performDatePicker.Name = "performDatePicker";
            this.performDatePicker.ValueChanged += new System.EventHandler(this.performDatePicker_ValueChanged);
            // 
            // physicianLookupBtn
            // 
            resources.ApplyResources(this.physicianLookupBtn, "physicianLookupBtn");
            this.physicianLookupBtn.Name = "physicianLookupBtn";
            this.physicianLookupBtn.UseVisualStyleBackColor = true;
            this.physicianLookupBtn.Click += new System.EventHandler(this.physicianLookupBtn_Click);
            // 
            // ExaminationView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.exmTools);
            this.Controls.Add(this.exmMenu);
            this.Name = "ExaminationView";
            this.Load += new System.EventHandler(this.ExaminationControl_Load);
            this.exmTools.ResumeLayout(false);
            this.exmTools.PerformLayout();
            this.exmTreeMenu.ResumeLayout(false);
            this.exmMenu.ResumeLayout(false);
            this.exmMenu.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList exmTreeImages;
        private System.Windows.Forms.TreeView exmTree;
        private System.Windows.Forms.Label kindLabel;
        private System.Windows.Forms.ToolStripMenuItem refreshTreeCmd;
        private System.Windows.Forms.ToolStripMenuItem renameNodeCmd;
        private System.Windows.Forms.ToolStripMenuItem deleteNodeCmd;
        private System.Windows.Forms.ToolStripMenuItem addCategoryCmd;
        private System.Windows.Forms.ToolStripMenuItem addIndicationDefCmd;
        private System.Windows.Forms.ToolStripMenuItem indicationSelectCmd;
        private System.Windows.Forms.ToolStripMenuItem indicationUnselectCmd;
        private System.Windows.Forms.ToolStripMenuItem indicationEditCommentCmd;
        private System.Windows.Forms.ToolStripMenuItem indicationClearCommentCmd;
        private System.Windows.Forms.ToolStripButton saveCmdButton;
        private System.Windows.Forms.ToolStripButton snapshotsCmdButton;
        private System.Windows.Forms.ToolStrip exmTools;
        private System.Windows.Forms.ContextMenuStrip exmTreeMenu;
        private System.Windows.Forms.MenuStrip exmMenu;
        private System.Windows.Forms.ToolStripMenuItem examinationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem treeToolStripMenuItem;
        private System.Windows.Forms.TextBox exmKindEdit;
        private System.Windows.Forms.Button exmKindLookupButton;
        private System.Windows.Forms.TextBox patientEdit;
        private System.Windows.Forms.Button patientLookupBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox conclusionEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox indCmt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox physicianEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker performDatePicker;
        private System.Windows.Forms.Button physicianLookupBtn;
        private System.Windows.Forms.ToolStripButton printCmdButton;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;

    }
}
