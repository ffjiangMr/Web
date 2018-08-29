using System;
using System.Reflection;

using Microsoft.AspNet.SignalR.Hubs;

using TomNet.Core.Security;
using TomNet.Utility.Extensions;
using TomNet.Web.SignalR.Properties;
using Microsoft.AspNet.SignalR;
using TomNet.Utility;

namespace TomNet.Web.SignalR.Initialize
{
    /// <summary>
    /// SignalR功能处理器
    /// </summary>
    public class SignalRFunctionHandler : FunctionHandlerBase<Function, Guid>
    {
        /// <summary>
        /// 获取 功能技术提供者，如Mvc/WebApi/SignalR等，用于区分功能来源，各技术更新功能时，只更新属于自己技术的功能
        /// </summary>
        protected override PlatformToken PlatformToken
        {
            get { return PlatformToken.SignalR; }
        }

        /// <summary>
        /// 重写以实现从类型信息创建功能信息
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <returns></returns>
        protected override Function GetFunction(Type type)
        {
            if (!typeof(IHub).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.HubMethodInfoFinder_TypeNotHubType.FormatWith(type.FullName));
            }
            Function function = new Function()
            {
                Name = type.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("Hub", string.Empty),
                IsController = true,
                FunctionType = FunctionType.Anonymouse,
                PlatformToken = PlatformToken
            };
            return function;
        }

        /// <summary>
        /// 重写以实现从方法信息创建功能信息
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <returns></returns>
        protected override Function GetFunction(MethodInfo method)
        {
            Type type = method.DeclaringType;
            if (type == null)
            {
                throw new InvalidOperationException(Resources.FunctionHandler_DefindActionTypeIsNull.FormatWith(method.Name));
            }
            if (!typeof(IHub).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(Resources.FunctionHandler_MethodOwnTypeNotHubType.FormatWith(method.Name, type.FullName));
            }

            FunctionType functionType = FunctionType.Anonymouse;

            if (method.HasAttribute<LoginedAttribute>(true))
            {
                functionType = FunctionType.Logined;
            }
            else if (method.HasAttribute<RoleLimitAttribute>(true))
            {
                functionType = FunctionType.RoleLimit;
            }

            Function function = new Function()
            {
                Name = method.ToDescription(),
                Area = GetArea(type),
                Controller = type.Name.Replace("Hub", string.Empty),
                Action = method.Name,
                FunctionType = functionType,
                PlatformToken = PlatformToken,
                IsController = false,
                IsAjax = false,
                IsChild = false
            };
            return function;
        }

        /// <summary>
        /// 重写以实现从类型中获取功能的区域信息
        /// </summary>
        protected override string GetArea(Type type)
        {
            type.Required<Type, InvalidOperationException>(m => typeof(IHub).IsAssignableFrom(m) && !m.IsAbstract,
                Resources.HubMethodInfoFinder_TypeNotHubType.FormatWith(type.FullName));
            string @namespace = type.Namespace;
            if (@namespace == null)
            {
                return null;
            }
            int index = @namespace.IndexOf("Areas", StringComparison.Ordinal) + 6;
            string area = index > 6 ? @namespace.Substring(index, @namespace.IndexOf(".Hubs", StringComparison.Ordinal) - index) : null;
            return area;
        }
    }
}