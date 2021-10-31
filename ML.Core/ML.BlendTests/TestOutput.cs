using Senparc.CO2NET.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.BlendTests
{
    public class TestOutput<T>
    {
        public static void Write(T t)
        {
            Console.WriteLine(t.ToJson());
        }
    }
}
