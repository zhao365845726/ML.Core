using ML.Core.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ML.Core
{
    /// <summary>
    /// 自动绑定属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class DatabaseAttribute : BaseAttribute
    {
        #region 声明变量
        public FieldType FieldType { get; set; }
        public string FieldName { get; set; }
        public string FieldExplain { get; set; }
        public int FieldLen { get; set; }
        public FormControllerType FormControllerType { get; set; }
        public string FormControllerValue { get; set; }
        public IDictionary<string, object> RelationFieldCollection { get; set; }
        public bool InParam { get; set; }
        public bool OutParam { get; set; }
        public bool Ignore { get; set; }
        #endregion
        /// <summary>
        /// DatabaseAttribute 构造函数
        /// </summary>
        public DatabaseAttribute() { }

        public DatabaseAttribute(bool ignore)
        {
            Ignore = ignore;
        }

        public DatabaseAttribute(FieldType fieldType, string fieldName, string fieldExplain, int fieldLen, FormControllerType formControllerType, string formControllerValue)
        {
            FieldType = fieldType;
            FieldName = fieldName;
            FieldExplain = fieldExplain;
            FieldLen = fieldLen;
            FormControllerType = formControllerType;
            FormControllerValue = formControllerValue;
        }

        public DatabaseAttribute(FieldType fieldType,string fieldName,string fieldExplain,int fieldLen,FormControllerType formControllerType, string formControllerValue, bool inParam, bool outParam )
        {
            FieldType = fieldType;
            FieldName = fieldName;
            FieldExplain = fieldExplain;
            FieldLen = fieldLen;
            FormControllerType = formControllerType;
            FormControllerValue = formControllerValue;
            InParam = inParam;
            OutParam = outParam;
        }

        public DatabaseAttribute(FieldType fieldType, string fieldName, string fieldExplain, int fieldLen, FormControllerType formControllerType, string formControllerValue,IDictionary<string,object> relationFieldCollection)
        {
            FieldType = fieldType;
            FieldName = fieldName;
            FieldExplain = fieldExplain;
            FieldLen = fieldLen;
            FormControllerType = formControllerType;
            FormControllerValue = formControllerValue;
            RelationFieldCollection = relationFieldCollection;
        }
    }
}
