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
        /// <summary>
        /// 表单的类型
        /// </summary>
        public FormControllerType FormControllerType { get; set; }
        /// <summary>
        /// 表单的值
        /// </summary>
        public string FormControllerValue { get; set; }
        /// <summary>
        /// 关联字段
        /// </summary>
        public string RelationField { get; set; }
        /// <summary>
        /// 关联表
        /// </summary>
        public string RelationTable { get; set; }
        /// <summary>
        /// 是否需要关联
        /// </summary>
        public bool IsRelation { get; set; }
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

        public DatabaseAttribute(FieldType fieldType,string fieldName,string fieldExplain,int fieldLen,FormControllerType formControllerType, string formControllerValue, bool isRelation, string relationTable, string relationField, bool inParam, bool outParam )
        {
            FieldType = fieldType;
            FieldName = fieldName;
            FieldExplain = fieldExplain;
            FieldLen = fieldLen;
            FormControllerType = formControllerType;
            FormControllerValue = formControllerValue;
            IsRelation = isRelation;
            RelationTable = relationTable;
            RelationField = relationField;
            InParam = inParam;
            OutParam = outParam;
        }

        public DatabaseAttribute(FieldType fieldType, string fieldName, string fieldExplain, int fieldLen, FormControllerType formControllerType, string formControllerValue, bool isRelation, string relationTable, string relationField)
        {
            FieldType = fieldType;
            FieldName = fieldName;
            FieldExplain = fieldExplain;
            FieldLen = fieldLen;
            FormControllerType = formControllerType;
            FormControllerValue = formControllerValue;
            IsRelation = isRelation;
            RelationTable = relationTable;
            RelationField = relationField;
        }
    }
}
