﻿//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
//using System.Management;
using System.Web;
using System.Threading;
using System.Diagnostics;

namespace ML.Core
{
    /// <summary>
    /// 系统操作相关的公共类
    /// </summary>    
    public static class SysHelper
    {
        #region 获取指定调用层级的方法名
        /// <summary>
        /// 获取指定调用层级的方法名
        /// </summary>
        /// <param name="level">调用的层数</param>        
        public static string GetMethodName(int level)
        {
            //创建一个堆栈跟踪
            StackTrace trace = new StackTrace();

            //获取指定调用层级的方法名
            return trace.GetFrame(level).GetMethod().Name;
        }
        #endregion

        #region 获取换行字符
        /// <summary>
        /// 获取换行字符
        /// </summary>
        public static string NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }
        #endregion

        #region 获取当前应用程序域
        /// <summary>
        /// 获取当前应用程序域
        /// </summary>
        public static AppDomain CurrentAppDomain
        {
            get
            {
                return Thread.GetDomain();
            }
        }
        #endregion

        #region 获取计算机基本信息
        /// <summary>  
        /// 获取本机机器名   
        /// </summary>  
        /// <returns></returns>  
        public static string GetMachineName()
        {
            return Environment.GetEnvironmentVariable("COMPUTERNAME");
        }
        #endregion
    }
}
