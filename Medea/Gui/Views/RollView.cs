// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      RollView.cs
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
using Gyrosoft.Medea.Config;
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Gui
{
    /// <summary>
    /// Provides user interface for list of business object.
    /// </summary>
    public partial class RollView : UserControl
    {
        #region Public Types

        public class Factory : IViewFactory
        {
            #region Private Instance Fields

            private RollView _control = null;
            private string _rollName = null;
            private string _itemFactory = null;
            private Filter _filter = null;

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

            public string ItemFactory
            {
                get
                {
                    return _itemFactory;
                }

                set
                {
                    _itemFactory = value;
                }
            }

            public Filter Filter
            {
                get
                {
                    return _filter;
                }

                set
                {
                    _filter = value;
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
                    _control = new RollView();
                }
            }

            public void DestroyControl()
            {
                if (_control != null) {
                    _control.Dispose();
                    _control = null;
                }
            }

            public void ActivateControl()
            {
                // *** TEMP !!!
                if (String.IsNullOrEmpty(_rollName)) {
                    _rollName = "Patient";
                    _itemFactory = "PatientViewFactory";
                    _filter = new Filter();
                    _filter.Filters.Add(new Filter("_familyName", FilterCondition.OrderAscending, null));
                }
                // *** END TEMP

                if (_control != null) {
                    _control.LoadData(_rollName, _itemFactory, _filter);
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
                    return Properties.Resources.ViewRoll;
                }
            }

            public string Detail
            {
                get
                {
                    return (_control != null && _control._rollConfig != null) ? 
                        _control._rollConfig.DisplayName : String.Empty;
                }
            }

            public ToolStrip ToolStrip
            {
                get
                {
                    return (_control != null ? _control.rollTools : null);
                }
            }

            public MenuStrip MenuStrip
            {
                get
                {
                    return (_control != null ? _control.rollMenu : null);
                }
            }

            #endregion
        }

        #endregion

        #region Private Fields

        private IRollConfig _rollConfig = null;
        private string _itemFactory = null;

        #endregion

        #region Construction

        public RollView()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Instance Methods

        private void LoadData(string rollName, string itemFactory, Filter filter)
        {
            _itemFactory = itemFactory;
            _rollConfig = Program.ConfigManager.GetRollConfig(rollName);
            if (_rollConfig == null) {
                throw new Exception();
            }

            ConfigManagerBase.InitGridView(rollGrid, _rollConfig, filter);
        }

        private void OpenItem(IIdentity id)
        {
            if (!String.IsNullOrEmpty(_itemFactory)) {
                Dictionary<string,object> factoryParams = null;
                if (id != null && !id.IsNull) {
                    factoryParams = new Dictionary<string,object>();
                    factoryParams.Add("Identity",id);
                }

                IViewFactory factory = Program.ConfigManager.GetViewFactory(_itemFactory, factoryParams);
                if (factory != null) {
                    Program.ViewManager.GetCurrentView(this).Host(factory);
                }
            }
        }

        #endregion

        #region RollView Event Handlers (Control)

        private void rollGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is Entity) {
                e.Value = (e.Value as Entity).GetDisplayName();
                e.FormattingApplied = true;
            }
        }

        private void rollGrid_DoubleClick(object sender, EventArgs e)
        {
            editRecordCmd_Click(null, null);
        }

        #endregion

        #region RollView Event Handlers (Commands)

        private void addRecordCmd_Click(object sender, EventArgs e)
        {
            this.OpenItem(null);
        }

        private void editRecordCmd_Click(object sender, EventArgs e)
        {
            if (rollGrid.SelectedRows.Count > 0) {
                Entity ent = rollGrid.SelectedRows[0].DataBoundItem as Entity;
                if (ent == null)
                    throw new Exception("");
                else {
                    IIdentity id = Program.Database.EntityStorage.IdentityOf(ent);
                    if (id == null || id.IsNull) {
                        throw new Exception("");
                    }

                    this.OpenItem(id);
                }
            }
        }

        #endregion
    }
}
