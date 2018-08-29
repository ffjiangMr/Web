using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;


namespace TomNet.Autofac.SignalR
{
    /// <summary>
    /// 带生命周期作用域的Hub基类
    /// </summary>
    public abstract class LifetimeHub : Hub, ILifetimeHub
    {
        /// <summary>
        /// 加入当前集线器的组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        public virtual async Task JoinGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 离开当前集线器的组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        public virtual async Task LeaveGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 对象释放事件
        /// </summary>
        public event EventHandler OnDisposing;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                EventHandler handler = this.OnDisposing;
                if (handler != null)
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    
    /// <summary>
    /// 带生命周期作用域的Hub基类
    /// </summary>
    public abstract class LifetimeHub<T> : Hub<T>, ILifetimeHub where T : class
    {
        /// <summary>
        /// 加入当前集线器的组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        public virtual async Task JoinGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 离开当前集线器的组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        public virtual async Task LeaveGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }

        /// <summary>
        /// 对象释放事件
        /// </summary>
        public event EventHandler OnDisposing;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                EventHandler handler = this.OnDisposing;
                if (handler != null)
                {
                    handler.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}