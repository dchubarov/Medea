using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Gui
{
    public partial class PatientView : UserControl
    {
        #region Public Nested Types

        public class Factory : IViewFactory
        {
            #region Private Instance Fields

            private PatientView _control = null;
            private IIdentity _patientId = null;

            #endregion

            #region Public Instance Constructors

            public Factory()
            {
            }

            #endregion

            #region Public Instance Properties

            public IIdentity Identity
            {
                get
                {
                    return _patientId;
                }

                set
                {
                    _patientId = value;
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
                    _control = new PatientView();
                }
            }

            public void DestroyControl()
            {
                if (_control != null) {
                    _control.Unbind();
                    _control.Dispose();
                    _control = null;
                }
            }

            public void ActivateControl()
            {
                if (_control != null) {
                    _control.Bind(_patientId);
                }
            }

            public bool CanClose(CloseReason reason)
            {
                if (_control != null && _control.Modified) {
                    DialogResult answer = MessageBox.Show(
                        Properties.Resources.ConfirmSavePatient,
                        Program.Title,
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question
                        );

                    if (answer == DialogResult.Cancel)
                        return false;
                    else {
                        if (answer == DialogResult.Yes) {
                            _control.Save();
                        }
                    }
                }
                return true;
            }

            public Control Control
            {
                get { return _control; }
            }

            public string Caption
            {
                get { return Properties.Resources.ViewPatient; }
            }

            public string Detail
            {
                get
                {
                    return ((_control != null && _control._patient != null) ?
                        _control._patient.GetDisplayName() : String.Empty);
                }
            }

            public ToolStrip ToolStrip
            {
                get { return ((_control != null) ? _control.patientTools : null); }
            }

            public MenuStrip MenuStrip
            {
                get { return ((_control != null) ? _control.patientMenu : null); }
            }

            #endregion
        }

        #endregion

        #region Private Instance Fields

        private Patient _patient = null;
        private bool _modified = false;
        private bool _persist = false;

        #endregion

        #region Public Instance Constructors

        public PatientView()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Instance Properties

        private bool Modified
        {
            get
            {
                return _modified;
            }

            set
            {
                _modified = value;
                UpdateCommandUI();
                Program.ViewManager.GetCurrentView(this).UpdateCaption();
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

                _persist = value;
                UpdateCommandUI();
                Program.ViewManager.GetCurrentView(this).UpdateCaption();
            }
        }

        #endregion

        #region Private Instance Methods

        private void Bind(IIdentity patientId)
        {
            if (patientId == null) {
                _patient = new Patient();
            }
            else {
                Entity e = Program.Database.EntityStorage.Retrieve(patientId);
                if (e == null || e.GetType() != typeof(Patient)) {
                    throw new Exception("Could not find patient id " + patientId.ToString());
                }

                _patient = (Patient)e;
                this.Persist = true;
            }

            // add data bindings
            familyNameEdit.DataBindings.Add("Text", _patient, "FamilyName");
            firstNameEdit.DataBindings.Add("Text", _patient, "FirstName");
            middleNameEdit.DataBindings.Add("Text", _patient, "MiddleName");
            birthDatePicker.DataBindings.Add("Value", _patient, "BirthDate");
            identityNoEdit.DataBindings.Add("Text", _patient, "IdentityNo");
            identityIssuerEdit.DataBindings.Add("Text", _patient, "IdentityIssuer");
            phoneEdit.DataBindings.Add("Text", _patient, "Phone");
            addressEdit.DataBindings.Add("Text", _patient, "Address");
            insuranceNoEdit.DataBindings.Add("Text", _patient, "InsuranceNo");
            sentFromEdit.DataBindings.Add("Text", _patient, "SentFrom");
            caseHistoryNoEdit.DataBindings.Add("Text", _patient, "CaseHistoryNo");
            diagnosisEdit.DataBindings.Add("Text", _patient, "Diagnosis");
            commentEdit.DataBindings.Add("Text", _patient, "Comment");

            if (_patient.Gender != Gender.Undefined) {
                genderCombo.SelectedIndex = _patient.Gender == Gender.Male ? 0 : 1;
            }

            // add event handler
            _patient.DataChange += new Entity.DataChangeEvent(BusinessObject_DataChange);
            UpdateCommandUI();
        }

        private void Unbind()
        {
            _patient.DataChange -= new Entity.DataChangeEvent(BusinessObject_DataChange);
            _patient = null;
        }

        private void Save()
        {
            Program.StatusManager.SetStatusText(Properties.Resources.StatusSavingData);
            Program.StatusManager.SetWaitCursor();
            Program.Database.EntityStorage.BeginTrans();
            try {
                Program.Database.EntityStorage.Store(_patient);
                Program.Database.EntityStorage.CommitTrans();
                this.Modified = false;
                this.Persist = true;
            }
            catch (Exception exc) {
                Program.Logger.Error("Cound not save object Patient=" + 
                    _patient.ToString() + ". Exception caught: " + exc.ToString());
                Program.ShowErrorMessage(Properties.Resources.ErrorSaveData);
            }
        }

        private void UpdateCommandUI()
        {
            //saveCmdItem.Enabled = false;
            //saveCmdTool.Enabled = false;
            examinationsCmdItem.Enabled = false;
            examinationsCmdTool.Enabled = false;

            if (this.Modified) {
                saveCmdItem.Enabled = true;
                saveCmdTool.Enabled = true;
            }

            if (this.Persist) {
                examinationsCmdItem.Enabled = true;
                examinationsCmdTool.Enabled = true;
            }
        }

        #endregion

        #region PatientView Event Handlers (Commands)

        private void saveCmd_Click(object sender, EventArgs e)
        {
            if (this.Modified) {
                this.Save();
            }
        }

        private void examinationsCmd_Click(object sender, EventArgs e)
        {
            if (this.Persist) {
                RollView.Factory factory = new RollView.Factory();
                factory.RollName = "Examination";
                factory.ItemFactory = "ExaminationViewFactory";
                factory.Filter = new Filter();
                factory.Filter.Filters.Add(new Filter("_patient", FilterCondition.EqualRef, this._patient));
                factory.Filter.Filters.Add(new Filter("_performDate", FilterCondition.OrderDescending, null));
                //factory.Filter = new Filter("_patient", FilterCondition.EqualRef, this._patient);
                Program.ViewManager.GetCurrentView(this).Host(factory);
            }
        }

        #endregion

        #region PatientView Event Handlers (Business Object)

        private void BusinessObject_DataChange(object sender)
        {
            this.Modified = true;
        }

        #endregion

        #region PatientView Event Handlers (Control)

        private void genderCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (genderCombo.SelectedIndex >= 0) {
                _patient.Gender = (genderCombo.SelectedIndex == 0) ? Gender.Male : Gender.Female;
            }
        }

        #endregion
    }
}
