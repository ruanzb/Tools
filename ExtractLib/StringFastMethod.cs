using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtractLib
{
    public class StringFastMethod
    {

        /// <summary>
        /// 取字符串中间 切割用
        /// </summary>
        /// <param name="str">总字符串</param>
        /// <param name="str1">左边字符串</param>
        /// <param name="str2">右边字符串</param>
        /// <returns>中间字符串</returns>
        public static string QuMiddle(string str, string str1, string str2)
        {
            int leftlocation;
            //左边位置
            int rightlocation
                ;//右边位置 
            int strmidlength;
            ;//中间字符串长度
            string strmid = null;
            ;//中间字符串
            leftlocation = str.IndexOf(str1);
            //获取左边字符串头所在位置 
            if (leftlocation == -1)
            //判断左边字符串是否存在于总字符串中
            {
                return "";
            }
            leftlocation = leftlocation + str1.Length;
            //获取左边字符串尾所在位置 
            rightlocation = str.IndexOf(str2, leftlocation);
            //获取右边字符串头所在位置 
            if (rightlocation == -1 || leftlocation > rightlocation)
            //判断右边字符串是否存在于总字符串中，左边字符串位置是否在右边字符串前
            {
                return "";
            }
            strmidlength = rightlocation - leftlocation;
            //计算中间字符串长度 
            strmid = str.Substring(leftlocation, strmidlength);
            //取出中间字符串 
            return strmid;
            //返回中间字符串
        }

        /// <summary>
        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unicode编码后的字符串</returns>
        public static string StringToUnicode(string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        public static string UnicodeToString(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                         source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
        /// <summary>
        /// Unicode转字符串 重载
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        public static string UnicodeToString(object source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                         Convert.ToString(source), x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
    }
}
