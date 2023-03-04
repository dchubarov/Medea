// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      EntityStorage.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
// db4objects
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Types;
// medea
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.Logging;

namespace Gyrosoft.Medea.DataAccess.Embedded
{
    /// <summary>
    /// Implements IEntityStorage for embedded database engine (db4o)
    /// </summary>
    public class EntityStorage : IEntityStorage
    {
        #region Private Fields

        private bool _autoCommit = false;
        private Database _database = null;
        private IObjectContainer _container = null;

        #endregion

        #region Construction

        internal EntityStorage(Database database)
        {
            _database = database;
        }

        #endregion

        #region Internal Methods

        internal void Open(string path)
        {
            this.Database.Logger.Info("Opening object storage...");

            if (_container != null)
                this.Database.Logger.Debug("Storage is already open.");
            else {
                this.Database.Logger.Indent();
                try {
                    // perform database engine configuration
                    this.Database.Logger.Info("Configuring database engine...");
                    Db4oFactory.Configure().Queries().EvaluationMode(QueryEvaluationMode.LAZY);

                    // make path to database file from working directory
                    // given in database connection string
                    if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()) &&
                        !path.EndsWith(Path.AltDirectorySeparatorChar.ToString())) {
                        path += Path.DirectorySeparatorChar;
                    }

                    path += "entity.db";
                    bool createDataFile = !File.Exists(path);
                    this.Database.Logger.Info("Data file name set to '" + path + "' (" +
                        (createDataFile ? "File does not exists" : "File exists") + ").");

                    if (createDataFile)
                        this.Database.Logger.Info("Creating data file...");
                    else
                        this.Database.Logger.Info("Opening data file...");

                    // open database file
                    if ((_container = Db4oFactory.OpenFile(path)) == null) {
                        throw new NullReferenceException();
                    }

                    // configure the container
                    this.Database.Logger.Info("Configuring object storage...");

                    // configure new database
                    if (createDataFile) {
                        BeginTrans();

                        Clinic cli = new Clinic();
                        cli.Name = Properties.Resources.DefaultClinicName;
                        Store(cli);

                        CommitTrans();
                    }
                }
                catch (Exception exc) {
                    this.Database.Logger.Fatal("Could not open object storage. Exception caught: '" + exc.ToString() + "'.");
                    throw new Exception(Properties.Resources.ErrorEDBOpen, exc);
                }
                finally {
                    this.Database.Logger.UnIndent();
                }
            }

            this.Database.Logger.Info("Object storage open.");
        }

        internal void Close()
        {
            this.Database.Logger.Info("Closing object storage...");

            if (_container == null)
                this.Database.Logger.Debug("Database is not open.");
            else {
                this.Database.Logger.Indent();
                try {
                    // commit last transaction
                    this.Database.Logger.Info("Commiting changes...");
                    _container.Commit();

                    // close data file
                    this.Database.Logger.Info("Closing data file...");
                    _container.Close();

                    // data file closed
                    _container = null;
                }
                catch (Exception exc) {
                    this.Database.Logger.Error("Could not close entity storage. Exception caught: " + exc.ToString());
                    throw new Exception(Properties.Resources.ErrorEDBClose, exc);
                }
                finally {
                    this.Database.Logger.UnIndent();
                }

                this.Database.Logger.Info("Object storage closed.");
            }
        }

        internal Database Database
        {
            get
            {
                return _database;
            }
        }

        #endregion

        #region IEntityStorage Members

        public bool Transactional
        {
            get
            {
                // db4o always supports transactions
                return true;
            }
        }

        public bool AutoCommit
        {
            get
            {
                return _autoCommit;
            }
            set
            {
                _autoCommit = value;
            }
        }

        public void BeginTrans()
        {
            if (_container == null) {
                throw new InvalidOperationException(Properties.Resources.ErrorEDBNotOpen);
            }

            this.Database.Logger.Debug("Begin new transaction.");
            // do nothing; db4o is always in transaction mode
        }

        public void CommitTrans()
        {
            if (_container == null) {
                throw new InvalidOperationException(Properties.Resources.ErrorEDBNotOpen);
            }

            this.Database.Logger.Debug("Commit transaction...");
            try {
                _container.Commit();
            }
            catch (Exception exc) {
                this.Database.Logger.Error("Could not commit changes. Exception caught: " + exc.Message);
                throw new Exception(Properties.Resources.ErrorEDBCommit, exc);
            }
        }

        public void RollbackTrans()
        {
            if (_container == null) {
                throw new InvalidOperationException(Properties.Resources.ErrorEDBNotOpen);
            }

            this.Database.Logger.Debug("Rollback transaction...");
            try {
                _container.Rollback();
            }
            catch (Exception exc) {
                this.Database.Logger.Error("Could not rollback changes. Exception caught: " + exc.Message);
                throw new Exception(Properties.Resources.ErrorEDBRollback, exc);
            }
        }

        public IIdentity IdentityOf(Entity entity)
        {
            return new Identity(_container.Ext().GetID(entity));
        }

        public Entity Retrieve(IIdentity identity)
        {
            return this.Retrieve(identity, null);
        }

        public Entity Retrieve(IIdentity identity, Type entityType)
        {
            // check identity type
            if (identity.GetType() != typeof(Identity)) {
                throw new ArgumentException("Invalid identity type.", "identity");
            }

            // check entity type
            if (entityType != null && !entityType.IsSubclassOf(typeof(Entity))) {
                throw new ArgumentException("Invalid entity type.", "entityType");
            }

            // obtain internal identifier and retrieve entity from database
            if (!identity.IsNull) {
                long id = ((Identity)identity).ID;
                object obj = _container.Ext().GetByID(id);

                if (obj != null) {


                    return (obj as Entity);
                }
            }

            return null;
        }

        public ICursor RetrieveMultiple(Type entityType, Filter filter)
        {
            // create main query and constrain it to entity type
            IQuery mainQuery = _container.Query();
            mainQuery.Constrain(entityType);

            // apply filter to query
            if (filter != null) {
                IEnumerable<Filter> fe;
                if (filter.IsComplex)
                    fe = filter.Filters;
                else {
                    Filter[] fa = new Filter[1];
                    fa[0] = filter;
                    fe = fa;
                }

                foreach (Filter subFilter in fe) {
                    IQuery subQuery = mainQuery.Descend(subFilter.Field);
                    if (subFilter.Condition == FilterCondition.OrderAscending) {
                        subQuery.OrderAscending();
                    }
                    else if (subFilter.Condition == FilterCondition.OrderDescending) {
                        subQuery.OrderDescending();
                    }
                    else {
                        IConstraint c = subQuery.Constrain(subFilter.Criteria);
                        switch (subFilter.Condition) {
                            case FilterCondition.Equal:
                                c = c.Equal();
                                break;
                            case FilterCondition.EqualRef:
                                c = subFilter.Criteria == null ? c.Equal() : c.Identity();
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                }
            }

            return (new Cursor(mainQuery.Execute()));
        }

        public void Store(Entity entity)
        {
            this.Database.Logger.Debug("Storing entity " + entity.GetType().Name + "=" + entity.ToString());

            _container.Set(entity);

            if (_autoCommit) {
                _container.Commit();
            }
        }

        public void Delete(IIdentity identity)
        {
            if (identity.GetType() != typeof(Identity)) {
                throw new ArgumentException("Invalid identity type.", "identity");
            }

            if (((Identity)identity).IsNull) {
                throw new ArgumentException("Null identity.", "identity");
            }

            this.Database.Logger.Debug("Request delete by ID: " + identity.ToString());
            Entity entity = _container.Ext().GetByID(((Identity)identity).ID) as Entity;
            if (entity == null) {
                throw new Exception(Properties.Resources.ErrorEDBIdentityNotFound);
            }

            this.Delete(entity);
        }

        public void Delete(Entity entity)
        {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            this.Database.Logger.Debug("Deleting entity " + entity.GetType().ToString() + "=" + entity.ToString());
            try {
                _container.Delete(entity);
                if (this.AutoCommit) {
                    _container.Commit();
                }
            }
            catch (Exception exc) {
                this.Database.Logger.Error("Could not delete. Exception caught: " + exc.Message);
                throw new Exception(Properties.Resources.ErrorEDBDelete, exc);
            }
        }

        #endregion
    }
}
