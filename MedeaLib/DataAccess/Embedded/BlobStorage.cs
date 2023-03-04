// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      BlobStorage.cs
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
using Gyrosoft.Medea.BusinessModel;

namespace Gyrosoft.Medea.DataAccess.Embedded
{
    public class BlobStorage : IBlobStorage
    {
        #region Private Fields

        private Database _database = null;
        private string _basePath = null;

        #endregion

        #region Construction

        internal BlobStorage(Database database)
        {
            _database = database;
        }

        #endregion

        #region Internal Methods

        internal void Open(string path)
        {
            _database.Logger.Info("Opening blob storage...");
            _database.Logger.Indent();
            try {
                if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()) &&
                    !path.EndsWith(Path.AltDirectorySeparatorChar.ToString())) {
                    path += Path.DirectorySeparatorChar;
                }
                path += @"blobs\";
                _database.Logger.Info("Blog storage directory: '" + path + "'.");

                if (!Directory.Exists(path)) {
                    _database.Logger.Info("Creating directory '" + path + "'...");
                    Directory.CreateDirectory(path);
                }

                _basePath = path;
            }
            finally {
                _database.Logger.UnIndent();
            }

            _database.Logger.Info("Blob storage open.");
        }

        #endregion

        #region IBlobStorage Members

        public Stream GetStream(Entity entity, bool forReading)
        {
            if (entity == null || !(entity is Snapshot)) {
                throw new InvalidOperationException();
            }

            Snapshot snap = (Snapshot)entity;

            IIdentity id = _database.EntityStorage.IdentityOf(snap.Examination);
            string path = _basePath + id.ToString() + Path.DirectorySeparatorChar;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            id = _database.EntityStorage.IdentityOf(snap);
            path += "snp" + id.ToString() + ".blob";

            FileStream fs = new FileStream(path, forReading ? FileMode.Open : FileMode.CreateNew);
            return fs;
        }

        public void DeleteBlob(Entity entity)
        {
            if (entity == null || !(entity is Snapshot)) {
                throw new InvalidOperationException();
            }

            Snapshot snap = (Snapshot)entity;

            IIdentity id = _database.EntityStorage.IdentityOf(snap.Examination);
            string path = _basePath + id.ToString() + Path.DirectorySeparatorChar;
            id = _database.EntityStorage.IdentityOf(snap);
            path += "snp" + id.ToString() + ".blob";

            if (File.Exists(path)) {
                File.Delete(path);
            }
        }

        #endregion
    }
}
