// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Thumbnail.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Gui;

namespace Gyrosoft.Medea.Imaging
{
    /// <summary>
    /// Represents a thumbnail.
    /// </summary>
    public class Thumbnail
    {
        #region Private Fields

        private ImageCollectionItem _source = null;
        private ListViewItem _listItem = null;
        private bool _createError = false;
        private byte[] _imageData = null;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new Thumbnail object.
        /// </summary>
        /// <param name="source">ImageCollectionItem object that represents source image.</param>
        public Thumbnail(ListViewItem listItem, ImageCollectionItem source)
        {
            _source = source;
            _listItem = listItem;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets an ImageCollectionItem object that represents source image.
        /// </summary>
        public ImageCollectionItem Source
        {
            get
            {
                return _source;
            }
        }

        /// <summary>
        /// Gets the ListViewItem object thumbnail belongs to.
        /// </summary>
        public ListViewItem ListItem
        {
            get
            {
                return _listItem;
            }
        }

        /// <summary>
        /// Gets a value indicating that error occured 
        /// during creating thumbnail from source image.
        /// </summary>
        public bool CreateError
        {
            get
            {
                return _createError;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves thumbnail image.
        /// </summary>
        /// <returns>Image object or null if error occur.</returns>
        public Image GetThumbnailImage()
        {
            if (_imageData != null && _imageData.Length > 0) {
                MemoryStream ms = new MemoryStream(_imageData);
                return new Bitmap(ms);
            }

            return null;
        }

        /// <summary>
        /// Creates thumbnail image from source image.
        /// </summary>
        public void CreateFromSource()
        {
            _createError = true;

            try {
                Size sizeThumb = new Size(
                    ImageCollectionListView.ThumbnailDefaultSize, 
                    ImageCollectionListView.ThumbnailDefaultSize
                    );

                if (this.ListItem != null && (this.ListItem.ListView is ImageCollectionListView)) {
                    sizeThumb.Width = sizeThumb.Height = (this.ListItem.ListView as ImageCollectionListView).ThumbnailSize;
                }

                if (this.Source != null) {
                    using (Image sourceImage = this.Source.GetImage()) {
                        sizeThumb = FitSize(sourceImage.Size, sizeThumb, true, true);
                        using (Image thumbnailImage = this.Source.GetThumbnailImage(sizeThumb.Width, sizeThumb.Height)) {
                            MemoryStream ms = new MemoryStream();
                            thumbnailImage.Save(ms, ImageFormat.Jpeg);
                            _imageData = ms.GetBuffer();
                            _createError = false;
                        }
                    }
                }

                /*
                if (this.Source != null) {
                    using (Image sourceImage = this.Source.GetImage()) {
                        sizeThumb = FitSize(sourceImage.Size, sizeThumb, true, true);
                        using (Image thumbnailImage = sourceImage.GetThumbnailImage(sizeThumb.Width, sizeThumb.Height, GetThumbnailImageAbort, IntPtr.Zero)) {
                            MemoryStream ms = new MemoryStream();
                            thumbnailImage.Save(ms, ImageFormat.Jpeg);
                            _imageData = ms.GetBuffer();
                            _createError = false;
                        }
                    }
                }
                */
            }
            catch (Exception) {
                // do nothing
            }
        }

        public static Size FitSize(Size sizeToFit, Size sizeToFitIn, bool reduceOnly, bool keepAspectRatio)
        {
            Size sizeFitted = new Size();

            if (reduceOnly &&
                sizeToFit.Width <= sizeToFitIn.Width &&
                sizeToFit.Height <= sizeToFitIn.Height)
                sizeFitted = sizeToFit;
            else {
                if (!keepAspectRatio) {
                    sizeFitted = sizeToFitIn;
                }
                else {
                    float ratio = (float)sizeToFit.Width / sizeToFit.Height;
                    if (ratio >= (float)sizeToFitIn.Width / sizeToFitIn.Height) {
                        sizeFitted.Width = sizeToFitIn.Width;
                        sizeFitted.Height = (int)(sizeFitted.Width / ratio);
                    }
                    else {
                        sizeFitted.Height = sizeToFitIn.Height;
                        sizeFitted.Width = (int)(sizeFitted.Height * ratio);
                    }
                }
            }
            return sizeFitted;
        }

        #endregion

        #region Private Methods

        private bool GetThumbnailImageAbort()
        {
            return false;
        }

        #endregion
    }
}