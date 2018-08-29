using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebApiTest
{
    public partial class 上传文件 : Form
    {
        public 上传文件()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cur2 = "http://localhost:5728/API/OutSide/UploadImageAsync";

            if (textBox1.Text.Trim().Equals(""))
            {
                MessageBox.Show("请先选择存储目录..!");
            }
            else
            {
                FileStream stream = File.Open(Path.Combine(textBox1.Text), FileMode.Open, FileAccess.ReadWrite); // new FileStream(Path.Combine(textBox1.Text), FileMode.Append);

                byte[] bytes = new byte[stream.Length];

                stream.Read(bytes, 0, bytes.Length);


                stream.Seek(0, SeekOrigin.Begin);

                string str = Convert.ToBase64String(bytes);


                var result1 = WebApiHelper.Post(cur2, "{Imagedata:'"+str+"' }","");

                textBox2.Text = result1;
            }
        }
    }
}
