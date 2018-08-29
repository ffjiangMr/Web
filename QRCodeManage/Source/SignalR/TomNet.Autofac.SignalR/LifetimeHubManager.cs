using System;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;

using Autofac;

using Microsoft.AspNet.SignalR.Hubs;

using TomNet.Utility;


namespace TomNet.Autofac.SignalR
{
    /// <summary>
    /// 带生命周期作用域的Hub管理器
    /// </summary>
    internal class LifetimeHubManager : Disposable
    {
        private readonly ConcurrentDictionary<IHub, ILifetimeScope> _hubLifetimeScopes = new ConcurrentDictionary<IHub, ILifetimeScope>();
        public const string LifetimeScopeKey = "TomNet:signalr_lifetime_scope";

        public T ResolveHub<T>(Type type, ILifetimeScope lifetimeScope) where T : ILifetimeHub
        {
            ILifetimeScope lifetimeScope2 = lifetimeScope.BeginLifetimeScope();
            CallContext.LogicalSetData(LifetimeScopeKey, lifetimeScope2);
            T t = (T)lifetimeScope2.Resolve(type);
            t.OnDisposing += HubOnDisposing;
            _hubLifetimeScopes.TryAdd(t, lifetimeScope2);
            return t;
        }

        /// <summary>
        /// 重写以实现释放派生类资源的逻辑
        /// </summary>
        protected override void Disposing()
        {
            foreach (IHub current in _hubLifetimeScopes.Keys)
            {
                current.Dispose();
            }
        }

        private void HubOnDisposing(object sender, EventArgs eventArgs)
        {
            IHub hub = sender as IHub;
            ILifetimeScope lifetimeScope = null;
            bool flag = hub != null && _hubLifetimeScopes.TryRemove(hub, out lifetimeScope);
            if (flag && lifetimeScope != null)
            {
                CallContext.FreeNamedDataSlot(LifetimeScopeKey);
                lifetimeScope.Dispose();
            }
        }
    }
}