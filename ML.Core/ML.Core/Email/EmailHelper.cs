using ML.Core.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ML.Core
{
    public class EmailHelper
    {
        public EmailHelper()
        {
        }

        /// <summary>
        /// 发送电子邮件,所有SMTP配置信息均在config配置文件中system.net节设置.
        /// </summary>
        /// <param name="sendEmailEntity">发送邮件实体</param>
        /// <param name="receiveEmailEntity">接受邮件实体</param>
        /// <returns></returns>
        public static bool SendEmail(SendEmailEntity sendEmailEntity, ReceiveEmailEntity receiveEmailEntity)
        {
            //创建电子邮件对象
            MailMessage email = new MailMessage();
            email.From = new MailAddress(sendEmailEntity.Address, sendEmailEntity.DisplayName);
            //设置接收人的电子邮件地址
            email.To.Add(receiveEmailEntity.ReceiveEmail);
            //设置邮件的标题
            email.Subject = receiveEmailEntity.MsgSubject;
            //设置邮件的正文
            email.Body = receiveEmailEntity.MsgBody;
            //设置邮件为HTML格式
            email.IsBodyHtml = true;
            //优先级
            email.Priority = MailPriority.High;

            //创建SMTP客户端，将自动从配置文件中获取SMTP服务器信息
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;//发送方式
                                                             //smtp.Host = "smtp.mxhichina.com";//服务器主机
                                                             //smtp.Credentials = new NetworkCredential("martyzane@milisx.com", "Mz19881023*.0");//用户名和密码

            smtp.Host = !string.IsNullOrEmpty(sendEmailEntity.Host) ? sendEmailEntity.Host : "smtp.163.com";
            smtp.Credentials = new NetworkCredential(sendEmailEntity.UserName, sendEmailEntity.Password);//用户名和密码

            //开启SSL
            smtp.EnableSsl = sendEmailEntity.IsEnableSSL;
            smtp.UseDefaultCredentials = true;
            if (sendEmailEntity.IsEnableSSL == true)
            {
                smtp.Port = 465;//端口
            }
            else
            {
                smtp.Port = 25;//端口
            }

            try
            {
                //发送电子邮件
                smtp.Send(email);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 发送电子邮件,所有SMTP配置信息均在config配置文件中system.net节设置.
        /// </summary>
        /// <param name="receiveEmail">接收电子邮件的地址</param>
        /// <param name="msgSubject">电子邮件的标题</param>
        /// <param name="msgBody">电子邮件的正文</param>
        /// <param name="IsEnableSSL">是否开启SSL</param>
        public static bool SendEmail(string receiveEmail, string msgSubject, string msgBody, bool IsEnableSSL)
        {
            //创建电子邮件对象
            MailMessage email = new MailMessage();
            email.From = new MailAddress("zhao365845726@163.com", "zhao365845726");
            //设置接收人的电子邮件地址
            email.To.Add(receiveEmail);
            //设置邮件的标题
            email.Subject = msgSubject;
            //设置邮件的正文
            email.Body = msgBody;
            //设置邮件为HTML格式
            email.IsBodyHtml = true;
            //优先级
            email.Priority = MailPriority.High;

            //创建SMTP客户端，将自动从配置文件中获取SMTP服务器信息
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;//发送方式
                                                             //smtp.Host = "smtp.mxhichina.com";//服务器主机
                                                             //smtp.Credentials = new NetworkCredential("martyzane@milisx.com", "Mz19881023*.0");//用户名和密码

            smtp.Host = "smtp.163.com";
            smtp.Credentials = new NetworkCredential("zhao365845726@163.com", "Mz19881023*.0");//用户名和密码

            //开启SSL
            smtp.EnableSsl = IsEnableSSL;
            if (IsEnableSSL == true)
            {
                smtp.Port = 465;//端口
            }
            else
            {
                smtp.Port = 25;//端口
            }

            try
            {
                //发送电子邮件
                smtp.Send(email);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
