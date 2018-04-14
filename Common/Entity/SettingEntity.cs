// create By 08628 20180411
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
    public partial class SettingPathEntity
    {

        private string _serverDirField;

        private string _deployServerDirField;

        private string _industryServerDirField;

        private string _industryDeployDirField;

        private string _programsField;

        private object _basicDirField;

        private string _exportDirField;

        private string _businessDirExtentionField;

        private string _implementDirExtentionField;

        private string _uIDirExtentionField;

        private string _dllExtentionField;

        private string _packageBaseNameField;

        private string _templateDirField;

        private string _templateTypeKeyField;
        

        /// <summary>
        /// 服务端部署路径
        /// </summary>
        public string ServerDir
        {
            get => this._serverDirField;
            set => this._serverDirField = value;
        }
        /// <summary>
        /// 客户端部署路径
        /// </summary>
        public string DeployServerDir
        {
            get => this._deployServerDirField;
            set => this._deployServerDirField = value;
        }

       /// <summary>
       /// 行业包服务端部署路径
       /// </summary>
        public string IndustryServerDir
        {
            get => this._industryServerDirField;
           set => this._industryServerDirField = value;
       }

        /// <summary>
        /// 行业包客户端部署路径
        /// </summary>
        public string IndustryDeployDir
        {
            get => this._industryDeployDirField;
            set => this._industryDeployDirField = value;
        }

        /// <summary>
        /// 平台dll目录
        /// </summary>
        public string Programs
        {
            get => this._programsField;
            set => this._programsField = value;
        }

        /// <summary>
        /// 源码路径
        /// </summary>
        public object BasicDir
        {
            get => this._basicDirField;
            set => this._basicDirField = value;
        }

        /// <summary>
        /// 导出路径
        /// </summary>
        public string ExportDir
        {
            get => this._exportDirField;
            set => this._exportDirField = value;
        }

        /// <summary>
        /// Business
        /// </summary>
        public string BusinessDirExtention
        {
            get => this._businessDirExtentionField;
            set => this._businessDirExtentionField = value;
        }

        /// <summary>
        /// Implement
        /// </summary>
        public string ImplementDirExtention
        {
            get => this._implementDirExtentionField;
            set => this._implementDirExtentionField = value;
        }

        /// <summary>
        /// UI
        /// </summary>
        public string UIDirExtention
        {
            get => this._uIDirExtentionField;
            set => this._uIDirExtentionField = value;
        }

        /// <summary>
        /// dll
        /// </summary>
        public string DllExtention
        {
            get => this._dllExtentionField;
            set => this._dllExtentionField = value;
        }

        /// <summary>
        /// 包名前缀
        /// </summary>
        public string PackageBaseName
        {
            get => this._packageBaseNameField;
            set => this._packageBaseNameField = value;
        }

       /// <summary>
       /// 模版目录
       /// </summary>
        public string TemplateDir
        {
            get => this.TemplateDirField;
           set => this.TemplateDirField = value;
       }

        public string TemplateTypeKey {
            get => _templateTypeKeyField;
            set => _templateTypeKeyField = value;
        }

        public string TemplateDirField {
            get => _templateDirField;
            set => _templateDirField = value;
        }
    }



}
