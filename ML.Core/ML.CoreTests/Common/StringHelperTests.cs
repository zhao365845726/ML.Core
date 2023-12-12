using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Tests
{
    [TestClass()]
    public class StringHelperTests
    {
        [TestMethod()]
        public void FindStrTest()
        {
            string strDemo = "(13)+(14)";
            string result = StringHelper.FindStr(strDemo);
            Console.WriteLine(result);
        }
    }
}