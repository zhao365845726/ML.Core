using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core.Assemblies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Assemblies.Tests
{
    [TestClass()]
    public class AssemblyHandlerTests
    {
        [TestMethod()]
        public void GetAssemblyDictionaryResultTest()
        {
            string path = @"F:\local_dll\P-ElectromechanicalWell\netstandard2.1";
            string assemblyName = "P-ElectromechanicalWell";
            string filterWords = "P_ElectromechanicalWell.DatabaseModel";

            AssemblyHandler assemblyHandler = new AssemblyHandler(path);
            assemblyHandler.GetAssemblyDictionaryResult(assemblyName, filterWords);
        }
    }
}