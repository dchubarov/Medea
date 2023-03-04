// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ILicenseInfo.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.Licensing
{
    /// <summary>
    /// License status.
    /// </summary>
    public enum LicenseStatus
    {
        Valid,
        Expired,
        Invalid
    }

    /// <summary>
    /// Program edition.
    /// </summary>
    public enum LicenseEdition
    {
        Unknown,
        Demo,
        Personal,
        Enterprise
    }

    /// <summary>
    /// Provides methods to access program license information.
    /// </summary>
    public interface ILicenseInfo
    {
        /// <summary>
        /// Get license status.
        /// </summary>
        LicenseStatus Status
            { get; }

        /// <summary>
        /// Gets license edition.
        /// </summary>
        LicenseEdition Edition
            { get; }

        /// <summary>
        /// Gets licensee name.
        /// </summary>
        string Licensee
            { get; }

        /// <summary>
        /// Gets licensee organization name.
        /// </summary>
        string LicenseeOrganization
            { get; }

        /// <summary>
        /// Gets license expiration date or zero date if license never expires.
        /// </summary>
        DateTime ExpireDate
            { get; }
    }
}