namespace Gyrosoft.Medea.Gui
{
    partial class PrintExaminationDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintExaminationDialog));
            this.okButton = new System.Windows.Forms.Button();
            this.repPrintDlg = new System.Windows.Forms.PrintDialog();
            this.repPrintDoc = new System.Drawing.Printing.PrintDocument();
            this.printBtn = new System.Windows.Forms.Button();
            this.saveAsBtn = new System.Windows.Forms.Button();
            this.repSaveDlg = new System.Windows.Forms.SaveFileDialog();
            this.repText = new Gyrosoft.Medea.Gui.PrintableRichTextBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // repPrintDlg
            // 
            this.repPrintDlg.Document = this.repPrintDoc;
            this.repPrintDlg.UseEXDialog = true;
            // 
            // repPrintDoc
            // 
            this.repPrintDoc.DocumentName = "Examination";
            this.repPrintDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.repPrintDoc_PrintPage);
            this.repPrintDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.repPrintDoc_BeginPrint);
            // 
            // printBtn
            // 
            resources.ApplyResources(this.printBtn, "printBtn");
            this.printBtn.Name = "printBtn";
            this.printBtn.UseVisualStyleBackColor = true;
            this.printBtn.Click += new System.EventHandler(this.printBtn_Click);
            // 
            // saveAsBtn
            // 
            resources.ApplyResources(this.saveAsBtn, "saveAsBtn");
            this.saveAsBtn.Name = "saveAsBtn";
            this.saveAsBtn.UseVisualStyleBackColor = true;
            this.saveAsBtn.Click += new System.EventHandler(this.saveAsBtn_Click);
            // 
            // repSaveDlg
            // 
            this.repSaveDlg.DefaultExt = "rtf";
            resources.ApplyResources(this.repSaveDlg, "repSaveDlg");
            // 
            // repText
            // 
            resources.ApplyResources(this.repText, "repText");
            this.repText.BackColor = System.Drawing.SystemColors.Window;
            this.repText.Name = "repText";
            this.repText.ReadOnly = true;
            // 
            // PrintExaminationDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.okButton;
            this.Controls.Add(this.saveAsBtn);
            this.Controls.Add(this.printBtn);
            this.Controls.Add(this.repText);
            this.Controls.Add(this.okButton);
            this.HelpButton = true;
            this.MinimizeBox = false;
            this.Name = "PrintExaminationDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private PrintableRichTextBox repText;
        private System.Windows.Forms.PrintDialog repPrintDlg;
        private System.Drawing.Printing.PrintDocument repPrintDoc;
        private System.Windows.Forms.Button printBtn;
        private System.Windows.Forms.Button saveAsBtn;
        private System.Windows.Forms.SaveFileDialog repSaveDlg;
    }
}