// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ICursor.cs
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
    /// Provides methods to fetch multiply result continuously.
    /// </summary>
    public interface ICursor : IEnumerator<Entity>
    {
        /// <summary>
        /// Gets a value indicating that end of file reached.
        /// </summary>
        bool Eof 
            { get; }

        /// <summary>
        /// Gets number of objects in a result set.
        /// </summary>
        int Count
            { get; }

        object DataSource
            { get; }
    }
}
