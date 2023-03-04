// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Person.cs
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
    /// Encapsulates basic properties of a person.
    /// </summary>
    public class Person : Entity
    {
        #region Private Fields

        private string _familyName = String.Empty;
        private string _firstName = String.Empty;
        private string _middleName = String.Empty;

        #endregion

        #region Construction

        public Person() : base()
        {
        }

        #endregion

        #region Public Properties

        public string FamilyName
        {
            get
            {
                return _familyName;
            }

            set
            {
                if (_familyName != value) {
                    _familyName = value;
                    OnDataChange();
                }
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (_firstName != value) {
                    _firstName = value;
                    OnDataChange();
                }
            }
        }

        public string MiddleName
        {
            get
            {
                return _middleName;
            }

            set
            {
                if (_middleName != value) {
                    _middleName = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string GetDisplayName()
        {
            if (!String.IsNullOrEmpty(_familyName)) {
                string display = _familyName;
                if (!String.IsNullOrEmpty(_firstName)) {
                    display += " " + _firstName.Substring(0, 1).ToUpper() + ".";
                    if (!String.IsNullOrEmpty(_middleName)) {
                        display += _middleName.Substring(0, 1).ToUpper() + ".";
                    }
                }
                return display;
            }

            return String.Empty;
        }

        public override string ToString()
        {
            return String.Format("(FamilyName={0}, FirstName={1}, MiddleName={2})",
                _familyName,
                _firstName,
                _middleName
                );
        }

        #endregion
    }
}