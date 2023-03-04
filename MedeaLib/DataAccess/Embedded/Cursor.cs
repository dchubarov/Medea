// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Cursor.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
// db4objects
using Db4objects.Db4o;
using Db4objects.Db4o.Types;
// medea
using Gyrosoft.Medea.DataAccess;

namespace Gyrosoft.Medea.DataAccess.Embedded
{
    /// <summary>
    /// Implements ICursor for embedded database engine (db4o).
    /// </summary>
    public class Cursor : ICursor
    {
        #region Private Fields

        private IObjectSet _resultSet = null;
        private int _index = 0;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new empty Db4oCursor object.
        /// </summary>
        public Cursor()
        {
        }

        /// <summary>
        /// Constructs new Db4oCursor object.
        /// </summary>
        /// <param name="resultSet">Db4o result set to wrap.</param>
        internal Cursor(IObjectSet resultSet)
        {
            _resultSet = resultSet;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region ICursor Members

        public bool Eof
        {
            get
            {
                return (_resultSet == null || _index >= _resultSet.Count);
            }
        }

        public int Count
        {
            get
            {
                return ((_resultSet == null) ? 0 : _resultSet.Count);
            }
        }

        public object DataSource
        {
            get
            {
                return _resultSet;
            }
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get
            {
                return (object)this.Current;
            }
        }

        public bool MoveNext()
        {
            if (_resultSet != null) {
                _index++;
                if (_index < _resultSet.Count - 1) {
                    return true;
                }
            }

            return false;
        }

        public void Reset()
        {
            _index = 0;
        }

        #endregion

        #region IEnumerator<Entity> Members

        public Entity Current
        {
            get
            {
                return ((_resultSet == null) ? null : (Entity)_resultSet[_index]);
            }
        }

        #endregion
    }
}
