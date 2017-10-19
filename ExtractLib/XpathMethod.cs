using System;
using System.Collections.Generic;
using HtmlAgilityPack;
namespace ExtractLib
{
    /// <summary>
    /// 使用xpath抽取文本
    /// </summary>
    public class XpathMethod
    {
        public XpathMethod(String html)
        {
            Document.LoadHtml(html);
        }

        public XpathMethod()
        {
            // TODO: Complete member initialization
        }

        private HtmlAgilityPack.HtmlDocument _Document = new HtmlDocument();

        public HtmlAgilityPack.HtmlDocument Document
        {
            get { return _Document; }
            set { _Document = value; }
        }
        #region
        /// <summary>
        /// 抽取单个node的内容
        /// </summary>
        /// <param name="Xpath">Xpath字符串</param>
        /// <param name="Html">待抽取的标准Html源代码</param>
        /// <returns></returns>
        public static String GetSingleResult(String Xpath, String Html)
        {
            String retStr = String.Empty;
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            HtmlNode node = doc.DocumentNode.SelectSingleNode(Xpath);
            if (node != null)
            {
                retStr = node.InnerText.Trim(); ;
            }
            return retStr;
        }
        /// <summary>
        /// 抽取单个node的内容  Flag标识抽取或匹配
        /// </summary>
        /// <param name="Xpath">Xpath字符串</param>
        /// <param name="Html">待抽取的标准Html源代码</param>
        /// <param name="Flag">1为匹配 0为抽取</param>
        /// <returns></returns>
        public static String GetSingleResult(String Xpath, String Html, int Flag)
        {
            String retStr = String.Empty;
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            HtmlNode node = doc.DocumentNode.SelectSingleNode(Xpath);
            if (node != null)
            {
                if (Flag == 1)
                {
                    retStr = node.InnerText;
                }
                else if (Flag == 0)
                {
                    retStr = node.OuterHtml;
                }
            }
            return retStr;
        }
        /// <summary>
        /// 抽取单个node指定属性
        /// </summary>
        /// <param name="Xpath">Xpath字符串</param>
        /// <param name="Html">待抽取的标准Html源代码</param>
        /// <param name="Attributes">待抽取的节点属性</param>
        /// <returns></returns>
        public static String GetSingleResult(String Xpath, String Html, String Attributes)
        {
            String retStr = String.Empty;
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            HtmlNode node = doc.DocumentNode.SelectSingleNode(Xpath);
            if (node != null)
            {
                try
                {
                    if (node.Attributes.Contains(Attributes))
                    {
                       retStr = node.Attributes[Attributes].Value;
                    }
                }
                catch
                {

                }
            }
            return retStr;
        }
        /// <summary>
        /// 抽取多个节点内容
        /// </summary>
        /// <param name="Xpath">Xpath字符串</param>
        /// <param name="Html">待抽取的标准Html源代码</param>
        /// <returns></returns>
        public static List<String> GetMutResult(String Xpath, String Html)
        {
            List<String> retlist = new List<string>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Xpath);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (var node in nodes)
                {
                    retlist.Add(node.InnerText.Trim());
                }
            }
            return retlist;
        }
        /// <summary>
        /// 抽取多个节点内容
        /// </summary>
        /// <param name="Xpath">Xpath字符串</param>
        /// <param name="Html">待抽取的标准Html源代码</param>
        /// <param name="Flag">0为匹配 1为抽取</param>
        /// <returns></returns>
        public static List<String> GetMutResult(String Xpath, String Html, int Flag)
        {
            List<String> retlist = new List<string>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Xpath);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (var node in nodes)
                {
                    if (Flag == 1)
                    {
                        retlist.Add(node.InnerText);
                    }
                    else if (Flag == 0)
                    {
                        retlist.Add(node.OuterHtml);
                    }
                }
            }
            return retlist;
        }
        /// <summary>
        /// 抽取多个node 指定属性
        /// </summary>
        /// <param name="Xpath">Xpath字符串</param>
        /// <param name="Html">待抽取的标准Html源代码</param>
        /// <param name="Attributes">待抽取的节点属性</param>
        /// <returns></returns>
        public static List<String> GetMutResult(String Xpath, String Html, String Attributes)
        {
            List<String> retlist = new List<string>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Xpath);
            if (nodes != null && nodes.Count > 0)
            {
                foreach (var node in nodes)
                {
                    try
                    {
                        if (node.Attributes.Contains(Attributes))
                        {
                            retlist.Add(node.Attributes[Attributes].Value);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            return retlist;
        }
        /// <summary>
        /// 抽取多个node
        /// </summary>
        /// <param name="Xpath">Xpath字符串</param>
        /// <param name="Html">待抽取的标准Html源代码</param>
        /// <returns></returns>
        public static HtmlNodeCollection GetMutNodes(String Xpath, String Html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Html);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(Xpath);

            return nodes;
        }
        #endregion

        public String GetSingleResultA(String Xpath, String Attributes = null, int Flag = 1)
        {
            if (Document == null)
            {
                return String.Empty;
            }
            var node = Document.DocumentNode.SelectSingleNode(Xpath);
            if (node == null)
            {
                return null;
            }
            if (String.IsNullOrEmpty(Attributes))
            {
                if (Flag == 0)
                {
                    return node.InnerHtml;
                }
                else if (Flag == 1)
                {
                    return node.InnerText;
                }
                else
                {
                    return String.Empty;
                }
            }
            else
            {
                if (node.Attributes.Contains(Attributes))
                {
                   return node.Attributes[Attributes].Value;
                }
                return node.Attributes[Attributes].Value;
            }
        }
        public List<String> GetMutResultA(String Xpath, int Flag = 1)
        {
            List<String> list = new List<string>();
            if (Document == null)
            {
                return list;
            }
            var nodes = Document.DocumentNode.SelectNodes(Xpath);
            if (nodes == null || nodes.Count == 0)
            {
                return null;
            }
            foreach (var node in nodes)
            {
                if (Flag == 0)
                {
                    list.Add(node.InnerHtml);
                }
                else if (Flag == 1)
                {
                    list.Add(node.InnerText);
                }
            }
            return list;
        }
        public List<String> GetMutResultA(String Xpath, String Attributes = null, int Flag = 1)
        {
            List<String> list = new List<string>();
            if (Document == null)
            {
                return list;
            }
            var nodes = Document.DocumentNode.SelectNodes(Xpath);
            if (nodes == null || nodes.Count == 0)
            {
                return null;
            }
            foreach (var node in nodes)
            {
                if (Flag == 0)
                {
                    if(String.IsNullOrEmpty(Attributes))
                    {
                        list.Add(node.InnerHtml);
                    }
                    else
                    {
                        list.Add(node.Attributes[Attributes].Value);
                    }
                }
                else if (Flag == 1)
                {
                    if (String.IsNullOrEmpty(Attributes))
                    {
                        list.Add(node.InnerText);
                    }
                    else
                    {
                        if (node.Attributes.Contains(Attributes))
                        {
                            list.Add(node.Attributes[Attributes].Value);
                        }
                    }
                }
            }
            return list;
        }
    }
}
