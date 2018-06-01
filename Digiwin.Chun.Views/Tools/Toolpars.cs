// create By 08628 20180411

using System;
using Digiwin.Chun.Models;
using static Digiwin.Chun.Common.Tools.PathTools;
using static Digiwin.Chun.Common.Tools.ReadToEntityTools;

namespace Digiwin.Chun.Views.Tools {
    /// <summary>
    ///     窗体参数
    /// </summary>
    public class Toolpars {
        private BuildeEntity _builderEntity;
        private MappingEntity _fileMappingEntity;
        private FormEntity _formEntity;
        private PathEntity _pathEntity;
        private SettingPathEntity _settingPathEntity;

        /// <summary>
        /// 配置类型
        /// </summary>
        public ModelType ModelType { get; set; }

        /// <summary>
        /// </summary>
        public Toolpars() {
            MvsToolpath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }

        /// <summary>
        ///     文件映射
        /// </summary>
        public MappingEntity FileMappingEntity {
            get {
                return _fileMappingEntity = GetEntity(_fileMappingEntity, "FileMapping");
            }
            set => _fileMappingEntity = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetEntity<T>(T obj,string fileName) where T:class {
            if (obj != null)
                return obj;
            var path = GetSettingPath(fileName,ModelType);
            if (CheckFile(path))
                obj = ReadToEntity<T>(path, ModelType);
            return obj;
        }

        /// <summary>
        ///     一些路径中常用字符配置
        /// </summary>
        public SettingPathEntity SettingPathEntity {
            get {
                return _settingPathEntity = GetEntity(_settingPathEntity, "SettingPathEntity"); 
            }
            set => _settingPathEntity = value;
        }

        /// <summary>
        ///     菜单项目实体
        /// </summary>
        public BuildeEntity BuilderEntity {
            get {
                return _builderEntity = GetEntity(_builderEntity, "BuildeEntity"); 
            }
            set => _builderEntity = value;
        }


        /// <summary>
        ///     要复制的typekey
        /// </summary>
        public string OldTypekey { get; set; }

        /// <summary>
        ///     全部参数信息，平台路径，设计器路径，个案路径，客户名，行业包
        /// </summary>
        public string Mall { get; set; }

        /// <summary>
        /// </summary>
        public string Mcodepath { get; set; }

        /// <summary>
        ///     客户名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        ///     行業包
        /// </summary>
        public bool MIndustry { get; set; }

        /// <summary>
        ///     VS当前路径
        /// </summary>
        public string MvsToolpath { get; set; }

        /// <summary>
        /// </summary>
        public bool MDistince { get; set; }

        /// <summary>
        /// </summary>
        public string Mpath { get; set; }

        /// <summary>
        /// </summary>
        public string MInpath { get; set; }

        /// <summary>
        ///     平台路径
        /// </summary>
        public string Mplatform { get; set; }

        /// <summary>
        ///     设计器路径
        /// </summary>
        public string MdesignPath { get; set; }

        /// <summary>
        ///     版本
        /// </summary>
        public string MVersion { get; set; }

        /// <summary>
        ///     窗體參數
        /// </summary>
        public FormEntity FormEntity {
            get => _formEntity ?? (_formEntity = new FormEntity());
            set => _formEntity = value;
        }

        /// <summary>
        ///     生成路徑
        /// </summary>
        public PathEntity PathEntity {
            get {
                _pathEntity =MyTools.GetPathEntity(this);
                return _pathEntity;
            }
            set => _pathEntity = value;
        }

    }
}