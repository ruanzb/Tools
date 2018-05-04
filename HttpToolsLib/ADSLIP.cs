#region 说明
//---------------------------------------名称:基于bat批处理的ADSL动态拨号帮助类
//---------------------------------------版本:1.1.0.0
//---------------------------------------更新时间:2017/10/18
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools
#endregion

#region 名空间
using DotRas;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
#endregion

namespace HttpToolsLib
{
    /// <summary>
    /// ADSL拨号帮助类 用批处理实现
    /// </summary>
    [Obsolete("过时  改用RAS")]
    public class ADSLIP
    {
        #region 变量
        /// <summary>
        ///生成的临时批处理文件名称
        /// </summary>
        static String _temppath = "temp.bat";
        public static String temppath
        {
            get { return ADSLIP._temppath; }
            set { ADSLIP._temppath = value; }
        }
        /// <summary>
        /// 字符串拼接用
        /// </summary>
        private static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 拨号等待 默认15秒
        /// </summary>
        public static int delay = 15;
        #endregion

        #region 常量
        /// <summary>
        /// 公网ip查询地址
        /// </summary>
        const String IPUrl = "http://2017.ip138.com/ic.asp";
        #endregion

        #region 方法
        /// <summary>
        /// 开始拨号
        /// </summary>
        /// <param name="ADSL_Name">宽带连接名称</param>
        /// <param name="ADSL_UserName">宽带连接用户名</param>
        /// <param name="ADSL_PassWord">宽带连接密码</param>
        public static void ChangeIp(String ADSL_Name = "宽带连接", String ADSL_UserName = "057475534480", String ADSL_PassWord = "578411")
        {
            sb.Clear();
            sb.AppendLine("@echo off");
            sb.AppendLine("set adslmingzi=" + ADSL_Name);
            sb.AppendLine("set adslzhanghao=" + ADSL_UserName);
            sb.AppendLine("set adslmima=" + ADSL_PassWord);
            sb.AppendLine("@Rasdial %adslmingzi% /disconnect");
            sb.AppendLine("ping 127.0.0.1 -n 2");
            sb.AppendLine("Rasdial %adslmingzi% %adslzhanghao% %adslmima%");
            sb.AppendLine("echo 连接中");
            sb.AppendLine("ping 127.0.0.1 -n 2");
            sb.AppendLine("ipconfig");
            // sb.AppendLine("pause");

            using (StreamWriter sw = new StreamWriter(temppath, false, Encoding.Default))
            {
                sw.Write(sb.ToString());
            }
            Process.Start(temppath);
            System.Threading.Thread.Sleep(delay * 1000);
            int i = 0;
            while (!HttpMethod.CheckIp(null))
            {
                Process.Start(temppath);
                System.Threading.Thread.Sleep((++i) * delay * 1000);
            }
            File.Delete(temppath);
        }
        /// <summary>
        /// 获得本地ip
        /// </summary>
        /// <returns>本地ip地址</returns>
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

        /// <summary>
        /// 获得公网ip
        /// </summary>
        /// <returns>公网IP地址</returns>
        public static String GetPublicIP()
        {
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            HttpResult result = new HttpResult();
            Regex reg = new Regex("\\[(.+?)\\]");
            item.URL = IPUrl;
            item.Timeout = 5000;
            item.ReadWriteTimeout = 10000;
            //item.UserAgent = UAPool.GetRandomUA();
            item.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.04";

            String html = helper.GetHtml(item).Html;
            String ip = String.Empty;
            try
            {
                 ip = reg.Match(html).Groups[1].Value;
            }
            catch
            {

            }
            return ip;
        }
        /// <summary>
        /// 代理Ip校验
        /// </summary>
        /// <param name="Ip">待校验代理IP</param>
        /// <param name="CheckUrl">检验地址</param>
        /// <returns></returns>
        public static bool CheckIp(String Ip, String CheckUrl = "http://2017.ip138.com/ic.asp")
        {
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem();
            HttpResult result = new HttpResult();
            item.URL = CheckUrl;
            item.ProxyIp = Ip;
            item.Timeout = 5000;
            item.ReadWriteTimeout = 10000;
            //item.UserAgent = UAPool.GetRandomUA();
            item.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.04";

            String html = helper.GetHtml(item).Html;

            return html.Contains("您的IP是");

        }
        /// <summary>
        /// 判断是否为正确的IP地址，IP范围（0.0.0.0～255.255.255）
        /// </summary>
        /// <param name="ip">需验证的IP地址</param>
        /// <returns>是否为正确的IP地址</returns>
        public bool IsIP(String ip)
        {
            return System.Text.RegularExpressions.Regex.Match(ip, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$").Success;
        }

        /// <summary>
        /// 比较两个IP地址的大小
        /// </summary>
        /// <param name="ipx">要比较的第一个对象</param>
        /// <param name="ipy">要比较的第二个对象</param>
        /// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
        private int CompareIp(string ipx, string ipy)
        {
            string[] ipxs = ipx.Split('.');
            string[] ipys = ipy.Split('.');
            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToInt32(ipxs[i]) > Convert.ToInt32(ipys[i]))
                {
                    return 1;
                }
                else if (Convert.ToInt32(ipxs[i]) < Convert.ToInt32(ipys[i]))
                {
                    return -1;
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }
        #endregion
    }

    /// <summary>
    /// ADSL拨号帮助类 用DotRas实现
    /// </summary>

    public class RASDisplay
    {
        public static string ADSLName { get;  set; }
        public static string ADSLUserName { get;  set; }
        public static string ADSLPassWord { get;  set; }

        /// <summary>
        /// 创建或更新一个PPPOE连接(指定PPPOE名称)
        /// </summary>
        static void CreateOrUpdatePPPOE(string updatePPPOEname)
        {
            RasDialer dialer = new RasDialer();
            RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
            string path = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            allUsersPhoneBook.Open(path);
            // 如果已经该名称的PPPOE已经存在，则更新这个PPPOE服务器地址
            if (allUsersPhoneBook.Entries.Contains(updatePPPOEname))
            {
                allUsersPhoneBook.Entries[updatePPPOEname].PhoneNumber = " ";
                // 不管当前PPPOE是否连接，服务器地址的更新总能成功，如果正在连接，则需要PPPOE重启后才能起作用
                allUsersPhoneBook.Entries[updatePPPOEname].Update();
            }
            // 创建一个新PPPOE
            else
            {
                string adds = string.Empty;
                ReadOnlyCollection<RasDevice> readOnlyCollection = RasDevice.GetDevices();
                RasDevice device = RasDevice.GetDevices().Where(o => o.DeviceType == RasDeviceType.PPPoE).First();
                RasEntry entry = RasEntry.CreateBroadbandEntry(updatePPPOEname, device);    //建立宽带连接Entry
                entry.PhoneNumber = " ";
                allUsersPhoneBook.Entries.Add(entry);
            }
        }

        /// <summary>
        /// 断开 宽带连接
        /// </summary>
        public static void Disconnect()
        {
            ReadOnlyCollection<RasConnection> conList = RasConnection.GetActiveConnections();
            foreach (RasConnection con in conList)
            {
                con.HangUp();
            }
        }

        /// <summary>
        /// 宽带连接，成功返回true,失败返回 false
        /// </summary>
        /// <param name="PPPOEname">宽带连接名称</param>
        /// <param name="username">宽带账号</param>
        /// <param name="password">宽带密码</param>
        /// <returns></returns>
        public static bool Connect(string PPPOEname, string username, string password, ref string msg,int delay = 10000)
        {
            try
            {
                CreateOrUpdatePPPOE(PPPOEname);
                using (RasDialer dialer = new RasDialer())
                {
                    dialer.EntryName = PPPOEname;
                    dialer.AllowUseStoredCredentials = true;
                    dialer.Timeout = 1000;
                    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                    dialer.Credentials = new NetworkCredential(username, password);
                    dialer.Dial();
                    return true;
                }
            }
            catch (RasException re)
            {
                msg = re.ErrorCode + " " + re.Message;
                return false;
            }
            finally
            {
                Thread.Sleep(delay);
            }
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public static bool Connect()
        {
            try
            {
                CreateOrUpdatePPPOE(ADSLName);
                using (RasDialer dialer = new RasDialer())
                {
                    dialer.EntryName = ADSLName;
                    dialer.AllowUseStoredCredentials = true;
                    dialer.Timeout = 1000;
                    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                    dialer.Credentials = new NetworkCredential(ADSLUserName, ADSLPassWord);
                    dialer.Dial();
                    return true;
                }
            }
            catch (RasException re)
            {
                Console.WriteLine(re.Message);
                return false;
            }
        }
    }
}
