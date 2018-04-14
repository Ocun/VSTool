// create By 08628 20180411
using Common.Implement.Entity;
using Common.Implement.Tools;

namespace Common.Implement.Entity {
    public class Toolpars {
        private string _oldTypekey;
        private string _mAll;
        private string _mcodepath; //代码
        private string _gToIni;
        private bool _mIndustry;
        private string _mVsToolpath;
        private bool _mDistince = false;
        private string _mpath;
        private string _mInpath;
        private string _mplatform; //平台
        private string _mdesignPath;
        private string _mVersion;
        private string _customerName;
        private FormEntity _formEntity;
        private PathEntity _pathEntity;
        private SettingPathEntity _settingPathEntity;
        private BuildeEntity _builderEntity;
        private MappingEntity _fileMappingEntity;

        public MappingEntity FileMappingEntity {
            get {
                if (_fileMappingEntity != null) return _fileMappingEntity;
                var path = $@"{MVSToolpath}Config\FileMapping.xml";
                if (ValidateTool.CheckFile(path)) {
                    _fileMappingEntity = ReadToEntityTools.ReadToEntity<MappingEntity>(path);
                }
                return _fileMappingEntity;
            }
            set => _fileMappingEntity = value;
        }

        public SettingPathEntity SettingPathEntity {
            get {
                if (_settingPathEntity != null) return _settingPathEntity;
                var settingPath = $@"{MVSToolpath}Config\SettingPathEntity.xml";
                if (ValidateTool.CheckFile(settingPath)) {
                    _settingPathEntity = ReadToEntityTools.ReadToEntity<SettingPathEntity>(settingPath);
                }
                return _settingPathEntity;
            }
            set => _settingPathEntity = value;
        }

        public BuildeEntity BuilderEntity {
            get {
                if (_builderEntity != null) return _builderEntity;
                var path = $@"{MVSToolpath}Config\BuildeEntity.xml";
                if (ValidateTool.CheckFile(path)) {
                    _builderEntity = ReadToEntityTools.ReadToEntity<BuildeEntity>(path);
                }
                return _builderEntity;
            }
            set => _builderEntity = value;
        }



        public Toolpars() {
            _mVsToolpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }


        /// <summary>
        /// 要复制的typekey
        /// </summary>
        public string OldTypekey {
            get => _oldTypekey;
            set => _oldTypekey = value;
        }

        /// <summary>
        /// 全部参数信息，平台路径，设计器路径，个案路径，客户名，行业包
        /// </summary>
        public string MALL {
            get => _mAll;
            set => _mAll = value;
        }

        public string Mcodepath {
            get => _mcodepath;
            set => _mcodepath = value;
        }

        public string CustomerName {
            get => _customerName;
            set => _customerName = value;
        }

        /// <summary>
        /// 個案目錄
        /// </summary>
        public string GToIni {
            get => _gToIni;
            set => _gToIni = value;
        }

        /// <summary>
        /// 行業包
        /// </summary>
        public bool MIndustry {
            get => _mIndustry;
            set => _mIndustry = value;
        }

        /// <summary>
        /// VS当前路径
        /// </summary>
        public string MVSToolpath {
            get => _mVsToolpath;
            set => _mVsToolpath = value;
        }

        public bool MDistince {
            get => _mDistince;
            set => _mDistince = value;
        }

        public string Mpath {
            get => _mpath;
            set => _mpath = value;
        }

        public string MInpath {
            get => _mInpath;
            set => _mInpath = value;
        }

        public string Mplatform {
            get => _mplatform;
            set => _mplatform = value;
        }

        public string MdesignPath {
            get => _mdesignPath;
            set => _mdesignPath = value;
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string MVersion {
            get => _mVersion;
            set => _mVersion = value;
        }

        /// <summary>
        /// 窗體參數
        /// </summary>
        public FormEntity FormEntity {
            get => _formEntity ?? (_formEntity = new FormEntity());
            set => _formEntity = value;
        }

        /// <summary>
        /// 各個路徑
        /// </summary>
        public PathEntity PathEntity {
            get {
                _pathEntity = PathTools.GetPathEntity(this);
                return _pathEntity;
            }
            set => _pathEntity = value;
        }
    }
}