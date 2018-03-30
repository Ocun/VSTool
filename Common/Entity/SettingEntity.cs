using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Implement
{


    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class SettingPathEntity
    {

        private string serverDirField;

        private string deployServerDirField;

        private string industryServerDirField;

        private string industryDeployDirField;

        private string programsField;

        private object basicDirField;

        private string exportDirField;

        private string businessDirExtentionField;

        private string implementDirExtentionField;

        private string uIDirExtentionField;

        private string dllExtentionField;

        private string packageBaseNameField;

        private string templateDirField;

        private string templateTypeKeyField;
        

        /// <summary>
        /// 服务端部署路径
        /// </summary>
        public string ServerDir
        {
            get
            {
                return this.serverDirField;
            }
            set
            {
                this.serverDirField = value;
            }
        }
        /// <summary>
        /// 客户端部署路径
        /// </summary>
        public string DeployServerDir
        {
            get
            {
                return this.deployServerDirField;
            }
            set
            {
                this.deployServerDirField = value;
            }
        }

       /// <summary>
       /// 行业包服务端部署路径
       /// </summary>
        public string IndustryServerDir
        {
            get
            {
                return this.industryServerDirField;
            }
            set
            {
                this.industryServerDirField = value;
            }
        }

        /// <summary>
        /// 行业包客户端部署路径
        /// </summary>
        public string IndustryDeployDir
        {
            get
            {
                return this.industryDeployDirField;
            }
            set
            {
                this.industryDeployDirField = value;
            }
        }

        /// <summary>
        /// 平台dll目录
        /// </summary>
        public string Programs
        {
            get
            {
                return this.programsField;
            }
            set
            {
                this.programsField = value;
            }
        }

        /// <summary>
        /// 源码路径
        /// </summary>
        public object BasicDir
        {
            get
            {
                return this.basicDirField;
            }
            set
            {
                this.basicDirField = value;
            }
        }

        /// <summary>
        /// 导出路径
        /// </summary>
        public string ExportDir
        {
            get
            {
                return this.exportDirField;
            }
            set
            {
                this.exportDirField = value;
            }
        }

        /// <summary>
        /// Business
        /// </summary>
        public string BusinessDirExtention
        {
            get
            {
                return this.businessDirExtentionField;
            }
            set
            {
                this.businessDirExtentionField = value;
            }
        }

        /// <summary>
        /// Implement
        /// </summary>
        public string ImplementDirExtention
        {
            get
            {
                return this.implementDirExtentionField;
            }
            set
            {
                this.implementDirExtentionField = value;
            }
        }

        /// <summary>
        /// UI
        /// </summary>
        public string UIDirExtention
        {
            get
            {
                return this.uIDirExtentionField;
            }
            set
            {
                this.uIDirExtentionField = value;
            }
        }

        /// <summary>
        /// dll
        /// </summary>
        public string DllExtention
        {
            get
            {
                return this.dllExtentionField;
            }
            set
            {
                this.dllExtentionField = value;
            }
        }

        /// <summary>
        /// 包名前缀
        /// </summary>
        public string PackageBaseName
        {
            get
            {
                return this.packageBaseNameField;
            }
            set
            {
                this.packageBaseNameField = value;
            }
        }

       /// <summary>
       /// 模版目录
       /// </summary>
        public string TemplateDir
        {
            get
            {
                return this.templateDirField;
            }
            set
            {
                this.templateDirField = value;
            }
        }

        public string TemplateTypeKey {
            get { return templateTypeKeyField; }
            set { templateTypeKeyField = value; }
        }
    }



}
