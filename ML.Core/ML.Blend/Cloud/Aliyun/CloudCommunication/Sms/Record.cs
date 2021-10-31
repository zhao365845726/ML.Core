using AlibabaCloud.SDK.Dysmsapi20170525;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Aliyun.CloudCommunication.Sms
{
    public class Record : ClientBase
    {
        public Record(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 查看短信发送记录和发送状态
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="strDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public QuerySendDetailsResponse QuerySendDetails(string mobile, string strDate, int pageIndex,int pageSize)
        {
            Client client = CreateSmsClient("");
            QuerySendDetailsRequest request = new QuerySendDetailsRequest
            {
                PhoneNumber = mobile,
                SendDate = strDate,
                PageSize = pageSize,
                CurrentPage = pageIndex,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.QuerySendDetails(request);
        }
    }
}
