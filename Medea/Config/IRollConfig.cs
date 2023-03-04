using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.Config
{
    public interface IRollConfig
    {
        /// <summary>
        /// Gets display name for the roll.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets type of business object.
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// Gets array of column configuration objects.
        /// </summary>
        IRollColumnConfig[] Columns { get; }
    }
}
