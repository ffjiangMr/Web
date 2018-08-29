using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using TomNet.Core.Data;
using TomNet.Core.Data.Extensions;
using TomNet.Core.Dependency;
using TomNet.Core.Security;
using TomNet.Utility;


namespace TomNet.Data.Entity
{
    /// <summary>
    /// 上下文扩展辅助操作类
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 更新上下文中指定的实体的状态
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TKey">主键类型</typeparam>
        /// <param name="dbContext">上下文对象</param>
        /// <param name="entities">要更新的实体类型</param>
        public static void Update<TEntity, TKey>(this DbContext dbContext, params TEntity[] entities)
            where TEntity : class, IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            dbContext.CheckNotNull("dbContext");
            entities.CheckNotNull("entities");

            DbSet<TEntity> dbSet = dbContext.Set<TEntity>();
            foreach (TEntity entity in entities)
            {
                try
                {
                    DbEntityEntry<TEntity> entry = dbContext.Entry(entity);
                    if (entry.State == EntityState.Detached)
                    {
                        dbSet.Attach(entity);
                        entry.State = EntityState.Modified;
                    }
                }
                catch (InvalidOperationException)
                {
                    TEntity oldEntity = dbSet.Find(entity.Id);
                    dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
                }
            }
        }        
    }
}