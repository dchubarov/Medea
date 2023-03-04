// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IIdentity.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.DataAccess
{
    /// <summary>
    /// Represents generic identity.
    /// </summary>
    public interface IIdentity
    {
        /// <summary>
        /// Gets a value indicating that identity is not initialized.
        /// </summary>
        bool IsNull
            { get; }
    }
}