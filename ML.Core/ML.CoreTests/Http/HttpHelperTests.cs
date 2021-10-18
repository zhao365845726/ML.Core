using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Core;
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
    }
}