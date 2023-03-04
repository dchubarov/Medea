// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      ImageCollectionListView.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Imaging;

namespace Gyrosoft.Medea.Gui
{
    /// <summary>
    /// Extends ListView to meet <see cref="ImagingControl">ImagingControl</see> requirments.
    /// </summary>
    public partial class ImageCollectionListView : ListView
    {
        #region Public Constants

        public const int ThumbnailMinSize = 32;
        public const int ThumbnailMaxSize = 256;
        public const int ThumbnailDefaultSize = 96;
        public const int ThumbnailDefaultMargin = 8;
        public const int ThumbnailDefaultBorderWidth = 2;

        #endregion

        #region Private Fields

        private int _thumbnailSize = ThumbnailDefaultSize;
        private int _thumbnailMargin = ThumbnailDefaultMargin;
        private int _thumbnailBorderWidth = ThumbnailDefaultBorderWidth;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new ImageCollectionListView object.
        /// </summary>
        public ImageCollectionListView()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets size of thumbnail.
        /// </summary>
        [Category("Appearance"), DefaultValue(ImageCollectionListView.ThumbnailDefaultSize)]
        public int ThumbnailSize
        {
            get
            {
                return _thumbnailSize;
            }

            set
            {
                if (value < ThumbnailMinSize || value > ThumbnailMaxSize) {
                    throw new Exception("ThumbnailSize: invalid property value.");
                }

                _thumbnailSize = value;
                this.SetupControlImageList();
            }
        }

        /// <summary>
        /// Gets or sets thumbnail margin size.
        /// </summary>
        [Category("Appearance"), DefaultValue(ImageCollectionListView.ThumbnailDefaultMargin)]
        public int ThumbnailMargin
        {
            get
            {
                return _thumbnailMargin;
            }

            set
            {

                _thumbnailMargin = value;
                this.SetupControlImageList();
            }
        }

        /// <summary>
        /// Gets or sets thumbnail border width.
        /// </summary>
        [Category("Appearance"), DefaultValue(ImageCollectionListView.ThumbnailDefaultBorderWidth)]
        public int ThumbnailBorderWidth
        {
            get
            {
                return _thumbnailBorderWidth;
            }

            set
            {

                _thumbnailBorderWidth = value;
                this.SetupControlImageList();
            }
        }

        #endregion

        #region Protected Methods

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetupControlImageList();
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (this.View != View.LargeIcon)
                e.DrawDefault = true;
            else {
                Rectangle rectDraw = new Rectangle(
                    e.Bounds.Left + (e.Bounds.Width - this.ThumbnailSize) / 2,
                    e.Bounds.Top + this.ThumbnailMargin + this.ThumbnailBorderWidth,
                    this.ThumbnailSize,
                    this.ThumbnailSize
                    );

                Image thumbnailImage = null;

                if (e.Item is ImageCollectionListViewItem) {
                    Thumbnail thumbnail = (e.Item as ImageCollectionListViewItem).Thumbnail;
                    if (thumbnail != null) {
                        if (!thumbnail.CreateError)
                            thumbnailImage = thumbnail.GetThumbnailImage();
                        else {
                            thumbnailImage = Properties.Resources.BitmapNone_HighColor;
                            (thumbnailImage as Bitmap).MakeTransparent(Color.Magenta);
                        }
                    }
                }

                if (thumbnailImage == null) {
                    thumbnailImage = Properties.Resources.BitmapProgress_HighColor;
                    (thumbnailImage as Bitmap).MakeTransparent(Color.Magenta);
                }

                using (thumbnailImage) {
                    Size sizeThumb = Thumbnail.FitSize(thumbnailImage.Size, rectDraw.Size, true, true);
                    e.Graphics.DrawImage(
                        thumbnailImage,
                        rectDraw.Left + (rectDraw.Width - sizeThumb.Width) / 2,
                        rectDraw.Top + (rectDraw.Height - sizeThumb.Height) / 2,
                        sizeThumb.Width,
                        sizeThumb.Height
                        );
                }

                rectDraw.Inflate(this.ThumbnailMargin, this.ThumbnailMargin);
                using (Pen thePen = new Pen((e.State & ListViewItemStates.Selected) != 0 ?
                    SystemColors.Highlight : SystemColors.ControlLight, this.ThumbnailBorderWidth)) {
                    e.Graphics.DrawRectangle(thePen, rectDraw);
                }

                rectDraw = new Rectangle(
                    e.Bounds.Left,
                    e.Bounds.Top + this.ThumbnailSize + this.ThumbnailMargin * 2 + this.ThumbnailBorderWidth * 2,
                    e.Bounds.Width,
                    (int)e.Item.Font.GetHeight(e.Graphics)
                    );

                TextRenderer.DrawText(
                    e.Graphics,
                    e.Item.Text,
                    e.Item.Font,
                    rectDraw,
                    (e.State & ListViewItemStates.Selected) != 0 ? SystemColors.HighlightText : e.Item.ForeColor,
                    (e.State & ListViewItemStates.Selected) != 0 ? SystemColors.Highlight : e.Item.BackColor,
                    TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine
                    );
            }

            base.OnDrawItem(e);
        }

        protected void SetupControlImageList()
        {
            if (this.LargeImageList != null) {
                this.LargeImageList.Images.Clear();
                this.LargeImageList.ImageSize = new Size(
                    this.ThumbnailSize + this.ThumbnailMargin * 2,
                    this.ThumbnailSize + this.ThumbnailMargin * 2
                    );
            }
        }

        #endregion
    }
}