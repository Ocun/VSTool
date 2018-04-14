// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Implement.Entity {

    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class MappingEntity
    {

        private MappingItem[] _mappingItemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MappingItem", IsNullable = false)]
        public MappingItem[] MappingItems
        {
            get => this._mappingItemsField;
            set => this._mappingItemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MappingItem
    {

        private string _idField;

        private string _descriptionField;

        private string[] _pathsField;

        /// <remarks/>
        public string Id
        {
            get => this._idField;
            set => this._idField = value;
        }

        /// <remarks/>
        public string Description
        {
            get => this._descriptionField;
            set => this._descriptionField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Path", IsNullable = false)]
        public string[] Paths
        {
            get => this._pathsField;
            set => this._pathsField = value;
        }
    }







}


