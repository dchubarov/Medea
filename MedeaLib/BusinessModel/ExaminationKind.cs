// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ExaminationKind.cs
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
    /// Encapsulates properties of a kind of an examination.
    /// </summary>
    public class ExaminationKind : NamedEntity
    {
        #region Construction

        /// <summary>
        /// Constructs new ExaminationKind object.
        /// </summary>
        public ExaminationKind() : base()
        {
        }

        /// <summary>
        /// Constructs new ExaminationKind object.
        /// </summary>
        /// <param name="name">The name of examination kind.</param>
        public ExaminationKind(string name) : base(name)
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
