using System;
using System.Collections.Generic;
using System.Text;
using ML.Core.Enum;
using ML.Core.Html;

namespace ML.Core
{
    public class MLHtml
    {
        public const string XGRIGHT = "\\";
        public const string YHDOUBLE = "\"";
        public const string EVENTFH = "@";  //事件符号
        public const string BINDFH = "";  //属性绑定符号

        /// <summary>
        /// 根据元素生成
        /// </summary>
        /// <param name="ele">元素对象</param>
        /// <returns></returns>
        public string GeneralByElement(MLElement ele)
        {
            string strAttribute = string.Empty;
            string strEvent = string.Empty;
            if(ele.DicAttribute.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in ele.DicAttribute)
                {
                    strAttribute += $" {BINDFH}{pair.Key}={YHDOUBLE}{pair.Value}{YHDOUBLE}";
                }
            }
            if(ele.DicEvents.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in ele.DicEvents)
                {
                    strEvent += $" {EVENTFH}{pair.Key}={YHDOUBLE}{pair.Value}{YHDOUBLE}";
                }
            }
            return SetTag(ele.SpaceNumber, ele.TagName, $"{strAttribute}{strEvent}", ele.Content,ele.TagSwitch);
        }

        /// <summary>
        /// 根据元素生成
        /// </summary>
        /// <param name="ele">元素对象</param>
        /// <returns></returns>
        public string GeneralByElement(Element ele)
        {
            string strAttribute = string.Empty;
            string strEvent = string.Empty;
            string strCustomData = string.Empty;
            if (ele.DicAttribute.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in ele.DicAttribute)
                {
                    strAttribute += $" {BINDFH}{pair.Key}={YHDOUBLE}{pair.Value}{YHDOUBLE}";
                }
            }
            if (ele.DicEvents.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in ele.DicEvents)
                {
                    strEvent += $" {EVENTFH}{pair.Key}={YHDOUBLE}{pair.Value}{YHDOUBLE}";
                }
            }
            if (!string.IsNullOrEmpty(ele.CustomData))
            {
                strCustomData = ele.CustomData;
            }
            return SetTag(ele.SpaceNumber, ele.TagName, $"{strAttribute}{strEvent}{strCustomData}", ele.Content, ele.TagSwitch);
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="iSpace">缩进</param>
        /// <param name="key">元素</param>
        /// <param name="attribute">属性</param>
        /// <param name="content">内容</param>
        /// <param name="tagSwitch">元素闭合类型</param>
        /// <returns></returns>
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

        /// <summary>
        /// 生成Html标记
        /// </summary>
        /// <param name="iSpace">缩进</param>
        /// <param name="tagName">标签名</param>
        /// <param name="tagSwitch">标签闭合类型</param>
        /// <returns></returns>
        public string BuildHtml(int iSpace, string tagName, TagSwitch tagSwitch)
        {
            Dictionary<string, string> dicAttr = new Dictionary<string, string>();
            Dictionary<string, string> dicEvent = new Dictionary<string, string>();
            MLElement element = new MLElement()
            {
                TagName = tagName,
                SpaceNumber = iSpace,
                Content = string.Empty,
                DicAttribute = dicAttr,
                DicEvents = dicEvent,
                TagSwitch = tagSwitch
            };
            return GeneralByElement(element);
        }

        /// <summary>
        /// 生成Html标记
        /// </summary>
        /// <param name="iSpace">缩进</param>
        /// <param name="tagName">标签名</param>
        /// <param name="content">内容</param>
        /// <param name="tagSwitch">标签闭合类型</param>
        /// <returns></returns>
        public string BuildHtml(int iSpace, string tagName, string content, TagSwitch tagSwitch)
        {
            Dictionary<string, string> dicAttr = new Dictionary<string, string>();
            Dictionary<string, string> dicEvent = new Dictionary<string, string>();
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

        /// <summary>
        /// 生成Html标记
        /// </summary>
        /// <param name="iSpace">缩进</param>
        /// <param name="tagName">标签名</param>
        /// <param name="content">内容</param>
        /// <param name="attribute">属性</param>
        /// <param name="tagSwitch">标签闭合类型</param>
        /// <returns></returns>
        public string BuildHtml(int iSpace, string tagName, string content, string attribute, TagSwitch tagSwitch)
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

        /// <summary>
        /// 生成Html标记
        /// </summary>
        /// <param name="iSpace">缩进</param>
        /// <param name="tagName">标签名</param>
        /// <param name="content">内容</param>
        /// <param name="attribute">属性</param>
        /// <param name="events">事件</param>
        /// <param name="tagSwitch">标签闭合类型</param>
        /// <returns></returns>
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

        /// <summary>
        /// 生成Html标记
        /// </summary>
        /// <param name="iSpace">缩进</param>
        /// <param name="tagName">标签名</param>
        /// <param name="content">内容</param>
        /// <param name="attribute">属性</param>
        /// <param name="events">事件</param>
        /// <param name="tagSwitch">标签闭合类型</param>
        /// <returns></returns>
        public string BuildHtml(int iSpace, string tagName, string content, string attribute, string events,string customData, TagSwitch tagSwitch)
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
            Element element = new Element()
            {
                TagName = tagName,
                SpaceNumber = iSpace,
                Content = content,
                DicAttribute = dicAttr,
                DicEvents = dicEvent,
                CustomData = customData,
                TagSwitch = tagSwitch
            };
            return GeneralByElement(element);
        }
    }

    /// <summary>
    /// 元素对象
    /// </summary>
    public class MLElement
    {
        public string TagName { get; set; }
        public string Content { get; set; }
        public int SpaceNumber { get; set; }
        public TagSwitch TagSwitch { get; set; }
        public Dictionary<string,string> DicAttribute { get; set; }
        public Dictionary<string,string> DicEvents { get; set; }
    }

    /// <summary>
    /// 元素属性
    /// 参考地址：https://www.runoob.com/tags/ref-standardattributes.html
    /// </summary>
    public class MLElementAttribute
    {
        /// <summary>
        /// 设置访问元素的键盘快捷键
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// 规定元素的类名（classname）
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 规定是否可编辑元素的内容
        /// </summary>
        public string ContentEditable { get; set; }
        /// <summary>
        /// 指定一个元素的上下文菜单。当用户右击该元素，出现上下文菜单
        /// </summary>
        public string ContextMenu { get; set; }
        /// <summary>
        /// 用于存储页面的自定义数据
        /// </summary>
        public Dictionary<string,string> Data { get; set; }
        /// <summary>
        /// 设置元素中内容的文本方向
        /// </summary>
        public string Dir { get; set; }
        /// <summary>
        /// 指定某个元素是否可以拖动
        /// </summary>
        public string Draggable { get; set; }
        /// <summary>
        /// 指定是否将数据复制，移动，或链接，或删除
        /// </summary>
        public string Dropzone { get; set; }
        /// <summary>
        /// hidden 属性规定对元素进行隐藏
        /// </summary>
        public string Hidden { get; set; }
        /// <summary>
        /// 规定元素的唯一 id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设置元素中内容的语言代码
        /// </summary>
        public string Lang { get; set; }
        /// <summary>
        /// 检测元素是否拼写错误
        /// </summary>
        public string SpellCheck { get; set; }
        /// <summary>
        /// 规定元素的行内样式（inline style）
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 设置元素的 Tab 键控制次序
        /// </summary>
        public string Tabindex { get; set; }
        /// <summary>
        /// 规定元素的额外信息（可在工具提示中显示）
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 指定是否一个元素的值在页面载入时是否需要翻译
        /// </summary>
        public string Translate { get; set; }
    }
}
