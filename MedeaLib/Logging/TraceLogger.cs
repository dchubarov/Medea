// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      TraceLogger.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Gyrosoft.Medea.Logging
{
    /// <summary>
    /// Logging messages to trace system.
    /// </summary>
    public class TraceLogger : ILogger
    {
        #region Private Fields

        private int _indent = 0;
        private int _indentSize = 4;

        #endregion

        #region Construction

        public TraceLogger()
        {
        }

        public TraceLogger(int indentSize)
        {
            _indentSize = indentSize;
        }

        #endregion

        #region Private Methods

        private string IndentMessage(string msg)
        {
            if (!String.IsNullOrEmpty(msg) && _indent > 0 && _indentSize > 0) {
                return msg.PadLeft(msg.Length + _indent * _indentSize);
            }
            return msg;
        }

        #endregion

        #region ILogger Members

        public void Debug(string msg)
        {
            Trace.WriteLine(this.IndentMessage(msg), "Debug");
        }

        public void Info(string msg)
        {
            Trace.WriteLine(this.IndentMessage(msg), "Info ");
        }

        public void Warn(string msg)
        {
            Trace.WriteLine(this.IndentMessage(msg), "Warn ");
        }

        public void Error(string msg)
        {
            Trace.WriteLine(this.IndentMessage(msg), "Error");
        }

        public void Fatal(string msg)
        {
            Trace.WriteLine(this.IndentMessage(msg), "Fatal");
        }

        public void Indent()
        {
            _indent++;
        }

        public void UnIndent()
        {
            _indent--;
        }

        #endregion
    }
}