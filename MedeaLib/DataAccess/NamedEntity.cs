// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      NamedEntity.cs
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
    /// Encapsulates properties of simple named object.
    /// </summary>
    public class NamedEntity : Entity
    {
        #region Private Fields

        private string _name = String.Empty;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new NamedEntity object.
        /// </summary>
        public NamedEntity() : base()
        {
        }

        /// <summary>
        /// Constructs new NamedEntity object with field initialization.
        /// </summary>
        /// <param name="name">Entity name.</param>
        public NamedEntity(string name) : base()
        {
            _name = name;
        }

        #endregion

        #region Public Properties

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name != value) {
                    _name = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string GetDisplayName()
        {
            return this.Name;
        }

        public override string ToString()
        {
            return String.Format("(Name={0})", _name);
        }

        #endregion
    }
}
