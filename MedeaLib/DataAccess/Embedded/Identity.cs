// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Identity.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;
// medea
using Gyrosoft.Medea.DataAccess;

namespace Gyrosoft.Medea.DataAccess.Embedded
{
    /// <summary>
    /// Implements IIdentity for embedded database engine (db4o).
    /// </summary>
    public class Identity : IIdentity
    {
        #region Private Fields

        private long? _id = null;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new empty Db4oIdentity object.
        /// </summary>
        public Identity()
        {
        }

        /// <summary>
        /// Constructs new Db4oIdentity object.
        /// </summary>
        /// <param name="id">Object id.</param>
        internal Identity(long? id)
        {
            _id = (id.HasValue && id.Value > 0) ? id : null;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets underlying object id.
        /// </summary>
        internal long ID
        {
            get
            {
                return _id.Value;
            }

            set
            {
                _id = value;
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return (this.IsNull ? "(null)" : this.ID.ToString());
        }

        #endregion

        #region IIdentity Members

        public bool IsNull
        {
            get
            {
                return !(_id.HasValue);
            }
        }

        #endregion
    }
}
