using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
using ML.CoreTests.Dto;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace ML.Core.Tests
{
    [TestClass()]
    public class JsonHelperTests
    {
        [TestMethod()]
        public void JsonToEntityTest()
        {
            List<Student> list = new List<Student>();
            for (int i=1;i< 3;i++)
            {
                Student stu = new Student()
                {
                    Id = i,
                    Name = "Test",
                    Description = "Test",
                    Age = 18 + i
                };
                list.Add(stu); 
            }

            string result = JsonHelper.ToJson(list[0]);

            Console.WriteLine(result);

            var aa = JsonHelper.JsonToXml(result);


            //string Xml = result;
            //XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(Xml), XmlDictionaryReaderQuotas.Max);
            //XmlDocument doc = new XmlDocument();
            //doc.Load(reader);
            //Xml = doc.InnerXml;

            Console.WriteLine(aa.InnerXml);
        }
    }
}