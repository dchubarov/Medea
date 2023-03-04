using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Extensibility;

namespace Gyrosoft.Medea.Config
{
    public abstract class ConfigManagerBase
    {
        #region Public Instance Constructors

        public ConfigManagerBase()
        {
        }

        #endregion

        #region Public Instance Methods

        public abstract IViewFactory GetViewFactory(string factoryName, IDictionary<string,object> createParams);

        public abstract IRollConfig GetRollConfig(string rollName);

        public abstract ITaskGroupConfig[] GetTaskGroups();

        public abstract ITaskConfig[] GetTasks();

        #endregion

        #region Public Static Methods

        public static void InitGridView(DataGridView rollGrid, IRollConfig rc, Filter filter)
        {
            ICursor cur = Program.Database.EntityStorage.RetrieveMultiple(rc.EntityType, filter);

            rollGrid.Columns.Clear();
            rollGrid.AutoGenerateColumns = false;
            rollGrid.DataSource = cur.DataSource;

            foreach (IRollColumnConfig rcc in rc.Columns) {
                int index = rollGrid.Columns.Add(rcc.FieldName, rcc.DisplayName);
                DataGridViewColumn gridColumn = rollGrid.Columns[index];

                gridColumn.DataPropertyName = rcc.FieldName;

                if (rcc.Width >= 0)
                    gridColumn.Width = rcc.Width;
                else {
                    gridColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    gridColumn.FillWeight = -rcc.Width;
                }
            }
        }

        #endregion
    }
}
