//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
//using System.Management;
using System.Web;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

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

        public static string GetCommandLine()
        {
            return Environment.CommandLine;
        }

        public static string GetCurrentDirectory()
        {
            return Environment.CurrentDirectory;
        }

        public static string GetCustomDirectory(string env)
        {
            // Change the directory to %WINDIR%
            return Environment.GetEnvironmentVariable(env);
        }

        //public static List<string> GetCustomDirectory(string env,string separator)
        //{
        //    List<string> lstRes = new List<string>();
        //    // Change the directory to %WINDIR%
        //    var data = Environment.GetEnvironmentVariable(env);
        //    if (data.Contains(separator))
        //    {
        //        lstRes = data.Split(separator).ToList();
        //    }
        //    return lstRes;
        //}

        public static int GetCurrentManagedThreadId()
        {
            return Environment.CurrentManagedThreadId;
        }

        public static bool GetHasShutdownStarted()
        {
            return Environment.HasShutdownStarted;
        }

        public static bool GetIs64BitOperatingSystem()
        {
            return Environment.Is64BitOperatingSystem;
        }

        public static bool GetIs64BitProcess()
        {
            return Environment.Is64BitProcess;
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }

        public static string GetNewLine()
        {
            return Environment.NewLine;
        }

        //public static string GetOSVersion()
        //{
        //    return Environment.OSVersion;
        //}

        //public static string GetMachineName()
        //{
        //    return Environment.MachineName;
        //}

        //public static string GetMachineName()
        //{
        //    return Environment.MachineName;
        //}

        //public static string GetMachineName()
        //{
        //    return Environment.MachineName;
        //}

        public static List<string> GetMacAddress(string separator = "-")
        {
            //IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            //Console.WriteLine("Interface information for {0}.{1}     ",
            //    computerProperties.HostName, computerProperties.DomainName);
            if (nics == null || nics.Length < 1)
            {
                Console.WriteLine("  No network interfaces found.");
                return new List<string>();
            }

            Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
            var macAddress = new List<string>();

            foreach (NetworkInterface adapter in nics.Where(c =>
                c.NetworkInterfaceType != NetworkInterfaceType.Loopback && c.OperationalStatus == OperationalStatus.Up))
            {
                //Console.WriteLine();
                //Console.WriteLine(adapter.Name + "," + adapter.Description);
                //Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                //Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                //Console.Write("  Physical address ........................ : ");
                //PhysicalAddress address = adapter.GetPhysicalAddress();
                //byte[] bytes = address.GetAddressBytes();
                //for (int i = 0; i < bytes.Length; i++)
                //{
                //    // Display the physical address in hexadecimal.
                //    Console.Write("{0}", bytes[i].ToString("X2"));
                //    // Insert a hyphen after each byte, unless we are at the end of the 
                //    // address.
                //    if (i != bytes.Length - 1)
                //    {
                //        Console.Write("-");
                //    }
                //}

                //Console.WriteLine();

                IPInterfaceProperties properties = adapter.GetIPProperties();

                var unicastAddresses = properties.UnicastAddresses;
                if (unicastAddresses.Any(temp => temp.Address.AddressFamily == AddressFamily.InterNetwork))
                {
                    var address = adapter.GetPhysicalAddress();
                    if (string.IsNullOrEmpty(separator))
                    {
                        macAddress.Add(address.ToString());
                    }
                    else
                    {
                        macAddress.Add(string.Join(separator, address.GetAddressBytes()));
                    }
                }
            }

            return macAddress;
        }

        public static Dictionary<string, string> GetMacAddressDic(string separator = "-")
        {
            //IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            //Console.WriteLine("Interface information for {0}.{1}     ",
            //    computerProperties.HostName, computerProperties.DomainName);
            if (nics == null || nics.Length < 1)
            {
                Console.WriteLine("  No network interfaces found.");
                return null;
            }

            Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
            Dictionary<string, string> macAddress = new Dictionary<string, string>();

            foreach (NetworkInterface adapter in nics.Where(c =>
                c.NetworkInterfaceType != NetworkInterfaceType.Loopback && c.OperationalStatus == OperationalStatus.Up))
            {
                //Console.WriteLine();
                //Console.WriteLine(adapter.Name + "," + adapter.Description);
                //Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                //Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                //Console.Write("  Physical address ........................ : ");
                //PhysicalAddress address = adapter.GetPhysicalAddress();
                //byte[] bytes = address.GetAddressBytes();
                //for (int i = 0; i < bytes.Length; i++)
                //{
                //    // Display the physical address in hexadecimal.
                //    Console.Write("{0}", bytes[i].ToString("X2"));
                //    // Insert a hyphen after each byte, unless we are at the end of the 
                //    // address.
                //    if (i != bytes.Length - 1)
                //    {
                //        Console.Write("-");
                //    }
                //}

                //Console.WriteLine();

                IPInterfaceProperties properties = adapter.GetIPProperties();

                var unicastAddresses = properties.UnicastAddresses;
                if (unicastAddresses.Any(temp => temp.Address.AddressFamily == AddressFamily.InterNetwork))
                {

                    var address = adapter.GetPhysicalAddress();
                    var name = adapter.Name;
                    if (string.IsNullOrEmpty(separator))
                    {
                        macAddress.Add(name, address.ToString());
                    }
                    else
                    {
                        macAddress.Add(name, string.Join(separator, address.GetAddressBytes()));
                    }
                }
            }

            return macAddress;
        }
        #endregion
    }
}
