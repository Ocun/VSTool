using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Digiwin.Chun.Common.Controller;
using Digiwin.Chun.Common.Model;
using Digiwin.Chun.Common.Properties;

namespace Digiwin.Chun.Common.Views
{
    /// <summary>
    /// 插单示例窗口
    /// </summary>
    public sealed partial class InsertForm : Form
    {
        /// <summary>
        /// 主窗体参数
        /// </summary>
        public Toolpars Toolpars { get; set; }

        /// <summary>
        /// 窗体构造
        /// </summary>
        /// <param name="toolpars"></param>
        public InsertForm(Toolpars toolpars)
        {
            InitializeComponent();
            Toolpars = toolpars;
        }
        /// <summary>
        /// 构造器
        /// </summary>
        public InsertForm()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var fulltypeKey = textBox1.Text.Trim();
            if (fulltypeKey.Equals(string.Empty)) 
                return;
            var typeKey = fulltypeKey;
            var isSplit = false;
            if (fulltypeKey.Contains(@".")) {
                var splitRes = fulltypeKey.Split('.');
                isSplit = true;
                typeKey = splitRes[0];
            }
            var path = $@"{Toolpars.Mplatform}\Server\Application\BusinessObjects\{typeKey}";
            if (!Directory.Exists(path)) {
                MessageBox.Show($@"{path}{Resources.NotFind}");
                return;
            }
            var targetDir = new DirectoryInfo(path);
            var files = targetDir.GetFiles("MetadataContainer.dcxml", SearchOption.AllDirectories);
            if (files.Any())
            {
                var metadataContainer = ReadToEntityTools.ReadToEntity<MetadataContainer>(files[0].FullName, ModelType.Xml);
                if (metadataContainer == null) {
                    MessageBox.Show(Resources.ReadEntityError,Resources.ErrorMsg);
                    return;
                }
                   
                if (isSplit) {
                    var splitRes = fulltypeKey.Split('.');
                    var lastTypeKey = splitRes[splitRes.Length - 1];
                    var isCurrect = true;
                    splitRes.ToList().ForEach(currentKey =>
                    {
                        if (!metadataContainer.DataEntityTypes.Any(entityType => entityType.Name.Equals(currentKey))) {
                            isCurrect = false;
                        }
                    });
                    if (isCurrect) {
                        var propies = MyTools.GetPropNameByEntity(metadataContainer, lastTypeKey);
                       richTextBox1.Text = CreateInsert(propies, fulltypeKey);
                    }
                    else {
                        MessageBox.Show($@"{fulltypeKey}{Resources.NotFind}{lastTypeKey}");
                    }
                   
                }
                else {
                    var propies = MyTools.GetPropNameByEntity(metadataContainer, fulltypeKey);
                    richTextBox1.Text = CreateInsert(propies, fulltypeKey);
                }

            }
            else
            {
                MessageBox.Show($@"平台TypeKey{typeKey}{Resources.NotFind}");
            }
        }

        private string CreateInsert(List<string> propies,string fullTypeKey) {
            var sb = new StringBuilder();
            if (propies == null) return sb.ToString();
            sb.AppendLine("--查询示例,亦可用于批量插入--");
            sb.AppendLine("List<QueryProperty> properties = new List<QueryProperty>();");
            sb.AppendLine(" properties.AddRange(new QueryProperty[]{");
            propies.ForEach(prop => {
                var propName = $@"OOQL.CreateProperty(""{prop}"",""{prop}"")";
                sb.AppendLine(prop.Equals(propies[propies.Count - 1])
                    ? propName
                    : propName + @", ");
            });
            sb.AppendLine(" });");
            sb.AppendLine("--插入示例,单笔--");
            sb.AppendLine(@"Dictionary<string, QueryProperty> columns = new Dictionary<string, QueryProperty>();");
            propies.ForEach(prop => {
                var propName = $@"columns.Add(""{prop}"",OOQL.CreateConstants())";
                sb.AppendLine(prop.Equals(propies[propies.Count - 1])
                    ? propName : propName + @",");
            });
            sb.AppendLine($@"node = OOQL.Insert(""{fullTypeKey}"", columns.Keys.ToArray()).Values(columns.Values.ToArray())");

            return sb.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
