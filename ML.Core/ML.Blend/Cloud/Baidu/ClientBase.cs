using System;
using System.Collections.Generic;
using System.Text;
using BaiduBce;
using BaiduBce.Auth;
using BaiduBce.Services.Bos;

namespace ML.Blend.Cloud.Baidu
{
    public class ClientBase : Base
    {
        public ClientBase(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
        {
        }
    }
}
