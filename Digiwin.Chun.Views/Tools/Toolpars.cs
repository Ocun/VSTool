// create By 08628 20180411

using System;
using Digiwin.Chun.Models;
using static Digiwin.Chun.Common.Tools.PathTools;
using static Digiwin.Chun.Common.Tools.ReadToEntityTools;

namespace Digiwin.Chun.Views.Tools {
    /// <summary>
    ///     �������
    /// </summary>
    public class Toolpars {
        private BuildeEntity _builderEntity;
        private MappingEntity _fileMappingEntity;
        private FormEntity _formEntity;
        private PathEntity _pathEntity;
        private SettingPathEntity _settingPathEntity;

        /// <summary>
        /// ��������
        /// </summary>
        public ModelType ModelType { get; set; }

        /// <summary>
        /// </summary>
        public Toolpars() {
            MvsToolpath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }

        /// <summary>
        ///     �ļ�ӳ��
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
        ///     һЩ·���г����ַ�����
        /// </summary>
        public SettingPathEntity SettingPathEntity {
            get {
                return _settingPathEntity = GetEntity(_settingPathEntity, "SettingPathEntity"); 
            }
            set => _settingPathEntity = value;
        }

        /// <summary>
        ///     �˵���Ŀʵ��
        /// </summary>
        public BuildeEntity BuilderEntity {
            get {
                return _builderEntity = GetEntity(_builderEntity, "BuildeEntity"); 
            }
            set => _builderEntity = value;
        }


        /// <summary>
        ///     Ҫ���Ƶ�typekey
        /// </summary>
        public string OldTypekey { get; set; }

        /// <summary>
        ///     ȫ��������Ϣ��ƽ̨·���������·��������·�����ͻ�������ҵ��
        /// </summary>
        public string Mall { get; set; }

        /// <summary>
        /// </summary>
        public string Mcodepath { get; set; }

        /// <summary>
        ///     �ͻ���
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        ///     �ИI��
        /// </summary>
        public bool MIndustry { get; set; }

        /// <summary>
        ///     VS��ǰ·��
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
        ///     ƽ̨·��
        /// </summary>
        public string Mplatform { get; set; }

        /// <summary>
        ///     �����·��
        /// </summary>
        public string MdesignPath { get; set; }

        /// <summary>
        ///     �汾
        /// </summary>
        public string MVersion { get; set; }

        /// <summary>
        ///     ���w����
        /// </summary>
        public FormEntity FormEntity {
            get => _formEntity ?? (_formEntity = new FormEntity());
            set => _formEntity = value;
        }

        /// <summary>
        ///     ����·��
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