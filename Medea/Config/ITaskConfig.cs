using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Gyrosoft.Medea.Config
{
    public interface ITaskConfig
    {
        string TaskName { get; }

        string GroupName { get; }

        string DisplayName { get; }

        string FactoryName { get; }

        Image Icon { get; }
    }
}
