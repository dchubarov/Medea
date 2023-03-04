// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Logger.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Xml;
// log4net
using log4net;
using log4net.Config;
// medea
using Gyrosoft.Medea.Logging;

namespace Gyrosoft.Medea
{
    public class Logger : ILogger
    {
        #region Private Fields

        private const string LogFileName = "trace.log";
        private ILog _log = null;

        #endregion

        #region Construction

        public Logger()
        {
            try {
                XmlDocument configDoc = new XmlDocument();
                configDoc.Load(Assembly.GetExecutingAssembly().Location + ".config");
                XmlNodeList nodes = configDoc.DocumentElement.GetElementsByTagName("log4net");
                if (nodes.Count > 0) {
                    XmlElement elementLogConfig = (XmlElement)nodes[0];
                    XmlElement elementAppenderConfig = (XmlElement)elementLogConfig.SelectSingleNode("./appender[@name=\"LogFileAppender\"]");
                    if (elementAppenderConfig != null) {
                        XmlElement elementFileConfig = null;
                        foreach (XmlNode e in elementAppenderConfig.ChildNodes) {
                            if (e.NodeType == XmlNodeType.Element && String.Compare(e.Name, "file", true) == 0) {
                                elementFileConfig = (XmlElement)e;
                                break;
                            }
                        }

                        if (elementFileConfig == null) {
                            elementFileConfig = configDoc.CreateElement("file");
                            elementAppenderConfig.AppendChild(elementFileConfig);
                        }

                        XmlAttribute attrFileName = elementFileConfig.GetAttributeNode("value");
                        if (attrFileName == null) {
                            attrFileName = configDoc.CreateAttribute("value");
                            elementFileConfig.Attributes.Append(attrFileName);
                        }

                        if (String.IsNullOrEmpty(attrFileName.Value)) {
                            attrFileName.Value = Program.GetWorkingDirectory(Environment.SpecialFolder.ApplicationData) + LogFileName;
                        }
                    }

                    XmlConfigurator.Configure(elementLogConfig);
                }
            }
            catch (Exception exc) {
                Trace.WriteLine("Log file configuration failed. Exception caught: " + exc.Message);
            }

            // create logger object
            _log = LogManager.GetLogger(typeof(Program));
            this.Info("Log started.");
        }

        #endregion

        #region ILogger Members

        public void Debug(string msg)
        {
            if (_log.IsDebugEnabled) {
                _log.Debug(msg);
            }
        }

        public void Info(string msg)
        {
            if (_log.IsInfoEnabled) {
                _log.Info(msg);
            }
        }

        public void Warn(string msg)
        {
            if (_log.IsWarnEnabled) {
                _log.Warn(msg);
            }
        }

        public void Error(string msg)
        {
            if (_log.IsErrorEnabled) {
                _log.Error(msg);
            }
        }

        public void Fatal(string msg)
        {
            if (_log.IsFatalEnabled) {
                _log.Fatal(msg);
            }
        }

        public void Indent()
        {
            // not implemented
        }

        public void UnIndent()
        {
            // not implemented
        }

        #endregion
    }
}
