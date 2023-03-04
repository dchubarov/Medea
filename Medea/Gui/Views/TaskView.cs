// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      TaskView.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Config;
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Gui
{
    /// <summary>
    /// The task pad control.
    /// </summary>
    public partial class TaskView : UserControl
    {
        #region Public Types

        public class Factory : IViewFactory
        {
            #region Private Fields

            private TaskView _control = null;

            #endregion

            #region Construction

            public Factory()
            {
            }

            #endregion

            #region IHostedControlFactory Members

            public bool HandleKeyPress(KeyPressEventArgs e)
            {
                return false;
            }

            public void CreateControl()
            {
                if (_control == null) {
                    _control = new TaskView();
                }
            }

            public void DestroyControl()
            {
                if (_control != null) {
                    _control.Dispose();
                    _control = null;
                }
            }

            public void ActivateControl()
            {
            }

            public bool CanClose(CloseReason reason)
            {
                return true;
            }

            public Control Control
            {
                get
                {
                    return _control;
                }
            }

            public string Caption
            {
                get
                {
                    return Properties.Resources.ViewTaskPad;
                }
            }

            public string Detail
            {
                get
                {
                    return String.Empty;
                }
            }

            public ToolStrip ToolStrip
            {
                get
                {
                    return null;
                }
            }

            public MenuStrip MenuStrip
            {
                get
                {
                    return null;
                }
            }

            #endregion
        }

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new TaskPadControl object.
        /// </summary>
        public TaskView()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void OpenItem(ListViewItem item)
        {
            try {
                if (item != null) {
                    if (item.Tag == null || !(item.Tag is ITaskConfig))
                        throw new Exception("Invalid task configuration.");

                    ITaskConfig task = (ITaskConfig)item.Tag;
                    IViewFactory factory = Program.ConfigManager.GetViewFactory(task.FactoryName, null);
                    Program.ViewManager.GetCurrentView(this).Host(factory);
                }
            }
            catch (Exception exc) {
                Program.Logger.Error("Failed to execute task. Exception caught: " + exc.ToString());
                Program.ShowErrorMessage(exc.Message);
            }
        }

        #endregion

        #region TaskView Event Handlers (Control)

        private void TaskView_Load(object sender, EventArgs e)
        {
            taskList.Items.Clear();
            taskList.Groups.Clear();
            //taskImages.Images.Clear();

            ITaskGroupConfig[] groups = Program.ConfigManager.GetTaskGroups();
            if (groups != null) {
                foreach (ITaskGroupConfig group in groups) {
                    ListViewGroup lvg = taskList.Groups.Add(group.GroupName, group.DisplayName);
                    lvg.Tag = group;
                }
            }

            ITaskConfig[] tasks = Program.ConfigManager.GetTasks();
            if (tasks != null) {
                int iTask = 0;
                foreach (ITaskConfig task in tasks) {
                    ListViewItem lvi = taskList.Items.Add(task.TaskName, task.DisplayName, -1);
                    lvi.Tag = task;

                    foreach (ListViewGroup g in taskList.Groups) {
                        if (g.Name == task.GroupName) {
                            lvi.Group = g;
                            break;
                        }
                    }
                    /*
                    Image im = task.Icon;
                    if (im != null) {
                        taskImages.Images.Add(task.TaskName, im);
                        lvi.ImageKey = task.TaskName;
                    }
                     * */
                    lvi.ImageIndex = iTask++;
                }
            }
        }

        private void taskList_Click(object sender, EventArgs e)
        {
            if (taskList.FocusedItem != null) {
                this.OpenItem(taskList.FocusedItem);
            }
        }

        #endregion

        private void taskList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
                taskList_Click(null, null);
                e.Handled = true;
            }
        }
    }
}
