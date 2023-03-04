using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.Config
{
    public interface ITaskGroupConfig
    {
        string GroupName { get; }

        string DisplayName { get; }
    }
}
