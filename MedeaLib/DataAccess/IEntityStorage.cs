// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IEntityStorage.cs
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
    /// Provides methods to access object data in a database.
    /// </summary>
    public interface IEntityStorage
    {
        /// <summary>
        /// Gets a value indicating that database supports transactions.
        /// </summary>
        bool Transactional
            { get; }

        /// <summary>
        /// Gets a value indicating that changes should be commited automatically.
        /// </summary>
        bool AutoCommit
            { get; set; }

        /// <summary>
        /// Begins a transaction.
        /// </summary>
        void BeginTrans();

        /// <summary>
        /// Commits transaction.
        /// </summary>
        void CommitTrans();

        /// <summary>
        /// Roll back transaction.
        /// </summary>
        void RollbackTrans();

        /// <summary>
        /// Returns an identity for given entity.
        /// </summary>
        /// <param name="entity">Entity to retrieve identity of.</param>
        /// <returns>
        /// Returns IIdentity object represents identity of given 
        /// entity or null if error occur.
        /// </returns>
        IIdentity IdentityOf(Entity entity);

        /// <summary>
        /// Retrieves an entity from database using identity.
        /// </summary>
        /// <param name="identity">Identity to look-up.</param>
        /// <returns>Returns Entity object or null if not found or error occur.</returns>
        Entity Retrieve(IIdentity identity);

        /// <summary>
        /// Retrieves an entity from database using identity and check entity type.
        /// </summary>
        /// <param name="identity">Identity to look-up.</param>
        /// <param name="entityType">System.Type to check for retrieved entity.</param>
        /// <returns></returns>
        Entity Retrieve(IIdentity identity, Type entityType);

        /// <summary>
        /// Retrieves multiple entities from database.
        /// </summary>
        /// <param name="entityType">System.Type representing type of entity to retrieve.</param>
        /// <param name="filter">Filter object to constrain result set in some way.</param>
        /// <returns>Returns ICursor object that represents a result set.</returns>
        ICursor RetrieveMultiple(Type entityType, Filter filter);

        /// <summary>
        /// Stores an entity into database.
        /// </summary>
        /// <param name="entity">Entity object to store.</param>
        void Store(Entity entity);

        /// <summary>
        /// Deletes an entity from database.
        /// </summary>
        /// <param name="entityId">Identity of an entity to delete.</param>
        void Delete(IIdentity entityId);

        /// <summary>
        /// Deletes an entity from database.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        void Delete(Entity entity);
    }
}
