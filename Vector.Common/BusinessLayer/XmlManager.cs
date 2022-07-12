
namespace Vector.Common.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using System.Text;
    using System.Xml;
    using System.Security;
    using System.Web;
    using System.Xml.Serialization;

    public static class XmlManager
    {
        public const string XPathForSMPT = "//Smtp/Settings[@Page='";
        public const string XmlFilePathSMTPConfiguration = "~/XML/SMTPEmailConfiguration.xml";

        #region Enum
        public enum XmlNode
        {
            xmlns, ////Don't change case upper to lower
            ESSiteId, ////Don't change case upper to lower
            ESSiteDetailId, ////Don't change case upper to lower
            VirtualMeterNumber, ////Don't change case upper to lower
            Meter_Details, ////Don't change case upper to lower
            Meter_Header_Details, ////Don't change case upper to lower
            FundingStmt_Header_Details, ////Don't change case upper to lower
            FundingStmt_Details, ////Don't change case upper to lower
            FsId, ////Don't change case upper to lower
            Status ////Don't change case upper to lower
        }


        #endregion

        //Loading a document 
        #region XML

        #region Linq to XML

        //Loading a document 

        public static XElement LoadXmlDocumentAsXElement(string xmlPath)
        {
            XElement xData = XElement.Load(xmlPath);
            return xData;
        }


        //Loading a document 
        public static XDocument LoadXmlDocument(string xmlPath)
        {
            XDocument xData = XDocument.Load(xmlPath);
            return xData;
        }


        // GetXMLValueByElement 
        public static string GetXmlValueByElement(string xmlFileName,
                                                  string element1 = null, string element2 = null,
                                                  string element1Value = null)
        {
            XElement xData = XElement.Load(xmlFileName);
            var elementDataFromXml = (from c in xData.Elements(element1).AsEnumerable()
                                      where c.Attribute(element1).ToString() == element1Value
                                      select c.Attribute(element2).ToString()).FirstOrDefault();

            return elementDataFromXml;
        }


        // GetXMLValueByElement 
        public static string GetXmlValueByElement(XContainer xData,
                                                  string element1 = null, string element2 = null,
                                                  string element1Value = null)
        {
            var elementDataFromXml = (from c in xData.Elements(element1).AsEnumerable()
                                      where c.Attribute(element1).ToString() == element1Value
                                      select c.Attribute(element2).ToString()).FirstOrDefault();

            return elementDataFromXml;
        }

        /// <summary>
        /// Get A value for Particular node by comparing another node value
        /// </summary>
        /// <param name="xData"></param>
        /// <param name="rootNode"></param>
        /// <param name="parentNode"></param>
        /// <param name="comparingNode"></param>
        /// <param name="valueNode"></param>
        /// <param name="comparenodeValue"></param>
        /// <returns></returns>
        public static string GetXmlValueByElementWithParent(XContainer xData,
                                                                           string rootNode,
                                                                           string parentNode,
                                                                           string comparingNode,
                                                                           string valueNode,
                                                                           string comparenodeValue)
        {

            //get a particular node value by comparing another node value.
            //Will return the first match found.
            var result = xData.Descendants(rootNode)
                      .Elements(parentNode)
                      .Where(node => node.Element(comparingNode).Value == comparenodeValue)
                      .Elements(valueNode)
                      .SingleOrDefault().Value;

            return result;
        }

        public static IEnumerable<XNode> GetXmlAllDecendentsElement(XContainer xData, string element)
        {
            return xData.Descendants(element);
        }

        public static DataTable XElementToDataTable(XContainer node)
        {
            DataTable dtTable = new DataTable();
            dtTable.Locale = CultureInfo.InvariantCulture;

            XElement setup = (from p in node.Descendants() select p).First();
            foreach (XElement xe in setup.Descendants())                                        // build your DataTable
                dtTable.Columns.Add(new DataColumn(xe.Name.ToString(), typeof(string)));             // add columns to your dt

            var all = from p in node.Descendants(setup.Name.ToString()) select p;
            foreach (XElement xe in all)
            {
                DataRow dr = dtTable.NewRow();
                foreach (XElement xe2 in xe.Descendants())
                    dr[xe2.Name.ToString()] = xe2.Value;                                         //add in the values
                dtTable.Rows.Add(dr);
            }
            return dtTable;
        }

        public static XElement GetXmlElementByValue(XContainer xData,
                                                                           string rootNode,
                                                                           string parentNode,
                                                                           string comparingNode,
                                                                           string valueNode,
                                                                           string comparenodeValue)
        {
            //get a particular node value by comparing another node value.
            //Will return the first match found.
            var result = xData.Descendants(rootNode)
                       .Elements(parentNode)
                       .Where(node => node.Element(comparingNode).Value == comparenodeValue)
                       .Elements(valueNode)
                       .SingleOrDefault();

            //  foreach(var item in result.

            return result;

        }

        public static string GetXmlvalueByConcatinationOfNodeValues(XContainer xData,
                                                                           string rootNode,
                                                                           string parentNode,
                                                                           string comparingNode,
                                                                           string valueNode,
                                                                           string comparenodeValue)
        {
            XElement element = GetXmlElementByValue(xData, rootNode, parentNode, comparingNode, valueNode, comparenodeValue);

            return element.Value;
        }

        public static IEnumerable<XElement> GetXmlElementByXPath(XNode xData, string xPath)
        {
            // XPath expression

            IEnumerable<XElement> list2 = xData.XPathSelectElements(xPath);
            return list2;
        }

        public static string GetXmlElementByValue(XNode xData, string xPath)
        {
            //Validate node 
            IEnumerable<XElement> IsNodeExists = GetXmlElementByXPath(xData, xPath);

            if (IsNodeExists.Count() > VectorConstants.Zero)
                return xData.XPathSelectElements(xPath).First().Value;

            return string.Empty;
        }

        public static string GetXmlElementAttributeValue(XNode xData, string xPath, string attribute)
        {
            //Validate node 
            IEnumerable<XElement> IsNodeExists = GetXmlElementByXPath(xData, xPath);

            if (IsNodeExists.Count() > VectorConstants.Zero && IsNodeExists.First().Attributes(attribute).Count() != VectorConstants.Zero)
                return xData.XPathSelectElements(xPath).First().Attributes(attribute).First().Value;

            return string.Empty;
        }

        public static string GetNodeValue(XDocument xData, string rootNode, string element)
        {
            var result = (from item in xData.Root.Elements(rootNode)
                          select (string)item.Element(element)).FirstOrDefault();

            return result;
        }

        public static string GetNodeValue(XDocument xData, string rootNode)
        {
            var result = (from item in xData.Root.Elements(rootNode)
                          select item.Value).FirstOrDefault();

            return result;
        }

        public static string GetNodeValue(XContainer xData, string subElement)
        {
            var result = (from item in xData.Elements(subElement)
                          select item.Value).FirstOrDefault();

            return result;
        }

        public static void UpdateNodeValue(XmlElement xElement, string nodeName, string value)
        {
            xElement.SelectSingleNode(nodeName).InnerText = value;
        }

        /// <summary>
        /// Update the XML node usign the XPath
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="xPath"></param>
        /// <param name="value"></param>
        /// <param name="saveXmlPath"></param>
        public static void UpdateNodeValue(string xmlPath, string xPath, string value, string saveXmlPath)
        {
            //Load XMl document
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlPath);

            //create navigator for the XMlDocument
            XPathNavigator navigator = xmlDocument.CreateNavigator();

            //Create Xml NameSapece Manager
            XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);

            //Update the XMl Document value for the selected Client
            foreach (XPathNavigator nav in navigator.Select(xPath, manager))
            {
                //update the node value
                nav.SetValue(value);
            }

            //Save Document at the same place
            xmlDocument.Save(saveXmlPath);
        }

        #endregion

        #region Create Xml


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootElemnt"> application </param>
        /// <param name="rootElemntValue"> </param>
        /// <param name="attributes"> name=versia </param>
        /// <returns></returns>
        public static XDocument CreateDocument(string rootElemnt, string rootElemntValue = "",
                                                NameValueCollection attributes = null,
                                                bool isXsiNeeded = false, XNamespace xsi = null,
                                                bool isSchemaLocationNeeded = false, XNamespace schemaLocation = null)
        {

            XDocument document = null;

            if (attributes != null)
            {
                var attributeValues = attributes.AllKeys.SelectMany(attributes.GetValues, (k, v) => new { key = k, value = v });

                var nameSpaceValue = (from c in attributeValues
                                      where StringManager.IsEqual(c.key, XmlNode.xmlns.ToString())
                                      select c.value).FirstOrDefault();


                //// Create element with/without name space attribute 
                XElement element;
                if (!string.IsNullOrEmpty(nameSpaceValue))
                {
                    XNamespace nameSpace = @nameSpaceValue;
                    element = new XElement(nameSpace + rootElemnt, rootElemntValue);
                }
                else
                {
                    element = new XElement(rootElemnt, rootElemntValue);
                }


                //// Add all attributes to elements except name space [xmlns]
                foreach (var attribute in attributeValues)
                {

                    if (StringManager.IsNotEqual(attribute.key, XmlNode.xmlns.ToString()))
                    {
                        XAttribute xAttribute = new XAttribute(attribute.key, attribute.value);
                        element.Add(xAttribute);
                    }


                }

                if (isXsiNeeded)
                {
                    ///Adding XSI element to the Root Attribute

                    XAttribute xAttribute = new XAttribute(XNamespace.Xmlns + "xsi", xsi);
                    element.Add(xAttribute);


                }

                if (isSchemaLocationNeeded)
                {
                    ///Adding Schemalocation 
                    XAttribute xAttribute = new XAttribute(xsi + "schemaLocation", schemaLocation);
                    element.Add(xAttribute);
                }


                document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), element);
            }


            return document;
        }

        public static XElement CreateAttribute(XElement element, string attributeName, string attributeValue)
        {

            XAttribute xAttribute = new XAttribute(attributeName, attributeValue);
            element.Add(xAttribute);

            return element;

        }

        public static XElement CreateElement(string elementName, string elementValue, NameValueCollection attributes = null, XNamespace xmlsNameSpace = null)
        {
            XElement element;
            if (xmlsNameSpace != null)
                element = new XElement(xmlsNameSpace == null ? string.Empty : xmlsNameSpace + elementName, elementValue);
            else
                element = new XElement(elementName, elementValue);

            if (attributes != null)
            {
                var attrivuteValues = attributes.AllKeys.SelectMany(attributes.GetValues, (k, v) => new { key = k, value = v });
                foreach (var attribute in attrivuteValues)
                {
                    XAttribute xAttribute = new XAttribute(attribute.key, attribute.value);
                    element.Add(xAttribute);
                }
            }

            return element;
        }

        public static XElement CreateNestedElement(XElement parentElement, string elementName, string elementValue, NameValueCollection attributes = null, XNamespace xmlsNameSpace = null)
        {

            XElement element;
            if (xmlsNameSpace != null)
                element = new XElement(xmlsNameSpace == null ? string.Empty : xmlsNameSpace + elementName, elementValue);
            else
                element = new XElement(elementName, elementValue);

            if (attributes != null)
            {
                var attrivuteValues = attributes.AllKeys.SelectMany(attributes.GetValues, (k, v) => new { key = k, value = v });
                foreach (var attribute in attrivuteValues)
                {
                    XAttribute xAttribute = new XAttribute(attribute.key, attribute.value);
                    element.Add(xAttribute);
                }
            }


            parentElement.Add(element);
            return parentElement;
        }






        public static XElement CreateElement(string elementName, string elementValue, string subElementName, DataTable dtData,
                                       string atttributeName1, string columnName1,
                                        string atttributeName2, string columnName2,
                                        string atttributeName3, string columnName3,
                                        string atttributeName4, string columnName4)
        {
            XElement element = new XElement(elementName, elementValue);


            dtData.AsEnumerable().ToList().ForEach(order => element.Add(new XElement(subElementName,
                                               new XAttribute(atttributeName1, dtData.Columns.Contains(columnName1) == true ? order.Field<string>(columnName1) : string.Empty),
                                               new XAttribute(atttributeName2, dtData.Columns.Contains(columnName2) == true ? order.Field<string>(columnName2) : string.Empty),
                                               new XAttribute(atttributeName3, dtData.Columns.Contains(columnName3) == true ? order.Field<string>(columnName3) : string.Empty),
                                               new XAttribute(atttributeName4, dtData.Columns.Contains(columnName4) == true ? order.Field<string>(columnName4) : string.Empty))));

            return element;
        }



        public static XElement CreateNestedElement(
                        XElement parentElement,
                        string elementName,
                        string elementValue,
                        string subElementName,
                        DataTable dtData,
                        string atttributeName1,
                        string columnName1,
                        string atttributeName2,
                        string columnName2,
                        string atttributeName3,
                        string columnName3,
                        string atttributeName4,
                        string columnName4,
                        XNamespace xmlsNameSpace = null)
        {

            XElement element;
            if (xmlsNameSpace != null)
                element = new XElement(xmlsNameSpace == null ? string.Empty : xmlsNameSpace + elementName, elementValue);
            else
                element = new XElement(elementName, elementValue);

            if (!string.IsNullOrEmpty(columnName1) && !string.IsNullOrEmpty(columnName2) && !string.IsNullOrEmpty(columnName3) &&
                string.IsNullOrEmpty(columnName4))
            {
                dtData.AsEnumerable().ToList().ForEach(order => element.Add(new XElement(xmlsNameSpace == null ? subElementName : xmlsNameSpace + subElementName,
                                                   new XAttribute(atttributeName1, dtData.Columns.Contains(columnName1) == true ? order.Field<string>(columnName1) : string.Empty),
                                                   new XAttribute(atttributeName2, dtData.Columns.Contains(columnName2) == true ? order.Field<string>(columnName2) : string.Empty),
                                                   new XAttribute(atttributeName3, dtData.Columns.Contains(columnName3) == true ? Convert.ToString(order.Field<decimal>(columnName3), CultureInfo.CurrentCulture) : string.Empty),
                                                   new XAttribute(atttributeName4, dtData.Columns.Contains(columnName4) == true ? order.Field<string>(columnName4) : string.Empty))));
            }
            else if (!string.IsNullOrEmpty(columnName1) && !string.IsNullOrEmpty(columnName2) && !string.IsNullOrEmpty(columnName3))
            {
                dtData.AsEnumerable().ToList().ForEach(order => element.Add(new XElement(xmlsNameSpace == null ? subElementName : xmlsNameSpace + subElementName,
                                                   new XAttribute(atttributeName1, dtData.Columns.Contains(columnName1) == true ? order.Field<string>(columnName1) : string.Empty),
                                                   new XAttribute(atttributeName2, dtData.Columns.Contains(columnName2) == true ? order.Field<string>(columnName2) : string.Empty),
                                                   new XAttribute(atttributeName3, dtData.Columns.Contains(columnName3) == true ? Convert.ToString(order.Field<decimal>(columnName3),
                                                       CultureInfo.CurrentCulture) : string.Empty))));

            }
            else if (!string.IsNullOrEmpty(columnName1) && !string.IsNullOrEmpty(columnName2))
            {
                dtData.AsEnumerable().ToList().ForEach(order => element.Add(new XElement(xmlsNameSpace == null ? subElementName : xmlsNameSpace + subElementName,
                                                   new XAttribute(atttributeName1, dtData.Columns.Contains(columnName1) == true ? order.Field<string>(columnName1) : string.Empty),
                                                   new XAttribute(atttributeName2, dtData.Columns.Contains(columnName2) == true ? order.Field<string>(columnName2) : string.Empty))));

            }
            else if (!string.IsNullOrEmpty(columnName1))
            {
                dtData.AsEnumerable().ToList().ForEach(order => element.Add(new XElement(xmlsNameSpace == null ? subElementName : xmlsNameSpace + subElementName,
                                                   new XAttribute(atttributeName1, dtData.Columns.Contains(columnName1) == true ? order.Field<string>(columnName1) : string.Empty))));

            }
            else
            {
                dtData.AsEnumerable().ToList().ForEach(order => element.Add(new XElement(xmlsNameSpace == null ? subElementName : xmlsNameSpace + subElementName)));

            }

            parentElement.Add(element);
            return parentElement;
        }

        #endregion
        /// <summary>
        /// Returns the Rootnode attribute name
        /// </summary>
        /// <param name="serializedObjectType"></param>
        /// <returns></returns>
        public static string GetRootNodeElementNameForType(Type serializedObjectType)
        {
            // Determine if the Type contains an XmlRoot Attribute.  If so, the XmlRoot attribute should contain
            // the name of the element-name for this type.
            // Otherwise, the name of the type should 've been used for serializing objects of this type.
            XmlRootAttribute theAttrib = Attribute.GetCustomAttribute(serializedObjectType, typeof(XmlRootAttribute)) as XmlRootAttribute;

            if (theAttrib != null)
            {
                if (String.IsNullOrEmpty(theAttrib.ElementName) == false)
                {
                    return theAttrib.ElementName;
                }
                else
                {
                    return serializedObjectType.Name;
                }
            }
            else
            {
                return serializedObjectType.Name;
            }
        }
        #endregion

        #region SMPT Settings

        public static string GetNetworkHost(string pageName)
        {
            var result = XmlManager.GetXmlElementByValue(XmlManager.LoadXmlDocument(GetXmlPathForSMTPConfiguration()),
                        XPathForSMPT + pageName + "']/Host");

            //if (string.IsNullOrEmpty(result))
            //    result = "smtp.gmail.com"; //Default Value

            return result;
        }

        public static int GetNetworkPort(string pageName)
        {
            var result = XmlManager.GetXmlElementByValue(XmlManager.LoadXmlDocument(GetXmlPathForSMTPConfiguration()),
                        XPathForSMPT + pageName + "']/Port");

            if (string.IsNullOrEmpty(result))
                result = 0.ToString(); //Default Value

            return Convert.ToInt32(result);
        }

        public static string GetNetworkUserName(string pageName)
        {
            var result = XmlManager.GetXmlElementByValue(XmlManager.LoadXmlDocument(GetXmlPathForSMTPConfiguration()),
                        XPathForSMPT + pageName + "']/UserName");

            //if (string.IsNullOrEmpty(result))
            //    result = "refusespecialists@gmail.com"; //Default Value

            return result;
        }

        public static string GetNetworkPassword(string pageName)
        {
            var result = XmlManager.GetXmlElementByValue(XmlManager.LoadXmlDocument(GetXmlPathForSMTPConfiguration()),
            XPathForSMPT + pageName + "']/Password");

            //if (string.IsNullOrEmpty(result))
            //    result = "prokarma"; //Default Value

            return result.ToString();
        }

        private static string GetXmlPathForSMTPConfiguration()
        {
            return System.Web.HttpContext.Current.Server.MapPath(XmlFilePathSMTPConfiguration);
        }

        #endregion
    }
}

