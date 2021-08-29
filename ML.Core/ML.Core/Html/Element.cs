using ML.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Html
{
    public class Element
    {
        public string TagName { get; set; }
        public string Content { get; set; }
        public int SpaceNumber { get; set; }
        public TagSwitch TagSwitch { get; set; }
        public Dictionary<string, string> DicAttribute { get; set; }
        public Dictionary<string, string> DicEvents { get; set; }
        public string CustomData { get; set; }
    }
}
