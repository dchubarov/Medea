// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IndicationDefinition.cs
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

namespace Gyrosoft.Medea.BusinessModel
{
    /// <summary>
    /// Encapsulates properties of an indication definition.
    /// </summary>
    public class IndicationDefinition : NamedEntity
    {
        #region Private Fields

        private ExaminationKind _examinationKind = null;
        private IndicationCategory _category = null;
        private DateTime _createDate = DateTime.Now;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new IndicationDefinition object.
        /// </summary>
        public IndicationDefinition() : base()
        {
        }

        /// <summary>
        /// Constructs new IndicationDefinition object with field initialization.
        /// </summary>
        /// <param name="name">Entity name.</param>
        public IndicationDefinition(string name) : base(name)
        {
        }

        /// <summary>
        /// Constructs new IndicationDefinition object with field initialization.
        /// </summary>
        /// <param name="name">Entity name.</param>
        /// <param name="examinationKind">Examination kind the current definition belongs to.</param>
        public IndicationDefinition(string name, ExaminationKind examinationKind) : base(name)
        {
            _examinationKind = examinationKind;
        }

        /// <summary>
        /// Constructs new IndicationDefinition object with field initialization.
        /// </summary>
        /// <param name="name">Entity name.</param>
        /// <param name="examinationKind">Examination kind the current definition belongs to.</param>
        /// <param name="category">IndicationCategory the current definition belongs to.</param>
        public IndicationDefinition(string name, ExaminationKind examinationKind, IndicationCategory category) : base(name)
        {
            _examinationKind = examinationKind;
            _category = category;
        }

        #endregion

        #region Public Properties

        public ExaminationKind ExaminationKind
        {
            get
            {
                return _examinationKind;
            }

            set
            {
                if (!Object.ReferenceEquals(_examinationKind, value)) {
                    _examinationKind = value;
                    OnDataChange();
                }
            }
        }

        public IndicationCategory Category
        {
            get
            {
                return _category;
            }

            set
            {
                if (!Object.ReferenceEquals(_category, value)) {
                    _category = value;
                    OnDataChange();
                }
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }

            set
            {
                if (_createDate != value) {
                    _createDate = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(base%NamedEntity={0}, ExaminationKind={1}, Category={2})",
                base.ToString(),
                _examinationKind == null ? Entity.NullString : _examinationKind.ToString(),
                _category == null ? Entity.NullString : _category.ToString()
                );
        }

        #endregion
    }
}
