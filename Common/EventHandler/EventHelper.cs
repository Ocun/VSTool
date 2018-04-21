// create By 08628 20180411

using System;
using Digiwin.Chun.Common.Views;

namespace Digiwin.Chun.Common.EventHandler {
    /// <summary>
    /// 事件帮助类
    /// </summary>
    public class EventHelper {
        /// <summary>
        ///     防止重获焦点时，选择项瞬间跳离的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MyTreeView_Leave(object sender, EventArgs e) {
            var mytreeView = sender as MyTreeView;
            if (mytreeView != null)
                mytreeView.SelectedNode = null;
        }
    }
}