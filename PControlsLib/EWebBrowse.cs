
//---------------------------------------名称:重写webbrowser控件
//---------------------------------------依赖DLL:Interop.SHDocVw.dll
//---------------------------------------说明:屏蔽webbrowserjs报错弹框  判断页面加载完毕 部分代码来源于网上 侵删
//---------------------------------------版本:1.0.0.0
//---------------------------------------更新时间:2017/10/19
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools

#region 名空间
using System;
using System.Windows.Forms; 
#endregion

namespace PControlsLib
{
    /// <summary>
    /// WebBrowser 重绘 
    /// </summary>
    public class EWebBrowser : System.Windows.Forms.WebBrowser
    {
        #region 变量声明

        SHDocVw.IWebBrowser2 Iwb2;
        
        #endregion

        #region 函数重写
        protected override void AttachInterfaces(object nativeActiveXObject)
        {
            Iwb2 = (SHDocVw.IWebBrowser2)nativeActiveXObject;
            //忽略js错误
            Iwb2.Silent = true;
            base.AttachInterfaces(nativeActiveXObject);
        }

        protected override void DetachInterfaces()
        {
            Iwb2 = null;
            base.DetachInterfaces();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 延迟
        /// </summary>
        /// <param name="Millisecond"></param>
        private static void Delay(int Millisecond) //延迟系统时间，但系统又能同时能执行其它任务；  
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(Millisecond) > DateTime.Now)
            {
                Application.DoEvents();//转让控制权              
            }
            return;
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 等待页面加载完毕
        /// </summary>
        /// <param name="webBrowser1"></param>
        /// <returns></returns>
        public static bool WaitWebPageLoad(System.Windows.Forms.WebBrowser webBrowser1)
        {
            int i = 0;
            string sUrl;
            while (true)
            {
                //系统延迟50毫秒
                Delay(50);
                //先判断是否发生完成事件
                if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
                {
                    //再判断是浏览器是否繁忙       
                    if (!webBrowser1.IsBusy)
                    {
                        i = i + 1;
                        if (i == 2)
                        {
                            sUrl = webBrowser1.Url.ToString();
                            //判断没有网络的情况下     
                            if (sUrl.Contains("res"))
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        continue;
                    }
                    i = 0;
                }
            }
        }
        #endregion
    } 
}
