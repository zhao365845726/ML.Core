using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Unified.Enum
{
    /// <summary>
    /// 七牛云访问Token类型
    /// </summary>
    public enum QiniuAccessTokenType
    {
        /// <summary>
        /// 上传Token
        /// </summary>
        UPLOAD = 1,
        /// <summary>
        /// 下载Token
        /// </summary>
        DOWNLOAD = 2,
        /// <summary>
        /// 管理Token
        /// </summary>
        MANAGE = 3,
    }
}
