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
        /// <summary>
        /// 类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 类描述
        /// </summary>
        public string ClassExplain { get; set; }
        /// <summary>
        /// 编译日期类型
        /// </summary>
        public BuildClassDateType BuildDateType { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public string BuildDate { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 是否显示菜单
        /// </summary>
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

        public DatabaseClassAttribute(string name, string explain, BuildClassDateType buildDateType,string buildDate)
        {
            ClassName = name;
            ClassExplain = explain;
            BuildDateType = buildDateType;
            BuildDate = buildDate;
        }

        public DatabaseClassAttribute(string name,string explain,string menuName,bool isShowMenu)
        {
            ClassName = name;
            ClassExplain = explain;
            MenuName = menuName;
            IsShowMenu = isShowMenu;
        }

        public DatabaseClassAttribute(string name, string explain, string menuName, bool isShowMenu, BuildClassDateType buildDateType, string buildDate)
        {
            ClassName = name;
            ClassExplain = explain;
            MenuName = menuName;
            IsShowMenu = isShowMenu;
            BuildDateType = buildDateType;
            BuildDate = buildDate;
        }
    }
}
