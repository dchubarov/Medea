// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      AppMainForm.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Gui
{
    public partial class AppMainForm : Form, IStatusManager, IViewManager
    {
        #region Public Types

        [Flags]
        public enum UpdateMenuMode
        {
            None   = 0,
            Merge  = 1,
            Revert = 2,
            Update = Revert | Merge
        }

        #endregion

        #region Private Fields

        private AppStartForm _startForm = null;
        private ViewHost _activeView = null;
        private ViewHost _menuOwner = null;
        private object _progressOwner = null;
        private bool _progressActive = false;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new AppMainForm object.
        /// </summary>
        public AppMainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Properties

        private bool Splitted
        {
            get
            {
                return (!splitContainer.Panel1Collapsed && !splitContainer.Panel2Collapsed);
            }
        }

        private SplitterPanel ActivePanel
        {
            get
            {
                return (_activeView == null ? null : (SplitterPanel)_activeView.Parent);
            }
        }

        #endregion

        #region Public Methods

        public void UpdateMenu()
        {
            this.UpdateMenu(UpdateMenuMode.Update);
        }

        public void UpdateMenu(UpdateMenuMode mode)
        {
            MenuStrip menu = null;

            if ((mode & UpdateMenuMode.Revert) != 0) {
                if (_menuOwner != null && !_menuOwner.IsEmpty) {
                    if ((menu = _menuOwner.Menu) != null) {
                        ToolStripManager.RevertMerge(this.MainMenuStrip, menu);
                    }
                    _menuOwner = null;
                }
            }

            if ((mode & UpdateMenuMode.Merge) != 0) {
                if (_activeView != null && (menu = _activeView.Menu) != null) {
                    ToolStripManager.Merge(menu, this.MainMenuStrip);
                    _menuOwner = _activeView;
                }
            }
        }

        #endregion

        #region Private Methods

        private void Split()
        {
            if (!this.Splitted) {
                this.SuspendLayout();
                try {
                    ViewHost inactiveView;

                    // expand the the panel currently collapsed
                    if (splitContainer.Panel1Collapsed) {
                        splitContainer.Panel1Collapsed = false;
                        inactiveView = viewHost1;
                    }
                    else {
                        splitContainer.Panel2Collapsed = false;
                        inactiveView = viewHost2;
                    }

                    // host a task view if newly open view is empty
                    if (inactiveView.IsEmpty) {
                        inactiveView.Host(new TaskView.Factory());
                    }

                    // add spacing between window frame and interior
                    Padding panelPadding = new Padding(1, 0, 1, 1);
                    layoutHostPanel.Padding = panelPadding;

                    // add spacing for view selection border
                    panelPadding = new Padding(2);
                    splitContainer.Panel1.Padding = panelPadding;
                    splitContainer.Panel2.Padding = panelPadding;

                    // add spacing around both view hosts
                    panelPadding = new Padding(1);
                    viewHost1.Padding = panelPadding;
                    viewHost2.Padding = panelPadding;

                    // enable timer that tracks focus changes
                    viewStateTimer.Enabled = true;
                }
                finally {
                    this.ResumeLayout();
                }
            }
        }

        private void Unsplit()
        {
            if (this.Splitted) {
                this.SuspendLayout();
                try {
                    // disable focus track timer
                    viewStateTimer.Enabled = false;

                    // collapse panel the invactive view belongs to
                    if (Object.ReferenceEquals(_activeView, viewHost1)) {
                        splitContainer.Panel2Collapsed = true;
                    }
                    else {
                        splitContainer.Panel1Collapsed = true;
                    }

                    // revert layout spacers to initial state

                    Padding panelPadding = new Padding(0);

                    viewHost1.Padding = panelPadding;
                    viewHost2.Padding = panelPadding;

                    splitContainer.Panel1.Padding = panelPadding;
                    splitContainer.Panel2.Padding = panelPadding;

                    layoutHostPanel.Padding = panelPadding;
                }
                finally {
                    this.ResumeLayout();
                }
            }
        }

        private void SetActiveView(ViewHost view)
        {
            if (!Object.ReferenceEquals(viewHost1, view) && !Object.ReferenceEquals(viewHost2, view)) {
                throw new Exception("Unable to activate view.");
            }

            if (!Object.ReferenceEquals(_activeView, view)) {
                ViewHost prevActiveView = _activeView;
                SplitterPanel prevActivePanel = this.ActivePanel;

                _activeView = view;
                _activeView.UpdateCaption();
                this.ActivePanel.BackColor = SystemColors.Highlight;

                if (!Object.ReferenceEquals(prevActiveView, null)) {
                    prevActiveView.UpdateCaption();
                    prevActivePanel.BackColor = SystemColors.Control;
                }

                UpdateMenu();
            }
        }

        private void InitSplash()
        {
            if (_startForm == null) {
                _startForm = new AppStartForm();
                _startForm.Show();
                _startForm.SetStatusText();
            }
        }

        private void DestroySplash()
        {
            if (_startForm != null) {
                _startForm.Hide();
                _startForm.Dispose();
                _startForm = null;
            }
        }

        private void SetupFormCaption()
        {
            string userName = "***";
            try {
                ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Clinic), null);
                if (!cur.Eof) {
                    Clinic clinic = cur.Current as Clinic;
                    if (clinic != null) {
                        userName = clinic.GetDisplayName();
                    }
                }
            }
            catch (Exception) {
                // eat
            }

            // set localized window caption
            this.Text = String.Format(
                Properties.Settings.Default.DisplayAssemblyVersion ?
                    Properties.Resources.ApplicationFormCaptionFmtWithVersion :
                    Properties.Resources.ApplicationFormCaptionFmt,
                Program.Title,
                userName,
//              Licensing.LicenseInfoHelper.GetEditionName(Program.LicenseManager),
                this.GetType().Assembly.GetName().Version.ToString()
            );
        }

        #endregion

        #region IStatusWindow Members

        /// <summary>
        /// <see cref="IStatusWindow.SetStatusText()" />
        /// </summary>
        public void SetStatusText()
        {
            this.SetStatusText("StatusReady", true);
        }

        /// <summary>
        /// <see cref="IStatusWindow.SetStatusText(string)" />
        /// </summary>
        public void SetStatusText(string statusText)
        {
            this.SetStatusText(statusText, false);
        }

        /// <summary>
        /// <see cref="IStatusWindow.SetStatusText(string, bool)" />
        /// </summary>
        public void SetStatusText(string statusTextOrResourceName, bool isResourceName)
        {
            statusMsg.Text = (isResourceName) ? 
                Properties.Resources.ResourceManager.GetString(statusTextOrResourceName) : 
                statusTextOrResourceName;

            statusStrip.Refresh();
        }

        public void ProgressControl(object owner, ProgressControlCommand command, params object[] args)
        {
            if (_progressOwner != null && !Object.ReferenceEquals(owner, _progressOwner)) {
                throw new Exception("Progress bar is occupied by another object.");
            }

            if (command != ProgressControlCommand.Init && !_progressActive) {
                throw new InvalidOperationException("Progress bar is not active.");
            }

            switch (command) {
                case ProgressControlCommand.Init:
                    if (args.Length < 2 || args[0].GetType() != typeof(System.Int32) || args[1].GetType() != typeof(System.Int32)) {
                        throw new ArgumentException("ProgressBarControl (Init): required parameters(s) missing.");
                    }

                    statusProgress.Minimum = (int)args[0];
                    statusProgress.Maximum = (int)args[1];

                    if (args.Length > 2) {
                        statusProgress.ToolTipText = args[2].ToString();
                    }

                    statusProgress.Value = statusProgress.Minimum;
                    statusProgress.Visible = true;

                    _progressActive = true;
                    _progressOwner = owner;
                    break;

                case ProgressControlCommand.Release:
                    statusProgress.Visible = false;
                    _progressActive = false;
                    _progressOwner = null;
                    break;

                case ProgressControlCommand.Set:
                    if (args.Length < 1 || args[0].GetType() != typeof(System.Int32)) {
                        throw new ArgumentException("ProgressBarControl (Set): required parameter(s) missing.");
                    }
                    statusProgress.Value = (int)args[0];
                    if (args.Length > 1) {
                        statusProgress.ToolTipText = args[1].ToString();
                    }
                    break;

                case ProgressControlCommand.SetToolTipText:
                    if (args.Length < 1) {
                        statusProgress.ToolTipText = String.Empty;
                    }
                    else {
                        statusProgress.ToolTipText = args[0].ToString();
                    }
                    break;

            }

            statusStrip.Refresh();
        }

        public void SetWaitCursor()
        {
            this.Cursor = Cursors.WaitCursor;
        }

        #endregion

        #region IViewManager Members

        public IViewHost ActiveView
        {
            get
            {
                return _activeView;
            }
        }

        public IViewHost GetCurrentView(Control childControl)
        {
            if (childControl == null) {
                throw new ArgumentNullException("childControl");
            }

            Control parentControl = childControl.Parent;
            while (parentControl != null) {
                if (parentControl is IViewHost) {
                    break;
                }
                parentControl = parentControl.Parent;
            }

            return (IViewHost)parentControl;
        }

        public IViewHost GetOppositeView(Control childControl)
        {
            IViewHost currentView = this.GetCurrentView(childControl);
            if (currentView != null) {
                return (IViewHost)(Object.ReferenceEquals(currentView, viewHost1) ? viewHost2 : viewHost1);
            }
            return null;
        }

        #endregion

        #region AppMainForm Event Handlers (Form)

        private void Form_Load(object sender, EventArgs e)
        {
            // initialize start-up form
            this.InitSplash();

            // perform program start-up sequence
            if (!Program.Startup()) {
                Program.Logger.Fatal("Fatal error occured during application startup, terminating application...");
                this.Close();
                return;
            }

            // *** TEMP
            ToolStripManager.RenderMode = ToolStripManagerRenderMode.System;
            // *** END TEMP

            SetupFormCaption();

            // register application event handlers
            Application.Idle += new EventHandler(Application_Idle);
            Program.StatusManager = this;
            Program.ViewManager = this;
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            // destroy start-up form
            this.DestroySplash();

            viewHost1.Host(new TaskView.Factory());
            this.SetActiveView(viewHost1);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            try {
                // if form was not shown due to startup errors we need to destroy splash screen
                this.DestroySplash();

                // allow views to take actions on unsaved data
                if (!viewHost1.CanClose(e.CloseReason) || !viewHost2.CanClose(e.CloseReason)) {
                    e.Cancel = true;
                }
                else {
                    // close left view
                    if (!viewHost1.IsEmpty) {
                        viewHost1.SuppressCanClose = true;
                        viewHost1.Host(null);
                    }

                    // close right view
                    if (!viewHost2.IsEmpty) {
                        viewHost2.SuppressCanClose = true;
                        viewHost2.Host(null);
                    }

                    // shutdown application
                    Program.Shutdown();
                }
            }
            catch (Exception exc) {
                Program.ShowErrorMessage(exc.Message);
                e.Cancel = true;
            }
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            // unregister application event handler(s)
            Application.Idle -= new EventHandler(Application_Idle);

            // unregister view manager
            if (Object.ReferenceEquals(this, Program.ViewManager)) {
                Program.ViewManager = null;
            }

            // unregister status window
            if (Object.ReferenceEquals(this, Program.StatusManager)) {
                Program.StatusManager = null;
            }

            Program.Logger.Info("User interface closed.");
        }

        private void viewStateTimer_Tick(object sender, EventArgs e)
        {
            if (this.Splitted) {
                if (viewHost1.ContainsFocus) {
                    this.SetActiveView(viewHost1);
                }
                else if (viewHost2.ContainsFocus) {
                    this.SetActiveView(viewHost2);
                }
            }
        }

        private void AppMainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ActiveView != null) {
                if (ActiveView.HandleKeyPress(e)) {
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region AppMainForm Event Handlers (Commands)

        private void mainMenuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainMenuView_DropDownOpening(object sender, EventArgs e)
        {
            mainMenuViewSplit.Checked = this.Splitted;
        }

        private void mainMenuViewSplit_Click(object sender, EventArgs e)
        {
            if (this.Splitted) {
                this.Unsplit();
            }
            else {
                this.Split();
            }
        }

        private void mainMenuToolsOptions_Click(object sender, EventArgs e)
        {
            OptionDialog optionsDlg = new OptionDialog();
            if (optionsDlg.ShowDialog() == DialogResult.OK) {

            }
        }

        private void mainMenuHelp_DropDownOpening(object sender, EventArgs e)
        {
            // format and set localized text for About... item 
            // when help menu displayed for the first time
            if (mainMenuHelpAbout.Tag == null) {
                mainMenuHelpAbout.Text = String.Format(
                    Properties.Resources.ApplicationAboutMenuItemFmt,
                    Properties.Resources.ApplicationTitle
                );
                mainMenuHelpAbout.Tag = true;
            }
        }

        private void mainMenuHelpAbout_Click(object sender, EventArgs e)
        {
            // display about box
            AboutDialog aboutDlg = new AboutDialog();
            aboutDlg.ShowDialog();
        }

        private void personalizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonalizationDialog dlg = new PersonalizationDialog();
            dlg.ShowDialog();
            SetupFormCaption();
        }

        #endregion

        #region AppMainForm Event Handlers (Application)

        private void Application_Idle(object sender, EventArgs e)
        {
            if (Program.StatusManager != null) {
                Program.StatusManager.SetStatusText();
            }

            if (!Object.ReferenceEquals(this.Cursor, Cursors.Default)) {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}