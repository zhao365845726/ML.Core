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

        /// <summary>
        /// 发送单条信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="signName"></param>
        /// <param name="templateCode"></param>
        /// <param name="code"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 批量发送短信内容
        /// </summary>
        /// <param name="phoneNameJson"></param>
        /// <param name="signNameJson"></param>
        /// <param name="templateCode"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public SendBatchSmsResponse BatchSend(string phoneNameJson, string signNameJson, string templateCode, string code)
        {
            Client client = CreateSmsClient("");
            SendBatchSmsRequest sendBatchSmsRequest = new SendBatchSmsRequest
            {
                PhoneNumberJson = phoneNameJson,
                SignNameJson = signNameJson,
                TemplateCode = templateCode,
                TemplateParamJson = $"{{\"code\":\"{code}\"}}",
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.SendBatchSms(sendBatchSmsRequest);
        }
    }
}
