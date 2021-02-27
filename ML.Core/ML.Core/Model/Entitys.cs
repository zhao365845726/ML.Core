//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Core
{
    /// <summary>
    /// 适配器实体
    /// </summary>
    public class AdapterEntity
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 标识符
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string NetworkInterfaceType { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public string Speed { get; set; }
        /// <summary>
        /// 操作状态
        /// </summary>
        public string OperationalStatus { get; set; }
        /// <summary>
        /// MAC地址
        /// </summary>
        public string MacPhysicalAddress { get; set; }
    }
}
