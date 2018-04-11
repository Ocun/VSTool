// create By 08628 20180411
using Common.Implement.Entity;
using Common.Implement.Tools;

namespace Common.Implement {
    public class toolpars {
        private string _oldTypekey;
        private string _mALL;
        private string _mcodepath; //����
        private string _gToIni;
        private bool _mIndustry;
        private string _mVSToolpath;
        private bool _mDistince = false;
        private string _mpath;
        private string _mInpath;
        private string _mplatform; //ƽ̨
        private string _mdesignPath;
        private string _mVersion;
        private string _customerName;
        private FormEntity _formEntity;
        private PathEntity _pathEntity;
        private SettingPathEntity _settingPathEntity;
        private BuildeEntity _builderEntity;
        private MappingEntity _FileMappingEntity;

        public MappingEntity FileMappingEntity {
            get {
                if (_FileMappingEntity == null) {
                    string Path = string.Format(@"{0}Config\FileMapping.xml", MVSToolpath);
                    if (ValidateTool.checkFile(Path)) {
                        _FileMappingEntity = ReadToEntityTools.ReadToEntity<MappingEntity>(Path);
                    }
                }
                return _FileMappingEntity;
            }
            set { _FileMappingEntity = value; }
        }

        public SettingPathEntity SettingPathEntity {
            get {
                if (_settingPathEntity == null) {
                    string settingPath = string.Format(@"{0}Config\SettingPathEntity.xml", MVSToolpath);
                    if (ValidateTool.checkFile(settingPath)) {
                        _settingPathEntity = ReadToEntityTools.ReadToEntity<SettingPathEntity>(settingPath);
                    }
                }
                return _settingPathEntity;
            }
            set { _settingPathEntity = value; }
        }

        public BuildeEntity BuilderEntity {
            get {
                if (_builderEntity == null) {
                    string Path = string.Format(@"{0}Config\BuildeEntity.xml", MVSToolpath);
                    if (ValidateTool.checkFile(Path)) {
                        _builderEntity = ReadToEntityTools.ReadToEntity<BuildeEntity>(Path);
                    }
                }
                return _builderEntity;
            }
            set { _builderEntity = value; }
        }



        public toolpars() {
            _mVSToolpath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }


        /// <summary>
        /// Ҫ���Ƶ�typekey
        /// </summary>
        public string OldTypekey {
            get { return _oldTypekey; }
            set { _oldTypekey = value; }
        }

        /// <summary>
        /// ȫ��������Ϣ��ƽ̨·���������·��������·�����ͻ�������ҵ��
        /// </summary>
        public string MALL {
            get { return _mALL; }
            set { _mALL = value; }
        }

        public string Mcodepath {
            get { return _mcodepath; }
            set { _mcodepath = value; }
        }

        public string CustomerName {
            get { return _customerName; }
            set { _customerName = value; }
        }

        /// <summary>
        /// ����Ŀ�
        /// </summary>
        public string GToIni {
            get { return _gToIni; }
            set { _gToIni = value; }
        }

        /// <summary>
        /// �ИI��
        /// </summary>
        public bool MIndustry {
            get { return _mIndustry; }
            set { _mIndustry = value; }
        }

        /// <summary>
        /// VS��ǰ·��
        /// </summary>
        public string MVSToolpath {
            get { return _mVSToolpath; }
            set { _mVSToolpath = value; }
        }

        public bool MDistince {
            get { return _mDistince; }
            set { _mDistince = value; }
        }

        public string Mpath {
            get { return _mpath; }
            set { _mpath = value; }
        }

        public string MInpath {
            get { return _mInpath; }
            set { _mInpath = value; }
        }

        public string Mplatform {
            get { return _mplatform; }
            set { _mplatform = value; }
        }

        public string MdesignPath {
            get { return _mdesignPath; }
            set { _mdesignPath = value; }
        }

        /// <summary>
        /// �汾
        /// </summary>
        public string MVersion {
            get { return _mVersion; }
            set { _mVersion = value; }
        }

        /// <summary>
        /// ���w����
        /// </summary>
        public FormEntity formEntity {
            get {
                if (_formEntity == null) _formEntity = new FormEntity();
                return _formEntity;
            }
            set { _formEntity = value; }
        }

        /// <summary>
        /// ����·��
        /// </summary>
        public PathEntity PathEntity {
            get {
                _pathEntity = PathTools.getPathEntity(this);
                return _pathEntity;
            }
            set { _pathEntity = value; }
        }
    }
}