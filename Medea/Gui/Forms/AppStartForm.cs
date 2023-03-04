// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      AppStartForm.cs
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
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Gui
{
    public partial class AppStartForm : Form, IStatusManager
    {
        private bool _disableClose = true;

        /// <summary>
        /// Constructs new AppStartForm object.
        /// </summary>
        public AppStartForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets value indicating whether form can be closed by user.
        /// </summary>
        public bool DisableClose
        {
            get
            {
                return _disableClose;
            }

            set
            {
                _disableClose = value;
            }
        }

        #region IStatusWindow Members

        /// <summary>
        /// <see cref="IStatusWindow.SetStatusText()" />
        /// </summary>
        public void SetStatusText()
        {
            this.SetStatusText("StatusLoading", true);
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
            statusLabel.Text = (isResourceName) ?
                Properties.Resources.ResourceManager.GetString(statusTextOrResourceName) :
                statusTextOrResourceName;

            this.Refresh();
        }

        public void ProgressControl(object owner, ProgressControlCommand command, params object[] args)
        {
            // AppStartForm does not provide progress, do nothing
        }

        public void SetWaitCursor()
        {
            this.Cursor = Cursors.WaitCursor;
        }

        #endregion

        #region AppStartForm Event Handlers (Form)

        private void Form_Load(object sender, EventArgs e)
        {
            // set localized window caption
            this.Text = Properties.Resources.ApplicationTitle;

            companyLabel.Text = Properties.Resources.CompanyName;
            //companyLabel.Text = this.CompanyName;
            //versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // register form as application status window
            Program.StatusManager = this;
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = _disableClose;
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            // unregister status window
            if (Object.ReferenceEquals(this, Program.StatusManager))
                Program.StatusManager = null;
        }

        #endregion
    }
}