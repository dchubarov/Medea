// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IViewHost.cs
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
    /// <summary>
    /// Represents a container for a view.
    /// </summary>
    public interface IViewHost
    {
        /// <summary>
        /// Gets an IViewManager object the current view belongs to.
        /// </summary>
        IViewManager ViewManager 
            { get; }

        /// <summary>
        /// Creates a view.
        /// </summary>
        /// <param name="factory">IViewFactory object that creates a view.</param>
        /// <returns>Returns true if view hosted successfully, or false if operation cancelled by user.</returns>
        bool Host(IViewFactory factory);

        /// <summary>
        /// Creates a view.
        /// </summary>
        /// <param name="factory">IViewFactory object that creates a view.</param>
        /// <param name="reason">Reason to close current view.</param>
        /// <returns>Returns true if view hosted successfully, or false if operation cancelled by user.</returns>
        bool Host(IViewFactory factory, CloseReason reason);

        /// <summary>
        /// Updates view caption area.
        /// </summary>
        void UpdateCaption();

        bool HandleKeyPress(KeyPressEventArgs e);
    }
}
