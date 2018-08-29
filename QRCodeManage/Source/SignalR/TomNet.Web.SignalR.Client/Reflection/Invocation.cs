using System;


namespace TomNet.Web.SignalR.Client.Reflection
{
    /// <summary>
    /// Container for method name and parameter values
    /// </summary>
    internal class Invocation
    {
        /// <summary> Name of reflected method </summary>
        public string MethodName { get; set; }

        /// <summary> Parameter values </summary>
        public object[] ParameterValues { get; set; }

        /// <summary> Return type, or null if void return </summary>
        public Type ReturnType { get; set; }
    }
}