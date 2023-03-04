// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Database.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
// medea
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Logging;

namespace Gyrosoft.Medea.DataAccess.Embedded
{
    /// <summary>
    /// Implements IDatabase for embedded database engine (db4o).
    /// </summary>
    public class Database : IDatabase
    {
        #region Private Fields

        private ILogger _logger = null;
        private string _connectionString = null;
        private EntityStorage _es = null;
        private BlobStorage _bs = null;

        #endregion

        #region Construction

        public Database()
        {
        }

        #endregion

        #region Internal Methods

        #endregion

        #region Private Methods

        private static ILogger GetDefaultLogger()
        {
            return new TraceLogger();
        }

        #endregion

        #region IDatabase Members

        public void Open()
        {
            if (_es == null) {
                if (this.Logger == null) {
                    this.Logger = GetDefaultLogger();
                }

                try {
                    // check whether the connection string is not empty
                    this.Logger.Info("Connection string is '" + this.ConnectionString + "'.");
                    if (this.ConnectionString == null || this.ConnectionString.Length < 1) {
                        throw new Exception(Properties.Resources.ErrorEDBInvalidConnectionString);
                    }

                    // check whether directory specified by connection string exists
                    this.Logger.Info("Checking database directory...");
                    if (!Directory.Exists(this.ConnectionString)) {
                        throw new Exception(Properties.Resources.ErrorEDBInvalidDirectory);
                    }

                    // open an entity storage
                    _es = new EntityStorage(this);
                    _es.Open(this.ConnectionString);

                    // open a blob storage
                    _bs = new BlobStorage(this);
                    _bs.Open(this.ConnectionString);
                }
                catch (Exception exc) {
                    this.Logger.Fatal("Could not open database. Exception caught: '" + exc.ToString() + "'.");
                    throw;
                }

                this.Logger.Info("Database open.");
            }
        }

        public void Close()
        {
            if (_es != null) {
                try {


                    // close object storage
                    _es.Close();
                    _es = null;
                }
                catch (Exception exc) {
                    this.Logger.Fatal("Could not close database. Exception caught: '" + exc.ToString() + "'.");
                    throw;
                }

                this.Logger.Info("Database closed.");
            }
        }

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }

            set
            {
                _connectionString = value;
            }
        }

        public ILogger Logger
        {
            get
            {
                if (_logger == null) {
                    _logger = new TraceLogger();
                }

                return _logger;
            }
            set
            {
                _logger = value;
            }
        }


        public IEntityStorage EntityStorage
        {
            get
            {
                return _es;
            }
        }

        public IBlobStorage BlobStorage
        {
            get
            {
                return _bs;
            }
        }

        #endregion
    }
}