using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Implement
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
        private string _UIDllName = string.Empty;
        private string _UIImplementDllName = string.Empty;


        private string _businessDir = string.Empty;
        private string __implementDllDir = string.Empty;
        private string _UIDir= string.Empty;
        private string _UIImplementDir = string.Empty;


        //private string _UIImplementDir = string.Empty;
        //private string _UIImplementDir = string.Empty;



        /// <summary>
        /// 平臺服務目錄
        /// </summary>
        public string ServerPath {
            get { return _serverPath; }
            set { _serverPath = value; }
        }
        /// <summary>
        /// 平臺客戶目錄
        /// </summary>
        public string DeployServerPath {
            get { return _deployServerPath; }
            set { _deployServerPath = value; }
        }
        /// <summary>
        /// 產生接口dll名稱
        /// </summary>
        public string BusinessDllName {
            get { return _businessDllName; }
            set { _businessDllName = value; }
        }
        /// <summary>
        /// 產生實現dll名稱
        /// </summary>
        public string ImplementDllName {
            get { return _implementDllName; }
            set { _implementDllName = value; }
        }
        /// <summary>
        /// UI端接口dll名稱
        /// </summary>
        public string UIDllName {
            get { return _UIDllName; }
            set { _UIDllName = value; }
        }
        /// <summary>
        /// ui端實現dll名稱
        /// </summary>
        public string UIImplementDllName {
            get { return _UIImplementDllName; }
            set { _UIImplementDllName = value; }
        }
        /// <summary>
        /// 導出目錄
        /// </summary>
        public string ExportPath {
            get { return _exportPath; }
            set { _exportPath = value; }
        }
        /// <summary>
        /// 个案源码之接口目录
        /// </summary>
        public string BusinessDir {
            get { return _businessDir; }
            set { _businessDir = value; }
        }
        /// <summary>
        /// 个案源码之接口实现目录
        /// </summary>
        public string ImplementDllDir {
            get { return __implementDllDir; }
            set { __implementDllDir = value; }
        }
        /// <summary>
        /// 个案源码之接口目录
        /// </summary>
        public string UIDir {
            get { return _UIDir; }
            set { _UIDir = value; }
        }
        /// <summary>
        /// 个案源码之客户端目录
        /// </summary>
        public string UIImplementDir {
            get { return _UIImplementDir; }
            set { _UIImplementDir = value; }
        }
    }
}
