using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebApiTest
{
    public partial class Form1 : Form
    {
        private string curl = "http://192.168.1.157:8001/API/";
        private string cur2 = "http://localhost:13440/API/QRCode/";
        public Form1()
        {
            InitializeComponent();

            textBox5.Text = cur2;
            textBox3.Text = "Register";
            textBox4.Text = "{UserName:'用户1',Position:'行业1',Phone:'电话1',Explain:'说明1',GuIdNumber:'GuIdNumber1'}";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string url1 = textBox5.Text + textBox3.Text;// @"Leave/LeaveInfo";

            string data = textBox4.Text;

            var result1 = WebApiHelper.Post(url1, data, "");

            if (textBox3.Text.Trim() == "UserNumberGetCode")
            {
                var Td = DeserializeJsonToObject<model>(result1);

                byte[] byteArray = Td.Data;

                Image photo = null;
                using (MemoryStream ms = new MemoryStream(byteArray))
                {
                    ms.Write(byteArray, 0, byteArray.Length);
                    photo = Image.FromStream(ms, true);
                }

                pictureBox1.Image = photo;



            }

            textBox2.Text = result1;

        }



        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }
    }

    public class model
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public byte[] Data { get; set; }

    }

}
