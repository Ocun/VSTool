// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Common.Implement.Tools {
    public static class XmlTools {
        public static void ModiXml(string xmlPath, string id, string value) {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            var xns = xmlDoc.SelectNodes("//BuildeItem");
            if (xns != null)
                foreach (XmlNode xn in xns) {
                    var childNodes = xn.ChildNodes;
                    var f = true;
                    foreach (XmlNode node in childNodes)
                        if (node.Name == "Id"
                            && node.InnerText.Equals(id)) {
                            f = false;
                            break;
                        }
                        else {
                            break;
                        }
                    if (f)
                        continue;
                    {
                        foreach (XmlNode node in childNodes)
                            if (node.Name == "Url") {
                                node.InnerText = value;
                                break;
                            }

                        break;
                    }
                }
            xmlDoc.Save(xmlPath);
        }

        #region  修改解决方案的csproj文件 添加类文件

        /// <summary>
        ///     删除节点
        /// </summary>
        public static void XmlNodeByXPath(string xmlFileName, string xpath, List<string> pathList) {
            var basePath = Path.GetDirectoryName(xmlFileName) + @"\";
            var abPathList = new List<string>();
            pathList.ForEach(f => {
                    var fpath = f;
                    var index = fpath.IndexOf(basePath, StringComparison.Ordinal);
                    if (index > -1)
                        abPathList.Add(fpath.Substring(index + basePath.Length));
                }
            );
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            var mnamespce = new XmlNamespaceManager(xmlDoc.NameTable);
            var namespaceUri = xmlDoc.ChildNodes[1].NamespaceURI;
            mnamespce.AddNamespace("nhb", namespaceUri);
            xpath = $@"//nhb:{xpath}";
            var xNodes = xmlDoc.SelectNodes(xpath, mnamespce);

            if (xNodes == null)
                return;
            for (var i = xNodes.Count - 1; i >= 0; i--) {
                var xmlAttributeCollection = xNodes[i].Attributes;
                if (xmlAttributeCollection?["Include"] == null)
                    continue;
                if (abPathList.Contains(xmlAttributeCollection["Include"].Value))
                    continue;
                var parentNode = xNodes[i].ParentNode;
                parentNode?.RemoveChild(xNodes[i]);
            }
            xmlDoc.Save(xmlFileName);
        }


        /// <summary>
        ///     修改解决方案
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="csName"></param>
        public static void ModiCs(string xmlPath, string csName) {
            if (csName.Contains(".designer.cs")) {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                if (xmlDoc.DocumentElement != null) {
                    var root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                    var xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                    xe1.SetAttribute("Include", csName); //设置该节点genre属性 
                    if (xmlDoc.DocumentElement != null) {
                        var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                        var msname = csName.Substring(csName.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                        msname = msname.Substring(0, msname.IndexOf(".", StringComparison.Ordinal));
                        msname = msname + ".cs";
                        //loc.InnerText = model.loc;
                        loc.InnerText = msname;
                        xe1.AppendChild(loc);
                    }
                    //xe1.InnerText = "WPF";
                    var notExisted = CheckXmlNode(root, csName);
                    if (notExisted)
                        root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }
            else if (csName.Contains(".resx")) {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
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
                        //loc.InnerText = model.loc;
                        loc.InnerText = msname;
                        xe1.AppendChild(loc);
                    }
                    //xe1.InnerText = "WPF";
                    var notExisted = CheckXmlNode(root, csName);
                    if (notExisted)
                        root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }

            else {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
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