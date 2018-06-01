// create By 08628 20180411

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Digiwin.Chun.Models;

namespace Digiwin.Chun.Common.Tools {
   /// <summary>
   /// 路径辅助类
   /// </summary>
   public static class PathTools {
      

       /// <summary>
       /// 检查是否为True
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static bool IsTrue(string str) {
           return str != null && str.Trim().ToUpper().Equals("TRUE") ;
       }
       /// <summary>
       /// 检查是否为False
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static bool IsFasle(string str) {
           return str != null && str.Trim().ToUpper().Equals("FALSE") ;
       }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string CombineStr(IEnumerable<string> strs) {
            var sb = new StringBuilder();
            strs.ToList().ForEach(str => sb.Append((str ?? string.Empty).Trim()));
            return sb.ToString();
        }

       /// <summary>
       /// 连接路径
       /// </summary>
       /// <param name="strs"></param>
       /// <returns></returns>
       public static string PathCombine(params string[] strs) {
            var path = string.Empty;
            strs.ToList().ForEach(str => {
                    var pathArg = (str ?? string.Empty).Trim();
                    if (path.Equals(string.Empty)) {
                        path = pathArg;
                    }
                    else if (path.EndsWith(@"\")) {
                        path = $@"{path}{pathArg}";
                    }
                    else {
                        path = $@"{path}\{pathArg}";
                    }
                   
                }
            );
            return path;
        } 

       // /// <summary>
       ///// 连接路径，指定连接字符串
       ///// </summary>
       ///// <param name="combineStr">连接字符串</param>
       ///// <param name="strs"></param>
       ///// <returns></returns>
       //public static string PathCombine(string combineStr, params string[] strs) {
       //     var path = string.Empty;
       //     strs.ToList().ForEach(str => {
       //             var pathArg = (str ?? string.Empty).Trim();
       //             if (path.Equals(string.Empty)) {
       //                 path = pathArg;
       //             }
       //             else if (path.EndsWith(@combineStr)) {
       //                 path = $@"{path}{pathArg}";
       //             }
       //             else {
       //                 path = $@"{path}{combineStr}{pathArg}";
       //             }
                   
       //         }
       //     );
       //     return path;
       // }

        /// <summary>
        /// 检查空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object value) {
            var f = value == null;
            if(f) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            if (value is string) {
                f = string.Equals(((string) value).Trim(),string.Empty, StringComparison.Ordinal);
            }
            if (value is Array) {
               f = (value as Array).Length==0;

            }
            return f;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool CheckFile(string filePath)
        {
            if (!File.Exists(filePath))
                LogTools.LogError($"未找到文件{filePath}");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public static string GetSettingPath(string fileName, ModelType modelType)
        {
            var path = string.Empty;
            var mvsToolpath = AppDomain.CurrentDomain.BaseDirectory;
            switch (modelType)
            {
                case ModelType.Xml:
                    path = fileName + ".xml";
                    break;
                case ModelType.Json:
                    path = fileName + ".json";
                    break;
                case ModelType.Binary:
                    path = fileName + ".data";
                    break;
            }
            path = PathCombine(mvsToolpath, "Config", path);
            return path;
        }
    }
}