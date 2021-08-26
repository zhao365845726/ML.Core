using System;
using System.Collections.Generic;
using System.IO;
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
            //GetClassNameList(assemblyName, filterWords);
            //GetClassPropertiesInfoList(assemblyName, className);
            //GetClassMethodsInfoList(assemblyName, className);


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
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public AssemblyDictionaryResult GetAssemblyDictionaryResult(string assemblyName, string filterWords)
        {
            AssemblyDictionaryResult assemblyDictionaryResult = new AssemblyDictionaryResult();
            GetAssemblyNameList(assemblyName);
            assemblyDictionaryResult.AssemblyName = assemblyResult.AssemblyName;
            if (assemblyResult.AssemblyName.Count > 0)
            {
                Dictionary<string, List<string>> dicClass = new Dictionary<string, List<string>>();
                foreach (var assemblyItem in assemblyResult.AssemblyName)
                {
                    //将程序集和类列表
                    dicClass.Add(assemblyItem, GetClassNameList(assemblyItem, filterWords));
                    List<Dictionary<string, List<string>>> lstDicProperties = new List<Dictionary<string, List<string>>>();
                    List<Dictionary<string, List<string>>> lstDicMethod = new List<Dictionary<string, List<string>>>();
                    foreach (var classItem in assemblyResult.ClassName)
                    {
                        Dictionary<string, List<string>> dicProperties = new Dictionary<string, List<string>>();
                        Dictionary<string, List<string>> dicMethod = new Dictionary<string, List<string>>();
                        dicProperties.Add(classItem, GetClassPropertiesInfoList(assemblyItem, classItem));
                        dicProperties.Add(classItem, GetClassMethodsInfoList(assemblyItem, classItem));
                        lstDicProperties.Add(dicProperties);
                        lstDicMethod.Add(dicMethod);
                    }
                    assemblyDictionaryResult.Properties = lstDicProperties;
                    assemblyDictionaryResult.Methods = lstDicMethod;
                }
                assemblyDictionaryResult.ClassName = dicClass;
            }

            return assemblyDictionaryResult;
        }
    }
}
