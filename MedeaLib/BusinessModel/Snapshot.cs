// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Snapshot.cs
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
    /// Encapsulates properties of a snapshot.
    /// </summary>
    public class Snapshot : Entity
    {
        #region Private Fields

        private Examination _examination = null;
        private string _comment = String.Empty;
        private DateTime _date = DateTime.Now;
        private int _imageWidth = 0;
        private int _imageHeight = 0;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Snapshot object.
        /// </summary>
        public Snapshot() : base()
        {
        }

        public Snapshot(Examination examination) : base()
        {
            _examination = examination;
        }

        public Snapshot(Examination examination, int imageWidth, int imageHeight) : base()
        {
            _examination = examination;
            _imageWidth = imageWidth;
            _imageHeight = imageHeight;
        }

        #endregion

        #region Public Properties

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

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                if (_date != value) {
                    _date = value;
                    OnDataChange();
                }
            }
        }

        public int ImageWidth
        {
            get
            {
                return _imageWidth;
            }

            set
            {
                if (_imageWidth != value) {
                    _imageWidth = value;
                    OnDataChange();
                }
            }
        }

        public int ImageHeight
        {
            get
            {
                return _imageHeight;
            }

            set
            {
                if (_imageHeight != value) {
                    _imageHeight = value;
                    OnDataChange();
                }
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("(Examination={0}, Comment={1})",
                _examination == null ? Entity.NullString : _examination.ToString(),
                _comment
                );
        }

        #endregion
    }
}