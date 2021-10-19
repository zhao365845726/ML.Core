using System;
using System.Collections.Generic;
using System.Text;
using AlibabaCloud.SDK.Dysmsapi20170525;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using Tea;
using Tea.Utils;


namespace ML.Blend.Cloud.Aliyun.CloudCommunication.Sms
{
    public class Send : ClientBase
    {
        public Send(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        public SendSmsResponse SingleSend(string mobile,string signName,string templateCode,string code)
        {
            Client client = CreateSmsClient("");
            SendSmsRequest sendSmsRequest = new SendSmsRequest
            {
                PhoneNumbers = mobile,
                SignName = signName,
                TemplateCode = templateCode,
                TemplateParam = $"{{\"code\":\"{code}\"}}",
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.SendSms(sendSmsRequest);
        }
    }
}
