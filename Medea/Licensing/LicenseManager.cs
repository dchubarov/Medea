// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      LicenseManager.cs
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
    class LicenseManager : ILicenseInfo
    {
        /// <summary>
        /// Constructs new LicenseManager object.
        /// </summary>
        public LicenseManager()
        {
        }

        #region ILicenseInfo Members

        public LicenseStatus Status
        {
            get { return LicenseStatus.Valid; }
        }

        public LicenseEdition Edition
        {
            get { return LicenseEdition.Personal; }
        }

        public string Licensee
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public string LicenseeOrganization
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public DateTime ExpireDate
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}