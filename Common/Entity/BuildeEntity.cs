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

        private BuildeType[] _buildeTypiesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("BuildeType", IsNullable = false)]
        public BuildeType[] BuildeTypies
        {
            get => this._buildeTypiesField;
            set => this._buildeTypiesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BuildeType
    {

        private string _idField;

        private string _nameField;

        private string _descriptionField;

        private string _checkedField;
        private string _readOnlyField;
        private string _showParWindowField;
        private string _partIdField;
        private string _isMergeField;
        private string _showCheckedBoxField;
        private string _isTools;
        private string _url;
        private string _visiable;

        /// <summary>
        /// 来源自同一模板代码片段是否合并
        /// </summary>
        public string IsMerge
        {
            get => _isMergeField;
            set => _isMergeField = value;
        }
        private BuildeType[] _buildeItemsField;
        private List<FileInfos> _fileInfosField;

        /// <remarks/>
        public string Id
        {
            get => this._idField;
            set => this._idField = value;
        }

        /// <remarks/>
        public string Name
        {
            get => this._nameField;
            set => this._nameField = value;
        }

        /// <remarks/>
        public string Description
        {
            get => this._descriptionField;
            set => this._descriptionField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("BuildeItem", IsNullable = false)]
        public BuildeType[] BuildeItems
        {
            get => this._buildeItemsField;
            set => this._buildeItemsField = value;
        }
        /// <summary>
        /// 是否选中
        /// </summary>
        public string Checked {
            get => _checkedField;
            set => _checkedField = value;
        }
        /// <summary>
        /// 是否可选
        /// </summary>
        public string ReadOnly {
            get => _readOnlyField;
            set => _readOnlyField = value;
        }
        /// <summary>
        /// 将要生成的文件信息
        /// </summary>
        public List<FileInfos> FileInfos {
            get => _fileInfosField;
            set => _fileInfosField = value;
        }
        /// <summary>
        /// 点击是否显示参数框
        /// </summary>
        public string ShowParWindow {
            get => _showParWindowField;
            set => _showParWindowField = value;
        }

        /// <summary>
        /// 指示所属代码片段
        /// </summary>
        public string PartId {
            get => _partIdField;
            set => _partIdField = value;
        }

        /// <summary>
        /// 是否显示复选框
        /// </summary>
        public string ShowCheckedBox {
            get => _showCheckedBoxField;
            set => _showCheckedBoxField = value;
        }

        /// <summary>
        /// 是否时外部工具
        /// </summary>
        public string IsTools {
            get => _isTools;
            set => _isTools = value;
        }

        /// <summary>
        /// 外部应用程序地址
        /// </summary>
        public string Url {
            get => _url;
            set => _url = value;
        }
        /// <summary>
        /// 项目是否可见，仅在无子项时生效
        /// </summary>
        public string Visiable {
            get => _visiable;
            set => _visiable = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FileInfos {

        private string _fileNameFiled;
        private string _classNameFiled;
        private string _functionNameFiled;
        private string _actionNameFiled;
        private string _fromPathFiled;
        private string _toPathFiled;
        private string _basePathFiled;
        private string _partIdField;
        private string _idField;
        private string _isMergeField;

        /// <summary>
        /// 实际点
        /// </summary>
        public string ActionName{
            get => _actionNameFiled;
            set => _actionNameFiled = value;
        }

        /// <summary>
        /// 方法名
        /// </summary>
        public string FunctionName{
            get => _functionNameFiled;
            set => _functionNameFiled = value;
        }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName {
            get => _classNameFiled;
            set => _classNameFiled = value;
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName {
            get => _fileNameFiled;
            set => _fileNameFiled = value;
        }

        /// <summary>
        /// 模板路径
        /// </summary>
        public string FromPath
        {
            get => _fromPathFiled;
            set => _fromPathFiled = value;
        }
        /// <summary>
        /// 个案路径
        /// </summary>
        public string ToPath
        {
            get => _toPathFiled;
            set => _toPathFiled = value;
        }
        /// <summary>
        /// 模板路径（相对路径）
        /// </summary>
        public string BasePath {
            get => _basePathFiled;
            set => _basePathFiled = value;
        }
        /// <summary>
        /// 引用代码片段
        /// </summary>
        public string PartId {
            get => _partIdField;
            set => _partIdField = value;
        }
        /// <summary>
        /// 来源自同一模板代码片段是否合并
        /// </summary>
        public string IsMerge {
            get => _isMergeField;
            set => _isMergeField = value;
        }

        public string Id {
            get => _idField;
            set => _idField = value;
        }
    }



}
