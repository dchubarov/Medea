// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IBlobStorage.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gyrosoft.Medea.DataAccess
{
    /// <summary>
    /// Provides methods to access binary data in a database.
    /// </summary>
    public interface IBlobStorage
    {
        /// <summary>
        /// Returns a Stream object to read or write data associated with specified entity.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="forReading">Indicating that called want read existing data.</param>
        /// <returns>Stream object.</returns>
        Stream GetStream(
            Entity entity, 
            bool forReading
            );

        void DeleteBlob(
            Entity entity
            );
    }
}
