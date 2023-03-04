// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IStatusManager.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.Extensibility
{
    public enum ProgressControlCommand
    {
        Init,
        Release,
        Set,
        SetToolTipText
    }

    /// <summary>
    /// Provides methods for displaying process status.
    /// </summary>
    public interface IStatusManager
    {
        /// <summary>
        /// Sets default status text.
        /// </summary>
        void SetStatusText();

        /// <summary>
        /// Sets specified status text.
        /// </summary>
        /// <param name="statusText">Status text.</param>
        void SetStatusText(string statusText);

        /// <summary>
        /// Sets status text using specified text or resource name.
        /// </summary>
        /// <param name="statusTextOrResourceName">Status text or string resource name.</param>
        /// <param name="isResourceName">Indicates that first parameter is a resource name.</param>
        void SetStatusText(string statusTextOrResourceName, bool isResourceName);

        /// <summary>
        /// Controls the shared progress bar.
        /// </summary>
        /// <param name="owner">Progress bar owner.</param>
        /// <param name="command">Command, see <see cref="ProgressControlCommand"/>.</param>
        /// <param name="args">Command arguments.</param>
        void ProgressControl(object owner, ProgressControlCommand command, params object[] args);

        /// <summary>
        /// Sets wait cursor.
        /// </summary>
        void SetWaitCursor();
    }
}