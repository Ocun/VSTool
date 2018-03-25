using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Implement.UI;

namespace Common.Implement.EventHandler
{
   public    class EventHelper
    {
       
        /// <summary>
        /// 防止重获焦点时，选择项瞬间跳离的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void myTreeView_Leave(object sender, EventArgs e)
        {
            MyTreeView mytreeView = (sender as MyTreeView);
            mytreeView.SelectedNode = null;
        }
     
        
    }
}
