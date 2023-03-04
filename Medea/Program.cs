// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      Program.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
// medea
using Gyrosoft.Medea.Config;
using Gyrosoft.Medea.DataAccess;
using Gyrosoft.Medea.DataAccess.Embedded;
using Gyrosoft.Medea.Extensibility;
using Gyrosoft.Medea.Gui;
using Gyrosoft.Medea.Imaging;
using Gyrosoft.Medea.Licensing;

namespace Gyrosoft.Medea
{
    static class Program
    {
        #region Private Fields

        private static string _appTitle = null;
        private static Logger _logger = new Logger();
        private static ConfigManagerBase _configManager = new StaticConfigManager();
        private static LicenseManager _licenseManager = new LicenseManager();
        private static IStatusManager _statusManager = null;
        private static IViewManager _viewManager = null;
        //private static VideoManager _videoManager = new VideoManager();
        private static IDatabase _database = new Database();

        #endregion

        #region Public Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Gui.AppMainForm());
                Logger.Info("Normal quit.");
            }
            catch (Exception exc) {
                Logger.Fatal("Crash quit. Unhandled exception: " + exc.Message);
                ShowErrorMessage(exc.Message);
            }
        }

        /// <summary>
        /// Initialize application.
        /// </summary>
        public static bool Startup()
        {
            bool criticalError = false;

            Logger.Info("Operating system: " + Environment.OSVersion.ToString());
            Logger.Info("Culture: " + Application.CurrentCulture.ToString());

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Logger.Info("Application assembly: " + executingAssembly.GetName().ToString());
            foreach (AssemblyName referencedAssemblyName in executingAssembly.GetReferencedAssemblies()) {
                Logger.Info("Referenced assembly: " + referencedAssemblyName.ToString());
            }

            Logger.Info("Starting application...");

            // load configuration information
            /*
            Logger.Info("Loading configuration...");
            StatusManager.SetStatusText(Properties.Resources.StatusLoadingConfig);
            try {
                TaskPadConfig.Configure();
            }
            catch (Exception exc) {
                Logger.Error("Error occured while loading configuration. Exception caught: " + exc.ToString());
                ShowErrorMessage(exc.Message,MessageBoxIcon.Exclamation);
            }
             * */

            // open database
            Logger.Info("Opening database...");
            StatusManager.SetStatusText(Properties.Resources.StatusOpenDatabase);
            try {
                Database.ConnectionString = GetWorkingDirectory(Environment.SpecialFolder.ApplicationData);
                Database.Logger = Logger;
                Database.Open();
            }
            catch (Exception exc) {
                Logger.Fatal("Could not open database. Exception caught: " + exc.ToString());
                ShowErrorMessage(exc.Message);
                criticalError = true;
            }

            return (criticalError ? false : true);
        }

        public static void Shutdown()
        {
            Program.StatusManager.SetStatusText(Properties.Resources.StatusShutdown);

            // close database
            Logger.Info("Closing database...");
            try {
                Program.Database.Close();
            }
            catch (Exception exc) {
                Logger.Error("Could not close database. Exception caught: " + exc.ToString());
                ShowErrorMessage(exc.Message);
            }

            // save configuration
            Logger.Info("Saving configuration...");
            try {
                Properties.Settings.Default.Save();
            }
            catch (Exception exc) {
                Logger.Error("Could not save configuration. Exception caught: " + exc.ToString());
                ShowErrorMessage(exc.Message);
            }

            /*
            Logger.Info("Saving configuration...");
            try {
                Configuration.Db4oSettingsSerializer ss = new Configuration.Db4oSettingsSerializer();
                SettingsManager.SaveAll(ss);
            }
            catch (Exception exc) {
                Logger.Error("Could not save configuration. Exception caught: " + exc.ToString());
                ShowErrorMessage(exc.Message);
            }
             * */
        }

        public static string GetWorkingDirectory(Environment.SpecialFolder specialFolder)
        {
            if (specialFolder != Environment.SpecialFolder.ApplicationData &&
                specialFolder != Environment.SpecialFolder.CommonApplicationData) {
                throw new InvalidOperationException();
            }

            return GetWorkingDirectory(Environment.GetFolderPath(specialFolder));
        }

        public static string GetWorkingDirectory(string basePath)
        {
            string path = basePath;

            // add directory separator to end of given path
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()) &&
                !path.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                path += Path.DirectorySeparatorChar;

            object[] customAttributes;

            // obtain company name from assembly manifest, add to path
            customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (customAttributes.Length > 0) {
                path += ((AssemblyCompanyAttribute)customAttributes[0]).Company;
                path += Path.DirectorySeparatorChar;
            }

            // attempt to create company directory
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            // obtain product name from assembly manifest, add to path
            customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (customAttributes.Length > 0) {
                path += ((AssemblyProductAttribute)customAttributes[0]).Product;
                path += Path.DirectorySeparatorChar;
            }

            // attempt to create product directory
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public static void ShowErrorMessage(string message)
        {
            ShowErrorMessage(message, MessageBoxIcon.Stop);
        }

        public static void ShowErrorMessage(string message, MessageBoxIcon icon)
        {
            MessageBox.Show(message, null, MessageBoxButtons.OK, icon);
        }

        #endregion

        #region Public Properties

        public static string Title
        {
            get
            {
                if (Object.ReferenceEquals(_appTitle, null)) {
                    try {
                        _appTitle = Properties.Resources.ApplicationTitle;
                    }
                    catch (Exception) {
                        try {
                            _appTitle = typeof(Program).Assembly.GetName().Name;
                        }
                        catch (Exception) {
                            _appTitle = String.Empty;
                        }
                    }
                }
                return _appTitle;
            }
        }

        public static Logger Logger
        {
            get
            {
                return _logger;
            }
        }

        public static ConfigManagerBase ConfigManager
        {
            get
            {
                return _configManager;
            }
        }

        /// <summary>
        /// Gets application instance of license manager.
        /// </summary>
        public static LicenseManager LicenseManager
        {
            get
            {
                return _licenseManager;
            }
        }

        /// <summary>
        /// Gets or sets application status window.
        /// </summary>
        public static IStatusManager StatusManager
        {
            get
            {
                return _statusManager;
            }

            set
            {
                _statusManager = value;
            }
        }

        /// <summary>
        /// Gets or sets application view manager.
        /// </summary>
        public static IViewManager ViewManager
        {
            get
            {
                return _viewManager;
            }

            set
            {
                _viewManager = value;
            }
        }

        public static IDatabase Database
        {
            get
            {
                return _database;
            }
        }

        #endregion
    }
}