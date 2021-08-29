using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Html
{
    /// <summary>
    /// 元素属性
    /// 参考地址：https://www.runoob.com/tags/ref-standardattributes.html
    /// </summary>
    public class ElementAttribute
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
        public Dictionary<string, string> Data { get; set; }
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
