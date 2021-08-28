using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Assemblies
{
    /// <summary>
    /// 反射结果类
    /// </summary>
    public class AssemblyResult
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public List<string> AssemblyName { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public List<string> ClassName { get; set; }
        /// <summary>
        /// 类的属性
        /// </summary>
        public List<string> Properties { get; set; }
        /// <summary>
        /// 类的方法
        /// </summary>
        public List<string> Methods { get; set; }
    }

    /// <summary>
    /// 反射结果类
    /// </summary>
    public class AssemblyDictionaryResult
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public List<string> AssemblyName { get; set; }
        ///// <summary>
        ///// 类名
        ///// </summary>
        //public Dictionary<string, List<string>> ClassName { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public Dictionary<string, Dictionary<string, DatabaseClassAttribute>> ClassName { get; set; }
        /// <summary>
        /// 类的属性
        /// </summary>
        public List<Dictionary<string, List<string>>> Properties { get; set; }
        /// <summary>
        /// 类的属性
        /// </summary>
        public List<Dictionary<string, Dictionary<string, DatabaseAttribute>>> PropertiesAttributes { get; set; }
        /// <summary>
        /// 类的方法
        /// </summary>
        public List<Dictionary<string, List<string>>> Methods { get; set; }
    }
}
