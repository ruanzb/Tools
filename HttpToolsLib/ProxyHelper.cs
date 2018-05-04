#region 说明
//---------------------------------------名称:代理IP帮助类
//---------------------------------------版本:1.1.0.0
//---------------------------------------更新时间:2017/10/19
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools 
#endregion

#region 名空间
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks; 
#endregion

namespace HttpToolsLib
{
    /// <summary>
    /// 代理Ip帮助类 提高代码复用率
    /// </summary>
    public class ProxyHelper
    {
        #region 私有变量
        /// <summary>
        /// 有效Ip栈
        /// </summary>
        private static ConcurrentStack<String> IPCheckStack = new ConcurrentStack<string>();
        /// <summary>
        /// IP栈
        /// </summary>
        private static ConcurrentStack<String> IPS = new ConcurrentStack<string>();
        #endregion

        #region 属性
        /// <summary>
        /// 代理Ip地址
        /// </summary>
        static List<String> _API_Url_List = new List<string>();

        public static List<String> API_Url_List
        {
            get { return _API_Url_List; }
            set { _API_Url_List = value; }
        }
        #endregion

        #region 方法

        #region IP获取相关
        /// <summary>
        /// 验Ip线程函数
        /// </summary>
        /// <param name="index">代理ip地址在APIList中的索引</param>
        /// <param name="checknum">验ip线程数</param>
        public static void IPCheckFunc(String api = "",int index = 1, int checknum = 100,int limit = 1000,int delay = 5000)
        {
            String API_Url = String.IsNullOrEmpty(api)?String.Format(API_Url_List[index], limit):api;
            var ips = HttpMethod.InputApi(API_Url);
            Thread[] tarr = new Thread[checknum];
            Task.Factory.StartNew(() =>
            {
                while (IPCheckStack.Count < 500)
                {
                    if (ips.Count == 0)
                    {
                        ips = HttpMethod.InputApi(API_Url);
                    }
                    Thread.Sleep(delay);
                }
            });


            for (int i = 0; i < checknum; i++)
            {
                tarr[i] = new Thread(new ThreadStart(delegate {
                    String ip = String.Empty;
                    HttpHelper helper = new HttpHelper();
                    HttpItem item = new HttpItem();
                    HttpResult result = new HttpResult();
                    item.URL = "http://2017.ip138.com/ic.asp";
                    item.Timeout = 6000;
                    item.ReadWriteTimeout = 8000;
                    item.UserAgent = "Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.04";
                    String Ips = String.Empty;
                    while (true)
                    {
                        if (IPCheckStack.Count < 500)
                        {
                            if (ips.TryPop(out ip))
                            {
                                item.ProxyIp = ip;
                                try
                                {
                                    if (helper.GetHtml(item).Html.Contains(ip.Substring(0, ip.IndexOf(":"))))
                                    {
                                        IPCheckStack.Push(ip);
                                    }
                                }
                                catch
                                {

                                }
                            }
                            else
                            {
                                Thread.Sleep(1000);
                            }
                        }
                        else
                        {
                            Thread.Sleep(5000);
                        }
                    }
                }));
                tarr[i].IsBackground = true;
                tarr[i].Start();
            }
        }
        /// <summary>
        /// 获取Ip线程函数
        /// </summary>
        /// <param name="index">代理ip地址在APIList中的索引<</param>
        /// <param name="DelayTime">获取间隔</param>
        public static void IPFunc(String api = null, int index = 1, int DelayTime = 5000)
        {
            Task.Factory.StartNew(()=> {
                String API_Url = api ?? API_Url_List[index];
                while (true)
                {
                    if (String.IsNullOrEmpty(API_Url))
                    {
                        return;
                    }
                    String ip = String.Empty;
                    if (IPS.Count == 0)
                    {
                        var ips = HttpMethod.InputApi(API_Url);
                        if (ips == null || ips.Count <= 0)
                        {
                            continue;
                        }
                        foreach (var item in ips)
                        {
                            IPS.Push(item);
                        }
                    }
                    Thread.Sleep(DelayTime);
                }
            });

        }
        #endregion

        #region IP栈堆操作
        /// <summary>
        /// IP出栈
        /// </summary>
        /// <returns></returns>
        public static string PopIp()
        {
            String ip = String.Empty;
            if (IPCheckStack.TryPop(out ip))
            {
                return ip;
            }
            while (!IPS.TryPop(out ip))
            {
                Thread.Sleep(1);
            }
            return ip;
        }

        public static String PopIp_Checked()
        {
            String ip = String.Empty;
            while (!IPCheckStack.TryPop(out ip))
            {
                Thread.Sleep(1);
                // Console.WriteLine(ip);
            }
            return ip;
        }
        /// <summary>
        ///  有效Ip入栈
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool PushIp(String ip)
        {
            if (!String.IsNullOrEmpty(ip))
            {
                IPCheckStack.Push(ip);
                return true;
            }
            return false;
        }
        #endregion

        #endregion
    }
}
