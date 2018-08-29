using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Source.Admin.Web.Common
{
    public class JsonBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(controllerContext.HttpContext.Request.InputStream);
            string json = reader.ReadToEnd();

            if (string.IsNullOrEmpty(json))
                return json;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            object jsonData = serializer.DeserializeObject(json);
            return serializer.Deserialize<T>(json);
        }

        /// <summary>  
        /// 将Dictionary序列化为json数据  
        /// </summary>  
        /// <param name="jsonData">json数据</param>  
        /// <returns></returns>  
        public static string DictionaryToJson(Dictionary<string, object> dic)
        {
            //实例化JavaScriptSerializer类的新实例  
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象  
                return jss.Serialize(dic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static string ObjectToJson(List<T> dic)
        {
            //实例化JavaScriptSerializer类的新实例  
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象  
                return jss.Serialize(dic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void GetObjectData(SerializationInfo info, StreamingContext context, Dictionary<string, object> dict)
        {
            foreach (string key in dict.Keys)
            {
                info.AddValue(key, dict[key], dict[key] == null ? typeof(object) : dict[key].GetType());
            }
        }
        public static T JsonToObject(string jsonText)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {

                return jss.Deserialize<T>(jsonText);

                //Dictionary<string, object> dataSet = jss.Deserialize<Dictionary<string, object>>(jsonData);

                ////使用KeyValuePair遍历数据
                //foreach (KeyValuePair<string, object> item in dataSet)
                //{
                //    if (item.Key.ToString() == "header")//获取header数据
                //    {
                //        var subItem = (Dictionary<string, object>)item.Value;
                //        foreach (var str in subItem)
                //        {
                //            //textBox1.AppendText(str.Key + ":" + str.Value + "\r\n");//显示到界面
                //        }
                //        break;
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

    }
}