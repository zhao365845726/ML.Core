using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Tests
{
    [TestClass()]
    public class CommonHelperTests
    {
        [TestMethod()]
        public void DividedTest()
        {
            int x = 10;
            int y = 3;

            var lstRes = CommonHelper.Divided(x, y);
            for (int i = 0; i < lstRes.Count; i++)
            {
                Console.WriteLine(lstRes[i].ToString());
            }
        }
    }
}