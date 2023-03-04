// ==========================================================================
//  Project MEDEA
//  Medical Examination Database & Endoscopy Application
//  Copyright (c) 2006 Gyro Software. All rights reserved.
//
//  Module: 
//
//      XmlConfigHelper.cs
//
//  Purpose:
//
//
// ==========================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Gyrosoft.Medea.Config
{
    public static class XmlConfigHelper
    {
        #region Private Constants

        private const string NameAttributeName = "name";
        private const string ValueAttributeName = "value";

        #endregion

        #region Public Methods

        public static string GetElementName(XmlElement element)
        {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            XmlAttribute nameAttribute = element.Attributes[NameAttributeName];
            if (nameAttribute != null) {
                string elementName = nameAttribute.Value.Trim();
                if (!String.IsNullOrEmpty(elementName)) {
                    return elementName;
                }
            }

            return null;
        }

        public static string GetPropertyValue(XmlElement element, string propertyName)
        {
            return GetPropertyValue(element, propertyName, ValueAttributeName);
        }

        public static string GetPropertyValue(XmlElement element, string propertyName, string valueAttributeName)
        {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            XmlNode propertyElement = element.SelectSingleNode(propertyName);
            if (propertyElement != null && propertyElement is XmlElement) {
                XmlAttribute valueAttribute = (propertyElement as XmlElement).Attributes[valueAttributeName];
                if (valueAttribute != null) {
                    return valueAttribute.Value;
                }
            }

            return null;
        }

        #endregion
    }
}
