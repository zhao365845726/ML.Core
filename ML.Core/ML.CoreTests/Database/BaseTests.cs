﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Database.Tests
{
    [TestClass()]
    public class BaseTests
    {
        [TestMethod()]
        public void BaseTest()
        {
            Base _base = new Base();
            _base.Select("id,name,gender");
        }
    }
}