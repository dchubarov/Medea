// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      IViewFactory.cs
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
    public interface IViewFactory
    {
        /// <summary>
        /// Creates a control.
        /// </summary>
        void CreateControl();

        /// <summary>
        /// Destroys a control.
        /// </summary>
        void DestroyControl();

        /// <summary>
        /// Activates a control.
        /// </summary>
        void ActivateControl();

        /// <summary>
        /// Returns a value indicating that control may be closed.
        /// </summary>
        /// <param name="reason">Close reason.</param>
        /// <returns>Returns true if hosted control can be closed, otherwise false.</returns>
        bool CanClose(CloseReason reason);

        /// <summary>
        /// Gets a control.
        /// </summary>
        Control Control
            { get; }

        /// <summary>
        /// Gets a caption text.
        /// </summary>
        string Caption
            { get; }

        /// <summary>
        /// Gets a detailed text.
        /// </summary>
        string Detail
            { get; }

        /// <summary>
        /// Gets a tool strip contains view specific commands.
        /// </summary>
        ToolStrip ToolStrip
            { get; }

        /// <summary>
        /// Gets a menu contains view specific commands.
        /// </summary>
        MenuStrip MenuStrip
            { get; }

        bool HandleKeyPress(KeyPressEventArgs e);
    }
}
