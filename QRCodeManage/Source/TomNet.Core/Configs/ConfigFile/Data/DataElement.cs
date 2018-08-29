using System.Configuration;


namespace TomNet.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据配置节点
    /// </summary>
    internal class DataElement : ConfigurationElement
    {
        private const string ContextKey = "contexts";

        /// <summary>
        /// 数据上下文配置节点集合
        /// </summary>
        [ConfigurationProperty(ContextKey)]
        public virtual ContextCollection Contexts
        {
            get { return (ContextCollection)base[ContextKey]; }
        }
    }
}