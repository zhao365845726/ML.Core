using System;
using System.Collections.Generic;
using System.Text;
using ML.Core.Enum;

namespace ML.Core
{
    public class MLHtml
    {
        public const string XGRIGHT = "\\";
        public const string YHDOUBLE = "\"";
        public const string EVENTFH = "@";  //事件符号
        public const string BINDFH = "";  //属性绑定符号

        public string GeneralByElement(MLElement ele)
        {
            string strAttribute = string.Empty;
            string strEvent = string.Empty;
            foreach (KeyValuePair<string,string> pair in ele.DicAttribute)
            {
                strAttribute += $" {BINDFH}{pair.Key}={YHDOUBLE}{pair.Value}{YHDOUBLE}";
            }
            foreach (KeyValuePair<string, string> pair in ele.DicEvents)
            {
                strEvent += $" {EVENTFH}{pair.Key}={YHDOUBLE}{pair.Value}{YHDOUBLE}";
            }
            return SetTag(ele.SpaceNumber, ele.TagName, $"{strAttribute}{strEvent}", ele.Content,ele.TagSwitch);
        }

        public string SetTag(int iSpace, string key, string attribute, string content, TagSwitch tagSwitch)
        {
            StringBuilder sb = new StringBuilder();
            string strSpace = string.Empty;
            if (iSpace > 0)
            {
                for (int i = 0; i < iSpace; i++)
                {
                    strSpace += "   ";
                }
            }
            switch (tagSwitch)
            {
                case TagSwitch.TAGFULL:
                    sb.Append($"{strSpace}<{key}{attribute}>{content}</{key}>\r\n");
                    break;
                case TagSwitch.TAGSIMPLEFULL:
                    sb.Append($"{strSpace}<{key}{attribute}/>\r\n");
                    break;
                case TagSwitch.TAGSTARTONLY:
                    sb.Append($"{strSpace}<{key}{attribute}>\r\n");
                    break;
                case TagSwitch.TAGENDONLY:
                    sb.Append($"{strSpace}</{key}>\r\n");
                    break;
            }
            
            return sb.ToString();
        }

        public string BuildHtml(int iSpace, string tagName, string content, string attribute, string events, TagSwitch tagSwitch)
        {
            Dictionary<string, string> dicAttr = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(attribute))
            {
                string[] saAttribute = attribute.Split('&');
                if (saAttribute.Length > 0)
                {
                    for (int i = 0; i < saAttribute.Length; i++)
                    {
                        string[] saTemp = saAttribute[i].Split('=');
                        dicAttr.Add(saTemp[0], saTemp[1]);
                    }
                }
            }
            Dictionary<string, string> dicEvent = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(events))
            {
                string[] saEvent = events.Split('&');
                if (saEvent.Length > 0)
                {
                    for (int i = 0; i < saEvent.Length; i++)
                    {
                        string[] saTemp = saEvent[i].Split('=');
                        dicEvent.Add(saTemp[0], saTemp[1]);
                    }
                }
            }
            MLElement element = new MLElement()
            {
                TagName = tagName,
                SpaceNumber = iSpace,
                Content = content,
                DicAttribute = dicAttr,
                DicEvents = dicEvent,
                TagSwitch = tagSwitch
            };
            return GeneralByElement(element);
        }
    }

    public class MLElement
    {
        public string TagName { get; set; }
        public string Content { get; set; }
        public int SpaceNumber { get; set; }
        public TagSwitch TagSwitch { get; set; }
        public Dictionary<string,string> DicAttribute { get; set; }
        public Dictionary<string,string> DicEvents { get; set; }
    }

    
}
