// create By 08628 20180411

using System;
using System.IO;

namespace Common.Implement.Tools {
    public class ValidateTool {
        public static bool CheckFile(string filePath) {
            if (!File.Exists(filePath))
                throw new Exception($"文件{filePath}不存在");
            return true;
        }

        public static bool IsNullorEmpty(string str) {
            return str == null || str.Trim().Equals(string.Empty);
        }
    }
}