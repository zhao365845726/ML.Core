using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Tests
{
    [TestClass()]
    public class HttpHelperTests
    {
        [TestMethod()]
        public void GetClientIPAddressTest()
        {
            var result = HttpHelper.GetClientIPAddress();
            Console.Write($"Hello GetClientIPAddressTest Result is : {result}");
        }

        [TestMethod()]
        public void HttpPostDataTest()
        {
            RandomHelper rh = new RandomHelper();
            string id = "ozzdgc2gkxf6o";
            string captcha = string.Empty;
            string network = "wifi";
            string dx_token = rh.GenerateRandomNumber(40);
            string dx_captcha_token = string.Empty;
            string hx_info_vid = "77ummtv3d3gmg";
            //string hx_info_uid = "oFKxuw_IT6p169KFNmTO5Bxbe8f4";
            string hx_info_uid = rh.GenerateRandomNumber(28);
            string hx_info_wnw = "0";
            string hx_info_time = DateTimeHelper.ConvertToUnix(DateTime.Now).ToString();
            string hx_info_token = rh.GenerateRandomNumber(32);
            string param = $"data[id][]={id}&data[captcha]={captcha}&data[network]={network}&data[dx_token]={dx_token}&data[dx_captcha_token]={dx_captcha_token}&data[hx_info][vid]={hx_info_vid}&data[hx_info][uid]={hx_info_uid}&data[hx_info][wnw]={hx_info_wnw}&data[hx_info][time]={hx_info_time}&data[hx_info][token]={hx_info_token}";
            var result = HttpHelper.HttpPostData("https://77ummtv3d3gmg.v.jisutp.com/sendVote", param);
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void GetTest()
        {
            //var result = HttpHelper.Get("https://localhost:44311/api/well/user/accountlogin?mobile=13111111111&password=123456");
            //Console.WriteLine(result);

            string param = $"https://localhost:44311/api/well/well/add?WellID=13&Owner=12&OwnerIdentityID=12&OwnerTel=12&WaterIntake=12&OrganizationID=12&Address=12&WateredLandArea=12&WateredLandAreaOfRight=12&WateredLandAreaOfNoRight=12&ConstructionPerson=12&Administrator=12&IsPeitao=false&EntryName=12&Latitude=12&Longitude=12&IrrigationCategory=12&CurrentSpeed=12&IrrigationArea=12&Diameter=12&BuildYear=12&Deep=12&Status=1&Creator=12";

            param = $"https://open.diwork.com/iuap-api-management/ucgManage/baseapi/api/getByVersionForTest/0f1453d26e6741faa95ace9533a61683/running?shareKey=&ts=1652886559287&isAjax=1";
            var result = HttpHelper.Get(param);
            Console.WriteLine(result);
        }

        [TestMethod("机电井:添加")]
        public void HttpPostDataTestWellAdd()
        {
            string param = $"WellID=13&Owner=12&OwnerIdentityID=12&OwnerTel=12&WaterIntake=12&OrganizationID=12&Address=12&WateredLandArea=12&WateredLandAreaOfRight=12&WateredLandAreaOfNoRight=12&ConstructionPerson=12&Administrator=12&IsPeitao=false&EntryName=12&Latitude=12&Longitude=12&IrrigationCategory=12&CurrentSpeed=12&IrrigationArea=12&Diameter=12&BuildYear=12&Deep=12&Status=1&Creator=12";
            var result = HttpHelper.HttpPostData("https://localhost:44311/api/well/well/add", param);
            Console.WriteLine(result);
        }

        [TestMethod("新美光:采购入库列表查询")]
        public void HttpPostDataTestNewMeguiar()
        {
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara.Add("pageIndex", "1");
            dicPara.Add("pageSize", "10");
            dicPara.Add("isSum", "false");
            string param = JsonConvert.SerializeObject(dicPara);
            string baseUrl = "https://api.diwork.com/yonbip/scm/purinrecord/list?access_token=f0670039271f48e0a1e9672ecae51227";
            var result = HttpHelper.HttpPostData<Object>(baseUrl, null, null, param);
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void GetWebCodeTest()
        {
            string strUrl = "https://open.diwork.com/#/doc-center/docDes/api?code=yonbip&section=0d0c4b299b4644b4916f12a86744147d";
            var result = HttpHelper.GetWebCode(strUrl, Encoding.UTF8);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [TestMethod()]
        public void GetWebClientTest()
        {
            string strUrl = "https://open.diwork.com/#/doc-center/docDes/api?code=yonbip&section=0d0c4b299b4644b4916f12a86744147d";
            var result = HttpHelper.GetWebClient(strUrl);
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [TestMethod()]
        public void GetTest1()
        {
            string param = $"https://open.diwork.com/iuap-api-management/ucgManage/baseapi/api/getByVersionForTest/0f1453d26e6741faa95ace9533a61683/running?shareKey=&ts=1652886559287&isAjax=1";
            var result = HttpHelper.Get(param);
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void HttpPostDataTest1()
        {
            string param = $"{{\"WarehouseId\":6,\"POReference\":\"PO-0764-2(Yonyou Test)\",\"Supplier\":\"Bellissimo Company Limited\",\"EstimatedDelivery\":\"2022-6-8 15:25:22\",\"Comments\":\"PO2228（S22-05-27)(Yonyou Test)\",\"GoodsInType\":\"Carton\",\"Quantity\":30,\"ProductSupplierId\":11,\"ClientId\":3,\"Items\":[{{\"ProductId\":2422,\"SKU\":\"10\",\"Quantity\":20}}]}}";
            var result = HttpHelper.HttpPostData($"https://api.mintsoft.co.uk/api/ASN?APIKey=d0a925f7-9d97-49fb-b55e-efac39cd6ace", param, "application/json");
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void HttpPutJsonStringTest()
        {
            string param = $"{{\"WarehouseId\":6,\"POReference\":\"PO-0764-2(Yonyou Test)\",\"Supplier\":\"Bellissimo Company Limited\",\"EstimatedDelivery\":\"2022-6-8 15:25:22\",\"Comments\":\"PO2228（S22-05-27)(Yonyou Test)\",\"GoodsInType\":\"Carton\",\"Quantity\":30,\"ProductSupplierId\":11,\"ClientId\":3,\"Items\":[{{\"ProductId\":2422,\"SKU\":\"10\",\"Quantity\":20}}]}}";
            var result = HttpHelper.HttpPutJsonString($"https://api.mintsoft.co.uk/api/ASN?APIKey=d0a925f7-9d97-49fb-b55e-efac39cd6ace", param);
            Console.WriteLine(result);
        }
    }
}