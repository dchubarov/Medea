// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Settings.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Configuration;

namespace Gyrosoft.Medea.Properties
{
    /// <summary>
    /// Responds to configuration related events and allows to specify a settings provider.
    /// </summary>
    [SettingsProvider(typeof(LocalFileSettingsProvider))]
    internal sealed partial class Settings
    {
        #region Construction

        /// <summary>
        /// Constructs new Settings object.
        /// </summary>
        public Settings()
        {
        }

        #endregion

        #region Settings Event Handlers (Settings)

        #endregion
    }
}