// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Implement.Entity
{

    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class BuildeEntity
    {

        private BuildeType[] buildeTypiesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("BuildeType", IsNullable = false)]
        public BuildeType[] BuildeTypies
        {
            get
            {
                return this.buildeTypiesField;
            }
            set
            {
                this.buildeTypiesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BuildeType
    {

        private string idField;

        private string nameField;

        private string descriptionField;

        private string checkedField;
        private string readOnlyField;
        private string showParWindowField;
        private string partIDField;
        private string isMergeField;
        private string showCheckedBoxField;
        private string isTools;
        private string url;

        /// <summary>
        /// 来源自同一模板代码片段是否合并
        /// </summary>
        public string IsMerge
        {
            get { return isMergeField; }
            set { isMergeField = value; }
        }
        private BuildeType[] buildeItemsField;
        private List<FileInfos> fileInfosField;

        /// <remarks/>
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("BuildeItem", IsNullable = false)]
        public BuildeType[] BuildeItems
        {
            get
            {
                return this.buildeItemsField;
            }
            set
            {
                this.buildeItemsField = value;
            }
        }
        /// <summary>
        /// 是否选中
        /// </summary>
        public string Checked {
            get { return checkedField; }
            set { checkedField = value; }
        }
        /// <summary>
        /// 是否可选
        /// </summary>
        public string ReadOnly {
            get { return readOnlyField; }
            set { readOnlyField = value; }
        }
        /// <summary>
        /// 将要生成的文件信息
        /// </summary>
        public List<FileInfos> FileInfos {
            get { return fileInfosField; }
            set { fileInfosField = value; }
        }
        /// <summary>
        /// 点击是否显示参数框
        /// </summary>
        public string ShowParWindow {
            get { return showParWindowField; }
            set { showParWindowField = value; }
        }

        /// <summary>
        /// 指示所属代码片段
        /// </summary>
        public string PartId {
            get { return partIDField; }
            set { partIDField = value; }
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        public string ShowCheckedBox {
            get { return showCheckedBoxField; }
            set { showCheckedBoxField = value; }
        }

        public string IsTools {
            get { return isTools; }
            set { isTools = value; }
        }

        public string Url {
            get { return url; }
            set { url = value; }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FileInfos {

        public string fileNameFiled;
        public string classNameFiled;
        public string functionNameFiled;
        public string actionNameFiled;
        public string fromPathFiled;
        public string toPathFiled;
        public string basePathFiled;
        public string partIDField;
        public string idField;
        public string isMergeField;

        /// <summary>
        /// 实际点
        /// </summary>
        public string ActionName{
            get { return actionNameFiled; }
            set { actionNameFiled = value; }
        }

        /// <summary>
        /// 方法名
        /// </summary>
        public string FunctionName{
            get { return functionNameFiled; }
            set { functionNameFiled = value; }
        }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName {
            get { return classNameFiled; }
            set { classNameFiled = value; }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName {
            get { return fileNameFiled; }
            set { fileNameFiled = value; }
        }

        /// <summary>
        /// 模板路径
        /// </summary>
        public string FromPath
        {
            get { return fromPathFiled; }
            set { fromPathFiled = value; }
        }
        /// <summary>
        /// 个案路径
        /// </summary>
        public string ToPath
        {
            get { return toPathFiled; }
            set { toPathFiled = value; }
        }
        /// <summary>
        /// 模板路径（相对路径）
        /// </summary>
        public string BasePath {
            get { return basePathFiled; }
            set { basePathFiled = value; }
        }
        /// <summary>
        /// 引用代码片段
        /// </summary>
        public string PartId {
            get { return partIDField; }
            set { partIDField = value; }
        }
        /// <summary>
        /// 来源自同一模板代码片段是否合并
        /// </summary>
        public string IsMerge {
            get { return isMergeField; }
            set { isMergeField = value; }
        }

        public string Id {
            get { return idField; }
            set { idField = value; }
        }
    }



}
