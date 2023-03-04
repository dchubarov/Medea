// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IndicationCategory.cs
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
    /// Encapsulates properties of indication category.
    /// </summary>
    public class IndicationCategory : NamedEntity
    {
        #region Private Fields

        private ExaminationKind _examinationKind = null;
        private IndicationCategory _parentCategory = null;
        private DateTime _createDate = DateTime.Now;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new IndicationCategory object.
        /// </summary>
        public IndicationCategory() : base()
        {
        }

        /// <summary>
        /// Constructs new IndicationCategory object.
        /// </summary>
        /// <param name="name">The name of indication category.</param>
        public IndicationCategory(string name) : base(name)
        {
        }

        /// <summary>
        /// Constructs new IndicationCategory object.
        /// </summary>
        /// <param name="name">The name of indication category.</param>
        /// <param name="examinationKind">Examination kind the current category belongs to.</param>
        public IndicationCategory(string name, ExaminationKind examinationKind) : base(name)
        {
            _examinationKind = examinationKind;
        }

        /// <summary>
        /// Constructs new IndicationCategory object.
        /// </summary>
        /// <param name="name">The name of indication category.</param>
        /// <param name="examinationKind">Examination kind the current category belongs to.</param>
        /// <param name="parentCategory">Category the current category belongs to.</param>
        public IndicationCategory(string name, ExaminationKind examinationKind, IndicationCategory parentCategory) : base(name)
        {
            _examinationKind = examinationKind;
            _parentCategory = parentCategory;
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

        public IndicationCategory ParentCategory
        {
            get
            {
                return _parentCategory;
            }

            set
            {
                if (!Object.ReferenceEquals(_parentCategory, value)) {
                    _parentCategory = value;
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
            return String.Format("(base%NamedEntity={0}, ExaminationKind={1}, ParentCategory={2})", 
                base.ToString(), 
                _examinationKind == null ? Entity.NullString : _examinationKind.ToString(),
                _parentCategory == null ? Entity.NullString : _parentCategory.ToString()
                );
        }

        #endregion
    }
}
