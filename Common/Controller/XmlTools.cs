// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    ///     xml辅助类
    /// </summary>
    public static class XmlTools {
        /// <summary>
        ///     读取并返回一个文档对象
        /// </summary>
        /// <returns></returns>
        public static XmlDocument LoadXml(string xmlPath) {
            XmlDocument xmlDocument;
            try {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlPath);
            }
            catch (Exception exception) {
                LogTools.LogError($"LoadXml Error! Detail{exception.Message}");
                return null;
            }
            return xmlDocument;
        }

        /// <summary>
        ///     获取xml结点，这里依据文档根目录处理命名空间的问题
        /// </summary>
        /// <returns></returns>
        public static XmlNode GetXmlNodeByXpath(XmlDocument xmlDoc, string xpath) {
            XmlNode selectedNode = null;
            try {
                //处理命名空间

                if (!xmlDoc.HasChildNodes) return null;
                var namespaceUri = xmlDoc.ChildNodes[1].NamespaceURI;
                if (!namespaceUri.Equals(string.Empty)) {
                    var mnamespce = new XmlNamespaceManager(xmlDoc.NameTable);
                    mnamespce.AddNamespace("nhb", namespaceUri);
                    xpath = $@"//nhb:{xpath}";
                    selectedNode = xmlDoc.SelectSingleNode(xpath, mnamespce);
                }
                else {
                    selectedNode = xmlDoc.SelectSingleNode(xpath);
                }
            }
            catch (Exception ex) {
                LogTools.LogError($"GetXmlNode Error! Detail {ex.Message}");
            }

            return selectedNode;
        }

        /// <summary>
        ///     获取xml结点，这里依据文档根目录处理命名空间的问题
        /// </summary>
        /// <returns></returns>
        public static XmlNodeList GetXmlNodeListByXpath(XmlDocument xmlDoc, string xpath) {
            XmlNodeList selectedNode = null;
            try {
                //处理命名空间

                if (!xmlDoc.HasChildNodes) return null;
                var namespaceUri = xmlDoc.ChildNodes[1].NamespaceURI;
                if (!namespaceUri.Equals(string.Empty)) {
                    var mnamespce = new XmlNamespaceManager(xmlDoc.NameTable);
                    mnamespce.AddNamespace("nhb", namespaceUri);
                    xpath = $@"//nhb:{xpath}";
                    selectedNode = xmlDoc.SelectNodes(xpath, mnamespce);
                }
                else {
                    selectedNode = xmlDoc.SelectNodes(xpath);
                }
            }
            catch (Exception ex) {
                LogTools.LogError($"GetXmlNodeList Error! Detail {ex.Message}");
            }

            return selectedNode;
        }

        /// <summary>
        ///     根据xpath，修改xml某项的值
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="xpath"></param>
        /// <param name="value"></param>
        public static void ModiXmlByXpath(string xmlPath, string xpath, string value) {
            var xmlDoc = LoadXml(xmlPath);
            //$@"//BuildeItem[Id='{id}']/Url"
            if (xmlDoc == null) return;
            var selectedNode = GetXmlNodeByXpath(xmlDoc, xpath);
            if (selectedNode == null) return;
            selectedNode.InnerText = value;
            xmlDoc.Save(xmlPath);
        }

        #region  修改解决方案的csproj文件 添加类文件

        /// <summary>
        ///     根据xmlNode删除Node结点
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="xpath"></param>
        public static void DeleteXmlNodeByXPath(string xmlFileName, string xpath) {
            var xmlDoc = LoadXml(xmlFileName);
            if (xmlDoc == null) return;
            if (!xmlDoc.HasChildNodes)
                return;
            var xNodes = GetXmlNodeListByXpath(xmlDoc, xpath);
            if (xNodes == null)
                return;
            for (var i = xNodes.Count - 1; i >= 0; i--) {
                var xmlAttributeCollection = xNodes[i].Attributes;
                if (xmlAttributeCollection?["Include"] == null)
                    continue;
                var parentNode = xNodes[i].ParentNode;
                parentNode?.RemoveChild(xNodes[i]);
            }
            xmlDoc.Save(xmlFileName);
        }
    
        /// <summary>
        ///     获取模板
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetPathByXpath(string xmlFileName, string xpath) {
            var pathList = new List<string>();
            var xmlDoc = LoadXml(xmlFileName);
            if (!xmlDoc.HasChildNodes) return pathList;
            var xNodes = GetXmlNodeListByXpath(xmlDoc, xpath);
            if (xNodes != null) pathList.AddRange(from XmlNode node in xNodes select node.InnerText);
            return pathList;
        }

        /// <summary>
        ///  添加解决方案
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="csName"></param>
        public static void AddCsproj(string xmlPath, string csName) {
            var xmlDoc = LoadXml(xmlPath);
            if (csName.ToLower().Contains(".designer.cs")) {
                if (xmlDoc.DocumentElement != null) {
                    var root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                    var xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                    xe1.SetAttribute("Include", csName); //设置该节点genre属性 
                    if (xmlDoc.DocumentElement != null) {
                        var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                        var msname = csName.Substring(csName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                        msname = msname.Substring(0, msname.IndexOf(".", StringComparison.Ordinal));
                        msname = msname + ".cs";
                        loc.InnerText = msname;
                        xe1.AppendChild(loc);
                    }
                    var notExisted = CheckXmlNode(root, csName);
                    if (notExisted)
                        root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }
            else if (csName.ToLower().Contains(".resx")) {
                if (xmlDoc.DocumentElement != null) {
                    var root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                    var xe1 = xmlDoc.CreateElement("EmbeddedResource", xmlDoc.DocumentElement.NamespaceURI);
                    //创建一个<book>节点
                    xe1.SetAttribute("Include", csName); //设置该节点genre属性 
                    if (xmlDoc.DocumentElement != null) {
                        var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                        var msname = csName.Substring(csName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                        msname = msname.Substring(0, msname.IndexOf(".", StringComparison.Ordinal));
                        msname = msname + ".cs";
                        loc.InnerText = msname;
                        xe1.AppendChild(loc);
                    }
                    var notExisted = CheckXmlNode(root, csName);
                    if (notExisted)
                        root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }

            else {
                if (xmlDoc.DocumentElement != null) {
                    var root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                    var xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                    xe1.SetAttribute("Include", csName); //设置该节点genre属性    
                    var notExisted = CheckXmlNode(root, csName);
                    if (notExisted)
                        root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }
        }

        /// <summary>
        ///     判断节点是否存在
        /// </summary>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool CheckXmlNode(XmlNode root, string name) {
            #region 判断是否已经添加

            return root.ChildNodes.Cast<XmlNode>()
                .Where(node => node.Attributes?["Include"] != null)
                .All(node => !node.Attributes["Include"].Value.Equals(name));

            #endregion
        }

        #endregion
    }
}