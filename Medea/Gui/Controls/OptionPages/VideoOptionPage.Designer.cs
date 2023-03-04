namespace Gyrosoft.Medea.Gui.OptionPages
{
    partial class VideoOptionPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoOptionPage));
            this.captureDeviceGroup = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.videoCaptureDeviceLabel = new System.Windows.Forms.Label();
            this.deviceSettingsButton = new System.Windows.Forms.Button();
            this.captureDeviceGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // captureDeviceGroup
            // 
            this.captureDeviceGroup.Controls.Add(this.deviceSettingsButton);
            this.captureDeviceGroup.Controls.Add(this.comboBox1);
            this.captureDeviceGroup.Controls.Add(this.videoCaptureDeviceLabel);
            resources.ApplyResources(this.captureDeviceGroup, "captureDeviceGroup");
            this.captureDeviceGroup.Name = "captureDeviceGroup";
            this.captureDeviceGroup.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            // 
            // videoCaptureDeviceLabel
            // 
            resources.ApplyResources(this.videoCaptureDeviceLabel, "videoCaptureDeviceLabel");
            this.videoCaptureDeviceLabel.Name = "videoCaptureDeviceLabel";
            // 
            // deviceSettingsButton
            // 
            resources.ApplyResources(this.deviceSettingsButton, "deviceSettingsButton");
            this.deviceSettingsButton.Name = "deviceSettingsButton";
            this.deviceSettingsButton.UseVisualStyleBackColor = true;
            // 
            // VideoOptionPage
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.captureDeviceGroup);
            this.Name = "VideoOptionPage";
            this.captureDeviceGroup.ResumeLayout(false);
            this.captureDeviceGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox captureDeviceGroup;
        private System.Windows.Forms.Button deviceSettingsButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label videoCaptureDeviceLabel;


    }
}
