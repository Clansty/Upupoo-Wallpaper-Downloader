using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Upupoo_Wallpaper_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string link = textBox1.Text;
            // http://wallpaper.upupoo.com/store/paperDetail-1800068298.htm
            link = link.Between("paperDetail-", ".htm");
            string url = $"http://source.upupoo.com/theme/{link}/theme.upup";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader reader = new StreamReader(datastream, Encoding.UTF8);
            string r = reader.ReadToEnd();
            reader.Close();
            datastream.Close();
            response.Close();
            // {"ver":"'1'","src":"0001.哔哩哔哩-【自制展示】pop子和pipi美的日常动态桌面展示-木马镜像[超清版].mp4","author":"1001157437","themeno":1800068298,"name":"pop子和pipi美的日常动态桌","themeType":1,"tag":"12","url":"http://source.upupoo.com/theme/1800068298/theme.upup"}
            r = r.Between("\"src\":\"", "\"");
            textBox2.Text = $"http://source.upupoo.com/theme/{link}/{r}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(textBox2.Text);
        }
    }


    public static class StringHelper
    {
        /// <summary>
        /// 取文本左边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>左边内容</returns>
        public static string GetLeft(this string str, string s)
        {
            string temp = str.Substring(0, str.IndexOf(s));
            return temp;
        }


        /// <summary>
        /// 取文本右边内容
        /// </summary>
        /// <param name="str">文本</param>
        /// <param name="s">标识符</param>
        /// <returns>右边内容</returns>
        public static string GetRight(this string str, string s)
        {
            string temp = str.Substring(str.IndexOf(s) + s.Length);
            return temp;
        }

        /// <summary>
        /// 取文本中间内容
        /// </summary>
        /// <param name="str">原文本</param>
        /// <param name="leftstr">左边文本</param>
        /// <param name="rightstr">右边文本</param>
        /// <returns>返回中间文本内容</returns>
        public static string Between(this string str, string leftstr, string rightstr)
        {
            int i = str.IndexOf(leftstr) + leftstr.Length;
            string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);
            return temp;
        }
    }
}
