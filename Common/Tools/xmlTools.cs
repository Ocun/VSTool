// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Common.Implement.Tools
{
    public static class XMLTools
    {


        public static void modiXml(string xmlPath, string Id, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNodeList xns = xmlDoc.SelectNodes("//BuildeItem");
            foreach (XmlNode xn in xns)
            {
                var childNodes = xn.ChildNodes;
                bool f = true;
                foreach (XmlNode node in childNodes)
                {
                    if (node.Name == "Id"
                        && node.InnerText.Equals(Id))
                    {
                        f = false;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (!f)
                {
                    foreach (XmlNode node in childNodes)
                    {
                        if (node.Name == "Url")
                        {
                            node.InnerText = value;
                            break;
                        }

                    }

                    break;
                }

            }
            xmlDoc.Save(xmlPath);

        }

        #region  修改解决方案的csproj文件 添加类文件

        /// <summary>  
        /// 删除节点  
        /// </summary>  
        public static void XmlNodeByXPath(string xmlFileName, string xpath, List<string> pathList)
        {
            string basePath = Path.GetDirectoryName(xmlFileName) + @"\";
            List<string> abPathList = new List<string>();
            pathList.ForEach(f => {
                string fpath = f;
                int index = fpath.IndexOf(basePath);
                if (index > -1)
                {
                    abPathList.Add(fpath.Substring(index + basePath.Length));

                }
            }
            );
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);
            XmlNamespaceManager mnamespce = new XmlNamespaceManager(xmlDoc.NameTable);
            string NamespaceURI = xmlDoc.ChildNodes[1].NamespaceURI;
            XmlNodeList xNodes = null;
            if (NamespaceURI != null)
            {
                mnamespce.AddNamespace("nhb", NamespaceURI);
                xpath = string.Format(@"//nhb:{0}", xpath);
                xNodes = xmlDoc.SelectNodes(xpath, mnamespce);
            }
            else
            {
                xNodes = xmlDoc.SelectNodes(xpath);
            }

            if (xNodes != null)
            {
                for (int i = xNodes.Count - 1; i >= 0; i--)
                {
                    XmlElement xe = (XmlElement)xNodes[i];
                    var xmlAttributeCollection = xNodes[i].Attributes;
                    if (xmlAttributeCollection != null && xmlAttributeCollection["Include"] != null)
                    {

                        if (!abPathList.Contains(xmlAttributeCollection["Include"].Value))
                        {
                            xNodes[i].ParentNode.RemoveChild(xNodes[i]);
                        }
                    }
                }
                xmlDoc.Save(xmlFileName);
            }
        }


        /// <summary>
        /// 修改解决方案
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="CSName"></param>
        public static void ModiCS(string xmlPath, string CSName)
        {
            if (CSName.Contains(".designer.cs"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性 
                var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                String msname = CSName.Substring(CSName.LastIndexOf("\\") + 1);
                msname = msname.Substring(0, msname.IndexOf("."));
                msname = msname + ".cs";
                //loc.InnerText = model.loc;
                loc.InnerText = msname;
                xe1.AppendChild(loc);
                //xe1.InnerText = "WPF";
                var notExisted = checkXmlNode(root, CSName);
                if (notExisted)
                {
                    root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }
            else if (CSName.Contains(".resx"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("EmbeddedResource", xmlDoc.DocumentElement.NamespaceURI);
                //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性 
                var loc = xmlDoc.CreateElement("DependentUpon", xmlDoc.DocumentElement.NamespaceURI);
                String msname = CSName.Substring(CSName.LastIndexOf("\\") + 1);
                msname = msname.Substring(0, msname.IndexOf("."));
                msname = msname + ".cs";
                //loc.InnerText = model.loc;
                loc.InnerText = msname;
                xe1.AppendChild(loc);
                //xe1.InnerText = "WPF";
                var notExisted = checkXmlNode(root, CSName);
                if (notExisted)
                {
                    root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }

            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode root = xmlDoc.DocumentElement.ChildNodes[4]; //查找<bookstore>
                XmlElement xe1 = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI); //创建一个<book>节点
                xe1.SetAttribute("Include", CSName); //设置该节点genre属性    
                var notExisted = checkXmlNode(root, CSName);
                if (notExisted)
                {
                    root.AppendChild(xe1);
                }

                xmlDoc.Save(xmlPath);
            }
        }

        /// <summary>
        /// 判断节点是否存在
        /// </summary>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static bool checkXmlNode(XmlNode root, string name)
        {
            #region 判断是否已经添加

            bool notExisted = true;
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.Attributes["Include"] != null)
                {
                    if (node.Attributes["Include"].Value.Equals(name))
                    {
                        notExisted = false;
                        break;
                    }
                }
            }

            #endregion

            return notExisted;
        }

        #endregion
    }
}
