using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Implement.Tools
{
   public class ValidateTool {
       public static bool checkFile(string filePath) {
           if (!File.Exists(filePath))
               throw new Exception(string.Format("文件{0}不存在",filePath));
           return true;
       }

       public static bool isNullorEmpty(string str) {
           return str == null || str.Trim().Equals(string.Empty);
       }

   }
}
