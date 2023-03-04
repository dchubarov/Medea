using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.Config
{
    public interface IRollColumnConfig
    {
        /// <summary>
        /// Gets column's display name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets field name the column is bound to.
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Get or sets column width.
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets column order number.
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// Gets or sets column visibility.
        /// </summary>
        bool Visible { get; set; }
    }
}
