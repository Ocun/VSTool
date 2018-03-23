using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VSTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length <= 0)
            {
                //MessageBox.Show("请输入启动参数");
                //Application.Exit();
                Application.Run(new VSTOOL(null));
            }
            if (args.Length == 1)
            {
                Application.Run(new VSTOOL(args));
               
            }
        }
}
}
