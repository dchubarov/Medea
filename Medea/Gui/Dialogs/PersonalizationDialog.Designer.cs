namespace Gyrosoft.Medea.Gui
{
    partial class PersonalizationDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonalizationDialog));
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.clinicTab = new System.Windows.Forms.TabPage();
            this.clinicNameSetBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.editBtn = new System.Windows.Forms.Button();
            this.addPhysicianBtn = new System.Windows.Forms.Button();
            this.addDepartmentBtn = new System.Windows.Forms.Button();
            this.clinicTree = new System.Windows.Forms.TreeView();
            this.clinicNameEdit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.personalizationTab = new System.Windows.Forms.TabPage();
            this.extList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.extSetBtn = new System.Windows.Forms.Button();
            this.extEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.clinicTab.SuspendLayout();
            this.personalizationTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.clinicTab);
            this.tabControl1.Controls.Add(this.personalizationTab);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // clinicTab
            // 
            this.clinicTab.Controls.Add(this.clinicNameSetBtn);
            this.clinicTab.Controls.Add(this.deleteBtn);
            this.clinicTab.Controls.Add(this.editBtn);
            this.clinicTab.Controls.Add(this.addPhysicianBtn);
            this.clinicTab.Controls.Add(this.addDepartmentBtn);
            this.clinicTab.Controls.Add(this.clinicTree);
            this.clinicTab.Controls.Add(this.clinicNameEdit);
            this.clinicTab.Controls.Add(this.label1);
            resources.ApplyResources(this.clinicTab, "clinicTab");
            this.clinicTab.Name = "clinicTab";
            this.clinicTab.UseVisualStyleBackColor = true;
            // 
            // clinicNameSetBtn
            // 
            resources.ApplyResources(this.clinicNameSetBtn, "clinicNameSetBtn");
            this.clinicNameSetBtn.Name = "clinicNameSetBtn";
            this.clinicNameSetBtn.UseVisualStyleBackColor = true;
            this.clinicNameSetBtn.Click += new System.EventHandler(this.clinicNameSetBtn_Click);
            // 
            // deleteBtn
            // 
            resources.ApplyResources(this.deleteBtn, "deleteBtn");
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // editBtn
            // 
            resources.ApplyResources(this.editBtn, "editBtn");
            this.editBtn.Name = "editBtn";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // addPhysicianBtn
            // 
            resources.ApplyResources(this.addPhysicianBtn, "addPhysicianBtn");
            this.addPhysicianBtn.Name = "addPhysicianBtn";
            this.addPhysicianBtn.UseVisualStyleBackColor = true;
            this.addPhysicianBtn.Click += new System.EventHandler(this.addPhysicianBtn_Click);
            // 
            // addDepartmentBtn
            // 
            resources.ApplyResources(this.addDepartmentBtn, "addDepartmentBtn");
            this.addDepartmentBtn.Name = "addDepartmentBtn";
            this.addDepartmentBtn.UseVisualStyleBackColor = true;
            this.addDepartmentBtn.Click += new System.EventHandler(this.addDepartmentBtn_Click);
            // 
            // clinicTree
            // 
            this.clinicTree.HideSelection = false;
            resources.ApplyResources(this.clinicTree, "clinicTree");
            this.clinicTree.Name = "clinicTree";
            this.clinicTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.clinicTree_AfterSelect);
            // 
            // clinicNameEdit
            // 
            resources.ApplyResources(this.clinicNameEdit, "clinicNameEdit");
            this.clinicNameEdit.Name = "clinicNameEdit";
            this.clinicNameEdit.Enter += new System.EventHandler(this.clinicNameEdit_Enter);
            this.clinicNameEdit.Leave += new System.EventHandler(this.clinicNameEdit_Leave);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // personalizationTab
            // 
            this.personalizationTab.Controls.Add(this.extList);
            this.personalizationTab.Controls.Add(this.label3);
            this.personalizationTab.Controls.Add(this.extSetBtn);
            this.personalizationTab.Controls.Add(this.extEdit);
            this.personalizationTab.Controls.Add(this.label2);
            resources.ApplyResources(this.personalizationTab, "personalizationTab");
            this.personalizationTab.Name = "personalizationTab";
            this.personalizationTab.UseVisualStyleBackColor = true;
            // 
            // extList
            // 
            this.extList.FormattingEnabled = true;
            resources.ApplyResources(this.extList, "extList");
            this.extList.Name = "extList";
            this.extList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.extList_MouseDoubleClick);
            this.extList.SelectedIndexChanged += new System.EventHandler(this.extList_SelectedIndexChanged);
            this.extList.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.extList_Format);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // extSetBtn
            // 
            resources.ApplyResources(this.extSetBtn, "extSetBtn");
            this.extSetBtn.Name = "extSetBtn";
            this.extSetBtn.UseVisualStyleBackColor = true;
            this.extSetBtn.Click += new System.EventHandler(this.extSetBtn_Click);
            // 
            // extEdit
            // 
            resources.ApplyResources(this.extEdit, "extEdit");
            this.extEdit.Name = "extEdit";
            this.extEdit.Enter += new System.EventHandler(this.extEdit_Enter);
            this.extEdit.Leave += new System.EventHandler(this.extEdit_Leave);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // PersonalizationDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonalizationDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.PersonalizationDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.clinicTab.ResumeLayout(false);
            this.clinicTab.PerformLayout();
            this.personalizationTab.ResumeLayout(false);
            this.personalizationTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage clinicTab;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button addPhysicianBtn;
        private System.Windows.Forms.Button addDepartmentBtn;
        private System.Windows.Forms.TreeView clinicTree;
        private System.Windows.Forms.TextBox clinicNameEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage personalizationTab;
        private System.Windows.Forms.ListBox extList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button extSetBtn;
        private System.Windows.Forms.TextBox extEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button clinicNameSetBtn;
    }
}