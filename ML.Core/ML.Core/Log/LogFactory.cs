//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ML.Core
{
    /// <summary>
    /// Log4Net日志 工厂
    /// 版本：2.0
    /// <author>
    ///		<name>MartyZane</name>
    ///		<date>2014.03.03</date>
    /// </author>
    /// </summary>
    public class LogFactory
    {
        static LogFactory()
        {
            FileInfo configFile = new FileInfo(HttpContext.Current.Server.MapPath("/XmlConfig/log4net.config"));

            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        public static LogHelper GetLogger(Type type)
        {
            return new LogHelper(LogManager.GetLogger(type));
        }

        public static LogHelper GetLogger(string str)
        {
            return new LogHelper(LogManager.GetLogger(str));
        }
    }
}
