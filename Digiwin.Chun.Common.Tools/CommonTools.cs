using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Digiwin.Chun.Models;

namespace Digiwin.Chun.Common.Tools
{
    public static class CommonTools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void OpenExe(string path)
        {
            try
            {
                var p = new Process { StartInfo = { FileName = path } };

                p.Start();
            }
            catch (Exception ex)
            {
                LogTools.LogError($"Open{path} Error! Detail:{ex.Message}");
            }
        }


    }
}
