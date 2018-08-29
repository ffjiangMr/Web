using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Hubs;


namespace TomNet.Autofac.SignalR
{
    /// <summary>
    /// 带生命周期作用域的Hub
    /// </summary>
    public interface ILifetimeHub : IHub
    {
        /// <summary>
        /// 加入当前集线器的组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        Task JoinGroup(string groupName);

        /// <summary>
        /// 离开当前集线器的组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        Task LeaveGroup(string groupName);

        /// <summary>
        /// 对象释放事件
        /// </summary>
        event EventHandler OnDisposing;
    }
}