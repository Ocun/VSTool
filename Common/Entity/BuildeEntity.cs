// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Common.Implement.Entity {
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks />
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class BuildeEntity {
        /// <remarks />
        [XmlArrayItem("BuildeType", IsNullable = false)]
        public BuildeType[] BuildeTypies { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    [Serializable]
    public class BuildeType {
    
        /// <summary>
        ///     来源自同一模板代码片段是否合并
        /// </summary>
        public string IsMerge { get; set; }

        /// <remarks />
        public string Id { get; set; }

        /// <remarks />
        public string Name { get; set; }

        /// <remarks />
        public string Description { get; set; }

        /// <remarks />
        [XmlArrayItem("BuildeItem", IsNullable = false)]
        public BuildeType[] BuildeItems { get; set; }

        /// <summary>
        ///     是否选中
        /// </summary>
        public string Checked { get; set; }

        /// <summary>
        ///     是否可选
        /// </summary>
        public string ReadOnly { get; set; }

        /// <summary>
        ///   对应的模板文件
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public List<FileInfos> FileInfos { get; set; }

        /// <summary>
        ///     点击是否显示参数框
        /// </summary>
        public string ShowParWindow { get; set; }

        /// <summary>
        ///     指示所属代码片段
        /// </summary>
        public string PartId { get; set; }

        /// <summary>
        ///     是否显示复选框
        /// </summary>
        public string ShowCheckedBox { get; set; }

        /// <summary>
        ///     是否时外部工具
        /// </summary>
        public string IsTools { get; set; }

        /// <summary>
        ///     外部应用程序地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     项目是否可见，仅在无子项时生效
        /// </summary>
        public string Visiable { get; set; }

        /// <summary>
        ///     是否是插件
        /// </summary>
        public string IsPlug { get; set; }

        /// <summary>
        ///     插件路径
        /// </summary>
        public string PlugPath { get; set; }

        /// <summary>
        ///     模块名
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///     是否显示Icon
        /// </summary>
        public string ShowIcon { get; set; }
    }

    /// <remarks />
    [XmlType(AnonymousType = true)]
    [Serializable]
    public class FileInfos {
        /// <summary>
        ///     时机点
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        ///     方法名
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        ///     类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        ///     文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        ///     模板路径
        /// </summary>
        public string FromPath { get; set; }

        /// <summary>
        ///     个案路径
        /// </summary>
        public string ToPath { get; set; }

        /// <summary>
        ///     模板路径（相对路径）
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        ///     引用代码片段
        /// </summary>
        public string PartId { get; set; }

        /// <summary>
        ///     来源自同一模板代码片段是否合并
        /// </summary>
        public string IsMerge { get; set; }

        /// <summary>
        ///     Id唯一
        /// </summary>
        public string Id { get; set; }
    }
}