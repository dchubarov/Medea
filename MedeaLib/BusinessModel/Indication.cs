// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Indication.cs
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
    /// Encapsulates properties of an indication.
    /// </summary>
    public class Indication : Entity
    {
        #region Private Fields

        private IndicationDefinition _definition = null;
        private Examination _examination = null;
        private string _comment = String.Empty;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Indication object.
        /// </summary>
        public Indication() : base()
        {
        }

        /// <summary>
        /// Constructs new Indication object.
        /// </summary>
        /// <param name="definition">Definition for current indication.</param>
        public Indication(IndicationDefinition definition) : base()
        {
            _definition = definition;
        }

        /// <summary>
        /// Constructs new Indication object.
        /// </summary>
        /// <param name="definition">Definition for current indication.</param>
        /// <param name="examination">Examination the current indication belongs to.</param>
        public Indication(IndicationDefinition definition, Examination examination) : base()
        {
            _definition = definition;
            _examination = examination;
        }

        #endregion

        #region Public Properties

        public IndicationDefinition Definition
        {
            get
            {
                return _definition;
            }

            set
            {
                if (!Object.ReferenceEquals(_definition, value)) {
                    _definition = value;
                    OnDataChange();
                }
            }
        }

        public Examination Examination
        {
            get
            {
                return _examination;
            }

            set
            {
                if (!Object.ReferenceEquals(_examination, value)) {
                    _examination = value;
                    OnDataChange();
                }
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment != value) {
                    _comment = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(Definition={0}, Examination={1}, Comment={2})",
                _definition == null ? Entity.NullString : _definition.ToString(),
                _examination == null ? Entity.NullString : _examination.ToString(),
                _comment
                );
        }

        #endregion
    }
}
