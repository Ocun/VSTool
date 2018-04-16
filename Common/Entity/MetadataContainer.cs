using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Implement.Entity 
{
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement", IsNullable = false)]
    public partial class MetadataContainer
    {

        private DataEntityType[] dataEntityTypesField;

        private string fileVersionField;

        private string productVersionField;

        private string productNameField;

        private string copyRightField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DataEntityType", IsNullable = false)]
        public DataEntityType[] DataEntityTypes
        {
            get
            {
                return this.dataEntityTypesField;
            }
            set
            {
                this.dataEntityTypesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FileVersion
        {
            get
            {
                return this.fileVersionField;
            }
            set
            {
                this.fileVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ProductVersion
        {
            get
            {
                return this.productVersionField;
            }
            set
            {
                this.productVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ProductName
        {
            get
            {
                return this.productNameField;
            }
            set
            {
                this.productNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CopyRight
        {
            get
            {
                return this.copyRightField;
            }
            set
            {
                this.copyRightField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class DataEntityType
    {

        private string nameField;

        private string aliasField;

        private string dataEntityTypeTypeField;

        private string displayNameField;

        private string descriptionField;

        private string primaryKeyField;

        private string domainField;

        private string businessPrimaryKeyField;

        private Properties propertiesField;

        private DataEntityIndex[] _dataEntityIndexesField;

        private InterfaceReference[] _interfaceReferencesField;

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
        public string Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        /// <remarks/>
        public string DataEntityTypeType
        {
            get
            {
                return this.dataEntityTypeTypeField;
            }
            set
            {
                this.dataEntityTypeTypeField = value;
            }
        }

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
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
        public string PrimaryKey
        {
            get
            {
                return this.primaryKeyField;
            }
            set
            {
                this.primaryKeyField = value;
            }
        }

        /// <remarks/>
        public string Domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
            }
        }

        /// <remarks/>
        public string BusinessPrimaryKey
        {
            get
            {
                return this.businessPrimaryKeyField;
            }
            set
            {
                this.businessPrimaryKeyField = value;
            }
        }

        /// <remarks/>
        public Properties Properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DataEntityIndex", IsNullable = false)]
        public DataEntityIndex[] DataEntityIndexes
        {
            get
            {
                return this._dataEntityIndexesField;
            }
            set
            {
                this._dataEntityIndexesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DataEntityTypeReference", IsNullable = false)]
        public InterfaceReference[] InterfaceReferences
        {
            get
            {
                return this._interfaceReferencesField;
            }
            set
            {
                this._interfaceReferencesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class Properties
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CollectionProperty", typeof(CollectionProperty))]
        [System.Xml.Serialization.XmlElementAttribute("ComplexProperty", typeof(ComplexProperty))]
        [System.Xml.Serialization.XmlElementAttribute("ReferenceProperty", typeof(ReferenceProperty))]
        [System.Xml.Serialization.XmlElementAttribute("SimpleProperty", typeof(SimpleProperty))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class CollectionProperty
    {

        private string itemDataEntityTypeNameField;

        private string nameField;

        private string aliasField;

        private string displayNameField;

        private string descriptionField;

        private bool isBrowsableField;

        private bool isSystemField;

        private string businessTypeNameField;

        /// <remarks/>
        public string ItemDataEntityTypeName
        {
            get
            {
                return this.itemDataEntityTypeNameField;
            }
            set
            {
                this.itemDataEntityTypeNameField = value;
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
        public string Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
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
        public bool IsBrowsable
        {
            get
            {
                return this.isBrowsableField;
            }
            set
            {
                this.isBrowsableField = value;
            }
        }

        /// <remarks/>
        public bool IsSystem
        {
            get
            {
                return this.isSystemField;
            }
            set
            {
                this.isSystemField = value;
            }
        }

        /// <remarks/>
        public string BusinessTypeName
        {
            get
            {
                return this.businessTypeNameField;
            }
            set
            {
                this.businessTypeNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class ComplexProperty
    {

        private string dataEntityTypeNameField;

        private string pickListTypeField;

        private string nameField;

        private string aliasField;

        private string displayNameField;

        private string descriptionField;

        private bool isBrowsableField;

        private bool isSystemField;

        /// <remarks/>
        public string DataEntityTypeName
        {
            get
            {
                return this.dataEntityTypeNameField;
            }
            set
            {
                this.dataEntityTypeNameField = value;
            }
        }

        /// <remarks/>
        public string PickListType
        {
            get
            {
                return this.pickListTypeField;
            }
            set
            {
                this.pickListTypeField = value;
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
        public string Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
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
        public bool IsBrowsable
        {
            get
            {
                return this.isBrowsableField;
            }
            set
            {
                this.isBrowsableField = value;
            }
        }

        /// <remarks/>
        public bool IsSystem
        {
            get
            {
                return this.isSystemField;
            }
            set
            {
                this.isSystemField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class ReferenceProperty
    {

        private string referenceToNameField;

        private bool isIndexField;

        private bool isUniqueField;

        private string autoSyncField;

        private bool defaultValueField;

        private bool defaultValueFieldSpecified;

        private string nameField;

        private string aliasField;

        private string displayNameField;

        private string descriptionField;

        private bool isBrowsableField;

        private bool isSystemField;

        private string businessTypeNameField;

        /// <remarks/>
        public string ReferenceToName
        {
            get
            {
                return this.referenceToNameField;
            }
            set
            {
                this.referenceToNameField = value;
            }
        }

        /// <remarks/>
        public bool IsIndex
        {
            get
            {
                return this.isIndexField;
            }
            set
            {
                this.isIndexField = value;
            }
        }

        /// <remarks/>
        public bool IsUnique
        {
            get
            {
                return this.isUniqueField;
            }
            set
            {
                this.isUniqueField = value;
            }
        }

        /// <remarks/>
        public string AutoSync
        {
            get
            {
                return this.autoSyncField;
            }
            set
            {
                this.autoSyncField = value;
            }
        }

        /// <remarks/>
        public bool DefaultValue
        {
            get
            {
                return this.defaultValueField;
            }
            set
            {
                this.defaultValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DefaultValueSpecified
        {
            get
            {
                return this.defaultValueFieldSpecified;
            }
            set
            {
                this.defaultValueFieldSpecified = value;
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
        public string Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
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
        public bool IsBrowsable
        {
            get
            {
                return this.isBrowsableField;
            }
            set
            {
                this.isBrowsableField = value;
            }
        }

        /// <remarks/>
        public bool IsSystem
        {
            get
            {
                return this.isSystemField;
            }
            set
            {
                this.isSystemField = value;
            }
        }

        /// <remarks/>
        public string BusinessTypeName
        {
            get
            {
                return this.businessTypeNameField;
            }
            set
            {
                this.businessTypeNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class SimpleProperty
    {

        private string sizeField;

        private bool sizeFieldSpecified;

        private bool isIndexField;

        private bool isUniqueField;

        private string autoSyncField;

        private bool ignoreUpdateField;

        private bool ignoreUpdateFieldSpecified;

        private string defaultValueField;

        private string pickListTypeField;

        private string dbTypeField;

        private string nameField;

        private string aliasField;

        private string displayNameField;

        private string descriptionField;

        private bool isBrowsableField;

        private bool isSystemField;

        private string businessTypeNameField;

        /// <remarks/>
        public string Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SizeSpecified
        {
            get
            {
                return this.sizeFieldSpecified;
            }
            set
            {
                this.sizeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool IsIndex
        {
            get
            {
                return this.isIndexField;
            }
            set
            {
                this.isIndexField = value;
            }
        }

        /// <remarks/>
        public bool IsUnique
        {
            get
            {
                return this.isUniqueField;
            }
            set
            {
                this.isUniqueField = value;
            }
        }

        /// <remarks/>
        public string AutoSync
        {
            get
            {
                return this.autoSyncField;
            }
            set
            {
                this.autoSyncField = value;
            }
        }

        /// <remarks/>
        public bool IgnoreUpdate
        {
            get
            {
                return this.ignoreUpdateField;
            }
            set
            {
                this.ignoreUpdateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IgnoreUpdateSpecified
        {
            get
            {
                return this.ignoreUpdateFieldSpecified;
            }
            set
            {
                this.ignoreUpdateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string DefaultValue
        {
            get
            {
                return this.defaultValueField;
            }
            set
            {
                this.defaultValueField = value;
            }
        }

        /// <remarks/>
        public string PickListType
        {
            get
            {
                return this.pickListTypeField;
            }
            set
            {
                this.pickListTypeField = value;
            }
        }

        /// <remarks/>
        public string DbType
        {
            get
            {
                return this.dbTypeField;
            }
            set
            {
                this.dbTypeField = value;
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
        public string Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
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
        public bool IsBrowsable
        {
            get
            {
                return this.isBrowsableField;
            }
            set
            {
                this.isBrowsableField = value;
            }
        }

        /// <remarks/>
        public bool IsSystem
        {
            get
            {
                return this.isSystemField;
            }
            set
            {
                this.isSystemField = value;
            }
        }

        /// <remarks/>
        public string BusinessTypeName
        {
            get
            {
                return this.businessTypeNameField;
            }
            set
            {
                this.businessTypeNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class DataEntityIndex
    {

        private string nameField;

        private string dataEntityIndexIdField;

        private bool uniqueField;

        private bool uniqueFieldSpecified;

        private string relatedPropertiesField;

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
        public string DataEntityIndexId
        {
            get
            {
                return this.dataEntityIndexIdField;
            }
            set
            {
                this.dataEntityIndexIdField = value;
            }
        }

        /// <remarks/>
        public bool Unique
        {
            get
            {
                return this.uniqueField;
            }
            set
            {
                this.uniqueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UniqueSpecified
        {
            get
            {
                return this.uniqueFieldSpecified;
            }
            set
            {
                this.uniqueFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string RelatedProperties
        {
            get
            {
                return this.relatedPropertiesField;
            }
            set
            {
                this.relatedPropertiesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class IndexesDataEntityIndex
    {

        private string nameField;

        private string dataEntityIndexIdField;

        private bool clusteredField;

        private bool uniqueField;

        private string relatedPropertiesField;

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
        public string DataEntityIndexId
        {
            get
            {
                return this.dataEntityIndexIdField;
            }
            set
            {
                this.dataEntityIndexIdField = value;
            }
        }

        /// <remarks/>
        public bool Clustered
        {
            get
            {
                return this.clusteredField;
            }
            set
            {
                this.clusteredField = value;
            }
        }

        /// <remarks/>
        public bool Unique
        {
            get
            {
                return this.uniqueField;
            }
            set
            {
                this.uniqueField = value;
            }
        }

        /// <remarks/>
        public string RelatedProperties
        {
            get
            {
                return this.relatedPropertiesField;
            }
            set
            {
                this.relatedPropertiesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class InterfaceReference
    {


        private string nameField;

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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    public partial class DataEntityTypeReference
    {

        private string nameField;

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
    }


}
