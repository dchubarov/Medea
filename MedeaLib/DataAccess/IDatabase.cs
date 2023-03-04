// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IAccessor.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;
// medea
using Gyrosoft.Medea.Logging;

namespace Gyrosoft.Medea.DataAccess
{
    /// <summary>
    /// Provides methods to access database.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Opens a database.
        /// </summary>
        void Open();

        /// <summary>
        /// Closes a database.
        /// </summary>
        void Close();

        /// <summary>
        /// Gets or sets connection string.
        /// </summary>
        string ConnectionString
            { get; set; }

        /// <summary>
        /// Gets or sets event logging object.
        /// </summary>
        ILogger Logger
            { get; set; }

        /// <summary>
        /// Gets an IEntityStorage object represents storage of object data.
        /// </summary>
        IEntityStorage EntityStorage 
            { get; }

        /// <summary>
        /// Gets an IBlobStorage object represents storage of binary data.
        /// </summary>
        IBlobStorage BlobStorage
            { get; }
    }
}