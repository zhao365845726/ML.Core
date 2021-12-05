using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core.Assemblies;
using System;
using System.Collections.Generic;
using System.Text;
using ML.Core.Enum;

namespace ML.Core.Assemblies.Tests
{
    [TestClass()]
    public class AssemblyHandlerTests
    {
        [TestMethod()]
        public void GetAssemblyDictionaryResultTest()
        {
            string path = @"F:\local_dll\P_NetConfChina\netstandard2.1";
            string assemblyName = "P-NetConfChina";
            string filterWords = "P_NetConfChina.DatabaseModel";

            AssemblyHandler assemblyHandler = new AssemblyHandler(path);
            var result = assemblyHandler.GetAssemblyDictionaryResult(assemblyName, filterWords,BuildClassDateType.DAY,"2021-12-05");
            Console.WriteLine(result);
        }
    }
}