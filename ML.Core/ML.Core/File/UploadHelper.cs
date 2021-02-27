//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;

namespace ML.Core
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    public class UploadHelper
    {
        /// <summary>
        /// 附件上传 成功：succeed、失败：error、文件太大：-1、
        /// </summary>
        /// <param name="file">单独文件的访问</param>
        /// <param name="path">存储路径</param>
        /// <param name="filename">输出文件名</param>
        /// <returns></returns>
        public static string FileUpload(HttpPostedFileBase file, string path, string FileName)
        {
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }
            //取得文件的扩展名,并转换成小写
            string Extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            //取得文件大小
            string filesize = SizeHelper.CountSize(file.ContentLength);
            try
            {
                int Size = file.ContentLength / 1024 / 1024;
                if (Size > 10)
                {
                    return "-1";
                }
                else
                {
                    file.SaveAs(path + FileName);
                    return "succeed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        ///// <summary>
        ///// 上传文件
        ///// </summary>
        ///// <param name="fileinfo">需要上传的文件</param>
        ///// <param name="targetDir">目标路径</param>
        ///// <param name="hostname">ftp地址</param>
        ///// <param name="username">ftp用户名</param>
        ///// <param name="password">ftp密码</param>
        //public void UploadFile(FileInfo fileinfo, string targetDir, string hostname, string username, string password, string Urlstr)
        //{
        //  //1. check target
        //  //target = Guid.NewGuid().ToString();  //使用临时文件名
        //  string URI = Urlstr;
        //  ///WebClient webcl = new WebClient();
        //  System.Net.FtpWebRequest ftp = GetRequest(URI, username, password);

        //  //设置FTP命令 设置所要执行的FTP命令，
        //  //ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectoryDetails;//假设此处为显示指定路径下的文件列表
        //  ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
        //  //指定文件传输的数据类型
        //  ftp.UseBinary = true;
        //  ftp.UsePassive = true;

        //  //告诉ftp文件大小
        //  ftp.ContentLength = fileinfo.Length;
        //  //缓冲大小设置为2KB
        //  const int BufferSize = 2048;
        //  byte[] content = new byte[BufferSize - 1 + 1];
        //  int dataRead;

        //  //打开一个文件流 (System.IO.FileStream) 去读上传的文件
        //  using (FileStream fs = fileinfo.OpenRead())
        //  {
        //    try
        //    {
        //      //把上传的文件写入流
        //      using (Stream rs = ftp.GetRequestStream())
        //      {
        //        do
        //        {
        //          //每次读文件流的2KB
        //          dataRead = fs.Read(content, 0, BufferSize);
        //          rs.Write(content, 0, dataRead);
        //        } while (!(dataRead < BufferSize));
        //        rs.Close();
        //      }

        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //      fs.Close();
        //    }
        //    //SendOk = true;
        //    //listBox1.Items.Add("同步完成!" + fileinfo.Name + "---" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    //SendMessage(listBox1.Handle, WM_VSCROLL, SB_BOTTOM, 0);
        //  }
        //  ftp = null;
        //}
    }
}
