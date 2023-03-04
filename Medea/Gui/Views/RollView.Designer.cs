namespace Gyrosoft.Medea.Gui
{
    partial class RollView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RollView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rollMenu = new System.Windows.Forms.MenuStrip();
            this.rollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRecordCmdItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editRecordCmdItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rollTools = new System.Windows.Forms.ToolStrip();
            this.addRecordCmdButton = new System.Windows.Forms.ToolStripButton();
            this.editRecordCmdButton = new System.Windows.Forms.ToolStripButton();
            this.rollGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rollMenu.SuspendLayout();
            this.rollTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // rollMenu
            // 
            this.rollMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rollToolStripMenuItem});
            resources.ApplyResources(this.rollMenu, "rollMenu");
            this.rollMenu.Name = "rollMenu";
            // 
            // rollToolStripMenuItem
            // 
            this.rollToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRecordCmdItem,
            this.editRecordCmdItem});
            this.rollToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.rollToolStripMenuItem.MergeIndex = 2;
            this.rollToolStripMenuItem.Name = "rollToolStripMenuItem";
            resources.ApplyResources(this.rollToolStripMenuItem, "rollToolStripMenuItem");
            // 
            // addRecordCmdItem
            // 
            this.addRecordCmdItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapAddRecord_HighColor;
            resources.ApplyResources(this.addRecordCmdItem, "addRecordCmdItem");
            this.addRecordCmdItem.Name = "addRecordCmdItem";
            this.addRecordCmdItem.Click += new System.EventHandler(this.addRecordCmd_Click);
            // 
            // editRecordCmdItem
            // 
            this.editRecordCmdItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapEditRecord_HighColor;
            resources.ApplyResources(this.editRecordCmdItem, "editRecordCmdItem");
            this.editRecordCmdItem.Name = "editRecordCmdItem";
            this.editRecordCmdItem.Click += new System.EventHandler(this.editRecordCmd_Click);
            // 
            // rollTools
            // 
            this.rollTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.rollTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRecordCmdButton,
            this.editRecordCmdButton});
            resources.ApplyResources(this.rollTools, "rollTools");
            this.rollTools.Name = "rollTools";
            // 
            // addRecordCmdButton
            // 
            this.addRecordCmdButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapAddRecord_HighColor;
            resources.ApplyResources(this.addRecordCmdButton, "addRecordCmdButton");
            this.addRecordCmdButton.Name = "addRecordCmdButton";
            this.addRecordCmdButton.Click += new System.EventHandler(this.addRecordCmd_Click);
            // 
            // editRecordCmdButton
            // 
            this.editRecordCmdButton.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapEditRecord_HighColor;
            resources.ApplyResources(this.editRecordCmdButton, "editRecordCmdButton");
            this.editRecordCmdButton.Name = "editRecordCmdButton";
            this.editRecordCmdButton.Click += new System.EventHandler(this.editRecordCmd_Click);
            // 
            // rollGrid
            // 
            this.rollGrid.AllowUserToOrderColumns = true;
            this.rollGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.rollGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rollGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.rollGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.rollGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rollGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            resources.ApplyResources(this.rollGrid, "rollGrid");
            this.rollGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.rollGrid.MultiSelect = false;
            this.rollGrid.Name = "rollGrid";
            this.rollGrid.RowHeadersVisible = false;
            this.rollGrid.RowTemplate.ReadOnly = true;
            this.rollGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.rollGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.rollGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.rollGrid_CellFormatting);
            this.rollGrid.DoubleClick += new System.EventHandler(this.rollGrid_DoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // RollView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rollGrid);
            this.Controls.Add(this.rollTools);
            this.Controls.Add(this.rollMenu);
            this.Name = "RollView";
            this.rollMenu.ResumeLayout(false);
            this.rollMenu.PerformLayout();
            this.rollTools.ResumeLayout(false);
            this.rollTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip rollMenu;
        private System.Windows.Forms.ToolStripMenuItem rollToolStripMenuItem;
        private System.Windows.Forms.ToolStrip rollTools;
        private System.Windows.Forms.DataGridView rollGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.ToolStripButton addRecordCmdButton;
        private System.Windows.Forms.ToolStripButton editRecordCmdButton;
        private System.Windows.Forms.ToolStripMenuItem addRecordCmdItem;
        private System.Windows.Forms.ToolStripMenuItem editRecordCmdItem;
    }
}
