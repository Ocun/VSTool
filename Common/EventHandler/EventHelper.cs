// create By 08628 20180411
using System;
using System.Windows.Forms;
using Common.Implement.Entity;
using Common.Implement.Tools;
using Common.Implement.UI;

namespace Common.Implement.EventHandler
{
    public class EventHelper
    {
        /// <summary>
        /// 防止重获焦点时，选择项瞬间跳离的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void myTreeView_Leave(object sender, EventArgs e) {
            var mytreeView = (sender as MyTreeView);
            if (mytreeView != null)
                mytreeView.SelectedNode = null;
        }
     

    }
}
