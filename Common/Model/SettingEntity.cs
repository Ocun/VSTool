// create By 08628 20180411

using System;
using System.Xml.Serialization;

namespace Digiwin.Chun.Common.Model {
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks />
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    [Serializable]
    public class SettingPathEntity {
        /// <summary>
        ///     服务端部署路径
        /// </summary>
        public string ServerDir { get; set; }

        /// <summary>
        ///     客户端部署路径
        /// </summary>
        public string DeployServerDir { get; set; }

        /// <summary>
        ///     行业包服务端部署路径
        /// </summary>
        public string IndustryServerDir { get; set; }

        /// <summary>
        ///     行业包客户端部署路径
        /// </summary>
        public string IndustryDeployDir { get; set; }

        /// <summary>
        ///     平台dll目录
        /// </summary>
        public string Programs { get; set; }

        /// <summary>
        ///     源码路径
        /// </summary>
        public object BasicDir { get; set; }

        /// <summary>
        ///     导出路径
        /// </summary>
        public string ExportDir { get; set; }

        /// <summary>
        ///     Business
        /// </summary>
        public string BusinessDirExtention { get; set; }

        /// <summary>
        ///     Implement
        /// </summary>
        public string ImplementDirExtention { get; set; }

        /// <summary>
        ///     UI
        /// </summary>
        public string UiDirExtention { get; set; }

        /// <summary>
        ///     dll
        /// </summary>
        public string DllExtention { get; set; }

        /// <summary>
        ///     包名前缀
        /// </summary>
        public string PackageBaseName { get; set; }

        /// <summary>
        ///     模版目录
        /// </summary>
        public string TemplateDir {
            get => TemplateDirField;
            set => TemplateDirField = value;
        }

        /// <summary>
        /// 模板typeKey
        /// </summary>
        public string TemplateTypeKey { get; set; }

        /// <summary>
        /// 模板路径
        /// </summary>
        public string TemplateDirField { get; set; }
    }
}