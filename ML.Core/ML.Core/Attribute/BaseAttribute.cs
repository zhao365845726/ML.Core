using System;
using System.Reflection;

namespace ML.Core
{
    /// <summary>
    /// 基属性
    /// 参考：https://docs.microsoft.com/zh-cn/dotnet/standard/attributes/writing-custom-attributes#applying-the-attributeusageattribute
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class BaseAttribute : Attribute
    {
        /// <summary>
        /// 是否忽略当前标签
        /// </summary>
        public bool Ignore { get; set; }

        /// <summary>
        /// BaseAttribute 构造函数
        /// </summary>
        public BaseAttribute() { }

    }
}
