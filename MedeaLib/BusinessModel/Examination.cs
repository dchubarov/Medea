// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Examination.cs
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
    /// Encapsulates properties of an examination.
    /// </summary>
    public class Examination : Entity
    {
        #region Private Fields

        private ExaminationKind _kind = null;
        private Patient _patient = null;
        private Physician _physician = null;
        private string _conclusion = String.Empty;
        private DateTime _performDate = DateTime.Today;
        private DateTime _completeDate = DateTime.Today;
        private bool _completed = false;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Examination object.
        /// </summary>
        public Examination() : base()
        {
        }

        /// <summary>
        /// Constructs new Examination object.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="patient"></param>
        /// <param name="physician"></param>
        public Examination(ExaminationKind kind, Patient patient, Physician physician) : base()
        {
            _kind = kind;
            _patient = patient;
            _physician = physician;
        }

        #endregion

        #region Public Properties

        public ExaminationKind Kind
        {
            get
            {
                return _kind;
            }

            set
            {
                if (!Object.ReferenceEquals(_kind, value)) {
                    _kind = value;
                    OnDataChange();
                }
            }
        }

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

        public string Conclusion
        {
            get
            {
                return _conclusion;
            }

            set
            {
                if (_conclusion != value) {
                    _conclusion = value;
                    OnDataChange();
                }
            }
        }

        public DateTime PerformDate
        {
            get
            {
                return _performDate;
            }

            set
            {
                if (_performDate != value) {
                    _performDate = value;
                    OnDataChange();
                }
            }
        }

        public DateTime CompleteDate
        {
            get
            {
                return _completeDate;
            }

            set
            {
                if (_completeDate != value) {
                    _completeDate = value;
                    OnDataChange();
                }
            }
        }

        public bool Completed
        {
            get
            {
                return _completed;
            }

            set
            {
                if (_completed != value) {
                    _completed = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string GetDisplayName()
        {
            return String.Format("{0}{1}{2}", 
                this.PerformDate.ToShortDateString(),
                this.Kind == null ? String.Empty : ", " + this.Kind.GetDisplayName(),
                this.Patient == null ? String.Empty : ", " + this.Patient.GetDisplayName()
                );
        }

        public override string ToString()
        {
            return String.Format("(Kind={0}, Patient={1}, Physician={2}, Conclusion={3}, PerformDate={4}, CompleteDate={5}, Completed={6})",
                _kind == null ? Entity.NullString : _kind.ToString(),
                _patient == null ? Entity.NullString : _patient.ToString(),
                _physician == null ? Entity.NullString : _physician.ToString(),
                _conclusion,
                _performDate,
                _completeDate,
                _completed
                );
        }

        #endregion
    }
}