using ML.Core.Assemblies;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GetAssemblyDictionaryResult();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        public static void GetAssemblyDictionaryResult()
        {
            //string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"/nuget/";
            ////string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //string path = @"F:\local_dll";
            string path = @"F:\local_dll\P_NetConfChina\netstandard2.1";
            Console.WriteLine(path);
            AssemblyHandler assemblyHandler = new AssemblyHandler(path);
            var aa = assemblyHandler.GetAssemblyDictionaryResult("P-NetConfChina", "P_NetConfChina.DatabaseModel");
        }
    }
}
