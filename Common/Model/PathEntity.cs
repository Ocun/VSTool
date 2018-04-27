// create By 08628 20180411

namespace Digiwin.Chun.Common.Model {
    /// <summary>
    ///     路径相关
    /// </summary>
    public class PathEntity {

        /// <summary>
        ///     產生接口dll名稱
        /// </summary>
        public string BusinessDllName { get; set; } = string.Empty;

        /// <summary>
        ///     產生實現dll名稱
        /// </summary>
        public string ImplementDllName { get; set; } = string.Empty;

        /// <summary>
        ///     UI端接口dll名稱
        /// </summary>
        public string UiDllName { get; set; } = string.Empty;

        /// <summary>
        ///     ui端實現dll名稱
        /// </summary>
        public string UiImplementDllName { get; set; } = string.Empty;

        /// <summary>
        ///     導出目錄
        /// </summary>
        public string ExportFullPath { get; set; } = string.Empty;

        /// <summary>
        ///     个案源码之接口目录
        /// </summary>
        public string BusinessDir { get; set; } = string.Empty;

        /// <summary>
        ///     个案源码之接口实现目录
        /// </summary>
        public string ImplementDir { get; set; } = string.Empty;

        /// <summary>
        ///     个案源码之接口目录
        /// </summary>
        public string UiDir { get; set; } = string.Empty;

        /// <summary>
        ///     个案源码之客户端目录
        /// </summary>
        public string UiImplementDir { get; set; } = string.Empty;

        /// <summary>
        ///     服务端部署路径
        /// </summary>
        public string ServerFullPath { get; set; } = string.Empty;
        /// <summary>
        ///     dll部署路径
        /// </summary>
        public string ServerProgramsFullPath { get; set; } = string.Empty;

        /// <summary>
        ///    客户端部署路径
        /// </summary>
        public string DeployFullPath { get; set; } = string.Empty;
        /// <summary>
        ///     dll部署路径
        /// </summary>
        public string DeployProgramsFullPath { get; set; } = string.Empty;

        /// <summary>
        ///     新typekey目录,相对目录
        /// </summary>
        public string TypeKeyRootDir { get; set; } = string.Empty;

        /// <summary>
        ///     新typekey目录，绝对目录
        /// </summary>
        public string TypeKeyFullRootDir { get; set; } = string.Empty;

        /// <summary>
        ///     借用typekey路径(代码)，相对路径
        /// </summary>
        public string PkgTypeKeyRootDir { get; set; } = string.Empty;

        /// <summary>
        ///     借用typekey路径(代码)，绝对路径
        /// </summary>
        public string PkgTypeKeyFullRootDir { get; set; } = string.Empty;

        
    }
}