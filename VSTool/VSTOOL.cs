// create By 08628 20180411

using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Digiwin.Chun.Common.Controller;
using Digiwin.Chun.Common.Model;
using VSTool.Properties;

namespace VSTool {
    // ReSharper disable once InconsistentNaming
    public partial class VSTOOL : Form {
        public VSTOOL(string[] pToIni) {
            InitializeComponent();
            Version.Text = string.Format(Resources.Version, Version.Text);
            ControlDataBingding();

            #region 自動更新

            Task.Factory.StartNew(() => {
               var existedUpdate= CallUpdate.CheckAndUpdate(Version.Text);
                if (existedUpdate) {
                    CallUpdate.MyCallUpdate();
                }
            });
            //CallUpdate.AutoUpgrade();
            #endregion

            #region 複製最新的佈署dll

            try {
                //var mServerExePath = CallUpdate.GetExeFolder(CallUpdate.GetServerExePath("VSTool"));
                //OldTools.CopynewVsTool(mServerExePath, Toolpars.MvsToolpath);
            }
            catch {
                // ignored
            }

            #endregion

            MyTools.InitToolpars(pToIni);
            AddEventHandler();
        }

        #region 屬性

        private Toolpars Toolpars { get; } = MyTools.Toolpars;

        #endregion


        private void ControlDataBingding() {
            SplitContainer2.Panel2.HorizontalScroll.Visible = false;
            //绑定类，当类或控件值改变时触发更新
            txtToPath.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "txtToPath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            TxtPkGpath.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "TxtPkGpath", true,
                DataSourceUpdateMode.OnPropertyChanged));
            txtNewTypeKey.DataBindings.Add(new Binding("Text", Toolpars.FormEntity, "txtNewTypeKey", true,
                DataSourceUpdateMode.OnPropertyChanged));
            Industry.DataBindings.Add(new Binding("Checked", Toolpars.FormEntity, "Industry", true,
                DataSourceUpdateMode.OnPropertyChanged));
        }

        private void AddEventHandler() {
            ControlTools.VsForm = this;
            Load += ControlTools.VSTOOL_Load;
            ClientSizeChanged += ControlTools.VSTOOL_ClientSizeChanged;
            KeyDown += ControlTools.VSTOOL_KeyDown;
            BtnCreate.Click += ControlTools.BtnCreate_Click;
            btnClear.Click += ControlTools.BtnClear_Click;
            BtnOpenTo.Click += ControlTools.BtnOpenTo_Click;
            btnOpen.Click += ControlTools.BtnOpen_Click;
            OpenCode.Click += ControlTools.OpenCode_Click;
            btnP.Click += ControlTools.BtnP_Click;
            btnG.Click += ControlTools.BtnG_Click;
            TreeView1.AfterCheck += ControlTools.TreeView1_AfterSelect;
            txtNewTypeKey.TextChanged += ControlTools.TxtNewTypeKey_TextChanged;
            Industry.CheckedChanged += ControlTools.Industry_CheckedChanged;
            PkgOpenTo.Click += ControlTools.BtnOpenTo_Click;
            NavTreeView.NodeMouseClick += ControlTools.MyTreeView1_NodeMouseClick;
            ModiCkb.CheckedChanged += ControlTools.CheckBox1_CheckedChanged;
            linkLabel1.LinkClicked += ControlTools.LinkLabel1_LinkClicked;
            ScrollPanel.DragDrop += ControlTools.ScrollPanel_DragDrop;
            ScrollPanel.DragEnter += ControlTools.ScrollPanel_DragEnter;
        }


        private void Panel3_Paint(object sender, PaintEventArgs e) {
            const ButtonBorderStyle borderLineStyle = ButtonBorderStyle.Solid;
            const ButtonBorderStyle borderLineStyleNo = ButtonBorderStyle.None;
            const int borderWidth = 1;
            var borderColor = Color.LightGray;
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                borderColor, borderWidth, borderLineStyle,
                borderColor, borderWidth, borderLineStyleNo,
                borderColor, borderWidth, borderLineStyleNo,
                borderColor, borderWidth, borderLineStyleNo);
        }

      
    }
}