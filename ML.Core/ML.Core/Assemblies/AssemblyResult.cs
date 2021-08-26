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
}
