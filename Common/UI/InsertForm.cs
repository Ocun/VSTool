using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Properties;
using Common.Implement.Tools;

namespace Common.Implement.UI
{
    public sealed partial class InsertForm : Form
    {
        public Toolpars Toolpars { get; set; }
        public InsertForm(Toolpars toolpars)
        {
            InitializeComponent();
            this.Toolpars = toolpars;
        }

        public InsertForm()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
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
                MessageBox.Show($@"{path}不存在");
                return;
            }
            var targetDir = new DirectoryInfo(path);
            var files = targetDir.GetFiles("MetadataContainer.dcxml", SearchOption.AllDirectories);
            if (files.Any())
            {
                var metadataContainer = ReadToEntityTools.ReadToEntity<MetadataContainer>(files[0].FullName);
                if (metadataContainer == null) {
                    MessageBox.Show(Resource.ReadEntityError,Resource.ErrorMsg);
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
                        };
                    });
                    if (isCurrect) {
                        var propies = MyTool.GetPropNameByEntity(metadataContainer, lastTypeKey);
                       richTextBox1.Text = CreateInsert(propies, fulltypeKey);
                    }
                    else {
                        MessageBox.Show($@"{fulltypeKey}不存在{lastTypeKey}");
                    }
                   
                }
                else {
                    var propies = MyTool.GetPropNameByEntity(metadataContainer, fulltypeKey);
                    richTextBox1.Text = CreateInsert(propies, fulltypeKey);
                }

            }
            else
            {
                MessageBox.Show($@"平台TypeKey{typeKey}不存在");
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
                sb.AppendLine(prop.Equals(propies[propies.Count() - 1])
                    ? propName
                    : propName + @", ");
            });
            sb.AppendLine(" });");
            sb.AppendLine("--插入示例,单笔--");
            sb.AppendLine(@"Dictionary<string, QueryProperty> columns = new Dictionary<string, QueryProperty>();");
            propies.ForEach(prop => {
                var propName = $@"columns.Add(""{prop}"",OOQL.CreateConstants())";
                sb.AppendLine(prop.Equals(propies[propies.Count() - 1])
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
