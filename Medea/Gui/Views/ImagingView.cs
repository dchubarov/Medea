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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// gyrosoft.winforms
using Gyrosoft.WinForms;
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

            public bool HandleKeyPress(KeyPressEventArgs e)
            {
                if (e.KeyChar == (char)32) {
                    if (_control != null &&
                        (_control.captureBox.State == DxCaptureBoxState.Playing ||
                        _control.captureBox.State == DxCaptureBoxState.Paused) && !_control.imageCmt.Focused) {
                        _control.captureSnapshotButton_Click(null, null);
                        return true;
                    }
                }

                return false;
            }

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

        private class CollectionSorter : IComparer
        {
            #region IComparer Members

            public int Compare(object x, object y)
            {
                ImageCollectionListViewItem ix = x as ImageCollectionListViewItem;
                ImageCollectionListViewItem iy = y as ImageCollectionListViewItem;
                return -ix.Thumbnail.Source.Snapshot.Date.CompareTo(iy.Thumbnail.Source.Snapshot.Date);
            }

            #endregion
        }

        #endregion

        #region Private Fields

        private Size _imagePanelDefaultSize = new Size(320, 240);
        private ImageCollection _imageCollection = null;
        private Entity _imageCollectionOwner = null;
        private ImageCollectionListViewItem _editCmtItem = null;
        private ImageCollectionListViewItem _displayItem = null;

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
                    collectionToggleItem.Checked = true;
                    Properties.Settings.Default.ImageCollectionVisible = true;
                }
                else {
                    if (!splitContainer.Panel2Collapsed) {
                        CancelThumbnailUpdateWorker(true);
                        splitContainer.Panel2Collapsed = true;
                    }

                    collectionToggleButton.ToolTipText = Properties.Resources.ImagingCollectionShow;
                    collectionToggleButton.Checked = false;
                    collectionToggleItem.Checked = false;
                    Properties.Settings.Default.ImageCollectionVisible = false;
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

        #region Private Methods

        private void PositionImagePanel()
        {
            this.SuspendLayout();
            try {
                Size sizeOuter = splitContainer.Panel1.ClientSize;
                sizeOuter.Width -= splitContainer.Panel1.Padding.Left + splitContainer.Panel1.Padding.Right;
                sizeOuter.Height -= splitContainer.Panel1.Padding.Top + splitContainer.Panel1.Padding.Bottom;

                Size sizeInner = captureBox.PreferredBoxSize;
                if (sizeInner.Width < 100 || sizeInner.Height < 100) {
                    sizeInner = _imagePanelDefaultSize;
                    sizeInner.Width += 4;
                    sizeInner.Height += 4;
                }

                sizeInner = Thumbnail.FitSize(sizeInner, sizeOuter, false, true);

                captureBox.Width = sizeInner.Width;
                captureBox.Height = sizeInner.Height - imageStatusLabel.Height;
                captureBox.Left = (splitContainer.Panel1.ClientSize.Width - sizeInner.Width) / 2;
                captureBox.Top = (splitContainer.Panel1.ClientSize.Height - sizeInner.Height) / 2;

                imageStatusLabel.Left = splitContainer.Panel1.Padding.Left;
                imageStatusLabel.Width = sizeOuter.Width;
                imageStatusLabel.Top = captureBox.Bottom + 1;
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
            this.UpdateSnapshotCount();
        }

        private void UpdateSnapshotCount()
        {
            snapshotCountLabel.Text = String.Format(
                Properties.Resources.ImagingSnapshotCount, 
                collectionListView.Items.Count);
        }

        private void LoadCollectionComplete()
        {
            if (this.ImageCollectionVisible) {
                thumbnailUpdateWorker.RunWorkerAsyncIfNecessary();
            }
        }

        private void UpdateItemText(ImageCollectionListViewItem lvi)
        {
            String tmp = String.Empty;
            Snapshot snap = lvi.Thumbnail.Source.Snapshot;

            if (snap.Date.Year != DateTime.Now.Year || snap.Date.Month != DateTime.Now.Month || snap.Date.Day != DateTime.Now.Day) {
                tmp += snap.Date.ToShortDateString();
                tmp += " ";
            }

            tmp += snap.Date.ToShortTimeString();
            tmp += ", ";

            tmp += snap.ImageWidth.ToString() + "x" + snap.ImageHeight.ToString();

            if (!String.IsNullOrEmpty(snap.Comment)) {
                tmp += " [";
                tmp += snap.Comment;
                tmp += "]";
            }

            lvi.Text = tmp;
        }

        private ImageCollectionListViewItem AddCollectionItem(Snapshot snap)
        {
            // create an ImageCollectionItem for given snapshot
            ImageCollectionItem item = this.ImageCollection.Add(snap);
            
            // create list view item
            ImageCollectionListViewItem listItem = new ImageCollectionListViewItem(item);
            UpdateItemText(listItem);
            collectionListView.Items.Insert(0, listItem);

            // add a thumbnail for current snapshot to the update queue
            thumbnailUpdateWorker.Enqueue(listItem.Thumbnail, false);

            return listItem;
        }

        private void ShowImage(ImageCollectionListViewItem lvi)
        {
            try {
                if (lvi != null) {
                    Image img = lvi.Thumbnail.Source.GetImage();
                    captureBox.ShowStillImage(lvi.Thumbnail.Source.GetImage());
                    imageStatusLabel.Text = lvi.Text;

                    collectionListView.SelectedItems.Clear();
                    lvi.Selected = true;
                    lvi.EnsureVisible();
                }

                _displayItem = lvi;
            }
            catch (Exception exc) {
                Program.ShowErrorMessage(exc.Message);
            }
        }

        private void AddCaptureDevicesToMenu()
        {
            String[] deviceNames = captureBox.GetDeviceNames();
            int iDevice = 0;

            foreach (String deviceName in deviceNames) {
                ToolStripItem item = videoDevicesMenu.DropDown.Items.Add(deviceName);
                item.Tag = iDevice++;
            }
        }

        #endregion

        #region ImagingView Event Handlers (Control)

        private void ImagingControl_Load(object sender, EventArgs e)
        {
            this.ImageCollectionVisible = Properties.Settings.Default.ImageCollectionVisible;
            imageStatusLabel.Text = String.Empty;
            collectionListView.ListViewItemSorter = new CollectionSorter();
            this.AddCaptureDevicesToMenu();

            //splitContainer.SplitterDistance = Properties.Settings.Default.ImagingSplitPosition;
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

        private void collectionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (collectionListView.SelectedItems.Count == 1) {
                imageCmt.Enabled = true;
                imageCmt.Text = (collectionListView.SelectedItems[0] 
                    as ImageCollectionListViewItem).Thumbnail.Source.Snapshot.Comment;
            }
            else {
                imageCmt.Text = String.Empty;
                imageCmt.Enabled = false;
            }
        }

        private void imageCmt_Enter(object sender, EventArgs e)
        {
            if (collectionListView.SelectedItems.Count == 1) {
                _editCmtItem = collectionListView.SelectedItems[0] as ImageCollectionListViewItem;
            }
        }

        private void imageCmt_Leave(object sender, EventArgs e)
        {
            if (_editCmtItem != null) {
                Snapshot snap = _editCmtItem.Thumbnail.Source.Snapshot;
                String tmp = imageCmt.Text.Trim();
                snap.Comment = imageCmt.Text;
                try {
                    Program.Database.EntityStorage.BeginTrans();
                    Program.Database.EntityStorage.Store(snap);
                    Program.Database.EntityStorage.CommitTrans();
                    UpdateItemText(_editCmtItem);

                    if (captureBox.State == DxCaptureBoxState.Still && 
                        Object.ReferenceEquals(_editCmtItem, _displayItem)) {
                        ShowImage(_editCmtItem);
                    }

                    _editCmtItem = null;
                }
                catch (Exception exc) {
                    MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Program.Database.EntityStorage.RollbackTrans();
                }
            }
        }

        private void imageCmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
                collectionListView.Focus();
            }
        }

        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Properties.Settings.Default.ImagingSplitPosition = splitContainer.SplitterDistance;
        }

        private void collectionListView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter) {
                imageViewCmd_Click(null, null);
                e.Handled = true;
            }
            else if (e.Modifiers == Keys.None && e.KeyCode == Keys.Delete) {
                imageDeleteCmd_Click(null, null);
                e.Handled = true;
            }
        }

        #endregion

        #region ImagingView Event Handlers (Tools)

        private void imageViewCmd_Click(object sender, EventArgs e)
        {
            if (collectionListView.FocusedItem != null) {
                ShowImage(collectionListView.FocusedItem as ImageCollectionListViewItem);
            }
        }

        private void imageSaveAsCmd_Click(object sender, EventArgs e)
        {
            try {
                if (collectionListView.FocusedItem != null) {
                    ImageCollectionListViewItem lvi = collectionListView.FocusedItem as ImageCollectionListViewItem;
                    Snapshot snp = lvi.Thumbnail.Source.Snapshot;
                    if (String.IsNullOrEmpty(snp.Comment)) {
                        imageSaveDialog.FileName = Program.Database.EntityStorage.IdentityOf(snp).ToString();
                    }
                    else {
                        imageSaveDialog.FileName = snp.Comment;
                    }

                    if (imageSaveDialog.ShowDialog() == DialogResult.OK) {
                        Stream src = Program.Database.BlobStorage.GetStream(snp, true);
                        FileStream dst = new FileStream(imageSaveDialog.FileName, FileMode.Create);
                        byte[] buf = new byte[src.Length];
                        src.Read(buf, 0, (int)src.Length);
                        dst.Write(buf, 0, (int)src.Length);
                        src.Close();
                        dst.Close();
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void imageDeleteCmd_Click(object sender, EventArgs e)
        {
            try {
                if (collectionListView.SelectedItems.Count > 0) {
                    if (MessageBox.Show(String.Format(Properties.Resources.ConfirmDeleteSnapshots, collectionListView.SelectedItems.Count),
                        Program.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                        Program.StatusManager.SetWaitCursor();
                        Program.StatusManager.SetStatusText(Properties.Resources.StatusDeleting);
                        Program.Database.EntityStorage.BeginTrans();
                        foreach (ImageCollectionListViewItem lvi in collectionListView.SelectedItems) {
                            Snapshot snap = lvi.Thumbnail.Source.Snapshot;
                            if (snap != null) {
                                try {
                                    Program.Database.BlobStorage.DeleteBlob(snap);
                                }
                                catch (Exception) {
                                    // eat
                                }

                                try {
                                    Program.Database.EntityStorage.Delete(snap);
                                }
                                catch (Exception) {
                                    Program.Database.EntityStorage.RollbackTrans();
                                    throw;
                                }

                                if (captureBox.State == DxCaptureBoxState.Still &&
                                    Object.ReferenceEquals(_displayItem, lvi)) {
                                    captureBox.ShowStillImage(null);
                                    _displayItem = null;
                                }

                                collectionListView.Items.Remove(lvi);
                            }
                        }

                        Program.Database.EntityStorage.CommitTrans();
                        UpdateSnapshotCount();
                    }
                }

                return;
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            LoadCollection();
        }

        private void captureStartButton_Click(object sender, EventArgs e)
        {
            try {
                int iDevice = Properties.Settings.Default.VideoDevice;
                captureBox.Play(
                    iDevice, 
                    Properties.Settings.Default.VideoWidth, 
                    Properties.Settings.Default.VideoHeight, 
                    Properties.Settings.Default.VideoColorDepth, 
                    Properties.Settings.Default.VideoInterlace
                    );

                String[] devices = captureBox.GetDeviceNames();
                imageStatusLabel.Text = String.Format(
                    Properties.Resources.InfoCapture,
                    devices[iDevice],
                    captureBox.VideoWidth,
                    captureBox.VideoHeight
                );
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void captureStopButton_Click(object sender, EventArgs e)
        {
            try {
                captureBox.Stop();
                imageStatusLabel.Text = String.Empty;
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void capturePauseButton_Click(object sender, EventArgs e)
        {
            try {
                captureBox.Pause();
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void captureSnapshotButton_Click(object sender, EventArgs e)
        {
            Program.StatusManager.SetWaitCursor();
            Program.StatusManager.SetStatusText(Properties.Resources.StatusSnapshot);
            snapshotCountLabel.BackColor = Color.Red;
            imagingTools.Refresh();

            try {
                Bitmap snap_bitmap = captureBox.Snap();

                if (snap_bitmap != null) {
                    Snapshot snap = new Snapshot((Examination)_imageCollectionOwner, snap_bitmap.Width, snap_bitmap.Height);
                    Program.Database.EntityStorage.BeginTrans();
                    // store snapshot object
                    Program.Database.EntityStorage.Store(snap);

                    // save image in blob storage
                    Stream blobStream = Program.Database.BlobStorage.GetStream(snap, false);
                    snap_bitmap.Save(blobStream, ImageFormat.Jpeg);
                    blobStream.Close();

                    // add snapshot to image collection
                    ImageCollectionListViewItem lvi = this.AddCollectionItem(snap);

                    // commit transaction on object storage
                    Program.Database.EntityStorage.CommitTrans();

                    UpdateSnapshotCount();

                    collectionListView.BeginUpdate();
                    try {
                        collectionListView.Sort();
                        collectionListView.SelectedItems.Clear();
                        if (lvi != null) {
                            lvi.Selected = true;
                            lvi.EnsureVisible();
                        }
                    }
                    finally {
                        collectionListView.EndUpdate();
                    }

                    if (this.ImageCollectionVisible) {
                        thumbnailUpdateWorker.RunWorkerAsyncIfNecessary();
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Program.Database.EntityStorage.RollbackTrans();
            }
            finally {
                snapshotCountLabel.BackColor = SystemColors.Control;
            }
        }

        private void collectionToggleButton_Click(object sender, EventArgs e)
        {
            this.ImageCollectionVisible = !this.ImageCollectionVisible;
        }

        private void videoResolutionMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = sender as ToolStripItem;
            bool interlace = Properties.Settings.Default.VideoInterlace;
            int w = 0, h = 0;

            if (tsi.Text == "&320x240") {
                w = 320; h = 240;
            }
            else if (tsi.Text == "&640x480") {
                w = 640; h = 480;
            }
            else if (tsi.Text == "&720x576") {
                w = 720; h = 576;
            }
            else if (tsi.Text == "&1024x768") {
                w = 1024; h = 768;
            }
            else {
                interlace = !interlace;
            }

            if (h > 0 && w > 0) {
                Properties.Settings.Default.VideoWidth = w;
                Properties.Settings.Default.VideoHeight = h;
            }
            Properties.Settings.Default.VideoInterlace = interlace;

            if (captureBox.State == DxCaptureBoxState.Playing || captureBox.State == DxCaptureBoxState.Paused) {
                captureStopButton_Click(null, null);
                captureStartButton_Click(null, null);
            }
        }

        private void videoColorDepthItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tsi = sender as ToolStripItem;
            short bpp = 0;

            if (tsi.Text.StartsWith("&8"))
                bpp = 8;
            else if (tsi.Text.StartsWith("&16"))
                bpp = 16;
            else if (tsi.Text.StartsWith("&24"))
                bpp = 24;
            else if (tsi.Text.StartsWith("&32"))
                bpp = 32;

            if (bpp > 0)
                Properties.Settings.Default.VideoColorDepth = bpp;

            if (captureBox.State == DxCaptureBoxState.Playing || captureBox.State == DxCaptureBoxState.Paused) {
                captureStopButton_Click(null, null);
                captureStartButton_Click(null, null);
            }
        }

        private void videoDevicesMenu_DropDownOpening(object sender, EventArgs e)
        {
            int iDevice = 0;
            foreach (ToolStripMenuItem tsmi in videoDevicesMenu.DropDown.Items) {
                tsmi.Checked = (iDevice++ == Properties.Settings.Default.VideoDevice);
            }
        }

        private void videoResolutionMenu_DropDownOpening(object sender, EventArgs e)
        {
            videoResolution320x240MenuItem.Checked = (Properties.Settings.Default.VideoWidth == 320);
            videoResolution640x480MenuItem.Checked = (Properties.Settings.Default.VideoWidth == 640);
            videoResolution720x576MenuItem.Checked = (Properties.Settings.Default.VideoWidth == 720);
            videoResolution1024x768MenuItem.Checked = (Properties.Settings.Default.VideoWidth == 1024);
            videoResolutionInterlacedMenuItem.Checked = Properties.Settings.Default.VideoInterlace;
        }

        private void videoColorDepthMenu_DropDownOpening(object sender, EventArgs e)
        {
            videoColorDepth8bitMenuItem.Checked = (Properties.Settings.Default.VideoColorDepth == 8);
            videoColorDepth16bitMenuItem.Checked = (Properties.Settings.Default.VideoColorDepth == 16);
            videoColorDepth24bitMenuItem.Checked = (Properties.Settings.Default.VideoColorDepth == 24);
            videoColorDepth32bitMenuItem.Checked = (Properties.Settings.Default.VideoColorDepth == 32);
        }

        private void previousImageCmd_Click(object sender, EventArgs e)
        {
            if (captureBox.State == DxCaptureBoxState.Still && _displayItem != null) {
                if (_displayItem.Index > 0) {
                    ShowImage(collectionListView.Items[_displayItem.Index - 1] as ImageCollectionListViewItem);
                }
            }
        }

        private void nextImageCmd_Click(object sender, EventArgs e)
        {
            if (captureBox.State == DxCaptureBoxState.Still && _displayItem != null) {
                if (_displayItem.Index < collectionListView.Items.Count - 1) {
                    ShowImage(collectionListView.Items[_displayItem.Index + 1] as ImageCollectionListViewItem);
                }
            }
        }

        private void collectionRefreshCmd_Click(object sender, EventArgs e)
        {
            if (captureBox.State == DxCaptureBoxState.Still) {
                ShowImage(null);
            }
            LoadCollection();
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

        #region ImagingView Event Handlers (CaptureBox)

        private void captureBox_PlayStateChange(object sender, PlayStateChangeEventArgs e)
        {
            playToolStripMenuItem.Enabled = (e.State == DxCaptureBoxState.Inactive || e.State == DxCaptureBoxState.Paused || e.State == DxCaptureBoxState.Still);
            captureStartButton.Enabled = (e.State == DxCaptureBoxState.Inactive || e.State == DxCaptureBoxState.Paused || e.State == DxCaptureBoxState.Still);

            stopToolStripMenuItem.Enabled = (e.State == DxCaptureBoxState.Playing || e.State == DxCaptureBoxState.Paused);
            captureStopButton.Enabled = (e.State == DxCaptureBoxState.Playing || e.State == DxCaptureBoxState.Paused);

            snapshotToolStripMenuItem.Enabled = (e.State == DxCaptureBoxState.Playing || e.State == DxCaptureBoxState.Paused);
            captureSnapshotButton.Enabled = (e.State == DxCaptureBoxState.Playing || e.State == DxCaptureBoxState.Paused);

            if (e.State == DxCaptureBoxState.Inactive) {
                imageStatusLabel.Text = String.Empty;
            }
        }

        #endregion
    }
}