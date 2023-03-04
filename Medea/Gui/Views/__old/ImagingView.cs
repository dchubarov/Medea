// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ImagingView.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Extensibility;
using Gyrosoft.Medea.Imaging;

namespace Gyrosoft.Medea.Gui
{
    /// <summary>
    /// Provides imaging capabilities such as video capture and image viewing.
    /// </summary>
    public partial class ImagingView : UserControl
    {
        #region Public Types

        public class Factory : IViewFactory
        {
            #region Private Fields

            private ImagingView _control = null;
            private IIdentity _identityCollectionOwner = null;

            #endregion

            #region Construction

            public Factory(IIdentity identityCollectionOwner)
            {
                _identityCollectionOwner = identityCollectionOwner;
            }

            #endregion

            #region IViewFactory Members

            public void CreateControl()
            {
                if (_control == null) {
                    _control = new ImagingView();
                }
            }

            public void DestroyControl()
            {
                if (_control != null) {
                    // reset state
                    _identityCollectionOwner = null;

                    // gather control state
                    if (_control.ImageCollectionOwner != null) {
                        _identityCollectionOwner = Program.Database.EntityStorage.IdentityOf(_control.ImageCollectionOwner);
                    }

                    _control.captureStopButton_Click(null, null);

                    // destroy control
                    _control.Dispose();
                    _control = null;
                }
            }

            public void ActivateControl()
            {
                if (_control != null) {
                    _control.BindToCollection(_identityCollectionOwner);
                }
            }

            public bool CanClose(CloseReason reason)
            {
                return true;
            }

            public Control Control
            {
                get
                {
                    return _control;
                }
            }

            public string Caption
            {
                get
                {
                    return Properties.Resources.ViewImaging;
                }
            }

            public string Detail
            {
                get
                {
                    return String.Empty;
                }
            }

            public ToolStrip ToolStrip
            {
                get
                {
                    return (_control != null) ? _control.imagingTools : null;
                }
            }

            public MenuStrip MenuStrip
            {
                get
                {
                    return (_control != null) ? _control.imagingMenu : null;
                }
            }

            #endregion
        }

        #endregion

        #region Private Types

        enum ViewMode
        {
            None,
            Capture,
            Show
        }

        #endregion

        #region Private Fields

        private Size _imagePanelDefaultSize = new Size(320, 240);
        private ImageCollection _imageCollection = null;
        private Entity _imageCollectionOwner = null;
        private ViewMode _mode = ViewMode.None;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new ImagingControl object.
        /// </summary>
        public ImagingView()
        {
            InitializeComponent();
            _imageCollection = new ImageCollection();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets underlying ImageCollection object.
        /// </summary>
        public ImageCollection ImageCollection
        {
            get
            {
                return _imageCollection;
            }
        }

        /// <summary>
        /// Gets or set image collection visibility.
        /// </summary>
        public bool ImageCollectionVisible
        {
            get
            {
                return (!splitContainer.Panel2Collapsed);
            }

            set
            {
                if (value) {
                    if (splitContainer.Panel2Collapsed) {
                        splitContainer.Panel2Collapsed = false;
                        thumbnailUpdateWorker.RunWorkerAsyncIfNecessary();
                    }

                    collectionToggleButton.ToolTipText = Properties.Resources.ImagingCollectionHide;
                    collectionToggleButton.Checked = true;
                }
                else {
                    if (!splitContainer.Panel2Collapsed) {
                        CancelThumbnailUpdateWorker(true);
                        splitContainer.Panel2Collapsed = true;
                    }

                    collectionToggleButton.ToolTipText = Properties.Resources.ImagingCollectionShow;
                    collectionToggleButton.Checked = false;
                }
            }
        }

        #endregion

        #region Internal Properties

        internal Entity ImageCollectionOwner
        {
            get
            {
                return _imageCollectionOwner;
            }
        }

        #endregion

        #region Internal Methods

        internal void BindToCollection(IIdentity idObj)
        {
            bool invalidObject = true;

            if (idObj != null && !idObj.IsNull) {
                Entity e = Program.Database.EntityStorage.Retrieve(idObj);
                if (e != null) {
                    if (e is Examination) {
                        invalidObject = false;
                        _imageCollectionOwner = e;
                        this.LoadCollection();
                    }
                }
            }

            if (invalidObject) {
                throw new Exception("Invalid image collection.");
            }
        }

        #endregion

        #region Private Properties

        private ViewMode Mode
        {
            get
            {
                return _mode;
            }

            set
            {
                captureStartButton.Enabled = false;
                captureStopButton.Enabled = false;
                captureSnapshotButton.Enabled = false;

                switch (value) {
                    case ViewMode.Capture:
                        captureStopButton.Enabled = true;
                        captureSnapshotButton.Enabled = true;
                        imagePanel.BackColor = Color.Red;
                        break;
                    case ViewMode.Show:
                        captureStartButton.Enabled = true;
                        imagePanel.BackColor = Color.Lime;
                        break;
                    case ViewMode.None:
                        label1.Text = String.Empty;
                        captureStartButton.Enabled = true;
                        imagePanel.BackColor = SystemColors.ControlDark;
                        break;
                }

                _mode = value;
            }
        }

        #endregion

        #region Private Methods

        private void PositionImagePanel()
        {
            this.SuspendLayout();
            try {

                Size sizeOuter = splitContainer.Panel1.ClientSize;
                sizeOuter.Width -= splitContainer.Panel1.Padding.Left + splitContainer.Panel1.Padding.Right;
                sizeOuter.Height -= splitContainer.Panel1.Padding.Top + splitContainer.Panel1.Padding.Bottom;

                Size sizeInner = _imagePanelDefaultSize;
                sizeInner.Width += imagePanel.Padding.Left + imagePanel.Padding.Right;
                sizeInner.Height += imagePanel.Padding.Top + imagePanel.Padding.Bottom + label1.Height;

                sizeInner = Thumbnail.FitSize(sizeInner, sizeOuter, false, true);

                imagePanel.Width = sizeInner.Width;
                imagePanel.Height = sizeInner.Height - label1.Height;
                imagePanel.Left = (splitContainer.Panel1.ClientSize.Width - sizeInner.Width) / 2;
                imagePanel.Top = (splitContainer.Panel1.ClientSize.Height - sizeInner.Height) / 2;

                label1.Left = splitContainer.Panel1.Padding.Left;
                label1.Width = sizeOuter.Width;
                label1.Top = imagePanel.Bottom + 1;

                /*
                Size sizeOuter = splitContainer.Panel1.ClientSize;
                sizeOuter.Width -= splitContainer.Panel1.Padding.Left + splitContainer.Panel1.Padding.Right;
                sizeOuter.Height -= splitContainer.Panel1.Padding.Top + splitContainer.Panel1.Padding.Bottom;

                Size sizeInner = Thumbnail.FitSize(_imagePanelDefaultSize, sizeOuter, false, true);
                imagePanel.Width = sizeInner.Width;
                imagePanel.Height = sizeInner.Height;
                imagePanel.Left = (splitContainer.Panel1.ClientSize.Width - sizeInner.Width) / 2;
                imagePanel.Top = (splitContainer.Panel1.ClientSize.Height - sizeInner.Height) / 2;
                */
            }
            finally {
                this.ResumeLayout();
            }
        }

        private void CancelThumbnailUpdateWorker(bool wait)
        {
            thumbnailUpdateWorker.CancelAsync();
            if (wait) {
                do {
                    Application.DoEvents();
                    //Thread.Sleep(10);
                } while (thumbnailUpdateWorker.IsBusy);
            }
        }

        private void LoadCollection()
        {
            // clear image collection list view
            collectionListView.Items.Clear();
            collectionListView.Refresh();

            // clear image collection
            this.ImageCollection.Clear();

            Program.StatusManager.SetStatusText(Properties.Resources.StatusLoadingData);
            Program.StatusManager.SetWaitCursor();

            Filter f = null;
            if (_imageCollectionOwner is Examination) {
                f = new Filter("_examination", FilterCondition.EqualRef, _imageCollectionOwner);
            }

            ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Snapshot), f);
            while (!cur.Eof) {
                this.AddCollectionItem((Snapshot)cur.Current);
                cur.MoveNext();
            }

            this.LoadCollectionComplete();
        }

        private void LoadCollectionComplete()
        {
            if (this.ImageCollectionVisible) {
                thumbnailUpdateWorker.RunWorkerAsyncIfNecessary();
            }
        }

        private void AddCollectionItem(Snapshot snap)
        {
            // create an ImageCollectionItem for given snapshot
            ImageCollectionItem item = this.ImageCollection.Add(snap);
            
            // create list view item
            ImageCollectionListViewItem listItem = new ImageCollectionListViewItem(item);
            collectionListView.Items.Insert(0, listItem);

            // add a thumbnail for current snapshot to the update queue
            thumbnailUpdateWorker.Enqueue(listItem.Thumbnail, false);
        }

        private void ShowImage(ImageCollectionListViewItem lvi)
        {
            try {
                if (lvi == null) throw new ArgumentNullException("lvi");
                captureStopButton_Click(null, null);

                imageHost.Image = lvi.Thumbnail.Source.GetImage();
                label1.Text = String.Format(
                    Properties.Resources.InfoSnapshot, 
                    imageHost.Image.Width, 
                    imageHost.Image.Height);

                Mode = ViewMode.Show;
            }
            catch (Exception exc) {
                Program.ShowErrorMessage(exc.Message);
            }
        }

        #endregion

        #region ImagingView Event Handlers (Control)

        private void ImagingControl_Load(object sender, EventArgs e)
        {
            this.ImageCollectionVisible = true;
            this.Mode = ViewMode.None;
        }

        private void splitContainer_Panel1_Resize(object sender, EventArgs e)
        {
            PositionImagePanel();
        }

        private void collectionListView_DoubleClick(object sender, EventArgs e)
        {
            if (collectionListView.FocusedItem != null) {
                ShowImage(collectionListView.FocusedItem as ImageCollectionListViewItem);
            }
        }

        #endregion

        #region ImagingView Event Handlers (Tools)

        private void captureStartButton_Click(object sender, EventArgs e)
        {
            try {
                imageHost.Image = null;

                Program.VideoManager.Grabbed += new VideoManager.GrabbedEvent(this.VideoManager_Grabbed); 
                Program.VideoManager.Host = imageHost;
                Program.VideoManager.Start();

                label1.Text = String.Format(Properties.Resources.InfoCapture, Program.VideoManager.ActiveDeviceName, 0, 0);
                Mode = ViewMode.Capture;
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void captureStopButton_Click(object sender, EventArgs e)
        {
            try {
                Program.VideoManager.Stop();
                Program.VideoManager.Host = null;
                Program.VideoManager.Grabbed -= new VideoManager.GrabbedEvent(this.VideoManager_Grabbed);
                imageHost.Focus();
                Mode = ViewMode.None;
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void capturePauseButton_Click(object sender, EventArgs e)
        {
            try {
                Program.VideoManager.Pause();
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void captureSnapshotButton_Click(object sender, EventArgs e)
        {
            Program.VideoManager.Grab();
        }

        private void collectionToggleButton_Click(object sender, EventArgs e)
        {
            this.ImageCollectionVisible = !this.ImageCollectionVisible;
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.VideoManager.Configure();
        }

        #endregion

        #region ImagingView Event Handlers (Thumbnail Update Worker)

        private void thumbnailUpdateWorker_BeforeRunWorker(object sender)
        {
            string tip = String.Format(Properties.Resources.ImagingThumbnailsUpdating, 0);
            Program.StatusManager.ProgressControl(sender, ProgressControlCommand.Init, 0, 100, tip);
        }

        private void thumbnailUpdateWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try {
                if (e.UserState is Thumbnail) {
                    int index = (e.UserState as Thumbnail).ListItem.Index;
                    collectionListView.RedrawItems(index, index, false);
                }

                String tip = String.Format(Properties.Resources.ImagingThumbnailsUpdating, e.ProgressPercentage);
                Program.StatusManager.ProgressControl(sender, ProgressControlCommand.Set, e.ProgressPercentage, tip);
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thumbnailUpdateWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // release progress bar
            Program.StatusManager.ProgressControl(sender, ProgressControlCommand.Release);

            if (e.Error != null) {
                MessageBox.Show(e.Error.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ImagingView Event Handlers (Video Manager)

        private void VideoManager_Grabbed(object sender, GrabbedEventArgs e)
        {
            if (e.Image != null) {
                Snapshot snap = new Snapshot((Examination)_imageCollectionOwner);
                Program.Database.EntityStorage.BeginTrans();
                try {
                    // store snapshot object
                    Program.Database.EntityStorage.Store(snap);

                    // save image in blob storage
                    Stream blobStream = Program.Database.BlobStorage.GetStream(snap, false);
                    e.Image.Save(blobStream, ImageFormat.Jpeg);
                    blobStream.Close();

                    // add snapshot to image collection
                    this.AddCollectionItem(snap);

                    // commit transaction on object storage
                    Program.Database.EntityStorage.CommitTrans();

                    if (this.ImageCollectionVisible) {
                        thumbnailUpdateWorker.RunWorkerAsyncIfNecessary();
                    }
                }
                catch (Exception exc) {
                    MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Program.Database.EntityStorage.RollbackTrans();
                }

                e.Image.Dispose();
            }
        }

        #endregion
    }
}