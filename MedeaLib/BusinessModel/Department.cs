// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Department.cs
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
    /// Encapsulates properties of a department.
    /// </summary>
    public class Department : NamedEntity
    {
        #region Private Fields

        private Clinic _clinic = null;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Department object.
        /// </summary>
        public Department() : base()
        {
        }

        #endregion

        #region Public Properties

        public Clinic Clinic
        {
            get
            {
                return _clinic;
            }

            set
            {
                if (!Object.ReferenceEquals(_clinic, value)) {
                    _clinic = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(base%NamedEntity={0}, Clinic={1})", 
                base.ToString(), 
                _clinic == null ? Entity.NullString : _clinic.ToString()
                );
        }

        #endregion
    }
}
