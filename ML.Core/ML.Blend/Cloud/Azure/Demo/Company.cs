using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Azure.Demo
{
    public class Company
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
