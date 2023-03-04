// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ImageCollectionItem.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.BusinessModel;
using Gyrosoft.Medea.DataAccess;

namespace Gyrosoft.Medea.Imaging
{
    /// <summary>
    /// Represents item of the ImageCollection collection.
    /// </summary>
    public sealed class ImageCollectionItem
    {
        #region Private Fields

        ImageCollection _collection = null;
        Snapshot _snapshot = null;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new ImageCollectionItem object.
        /// </summary>
        public ImageCollectionItem(ImageCollection collection, Snapshot snapshot)
        {
            if (collection == null) {
                throw new ArgumentNullException("collection");
            }

            _collection = collection;
            _snapshot = snapshot;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns ImageCollection object containing current item.
        /// </summary>
        public ImageCollection Collection
        {
            get
            {
                return _collection;
            }
        }

        public Snapshot Snapshot
        {
            get
            {
                return _snapshot;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the image.
        /// </summary>
        /// <returns>Image object or null if error occur.</returns>
        public Image GetImage()
        {
            Stream blobStream = Program.Database.BlobStorage.GetStream(_snapshot, true);
            Image img;

            try {
                img = Bitmap.FromStream(blobStream);
            }
            finally {
                blobStream.Close();
            }
            return img;
        }

        public Image GetThumbnailImage(int width, int height)
        {
            Stream blobStream = Program.Database.BlobStorage.GetStream(_snapshot, true);
            try {
                using (Image img = Bitmap.FromStream(blobStream)) {
                    Image imgT = img.GetThumbnailImage(width, height, GetThumbnailImageAbort, IntPtr.Zero);
                    return imgT;
                }
            }
            finally {
                blobStream.Close();
            }
        }

        private bool GetThumbnailImageAbort()
        {
            return false;
        }

        #endregion
    }
}