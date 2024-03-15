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

        [TestMethod()]
        public void HttpGetAsyncTest()
        {
            string strTS = DateTimeHelper.ConvertToUnix(DateTime.Now).ToString();

            string requestUrl = "https://dbox.yonyoucloud.com/iuap-ipaas-base/openPortal/api/showApiList/yonsuite/productClassify";
            Dictionary<string, string> dicParam = new Dictionary<string, string>();
            dicParam.Add("shareKey", "");
            dicParam.Add("ytenantId", "");
            dicParam.Add("ts", strTS);
            dicParam.Add("type", "originalIsv");
            dicParam.Add("isAjax", "1");
            Dictionary<string, string> dicHeader = new Dictionary<string, string>();
            dicHeader.Add("Accept", "application/json, text/plain, */*");
            dicHeader.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            dicHeader.Add("Cache-Control", "no-cache");
            dicHeader.Add("Connection", "keep-alive");
            dicHeader.Add("Cookie", "_WorkbenchCross_=Ultraman; at=f4ebcbb5-f2df-4de9-9787-0406ba7a9e4c; HWWAFSESID=12703196f026b4a7b81; HWWAFSESTIME=1710347406507; JSESSIONID=87A95B48CAC0904872C444F3BFF9B500; yht_username_diwork=ST-3928837-BXWA5TmeFyFedgzd2D42-online__75fc5a66-d89f-463a-8377-a73add70fbe9; yht_usertoken_diwork=LDw0jVlSwa%2FkfRWGAxLx08H5baqRiDvhoZH4rePr8ZpbXNXasumH6EsH%2BUnGCvc4VNDeEI8ulzKgt%2FJKZXdeiA%3D%3D; yht_access_token=bttMHpDOFhtdGpwMkxGODQvM1RTYitPbTlJZEpucVp3ejdDcXp4NEdzYnYyKzJnTXJwUk5pQk1HVEtXODdPTWkxTl9fZXVjLnlvbnlvdWNsb3VkLmNvbQ..__cf4d8cf47ce522f496400cc1426eb5ae_1710347410204TGTGdccore2iuap-apcom-workbench5dd7e36eYT; multilingualFlag=false; timezone=UTC+08:00; language=001; locale=zh_CN; orgId=; defaultOrg=; tenantid=yfjz0nwh; theme=; languages=1_3; newArch=true; sysid=diwork; a00=KTF3IbQ_YFzlYMEsgF5U3ajMTAF1mggVtAUXcmtrEJ95Zmp6MG53aGAzMzkxMjI0NTc4ODA3NTY4YHlmanowbndoYDc1ZmM1YTY2LWQ4OWYtNDYzYS04Mzc3LWE3M2FkZDcwZmJlOWAxYGBlOGI1YjVlOTkzYWRlNTkzYjJgYGAxNzc3OTgwNDIyMjQ0Nzk0Mzc2YGZhbHNlYGAxNzEwMzQ3NDEwMjE0YHltc3NlczozODk0NDZjZmRiOWYyNmJiNDY1MzdlYTMzNTkyN2MyNGBkaXdvcmtg; a10=MDExMjY4MzU1NDA1MjM0MTAyMTQ; n_f_f=true; wb_at=LMjpvouuptj4QP3rMld8x8dcfyco6qojnmkhmd; c800=dccore2; jDiowrkTokenMock=bttMHpDOFhtdGpwMkxGODQvM1RTYitPbTlJZEpucVp3ejdDcXp4NEdzYnYyKzJnTXJwUk5pQk1HVEtXODdPTWkxTl9fZXVjLnlvbnlvdWNsb3VkLmNvbQ..__cf4d8cf47ce522f496400cc1426eb5ae_1710347410204TGTGdccore2iuap-apcom-workbench5dd7e36eYT; XSRF-TOKEN=AX_AOWSTDMSORIVNRWO0TCFCVJE1!003311; UBA_LAST_EID=dn07ltq0t840");
            dicHeader.Add("Referer", "https://dbox.yonyoucloud.com/");
            dicHeader.Add("Sec-Fetch-Dest", "empty");
            dicHeader.Add("Sec-Fetch-Mode", "cors");
            dicHeader.Add("Sec-Fetch-Site", "same-origin");
            dicHeader.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0");
            dicHeader.Add("X-Requested-With", "XMLHttpRequest");
            dicHeader.Add("from", "diwork");
            dicHeader.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Microsoft Edge\";v=\"120\"");
            dicHeader.Add("sec-ch-ua-mobile", "?0");
            dicHeader.Add("sec-ch-ua-platform", "\"Windows\"");
            dicHeader.Add("serviceCodeDiwork", "ipass0010");
            dicHeader.Add("ts", strTS);
            var result = HttpHelper.HttpGetAsync(requestUrl, dicParam, dicHeader);
            Console.WriteLine(result);
        }

        [TestMethod()]
        public void HttpGetAsyncTest1()
        {
            string strTS = DateTimeHelper.ConvertToUnix(DateTime.Now).ToString();

            string requestUrl = "https://dbox.yonyoucloud.com/iuap-ipaas-base/openPortal/api/showApiList/yonsuite/productClassify";
            Dictionary<string, string> dicParam = new Dictionary<string, string>();
            dicParam.Add("shareKey", "");
            dicParam.Add("ytenantId", "");
            dicParam.Add("ts", strTS);
            dicParam.Add("type", "originalIsv");
            dicParam.Add("isAjax", "1");
            Dictionary<string, string> dicHeader = new Dictionary<string, string>();
            dicHeader.Add("Accept", "application/json, text/plain, */*");
            dicHeader.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            dicHeader.Add("Cache-Control", "no-cache");
            dicHeader.Add("Connection", "keep-alive");
            dicHeader.Add("Cookie", "_WorkbenchCross_=Ultraman; at=f4ebcbb5-f2df-4de9-9787-0406ba7a9e4c; HWWAFSESID=12703196f026b4a7b81; HWWAFSESTIME=1710347406507; JSESSIONID=87A95B48CAC0904872C444F3BFF9B500; yht_username_diwork=ST-3928837-BXWA5TmeFyFedgzd2D42-online__75fc5a66-d89f-463a-8377-a73add70fbe9; yht_usertoken_diwork=LDw0jVlSwa%2FkfRWGAxLx08H5baqRiDvhoZH4rePr8ZpbXNXasumH6EsH%2BUnGCvc4VNDeEI8ulzKgt%2FJKZXdeiA%3D%3D; yht_access_token=bttMHpDOFhtdGpwMkxGODQvM1RTYitPbTlJZEpucVp3ejdDcXp4NEdzYnYyKzJnTXJwUk5pQk1HVEtXODdPTWkxTl9fZXVjLnlvbnlvdWNsb3VkLmNvbQ..__cf4d8cf47ce522f496400cc1426eb5ae_1710347410204TGTGdccore2iuap-apcom-workbench5dd7e36eYT; multilingualFlag=false; timezone=UTC+08:00; language=001; locale=zh_CN; orgId=; defaultOrg=; tenantid=yfjz0nwh; theme=; languages=1_3; newArch=true; sysid=diwork; a00=KTF3IbQ_YFzlYMEsgF5U3ajMTAF1mggVtAUXcmtrEJ95Zmp6MG53aGAzMzkxMjI0NTc4ODA3NTY4YHlmanowbndoYDc1ZmM1YTY2LWQ4OWYtNDYzYS04Mzc3LWE3M2FkZDcwZmJlOWAxYGBlOGI1YjVlOTkzYWRlNTkzYjJgYGAxNzc3OTgwNDIyMjQ0Nzk0Mzc2YGZhbHNlYGAxNzEwMzQ3NDEwMjE0YHltc3NlczozODk0NDZjZmRiOWYyNmJiNDY1MzdlYTMzNTkyN2MyNGBkaXdvcmtg; a10=MDExMjY4MzU1NDA1MjM0MTAyMTQ; n_f_f=true; wb_at=LMjpvouuptj4QP3rMld8x8dcfyco6qojnmkhmd; c800=dccore2; jDiowrkTokenMock=bttMHpDOFhtdGpwMkxGODQvM1RTYitPbTlJZEpucVp3ejdDcXp4NEdzYnYyKzJnTXJwUk5pQk1HVEtXODdPTWkxTl9fZXVjLnlvbnlvdWNsb3VkLmNvbQ..__cf4d8cf47ce522f496400cc1426eb5ae_1710347410204TGTGdccore2iuap-apcom-workbench5dd7e36eYT; XSRF-TOKEN=AX_AOWSTDMSORIVNRWO0TCFCVJE1!003311; UBA_LAST_EID=dn07ltq0t840");
            dicHeader.Add("Referer", "https://dbox.yonyoucloud.com/");
            dicHeader.Add("Sec-Fetch-Dest", "empty");
            dicHeader.Add("Sec-Fetch-Mode", "cors");
            dicHeader.Add("Sec-Fetch-Site", "same-origin");
            dicHeader.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0");
            dicHeader.Add("X-Requested-With", "XMLHttpRequest");
            dicHeader.Add("from", "diwork");
            dicHeader.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Microsoft Edge\";v=\"120\"");
            dicHeader.Add("sec-ch-ua-mobile", "?0");
            dicHeader.Add("sec-ch-ua-platform", "\"Windows\"");
            dicHeader.Add("serviceCodeDiwork", "ipass0010");
            dicHeader.Add("ts", strTS);

            var result = HttpHelper.HttpGetAsync(requestUrl, dicParam, dicHeader);
            Console.WriteLine(result);
        }
    }
}