// create By 08628 20180411

using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Implement.Tools {
    public class ReadToEntityTools {
        public static T ReadToEntity<T>(string path) where T : class {
            return !File.Exists(path) ? null : DeSerializer<T>(path);
        }

        /// <summary>
        ///     将类转化到xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T obj) {
            using (var sw = new StringWriter()) {
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                return sw.ToString();
            }
        }

        /// <summary>
        ///     将xml读取到类中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeSerializer<T>(string path) where T : class {
            try {
                using (var xr = new XmlTextReader(path)) {
                    var serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(xr) as T;
                }
            }
            catch (Exception) {
                return null;
            }
        }

        /**/
        /// <summary>
        ///     功能:读取XML到DataSet中
        /// </summary>
        /// <param name="xmlPath">xml路径</param>
        /// <returns>DataSet</returns>
        public static DataSet GetXml(string xmlPath) {
            var ds = new DataSet();
            ds.ReadXml(xmlPath);
            return ds;
        }
    }
}