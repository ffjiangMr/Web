using System.Configuration;


namespace TomNet.Core.Configs.ConfigFile
{
    /// <summary>
    /// 数据库创建策略配置节点
    /// </summary>
    internal class CreateDatabaseInitializerElement : ConfigurationElement
    {
        private const string TypeKey = "type";

        /// <summary>
        /// 获取或设置 数据库创建策略类型名称
        /// </summary>
        [ConfigurationProperty(TypeKey, IsRequired = true)]
        public virtual string InitializerTypeName
        {
            get { return (string)this[TypeKey]; } 
            set { this[TypeKey] = value; }
        }
    }
}