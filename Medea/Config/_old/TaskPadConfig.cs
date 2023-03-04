// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      TaskPadConfig.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;

namespace Gyrosoft.Medea.Config
{
    public class TaskPadConfig
    {
        #region Public Types

        public enum TaskAction
        {
            Unknown,
            OpenView
        }

        public class TaskInfo
        {
            #region Private Fields

            private const string ActionPropertyName = "action";
            private const string GroupRefPropertyName = "groupRef";

            private TaskPadConfig _config = null;
            private string _taskName = null;
            private string _groupName = null;
            private string _displayName = null;
            private TaskAction _action = TaskAction.Unknown;

            #endregion

            #region Construction

            private TaskInfo(TaskPadConfig config)
            {
                _config = config;
            }

            #endregion

            #region Public Properties

            public TaskPadConfig Config
            {
                get
                {
                    return _config;
                }
            }

            public string TaskName
            {
                get
                {
                    return _taskName;
                }
            }

            public string GroupName
            {
                get
                {
                    return _groupName;
                }
            }

            public GroupInfo Group
            {
                get
                {
                    return ((String.IsNullOrEmpty(_groupName) ||
                        !_config._groups.ContainsKey(_groupName)) ? null : _config._groups[_groupName]);
                }
            }

            public string DisplayName
            {
                get
                {
                    return (String.IsNullOrEmpty(_displayName) ? _taskName : _displayName);
                }
            }

            public TaskAction Action
            {
                get
                {
                    return _action;
                }
            }

            #endregion

            #region Public Methods

            public static TaskInfo FromXmlElement(TaskPadConfig config, XmlElement taskElement)
            {
                if (taskElement == null) {
                    throw new ArgumentNullException("taskElement");
                }

                TaskInfo taskInfo = new TaskInfo(config);

                // obtain internal name for a task
                taskInfo._taskName = XmlConfigHelper.GetElementName(taskElement);
                if (taskInfo.TaskName == null) {
                    Program.Logger.Warn("Task name is not valid.");
                }

                // obtain group name for a task
                taskInfo._groupName = XmlConfigHelper.GetPropertyValue(taskElement, GroupRefPropertyName);

                // obtain action for a task
                string tmp = XmlConfigHelper.GetPropertyValue(taskElement, ActionPropertyName);
                if (tmp == null)
                    throw new Exception("Missing action for a task '" + taskInfo.TaskName + "'");
                else {
                    if (String.Compare(tmp, TaskAction.OpenView.ToString(), true) == 0)
                        taskInfo._action = TaskAction.OpenView;
                    else {
                        throw new Exception("Task '" + taskInfo.TaskName + "': unrecognized action " + tmp + ".");
                    }
                }


                return taskInfo;
            }

            #endregion
        }

        public class FactoryInfo
        {

        }

        public class GroupInfo
        {

        }

        #endregion

        #region Private Constants

        private const string SectionName        = "taskpad";
        private const string FactoryElementName = "factory";
        private const string GroupElementName   = "group";
        private const string TaskElementName    = "task";

        #endregion

        #region Private Fields

        Dictionary<string, FactoryInfo> _factories = new Dictionary<string, FactoryInfo>();
        Dictionary<string, GroupInfo> _groups = new Dictionary<string, GroupInfo>();
        Dictionary<string, TaskInfo> _tasks = new Dictionary<string, TaskInfo>();

        #endregion

        #region Construction

        public TaskPadConfig()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        public void Configure()
        {
            // obtain root element of task pad configuration from program configuration
            XmlElement configElement = ConfigurationManager.GetSection(SectionName) as XmlElement;

            // there is no section in config file
            if (configElement == null) {
                Program.Logger.Error("There is no section '" + SectionName + "' in program configuration.");
                throw new Exception(Properties.Resources.ErrorImproperConfiguration);
            }

            // enumerate child nodes 
            foreach (XmlNode childNode in configElement.ChildNodes) {
                if (childNode.NodeType == XmlNodeType.Element) {
                    try {
                        if (childNode.Name == FactoryElementName) {

                        }
                        else if (childNode.Name == GroupElementName) {

                        }
                        else if (childNode.Name == TaskElementName) {
                            TaskInfo taskInfo = TaskInfo.FromXmlElement(this, childNode as XmlElement);
                            _tasks.Add(taskInfo.TaskName, taskInfo);
                        }
                    }
                    catch (Exception exc) {
                        Program.Logger.Warn("Failed to load taskpad configuration element. Exception caught: " + exc.ToString());
                    }
                }
            }
        }

        #endregion
    }
}
