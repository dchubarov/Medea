// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Clinic.cs
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
    /// Encapsulates properties of a clinic.
    /// </summary>
    public class Clinic : NamedEntity
    {
        #region Construction

        /// <summary>
        /// Constructs new Clinic object.
        /// </summary>
        public Clinic() : base()
        {
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(base%NamedEntity={0})", base.ToString());
        }

        #endregion
    }
}
