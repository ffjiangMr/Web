using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using TomNet.Core.Configs.ConfigFile;
using TomNet.Core.Properties;
using TomNet.Utility.Extensions;


namespace TomNet.Core.Configs
{
    /// <summary>
    /// 数据上下文初始化配置
    /// </summary>
    public class DbContextInitializerConfig
    {
        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerConfig"/>类型的新实例
        /// </summary>
        public DbContextInitializerConfig()
        {
            EntityMapperAssemblies = new List<Assembly>();
        }

        /// <summary>
        /// 初始化一个<see cref="DbContextInitializerConfig"/>类型的新实例
        /// </summary>
        internal DbContextInitializerConfig(DbContextInitializerElement element)
        {
            Type type = Type.GetType(element.InitializerTypeName);
            if (type == null)
            {
                throw new InvalidOperationException(Resources.DbContextInitializerConfig_InitializerNotExists.FormatWith(element.InitializerTypeName));
            }
            InitializerType = type;

            string binPath = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            string[] mapperFiles = element.EntityMapperFiles.Split(',')
                .Select(fileName => fileName.EndsWith(".dll") ? fileName : fileName + ".dll")
                .Select(fileName => Path.Combine(binPath, fileName)).ToArray();
            EntityMapperAssemblies = mapperFiles.Select(Assembly.LoadFrom).ToList();

            if (element.CreateDatabaseInitializer != null && !element.CreateDatabaseInitializer.InitializerTypeName.IsMissing())
            {
                CreateDatabaseInitializerType = Type.GetType(element.CreateDatabaseInitializer.InitializerTypeName);
                if (CreateDatabaseInitializerType == null)
                {
                    throw new InvalidOperationException(Resources.ConfigFile_NameToTypeIsNull.FormatWith(element.CreateDatabaseInitializer.InitializerTypeName));
                }
            }
        }

        /// <summary>
        /// 获取或设置 数据上下文初始化类型
        /// </summary>
        public Type InitializerType { get; set; }

        /// <summary>
        /// 获取或设置 实体映射类型所在程序集集合
        /// </summary>
        public ICollection<Assembly> EntityMapperAssemblies { get; set; }

        /// <summary>
        /// 获取或设置 创建数据库初始化类型
        /// </summary>
        public Type CreateDatabaseInitializerType { get; set; }
    }
}