// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Patient.cs
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
    public enum Gender
    {
        Undefined,
        Male,
        Female
    }

    /// <summary>
    /// Encapsulates properties of a patient.
    /// </summary>
    public class Patient : Person
    {
        #region Private Fields

        private DateTime _birthDate = new DateTime(1900,1,1);
        private DateTime _regDate = DateTime.Today;
        private Gender _gender = Gender.Undefined;
        private String _identityNo = String.Empty;
        private String _identityIssuer = String.Empty;
        private String _address = String.Empty;
        private String _phone = String.Empty;
        private String _insuranceNo = String.Empty;
        private String _sentFrom = String.Empty;
        private String _caseHistoryNo = String.Empty;
        private String _diagnosis = String.Empty;
        private String _comment = String.Empty;

        #endregion

        #region Construction

        public Patient() : base()
        {
        }

        #endregion

        #region Public Properties

        public DateTime BirthDate
        {
            get
            {
                return _birthDate;
            }

            set
            {
                if (_birthDate != value) {
                    _birthDate = value;
                    OnDataChange();
                }
            }
        }

        public DateTime RegDate
        {
            get
            {
                return _regDate;
            }

            set
            {
                if (_regDate != value) {
                    _regDate = value;
                    OnDataChange();
                }
            }
        }

        public Gender Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                if (_gender != value) {
                    _gender = value;
                    OnDataChange();
                }
            }
        }

        public String IdentityNo
        {
            get
            {
                return _identityNo;
            }

            set
            {
                if (_identityNo != value) {
                    _identityNo = value;
                    OnDataChange();
                }
            }
        }

        public String IdentityIssuer
        {
            get
            {
                return _identityIssuer;
            }

            set
            {
                if (_identityIssuer != value) {
                    _identityIssuer = value;
                    OnDataChange();
                }
            }
        }

        public String Address
        {
            get
            {
                return _address;
            }

            set
            {
                if (_address != value) {
                    _address = value;
                    OnDataChange();
                }
            }
        }

        public String Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                if (_phone != value) {
                    _phone = value;
                    OnDataChange();
                }
            }
        }

        public String InsuranceNo
        {
            get
            {
                return _insuranceNo;
            }

            set
            {
                if (_insuranceNo != value) {
                    _insuranceNo = value;
                    OnDataChange();
                }
            }
        }

        public String SentFrom
        {
            get
            {
                return _sentFrom;
            }

            set
            {
                if (_sentFrom != value) {
                    _sentFrom = value;
                    OnDataChange();
                }
            }
        }

        public String CaseHistoryNo
        {
            get
            {
                return _caseHistoryNo;
            }

            set
            {
                if (_caseHistoryNo != value) {
                    _caseHistoryNo = value;
                    OnDataChange();
                }
            }
        }

        public String Diagnosis
        {
            get
            {
                return _diagnosis;
            }

            set
            {
                if (_diagnosis != value) {
                    _diagnosis = value;
                    OnDataChange();
                }
            }
        }

        public String Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment != value) {
                    _comment = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(base%Person={0}, BirthDate={1})",
                base.ToString(),
                _birthDate.ToString()
                );
        }

        #endregion
    }
}