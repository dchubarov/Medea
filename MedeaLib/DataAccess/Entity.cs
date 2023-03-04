// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Entity.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace Gyrosoft.Medea.DataAccess
{
    /// <summary>
    /// Represents generic entity.
    /// </summary>
    public class Entity
    {
        #region Public Events

        public delegate void DataChangeEvent(object sender);
        public event DataChangeEvent DataChange = null;

        #endregion

        #region Protected Fields

        protected const string NullString = "(null)";

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Entity object.
        /// </summary>
        public Entity()
        {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a display name for entity.
        /// </summary>
        public virtual string GetDisplayName()
        {
            return String.Empty;
        }

        #endregion

        #region Protected Methods

        protected virtual void OnDataChange()
        {
            if (this.DataChange != null) {
                this.DataChange(this);
            }
        }

        #endregion
    }
}
