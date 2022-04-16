using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Tests
{
    [TestClass()]
    public class DirFileHelperTests
    {
        [TestMethod()]
        public void CreateDirectoryTest()
        {
            string strRootPath = @"C:\Code\90-Laboratory\WorkSpace";
            DirFileHelper.CreateDirectory($"{strRootPath}\\a");
        }
    }
}