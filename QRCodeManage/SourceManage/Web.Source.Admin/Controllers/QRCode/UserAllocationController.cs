using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Source.Admin.Web.Common;
using Source.Admin.Web.Filters;
using Source.Admin.Web.Models.ViewModel;
using Source.Core.Contracts.Base;
using Source.Core.Contracts.QRCode;
using Source.Model.DbModels.QRCode;
using TomNet.Web.Mvc;
using TomNet.Web.Mvc.UI;

namespace Source.Admin.Web.Controllers.Account
{
    public class UserAllocationController : BaseController
    {
        public IBaseDictionariesInfoContract DictionariesContract { get; set; }
        public IUserAccountContract UserAccountContract { get; set; }
        public IUserNumBoxContract UserNumBoxContract { get; set; }
        public IUserAllocationContract UserAllocationContract { get; set; }
        LoginModel mLogin = CookiesManagement.GetLoginModel(CookiesManagement.GetTicket());

        #region 列表页面


        // GET: Dictionaries
        [GlobalAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>-
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult IndexAsync(int pageIndex, int pageSize, string search = "")
        {
            //获取数据集合
            //var query = from ua in (UserAllocationContract.Entities.Where(d => d.IsDeleted == false))
            //            join u in (UserAccountContract.Entities.Where(d => d.IsDeleted == false)) on ua.UaId equals u.Id
            //            // into temp from tt in temp.DefaultIfEmpty()
            //            select new
            //            {
            //                ua.Id,
            //                u.UserName,
            //                ua.AllocationStare,
            //                ua.CreatedTime,
            //                ua.StartNumber,
            //                ua.EndNumber,
            //                ua.NumCount
            //            };


            var query = from t in (from uc in (UserAllocationContract.Entities.Where(d => d.IsDeleted == false))
                                   join u in (UserAccountContract.Entities.Where(d => d.IsDeleted == false)) on uc.UaId equals u.Id
                                   join ub in UserNumBoxContract.Entities.Where(d => d.IsDeleted == false) on uc.Id equals ub.UAllocationId
                                   into temp
                                   from tt in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       uc.Id,
                                       u.UserName,
                                       uc.StartNumber,
                                       uc.EndNumber,
                                       uc.NumCount,
                                       uc.AllocationStare,
                                       UseCount = tt.NumCount,
                                       uc.CreatedTime
                                   })
                        group t by t.Id into g
                        select new
                        {
                            Id = g.Key,
                            UserName = g.Max(p => p.UserName),
                            StartNumber = g.Max(p => p.StartNumber),
                            EndNumber = g.Max(p => p.EndNumber),
                            NumCount = g.Max(p => p.NumCount),
                            AllocationStare = g.Max(p => p.AllocationStare),
                            CreatedTime = g.Max(p => p.CreatedTime),
                            UseCount = g.Sum(p => (p.UseCount == null ? 0 : p.UseCount)),
                        };


            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.UserName.Contains(search));
            }
            //数据总数
            var total = query.Count();

            //默认给一个排序的字段
            query = query.OrderBy(m => m.Id);

            //分页(假如total == 0，则取消分页查询，提高性能)
            query = total > 0 ? query.Skip((pageIndex - 1) * pageSize).Take(pageSize) : query;

            //此处可以采用匿名对象 GridData<object>
            GridData<object> gridData = new GridData<object>() { Total = total, Rows = query.ToList() };

            //此处采用重写后的jsonResult
            return JsonEx(gridData, JsonRequestBehavior.AllowGet, "yyyy-MM-dd HH:mm");
        }


        #endregion


        #region 新增部分

        public ActionResult Insert()
        {
            var userlist = UserAccountContract.Entities.Where(d => d.IsDeleted == false).ToList();
            ViewData["userlist"] = userlist;
            //获取最大码
            var data = UserAllocationContract.Entities.Where(d => d.IsDeleted == false).ToList();
            if (data == null || data.Count == 0)
            {
                ViewData["StartNumber"] = 100000001;
            }
            else
            {
                ViewData["StartNumber"] = data.Max(d => d.EndNumber) + 1;
            }


            return View();
        }

        public ActionResult InsertAsync(UserAllocation entity)
        {

            entity.CreatorId = mLogin.Id.ToString();
            entity.CreatedTime = DateTime.Now;

            //所有AJAX的结果，返回统一数据格式
            var result = new AjaxResult();
            try
            {
                if (UserAccountContract.GetByKey(entity.UaId).UserStare != 2)
                {
                    result.Type = AjaxResultType.Error;
                    result.Content = "当前账户没有通过或已经被禁用";
                }
                else
                {
                    //判断是否已经存在码被分配
                    var isok = UserAllocationContract.Entities.Where(d => d.IsDeleted == false && ((d.StartNumber <= entity.StartNumber && d.EndNumber >= entity.StartNumber) ||
                     (d.StartNumber <= entity.EndNumber && d.EndNumber >= entity.EndNumber) || (d.StartNumber >= entity.StartNumber && d.StartNumber <= entity.EndNumber) ||
                     (d.EndNumber >= entity.StartNumber && d.EndNumber <= entity.EndNumber))).Count() > 0;
                    if (isok)
                    {
                        result.Type = AjaxResultType.Error;
                        result.Content = "您输入的码已经被分配";
                    }
                    else
                    {
                        var count = UserAllocationContract.Insert(entity);
                        if (count > 0)
                        {
                            result.Type = AjaxResultType.Success;
                            result.Content = "录入成功";
                        }
                        else
                        {
                            result.Type = AjaxResultType.Error;
                            result.Content = "录入失败";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Type = AjaxResultType.Error;
                result.Content = "异常操作";
            }
            return JsonEx(result);
        }



        #endregion


        #region 编辑部分



        /// <summary>
        /// 加载编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id == 0) throw new Exception("参数错误");

            var entity = UserAllocationContract.GetByKey(id);
            //    //后台容错，有异常数据直接抛出。框架会自动跳转到错误页面。
            if (entity == null) throw new Exception("未找到该实体");

            var userlist = UserAccountContract.Entities.Where(d => d.IsDeleted == false).ToList();
            ViewData["userlist"] = userlist;
            ViewData["entity"] = entity;
            ViewData["IsUser"] = UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.UAllocationId == entity.Id).Count() > 0 ;
            return View();

        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult EditAsync(UserAllocation entity)
        {
            var result = new AjaxResult();
            try
            {
                //判断是否已经存在码被分配
                var isok = UserAllocationContract.Entities.Where(d => d.IsDeleted == false && d.Id != entity.Id && ((d.StartNumber <= entity.StartNumber && d.EndNumber >= entity.StartNumber) ||
                 (d.StartNumber <= entity.EndNumber && d.EndNumber >= entity.EndNumber) || (d.StartNumber >= entity.StartNumber && d.StartNumber <= entity.EndNumber) ||
                 (d.EndNumber >= entity.StartNumber && d.EndNumber <= entity.EndNumber))).Count() > 0;

                if (isok)
                {
                    result.Type = AjaxResultType.Error;
                    result.Content = "您输入的码已经被分配";
                }
                else
                {
                    var count = UserAllocationContract.UpdateDirect(d => d.Id == entity.Id, d =>
                      new UserAllocation
                      {
                          UaId = entity.UaId,
                          AllocationStare = entity.AllocationStare,
                          StartNumber = entity.StartNumber,
                          EndNumber = entity.EndNumber,
                          NumCount = entity.NumCount

                      });
                    if (count > 0)
                    {
                        result.Type = AjaxResultType.Success;
                        result.Content = "修改成功";
                    }
                    else
                    {
                        result.Type = AjaxResultType.Error;
                        result.Content = "修改失败";
                    }
                }
            }
            catch
            {
                result.Type = AjaxResultType.Error;
                result.Content = "异常操作";
            }
            return JsonEx(result);
        }

        /// <summary>
        /// 加载导出Excel页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Excel(int id = 0)
        {
            if (id == 0)
            {
                var entity = new UserAllocation()
                {
                    Id = 0,
                    AllocationStare = 1,
                    StartNumber = 1,
                    EndNumber = 1,
                    NumCount = 1,
                    UaId = 1
                };
                ViewData["entity"] = entity;
            }
            else
            {
                var entity = UserAllocationContract.GetByKey(id);
                //    //后台容错，有异常数据直接抛出。框架会自动跳转到错误页面。
                if (entity == null) throw new Exception("未找到该实体");

                ViewData["entity"] = entity;
            }


            return View();

        }



        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult ExcelAsync(ParamsExcel entity)
        {
            var result = new AjaxResult();
            try
            {
                var dic = DictionariesContract.Entities;

                DataTable table = new DataTable();
                table.Columns.Add("编号", typeof(string));
                table.Columns.Add("内容", typeof(string));
                for (long i = entity.StartNumber; i <= entity.EndNumber; i++)
                {
                    table.Rows.Add(i.ToString(), dic.FirstOrDefault(d => d.KeyName == "Url").ValueName + i);
                }
                //string FellPathName = dic.FirstOrDefault(d => d.KeyName == "UplodPath").ValueName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"; ;

                //var bye = ExcelHelp.AuthGetFileData(table, FellPathName);

                result.Type = AjaxResultType.Success;
                result.Content = "获取成功-正在下载";
                result.Data = table; 

            }
            catch
            {
                result.Type = AjaxResultType.Error;
                result.Content = "异常操作";
            }
            return JsonEx(result);
        }

        #endregion


        #region 删除部分

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult DeleteAsync(string ids)
        {
            var result = new AjaxResult();
            try
            {
                string[] idsStr = ids.Split(new char[] { ',' });
                int[] idsInt = Array.ConvertAll(idsStr, id => Convert.ToInt32(id));

                if (UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && idsInt.Contains(d.UAllocationId)).Count() > 0)
                {
                    result.Type = AjaxResultType.Error;
                    result.Content = "您要删除数据已经被使用";

                }
                else
                {
                    UserAllocationContract.UpdateDirect(m => idsInt.Contains(m.Id), d => new UserAllocation { IsDeleted = true });

                    result.Type = AjaxResultType.Success;
                    result.Content = "删除成功";
                }
            }
            catch
            {
                result.Type = AjaxResultType.Error;
                result.Content = "异常操作";
            }
            return JsonEx(result);
        }

        #endregion
    }
}