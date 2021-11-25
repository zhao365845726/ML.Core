using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Enum
{
    /// <summary>
    /// 编译类的日期类型
    /// </summary>
    public enum BuildClassDateType
    {
        /// <summary>
        /// 当天
        /// </summary>
        DAY = 0,
        /// <summary>
        /// 本周
        /// </summary>
        WEEK = 1,
        /// <summary>
        /// 本月
        /// </summary>
        MONTH = 2,
        /// <summary>
        /// 本年
        /// </summary>
        YEAR = 3,
        /// <summary>
        /// 自定义日期
        /// </summary>
        CUSTOM = 90,
        /// <summary>
        /// 自定义天
        /// </summary>
        CUSTOMDAY = 90,
        /// <summary>
        /// 自定义周
        /// </summary>
        CUSTOMWEEK = 90,
        /// <summary>
        /// 自定义月
        /// </summary>
        CUSTOMMONTH = 90,
        /// <summary>
        /// 自定义年
        /// </summary>
        CUSTOMYEAR = 90,
    }

    public enum FieldType
    {
        INT = 1,
        STRING = 2,
        BOOL = 3,
        DATE = 4,
        TIME = 5,
        DATETIME = 6,
        DECIMAL = 7,
        LONG = 8,
        SHORT = 9,
        GUID = 10,
    }

    public enum FormControllerType
    {
        INPUT = 1,
        SELECT = 2,
        TEXTAREA = 3,
        RADIO = 4,
        CHECK = 5,
        PICKER = 6,
        NUMBER = 7,
        UPLOAD = 8,
        DATETIME = 9,
        DATE = 10
    }

    public enum ListColumnType
    {

    }

    public enum InterfaceParamType
    {
        IN = 1,
        OUT = 2
    }
}
