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

        private MappingItem[] mappingItemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MappingItem", IsNullable = false)]
        public MappingItem[] MappingItems
        {
            get
            {
                return this.mappingItemsField;
            }
            set
            {
                this.mappingItemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class MappingItem
    {

        private string idField;

        private string descriptionField;

        private string[] pathsField;

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
        [System.Xml.Serialization.XmlArrayItemAttribute("Path", IsNullable = false)]
        public string[] Paths
        {
            get
            {
                return this.pathsField;
            }
            set
            {
                this.pathsField = value;
            }
        }
    }







}


