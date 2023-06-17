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

            //string bb = @"{\"voucher\":{\"voucher_head\":{\"pk_vouchertype\":\"02\",\"pk_system\":\"GL\",\"voucherkind\":\"0\",\"discardflag\":\"N\",\"attachment\":\"0\",\"signflag\":\"Y\",\"pk_voucher\":\"CGRK20230131000010\",\"year\":\"2023\",\"pk_accountingbook\":\"0104-0001\",\"period\":\"1\",\"no\":\"\",\"prepareddate\":\"2023-01-31 00:00:00\",\"pk_prepared\":\"\",\"pk_casher\":\"\",\"pk_checked\":\"\",\"tallydate\":\"\",\"pk_manager\":\"\",\"reserve2\":\"N\",\"pk_org\":\"3119522072940800\",\"pk_org_v\":\"V1.0\",\"pk_group\":\"\",\"momo1\":\"\",\"momo2\":\"\",\"reserve1\":\"\",\"siscardflag\":\"\",\"details\":{\"item\":[{\"pk_unit\":\"0104\",\"pk_unit_v\":\"0104\",\"detailindex\":\"1\",\"explanation\":\"采购生成凭证\",\"verifydate\":\"\",\"price\":\"10.0\",\"excrate2\":\"1\",\"debitquantity\":\"10\",\"debitamount\":\"100\",\"localdebitamount\":\"100\",\"groupdebitamount\":\"\",\"globaldebitamount\":\"\",\"pk_currtype\":\"人民币\",\"pk_accasoa\":\"140399\",\"ass\":{\"item\":[{\"pk_Checktype\":\"\",\"pk_Checkvalue\":\"\",\"checktypename\":null,\"checkvaluecode\":null}]},\"vatdetail\":null,\"cashFlow\":null,\"creditquantity\":null,\"creditamount\":null,\"localcreditamount\":null,\"groupcreditamount\":null,\"globalcreditamount\":null},{\"pk_unit\":\"0104\",\"pk_unit_v\":\"0104\",\"detailindex\":\"2\",\"explanation\":\"取备用金\",\"verifydate\":\"\",\"price\":\"\",\"excrate2\":\"\",\"debitquantity\":null,\"debitamount\":null,\"localdebitamount\":null,\"groupdebitamount\":null,\"globaldebitamount\":null,\"pk_currtype\":\"人民币\",\"pk_accasoa\":\"140399\",\"ass\":{\"item\":[{\"pk_Checktype\":null,\"pk_Checkvalue\":null,\"checktypename\":\"海南三合优品物流有限公司\",\"checkvaluecode\":\"1637534175264768031\"}]},\"vatdetail\":null,\"cashFlow\":null,\"creditquantity\":\"10\",\"creditamount\":\"100\",\"localcreditamount\":\"100\",\"groupcreditamount\":\"\",\"globalcreditamount\":\"\"}]}}}}";
        }
    }
}