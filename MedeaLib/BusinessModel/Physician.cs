// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Physician.cs
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
    /// Encapsulates properties of a physician.
    /// </summary>
    public class Physician : Person
    {
        #region Private Fields

        private Department _department = null;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Physician object.
        /// </summary>
        public Physician() : base()
        {
        }

        #endregion

        #region Public Properties

        public Department Department
        {
            get
            {
                return _department;
            }

            set
            {
                if (!Object.ReferenceEquals(_department, value)) {
                    _department = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(base%Person={0}, Department={1})",
                base.ToString(),
                _department == null ? Entity.NullString : _department.ToString()
                );
        }

        #endregion
    }
}