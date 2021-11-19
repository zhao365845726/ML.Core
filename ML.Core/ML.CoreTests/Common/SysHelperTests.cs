using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Tests
{
    [TestClass()]
    public class SysHelperTests
    {
        [TestMethod()]
        public void GetMachineNameTest()
        {
            string result = SysHelper.GetMachineName();
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void GetCommandLineTest()
        {
            string result = SysHelper.GetCommandLine();
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void GetCurrentDirectoryTest()
        {
            string result = SysHelper.GetCurrentDirectory();
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void GetCustomDirectoryTest()
        {
            string result = SysHelper.GetCustomDirectory("Path");
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void GetCurrentManagedThreadIdTest()
        {
            int result = SysHelper.GetCurrentManagedThreadId();
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void GetCustomDirectoryTest1()
        {
            var result = SysHelper.GetCustomDirectory("Path",";");
            foreach(var item in result)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine(result.ToJson());
        }
    }
}