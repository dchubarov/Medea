// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      PrintExaminationDialog.cs
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
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;

namespace Gyrosoft.Medea.Gui
{
    public partial class PrintExaminationDialog : Form
    {
        private Font _baseFont = null;
        private Font _titleFont = null;
        private Font _subTitleFont = null;
        private Font _boldFont = null;
        private int _checkPrint = 0;

        /// <summary>
        /// Constructs new PrintExaminationDialog object.
        /// </summary>
        public PrintExaminationDialog()
        {
            InitializeComponent();
            _baseFont = new Font("Arial", 10);
            _titleFont = new Font("Arial", 16, FontStyle.Bold);
            _subTitleFont = new Font("Arial", 12, FontStyle.Bold);
            _boldFont = new Font(_baseFont, FontStyle.Bold);
        }

        public void CreateReport(Examination exm, IList<string> indList)
        {
            repText.Clear();

            repText.SelectionFont = _titleFont;
            repText.SelectionAlignment = HorizontalAlignment.Center;
            repText.AppendText(exm.Physician.Department.Clinic.GetDisplayName() + "\n");

            repText.SelectionFont = _subTitleFont;
            repText.SelectionAlignment = HorizontalAlignment.Center;
            repText.AppendText("(" + exm.Physician.Department.GetDisplayName() + ")\n\n");

            repText.SelectionAlignment = HorizontalAlignment.Left;

            repText.SelectionFont = _boldFont;
            repText.AppendText(Properties.Resources.RepExaminationDate + " ");
            repText.SelectionFont = _baseFont;
            repText.AppendText(exm.PerformDate.ToLongDateString() + "\n");

            repText.SelectionFont = _boldFont;
            repText.AppendText(Properties.Resources.RepExaminationKind + " ");
            repText.SelectionFont = _baseFont;
            repText.AppendText(exm.Kind.GetDisplayName() + "\n\n");

            repText.SelectionFont = _boldFont;
            repText.AppendText(Properties.Resources.RepPatient + " ");
            repText.SelectionFont = _baseFont;
            repText.AppendText(exm.Patient.FamilyName + " " + exm.Patient.FirstName + " " + exm.Patient.MiddleName);

            if (exm.Patient.Gender == Gender.Male)
                repText.AppendText(" (" + Properties.Resources.RepGenderMale + ")\n");
            else if (exm.Patient.Gender == Gender.Female)
                repText.AppendText(" (" + Properties.Resources.RepGenderFemale + ")\n");

            repText.SelectionFont = _boldFont;
            repText.AppendText(Properties.Resources.RepBirthdate + " ");
            repText.SelectionFont = _baseFont;
            repText.AppendText(exm.Patient.BirthDate.ToLongDateString() + "\n\n");

            repText.SelectionFont = _subTitleFont;
            repText.AppendText(Properties.Resources.RepIndications + "\n");

            if (indList != null && indList.Count > 0) {
                foreach (string indText in indList) {
                    repText.SelectionFont = _baseFont;
                    repText.AppendText(indText + "\n");
                }

                repText.AppendText("\n");
            }
            else {
                repText.SelectionFont = _baseFont;
                repText.AppendText(Properties.Resources.RepNoIndications + "\n\n");
            }

            repText.SelectionFont = _subTitleFont;
            repText.AppendText(Properties.Resources.RepConclusion + "\n");

            repText.SelectionFont = _baseFont;
            if (!String.IsNullOrEmpty(exm.Conclusion)) {
                repText.AppendText(exm.Conclusion);
            }
            else {
                repText.AppendText(Properties.Resources.RepNoConclusion);
            }
            repText.AppendText("\n\n");

            repText.SelectionFont = _boldFont;
            repText.AppendText("\n\n" + Properties.Resources.RepPhysician + "\t\t\t\t" + exm.Physician.GetDisplayName() + "\n\n\n");

            repText.SelectionFont = _baseFont;
            repText.AppendText(Properties.Resources.RepSignDate);

            repText.Select(0, 0);
        }

        private void saveAsBtn_Click(object sender, EventArgs e)
        {
            try {
                if (repSaveDlg.ShowDialog() == DialogResult.OK) {
                    repText.SaveFile(repSaveDlg.FileName);
                }
            }
            catch (Exception exc) {
                Program.ShowErrorMessage(exc.Message);
            }
        }

        private void repPrintDoc_BeginPrint(object sender, PrintEventArgs e)
        {
            _checkPrint = 0;
        }

        private void repPrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            _checkPrint = repText.Print(_checkPrint, repText.TextLength, e);
            e.HasMorePages = (_checkPrint < repText.TextLength);
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            try {
                if (repPrintDlg.ShowDialog() == DialogResult.OK) {
                    repPrintDoc.Print();
                }
            }
            catch (Exception exc) {
                Program.ShowErrorMessage(exc.Message);
            }
        }
    }
}