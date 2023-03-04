namespace Gyrosoft.Medea.Gui
{
    partial class PatientView
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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientView));
            this.patientMenu = new System.Windows.Forms.MenuStrip();
            this.patientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCmdItem = new System.Windows.Forms.ToolStripMenuItem();
            this.examinationsCmdItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientTools = new System.Windows.Forms.ToolStrip();
            this.saveCmdTool = new System.Windows.Forms.ToolStripButton();
            this.examinationsCmdTool = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.familyNameEdit = new System.Windows.Forms.TextBox();
            this.firstNameEdit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.middleNameEdit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.commentEdit = new System.Windows.Forms.TextBox();
            this.diagnosisEdit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.identityNoEdit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.identityIssuerEdit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.phoneEdit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.addressEdit = new System.Windows.Forms.TextBox();
            this.insuranceNoEdit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.sentFromEdit = new System.Windows.Forms.TextBox();
            this.caseHistoryNoEdit = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.genderCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.birthDatePicker = new System.Windows.Forms.DateTimePicker();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.patientMenu.SuspendLayout();
            this.patientTools.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // patientMenu
            // 
            this.patientMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patientToolStripMenuItem});
            resources.ApplyResources(this.patientMenu, "patientMenu");
            this.patientMenu.Name = "patientMenu";
            // 
            // patientToolStripMenuItem
            // 
            this.patientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCmdItem,
            toolStripSeparator1,
            this.examinationsCmdItem});
            this.patientToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.patientToolStripMenuItem.MergeIndex = 2;
            this.patientToolStripMenuItem.Name = "patientToolStripMenuItem";
            resources.ApplyResources(this.patientToolStripMenuItem, "patientToolStripMenuItem");
            // 
            // saveCmdItem
            // 
            this.saveCmdItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapSave_HighColor;
            resources.ApplyResources(this.saveCmdItem, "saveCmdItem");
            this.saveCmdItem.Name = "saveCmdItem";
            this.saveCmdItem.Click += new System.EventHandler(this.saveCmd_Click);
            // 
            // examinationsCmdItem
            // 
            this.examinationsCmdItem.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapExamination_HighColor;
            resources.ApplyResources(this.examinationsCmdItem, "examinationsCmdItem");
            this.examinationsCmdItem.Name = "examinationsCmdItem";
            this.examinationsCmdItem.Click += new System.EventHandler(this.examinationsCmd_Click);
            // 
            // patientTools
            // 
            this.patientTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.patientTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCmdTool,
            this.examinationsCmdTool});
            resources.ApplyResources(this.patientTools, "patientTools");
            this.patientTools.Name = "patientTools";
            // 
            // saveCmdTool
            // 
            this.saveCmdTool.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapSave_HighColor;
            resources.ApplyResources(this.saveCmdTool, "saveCmdTool");
            this.saveCmdTool.Name = "saveCmdTool";
            this.saveCmdTool.Click += new System.EventHandler(this.saveCmd_Click);
            // 
            // examinationsCmdTool
            // 
            this.examinationsCmdTool.Image = global::Gyrosoft.Medea.Properties.Resources.BitmapExamination_HighColor;
            resources.ApplyResources(this.examinationsCmdTool, "examinationsCmdTool");
            this.examinationsCmdTool.Name = "examinationsCmdTool";
            this.examinationsCmdTool.Click += new System.EventHandler(this.examinationsCmd_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // familyNameEdit
            // 
            resources.ApplyResources(this.familyNameEdit, "familyNameEdit");
            this.familyNameEdit.Name = "familyNameEdit";
            // 
            // firstNameEdit
            // 
            resources.ApplyResources(this.firstNameEdit, "firstNameEdit");
            this.firstNameEdit.Name = "firstNameEdit";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // middleNameEdit
            // 
            resources.ApplyResources(this.middleNameEdit, "middleNameEdit");
            this.middleNameEdit.Name = "middleNameEdit";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.commentEdit, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.diagnosisEdit, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.familyNameEdit, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.firstNameEdit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.middleNameEdit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.identityNoEdit, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.identityIssuerEdit, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.phoneEdit, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.addressEdit, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.insuranceNoEdit, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label12, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.sentFromEdit, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.caseHistoryNoEdit, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.genderCombo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.birthDatePicker, 0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // commentEdit
            // 
            resources.ApplyResources(this.commentEdit, "commentEdit");
            this.tableLayoutPanel1.SetColumnSpan(this.commentEdit, 4);
            this.commentEdit.Name = "commentEdit";
            // 
            // diagnosisEdit
            // 
            resources.ApplyResources(this.diagnosisEdit, "diagnosisEdit");
            this.tableLayoutPanel1.SetColumnSpan(this.diagnosisEdit, 4);
            this.diagnosisEdit.Name = "diagnosisEdit";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // identityNoEdit
            // 
            resources.ApplyResources(this.identityNoEdit, "identityNoEdit");
            this.identityNoEdit.Name = "identityNoEdit";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // identityIssuerEdit
            // 
            resources.ApplyResources(this.identityIssuerEdit, "identityIssuerEdit");
            this.identityIssuerEdit.Name = "identityIssuerEdit";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // phoneEdit
            // 
            resources.ApplyResources(this.phoneEdit, "phoneEdit");
            this.phoneEdit.Name = "phoneEdit";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // addressEdit
            // 
            resources.ApplyResources(this.addressEdit, "addressEdit");
            this.tableLayoutPanel1.SetColumnSpan(this.addressEdit, 3);
            this.addressEdit.Name = "addressEdit";
            // 
            // insuranceNoEdit
            // 
            resources.ApplyResources(this.insuranceNoEdit, "insuranceNoEdit");
            this.insuranceNoEdit.Name = "insuranceNoEdit";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // sentFromEdit
            // 
            resources.ApplyResources(this.sentFromEdit, "sentFromEdit");
            this.tableLayoutPanel1.SetColumnSpan(this.sentFromEdit, 3);
            this.sentFromEdit.Name = "sentFromEdit";
            // 
            // caseHistoryNoEdit
            // 
            resources.ApplyResources(this.caseHistoryNoEdit, "caseHistoryNoEdit");
            this.caseHistoryNoEdit.Name = "caseHistoryNoEdit";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // genderCombo
            // 
            resources.ApplyResources(this.genderCombo, "genderCombo");
            this.genderCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderCombo.FormattingEnabled = true;
            this.genderCombo.Items.AddRange(new object[] {
            resources.GetString("genderCombo.Items"),
            resources.GetString("genderCombo.Items1")});
            this.genderCombo.Name = "genderCombo";
            this.genderCombo.SelectedIndexChanged += new System.EventHandler(this.genderCombo_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // birthDatePicker
            // 
            resources.ApplyResources(this.birthDatePicker, "birthDatePicker");
            this.birthDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.birthDatePicker.Name = "birthDatePicker";
            // 
            // PatientView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.patientMenu);
            this.Controls.Add(this.patientTools);
            this.Name = "PatientView";
            this.patientMenu.ResumeLayout(false);
            this.patientMenu.PerformLayout();
            this.patientTools.ResumeLayout(false);
            this.patientTools.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip patientMenu;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem;
        private System.Windows.Forms.ToolStrip patientTools;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox familyNameEdit;
        private System.Windows.Forms.TextBox firstNameEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox middleNameEdit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem saveCmdItem;
        private System.Windows.Forms.ToolStripButton saveCmdTool;
        private System.Windows.Forms.ToolStripMenuItem examinationsCmdItem;
        private System.Windows.Forms.ToolStripButton examinationsCmdTool;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox identityNoEdit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox identityIssuerEdit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox genderCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox phoneEdit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox addressEdit;
        private System.Windows.Forms.TextBox insuranceNoEdit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox diagnosisEdit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox sentFromEdit;
        private System.Windows.Forms.TextBox caseHistoryNoEdit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox commentEdit;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker birthDatePicker;
    }
}
