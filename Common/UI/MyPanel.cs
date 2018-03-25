using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Implement.UI
{
    public class MyPanel : System.Windows.Forms.Panel
    {
        /// <summary>
        /// 解决 当pannel里的控件重获焦点时，自动滚动到最上面
        /// </summary>
        /// <param name="activeControl"></param>
        /// <returns></returns>
        protected override Point ScrollToControl(Control activeControl)
        {
            // return base.ScrollToControl(activeControl);
            return this.AutoScrollPosition;
        }
    }
}
