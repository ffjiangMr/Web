using Source.Model.DbModels.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomNet.Data.Entity.Migrations;

namespace Source.HIMS.Core.Data
{
    public class CreateDatabaseSeedAction : ISeedAction
    {
        /// <summary>
        /// 定义种子数据初始化过程
        /// </summary>
        /// <param name="context">数据上下文</param>
        public void Action(DbContext context)
        {
            context.Set<SysAccountInfo>()
                .Add(new SysAccountInfo()
                {
                    SuName = "管理员",
                    Login = "admin",
                    Password = "admin",
                    Role="0",
                    CreatedTime = DateTime.Now,
                    Explain = "系统自动创建管理员"

                });

            context.Set<BaseDictionariesInfo>()
              .Add(new BaseDictionariesInfo()
              {
                  Type = "1",
                  KeyName = "Url",
                  ValueName = "http://localhost:13440/Mobile/Index?id=",
                  CreatedTime = DateTime.Now,
                  Explain = "系统自动创建管理员"

              });

            context.Set<BaseDictionariesInfo>()
           .Add(new BaseDictionariesInfo()
           {
               Type = "1",
               KeyName = "Scale",
               ValueName = "3",
               CreatedTime = DateTime.Now,
               Explain = "系统初始化"

           });

            context.Set<BaseDictionariesInfo>()
           .Add(new BaseDictionariesInfo()
           {
               Type = "1",
               KeyName = "Version",
               ValueName = "0",
               CreatedTime = DateTime.Now,
               Explain = "系统初始化"

           });

            context.Set<BaseDictionariesInfo>()
           .Add(new BaseDictionariesInfo()
           {
               Type = "1",
               KeyName = "XY",
               ValueName = "120",
               CreatedTime = DateTime.Now,
               Explain = "系统初始化"

           });

        }

        /// <summary>
        /// 获取 操作排序，数值越小越先执行
        /// </summary>
        public int Order
        {
            get { return 1; }
        }

    }
}
