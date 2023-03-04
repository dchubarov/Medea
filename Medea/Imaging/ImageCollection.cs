// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ImageCollection.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.BusinessModel;

namespace Gyrosoft.Medea.Imaging
{
    /// <summary>
    /// Represents collection of images.
    /// </summary>
    public class ImageCollection : CollectionBase
    {
        #region Construction

        /// <summary>
        /// Constructs new ImageCollection object.
        /// </summary>
        public ImageCollection() : base()
        {
        }

        /// <summary>
        /// Constructs new ImageCollection object with initial capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity for the collection.</param>
        public ImageCollection(int capacity) : base(capacity)
        {
        }

        #endregion

        #region Public Properties

        public ImageCollectionItem this[int index]
        {
            get
            {
                return (ImageCollectionItem)List[index];
            }
        }

        #endregion

        #region Public Methods

        public bool Contains(ImageCollectionItem item)
        {
            return List.Contains(item);
        }

        public int IndexOf(ImageCollectionItem item)
        {
            return List.IndexOf(item);
        }

        public ImageCollectionItem Add()
        {
            return this.Add(null);
        }

        public ImageCollectionItem Add(Snapshot snapshot)
        {
            ImageCollectionItem newItem = new ImageCollectionItem(this, snapshot);
            List.Add(newItem);
            return newItem;
        }

        #endregion

        #region Protected Methods

        protected override void OnValidate(object item)
        {
            if (item.GetType() != typeof(ImageCollectionItem)) {
                throw new Exception("ImageCollection: invalid item type.");
            }
        }

        #endregion
    }
}