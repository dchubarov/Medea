// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      RollDialog.cs
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
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Config;
using Gyrosoft.Medea.DataAccess;

namespace Gyrosoft.Medea.Gui
{
    public partial class RollDialog : Form
    {
        #region Private Instance Fields

        private string _rollName = null;
        private Entity _entity = null;

        #endregion

        #region Public Instance Constructors

        /// <summary>
        /// Constructs new RollDialog object.
        /// </summary>
        public RollDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Instance Properties

        public string RollName
        {
            get
            {
                return _rollName;
            }

            set
            {
                _rollName = value;
            }
        }

        public Entity SelectedEntity
        {
            get
            {
                return _entity;
            }
        }

        #endregion

        #region Private Instance Methods

        private bool GetSelectedObject()
        {
            if (rollGrid.SelectedRows.Count > 0) {
                _entity = rollGrid.SelectedRows[0].DataBoundItem as Entity;
                return (_entity != null);
            }

            return false;
        }

        #endregion

        #region RollDialog Event Handlers (Control)

        private void RollDialog_Load(object sender, EventArgs e)
        {
            IRollConfig rc = null;

            if (!String.IsNullOrEmpty(this.RollName)) {
                rc = Program.ConfigManager.GetRollConfig(this.RollName);
            }

            if (rc == null) {
                throw new Exception("Invalid roll configuration.");
            }

            ConfigManagerBase.InitGridView(rollGrid, rc, null);
            this.Text = rc.DisplayName;
        }

        private void rollGrid_DoubleClick(object sender, EventArgs e)
        {
            if (GetSelectedObject()) {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (GetSelectedObject()) {
                this.DialogResult = DialogResult.OK;
            }
        }

        #endregion
    }
}