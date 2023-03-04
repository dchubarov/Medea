// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      PhysicianDialog.cs
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

namespace Gyrosoft.Medea.Gui
{
    public partial class PhysicianDialog : Form
    {
        /// <summary>
        /// Constructs new PhysicianDialog object.
        /// </summary>
        public PhysicianDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            String tmp = familyNameEdit.Text.Trim();
            if (String.IsNullOrEmpty(tmp)) {
                MessageBox.Show(Properties.Resources.ErrorInvalidName, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}