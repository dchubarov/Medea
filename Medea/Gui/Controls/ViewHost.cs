// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ViewControl.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Gui
{
    public partial class ViewHost : UserControl, IViewHost
    {
        #region Private Types

        private class HistoryItem
        {
            #region Private Fields

            private IViewFactory _factory;
            private string _displayName;

            #endregion

            #region Construction

            public HistoryItem(IViewFactory factory)
            {
                _factory = factory;
                _displayName = String.Empty;
            }

            #endregion

            #region Public Properties

            public IViewFactory Factory
            {
                get
                {
                    return _factory;
                }
            }

            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
            }

            #endregion

            #region Public Methods

            public void ObtainDisplayNameFromFactory()
            {
                if (this.Factory != null) {
                    if (String.IsNullOrEmpty(this.Factory.Detail))
                        _displayName = this.Factory.Caption;
                    else {
                        _displayName = String.Format(
                            Properties.Resources.NavHistoryTitle,
                            this.Factory.Caption,
                            this.Factory.Detail
                            );
                    }
                }
            }

            #endregion
        }

        #endregion

        #region Private Fields

        private List<HistoryItem> _historyList = new List<HistoryItem>();
        private int _historyPosition = -1;
        private bool _suppressCanClose = false;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new ViewControl object.
        /// </summary>
        public ViewHost()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public bool IsEmpty
        {
            get
            {
                return (Object.ReferenceEquals(this.CurrentHistoryItem, null) || 
                    Object.ReferenceEquals(this.CurrentHistoryItem.Factory, null) || 
                    Object.ReferenceEquals(this.CurrentHistoryItem.Factory.Control, null));
            }
        }

        public MenuStrip Menu
        {
            get
            {
                return (this.IsEmpty ? null : this.CurrentHistoryItem.Factory.MenuStrip);
            }
        }

        public bool SuppressCanClose
        {
            get
            {
                return _suppressCanClose;
            }

            set
            {
                _suppressCanClose = value;
            }
        }

        #endregion

        #region Private Properties

        private HistoryItem CurrentHistoryItem
        {
            get
            {
                return ((_historyPosition >= 0 && _historyPosition < _historyList.Count) ? 
                    _historyList[_historyPosition] : null);
            }
        }

        private int HistoryPosition
        {
            get
            {
                return _historyPosition;
            }

            set
            {
                bool cancelled = false;
                this.InternalHost(_historyList[value].Factory, true, CloseReason.UserClosing, out cancelled);
                if (!cancelled) {
                    _historyPosition = value;
                }
            }
        }

        #endregion

        #region Public Methods

        public bool CanClose(CloseReason closeReason)
        {
            return (this.IsEmpty ? true : this.CurrentHistoryItem.Factory.CanClose(closeReason));
        }

        #endregion

        #region Private Methods

        private void InternalHost(IViewFactory factory, bool historyItem, CloseReason closeReason, out bool cancelled)
        {
            cancelled = false;

            try {
                if (factory != null) {
                    // make factory to create a control
                    factory.CreateControl();

                    // check whether control created
                    if (Object.ReferenceEquals(factory.Control, null)) {
                        throw new Exception("Error creating control.");
                    }
                }

                ToolStrip hostedTools = null;

                // destroy current view
                if (!Object.ReferenceEquals(this.CurrentHistoryItem, null)) {
                    IViewFactory oldFactory = this.CurrentHistoryItem.Factory;
                    if (oldFactory == null || oldFactory.Control == null ||
                        !Object.ReferenceEquals(oldFactory.Control.Parent, hostPanel)) {
                        //throw new Exception("The current view in not valid.");
                        //do nothing
                    }
                    else {

                        if (!this.SuppressCanClose && !oldFactory.CanClose(closeReason))
                            cancelled = true;
                        else {
                            // obtain display name from last state of the control (for history)
                            this.CurrentHistoryItem.ObtainDisplayNameFromFactory();

                            // update main menu
                            this.UpdateMainMenu(AppMainForm.UpdateMenuMode.Revert);

                            // revert merge of current control's tool strip
                            if (!Object.ReferenceEquals((hostedTools = oldFactory.ToolStrip), null)) {
                                ToolStripManager.RevertMerge(viewTools, hostedTools);
                                hostedToolsSeparator.Visible = false;
                                hostedTools.Visible = false;
                                hostedTools = null;
                            }

                            // destroy a control
                            oldFactory.Control.Visible = false;
                            oldFactory.Control.Parent = null;
                            oldFactory.DestroyControl();

                            if (oldFactory.Control != null) {
                                throw new Exception("Failed to destroy control.");
                            }
                        }
                    }
                }

                if (!Object.ReferenceEquals(factory, null) && !cancelled) {
                    // set user interface wait state
                    Program.StatusManager.SetStatusText(Properties.Resources.StatusOpenView);
                    Program.StatusManager.SetWaitCursor();

                    Control hostedControl = factory.Control;
                    try {
                        int pos = 0;

                        if (historyItem) {
                            // search for factory in history list
                            HistoryItem historyItemFound = null;
                            foreach (HistoryItem hi in _historyList) {
                                if (Object.ReferenceEquals(hi.Factory, factory)) {
                                    historyItemFound = hi;
                                    break;
                                }
                                ++pos;
                            }

                            // factory not found in history list
                            if (Object.ReferenceEquals(historyItemFound, null)) {
                                throw new Exception("Could not find specified factory object in history list.");
                            }
                        }
                        else {
                            // factory must not exists in history list
                            foreach (HistoryItem hi in _historyList) {
                                if (Object.ReferenceEquals(hi.Factory, factory)) {
                                    throw new Exception("The specified factory object already in history list.");
                                }
                            }

                            if (_historyPosition < _historyList.Count - 1) {
                                _historyList.RemoveRange(_historyPosition + 1, _historyList.Count - _historyPosition - 1);
                            }

                            _historyList.Add(new HistoryItem(factory));
                            pos = _historyList.Count - 1;
                        }

                        _historyPosition = pos;
                        UpdateHistoryUI();

                        // initialize new control
                        hostedControl.Parent = hostPanel;
                        hostedControl.Dock = DockStyle.Fill;
                        hostedControl.Visible = true;
                        UpdateCaption();

                        // make factory to activate control (bind to data for example)
                        factory.ActivateControl();
                        UpdateCaption();

                        // update main menu
                        this.UpdateMainMenu(AppMainForm.UpdateMenuMode.Merge);

                        // merge view tool strip with control tool strip
                        if (!Object.ReferenceEquals((hostedTools = factory.ToolStrip), null)) {
                            ToolStripManager.Merge(hostedTools, viewTools);
                            hostedToolsSeparator.Visible = true;
                            hostedTools = null;
                        }

                        this.SuppressCanClose = false;
                        hostedControl = null;
                    }
                    finally {
                        if (hostedControl != null) {
                            factory.DestroyControl();
                        }
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void UpdateMainMenu(AppMainForm.UpdateMenuMode mode)
        {
            IViewManager vm = this.ViewManager;
            if (vm is AppMainForm) {
                ((AppMainForm)vm).UpdateMenu(mode);
            }
        }

        private void UpdateHistoryUI()
        {
            ToolStripItem toolItem;
            int index;

            // go back button state and drop-down menu
            navBackButton.Enabled = (this.HistoryPosition > 0);
            navBackButton.DropDown.Items.Clear();
            navBackButton.Tag = null;

            if (this.HistoryPosition < 1)
                navBackButton.ToolTipText = Properties.Resources.NavBack;
            else {
                for (index = this.HistoryPosition - 1; index >= 0; index--) {
                    toolItem = navBackButton.DropDown.Items.Add(_historyList[index].DisplayName);
                    toolItem.Click += new EventHandler(navigationCmd_Click);
                    toolItem.Tag = _historyList[index];
                }

                navBackButton.ToolTipText = String.Format(
                    Properties.Resources.NavBackFmt,
                    navBackButton.DropDown.Items[0].Text
                    );
            }

            // go forward button state and drop-down menu
            navForwardButton.Enabled = (this.HistoryPosition < _historyList.Count - 1);
            navForwardButton.DropDown.Items.Clear();
            navForwardButton.Tag = null;

            if (this.HistoryPosition >= _historyList.Count - 1)
                navForwardButton.ToolTipText = Properties.Resources.NavForward;
            else {
                for (index = this.HistoryPosition + 1; index <= _historyList.Count - 1; index++) {
                    toolItem = navForwardButton.DropDown.Items.Add(_historyList[index].DisplayName);
                    toolItem.Click += new EventHandler(navigationCmd_Click);
                    toolItem.Tag = _historyList[index];
                }

                navForwardButton.ToolTipText = String.Format(
                    Properties.Resources.NavForwardFmt,
                    navForwardButton.DropDown.Items[0].Text
                    );
            }

            // check whether taskpad view is open, hide button if so
            navHomeButton.Visible = (this.CurrentHistoryItem == null ||
                !(this.CurrentHistoryItem.Factory is TaskView.Factory));
        }

        #endregion

        #region IViewHost Members

        public IViewManager ViewManager
        {
            get
            {
                Control parentControl = this.Parent;
                while (parentControl != null) {
                    if (parentControl is IViewManager) break;
                    parentControl = parentControl.Parent;
                }

                if (parentControl == null) {
                    throw new Exception("Could not find view manager for current view.");
                }

                return (IViewManager)parentControl;
            }
        }

        public bool Host(IViewFactory factory)
        {
            return this.Host(factory, CloseReason.UserClosing);
        }

        public bool Host(IViewFactory factory, CloseReason closeReason)
        {
            bool cancelled = false;
            this.InternalHost(factory, false, closeReason, out cancelled);
            return (!cancelled);
        }

        public void UpdateCaption()
        {
            if (Object.ReferenceEquals(this.ViewManager.ActiveView, this)) {
                captionLabel.ForeColor = SystemColors.WindowText;
                //subCaptionLabel.ForeColor = SystemColors.WindowText;
            }
            else {
                captionLabel.ForeColor = SystemColors.ControlDark;
                //subCaptionLabel.ForeColor = SystemColors.ControlDark;
            }

            HistoryItem hi = this.CurrentHistoryItem;
            if (!Object.ReferenceEquals(hi, null)) {
                captionLabel.Text = hi.Factory.Caption;
                String tmp = hi.Factory.Detail;
                if (!String.IsNullOrEmpty(tmp)) {
                    captionLabel.Text += " [" + tmp + "]";
                }

                //captionLabel.Text = hi.Factory.Caption;
                //subCaptionLabel.Text = hi.Factory.Detail;
            }
            else {
                captionLabel.Text = String.Empty;
                //subCaptionLabel.Text = String.Empty;
            }

            captionBox.Refresh();
            viewTools.Refresh();
        }

        public bool HandleKeyPress(KeyPressEventArgs e)
        {
            HistoryItem hi = this.CurrentHistoryItem;
            if (hi != null && hi.Factory != null) {
                return hi.Factory.HandleKeyPress(e);
            }

            return false;
        }

        #endregion

        #region ViewHost Event Handlers (Control)

        private void ViewHost_Load(object sender, EventArgs e)
        {
            // create a font for view caption
            captionLabel.Font = new Font(
                SystemFonts.CaptionFont.FontFamily,
                captionLabel.Font.Size,
                captionLabel.Font.Style
                );

            // create a font for view sub-caption
            /*
            subCaptionLabel.Font = new Font(
                SystemFonts.CaptionFont.FontFamily,
                subCaptionLabel.Font.Size,
                subCaptionLabel.Font.Style
                );
             */
        }

        private void captionArea_Click(object sender, EventArgs e)
        {
            if (!this.IsEmpty) {
                this.CurrentHistoryItem.Factory.Control.Focus();
            }
            else {
                hostPanel.Focus();
            }
        }

        #endregion

        #region ViewHost Event Handlers (Tools)

        private void navigationCmd_Click(object sender, EventArgs e)
        {
            HistoryItem hi = null;

            if (Object.ReferenceEquals(sender, navBackButton)) {
                if (navBackButton.DropDown.Items.Count > 0)
                    hi = navBackButton.DropDown.Items[0].Tag as HistoryItem;
            }
            else if (Object.ReferenceEquals(sender, navForwardButton)) {
                if (navForwardButton.DropDown.Items.Count > 0)
                    hi = navForwardButton.DropDown.Items[0].Tag as HistoryItem;
            }
            else {
                if (!Object.ReferenceEquals(sender, null))
                    hi = ((ToolStripItem)sender).Tag as HistoryItem;
            }

            if (!Object.ReferenceEquals(hi, null)) {
                bool cancelled = false;
                this.InternalHost(hi.Factory, true, CloseReason.UserClosing, out cancelled);
            }
        }

        private void navigationHomeCmd_Click(object sender, EventArgs e)
        {
            if (this.HistoryPosition > 0) {
                for (int i = this.HistoryPosition - 1; i >= 0; i--) {
                    HistoryItem hi = _historyList[i];
                    if (hi.Factory is TaskView.Factory) {
                        this.HistoryPosition = i;
                        break;
                    }
                }
            }
        }

        #endregion
    }
}