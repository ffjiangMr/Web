﻿using System;
using System.Collections;
using System.Web.Mvc;

using TomNet.Core;
using TomNet.Core.Dependency;
using TomNet.Core.Security;
using TomNet.Utility.Filter;
using TomNet.Web.Mvc.UI;


namespace TomNet.Web.Mvc.Extensions
{
    /// <summary>
    /// 扩展辅助操作方法
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 获取MVC操作的相关功能信息
        /// </summary>
        public static IFunction GetExecuteFunction(this ControllerContext context, IServiceProvider provider)
        {
            const string key = Constants.CurrentMvcFunctionKey;
            IDictionary items = context.HttpContext.Items;
            if (items.Contains(key))
            {
                return (IFunction)items[key];
            }
            string area = context.GetAreaName();
            string controller = context.GetControllerName();
            string action = context.GetActionName();
            IFunctionHandler functionHandler = provider.GetService<IFunctionHandler>();
            if (functionHandler == null)
            {
                return null;
            }
            IFunction function = functionHandler.GetFunction(area, controller, action);
            if (function != null)
            {
                items.Add(key, function);
            }
            return function;
        }

        /// <summary>
        /// 获取MVC操作的相关功能信息
        /// </summary>
        public static IFunction GetExecuteFunction(this ControllerBase controller, IServiceProvider provider)
        {
            return controller.ControllerContext.GetExecuteFunction(provider);
        }

        /// <summary>
        /// 将分页数据转换为表格数据格式
        /// </summary>
        public static GridData<TData> ToGridData<TData>(this PageResult<TData> pageResult)
        {
            return new GridData<TData>(pageResult.Data, pageResult.Total);
        }
    }
}