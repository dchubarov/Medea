// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ExaminationSchedule.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;
// medea
using Gyrosoft.Medea.DataAccess;

namespace Gyrosoft.Medea.BusinessModel
{
    /// <summary>
    /// Encapsulates properties of examination schedule.
    /// </summary>
    public class ExaminationSchedule : Entity
    {
        #region Private Fields

        private Patient _patient = null;
        private Physician _physician = null;
        private ExaminationKind _examinationKind = null;
        private DateTime _scheduleTime = DateTime.Now;

        #endregion

        #region Construction

        public ExaminationSchedule() : base()
        {
        }

        #endregion

        #region Public Properties

        public Patient Patient
        {
            get
            {
                return _patient;
            }

            set
            {
                if (!Object.ReferenceEquals(_patient, value)) {
                    _patient = value;
                    OnDataChange();
                }
            }
        }

        public Physician Physician
        {
            get
            {
                return _physician;
            }

            set
            {
                if (!Object.ReferenceEquals(_physician, value)) {
                    _physician = value;
                    OnDataChange();
                }
            }
        }

        public ExaminationKind ExaminationKind
        {
            get
            {
                return _examinationKind;
            }

            set
            {
                if (!Object.ReferenceEquals(_examinationKind, value)) {
                    _examinationKind = value;
                    OnDataChange();
                }
            }
        }

        public DateTime ScheduleTime
        {
            get
            {
                return _scheduleTime;
            }

            set
            {
                if (_scheduleTime != value) {
                    _scheduleTime = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(Patient={0}, ExaminationKind={1}, ScheduleTime={3})",
                _patient == null ? Entity.NullString : _patient.ToString(),
                _physician == null ? Entity.NullString : _physician.ToString(),
                _examinationKind == null ? Entity.NullString : _examinationKind.ToString(),
                _scheduleTime.ToString()
                );
        }

        #endregion
    }
}
