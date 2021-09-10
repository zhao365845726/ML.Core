//using System;
//using System.Collections.Generic;
//using System.Text;
//using AlibabaCloud.SDK.Alidns20150109;
//using AlibabaCloud.SDK.Alidns20150109.Models;

//namespace ML.Blend.Cloud.Interface.Domain
//{
//    /// <summary>
//    /// 线路
//    /// </summary>
//    public class Line : Base
//    {
//        public Line(string akId, string akSecret) : base(akId, akSecret)
//        {
//        }

//        /// <summary>
//        /// 查询云解析支持的所有线路列表
//        /// </summary>
//        /// <param name="clientIp"></param>
//        /// <param name="domainName"></param>
//        /// <returns></returns>
//        public DescribeSupportLinesResponse DescribeSupportLines(string clientIp = "",string domainName = "")
//        {
//            Client client = CreateClient();
//            DescribeSupportLinesRequest describeSupportLinesRequest = new DescribeSupportLinesRequest
//            {
//                Lang = lang,
//                UserClientIp = clientIp,
//                DomainName = domainName,
//            };
//            // 复制代码运行请自行打印 API 的返回值
//            return client.DescribeSupportLines(describeSupportLinesRequest);
//        }

//        /// <summary>
//        /// 查询自定义线路列表
//        /// </summary>
//        /// <param name="domainName"></param>
//        /// <param name="pageIndex"></param>
//        /// <param name="pageSize"></param>
//        /// <returns></returns>
//        public DescribeCustomLinesResponse DescribeCustomLines(string domainName,int pageIndex,int pageSize)
//        {
//            Client client = CreateClient();
//            DescribeCustomLinesRequest describeCustomLinesRequest = new DescribeCustomLinesRequest
//            {
//                Lang = lang,
//                DomainName = domainName,
//                PageNumber = pageIndex,
//                PageSize = pageSize,
//            };
//            // 复制代码运行请自行打印 API 的返回值
//            return client.DescribeCustomLines(describeCustomLinesRequest);
//        }

//        /// <summary>
//        /// 查询自定义线路
//        /// </summary>
//        /// <param name="lineId"></param>
//        /// <returns></returns>
//        public DescribeCustomLineResponse DescribeCustomLine(long lineId)
//        {
//            Client client = CreateClient();
//            DescribeCustomLineRequest describeCustomLineRequest = new DescribeCustomLineRequest
//            {
//                LineId = lineId,
//                Lang = lang,
//            };
//            // 复制代码运行请自行打印 API 的返回值
//            return client.DescribeCustomLine(describeCustomLineRequest);
//        }

//        /// <summary>
//        /// 添加自定义线路
//        /// </summary>
//        /// <param name="endIp"></param>
//        /// <param name="startIp"></param>
//        /// <param name="domainName"></param>
//        /// <param name="lineName"></param>
//        /// <returns></returns>
//        public AddCustomLineResponse AddCustomLine(string endIp,string startIp,string domainName,string lineName)
//        {
//            Client client = CreateClient();
//            AddCustomLineRequest.AddCustomLineRequestIpSegment ipSegment0 = new AddCustomLineRequest.AddCustomLineRequestIpSegment
//            {
//                EndIp = endIp,
//                StartIp = startIp,
//            };
//            AddCustomLineRequest addCustomLineRequest = new AddCustomLineRequest
//            {
//                Lang = lang,
//                DomainName = domainName,
//                LineName = lineName,
//                IpSegment = new List<AddCustomLineRequest.AddCustomLineRequestIpSegment>
//                {
//                    ipSegment0
//                },
//            };
//            // 复制代码运行请自行打印 API 的返回值
//            return client.AddCustomLine(addCustomLineRequest);
//        }

//        /// <summary>
//        /// 编辑自定义线路
//        /// </summary>
//        /// <param name="endIp"></param>
//        /// <param name="startIp"></param>
//        /// <param name="lineName"></param>
//        /// <param name="lineId"></param>
//        /// <returns></returns>
//        public UpdateCustomLineResponse UpdateCustomLine(string endIp, string startIp, string lineName,long lineId)
//        {
//            Client client = CreateClient();
//            UpdateCustomLineRequest.UpdateCustomLineRequestIpSegment ipSegment0 = new UpdateCustomLineRequest.UpdateCustomLineRequestIpSegment
//            {
//                EndIp = endIp,
//                StartIp = startIp,
//            };
//            UpdateCustomLineRequest updateCustomLineRequest = new UpdateCustomLineRequest
//            {
//                Lang = lang,
//                LineName = lineName,
//                LineId = lineId,
//                IpSegment = new List<UpdateCustomLineRequest.UpdateCustomLineRequestIpSegment>
//                {
//                    ipSegment0
//                },
//            };
//            // 复制代码运行请自行打印 API 的返回值
//            return client.UpdateCustomLine(updateCustomLineRequest);
//        }

//        /// <summary>
//        /// 批量删除自定义线路
//        /// </summary>
//        /// <param name="lineIds"></param>
//        /// <returns></returns>
//        public DeleteCustomLinesResponse DeleteCustomLines(string lineIds)
//        {
//            Client client = CreateClient();
//            DeleteCustomLinesRequest deleteCustomLinesRequest = new DeleteCustomLinesRequest
//            {
//                Lang = lang,
//                LineIds = lineIds,
//            };
//            // 复制代码运行请自行打印 API 的返回值
//            return client.DeleteCustomLines(deleteCustomLinesRequest);
//        }
//    }
//}
