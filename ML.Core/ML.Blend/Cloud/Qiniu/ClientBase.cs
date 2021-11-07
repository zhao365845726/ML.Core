using ML.Blend.Unified.Enum;
using Qiniu.Common;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Qiniu
{
    public class ClientBase : Base
    {
        public ClientBase(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /**
         * 使用AK&SK初始化账号Client
         * string accessKeyId, string accessKeySecret
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public Mac CreateClient(string bucketName,ZoneID zoneId = ZoneID.CN_East,bool useHttps = false)
        {
            Mac mac = new Mac(accessKeyId, accessKeySecret);
            // ZoneID zoneId = ZoneID.CN_East; 
            // [CN_East:华东] [CN_South:华南] [CN_North:华北] [US_North:北美]
            // USE_HTTPS = (true|false) 是否使用HTTPS
            Config.AutoZone(accessKeyId, bucketName, useHttps);
            return mac;
        }

        public void SetZone(ZoneID zoneId = ZoneID.CN_East, bool useHttps = false)
        {
            // ZoneID zoneId = ZoneID.CN_East; 
            // [CN_East:华东] [CN_South:华南] [CN_North:华北] [US_North:北美]
            // USE_HTTPS = (true|false) 是否使用HTTPS
            Config.SetZone(zoneId, useHttps);
        }

        public string SetAuth(Mac mac,QiniuAccessTokenType qiniuAccessTokenType, PutPolicy putPolicy)
        {
            string strRes = string.Empty;
            Auth auth = new Auth(mac);
            switch (qiniuAccessTokenType)
            {
                case QiniuAccessTokenType.UPLOAD:
                    {
                        strRes = auth.CreateUploadToken(putPolicy.ToJsonString());
                        break;
                    }
                case QiniuAccessTokenType.DOWNLOAD:
                    {
                        strRes = auth.CreateDownloadToken("");
                        break;
                    }
                case QiniuAccessTokenType.MANAGE:
                    {
                        strRes = auth.CreateManageToken("");
                        break;
                    }
            }
            return strRes;
        }
    }
}
