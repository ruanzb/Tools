using HttpToolsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpToolsLib
{
    /// <summary>
    /// ADSL 拨号  使用bat实现
    /// </summary>
    public class ADSLIP
    {
        #region 属性和成员函数
        /// <summary>
        /// 宽带连接名
        /// </summary>
        public static String ADSL_Name = String.Empty;
        /// <summary>
        /// 宽带连接用户名
        /// </summary>
        public static String ADSL_UserName = String.Empty;
        /// <summary>
        /// 宽带连接密码
        /// </summary>
        public static String ADSL_PassWord = String.Empty;
        /// <summary>
        /// 生成的临时bat路径
        /// </summary>
        public static String temppath = "temp.bat";
        /// <summary>
        /// 用于拼接bat内容的StringBulider
        /// </summary>
        private static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 延迟 单位为秒
        /// </summary>
        public static int delay = 10;
        #endregion

        #region 静态方法
        /// <summary>
        /// 切换Ip 
        /// </summary>
        /// <param name="ADSL_Name">宽带连接名</param>
        /// <param name="ADSL_UserName">宽带连接用户名</param>
        /// <param name="ADSL_PassWord">宽带连接密码</param>
        public static void ChangeIp(String ADSL_Name = "宽带连接", String ADSL_UserName = "057476269528", String ADSL_PassWord = "147262")
        {
            #region bat内容构造
            //先清空
            sb.Clear();
            sb.AppendLine("@echo off");
            //设置宽带连接相关信息
            sb.AppendLine("set adslmingzi=" + ADSL_Name);
            sb.AppendLine("set adslzhanghao=" + ADSL_UserName);
            sb.AppendLine("set adslmima=" + ADSL_PassWord);
            //断开宽带连接
            sb.AppendLine("@Rasdial %adslmingzi% /disconnect");
            sb.AppendLine("ping 127.0.0.1 -n 2");
            //重连
            sb.AppendLine("Rasdial %adslmingzi% %adslzhanghao% %adslmima%");
            sb.AppendLine("echo 连接中");
            sb.AppendLine("ping 127.0.0.1 -n 2");
            sb.AppendLine("ipconfig");
            // sb.AppendLine("pause");

            //写入bat代码到文件
            using (StreamWriter sw = new StreamWriter(temppath, false, Encoding.Default))
            {
                sw.Write(sb.ToString());
            }
            #endregion

            #region ADSL拨号
            //运行bat文件
            Process.Start(temppath);
            //延迟
            System.Threading.Thread.Sleep(delay * 1000);
            //检查网络是否正常
            while (!HttpMethod.CheckIp(null))
            {
                //网络不正常时延迟重播
                Process.Start(temppath);
                System.Threading.Thread.Sleep(2 * delay * 1000);
            }
            //拨号完毕 删除临时bat文件
            File.Delete(temppath);
            #endregion
        }

        /// <summary>
        /// 获得本地ip
        /// </summary>
        /// <returns></returns>
        public static String GetIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            //遍历
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
        #endregion

    }
}
