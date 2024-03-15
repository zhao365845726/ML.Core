using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Tests
{
    [TestClass()]
    public class DateTimeHelperTests
    {
        [TestMethod()]
        public void TimeSpanTest()
        {
            Console.WriteLine("hello");
        }

        [TestMethod()]
        public void GetLastMonthTest()
        {
            string res = DateTimeHelper.GetLastMonth();
            Console.WriteLine(res);
        }

        [TestMethod()]
        public void GetLastMonthTest1()
        {
            string res = DateTimeHelper.GetLastMonth("2024-01");
            Console.WriteLine(res);
        }
    }
}