using System;
using ML.Core;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCopyFolder();
            Console.ReadLine();
        }

        static void TestCopyFolder()
        {
            string rootPath = @"E:\_GitHub.Contribution\NCF\src\Senparc.Web\wwwroot\NcfDocs";
            string sourcePath = $"{rootPath}\\branch\\v1.0";
            string destPath = $"{rootPath}\\v1.0";
            Dictionary<string, string> dicExclude = new Dictionary<string, string>();
            dicExclude.Add("FOLDER", ".git");
            DirFileHelper.CopyFolder(sourcePath, destPath, dicExclude);
        }

        //static void TestGetLogicalDrives()
        //{
        //    string strInfo = string.Empty;
        //    string[] saDrives = Directory.GetLogicalDrives();
        //    foreach (string item in saDrives)
        //    {
        //        WriteLog(item);
        //    }
        //}

        static void WriteLog(string strInfo)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine($"{strInfo}");
            Console.WriteLine($"");
            
        }
    }
}
