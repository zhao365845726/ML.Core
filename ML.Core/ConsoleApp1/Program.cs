using System;
using ML.Core;
using System.Collections.Generic;
using ML.Core.Assemblies;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAssemblyDictionaryResult();
            //TestCopyFolder();
            Console.ReadLine();
        }

        public static void GetAssemblyDictionaryResult()
        {
            //string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"/nuget/";
            ////string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            string path = @"F:\local_dll";
            Console.WriteLine(path);
            AssemblyHandler assemblyHandler = new AssemblyHandler(path);
            var aa = assemblyHandler.GetAssemblyDictionaryResult("P-AutoMate_TaskScheduler", "P_AutoMate_TaskScheduler.DatabaseModel");
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
