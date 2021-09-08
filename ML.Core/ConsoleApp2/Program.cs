using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        public static void GetAssemblyDictionaryResult()
        {
            //string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"/nuget/";
            ////string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //string path = @"F:\local_dll";
            //string path = @"F:\local_dll\P_NetConfChina\netstandard2.1";
            //Console.WriteLine(path);
            //AssemblyHandler assemblyHandler = new AssemblyHandler(path);
            //var aa = assemblyHandler.GetAssemblyDictionaryResult("P-NetConfChina", "P_NetConfChina.DatabaseModel");
        }

        //static void TestCopyFolder()
        //{
        //    string rootPath = @"E:\_GitHub.Contribution\NCF\src\Senparc.Web\wwwroot\NcfDocs";
        //    string sourcePath = $"{rootPath}\\branch\\v1.0";
        //    string destPath = $"{rootPath}\\v1.0";
        //    Dictionary<string, string> dicExclude = new Dictionary<string, string>();
        //    dicExclude.Add("FOLDER", ".git");
        //    DirFileHelper.CopyFolder(sourcePath, destPath, dicExclude);
        //}

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
