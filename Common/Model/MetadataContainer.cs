using System;
using System.Xml.Serialization;

namespace Digiwin.Chun.Common.Model {
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [XmlRoot(Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement", IsNullable = false)]
    [Serializable]
    public class MetadataContainer {
        /// <remarks />
        [XmlArrayItem("DataEntityType", IsNullable = false)]
        public DataEntityType[] DataEntityTypes { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string FileVersion { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string ProductVersion { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string ProductName { get; set; }

        /// <remarks />
        [XmlAttribute]
        public string CopyRight { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class DataEntityType {
        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string Alias { get; set; }

        /// <remarks />
        public string DataEntityTypeType { get; set; }

        /// <remarks />
        public string DisplayName { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string PrimaryKey { get; set; }

        /// <remarks />
        public string Domain { get; set; }

        /// <remarks />
        public string BusinessPrimaryKey { get; set; }

        /// <remarks />
        public Properties Properties { get; set; }

        /// <remarks />
        [XmlArrayItem("DataEntityIndex", IsNullable = false)]
        public DataEntityIndex[] DataEntityIndexes { get; set; }

        /// <remarks />
        [XmlArrayItem("DataEntityTypeReference", IsNullable = false)]
        public InterfaceReference[] InterfaceReferences { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class Properties {
        /// <remarks />
        [XmlElement("CollectionProperty", typeof(CollectionProperty))]
        [XmlElement("ComplexProperty", typeof(ComplexProperty))]
        [XmlElement("ReferenceProperty", typeof(ReferenceProperty))]
        [XmlElement("SimpleProperty", typeof(SimpleProperty))]
        public object[] Items { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class CollectionProperty {
        /// <remarks />
        public string ItemDataEntityTypeName { get; set; }

        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string Alias { get; set; }

        /// <remarks />
        public string DisplayName { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string IsBrowsable { get; set; }

        /// <remarks />
        public string IsSystem { get; set; }

        /// <remarks />
        public string BusinessTypeName { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class ComplexProperty {
        /// <remarks />
        public string DataEntityTypeName { get; set; }

        /// <remarks />
        public string PickListType { get; set; }

        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string Alias { get; set; }

        /// <remarks />
        public string DisplayName { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string IsBrowsable { get; set; }

        /// <remarks />
        public string IsSystem { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class ReferenceProperty {
        // private string defaultValueFieldSpecified;

        /// <remarks />
        public string ReferenceToName { get; set; }

        /// <remarks />
        public string IsIndex { get; set; }

        /// <remarks />
        public string IsUnique { get; set; }

        /// <remarks />
        public string AutoSync { get; set; }

        /// <remarks />
        public string DefaultValue { get; set; }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public string DefaultValueSpecified
        //{
        //    get
        //    {
        //        return this.defaultValueFieldSpecified;
        //    }
        //    set
        //    {
        //        this.defaultValueFieldSpecified = value;
        //    }
        //}

        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string Alias { get; set; }

        /// <remarks />
        public string DisplayName { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string IsBrowsable { get; set; }

        /// <remarks />
        public string IsSystem { get; set; }

        /// <remarks />
        public string BusinessTypeName { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class SimpleProperty {
        // private string ignoreUpdateFieldSpecified;

        // private string sizeFieldSpecified;

        /// <remarks />
        public string Size { get; set; }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public string SizeSpecified
        //{
        //    get
        //    {
        //        return this.sizeFieldSpecified;
        //    }
        //    set
        //    {
        //        this.sizeFieldSpecified = value;
        //    }
        //}

        /// <remarks />
        public string IsIndex { get; set; }

        /// <remarks />
        public string IsUnique { get; set; }

        /// <remarks />
        public string AutoSync { get; set; }

        /// <remarks />
        public string IgnoreUpdate { get; set; }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public string IgnoreUpdateSpecified
        //{
        //    get
        //    {
        //        return this.ignoreUpdateFieldSpecified;
        //    }
        //    set
        //    {
        //        this.ignoreUpdateFieldSpecified = value;
        //    }
        //}

        /// <remarks />
        public string DefaultValue { get; set; }

        /// <remarks />
        public string PickListType { get; set; }

        /// <remarks />
        public string DbType { get; set; }

        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string Alias { get; set; }

        /// <remarks />
        public string DisplayName { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        public string IsBrowsable { get; set; }

        /// <remarks />
        public string IsSystem { get; set; }

        /// <remarks />
        public string BusinessTypeName { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class DataEntityIndex {
        //private string uniqueFieldSpecified;

        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string DataEntityIndexId { get; set; }

        /// <remarks />
        public string Unique { get; set; }

        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public string UniqueSpecified
        //{
        //    get
        //    {
        //        return this.uniqueFieldSpecified;
        //    }
        //    set
        //    {
        //        this.uniqueFieldSpecified = value;
        //    }
        //}
        /// <remarks />
        /// <remarks />
        public string RelatedProperties { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class IndexesDataEntityIndex {
        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string DataEntityIndexId { get; set; }

        /// <remarks />
        public string Clustered { get; set; }

        /// <remarks />
        public string Unique { get; set; }

        /// <remarks />
        public string RelatedProperties { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class InterfaceReference {
        /// <remarks />
        public string Name { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true, Namespace = "http://schemas.dcms.com/configuration/MetadataContainerElement")]
    [Serializable]
    public class DataEntityTypeReference {
        /// <remarks />
        public string Name { get; set; }
    }
}