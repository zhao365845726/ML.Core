using ML.Core.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ML.Core.Assemblies
{
    /// <summary>
    /// 反射处理类
    /// </summary>
    public class AssemblyHandler
    {
        private string path = string.Empty;
        public AssemblyResult assemblyResult;
        public AssemblyDictionaryResult assemblyDictionaryResult;

        public AssemblyHandler(string packagePath)
        {
            if (!string.IsNullOrEmpty(packagePath))
            {
                path = packagePath;
            }
            //声明一个新的assemblyResult
            assemblyResult = new AssemblyResult();
        }

        /// <summary>
        /// 获取程序集名称列表
        /// </summary>
        /// <returns></returns>
        public AssemblyResult GetAssemblyName()
        {
            AssemblyResult result = new AssemblyResult();
            string[] dicFileName = Directory.GetFileSystemEntries(path);
            if (dicFileName != null)
            {
                List<string> assemblyList = new List<string>();
                foreach (string name in dicFileName)
                {
                    assemblyList.Add(name.Substring(name.LastIndexOf('/') + 1));
                }
                result.AssemblyName = assemblyList;
            }
            return result;
        }

        /// <summary>
        /// 获取程序集名称列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetAssemblyNameList(string assemblyName)
        {
            string[] dicFileName = Directory.GetFileSystemEntries(path);
            if (dicFileName != null)
            {
                List<string> assemblyList = new List<string>();
                foreach (string name in dicFileName)
                {
                    if (!string.IsNullOrEmpty(assemblyName))
                    {
                        if (name.Contains($"{assemblyName}.dll"))
                        {
                            assemblyList.Add(name.Substring(name.LastIndexOf('/') + 1));
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        assemblyList.Add(name.Substring(name.LastIndexOf('/') + 1));
                    }
                }
                assemblyResult.AssemblyName = assemblyList;
                return assemblyList;
            }
            return new List<string>();
        }

        /// <summary>
        /// 获取程序集中的类名称
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public AssemblyResult GetClassName(string assemblyName, string filterWords)
        {
            AssemblyResult result = new AssemblyResult();
            if (!String.IsNullOrEmpty(assemblyName))
            {
                assemblyName = path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type[] ts = assembly.GetTypes();
                List<string> classList = new List<string>();
                foreach (Type t in ts)
                {
                    //classList.Add(t.Name);
                    if (t.FullName.Contains(filterWords))
                    {
                        classList.Add(t.FullName);
                    }
                }
                result.ClassName = classList;
            }
            return result;
        }

        /// <summary>
        /// 获取程序集中的类名称
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public List<string> GetClassNameList(string assemblyName, string filterWords)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                //assemblyName = path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type[] ts = assembly.GetTypes();
                List<string> classList = new List<string>();
                foreach (Type t in ts)
                {
                    //classList.Add(t.Name);
                    if (t.FullName.Contains(filterWords))
                    {
                        classList.Add(t.FullName);
                    }
                }
                assemblyResult.ClassName = classList;
                return classList;
            }
            return new List<string>();
        }

        /// <summary>
        /// 获取程序集中的类名称
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <param name="filterWords">过滤取出的类命名空间</param>
        /// <returns></returns>
        public Dictionary<string,DatabaseClassAttribute> GetClassNameDictionary(string assemblyName, string filterWords)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                //assemblyName = path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type[] ts = assembly.GetTypes();
                Dictionary<string, DatabaseClassAttribute> dicClass = new Dictionary<string, DatabaseClassAttribute>();
                List<string> classList = new List<string>();
                foreach (Type t in ts)
                {
                    if (t == null)
                    {
                        continue;
                    }
                    //classList.Add(t.Name);
                    if (t.FullName.Contains(filterWords))
                    {
                        //获取类的属性信息
                        var classAttributeInfoList = GetClassAttributeInfoList(assemblyName, t.FullName);
                        if(classAttributeInfoList == null)
                        {
                            continue;
                        }
                        dicClass.Add(t.FullName, classAttributeInfoList);
                        classList.Add(t.FullName);
                    }
                }
                assemblyResult.ClassName = classList;
                return dicClass;
            }
            return new Dictionary<string, DatabaseClassAttribute>();
        }

        /// <summary>
        /// 获取程序集中的类名称
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <param name="filterWords">过滤取出的类命名空间</param>
        /// <param name="buildClassDateType">生成类的日期类型</param>
        /// <param name="buildDate">生成日期</param>
        /// <returns></returns>
        public Dictionary<string, DatabaseClassAttribute> GetClassNameDictionary(string assemblyName, string filterWords, BuildClassDateType buildClassDateType, string buildDate)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                //assemblyName = path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type[] ts = assembly.GetTypes();
                Dictionary<string, DatabaseClassAttribute> dicClass = new Dictionary<string, DatabaseClassAttribute>();
                List<string> classList = new List<string>();
                foreach (Type t in ts)
                {
                    if(t == null)
                    {
                        continue;
                    }
                    //classList.Add(t.Name);
                    if (t.FullName.Contains(filterWords))
                    {
                        //获取类的属性信息
                        var classAttributeInfoList = GetClassAttributeInfoList(assemblyName, t.FullName,buildClassDateType,buildDate);
                        if (classAttributeInfoList == null)
                        {
                            continue;
                        }
                        //根据生成的日期和日期类型来自定义生成指定的类
                        if(classAttributeInfoList.BuildDateType.Equals(buildClassDateType) && classAttributeInfoList.BuildDate.Equals(buildDate))
                        {
                            dicClass.Add(t.FullName, classAttributeInfoList);
                            classList.Add(t.FullName);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                assemblyResult.ClassName = classList;
                return dicClass;
            }
            return new Dictionary<string, DatabaseClassAttribute>();
        }

        /// <summary>
        /// 获取类信息
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public AssemblyResult GetClassInfo(string assemblyName, string className)
        {
            AssemblyResult result = new AssemblyResult();
            if (!String.IsNullOrEmpty(assemblyName) && !String.IsNullOrEmpty(className))
            {
                assemblyName = path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type type = assembly.GetType(className, true, true);
                if (type != null)
                {
                    //类的属性
                    List<string> propertieList = new List<string>();
                    PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    foreach (PropertyInfo p in propertyInfo)
                    {
                        propertieList.Add(p.ToString());
                    }
                    result.Properties = propertieList;

                    //类的方法
                    List<string> methods = new List<string>();
                    MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (MethodInfo mi in methodInfos)
                    {
                        methods.Add(mi.Name);
                        //方法的参数
                        //foreach(ParameterInfo p in mi.GetParameters())
                        //{

                        //}
                        ////方法的返回值
                        //string returnParameter = mi.ReturnParameter.ToString();
                    }
                    result.Methods = methods;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取类属性列表
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public List<string> GetClassPropertiesInfoList(string assemblyName, string className)
        {
            if (!String.IsNullOrEmpty(assemblyName) && !String.IsNullOrEmpty(className))
            {
                //assemblyName = path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type type = assembly.GetType(className, true, true);
                if (type != null)
                {
                    //类的属性
                    List<string> propertieList = new List<string>();
                    PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    foreach (PropertyInfo p in propertyInfo)
                    {
                        propertieList.Add(p.ToString());
                    }
                    assemblyResult.Properties = propertieList;
                    return propertieList;
                }
            }
            return new List<string>();
        }

        /// <summary>
        /// 获取类属性注解列表
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public Dictionary<string, DatabaseAttribute> GetClassPropertiesAttributeInfoList(string assemblyName, string className)
        {
            if (!String.IsNullOrEmpty(assemblyName) && !String.IsNullOrEmpty(className))
            {
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type type = assembly.GetType(className, true, true);
                if (type != null)
                {
                    //类的属性
                    Dictionary<string, DatabaseAttribute> propertieAttributeList = new Dictionary<string, DatabaseAttribute>();
                    PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    foreach (PropertyInfo p in propertyInfo)
                    {
                        DatabaseAttribute databaseAttribute = (DatabaseAttribute)Attribute.GetCustomAttribute(p, typeof(DatabaseAttribute));
                        propertieAttributeList.Add(p.ToString(),databaseAttribute);
                    }
                    return propertieAttributeList;
                }
            }
            return new Dictionary<string, DatabaseAttribute>();
        }

        /// <summary>
        /// 获取类属性注解列表
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public DatabaseClassAttribute GetClassAttributeInfoList(string assemblyName, string className)
        {
            if (!String.IsNullOrEmpty(assemblyName) && !String.IsNullOrEmpty(className))
            {
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type type = assembly.GetType(className, true, true);
                if (type != null)
                {
                    DatabaseClassAttribute databaseClassAttribute = (DatabaseClassAttribute)type.GetCustomAttribute(typeof(DatabaseClassAttribute), true);
                    return databaseClassAttribute;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取类属性注解列表
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="buildClassDateType">生成类的日期类型</param>
        /// <param name="buildDate">生成日期</param>
        /// <returns></returns>
        public DatabaseClassAttribute GetClassAttributeInfoList(string assemblyName, string className, BuildClassDateType buildClassDateType, string buildDate)
        {
            if (!String.IsNullOrEmpty(assemblyName) && !String.IsNullOrEmpty(className))
            {
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type type = assembly.GetType(className, true, true);
                if (type != null)
                {
                    DatabaseClassAttribute databaseClassAttribute = (DatabaseClassAttribute)type.GetCustomAttribute(typeof(DatabaseClassAttribute), true);
                    /*
                     * 如果生成日期类型小于等于0，或者生成日期类型与传入的不匹配
                     * 或者生成日期为null，或者生成日期与传入不匹配则全部返回null
                     */
                    if(databaseClassAttribute == null || databaseClassAttribute.BuildDate == null || databaseClassAttribute.BuildDateType < 0 || !databaseClassAttribute.BuildDateType.Equals(buildClassDateType) || !databaseClassAttribute.BuildDate.Equals(buildDate))
                    {
                        return null;
                    }
                    return databaseClassAttribute;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取类的自定义属性
        /// </summary>
        /// <param name="t"></param>
        public Dictionary<string,DatabaseAttribute> GetClassPropertiesAttribute(Type t)
        {
            var Properties = t.GetProperties();
            Dictionary<string, DatabaseAttribute> dicRes = new Dictionary<string, DatabaseAttribute>();
            foreach (var item in Properties)
            {
                DatabaseAttribute MyAttributes = (DatabaseAttribute)Attribute.GetCustomAttribute(item, typeof(DatabaseAttribute));
                if (MyAttributes == null)
                {
                    continue;
                }
                dicRes.Add(item.Name, MyAttributes);
            }
            return dicRes;
        }

        /// <summary>
        /// 获取类方法列表
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public List<string> GetClassMethodsInfoList(string assemblyName, string className)
        {
            if (!String.IsNullOrEmpty(assemblyName) && !String.IsNullOrEmpty(className))
            {
                //assemblyName = path + assemblyName;
                Assembly assembly = Assembly.LoadFrom(assemblyName);
                Type type = assembly.GetType(className, true, true);
                if (type != null)
                {
                    //类的方法
                    List<string> methods = new List<string>();
                    MethodInfo[] methodInfos = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (MethodInfo mi in methodInfos)
                    {
                        methods.Add(mi.Name);
                        //方法的参数
                        //foreach(ParameterInfo p in mi.GetParameters())
                        //{

                        //}
                        ////方法的返回值
                        //string returnParameter = mi.ReturnParameter.ToString();
                    }
                    assemblyResult.Methods = methods;
                    return methods;
                }
            }
            return new List<string>();
        }

        /// <summary>
        /// 获取类方法列表
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public AssemblyResult GetAssemblyResult(string assemblyName, string filterWords)
        {
            GetAssemblyNameList(assemblyName);
            if (assemblyResult.AssemblyName.Count > 0)
            {
                foreach (var assemblyItem in assemblyResult.AssemblyName)
                {
                    GetClassNameList(assemblyItem, filterWords);
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        GetClassPropertiesInfoList(assemblyItem, classItem);
                        GetClassMethodsInfoList(assemblyItem, classItem);
                    }
                }
            }

            return assemblyResult;
        }

        /// <summary>
        /// 获取类方法列表
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="filterWords">过滤取出的类命名空间</param>
        /// <returns></returns>
        public AssemblyDictionaryResult GetAssemblyDictionaryResult(string assemblyName, string filterWords)
        {
            AssemblyDictionaryResult assemblyDictionaryResult = new AssemblyDictionaryResult();
            GetAssemblyNameList(assemblyName);
            assemblyDictionaryResult.AssemblyName = assemblyResult.AssemblyName;
            if (assemblyResult.AssemblyName.Count > 0)
            {
                Dictionary<string, Dictionary<string, DatabaseClassAttribute>> dicClass = new Dictionary<string, Dictionary<string, DatabaseClassAttribute>>();
                foreach (var assemblyItem in assemblyResult.AssemblyName)
                {
                    //将程序集和类字典
                    dicClass.Add(assemblyItem, GetClassNameDictionary(assemblyItem, filterWords));
                    List<Dictionary<string, List<string>>> lstDicProperties = new List<Dictionary<string, List<string>>>();
                    List<Dictionary<string, Dictionary<string, DatabaseAttribute>>> lstDicPropertiesAttributes = new List<Dictionary<string, Dictionary<string, DatabaseAttribute>>>();
                    List<Dictionary<string, List<string>>> lstDicMethod = new List<Dictionary<string, List<string>>>();
                    //获取类的属性列表
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        Dictionary<string, List<string>> dicProperties = new Dictionary<string, List<string>>();
                        dicProperties.Add(classItem, GetClassPropertiesInfoList(assemblyItem, classItem));
                        lstDicProperties.Add(dicProperties);
                    }
                    //获取类的属性注解
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        Dictionary<string, Dictionary<string, DatabaseAttribute>> dicPropertiesAttribute = new Dictionary<string, Dictionary<string, DatabaseAttribute>>();
                        dicPropertiesAttribute.Add(classItem, GetClassPropertiesAttributeInfoList(assemblyItem, classItem));
                        lstDicPropertiesAttributes.Add(dicPropertiesAttribute);
                    }
                    //获取类的方法列表
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        Dictionary<string, List<string>> dicMethod = new Dictionary<string, List<string>>();
                        dicMethod.Add(classItem, GetClassMethodsInfoList(assemblyItem, classItem));
                        lstDicMethod.Add(dicMethod);
                    }
                    assemblyDictionaryResult.Properties = lstDicProperties;
                    assemblyDictionaryResult.PropertiesAttributes = lstDicPropertiesAttributes;
                    assemblyDictionaryResult.Methods = lstDicMethod;
                }
                assemblyDictionaryResult.ClassName = dicClass;
            }

            return assemblyDictionaryResult;
        }

        /// <summary>
        /// 获取类方法列表
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="filterWords">过滤取出的类命名空间</param>
        /// <param name="buildClassDateType">生成类的日期类型</param>
        /// <param name="buildDate">生成日期</param>
        /// <returns></returns>
        public AssemblyDictionaryResult GetAssemblyDictionaryResult(string assemblyName, string filterWords, BuildClassDateType buildClassDateType,string buildDate)
        {
            AssemblyDictionaryResult assemblyDictionaryResult = new AssemblyDictionaryResult();
            GetAssemblyNameList(assemblyName);
            assemblyDictionaryResult.AssemblyName = assemblyResult.AssemblyName;
            if (assemblyResult.AssemblyName.Count > 0)
            {
                Dictionary<string, Dictionary<string, DatabaseClassAttribute>> dicClass = new Dictionary<string, Dictionary<string, DatabaseClassAttribute>>();
                foreach (var assemblyItem in assemblyResult.AssemblyName)
                {
                    //将程序集和类字典
                    dicClass.Add(assemblyItem, GetClassNameDictionary(assemblyItem, filterWords,buildClassDateType,buildDate));
                    List<Dictionary<string, List<string>>> lstDicProperties = new List<Dictionary<string, List<string>>>();
                    List<Dictionary<string, Dictionary<string, DatabaseAttribute>>> lstDicPropertiesAttributes = new List<Dictionary<string, Dictionary<string, DatabaseAttribute>>>();
                    List<Dictionary<string, List<string>>> lstDicMethod = new List<Dictionary<string, List<string>>>();
                    //获取类的属性列表
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        Dictionary<string, List<string>> dicProperties = new Dictionary<string, List<string>>();
                        dicProperties.Add(classItem, GetClassPropertiesInfoList(assemblyItem, classItem));
                        lstDicProperties.Add(dicProperties);
                    }
                    //获取类的属性注解
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        Dictionary<string, Dictionary<string, DatabaseAttribute>> dicPropertiesAttribute = new Dictionary<string, Dictionary<string, DatabaseAttribute>>();
                        dicPropertiesAttribute.Add(classItem, GetClassPropertiesAttributeInfoList(assemblyItem, classItem));
                        lstDicPropertiesAttributes.Add(dicPropertiesAttribute);
                    }
                    //获取类的方法列表
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        Dictionary<string, List<string>> dicMethod = new Dictionary<string, List<string>>();
                        dicMethod.Add(classItem, GetClassMethodsInfoList(assemblyItem, classItem));
                        lstDicMethod.Add(dicMethod);
                    }
                    assemblyDictionaryResult.Properties = lstDicProperties;
                    assemblyDictionaryResult.PropertiesAttributes = lstDicPropertiesAttributes;
                    assemblyDictionaryResult.Methods = lstDicMethod;
                }
                assemblyDictionaryResult.ClassName = dicClass;
            }

            return assemblyDictionaryResult;
        }

        /// <summary>
        /// PropertyInfo 获取实体类的所有属性和值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string GetProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1},", name, value);
                }
                else
                {
                    GetProperties(value);
                }
            }
            return tStr;
        }

        /// <summary>
        /// PropertyInfo 获取实体类指定属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string GetPropertyValue<T>(T t, string field)
        {
            string value = "9";
            if (t == null)
            {
                return value;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return value;
            }
            var property = properties.Where(x => x.Name == field).FirstOrDefault();
            var property_value = property.GetValue(t, null);
            if(property_value != null)
            {
                value = property_value.ToString();
            }
            else
            {
                value = string.Empty;
            }
            return value;
        }

        /// <summary>
        /// PropertyInfo 获取实体类指定属性类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string GetPropertyType<T>(T t, string field)
        {
            string value = "9";
            if (t == null)
            {
                return value;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return value;
            }
            var property = properties.Where(x => x.Name == field).FirstOrDefault();
            value = property.GetType().Name;
            return value;
        }

        /// <summary>
        /// PropertyInfo 获取实体类指定属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string ConvertObject<T>(T t, string field)
        {
            string value = "9";
            if (t == null)
            {
                return value;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return value;
            }
            var property = properties.Where(x => x.Name == field).FirstOrDefault();
            var property_value = property.GetValue(t, null);
            if (property_value != null)
            {
                value = property_value.ToString();
            }
            else
            {
                value = string.Empty;
            }
            return value;
        }

        public static List<Object> ConvertTableToObject(DataTable data)
        {
            var objectList = new List<Object>();
            foreach (DataRow row in data.Rows)
            {
                dynamic dataItem = new ExpandoObject();
                var dataItemDic = dataItem as IDictionary<string, object>;
                foreach (DataColumn col in data.Columns)
                {
                    var name = col.ColumnName;
                    var val = row[name].ToString();
                    dataItemDic[name] = val;
                }
                objectList.Add(dataItem);
            }

            return objectList;
        }
    }
}
