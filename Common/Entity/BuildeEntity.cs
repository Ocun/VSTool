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

        private BuildeType[] buildeItemsField;

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

        public string Checked {
            get { return checkedField; }
            set { checkedField = value; }
        }

        public string ReadOnly {
            get { return readOnlyField; }
            set { readOnlyField = value; }
        }
    }

   

}
