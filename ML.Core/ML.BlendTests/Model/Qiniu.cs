using System;
using System.Collections.Generic;
using System.Text;

namespace ML.BlendTests.Model
{
    public class Qiniu
    {
        public string accessKeyId { get; set; }
        public string accessKeySecret { get; set; }
        public string defaultEndPoint { get; set; }
        public string region { get; set; }
        public string bucketName { get; set; }
    }
}
