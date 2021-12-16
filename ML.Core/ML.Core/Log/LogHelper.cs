////=====================================================================================
//// All Rights Reserved , Copyright © MLTechnology 2017-Now
////=====================================================================================
//using log4net;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;

//namespace ML.Core
//{
//    /// <summary>
//    /// Log4Net日志类
//    /// 版本：2.0
//    /// <author>
//    ///		<name>MartyZane</name>
//    ///		<date>2014.03.03</date>
//    /// </author>
//    /// </summary>
//    public class LogHelper
//    {
//        //private ILog logger;

//        //public LogHelper(ILog log)
//        //{
//        //    this.logger = log;
//        //}
//        //public void Debug(object message)
//        //{
//        //    this.logger.Debug(message);
//        //}
//        //public void Debug(object message, Exception e)
//        //{
//        //    this.logger.Debug(message, e);
//        //}
//        //public void Error(object message)
//        //{
//        //    this.logger.Error(message);
//        //}
//        //public void Error(object message, Exception e)
//        //{
//        //    this.logger.Error(message, e);
//        //}

//        private string _logFileLocation = @"C:\temp\servicelog.txt";
//        public LogHelper(string logFilePath)
//        {
//            this._logFileLocation = logFilePath;
//        }

//        public void Log(string logMessage)
//        {
//            Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
//            File.AppendAllText(_logFileLocation,
//                DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
//        }
//    }
//}
