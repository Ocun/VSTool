// create By 08628 20180411
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Implement.Entity
{
    /// <summary>
    /// 路径相关
    /// </summary>
    public class PathEntity {
        private string _serverPath = string.Empty;
        private string _deployServerPath = string.Empty;

        private string _exportPath = string.Empty;
        private string _businessDllName = string.Empty;
        private string _implementDllName = string.Empty;
        private string _uiDllName = string.Empty;
        private string _uiImplementDllName = string.Empty;


        private string _businessDir = string.Empty;
        private string _implementDir = string.Empty;
        private string _uiDir= string.Empty;
        private string _uiImplementDir = string.Empty;


        private string _serverProgramsPath = string.Empty;
        private string _clientProgramsPath = string.Empty;
        private string _rootDir = string.Empty;
        

        
        /// <summary>
        /// 產生接口dll名稱
        /// </summary>
        public string BusinessDllName {
            get => _businessDllName;
            set => _businessDllName = value;
        }
        /// <summary>
        /// 產生實現dll名稱
        /// </summary>
        public string ImplementDllName {
            get => _implementDllName;
            set => _implementDllName = value;
        }
        /// <summary>
        /// UI端接口dll名稱
        /// </summary>
        public string UIDllName {
            get => _uiDllName;
            set => _uiDllName = value;
        }
        /// <summary>
        /// ui端實現dll名稱
        /// </summary>
        public string UIImplementDllName {
            get => _uiImplementDllName;
            set => _uiImplementDllName = value;
        }
        /// <summary>
        /// 導出目錄
        /// </summary>
        public string ExportPath {
            get => _exportPath;
            set => _exportPath = value;
        }
        /// <summary>
        /// 个案源码之接口目录
        /// </summary>
        public string BusinessDir {
            get => _businessDir;
            set => _businessDir = value;
        }
        /// <summary>
        /// 个案源码之接口实现目录
        /// </summary>
        public string ImplementDir {
            get => _implementDir;
            set => _implementDir = value;
        }
        /// <summary>
        /// 个案源码之接口目录
        /// </summary>
        public string UIDir {
            get => _uiDir;
            set => _uiDir = value;
        }
        /// <summary>
        /// 个案源码之客户端目录
        /// </summary>
        public string UIImplementDir {
            get => _uiImplementDir;
            set => _uiImplementDir = value;
        }
        /// <summary>
        /// dll部署路径
        /// </summary>
        public string ServerProgramsPath
        {
            get => _serverProgramsPath;
            set => _serverProgramsPath = value;
        }
        /// <summary>
        /// dll部署路径
        /// </summary>
        public string DeployProgramsPath
        {
            get => _clientProgramsPath;
            set => _clientProgramsPath = value;
        }
        /// <summary>
        /// 代码根目录
        /// </summary>
        public string RootDir
        {
            get => _rootDir;
            set => _rootDir = value;
        }
    }
}
