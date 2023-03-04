// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IViewManager.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gyrosoft.Medea.Extensibility
{
    public interface IViewManager
    {
        /// <summary>
        /// Gets an active view.
        /// </summary>
        IViewHost ActiveView 
            { get; }

        /// <summary>
        /// Return a view control childControl belongs to.
        /// </summary>
        /// <param name="childControl">Any child control contained in the view.</param>
        /// <returns></returns>
        IViewHost GetCurrentView(Control childControl);

        /// <summary>
        /// Returns an opposite view for view childControl belogns to.
        /// </summary>
        /// <param name="childControl">Any child control contained in the view.</param>
        /// <returns></returns>
        IViewHost GetOppositeView(Control childControl);
    }
}
