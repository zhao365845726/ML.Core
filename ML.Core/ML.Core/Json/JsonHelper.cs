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
        public static List<T> JonsToList<T>(this string Json)
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
    }
}
