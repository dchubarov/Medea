// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      PersonalizationDialog.cs
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
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;

namespace Gyrosoft.Medea.Gui
{
    public partial class PersonalizationDialog : Form
    {
        private Clinic _clinic = null;
        private TreeNode _clinicNode = null;
        private ExaminationKind _extEdit = null;

        /// <summary>
        /// Constructs new PersonalizationDialog object.
        /// </summary>
        public PersonalizationDialog()
        {
            InitializeComponent();
        }

        private void PersonalizationDialog_Load(object sender, EventArgs e)
        {
            ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Clinic), null);
            if (cur.Count > 0)
                _clinic = (Clinic)cur.Current;
            else {
                _clinic = new Clinic();
                Program.Database.EntityStorage.BeginTrans();
                Program.Database.EntityStorage.Store(_clinic);
                Program.Database.EntityStorage.CommitTrans();
            }

            clinicNameEdit.Text = _clinic.Name;
            _clinicNode = clinicTree.Nodes.Add(_clinic.Name);
            _clinicNode.Tag = _clinic;

            Filter f = new Filter("_clinic", FilterCondition.EqualRef, _clinic);
            cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Department), f);
            while (!cur.Eof) {
                Department dep = (Department)cur.Current;
                TreeNode depNode = _clinicNode.Nodes.Add(dep.Name);
                depNode.Tag = dep;

                Filter pf = new Filter("_department", FilterCondition.EqualRef, dep);
                ICursor pcur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Physician), pf);
                while (!pcur.Eof) {
                    Physician phy = (Physician)pcur.Current;
                    TreeNode phyNode = depNode.Nodes.Add(phy.GetDisplayName());
                    phyNode.Tag = phy;
                    pcur.MoveNext();
                }
                cur.MoveNext();
            }

            clinicTree.SelectedNode = _clinicNode;
            _clinicNode.ExpandAll();

            cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(ExaminationKind), null);
            while (!cur.Eof) {
                extList.Items.Add(cur.Current);
                cur.MoveNext();
            }
        }

        private void clinicNameSetBtn_Click(object sender, EventArgs e)
        {
            try {
                String tmp = clinicNameEdit.Text.Trim();
                if (!String.IsNullOrEmpty(tmp)) {
                    _clinic.Name = tmp;
                    Program.Database.EntityStorage.BeginTrans();
                    Program.Database.EntityStorage.Store(_clinic);
                    Program.Database.EntityStorage.CommitTrans();
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            _clinicNode.Text = _clinic.Name;
        }

        private void clinicNameEdit_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = clinicNameSetBtn;
        }

        private void clinicNameEdit_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = cancelButton;
        }

        private void clinicTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            addDepartmentBtn.Enabled = (clinicTree.SelectedNode != null && clinicTree.SelectedNode.Tag is Clinic);
            addPhysicianBtn.Enabled = (clinicTree.SelectedNode != null && clinicTree.SelectedNode.Tag is Department);
            editBtn.Enabled = (clinicTree.SelectedNode != null && (clinicTree.SelectedNode.Tag is Department || clinicTree.SelectedNode.Tag is Physician));
            deleteBtn.Enabled = (clinicTree.SelectedNode != null && (clinicTree.SelectedNode.Tag is Department || clinicTree.SelectedNode.Tag is Physician));
        }

        private void addDepartmentBtn_Click(object sender, EventArgs e)
        {
            if (clinicTree.SelectedNode != null && clinicTree.SelectedNode.Tag is Clinic) {
                DepartmentDialog dlg = new DepartmentDialog();
                if (dlg.ShowDialog() == DialogResult.OK) {
                    try {
                        Department dep = new Department();
                        dep.Clinic = _clinic;
                        dep.Name = dlg.depNameEdit.Text.Trim();
                        Program.Database.EntityStorage.BeginTrans();
                        Program.Database.EntityStorage.Store(dep);
                        Program.Database.EntityStorage.CommitTrans();

                        TreeNode depNode = _clinicNode.Nodes.Add(dep.Name);
                        depNode.Tag = dep;
                        depNode.Parent.Expand();
                        depNode.EnsureVisible();
                    }
                    catch (Exception exc) {
                        MessageBox.Show(exc.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void addPhysicianBtn_Click(object sender, EventArgs e)
        {
            if (clinicTree.SelectedNode != null && clinicTree.SelectedNode.Tag is Department) {
                PhysicianDialog dlg = new PhysicianDialog();
                if (dlg.ShowDialog() == DialogResult.OK) {
                    try {
                        Physician phy = new Physician();
                        phy.Department = clinicTree.SelectedNode.Tag as Department;
                        phy.FamilyName = dlg.familyNameEdit.Text.Trim();
                        phy.FirstName = dlg.firstNameEdit.Text.Trim();
                        phy.MiddleName = dlg.middleNameEdit.Text.Trim();

                        Program.Database.EntityStorage.BeginTrans();
                        Program.Database.EntityStorage.Store(phy);
                        Program.Database.EntityStorage.CommitTrans();

                        TreeNode phyNode = clinicTree.SelectedNode.Nodes.Add(phy.GetDisplayName());
                        phyNode.Tag = phy;

                        phyNode.Parent.Expand();
                        phyNode.EnsureVisible();
                    }
                    catch (Exception exc) {
                        MessageBox.Show(exc.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            try {
                if (clinicTree.SelectedNode != null) {
                    if (clinicTree.SelectedNode.Tag is Department) {
                        Department dep = clinicTree.SelectedNode.Tag as Department;
                        DepartmentDialog dlg = new DepartmentDialog();
                        dlg.depNameEdit.Text = dep.Name;
                        if (dlg.ShowDialog() == DialogResult.OK) {
                            dep.Name = dlg.depNameEdit.Text.Trim();
                            Program.Database.EntityStorage.BeginTrans();
                            Program.Database.EntityStorage.Store(dep);
                            Program.Database.EntityStorage.CommitTrans();
                            clinicTree.SelectedNode.Text = dep.Name;
                        }
                    }
                    else if (clinicTree.SelectedNode.Tag is Physician) {
                        Physician phy = clinicTree.SelectedNode.Tag as Physician;
                        PhysicianDialog dlg = new PhysicianDialog();
                        dlg.familyNameEdit.Text = phy.FamilyName;
                        dlg.firstNameEdit.Text = phy.FirstName;
                        dlg.middleNameEdit.Text = phy.MiddleName;
                        if (dlg.ShowDialog() == DialogResult.OK) {
                            phy.FamilyName = dlg.familyNameEdit.Text.Trim();
                            phy.FirstName = dlg.firstNameEdit.Text.Trim();
                            phy.MiddleName = dlg.middleNameEdit.Text.Trim();
                            Program.Database.EntityStorage.BeginTrans();
                            Program.Database.EntityStorage.Store(phy);
                            Program.Database.EntityStorage.CommitTrans();
                            clinicTree.SelectedNode.Text = phy.GetDisplayName();
                        }
                    }
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void extEdit_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = extSetBtn;
        }

        private void extEdit_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = cancelButton;
        }

        private void extSetBtn_Click(object sender, EventArgs e)
        {
            try {
                String tmp = extEdit.Text.Trim();
                if (!String.IsNullOrEmpty(tmp)) {
                    bool bAdd = false;
                    if (_extEdit == null) {
                        _extEdit = new ExaminationKind();
                        bAdd = true;
                    }

                    _extEdit.Name = tmp;
                    Program.Database.EntityStorage.BeginTrans();
                    Program.Database.EntityStorage.Store(_extEdit);
                    Program.Database.EntityStorage.CommitTrans();

                    if (bAdd) extList.Items.Add(_extEdit);
                    _extEdit = null;

                    extEdit.Text = String.Empty;
                    extSetBtn.Text = Properties.Resources.ButtonAdd;
                    extList.FormattingEnabled = false;
                    extList.FormattingEnabled = true;
                }
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, Program.Title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void extList_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = (e.ListItem as ExaminationKind).Name;
        }

        private void extList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _extEdit = null;
            extSetBtn.Text = Properties.Resources.ButtonAdd;
            extEdit.Text = String.Empty;
        }

        private void extList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (extList.SelectedIndex >= 0) {
                _extEdit = extList.Items[extList.SelectedIndex] as ExaminationKind;
                extSetBtn.Text = Properties.Resources.ButtonChange;
                extEdit.Text = _extEdit.Name;
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            try {
                if (MessageBox.Show(Properties.Resources.ConfirmDeleteEntity, Program.Title,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                ICursor cur;
                if (clinicTree.SelectedNode != null && clinicTree.SelectedNode.Tag is Department) {
                    cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Physician), 
                        new Filter("_department", FilterCondition.EqualRef, clinicTree.SelectedNode.Tag));
                    if (!cur.Eof) {
                        throw new Exception(Properties.Resources.ErrorDeleteEntity);
                    }
                }
                else if (clinicTree.SelectedNode != null && clinicTree.SelectedNode.Tag is Physician) {
                    cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Examination),
                        new Filter("_physician", FilterCondition.EqualRef, clinicTree.SelectedNode.Tag));
                    if (!cur.Eof) {
                        throw new Exception(Properties.Resources.ErrorDeleteEntity);
                    }
                }

                Program.Database.EntityStorage.BeginTrans();
                Program.Database.EntityStorage.Delete(clinicTree.SelectedNode.Tag as Entity);
                Program.Database.EntityStorage.CommitTrans();

                clinicTree.SelectedNode.Tag = null;
                clinicTree.Nodes.Remove(clinicTree.SelectedNode);
            }
            catch (Exception exc) {
                Program.ShowErrorMessage(exc.Message);
            }
        }
    }
}