﻿// <autogenerated>
//   This file was generated by T4 code generator ServiceCodeScript.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

// -----------------------------------------------------------------------
//  <copyright file="UserNumberService.Generated.cs">
//      Copyright (c) 2016 TomNet. All rights reserved.
//  </copyright>
//  <last-editor>TomNet</last-editor>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using TomNet.Core.Data;
using TomNet.Utility.Data;
using Source.Model.DbModels.QRCode;
using Source.Core.Contracts.QRCode;

namespace Source.Core.Services.QRCode
{
    /// <summary>
    /// 服务类 UserNumber -- Source.Model.DbModels.QRCode.UserNumber
    /// </summary> 
	public partial class UserNumberService : IUserNumberContract
    { 
	    /// <summary>
        /// 构造函数
        /// </summary>
		public UserNumberService() { }

        /// <summary>
        /// 获取或设置 用户信息仓储对象
        /// </summary>
        public IRepository<UserNumber, int> Repository { get; set; }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，数据将使用不跟踪变化的方式来查询，当数据用于展现时，推荐使用此数据集，如果用于新增，更新，删除时，请使用<see cref="TrackEntities"/>数据集
        /// </summary>
        public IQueryable<UserNumber> Entities
        {
            get { return Repository.Entities; }
        }

        /// <summary>
        /// 获取 当前实体类型的查询数据集，当数据用于新增，更新，删除时，使用此数据集，如果数据用于展现，推荐使用<see cref="Entities"/>数据集
        /// </summary>
        public IQueryable<UserNumber> TrackEntities
        {
            get { return Repository.TrackEntities; }
        }

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Insert(UserNumber entity)
        {
            return Repository.Insert(entity);
        }

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Insert(IEnumerable<UserNumber> entities)
        {
            return Repository.Insert(entities);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(UserNumber entity)
        {
            return Repository.Delete(entity);
        }

        /// <summary>
        /// 删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public virtual int Delete(int key)
        {
            return Repository.Delete(key);
        }

        /// <summary>
        /// 删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(Expression<Func<UserNumber, bool>> predicate)
        {
            return Repository.Delete(predicate);
        }

        /// <summary>
        /// 批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public int Delete(IEnumerable<UserNumber> entities)
        {
            return Repository.Delete(entities);
        }

        /// <summary>
        /// 直接删除指定编号的实体，此方法不支持事务
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns></returns>
        public int DeleteDirect(int key)
        {
            return Repository.DeleteDirect(key);
        }

        /// <summary>
        /// 直接删除所有符合特定条件的实体，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public int DeleteDirect(Expression<Func<UserNumber, bool>> predicate)
        {
            return Repository.DeleteDirect(predicate);
        }

        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public int Update(UserNumber entity)
        {
            return Repository.Update(entity);
        }

        /// <summary>
        /// 直接更新指定编号的数据，此方法不支持事务
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public int UpdateDirect(int key, Expression<Func<UserNumber, UserNumber>> updatExpression)
        {
            return Repository.UpdateDirect(key, updatExpression);
        }

        /// <summary>
        /// 直接更新指定条件的数据，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public int UpdateDirect(Expression<Func<UserNumber, bool>> predicate, Expression<Func<UserNumber, UserNumber>> updatExpression)
        {
            return Repository.UpdateDirect(predicate, updatExpression);
        }

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        public bool CheckExists(Expression<Func<UserNumber, bool>> predicate, int id = default(int))
        {
            return Repository.CheckExists(predicate, id);
        }

        /// <summary>
        /// 查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public UserNumber GetByKey(int key)
        {
            return Repository.GetByKey(key);
        }

        /// <summary>
        /// 获取贪婪加载导航属性的查询数据集
        /// </summary>
        /// <param name="path">属性表达式，表示要贪婪加载的导航属性</param>
        /// <returns>查询数据集</returns>
        public IQueryable<UserNumber> GetInclude<TProperty>(Expression<Func<UserNumber, TProperty>> path)
        {
            return Repository.GetInclude(path);
        }

        /// <summary>
        /// 获取贪婪加载多个导航属性的查询数据集
        /// </summary>
        /// <param name="paths">要贪婪加载的导航属性名称数组</param>
        /// <returns>查询数据集</returns>
        public IQueryable<UserNumber> GetIncludes(params string[] paths)
        {
            return Repository.GetIncludes(paths);
        }

        /// <summary>
        /// 创建一个原始 SQL 查询，该查询将返回此集中的实体。 
        /// 默认情况下，上下文会跟踪返回的实体；可通过对返回的 DbRawSqlQuery 调用 AsNoTracking 来更改此设置。 请注意返回实体的类型始终是此集的类型，而不会是派生的类型。 如果查询的一个或多个表可能包含其他实体类型的数据，则必须编写适当的 SQL 查询以确保只返回适当类型的实体。 与接受 SQL 的任何 API 一样，对任何用户输入进行参数化以便避免 SQL 注入攻击是十分重要的。 您可以在 SQL 查询字符串中包含参数占位符，然后将参数值作为附加参数提供。 您提供的任何参数值都将自动转换为 DbParameter。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); 或者，您还可以构造一个 DbParameter 并将它提供给 SqlQuery。 这允许您在 SQL 查询字符串中使用命名参数。 context.Set(typeof(Blog)).SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        /// </summary>
        /// <param name="trackEnabled">是否跟踪返回实体</param>
        /// <param name="sql">SQL 查询字符串。</param>
        /// <param name="parameters">要应用于 SQL 查询字符串的参数。 如果使用输出参数，则它们的值在完全读取结果之前不可用。 这是由于 DbDataReader 的基础行为而导致的，有关详细信息，请参见 http://go.microsoft.com/fwlink/?LinkID=398589。</param>
        /// <returns></returns>
        public IEnumerable<UserNumber> SqlQuery(string sql, bool trackEnabled = true, params object[] parameters)
        {
            return Repository.SqlQuery(sql, trackEnabled, parameters);
        }

        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> InsertAsync(UserNumber entity)
        {
            return Repository.InsertAsync(entity);
        }

        /// <summary>
        /// 异步批量插入实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> InsertAsync(IEnumerable<UserNumber> entities)
        {
            return Repository.InsertAsync(entities);
        }

        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> DeleteAsync(UserNumber entity)
        {
            return Repository.DeleteAsync(entity);
        }

        /// <summary>
        /// 异步删除指定编号的实体
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> DeleteAsync(int key)
        {
            return Repository.DeleteAsync(key);
        }

        /// <summary>
        /// 异步删除所有符合特定条件的实体
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> DeleteAsync(Expression<Func<UserNumber, bool>> predicate)
        {
            return Repository.DeleteAsync(predicate);
        }

        /// <summary>
        /// 异步批量删除删除实体
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> DeleteAsync(IEnumerable<UserNumber> entities)
        {
            return Repository.DeleteAsync(entities);
        }

        /// <summary>
        /// 直接删除指定编号的实体，此方法不支持事务
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns></returns>
        public Task<int> DeleteDirectAsync(int key)
        {
            return Repository.DeleteDirectAsync(key);
        }

        /// <summary>
        /// 直接删除所有符合特定条件的实体，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> DeleteDirectAsync(Expression<Func<UserNumber, bool>> predicate)
        {
            return Repository.DeleteDirectAsync(predicate);
        }

        /// <summary>
        /// 异步更新实体对象
        /// </summary>
        /// <param name="entity">更新后的实体对象</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> UpdateAsync(UserNumber entity)
        {
            return Repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 直接更新指定编号的数据，此方法不支持事务
        /// </summary>
        /// <param name="key">实体编号</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> UpdateDirectAsync(int key, Expression<Func<UserNumber, UserNumber>> updatExpression)
        {
            return Repository.UpdateDirectAsync(key, updatExpression);
        }

        /// <summary>
        /// 直接更新指定条件的数据，此方法不支持事务
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="updatExpression">更新属性表达式</param>
        /// <returns>操作影响的行数</returns>
        public Task<int> UpdateDirectAsync(Expression<Func<UserNumber, bool>> predicate, Expression<Func<UserNumber, UserNumber>> updatExpression)
        {
            return Repository.UpdateDirectAsync(predicate, updatExpression);
        }

        /// <summary>
        /// 异步检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件谓语表达式</param>
        /// <param name="id">编辑的实体标识</param>
        /// <returns>是否存在</returns>
        public Task<bool> CheckExistsAsync(Expression<Func<UserNumber, bool>> predicate, int id = default(int))
        {
            return Repository.CheckExistsAsync(predicate, id);
        }

        /// <summary>
        /// 异步查找指定主键的实体
        /// </summary>
        /// <param name="key">实体主键</param>
        /// <returns>符合主键的实体，不存在时返回null</returns>
        public Task<UserNumber> GetByKeyAsync(int key)
        {
            return Repository.GetByKeyAsync(key);
        }
    }
}
