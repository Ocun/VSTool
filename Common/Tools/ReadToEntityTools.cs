using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common.Implement.Tools
{
    public class ReadToEntityTools
    {

        public static T ReadToEntity<T>(string path) where T : class {
            if (!File.Exists(path)) return null;
           return DESerializer<T>(path);
        }

        /// <summary>
        /// 将类转化到xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T obj)
        {
            using (StringWriter sw = new StringWriter())
            {
                Type t = obj.GetType();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                sw.Close();
                return sw.ToString();

            }
        }
        /// <summary>
        /// 将xml读取到类中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static T DESerializer<T>(string path) where T : class
        {
            try
            {
                using (XmlTextReader xr = new XmlTextReader(path)) {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(xr) as T;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /**//// <summary>
        /// 功能:读取XML到DataSet中
        /// </summary>
        /// <param name="XmlPath">xml路径</param>
        /// <returns>DataSet</returns>
        public static DataSet GetXml(string XmlPath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(@XmlPath);
            return ds;
        }
    }
}
