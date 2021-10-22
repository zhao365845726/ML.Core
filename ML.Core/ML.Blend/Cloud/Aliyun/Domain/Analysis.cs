﻿using System;
using System.Collections.Generic;
using System.Text;
using AlibabaCloud.SDK.Alidns20150109;
using AlibabaCloud.SDK.Alidns20150109.Models;
using Tea;
using Tea.Utils;

namespace ML.Blend.Cloud.Aliyun.Domain
{
    /// <summary>
    /// 解析
    /// </summary>
    public class Analysis : ClientBase
    {
        public Analysis(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 根据传入参数添加解析记录
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="rr"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public AddDomainRecordResponse AddDomainRecord(string domainName,string rr,string type,string value)
        {
            Client client = CreateDomainClient("");
            AddDomainRecordRequest addDomainRecordRequest = new AddDomainRecordRequest
            {
                Lang = lang,
                //UserClientIp = string.Empty,
                DomainName = domainName,
                RR = rr,
                Type = type,
                Value = value,
                //TTL = 600,
                //Priority = 1,
                //Line = string.Empty,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.AddDomainRecord(addDomainRecordRequest);
        }

        /// <summary>
        /// 根据传入参数删除解析记录
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public DeleteDomainRecordResponse DeleteDomainRecord(string recordId)
        {
            Client client = CreateDomainClient("");
            DeleteDomainRecordRequest deleteDomainRecordRequest = new DeleteDomainRecordRequest
            {
                Lang = lang,
                UserClientIp = string.Empty,
                RecordId = recordId,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DeleteDomainRecord(deleteDomainRecordRequest);
        }

        /// <summary>
        /// 根据传入参数删除主机记录对应的解析记录
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="rr"></param>
        /// <returns></returns>
        public DeleteSubDomainRecordsResponse DeleteSubDomainRecords(string domainName,string rr)
        {
            Client client = CreateDomainClient("");
            DeleteSubDomainRecordsRequest deleteSubDomainRecordsRequest = new DeleteSubDomainRecordsRequest
            {
                Lang = lang,
                UserClientIp = string.Empty,
                DomainName = domainName,
                RR = rr,
                Type = string.Empty,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DeleteSubDomainRecords(deleteSubDomainRecordsRequest);
        }

        /// <summary>
        /// 根据传入参数批量删除主机记录对应的解析记录
        /// </summary>
        /// <param name="lstRecordId"></param>
        /// <returns></returns>
        public bool BatchDeleteSubDomainRecords(List<string> lstRecordId)
        {
            try
            {
                foreach (string item in lstRecordId)
                {
                    DeleteDomainRecord(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据传入参数批量删除主机记录对应的解析记录
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="listRR">解析记录集合</param>
        /// <returns></returns>
        public bool BatchDeleteSubDomainRecords(string domainName, List<string> listRR)
        {
            try
            {
                foreach(string rr in listRR)
                {
                    DeleteSubDomainRecords(domainName, rr);
                }
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据传入参数修改解析记录
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="rr"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public UpdateDomainRecordResponse UpdateDomainRecord(string recordId,string rr,string type,string value)
        {
            Client client = CreateDomainClient("");
            UpdateDomainRecordRequest updateDomainRecordRequest = new UpdateDomainRecordRequest
            {
                Lang = lang,
                UserClientIp = string.Empty,
                RecordId = recordId,
                RR = rr,
                Type = type,
                Value = value,
                TTL = 600,
                Priority = 1,
                Line = string.Empty,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.UpdateDomainRecord(updateDomainRecordRequest);
        }

        /// <summary>
        /// 修改解析记录的备注
        /// </summary>
        /// <param name="clientIp"></param>
        /// <param name="recordId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public UpdateDomainRecordRemarkResponse UpdateDomainRecordRemark(string clientIp,string recordId,string remark)
        {
            Client client = CreateDomainClient("");
            UpdateDomainRecordRemarkRequest updateDomainRecordRemarkRequest = new UpdateDomainRecordRemarkRequest
            {
                Lang = lang,
                UserClientIp = clientIp,
                RecordId = recordId,
                Remark = remark,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.UpdateDomainRecordRemark(updateDomainRecordRemarkRequest);
        }

        /// <summary>
        /// 根据传入参数设置解析记录状态
        /// </summary>
        /// <param name="clientIp"></param>
        /// <param name="recordId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public SetDomainRecordStatusResponse SetDomainRecordStatus(string clientIp,string recordId,string status)
        {
            Client client = CreateDomainClient("");
            SetDomainRecordStatusRequest setDomainRecordStatusRequest = new SetDomainRecordStatusRequest
            {
                Lang = lang,
                UserClientIp = clientIp,
                RecordId = recordId,
                Status = status,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.SetDomainRecordStatus(setDomainRecordStatusRequest);
        }

        /// <summary>
        /// 获取解析记录的详细信息
        /// </summary>
        /// <param name="clientIp"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public DescribeDomainRecordInfoResponse DescribeDomainRecordInfo(string clientIp,string recordId)
        {
            Client client = CreateDomainClient("");
            DescribeDomainRecordInfoRequest describeDomainRecordInfoRequest = new DescribeDomainRecordInfoRequest
            {
                Lang = lang,
                UserClientIp = clientIp,
                RecordId = recordId,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DescribeDomainRecordInfo(describeDomainRecordInfoRequest);
        }

        /// <summary>
        /// 根据传入参数获取指定主域名的所有解析记录列表
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DescribeDomainRecordsResponse DescribeDomainRecords(string domainName,int pageIndex,int pageSize)
        {
            Client client = CreateDomainClient("");
            DescribeDomainRecordsRequest describeDomainRecordsRequest = new DescribeDomainRecordsRequest
            {
                Lang = lang,
                DomainName = domainName,
                PageNumber = pageIndex,
                PageSize = pageSize,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DescribeDomainRecords(describeDomainRecordsRequest);
        }

        /// <summary>
        /// 判断解决的二级域名列表中是否存在需要查询的域名
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="subDomainName"></param>
        /// <returns></returns>
        public bool DoesExistDomain(string domainName, string subDomainName)
        {
            try
            {
                bool isRes = false;
                for (int i = 1; i <= 10; i++)
                {
                    var result = DescribeDomainRecords(domainName, i, 10);
                    if (result != null)
                    {
                        var record = result.Body.DomainRecords.Record;
                        foreach (var recordItem in record)
                        {
                            if (recordItem.RR.Trim().Equals(subDomainName))
                            {
                                isRes = true;
                                break;
                            }
                        }
                        if (isRes)
                        {
                            break;
                        }
                    }
                }
                return isRes;
            }catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据传入参数获取域名的解析操作日志
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DescribeRecordLogsResponse DescribeRecordLogs(string domainName,int pageIndex,int pageSize)
        {
            Client client = CreateDomainClient("");
            DescribeRecordLogsRequest describeRecordLogsRequest = new DescribeRecordLogsRequest
            {
                Lang = lang,
                UserClientIp = string.Empty,
                DomainName = domainName,
                KeyWord = string.Empty,
                StartDate = string.Empty,
                EndDate = string.Empty,
                PageNumber = pageIndex,
                PageSize = pageSize,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DescribeRecordLogs(describeRecordLogsRequest);
        }

        /// <summary>
        /// 根据传入参数获取某个固定子域名的所有解析记录列表
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="subDomain"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DescribeSubDomainRecordsResponse DescribeSubDomainRecords(string domainName,string subDomain, int pageIndex, int pageSize)
        {
            Client client = CreateDomainClient("");
            DescribeSubDomainRecordsRequest describeSubDomainRecordsRequest = new DescribeSubDomainRecordsRequest
            {
                Lang = lang,
                UserClientIp = string.Empty,
                SubDomain = subDomain,
                Type = string.Empty,
                Line = string.Empty,
                DomainName = domainName,
                PageNumber = pageIndex,
                PageSize = pageSize,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DescribeSubDomainRecords(describeSubDomainRecordsRequest);
        }

        /// <summary>
        /// 生成txt记录。用于域名、子域名找回、添加子域名验证、批量找回等功能
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public GetTxtRecordForVerifyResponse GetTxtRecordForVerify(string domainName)
        {
            Client client = CreateDomainClient("");
            GetTxtRecordForVerifyRequest getTxtRecordForVerifyRequest = new GetTxtRecordForVerifyRequest
            {
                Lang = lang,
                DomainName = domainName,
                Type = string.Empty,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.GetTxtRecordForVerify(getTxtRecordForVerifyRequest);
        }
    }
}
