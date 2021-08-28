using ML.Core.Enum;
using System;
using System.Reflection;

namespace ML.Core
{
    /// <summary>
    /// 自动绑定属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class DatabaseClassAttribute : BaseAttribute
    {
        #region 声明变量
        public string ClassName { get; set; }
        public string ClassExplain { get; set; }
        public string MenuName { get; set; }
        public bool IsShowMenu { get; set; }
        #endregion
        /// <summary>
        /// DatabaseAttribute 构造函数
        /// </summary>
        public DatabaseClassAttribute() { }

        public DatabaseClassAttribute(bool ignore)
        {
            Ignore = ignore;
        }

        public DatabaseClassAttribute(string name, string explain)
        {
            ClassName = name;
            ClassExplain = explain;
        }

        public DatabaseClassAttribute(string name,string explain,string menuName,bool isShowMenu)
        {
            ClassName = name;
            ClassExplain = explain;
            MenuName = menuName;
            IsShowMenu = isShowMenu;
        }

    }
}
