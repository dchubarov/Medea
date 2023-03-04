// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ExaminationView.cs
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
using System.Data;
using System.Text;
using System.Windows.Forms;
// medea.data
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Gui
{
    /// <summary>
    /// Provides user interface for Examination business object.
    /// </summary>
    public partial class ExaminationView : UserControl
    {
        #region Public Nested Types

        public class Factory : IViewFactory
        {
            #region Private Fields

            private ExaminationView _control = null;
            private IIdentity _exmId = null;

            #endregion

            #region Public Instance Constructors

            public Factory()
            {
            }

            public Factory(IIdentity identity)
            {
                _exmId = identity;
            }

            #endregion

            #region Public Instance Properties

            public IIdentity Identity
            {
                get
                {
                    return _exmId;
                }

                set
                {
                    _exmId = value;
                }
            }

            #endregion

            #region IViewFactory Members

            public bool HandleKeyPress(KeyPressEventArgs e)
            {
                return false;
            }

            public void CreateControl()
            {
                if (_control == null) {
                    _control = new ExaminationView();
                }
            }

            public void DestroyControl()
            {
                if (_control != null) {
                    // gather control state
                    if (_control.Examination != null) {
                        _exmId = Program.Database.EntityStorage.IdentityOf(_control.Examination);
                    }

                    // destroy control
                    _control.Dispose();
                    _control = null;
                }
            }

            public void ActivateControl()
            {
                if (_control != null) {
                    _control.SetData(_exmId);
                }
            }

            public bool CanClose(CloseReason reason)
            {
                if (_control != null && _control.Modified) {
                    DialogResult answer = MessageBox.Show(
                        Properties.Resources.ConfirmSaveExamination, 
                        Program.Title, 
                        MessageBoxButtons.YesNoCancel, 
                        MessageBoxIcon.Question
                        );

                    if (answer == DialogResult.Cancel)
                        return false;
                    else {
                        if (answer == DialogResult.Yes) {
                            _control.SaveExamination();
                        }
                    }
                }
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
                    return Properties.Resources.ViewExamination;
                }
            }

            public string Detail
            {
                get
                {
                    return (_control != null && _control.Examination != null) ? 
                        _control.Examination.GetDisplayName() : String.Empty;
                }
            }

            public ToolStrip ToolStrip
            {
                get
                {
                    return (_control != null) ? _control.exmTools : null;
                }
            }

            public MenuStrip MenuStrip
            {
                get
                {
                    return (_control != null) ? _control.exmMenu : null;
                }
            }

            #endregion
        }

        #endregion

        #region Private Nested Types

        private class NodeComparer : IComparer
        {
            #region Private Methods

            private static long GetNodeCompareValue(object n)
            {
                long t = 0;
                TreeNode node;

                if ((node = n as TreeNode) != null && node.Tag != null) {
                    if (node.Tag is IndicationCategory) {
                        t = (node.Tag as IndicationCategory).CreateDate.Ticks;
                    }
                    else if (node.Tag is IndicationDefinition) {
                        t = (node.Tag as IndicationDefinition).CreateDate.Ticks;
                    }
                }

                return t;
            }

            #endregion

            #region IComparer Members

            public int Compare(object x, object y)
            {
                long vx = GetNodeCompareValue(x);
                long vy = GetNodeCompareValue(y);
                return (vx == vy) ? 0 : (vx < vy) ? -1 : 1;
            }

            #endregion
        }

        [Flags]
        private enum IndicationEditFlags
        {
            None = 0,
            Persistent = 1,
            Delete = 8
        }

        private class IndicationEditState
        {
            #region Private Fields

            private Indication _ind;
            private IndicationEditFlags _flags;

            #endregion

            #region Construction

            public IndicationEditState(Indication ind) : this(ind, IndicationEditFlags.None)
            {
            }

            public IndicationEditState(Indication ind, IndicationEditFlags state)
            {
                _ind = ind;
                _flags = state;
            }

            #endregion

            #region Public Properties

            public Indication Indication
            {
                get
                {
                    return _ind;
                }
            }

            public IndicationEditFlags Flags
            {
                get
                {
                    return _flags;
                }

                set
                {
                    _flags = value;
                }
            }

            #endregion
        }

        #endregion

        #region Private Instance Fields

        private bool _modified = false;
        private bool _persist = false;
        private bool _loadingTree = false;
        private Font _boldNodeFont = null;
        private Font _undrNodeFont = null;
        private Examination _examination = null;
        private Dictionary<IndicationDefinition, IndicationEditState> _indications =
            new Dictionary<IndicationDefinition, IndicationEditState>();
        private Dictionary<IndicationCategory, int> _activeCategories =
            new Dictionary<IndicationCategory, int>();
        private Indication _indicationCmtEdit = null;

        #endregion

        #region Public Instance Constructors

        /// <summary>
        /// Constructs new ExaminationControl object.
        /// </summary>
        public ExaminationView()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Instance Properties

        /// <summary>
        /// Gets a value indicating that data was modified.
        /// </summary>
        private bool Modified
        {
            get
            {
                return _modified;
            }

            set
            {
                if (this.Created) {
                    saveCmdButton.Enabled = value;
                }
                _modified = value;
            }
        }

        private bool Persist
        {
            get
            {
                return _persist;
            }

            set
            {
                if (this.Created) {
                    snapshotsCmdButton.Enabled = value;
                    printCmdButton.Enabled = value;
                }
                _persist = value;
            }
        }

        /// <summary>
        /// Gets an underlying business object.
        /// </summary>
        private Examination Examination
        {
            get
            {
                return _examination;
            }
        }

        #endregion

        #region Private Instance Methods

        private void SetData(IIdentity identity)
        {
            // create new blank examination object
            if (identity == null || identity.IsNull) {
                _examination = new Examination();
                this.Persist = false;
            }
            else {
                // retrieve specified entity from database
                Entity ent = Program.Database.EntityStorage.Retrieve(identity);
                bool invalidObject = false;

                if (ent == null)
                    invalidObject = true;
                else {
                    // identity specifies existing Examination object
                    if (ent is Examination) {
                        _examination = ent as Examination;
                        this.LoadIndications();
                        this.Persist = true;
                    }
                    // create a new examination based on existing ExaminationSchedule object
                    else if (ent is ExaminationSchedule) {
                        ExaminationSchedule exs = ent as ExaminationSchedule;
                        _examination = new Examination(exs.ExaminationKind, exs.Patient, exs.Physician);
                    }
                    // create a new examination based on existing Patient object
                    else if (ent is Patient) {
                        Patient ptn = ent as Patient;
                        _examination = new Examination(null, ptn, null);
                    }
                    else
                        invalidObject = true;
                }

                // the specified object cannot be used to construct examination
                if (invalidObject) {
                    throw new Exception("The specified object is invalid.");
                }
            }

            _examination.DataChange += new Entity.DataChangeEvent(Examination_DataChange);

            patientEdit.Text = (_examination.Patient != null) ? _examination.Patient.GetDisplayName() : String.Empty;
            physicianEdit.Text = (_examination.Physician != null) ? _examination.Physician.GetDisplayName() : String.Empty;
            exmKindEdit.Text = (_examination.Kind != null) ? _examination.Kind.GetDisplayName() : String.Empty;
            conclusionEdit.Text = _examination.Conclusion;
            performDatePicker.Value = _examination.PerformDate;

            this.PopulateTree();
            this.Modified = false;
        }

        private void ClearIndications()
        {
            if (_indications.Count > 0) {
                Program.Database.EntityStorage.BeginTrans();
                try {
                    foreach (IndicationEditState a in _indications.Values) {
                        if ((a.Flags & IndicationEditFlags.Persistent) != 0) {
                            Program.Database.EntityStorage.Delete(a.Indication);
                            a.Flags = IndicationEditFlags.None;
                        }
                    }
                    Program.Database.EntityStorage.CommitTrans();
                    _indications.Clear();
                }
                catch (Exception) {
                    Program.Database.EntityStorage.RollbackTrans();
                    throw;
                }
            }
        }

        private void LoadIndications()
        {
            this.ClearIndications();
            Filter examinationFilter = new Filter("_examination", FilterCondition.EqualRef, this.Examination);
            ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Indication), examinationFilter);
            while (!cur.Eof) {
                Indication indication = (Indication)cur.Current;
                _indications.Add(indication.Definition, new IndicationEditState(indication, IndicationEditFlags.Persistent));
                cur.MoveNext();
            }
        }

        private void SaveExamination()
        {
            Program.StatusManager.SetStatusText(Properties.Resources.StatusSavingData);
            Program.StatusManager.SetWaitCursor();

            Program.Database.EntityStorage.BeginTrans();
            try {
                if (this.Examination.Kind == null || this.Examination.Patient == null) {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Properties.Resources.ErrorExaminationValidation);
                    if (this.Examination.Kind == null)
                        sb.AppendLine(" * " + Properties.Resources.ErrorExaminationValidationKind);
                    if (this.Examination.Patient == null)
                        sb.AppendLine(" * " + Properties.Resources.ErrorExaminationValidationPatient);
                    if (this.Examination.Physician == null)
                        sb.AppendLine(" * " + Properties.Resources.ErrorExaminationValidationPhysician);

                    throw new ApplicationException(sb.ToString());
                }

                // store examination business object itself to database
                Program.Database.EntityStorage.Store(_examination);

                // create new dictionary for alive indications
                Dictionary<IndicationDefinition, IndicationEditState> dictNew = 
                    new Dictionary<IndicationDefinition, IndicationEditState>();

                // store selected indication(s) to database
                foreach (IndicationEditState assoc in _indications.Values) {
                    if ((assoc.Flags & IndicationEditFlags.Delete) != 0) {
                        if ((assoc.Flags & IndicationEditFlags.Persistent) != 0) {
                            Program.Database.EntityStorage.Delete(assoc.Indication);
                        }
                        continue;
                    }

                    Program.Database.EntityStorage.Store(assoc.Indication);
                    assoc.Flags = IndicationEditFlags.Persistent;
                    dictNew.Add(assoc.Indication.Definition, assoc);
                }

                Program.Database.EntityStorage.CommitTrans();
                _indications = dictNew;
                this.Persist = true;
            }
            catch (Exception) {
                Program.Database.EntityStorage.RollbackTrans();
                throw;
            }
        }

        private void SetExaminationKind(ExaminationKind kind)
        {
            this.ClearIndications();
            _examination.Kind = kind;
            this.PopulateTree();

            Program.ViewManager.GetCurrentView(this).UpdateCaption();
        }

        private void PopulateTree()
        {
            // clear active categories
            _activeCategories.Clear();
            _loadingTree = true;

            // clear tree
            exmTree.BeginUpdate();
            try {
                exmTree.Nodes.Clear();
            }
            finally {
                exmTree.EndUpdate();
            }
            exmTree.Refresh();

            if (_examination != null && _examination.Kind != null) {
                Program.StatusManager.SetStatusText(Properties.Resources.StatusLoadingData);
                Program.StatusManager.SetWaitCursor();
                exmTree.BeginUpdate();

                try {
                    Filter complexFilter = new Filter(), categoryFilter;
                    TreeNode node;

                    // add filter by examination kind
                    complexFilter.Filters.Add(new Filter("_examinationKind",
                        FilterCondition.EqualRef, _examination.Kind));

                    // add filter by category
                    categoryFilter = new Filter("_parentCategory", FilterCondition.EqualRef, null);
                    complexFilter.Filters.Add(categoryFilter);

                    // create root node
                    node = new TreeNode(_examination.Kind.Name);
                    node.Tag = _examination.Kind;

                    exmTree.Nodes.Add(node);
                    this.SetupTreeNode(node);

                    // populate tree recursively
                    this.PopulateSubTree(node, complexFilter, categoryFilter);
                    node.Expand();
                }
                finally {
                    exmTree.EndUpdate();
                    _loadingTree = false;
                }
            }
        }

        private void PopulateSubTree(TreeNode parentNode, Filter complexFilter, Filter categoryFilter)
        {
            TreeNode node;
            ICursor cur;

            if (parentNode != null) {
                categoryFilter.Field = "_parentCategory";
                categoryFilter.Criteria = (parentNode.Tag is IndicationCategory) ? parentNode.Tag : null;
                cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(IndicationCategory), complexFilter);
                while (!cur.Eof) {
                    IndicationCategory cat = (IndicationCategory)cur.Current;
                    node = new TreeNode(cat.Name);
                    node.Tag = cat;
                    parentNode.Nodes.Add(node);
                    SetupTreeNode(node);
                    PopulateSubTree(node, complexFilter, categoryFilter);
                    cur.MoveNext();
                }

                if (parentNode.Tag is IndicationCategory) {
                    categoryFilter.Field = "_category";
                    categoryFilter.Criteria = parentNode.Tag;
                    cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(IndicationDefinition), complexFilter);
                    while (!cur.Eof) {
                        IndicationDefinition idf = (IndicationDefinition)cur.Current;
                        node = new TreeNode(idf.Name);
                        node.Tag = idf;
                        parentNode.Nodes.Add(node);
                        SetupTreeNode(node);
                        cur.MoveNext();
                    }
                }
            }
        }

        private bool CheckCategoryUsage(IndicationCategory idc)
        {
            if (idc == null) throw new ArgumentNullException("idc");
            Filter filter = new Filter("_category", FilterCondition.EqualRef, idc);
            ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(IndicationDefinition), filter);
            while (!cur.Eof) {
                if (CheckIndicationUsage((IndicationDefinition)cur.Current)) return true;
                cur.MoveNext();
            }
            return false;
        }

        private bool CheckIndicationUsage(IndicationDefinition idf)
        {
            if (idf == null) throw new ArgumentNullException("idf");
            Filter filter = new Filter("_definition", FilterCondition.EqualRef, idf);
            ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(Indication), filter);
            return (cur.Eof ? false : true);
        }

        private void DeleteCategory(IndicationCategory idc)
        {
            if (idc == null) {
                throw new ArgumentNullException("idc");
            }

            try {
                Program.Database.EntityStorage.BeginTrans();
                RecursivelyDeleteCategory(idc);
                Program.Database.EntityStorage.CommitTrans();
            }
            catch (Exception) {
                Program.Database.EntityStorage.RollbackTrans();
                throw;
            }
        }

        private void RecursivelyDeleteCategory(IndicationCategory idc)
        {
            Filter filter = new Filter("_parentCategory", FilterCondition.EqualRef, idc);
            ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(IndicationCategory), filter);
            while (!cur.Eof) {
                RecursivelyDeleteCategory((IndicationCategory)cur.Current);
                cur.MoveNext();
            }

            filter.Field = "_category";
            cur = Program.Database.EntityStorage.RetrieveMultiple(typeof(IndicationDefinition), filter);
            while (!cur.Eof) {
                DeleteIndicationDefinition(cur.Current as IndicationDefinition, true);
                cur.MoveNext();
            }

            Program.Database.EntityStorage.Delete(idc);
        }

        private void DeleteIndicationDefinition(IndicationDefinition idf)
        {
            DeleteIndicationDefinition(idf, false);
        }

        private void DeleteIndicationDefinition(IndicationDefinition idf, bool disableTransaction)
        {
            if (CheckIndicationUsage(idf)) {
                throw new Exception(String.Format(Properties.Resources.ErrorIndicationUsed, idf.GetDisplayName()));
            }

            if (!disableTransaction) Program.Database.EntityStorage.BeginTrans();
            Program.Database.EntityStorage.Delete(idf);
            if (!disableTransaction) Program.Database.EntityStorage.CommitTrans();
        }

        private void AddCategory(TreeNode parentNode)
        {
            // require parent node
            if (parentNode == null) {
                throw new ArgumentNullException("parentNode");
            }

            // require valid examination object
            if (_examination == null || _examination.Kind == null) {
                throw new Exception("Could not determine examination kind.");
            }

            // create new indication category
            IndicationCategory category = new IndicationCategory(
                Properties.Resources.NewIndicationCategory,
                this.Examination.Kind
                );

            // get a parent category
            IndicationCategory parentCategory = (parentNode.Tag is IndicationCategory) ? 
                (IndicationCategory)parentNode.Tag : null;

#if DEBUG
            if (parentCategory != null && ((IndicationCategory)parentNode.Tag).ExaminationKind != _examination.Kind) {
                throw new Exception("[DEBUG] Category belongs to another examination kind.");
            }
#endif

            // assign parentCategory of the current category
            category.ParentCategory = parentCategory;

            // save new category to database
            Program.Database.EntityStorage.BeginTrans();
            Program.Database.EntityStorage.Store(category);
            Program.Database.EntityStorage.CommitTrans();

            // create tree node for new category
            TreeNode categoryNode = parentNode.Nodes.Add(category.Name);
            categoryNode.Tag = category;
            SetupTreeNode(categoryNode);
            parentNode.Expand();

            // select new category node and allow user to change default name
            categoryNode.TreeView.SelectedNode = categoryNode;
            categoryNode.BeginEdit();
        }

        private void AddIndicationDefinition(TreeNode parentNode)
        {
            // require parent node
            if (parentNode == null) {
                throw new ArgumentNullException("parentNode");
            }

            // require valid examination object
            if (_examination == null || _examination.Kind == null) {
                throw new Exception("Could not determine examination kind.");
            }

            // create new indication definition
            IndicationDefinition idf = new IndicationDefinition(
                Properties.Resources.NewIndicationDefinition,
                this.Examination.Kind
                );

            // get a parent category
            IndicationCategory parentCategory = (parentNode.Tag is IndicationCategory) ?
                (IndicationCategory)parentNode.Tag : null;

#if DEBUG
            if (parentCategory != null && ((IndicationCategory)parentNode.Tag).ExaminationKind != _examination.Kind) {
                throw new Exception("[DEBUG] Category belongs to another examination kind.");
            }
#endif

            // assign parentCategory to the new definition
            idf.Category = parentCategory;

            // store new definition to database
            Program.Database.EntityStorage.BeginTrans();
            Program.Database.EntityStorage.Store(idf);
            Program.Database.EntityStorage.CommitTrans();

            // create a tree node for new definition
            TreeNode idfNode = parentNode.Nodes.Add(idf.Name);
            idfNode.Tag = idf;
            SetupTreeNode(idfNode);
            parentNode.Expand();

            // select new definition node and allow user to change default name
            idfNode.TreeView.SelectedNode = idfNode;
            idfNode.BeginEdit();
        }

        private void IncludeIndication(IndicationDefinition idf)
        {
            if (_indications.ContainsKey(idf)) {
                _indications[idf].Flags &= ~IndicationEditFlags.Delete;
            } 
            else {
                Indication ind = new Indication(idf, this.Examination);
                _indications.Add(idf, new IndicationEditState(ind));
            }

            SetupTreeNode(exmTree.SelectedNode);
            this.Modified = true;
        }

        private void ExcludeIndication(IndicationDefinition idf)
        {
            if (_indications.ContainsKey(idf)) {
                if ((_indications[idf].Flags & IndicationEditFlags.Persistent) != 0) {
                    _indications[idf].Flags |= IndicationEditFlags.Delete;
                }
                else {
                    _indications.Remove(idf);
                }

                SetupTreeNode(exmTree.SelectedNode);
                this.Modified = true;
            }
        }

        private void ToggleIndication(IndicationDefinition idf)
        {
            if (!_indications.ContainsKey(idf) || (_indications[idf].Flags & IndicationEditFlags.Delete) != 0)
                this.IncludeIndication(idf);
            else
                this.ExcludeIndication(idf);
        }

        private void SetupTreeNode(TreeNode node)
        {
            if (node.Tag != null) {
                if (node.Tag is ExaminationKind) {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;
                    node.NodeFont = _boldNodeFont;
                }
                else if (node.Tag is IndicationCategory) {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;

                    if (_activeCategories.ContainsKey((IndicationCategory)node.Tag) &&
                        _activeCategories[(IndicationCategory)node.Tag] > 0) {
                        node.NodeFont = _undrNodeFont;
                        node.Expand();
                    }
                    else {
                        node.NodeFont = exmTree.Font;
                    }
                }
                else if (node.Tag is IndicationDefinition) {
                    bool found = false;
                    if (_indications.ContainsKey((IndicationDefinition)node.Tag) &&
                        (_indications[(IndicationDefinition)node.Tag].Flags & IndicationEditFlags.Delete) == 0) {
                        found = true;
                    }
                    node.ImageIndex = found ? 3 : 2;
                    node.SelectedImageIndex = found ? 3 : 2;

                    TreeNode parentNode = node.Parent;
                    do {
                        if (!(parentNode.Tag is IndicationCategory))
                            parentNode = null;
                        else {
                            IndicationCategory idc = (IndicationCategory)parentNode.Tag;
                            if (found) {
                                if (!_activeCategories.ContainsKey(idc))
                                    _activeCategories.Add(idc, 1);
                                else
                                    _activeCategories[idc]++;
                            }
                            else {
                                if (_activeCategories.ContainsKey(idc) && !_loadingTree) {
                                    if (--_activeCategories[idc] == 0)
                                        _activeCategories.Remove(idc);
                                }
                            }
                            this.SetupTreeNode(parentNode);
                            parentNode = parentNode.Parent;
                        }
                    } while (parentNode != null);
                }
                else {
                    throw new Exception("Tree node associated with invalid object.");
                }
            }
        }

        private List<string> GetTreeAsStrings()
        {
            List<string> ts = new List<string>();
            TreeNode node = exmTree.Nodes[0].FirstNode;
            bool skipToNextNode = false;
            String nodeText;
            int level = 0;

            while (node != null) {
                nodeText = String.Empty;

                if (!skipToNextNode) {
                    if (node.Tag is IndicationCategory && _activeCategories.ContainsKey((IndicationCategory)node.Tag))
                        nodeText = (node.Tag as IndicationCategory).GetDisplayName();
                    else if (node.Tag is IndicationDefinition && _indications.ContainsKey((IndicationDefinition)node.Tag)) {
                        IndicationEditState ies = _indications[(IndicationDefinition)node.Tag];
                        if ((ies.Flags & IndicationEditFlags.Delete) == 0) {
                            nodeText = "* " + (node.Tag as IndicationDefinition).GetDisplayName();
                            Indication ind = ies.Indication;
                            if (!String.IsNullOrEmpty(ind.Comment))
                                nodeText += " [" + ind.Comment + "]";
                        }
                    }

                    if (!String.IsNullOrEmpty(nodeText)) {
                        nodeText = nodeText.PadLeft(nodeText.Length + level * 4, ' ');
                        ts.Add(nodeText);
                    }
                }

                if (node.FirstNode != null && !skipToNextNode) {
                    node = node.FirstNode;
                    ++level;
                }
                else {
                    skipToNextNode = false;
                    if (node.NextNode != null)
                        node = node.NextNode;
                    else {
                        if (level < 1)
                            node = null;
                        else {
                            node = node.Parent;
                            skipToNextNode = true;
                            --level;
                        }
                    }
                }
            }

            return ts;
        }

        #endregion

        #region ExaminationView Event Handlers (Control)

        private void ExaminationControl_Load(object sender, EventArgs e)
        {
            _boldNodeFont = new Font(exmTree.Font, FontStyle.Bold);
            _undrNodeFont = new Font(exmTree.Font, FontStyle.Underline);

            // set comparer object to sort tree view nodes
            exmTree.TreeViewNodeSorter = new NodeComparer();
        }

        private void exmTree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null || e.Node.Tag is ExaminationKind) {
                e.CancelEdit = true;
            }
        }

        private void exmTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = true;

            if (e.Label != null) {
                if (e.Label.Trim() == "") {
                    MessageBox.Show(Properties.Resources.ErrorEmptyName, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } 
                else {
                    if (e.Node.Tag is IndicationCategory || e.Node.Tag is IndicationDefinition) {
                        NamedEntity ent = (NamedEntity)e.Node.Tag;
                        ent.Name = e.Label.Trim();

                        try {
                            Program.Database.EntityStorage.BeginTrans();
                            Program.Database.EntityStorage.Store(ent);
                            Program.Database.EntityStorage.CommitTrans();
                            e.CancelEdit = false;
                        }
                        catch (Exception exc) {
                            MessageBox.Show(exc.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }
            }
        }

        private void exmTree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                exmTree.SelectedNode = exmTree.GetNodeAt(e.X, e.Y);
            }
        }

        private void exmTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is IndicationDefinition) {
                this.ToggleIndication((IndicationDefinition)e.Node.Tag);
                exmTree_AfterSelect(null, null);
            }
        }

        private void exmTree_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None) {
                if (e.KeyCode == Keys.F2) {
                    renameNodeCmd.PerformClick();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F5) {
                    refreshTreeCmd.PerformClick();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Space) {
                    if (exmTree.SelectedNode != null && exmTree.SelectedNode.Tag is IndicationDefinition) {
                        this.ToggleIndication(exmTree.SelectedNode.Tag as IndicationDefinition);
                        exmTree_AfterSelect(null, null);
                        e.Handled = true;
                    }
                }
            }
        }

        private void exmMenu_Opening(object sender, CancelEventArgs e)
        {
            addCategoryCmd.Enabled = false;
            addIndicationDefCmd.Enabled = false;

            indicationSelectCmd.Enabled = false;
            indicationUnselectCmd.Enabled = false;
            indicationEditCommentCmd.Enabled = false;
            indicationClearCommentCmd.Enabled = false;

            renameNodeCmd.Enabled = false;
            deleteNodeCmd.Enabled = false;
            refreshTreeCmd.Enabled = false;

            if (_examination.Kind != null) {
                if (exmTree.SelectedNode != null && exmTree.SelectedNode.Tag != null) {
                    if (exmTree.SelectedNode.Tag is ExaminationKind ||
                        exmTree.SelectedNode.Tag is IndicationCategory) {
                        addCategoryCmd.Enabled = true;

                        if (exmTree.SelectedNode.Tag is IndicationCategory) {
                            addIndicationDefCmd.Enabled = true;
                        }
                    } else if (exmTree.SelectedNode.Tag is IndicationDefinition) {
                        IndicationDefinition idf = (IndicationDefinition)exmTree.SelectedNode.Tag;
                        if (!_indications.ContainsKey(idf) || (_indications[idf].Flags & IndicationEditFlags.Delete) != 0)
                            indicationSelectCmd.Enabled = true;
                        else {
                            indicationUnselectCmd.Enabled = true;
                            indicationEditCommentCmd.Enabled = true;
                            indicationClearCommentCmd.Enabled = true;
                        }
                    }

                    if (!(exmTree.SelectedNode.Tag is ExaminationKind)) {
                        renameNodeCmd.Enabled = true;
                        deleteNodeCmd.Enabled = true;
                    }
                }

                refreshTreeCmd.Enabled = true;
            }
        }

        private void exmKindLookupButton_Click(object sender, EventArgs e)
        {
            RollDialog dlg = new RollDialog();
            dlg.RollName = "ExaminationKind_";

            if (dlg.ShowDialog() == DialogResult.OK) {
                ExaminationKind exk = dlg.SelectedEntity as ExaminationKind;
                if (exk != null) {
                    this.SetExaminationKind(exk);
                    exmKindEdit.Text = exk.GetDisplayName();
                }
            }
        }

        private void patientLookupBtn_Click(object sender, EventArgs e)
        {
            RollDialog dlg = new RollDialog();
            dlg.RollName = "Patient";

            if (dlg.ShowDialog() == DialogResult.OK) {
                Patient pat = dlg.SelectedEntity as Patient;
                if (pat != null) {
                    _examination.Patient = pat;
                    patientEdit.Text = pat.GetDisplayName();
                }
            }
        }

        private void physicianLookupBtn_Click(object sender, EventArgs e)
        {
            RollDialog dlg = new RollDialog();
            dlg.RollName = "Physician";

            if (dlg.ShowDialog() == DialogResult.OK) {
                Physician phy = dlg.SelectedEntity as Physician;
                if (phy != null) {
                    _examination.Physician = phy;
                    physicianEdit.Text = phy.GetDisplayName();
                }
            }
        }

        private void conclusionEdit_TextChanged(object sender, EventArgs e)
        {
            _examination.Conclusion = conclusionEdit.Text;
        }

        private void exmTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _indicationCmtEdit = null;

            indCmt.Enabled = false;
            indCmt.Text = String.Empty;

            if (exmTree.SelectedNode != null && exmTree.SelectedNode.Tag is IndicationDefinition) {
                IndicationDefinition idf = exmTree.SelectedNode.Tag as IndicationDefinition;
                if (_indications.ContainsKey(idf)) {
                    if ((_indications[idf].Flags & IndicationEditFlags.Delete) == 0) {
                        _indicationCmtEdit = _indications[idf].Indication;
                        indCmt.Text = _indicationCmtEdit.Comment;
                        indCmt.Enabled = true;
                    }
                }
            }
        }

        private void indCmt_Enter(object sender, EventArgs e)
        {

        }

        private void indCmt_Leave(object sender, EventArgs e)
        {
            if (_indicationCmtEdit != null) {
                String tmp = indCmt.Text.Trim();
                _indicationCmtEdit.Comment = tmp;
                this.Modified = true;
            }
        }

        private void indCmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
                exmTree.Focus();
            }
        }

        private void performDatePicker_ValueChanged(object sender, EventArgs e)
        {
            _examination.PerformDate = performDatePicker.Value;
        }

        #endregion

        #region ExaminationView Event Handler (Commands)

        private void saveCmd_Click(object sender, EventArgs e)
        {
            try {
                this.SaveExamination();
                this.Modified = false;
            }
            catch (Exception exc) {
                string errorMsg = String.Format("{0}\n{1}",
                    Properties.Resources.ErrorSaveData, exc.Message);
                MessageBox.Show(errorMsg, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void snapshotsCmd_Click(object sender, EventArgs e)
        {
            Program.ViewManager.GetCurrentView(this).Host(
                new ImagingView.Factory(Program.Database.EntityStorage.IdentityOf(this.Examination))
                );
        }

        private void addCategoryCmd_Click(object sender, EventArgs e)
        {
            if (exmTree.SelectedNode != null && (exmTree.SelectedNode.Tag is ExaminationKind || 
                exmTree.SelectedNode.Tag is IndicationCategory)) {
                try {
                    this.AddCategory(exmTree.SelectedNode);
                }
                catch (Exception exc) {
                    string errorMsg = String.Format("{0}\n{1}", 
                        Properties.Resources.ErrorCreateIndicationCategory, exc.Message);
                    MessageBox.Show(errorMsg, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void addIndicationDefCmd_Click(object sender, EventArgs e)
        {
            if (exmTree.SelectedNode != null && (exmTree.SelectedNode.Tag is IndicationCategory)) {
                try {
                    this.AddIndicationDefinition(exmTree.SelectedNode);
                }
                catch(Exception exc) {
                    string errorMsg = String.Format("{0}\n{1}", 
                        Properties.Resources.ErrorCreateIndicationDefinition, exc.Message);
                    MessageBox.Show(errorMsg, null, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void indicationSelectCmd_Click(object sender, EventArgs e)
        {
            if (exmTree.SelectedNode != null && exmTree.SelectedNode.Tag is IndicationDefinition) {
                this.IncludeIndication((IndicationDefinition)exmTree.SelectedNode.Tag);
                exmTree_AfterSelect(null, null);
            }
        }

        private void indicationUnselectCmd_Click(object sender, EventArgs e)
        {
            if (exmTree.SelectedNode != null && exmTree.SelectedNode.Tag is IndicationDefinition) {
                this.ExcludeIndication((IndicationDefinition)exmTree.SelectedNode.Tag);
                exmTree_AfterSelect(null, null);
            }
        }

        private void renameNodeCmd_Click(object sender, EventArgs e)
        {
            if (exmTree.SelectedNode != null && (exmTree.SelectedNode.Tag is IndicationCategory || 
                exmTree.SelectedNode.Tag is IndicationDefinition)) {
                exmTree.SelectedNode.BeginEdit();
            }
        }

        private void deleteNodeCmd_Click(object sender, EventArgs e)
        {
            if (exmTree.SelectedNode != null && (exmTree.SelectedNode.Tag is IndicationCategory || 
                exmTree.SelectedNode.Tag is IndicationDefinition)) {

                string confirmMessage = String.Format(
                    exmTree.SelectedNode.Tag is IndicationDefinition ?
                    Properties.Resources.ConfirmDeleteItem : Properties.Resources.ConfirmDeleteTree,
                    exmTree.SelectedNode.Text);

                if (MessageBox.Show(confirmMessage, Program.Title, 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    Program.StatusManager.SetStatusText(Properties.Resources.StatusDeleting);
                    Program.StatusManager.SetWaitCursor();
                    try {
                        if (exmTree.SelectedNode.Tag is IndicationDefinition) {
                            DeleteIndicationDefinition(exmTree.SelectedNode.Tag as IndicationDefinition);
                        }
                        else {
                            DeleteCategory(exmTree.SelectedNode.Tag as IndicationCategory);
                        }
                        exmTree.SelectedNode.Tag = null;
                        exmTree.Nodes.Remove(exmTree.SelectedNode);
                    }
                    catch (Exception exc) {
                        Program.ShowErrorMessage(exc.Message, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void refreshTreeCmd_Click(object sender, EventArgs e)
        {
            this.PopulateTree();
        }

        private void printCmdButton_Click(object sender, EventArgs e)
        {
            PrintExaminationDialog dlg = new PrintExaminationDialog();
            try {
                Program.StatusManager.SetStatusText(Properties.Resources.StatusCreateReport);
                Program.StatusManager.SetWaitCursor();

                List<string> ts = GetTreeAsStrings();

                dlg.CreateReport(_examination, ts);
                dlg.ShowDialog();
            }
            catch (Exception ex) {
                Program.ShowErrorMessage(ex.Message, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region ExaminationView Event Handlers (Business Object)

        private void Examination_DataChange(object sender)
        {
            if (Object.ReferenceEquals(sender, _examination)) {
                this.Modified = true;
            }
        }

        #endregion
    }
}
