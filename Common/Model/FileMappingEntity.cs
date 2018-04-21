// create By 08628 20180411

using System;
using System.Xml.Serialization;

namespace Common.Implement.Entity {
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks />
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class MappingEntity {
        /// <remarks />
        [XmlArrayItem("MappingItem", IsNullable = false)]
        public MappingItem[] MappingItems { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    [Serializable]
    public class MappingItem {
        /// <remarks />
        public string Id { get; set; }
        /// <remarks />
        public string Description { get; set; }
        /// <remarks />
        [XmlArrayItem("Path", IsNullable = false)]
        public string[] Paths { get; set; }
    }
}