namespace Gyrosoft.Medea.Gui
{
    partial class TaskView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskView));
            this.taskList = new System.Windows.Forms.ListView();
            this.taskImages = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // taskList
            // 
            this.taskList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.taskList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.taskList, "taskList");
            this.taskList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("taskList.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("taskList.Groups1")))});
            this.taskList.HotTracking = true;
            this.taskList.HoverSelection = true;
            this.taskList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("taskList.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("taskList.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("taskList.Items2"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("taskList.Items3")))});
            this.taskList.LargeImageList = this.taskImages;
            this.taskList.MultiSelect = false;
            this.taskList.Name = "taskList";
            this.taskList.ShowGroups = false;
            this.taskList.UseCompatibleStateImageBehavior = false;
            this.taskList.View = System.Windows.Forms.View.Tile;
            this.taskList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.taskList_KeyPress);
            this.taskList.Click += new System.EventHandler(this.taskList_Click);
            // 
            // taskImages
            // 
            this.taskImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("taskImages.ImageStream")));
            this.taskImages.TransparentColor = System.Drawing.Color.Transparent;
            this.taskImages.Images.SetKeyName(0, "");
            this.taskImages.Images.SetKeyName(1, "");
            this.taskImages.Images.SetKeyName(2, "");
            // 
            // TaskView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.taskList);
            this.Name = "TaskView";
            this.Load += new System.EventHandler(this.TaskView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView taskList;
        private System.Windows.Forms.ImageList taskImages;
    }
}
