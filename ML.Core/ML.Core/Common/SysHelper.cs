using System;
using System.Management;
using System.Web;
using System.Threading;
using System.Diagnostics;

namespace ML.Core.Common
{
  /// <summary>
  /// 系统操作相关的公共类
  /// </summary>    
  public static class SysHelper
  {
    #region 获取文件相对路径映射的物理路径
    /// <summary>
    /// 获取文件相对路径映射的物理路径
    /// </summary>
    /// <param name="virtualPath">文件的相对路径</param>        
    public static string GetPath(string virtualPath)
    {

      return HttpContext.Current.Server.MapPath(virtualPath);

    }
    #endregion

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

    /// <summary>  
    /// 获取本机的MAC地址  
    /// </summary>  
    /// <returns></returns>  
    public static string GetLocalMac()
    {
      string mac = null;
      ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
      ManagementObjectCollection queryCollection = query.Get();
      foreach (ManagementObject mo in queryCollection)
      {
        if (mo["IPEnabled"].ToString() == "True")
          mac = mo["MacAddress"].ToString();
      }
      return (mac);
    }

    /// <summary>
    /// 取得设备硬盘的卷标号
    /// </summary>
    /// <returns></returns>
    public static string GetDiskVolumeSerialNumber()
    {
      ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
      ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
      disk.Get();
      return disk.GetPropertyValue("VolumeSerialNumber").ToString();
    }

    /// <summary>
    /// 获得CPU的序列号
    /// </summary>
    /// <returns></returns>
    public static string GetCPU()
    {
      string strCpu = null;
      ManagementClass myCpu = new ManagementClass("win32_Processor");
      ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
      foreach (ManagementObject myObject in myCpuConnection)
      {
        strCpu = myObject.Properties["Processorid"].Value.ToString();
        break;
      }
      return strCpu;
    }

    #endregion

    #region 电脑帮助类
    /// <summary>
    /// 获取CPU序列号
    /// </summary>
    /// <returns></returns>
    public static string GetCpuID()
    {
      try
      {
        //获取CPU序列号代码
        string cpuInfo = "";//cpu序列号
        ManagementClass mc = new ManagementClass("Win32_Processor");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {
          cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
        }
        moc = null;
        mc = null;
        return cpuInfo;
      }
      catch
      {
        return "unknow";
      }
      finally
      {
      }

    }

    /// <summary>
    /// 获取网卡MacAddress
    /// </summary>
    /// <returns></returns>
    public static string GetMacAddress()
    {
      try
      {
        //获取网卡硬件地址
        string mac = "";
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {
          if ((bool)mo["IPEnabled"] == true)
          {
            mac = mo["MacAddress"].ToString();
            break;
          }
        }
        moc = null;
        mc = null;
        return mac;
      }
      catch
      {
        return "unknow";
      }
      finally
      {
      }

    }

    public static string GetIPAddress()
    {
      try
      {
        //获取IP地址
        string st = "";
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {
          if ((bool)mo["IPEnabled"] == true)
          {
            //st=mo["IpAddress"].ToString();
            System.Array ar;
            ar = (System.Array)(mo.Properties["IpAddress"].Value);
            st = ar.GetValue(0).ToString();
            break;
          }
        }
        moc = null;
        mc = null;
        return st;
      }
      catch
      {
        return "unknow";
      }
      finally
      {
      }

    }

    /// <summary>
    /// 获取硬盘Id
    /// </summary>
    /// <returns></returns>
    public static string GetDiskID()
    {
      try
      {
        //获取硬盘ID
        String HDid = "";
        ManagementClass mc = new ManagementClass("Win32_DiskDrive");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {
          HDid = (string)mo.Properties["Model"].Value;
        }
        moc = null;
        mc = null;
        return HDid;
      }
      catch
      {
        return "unknow";
      }
      finally
      {
      }

    }

    /// <summary>
    /// 操作系统的登录用户名
    /// </summary>
    /// <returns></returns>
    public static string GetUserName()
    {
      try
      {
        string st = "";
        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {

          st = mo["UserName"].ToString();

        }
        moc = null;
        mc = null;
        return st;
      }
      catch
      {
        return "unknow";
      }
      finally
      {
      }

    }


    /// <summary>
    /// PC类型
    /// </summary>
    /// <returns></returns>
    public static string GetSystemType()
    {
      try
      {
        string st = "";
        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {

          st = mo["SystemType"].ToString();

        }
        moc = null;
        mc = null;
        return st;
      }
      catch
      {
        return "unknow";
      }
      finally
      {
      }

    }

    /// <summary>
    /// 物理内存
    /// </summary>
    /// <returns></returns>
    public static string GetTotalPhysicalMemory()
    {
      try
      {

        string st = "";
        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {

          st = mo["TotalPhysicalMemory"].ToString();

        }
        moc = null;
        mc = null;
        return st;
      }
      catch
      {
        return "unknow";
      }
      finally
      {
      }
    }
    //   /// <summary>
    //   /// 获取本机机器名
    //   /// </summary>
    //   /// <returns></returns>
    //   public static string GetComputerName()
    //   {
    //     try
    //     {
    //       return System.Environment.GetEnvironmentVariable("ComputerName");
    //     }
    //     catch
    //     {
    //       return "unknow";
    //     }
    //     finally
    //     {
    //     }
    //   }

    #endregion
  }
}
