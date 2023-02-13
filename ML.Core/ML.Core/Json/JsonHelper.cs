//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace ML.Core
{
    /// <summary>
    /// 转换Json格式帮助类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 把json字符串转换为Object对象
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static object ToJson(this string Json)
        {
            return JsonConvert.DeserializeObject(Json);
        }

        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将json字符串转换为DataTable
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(this string strJson)
        {
            #region
            DataTable tb = null;
            //获取数据  
            Regex rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                //string[] strRows = strRow.Split(',');
                string[] strRows = Regex.Split(strRow, "','");
                //创建表  
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = "Table";
                    foreach (string str in strRows)
                    {
                        DataColumn dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.DataType = typeof(String);
                        dc.ColumnName = strCell[0].ToString().Replace("\"", "").Replace("'", "").Trim();
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }
                //增加内容  
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    object strText = strRows[r].Split(':')[1].Trim()
                                              .Replace("，", ",")
                                              .Replace("：", ":")
                                              .Replace("/", "")
                                              .Replace("\"", "")
                                              .Replace("'", "")
                                              .Trim();
                    if (strText.ToString().Length >= 5)
                    {
                        if (strText.ToString().Substring(0, 5) == "Date(")//判断是否JSON日期格式
                        {
                            strText = CommonHelper.JsonToDateTime(strText.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    dr[r] = strText;
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
            #endregion
        }

        /// <summary>
        /// 将json文本转换为List实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static List<T> JsonToList<T>(this string Json)
        {
            return JsonConvert.DeserializeObject<List<T>>(Json);
        }

        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T JsonToEntity<T>(this string Json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Json);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将json文本转换为字典
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static IDictionary<string, object> LoadJsonStr(string jsonStr)
        {
            var data = JsonConvert.DeserializeObject<IDictionary<string, object>>(jsonStr);
            return data;
        }

        /// <summary>
        /// 将json byte转换为字典
        /// </summary>
        /// <param name="jsonByte"></param>
        /// <returns></returns>
        public static IDictionary<string, object> LoadJsonByte(byte[] jsonByte)
        {
            string result = Encoding.Default.GetString(jsonByte);
            var data = JsonConvert.DeserializeObject<IDictionary<string, object>>(result);
            return data;
        }

        public static IDictionary<string, object> GetConfigInfo(string filePath)
        {
            string text = File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            var configInfo = JsonConvert.DeserializeObject<IDictionary<string, object>>(text);
            return configInfo;
        }

        public static T GetConfigInfo<T>(string filePath,string section)
        {
            string text = File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            var configInfo = JsonConvert.DeserializeObject<IDictionary<string, object>>(text);
            var result = JsonConvert.DeserializeObject<T>(configInfo[section].ToString());
            return result;
        }

        /// <summary>
        /// json字符串转换为Xml对象
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static XmlDocument JsonToXml(string sJson)
        {
            //XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(sJson), XmlDictionaryReaderQuotas.Max);
            //XmlDocument doc = new XmlDocument();
            //doc.Load(reader);

            //JavaScriptSerializer oSerializer = new JavaScriptSerializer();
            //Dictionary<string, object> Dic = (Dictionary<string, object>)oSerializer.DeserializeObject(sJson);

            Dictionary<string, object> Dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(sJson);
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDec;
            xmlDec = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            doc.InsertBefore(xmlDec, doc.DocumentElement);
            XmlElement nTop = doc.CreateElement("ufinterface");
            doc.AppendChild(nTop);
            nTop.SetAttribute("account", "develop");
            nTop.SetAttribute("billtype", "vouchergl");
            nTop.SetAttribute("businessunitcode", "develop");
            nTop.SetAttribute("filename", "");
            nTop.SetAttribute("groupcode", "");
            nTop.SetAttribute("isexchange", "");
            nTop.SetAttribute("orgcode", "");
            nTop.SetAttribute("receiver", "0001121000000000JIYO");
            nTop.SetAttribute("replace", "");
            nTop.SetAttribute("roottag", "");
            nTop.SetAttribute("sender", "001");

            XmlElement nRoot = doc.CreateElement("voucher");
            nTop.AppendChild(nRoot);

            XmlElement nRoot_Head = doc.CreateElement("voucher_head");
            nRoot.AppendChild(nRoot_Head);

            foreach (KeyValuePair<string, object> item in Dic)
            {
                XmlElement element = doc.CreateElement(item.Key);
                KeyValue2Xml(element, item);
                nRoot_Head.AppendChild(element);
            }
            return doc;
        }

        /// <summary>
        /// json字符串转换为Xml对象
        /// </summary>
        /// <param name="sJson"></param>
        /// <returns></returns>
        public static XmlDocument JsonListToXml(string sJson)
        {
            List<Dictionary<string, object>> DicList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(sJson);
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDec;
            xmlDec = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            doc.InsertBefore(xmlDec, doc.DocumentElement);
            XmlElement nTop = doc.CreateElement("ufinterface");
            doc.AppendChild(nTop);
            nTop.SetAttribute("account", "develop");
            nTop.SetAttribute("billtype", "vouchergl");
            nTop.SetAttribute("businessunitcode", "develop");
            nTop.SetAttribute("filename", "");
            nTop.SetAttribute("groupcode", "");
            nTop.SetAttribute("isexchange", "");
            nTop.SetAttribute("orgcode", "");
            nTop.SetAttribute("receiver", "0001121000000000JIYO");
            nTop.SetAttribute("replace", "");
            nTop.SetAttribute("roottag", "");
            nTop.SetAttribute("sender", "001");

            //TODO:增加多个节点
            
            for (int i = 0; i < DicList.Count; i++)
            {
                XmlElement nRoot = doc.CreateElement("voucher");
                nTop.AppendChild(nRoot);

                XmlElement nRoot_Head = doc.CreateElement("voucher_head");
                nRoot.AppendChild(nRoot_Head);
                Dictionary<string, object> Dic = DicList[i];
                foreach (KeyValuePair<string, object> item in Dic)
                {
                    XmlElement element = doc.CreateElement(item.Key);
                    KeyValue2Xml(element, item);
                    nRoot_Head.AppendChild(element);
                }
            }
            return doc;
        }

        private static void KeyValue2Xml(XmlElement node, KeyValuePair<string, object> Source)
        {
            object kValue = Source.Value;
            if (kValue.GetType() == typeof(Dictionary<string, object>))
            {
                foreach (KeyValuePair<string, object> item in kValue as Dictionary<string, object>)
                {
                    XmlElement element = node.OwnerDocument.CreateElement(item.Key);
                    KeyValue2Xml(element, item);
                    node.AppendChild(element);
                }
            }
            else if (kValue.GetType() == typeof(object[]))
            {
                object[] o = kValue as object[];
                for (int i = 0; i < o.Length; i++)
                {
                    XmlElement xitem = node.OwnerDocument.CreateElement("Item");
                    KeyValuePair<string, object> item = new KeyValuePair<string, object>("Item", o);
                    KeyValue2Xml(xitem, item);
                    node.AppendChild(xitem);
                }

            }
            else
            {
                XmlText text = node.OwnerDocument.CreateTextNode(kValue.ToString());
                node.AppendChild(text);
            }
        }
    }
}
