// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ImageCollectionListViewItem.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Imaging;

namespace Gyrosoft.Medea.Gui
{
    public class ImageCollectionListViewItem : ListViewItem
    {
        #region Private Fields

        private Thumbnail _thumbnail = null;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new ImageCollectionListViewItem object.
        /// </summary>
        public ImageCollectionListViewItem(ImageCollectionItem _source) : base()
        {
            _thumbnail = new Thumbnail(this, _source);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets Thumbnail object for current item.
        /// </summary>
        public Thumbnail Thumbnail
        {
            get
            {
                return _thumbnail;
            }
        }

        #endregion
    }
}