// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      OptionDialog.cs
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
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Gyrosoft.Medea.Gui
{
    public partial class OptionDialog : Form
    {
        #region Private Types

        /// <summary>
        /// Provides all necessary information to display option pages.
        /// </summary>
        private class PageInfo
        {
            /// <summary>
            /// Option page type.
            /// </summary>
            private Type _pageType;

            /// <summary>
            /// Name of page title string resource.
            /// </summary>
            private string _titleResourceName;

            /// <summary>
            /// Constructs new PageInfo object.
            /// </summary>
            /// <param name="pageType">Option page type.</param>
            /// <param name="titleResourceName">Page title string resource name.</param>
            /// 
            public PageInfo(Type pageType, string titleResourceName)
            {
                // type is required
                if (pageType == null) {
                    throw new ArgumentNullException("pageType");
                }

                // option page class must derive from OptionDialogPage
                if (!pageType.IsSubclassOf(typeof(OptionDialogPage))) {
                    throw new Exception(String.Format("Invalid property page type '{0}'.", pageType.ToString()));
                }

                _pageType = pageType;
                _titleResourceName = titleResourceName;
            }

            /// <summary>
            /// Returns a string representation of the object.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                string ret = Properties.Resources.ResourceManager.GetString(_titleResourceName);
                if (ret == null) {
                    ret = _pageType.ToString();
                }
                return ret;
            }

            /// <summary>
            /// Creates an option page.
            /// </summary>
            /// <returns>New option page object.</returns>
            public OptionDialogPage CreatePage()
            {
                return null;
                /*
                ConstructorInfo ci = _pageType.GetConstructor(
                    new Type[] { typeof(Configuration.SettingsManager) });

                if (ci == null)
                    throw new Exception(
                        String.Format("Option page class '{0}' does not have appropriate constructor.", 
                        _pageType.ToString()));
                else {
                    OptionDialogPage page = (OptionDialogPage)ci.Invoke(new object[] { settings });
                    return page;
                }
                */
            }

            /// <summary>
            /// Gets option page type.
            /// </summary>
            public Type Type
            {
                get
                {
                    return _pageType;
                }
            }

            /// <summary>
            /// Gets option page title.
            /// </summary>
            public string Title
            {
                get
                {
                    return this.ToString();
                }
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Active property page.
        /// </summary>
        private OptionDialogPage _activePage = null;

        /// <summary>
        /// Active page index.
        /// </summary>
        private int _activePageIndex = -1;

        #endregion

        #region Construction

        /// <summary>
        /// Constructs new OptionsDialog object.
        /// </summary>
        public OptionDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Properties

        #endregion

        #region Private Methods

        private void Apply()
        {
            /*
            if (this.InternalSettings.SettingsChanged) {
                Program.SettingsManager.CopyFrom(this.InternalSettings);
                this.InternalSettings.SaveAll(null);

                if (_activePage != null) {
                    _activePage.Focus();
                }
                applyButton.Enabled = false;
            }
             * */
        }

        #endregion

        #region OptionDialog Event Handlers (Form)

        private void Form_Load(object sender, EventArgs e)
        {
            pageList.Items.Clear();
            pageList.Items.Add(new PageInfo(typeof(OptionPages.GeneralOptionPage), "OptionPageGeneral"));
            pageList.Items.Add(new PageInfo(typeof(OptionPages.VideoOptionPage), "OptionPageVideo"));
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (pageList.Items.Count > 0) {
                pageList.SelectedIndex = 0;
                if (_activePage != null) {
                    _activePage.Focus();
                }
            }
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            // destroy active page
            pageList.SelectedIndex = -1;
        }

        private void pageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pageList.SelectedIndex != _activePageIndex) {
                try {
                    //MessageBox.Show("About to display: " + (pageList.SelectedItem as PageInfo).Type.ToString());

                    if (_activePage != null) {
                        _activePage.Dispose();
                        _activePage = null;
                    }

                    PageInfo pageInfo;

                    if ((pageInfo = (PageInfo)pageList.SelectedItem) != null) {
                        _activePage = pageInfo.CreatePage();
                        _activePage.Parent = pageHostPanel;
                        _activePage.Dock = DockStyle.Fill;
                        _activePage.Visible = true;
                    }

                    _activePageIndex = pageList.SelectedIndex;
                }
                catch (Exception exception) {
                    string errorMsg = String.Format(
                        "{0}\n{1}",
                        Properties.Resources.ErrorShowOptionPage,
                        exception.Message
                        );

                    MessageBox.Show(
                        errorMsg,
                        null,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop
                        );

                    pageList.SelectedIndex = _activePageIndex;
                }
            }
        }

        private void pageList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

            if ((e.State & DrawItemState.Selected) != 0) {
                using (Bitmap bitmapCurrentItem = Properties.Resources.BitmapCurrentItem_HighColor) {
                    bitmapCurrentItem.MakeTransparent(Color.Magenta);
                    e.Graphics.DrawImage(
                        bitmapCurrentItem,
                        e.Bounds.Left + 3,
                        e.Bounds.Top + (pageList.ItemHeight - 16) / 2
                        );
                }

                if (pageList.Focused) {
                    using (Pen dottedPen = new Pen(SystemColors.Highlight)) {
                        dottedPen.DashStyle = DashStyle.Dot;
                        e.Graphics.DrawRectangle(
                            dottedPen, 
                            e.Bounds.Left, 
                            e.Bounds.Top, 
                            e.Bounds.Width - 1, 
                            e.Bounds.Height - 1
                            );
                    }
                }
            }

            Rectangle rectText = e.Bounds;
            rectText.X += 22;

            TextRenderer.DrawText(
                e.Graphics,
                pageList.Items[e.Index].ToString(),
                e.Font,
                rectText,
                SystemColors.WindowText,
                SystemColors.Window,
                TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter
            );
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Apply();
            this.DialogResult = DialogResult.OK;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.Apply();
        }

        #endregion

        #region OptionDialog Event Handlers (Settings)

        void OnInternalSettingsChanged()
        {
            applyButton.Enabled = true;
        }

        #endregion
    }
}