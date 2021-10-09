using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Model
{
    /// <summary>
    /// 发送邮件实体
    /// </summary>
    public class SendEmailEntity
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address {  get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName {  get; set; }
        /// <summary>
        /// 协议
        /// </summary>
        public string Host {  get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName {  get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password {  get; set; }
        /// <summary>
        /// 是否启用加密通道
        /// </summary>
        public bool IsEnableSSL { get; set; }
    }

    /// <summary>
    /// 接受邮件实体
    /// </summary>
    public class ReceiveEmailEntity
    {
        /// <summary>
        /// 接受邮件
        /// </summary>
        public string ReceiveEmail {  get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string MsgSubject {  get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string MsgBody {  get; set; }
    }
}
