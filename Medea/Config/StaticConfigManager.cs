using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
// medea
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Config
{
    class StaticConfigManager : ConfigManagerBase
    {
        #region Private Types

        class ViewFactoryConfig : IViewFactoryConfig {

            #region Private Instance Fields

            private string _typeName = null;

            #endregion

            #region Public Instance Constructors

            public ViewFactoryConfig(string typeName) 
            {
                _typeName = typeName;
            }

            #endregion

            #region IViewFactoryConfig Members

            public string  TypeName
            {
                get { return _typeName; }
            }

            #endregion
        }

        class RollColumnConfig : IRollColumnConfig
        {
            #region Private Instance Fields

            private string _fieldName = null;
            private string _displayName = null;
            private int _width = 0;
            private int _order = 0;
            private bool _visible = false;

            #endregion

            #region Public Instance Constructors

            public RollColumnConfig(string fieldName, string displayName, int width, int order, bool visible)
            {
                _fieldName = fieldName;
                _displayName = Properties.Resources.ResourceManager.GetString(displayName);
                if (String.IsNullOrEmpty(_displayName))
                    _displayName = _fieldName;
                _width = width;
                _order = order;
                _visible = visible;
            }

            #endregion

            #region IRollColumnConfig Members

            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
            }

            public string FieldName
            {
                get
                {
                    return _fieldName;
                }
            }

            public int Width
            {
                get
                {
                    return _width;
                }

                set
                {
                    _width = value;
                }
            }

            public int Order
            {
                get
                {
                    return _order;
                }

                set
                {
                    _order = value;
                }
            }

            public bool Visible
            {
                get
                {
                    return _visible;
                }
                set
                {
                    _visible = value;
                }
            }

            #endregion
        }

        class RollConfig : IRollConfig
        {
            #region Private Instance Fields

            string _displayName = null;
            Type _entityType = null;
            Dictionary<string, RollColumnConfig> _columns = new Dictionary<string, RollColumnConfig>(10);

            #endregion

            #region Public Instance Constructors

            public RollConfig(string rollName, Type entityType)
            {
                if (String.IsNullOrEmpty(rollName)) {
                    throw new ArgumentNullException("rollName");
                }
                _displayName = Properties.Resources.ResourceManager.GetString("Roll" + rollName);
                if (String.IsNullOrEmpty(_displayName)) {
                    _displayName = rollName;
                }
                _entityType = entityType;
            }

            #endregion

            #region Public Instance Methods

            public void AddColumn(RollColumnConfig column)
            {
                _columns.Add(column.FieldName, column);
            }

            #endregion

            #region IRollConfig Members

            public string DisplayName
            {
                get
                {
                    return _displayName;
                }
            }

            public Type EntityType
            {
                get
                {
                    return _entityType;
                }
            }

            public IRollColumnConfig[] Columns
            {
                get {
                    RollColumnConfig[] columnArray = new RollColumnConfig[_columns.Count];
                    _columns.Values.CopyTo(columnArray, 0);
                    return columnArray;
                }
            }

            #endregion
        }

        class TaskGroupConfig : ITaskGroupConfig
        {
            #region Private Instance Fields

            string _groupName = null;
            string _displayName = null;

            #endregion

            #region Public Instance Constructors

            public TaskGroupConfig(string groupName)
            {
                if (String.IsNullOrEmpty(groupName)) {
                    throw new ArgumentNullException("groupName");
                }
                _displayName = Properties.Resources.ResourceManager.GetString("TaskGroup" + groupName);
                if (String.IsNullOrEmpty(_displayName)) {
                    _displayName = groupName;
                }
                _groupName = groupName;
            }

            #endregion

            #region ITaskGroupConfig Members

            public string GroupName
            {
                get { return _groupName; }
            }

            public string DisplayName
            {
                get { return _displayName; }
            }

            #endregion
        }

        class TaskConfig : ITaskConfig
        {
            #region Private Instance Fields

            private string _taskName = null;
            private string _groupName = null;
            private string _factoryName = null;
            private string _displayName = null;

            #endregion

            #region Public Instance Constructors

            public TaskConfig(string taskName, string groupName, string factoryName)
            {
                if (String.IsNullOrEmpty(taskName))
                    throw new ArgumentNullException("taskName");
                if (String.IsNullOrEmpty(groupName))
                    throw new ArgumentNullException("groupName");
                if (String.IsNullOrEmpty(factoryName))
                    throw new ArgumentNullException("factoryName");

                _displayName = Properties.Resources.ResourceManager.GetString("Task" + taskName);
                if (String.IsNullOrEmpty(_displayName))
                    _displayName = taskName;

                _taskName = taskName;
                _factoryName = factoryName;
                _groupName = groupName;
            }

            #endregion

            #region ITaskConfig Members

            public string TaskName
            {
                get { return _taskName; }
            }

            public string GroupName
            {
                get { return _groupName; }
            }

            public string DisplayName
            {
                get { return _displayName; }
            }

            public string FactoryName
            {
                get { return _factoryName; }
            }

            public Image Icon
            {
                get { return (Image)Properties.Resources.ResourceManager.GetObject("BitmapTask" + _taskName); }
            }

            #endregion
        }

        #endregion

        #region Private Instance Fields

        private Dictionary<string, ViewFactoryConfig> _factories = new Dictionary<string, ViewFactoryConfig>();
        private Dictionary<string, RollConfig> _rolls = new Dictionary<string, RollConfig>();
        private Dictionary<string, TaskGroupConfig> _groups = new Dictionary<string, TaskGroupConfig>();
        private Dictionary<string, TaskConfig> _tasks = new Dictionary<string, TaskConfig>();

        #endregion

        #region Public Instance Constructors

        public StaticConfigManager() : base()
        {
            this.InitializeConfiguration();
        }

        #endregion

        #region Public Instance Methods

        public override IViewFactory GetViewFactory(string factoryName, IDictionary<string, object> createParams)
        {
            IViewFactory factory = null;
            IViewFactoryConfig factoryC = (_factories.ContainsKey(factoryName) ? _factories[factoryName] : null);
            if (factoryC == null) {
                throw new Exception("Could not create factory '" + factoryName + "'");
            }

            Type factoryT = Assembly.GetExecutingAssembly().GetType(factoryC.TypeName, true);
            if (factoryT == null || !factoryT.IsClass || factoryT.GetInterface("IViewFactory") == null)
                throw new Exception(String.Format("The specified class '{0}' is not valid view factory.", factoryC.TypeName));
            else {
                ConstructorInfo ci = factoryT.GetConstructor(Type.EmptyTypes);
                if (ci == null)
                    throw new Exception(String.Format("View factory '{0}' does not have default constructor.", factoryC.TypeName));
                else {
                    factory = (IViewFactory)ci.Invoke(null);
                    if (createParams != null) {
                        foreach (KeyValuePair<string, object> param in createParams) {
                            PropertyInfo pi = factoryT.GetProperty(param.Key);
                            if (pi == null)
                                Program.Logger.Warn("Property does not exists " + factoryT.ToString() + "." + param.Key);
                            else
                                pi.SetValue(factory, param.Value, null);
                        }
                    }
                }
            }

            return factory;
        }

        public override IRollConfig GetRollConfig(string rollName)
        {
            return _rolls.ContainsKey(rollName) ? _rolls[rollName] : null;
        }

        public override ITaskGroupConfig[] GetTaskGroups()
        {
            TaskGroupConfig[] taskGroupArray = new TaskGroupConfig[_groups.Count];
            _groups.Values.CopyTo(taskGroupArray, 0);
            return taskGroupArray;
        }

        public override ITaskConfig[] GetTasks()
        {
            TaskConfig[] taskArray = new TaskConfig[_tasks.Count];
            _tasks.Values.CopyTo(taskArray, 0);
            return taskArray;
        }

        #endregion

        #region Private Instance Methods

        private void InitializeConfiguration()
        {
            RollConfig rc;

            _factories.Add("PatientViewFactory", new ViewFactoryConfig("Gyrosoft.Medea.Gui.PatientView+Factory"));
            _factories.Add("ExaminationViewFactory", new ViewFactoryConfig("Gyrosoft.Medea.Gui.ExaminationView+Factory"));
            _factories.Add("ImagingViewFactory", new ViewFactoryConfig("Gyrosoft.Medea.Gui.ImagingView+Factory"));
            _factories.Add("RollViewFactory", new ViewFactoryConfig("Gyrosoft.Medea.Gui.RollView+Factory"));

            rc = new RollConfig("Patient", typeof(Patient));
            rc.AddColumn(new RollColumnConfig("FamilyName", "RollPatientFamilyName", 100, 0, true));
            rc.AddColumn(new RollColumnConfig("FirstName", "RollPatientFirstName", 100, 1, true));
            rc.AddColumn(new RollColumnConfig("MiddleName", "RollPatientMiddleName", 100, 2, true));
            rc.AddColumn(new RollColumnConfig("BirthDate", "RollPatientBirthDate", 100, 2, true));
            _rolls.Add("Patient", rc);

            rc = new RollConfig("Examination", typeof(Examination));
            rc.AddColumn(new RollColumnConfig("PerformDate", "RollExaminationPerformDate", 100, 0, true));
            rc.AddColumn(new RollColumnConfig("Kind", "RollExaminationKind", 150, 0, true));
            rc.AddColumn(new RollColumnConfig("Patient", "RollExaminationPatient", 150, 0, true));
            _rolls.Add("Examination", rc);

            rc = new RollConfig("ExaminationKind_", typeof(ExaminationKind));
            rc.AddColumn(new RollColumnConfig("Name", "RollNamedEntityName", -100, 0, true));
            _rolls.Add("ExaminationKind_", rc);

            rc = new RollConfig("Physician", typeof(Physician));
            rc.AddColumn(new RollColumnConfig("FamilyName", "RollPatientFamilyName", -34, 0, true));
            rc.AddColumn(new RollColumnConfig("FirstName", "RollPatientFirstName", -33, 1, true));
            rc.AddColumn(new RollColumnConfig("MiddleName", "RollPatientMiddleName", -33, 2, true));
            _rolls.Add("Physician", rc);

            _groups.Add("Create", new TaskGroupConfig("Create"));
            _groups.Add("Search", new TaskGroupConfig("Search"));

            _tasks.Add("NewExamination", new TaskConfig("NewExamination", "Create", "ExaminationViewFactory"));
            _tasks.Add("NewPatient", new TaskConfig("NewPatient", "Create", "PatientViewFactory"));
            _tasks.Add("Patients", new TaskConfig("Patients", "Search", "RollViewFactory"));
        }

        #endregion
    }
}
