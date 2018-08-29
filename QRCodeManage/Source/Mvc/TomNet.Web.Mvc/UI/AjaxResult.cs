﻿namespace TomNet.Web.Mvc.UI
{
    /// <summary>
    /// 表示Ajax操作结果 
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult()
            : this(null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, AjaxResultType type = AjaxResultType.Info, object data = null)
            : this(content, data, type)
        { }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, object data, AjaxResultType type = AjaxResultType.Info)
        {
            Type = type;
            Content = content;
            Data = data;
        }

        /// <summary>
        /// 获取或设置 Ajax操作结果类型
        /// </summary>
        public AjaxResultType Type { get; set; }

        /// <summary>
        /// 获取或设置 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}