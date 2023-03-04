// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ILogger.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.Logging
{
    /// <summary>
    /// Provides methods for message logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a debug message to a log.
        /// </summary>
        /// <param name="msg">The log message.</param>
        void Debug(string msg);

        /// <summary>
        /// Writes an informational message to a log.
        /// </summary>
        /// <param name="msg">The log message.</param>
        void Info(string msg);

        /// <summary>
        /// Writes a warning message to a log.
        /// </summary>
        /// <param name="msg">The log message.</param>
        void Warn(string msg);

        /// <summary>
        /// Writes an error message to a log.
        /// </summary>
        /// <param name="msg">The log message.</param>
        void Error(string msg);

        /// <summary>
        /// Writes a fatal error message to a log.
        /// </summary>
        /// <param name="msg">The log message.</param>
        void Fatal(string msg);

        /// <summary>
        /// Increases indent of log messages.
        /// </summary>
        void Indent();

        /// <summary>
        /// Decreases indent of log messages.
        /// </summary>
        void UnIndent();
    }
}