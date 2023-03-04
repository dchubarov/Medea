// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      LicenseInfoHelper.cs
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
    public static class LicenseInfoHelper
    {
        /// <summary>
        /// Gets friendly name of program edition.
        /// </summary>
        /// <param name="licenseInfo">License info provider object.</param>
        /// <returns>Display name for edition of license info provider or empty string if license is invalid.</returns>
        public static string GetEditionName(ILicenseInfo licenseInfo)
        {
            string editionName = String.Empty;
            if (licenseInfo != null && licenseInfo.Edition != LicenseEdition.Unknown) {
                switch (licenseInfo.Edition) {
                    case LicenseEdition.Demo:
                        editionName = Properties.Resources.EditionDemo;
                        break;
                    case LicenseEdition.Personal:
                        editionName = Properties.Resources.EditionPersonal;
                        break;
                    case LicenseEdition.Enterprise:
                        editionName = Properties.Resources.EditionEnterprise;
                        break;
                }
            }
            return editionName;
        }
    }
}