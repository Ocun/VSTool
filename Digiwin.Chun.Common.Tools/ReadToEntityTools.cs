// create By 08628 20180411

using System;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Digiwin.Chun.Models;
using Newtonsoft.Json;

namespace Digiwin.Chun.Common.Tools {

    
    /// <summary>
    /// Model辅助类
    /// </summary>
    public static class ReadToEntityTools {
        /// <summary>
        /// 读取文件到类
        /// </summary>
        /// <param name="path"></param>
        /// <param name="modelType">文件类型</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>T</returns>
        public static T ReadToEntity<T>(string path,ModelType modelType) where T : class {
            if (modelType.Equals(ModelType.Json)) {
              return !File.Exists(path) ? null : DeserializeObject<T>(path);
            }
            else if(modelType.Equals(ModelType.Xml))
            {
              return  !File.Exists(path) ? null : DeSerializer<T>(path);
            }
            else if (modelType.Equals(ModelType.Binary)) {
                return !File.Exists(path) ? null : BinaryDeSerializer<T>(path);
            }
            else {
                return null;
            }
        }

        /// <summary>
        ///     将类转化到二进制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] BinarySerialize<T>(T obj) {
          
            try
            {
                  using (var ms = new MemoryStream())
                  {
                      IFormatter iFormatter = new BinaryFormatter();
                      iFormatter.Serialize(ms, obj);
                      var buff = ms.GetBuffer();
                      return buff;
                    }
                     
            }
            catch (Exception ex)
            {
                LogTools.LogError($@"BinarySerialize error！ Detail:{ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     将二进制保存到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool BinarySave<T>(T obj,string fileName) {
          
            try
            {
                using (var flstr = new FileStream(fileName, FileMode.Create))
                {
                    using (var binaryWriter = new BinaryWriter(flstr)) {
                        var buff = BinarySerialize(obj);
                        binaryWriter.Write(buff);
                    }
                }
             
            }
            catch (Exception er)
            {
                LogTools.LogError($@"BinarySave error！ Detail:{er.Message}");
                throw new Exception(er.Message);
            }
            return true;
        }

        /// <summary>
        ///     将二进制读取到类中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T BinaryDeSerializer<T>(string path) where T : class {
            try {
                using (var fs = new FileStream(path, FileMode.Open)) {
                    var bf = new BinaryFormatter();
                    return  bf.Deserialize(fs) as T;
                }
            }
            catch (Exception ex) {
                LogTools.LogError($@"BinaryDeSerializer error！ Detail:{ex.Message}");
                return null;
            }
        }   

        /// <summary>
        ///     将类转化到xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(T obj) {
            using (var mem = new MemoryStream()) {
                    var ser = new XmlSerializer(obj.GetType());
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    ser.Serialize(mem, obj, ns);
                    return Encoding.UTF8.GetString(mem.ToArray());
            }
          
            //using (var sw = new StringWriter()) {
            
            //    var ns = new XmlSerializerNamespaces();
            //    //Add an empty namespace and empty value
            //    ns.Add("", "");
            //    var serializer = new XmlSerializer(obj.GetType());
            //    serializer.Serialize(sw, obj,ns);
            //    return sw.ToString();
            //}
        }

        /// <summary>
        /// 将当前配置保存成其他格式
        /// </summary>
        /// <param name="modelType"></param>
        //public static void ConvertToModelType(ModelType modelType) {
        //    var toolpars = MyTools.Toolpars;
        //    var xmlPath = PathTools.GetSettingPath("BuildeEntity", modelType);
        //    SaveSerialize(toolpars.BuilderEntity, modelType, xmlPath);
        //    xmlPath = PathTools.GetSettingPath("FileMappingEntity", modelType);
        //    SaveSerialize(toolpars.FileMappingEntity, modelType, xmlPath);
        //    xmlPath = PathTools.GetSettingPath("SettingPathEntity", modelType);
        //    SaveSerialize(toolpars.SettingPathEntity, modelType, xmlPath);
        //}

    


        /// <summary>
        ///     将类序列化保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="modelType"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool SaveSerialize<T>(T obj, ModelType modelType, string filePath) where T:class {
            try {
                switch (modelType)
                {
                    case ModelType.Xml:
                        var text = XmlSerialize(obj);
                        File.WriteAllText(filePath, text, Encoding.UTF8);
                        break;
                    case ModelType.Json:
                        var jsonText = SerializeObject(obj);
                        File.WriteAllText(filePath, jsonText, Encoding.UTF8);
                        break;
                    case ModelType.Binary:
                        BinarySave(obj, filePath);
                        break;
                }
            }
            catch (Exception ex) {
                LogTools.LogError($@"SaveSerialize error！ Detail:{ex.Message}");
                return false;
            } 
          
            return true;
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
            catch (Exception ex) {
                LogTools.LogError($@"ReadXml error！ Detail:{ex.Message}");
                return null;
            }
        }

        /// <summary>
        ///     将类转化为json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj) where T : class {
            try {
                //忽略null
                var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

                return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, jsonSetting);
            }
            catch (Exception ex) {
                LogTools.LogError($@"ConvertToJson error！ Detail:{ex.Message}");
                return null;
            }
        } /// <summary>
        ///     将json读取到类中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string path) where T : class {
            try {
                using (var sr = new StreamReader(path, Encoding.UTF8)) {

                    var jsonData = sr.ReadToEnd();
                   return JsonConvert.DeserializeObject<T>(jsonData);
                    
                }
               
            }
            catch (Exception ex) {
                LogTools.LogError($@"ReadJson error！ Detail:{ex.Message}");
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