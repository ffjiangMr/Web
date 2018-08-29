using System;


namespace TomNet.Web.SignalR.Client.Reflection
{
    /// <summary>
    /// Container for method name and parameters
    /// </summary>
    internal class MethodCallInfo
    {
        /// <summary> Name of reflected method </summary>
        public string MethodName { get; set; }

        /// <summary> Parameter values </summary>
        public Type[] ParameterTypes { get; set; }
    }
}