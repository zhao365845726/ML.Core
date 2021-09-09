using System;
using System.Collections.Generic;
using System.Text;
using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Model.Bucket;
using COSXML.CosException;

namespace ML.Blend.Cloud.Tencent
{
    public class ClientBase : Base
    {
        public ClientBase(string akId, string akSecret, string region) : base(akId, akSecret, region)
        {
        }
    }
}
