using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class WebApiHelper
    {
        /// <summary>
        /// Post请求
        /// </summary>
        public static T Post<T>(string url, string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //加入头信息
            //request.Headers.Add("Token", Token.ToString()); 

            //写数据
            request.Method = "POST";
            //request.ContentLength = bytes.Length;
            //request.ContentType = "application/json";

            request.ContentType = "application/x-www-form-urlencoded";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);

            //读数据
            request.Timeout = 300000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            string strResult = streamReader.ReadToEnd();

            //关闭流
            reqstream.Close();
            streamReader.Close();
            streamReceive.Close();
            request.Abort();
            response.Close();

            return JsonConvert.DeserializeObject<T>(strResult);
        }


        /// <summary>
        /// Post请求
        /// </summary>
        public static string Post(string url, string data)
        {
            var token = "";
            try
            {

                byte[] bytes = Encoding.UTF8.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                //加入头信息
                //request.Headers.Add("Token", Token.ToString()); 

                //写数据
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "application/json";

                //request.ContentType = "application/x-www-form-urlencoded";
                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);




                //读数据
                request.Timeout = 300000;
                request.Headers.Set("Pragma", "no-cache");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                token = response.GetResponseHeader("token");
                Stream streamReceive = response.GetResponseStream();

                StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);

                string strResult = streamReader.ReadToEnd();


                //关闭流
                reqstream.Close();
                streamReader.Close();
                streamReceive.Close();
                request.Abort();
                response.Close();
                return token;
            }
            catch (Exception e)
            {
                return token;
            }



        }


        public static string Post(string url, string data, string token)
        {
            var strResult = "";
            try
            {


                byte[] bytes = Encoding.UTF8.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                //加入头信息
                //request.Headers.Add("Token", Token.ToString()); 

                //写数据
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "application/json";
                //  request.ContentType = "application/x-www-form-urlencoded";
                if (token != "")
                {
                    request.Headers.Add("Token", token);
                }

                Stream reqstream = request.GetRequestStream();
                reqstream.Write(bytes, 0, bytes.Length);




                //读数据
                request.Timeout = 300000;
                request.Headers.Set("Pragma", "no-cache");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                token = response.GetResponseHeader("token");
                Stream streamReceive = response.GetResponseStream();

                StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);

                strResult = streamReader.ReadToEnd();


                //关闭流
                reqstream.Close();
                streamReader.Close();
                streamReceive.Close();
                request.Abort();
                response.Close();

                return strResult;
            }
            catch (Exception e)
            {
                return strResult;
            }
        }


        /// <summary>
        /// Get请求
        /// </summary>
        public static T Get<T>(string webApi, string queryStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webApi + "?" + queryStr);

            //加入头信息
            //request.Headers.Add("Token", Token.ToString()); 

            request.Method = "GET";
            request.ContentType = "application/json";
            request.Timeout = 90000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            string strResult = streamReader.ReadToEnd();

            streamReader.Close();
            streamReceive.Close();
            request.Abort();
            response.Close();

            return JsonConvert.DeserializeObject<T>(strResult);
        }


        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }


        /// <summary>  
        /// 获取随机数
        /// </summary>  
        /// <returns></returns>  
        private static string GetRandom()
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int i = rd.Next(0, int.MaxValue);
            return i.ToString();
        }
    }
}
