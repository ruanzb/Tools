#region 说明
//---------------------------------------名称:封装控件跨线程访问
//---------------------------------------版本:1.0.0.0
//---------------------------------------更新时间:2017/10/19
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools
#endregion

#region 名空间
using System;
using System.Windows.Forms; 
#endregion

namespace PControlsLib
{
    /// <summary>
    /// 封装控件跨线程访问
    /// </summary>
    public class DelegateMethod
    {
        #region 方法
        /// <summary>
        /// 改变box控件的text属性 可以设置追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Control"></param>
        /// <param name="txt"></param>
        /// <param name="append"></param>
        public static void UpdateBox<T>(T Control, String txt, bool append = false)
        {
            var type = Control.GetType();
            if (typeof(System.Windows.Forms.TextBox) == type)
            {
                if ((Control as TextBox).InvokeRequired)
                {
                    Action<String> method = delegate(String x)
                        {
                            (Control as TextBox).Text = x;
                        };
                    try
                    {
                        (Control as TextBox).Invoke(method, txt);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    (Control as TextBox).Text = txt;
                }
            }
            else if (typeof(System.Windows.Forms.RichTextBox) == type)
            {
                if ((Control as RichTextBox).InvokeRequired)
                {
                    Action method = delegate
                    {
                        if (append)
                        {
                            (Control as RichTextBox).AppendText(txt);
                        }
                        else
                        {
                            (Control as RichTextBox).Text = txt;
                        }
                        (Control as RichTextBox).ScrollToCaret();

                    };
                    try
                    {
                        (Control as RichTextBox).Invoke(method);
                    }
                    catch
                    {

                    }

                }
                else
                {
                    if (append)
                    {
                        (Control as RichTextBox).AppendText(txt);
                    }
                    else
                    {
                        (Control as RichTextBox).Text = txt;
                    }
                    try
                    {
                        (Control as RichTextBox).ScrollToCaret();
                    }
                    catch
                    {

                    }

                }
            }
            //var type = Control.GetType();
            //if(type.ToString() == )
            //{

            //}
            //object[] args = {  richtextbox,txt,append};
            //textboxdel.Invoke(richtextbox, txt,append);
        }
        /// <summary>
        /// 改变控件的Text属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Control"></param>
        /// <param name="txt"></param>
        public static void UpdateText<T>(T Control, String txt)
        {
            var type = Control.GetType();
            if ((Control as Control).InvokeRequired)
            {
                Action<String> method = delegate(String x)
                {
                    (Control as Control).Text = x;
                };
                try
                {
                    (Control as Control).Invoke(method, txt);
                }
                catch
                {

                }
            }
            else
            {
                (Control as Control).Text = txt;
            }
        }

        /// <summary>
        /// 改变控件的Enable属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Control"></param>
        /// <param name="enable"></param>
        public static void EnableChange<T>(T Control, bool enable)
        {
            if ((Control as Control).InvokeRequired)
            {
                Action<bool> method = delegate(bool cenable)
                {
                    (Control as Control).Enabled = cenable;
                };
                try
                {
                    (Control as Control).Invoke(method, enable);
                }
                catch
                {

                }
            }
            else
            {
                (Control as Control).Enabled = enable;

            }
        } 
        #endregion
    }
}
