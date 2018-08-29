using System;
using System.Linq;
using System.Web.Mvc;
using Source.Admin.Web.Filters;
using Source.Core.Contracts.QRCode;
using Source.Model.DbModels.QRCode;
using TomNet.Web.Mvc;
using TomNet.Web.Mvc.UI;

namespace Source.Admin.Web.Controllers.Account
{
    public class UserAccountController : BaseController
    {
        public IUserAccountContract UserAccountContract { get; set; }
        public IUserAllocationContract UserAllocationContract { get; set; }
        public IUserNumBoxContract UserNumBoxContract { get; set; }
        public IUserNumberContract UserNumberContract { get; set; }
        #region 列表页面


        // GET: Dictionaries
        [GlobalAuthorization]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>AllocationIndexAsync
        /// 查询数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>-
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult IndexAsync(int pageIndex, int pageSize, string search = "")
        {
            //获取数据集合
            //var query = UserAccountContract.Entities.Where(d => d.IsDeleted == false);


            var query = from t in (from u in (UserAccountContract.Entities.Where(d => d.IsDeleted == false))
                                   join ua in UserAllocationContract.Entities.Where(d => d.IsDeleted == false) on u.Id equals ua.UaId
                                   into temp
                                   from tt in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       u.Id,
                                       u.UserName,
                                       u.Position,
                                       u.Phone,
                                       u.UserStare,
                                       UseCount = tt.NumCount,
                                       u.GuIdNumber,
                                       u.CreatedTime
                                   })
                        group t by t.Id into g
                        select new
                        {
                            Id = g.Key,
                            UserName = g.Max(p => p.UserName),
                            Position = g.Max(p => p.Position),
                            Phone = g.Max(p => p.Phone),
                            GuIdNumber = g.Max(p => p.GuIdNumber),
                            UserStare = g.Max(p => p.UserStare),
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
            if (total > 0) query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            //此处可以采用匿名对象 GridData<object>
            GridData<object> gridData = new GridData<object>() { Total = total, Rows = query.ToList() };

            //此处采用重写后的jsonResult
            return JsonEx(gridData, JsonRequestBehavior.AllowGet, "yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 企业分码列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserAllocationViwe(int id = 0)
        {
            if (id == 0) throw new Exception("参数错误");

            var entity = UserAccountContract.GetByKey(id);
            //    //后台容错，有异常数据直接抛出。框架会自动跳转到错误页面。
            if (entity == null) throw new Exception("未找到该实体");


            ViewData["entity"] = entity;
            return View();

        }

        /// <summary>
        /// 查询企业分码列表数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>-
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult AllocationIndexAsync(int pageIndex, int pageSize, string search = "", int uaid = 0)
        {
            //获取数据集合
            //var query = UserAccountContract.Entities.Where(d => d.IsDeleted == false);
           


            var query = from t in (from uc in (UserAllocationContract.Entities.Where(d => d.IsDeleted == false && d.UaId == uaid))
                                   join ub in UserNumBoxContract.Entities.Where(d => d.IsDeleted == false) on uc.Id equals ub.UAllocationId
                                   into temp
                                   from tt in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       uc.Id,
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
                            StartNumber = g.Max(p => p.StartNumber),
                            EndNumber = g.Max(p => p.EndNumber),
                            NumCount = g.Max(p => p.NumCount),
                            AllocationStare = g.Max(p => p.AllocationStare),
                            CreatedTime = g.Max(p => p.CreatedTime),
                            UseCount = g.Sum(p => (p.UseCount == null ? 0 : p.UseCount)),
                        };



            if (!string.IsNullOrWhiteSpace(search))
            {
                var result = new AjaxResult();
                long code = 0;
                if (!long.TryParse(search, out code))
                {
                    result.Type = AjaxResultType.Error;
                    result.Content = "搜索条件必须是数字编号";
                    return JsonEx(result);
                }
                query = query.Where(m => code >= m.StartNumber && code <= m.EndNumber);
            }
            //数据总数
            var total = query.Count();

            //默认给一个排序的字段
            query = query.OrderBy(m => m.Id);

            //分页(假如total == 0，则取消分页查询，提高性能)
            if (total > 0 )   query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            

            //此处可以采用匿名对象 GridData<object>
            GridData<object> gridData = new GridData<object>() { Total = total, Rows = query.ToList() };

            //此处采用重写后的jsonResult
            return JsonEx(gridData, JsonRequestBehavior.AllowGet, "yyyy-MM-dd HH:mm:ss");
        }




        /// <summary>
        /// 企业用码列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserNumBoxViwe(int id = 0)
        {
            if (id == 0) throw new Exception("参数错误");

            var entity = UserAccountContract.GetByKey(id);
            //    //后台容错，有异常数据直接抛出。框架会自动跳转到错误页面。
            if (entity == null) throw new Exception("未找到该实体");


            ViewData["entity"] = entity;
            return View();

        }

        /// <summary>
        /// 查询企业用码列表数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>-
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult NumBoxIndexAsync(int pageIndex, int pageSize, string search = "", int uaid = 0)
        {
            //获取数据集合
            //var query = UserAccountContract.Entities.Where(d => d.IsDeleted == false);


            //获取数据集合
            var query = from t in (from uc in (UserNumBoxContract.Entities.Where(d => d.IsDeleted == false && d.UaId == uaid))
                                   join ub in UserNumberContract.Entities.Where(d => d.IsDeleted == false) on uc.Id equals ub.UNumBoxId
                                   into temp
                                   from tt in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       uc.Id,
                                       uc.StartNumber,
                                       uc.EndNumber,
                                       uc.NumCount,
                                       uc.NumBoxStart,
                                       uc.ValueURL,
                                       UseCount = tt.NumCount,
                                       uc.CreatedTime
                                   })
                        group t by t.Id into g
                        select new
                        {
                            Id = g.Key,
                            StartNumber = g.Max(p => p.StartNumber),
                            EndNumber = g.Max(p => p.EndNumber),
                            NumCount = g.Max(p => p.NumCount),
                            NumBoxStart = g.Max(p => p.NumBoxStart),
                            CreatedTime = g.Max(p => p.CreatedTime),
                            UseCount = g.Sum(p => (p.UseCount == null ? 0 : p.UseCount)),
                            ValueURL = g.Max(p => p.ValueURL)
                        };



            if (!string.IsNullOrWhiteSpace(search))
            {
                var result = new AjaxResult();
                long code = 0;
                if (!long.TryParse(search, out code))
                {
                    result.Type = AjaxResultType.Error;
                    result.Content = "搜索条件必须是数字编号";
                    return JsonEx(result);
                }
                query = query.Where(m => code >= m.StartNumber && code <= m.EndNumber);
            }
            //数据总数
            var total = query.Count();

            //默认给一个排序的字段
            query = query.OrderBy(m => m.Id);

            //分页(假如total == 0，则取消分页查询，提高性能)
            if (total > 0) query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            //此处可以采用匿名对象 GridData<object>
            GridData<object> gridData = new GridData<object>() { Total = total, Rows = query.ToList() };

            //此处采用重写后的jsonResult
            return JsonEx(gridData, JsonRequestBehavior.AllowGet, "yyyy-MM-dd HH:mm:ss");
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

            var entity = UserAccountContract.GetByKey(id);
            //    //后台容错，有异常数据直接抛出。框架会自动跳转到错误页面。
            if (entity == null) throw new Exception("未找到该实体");


            ViewData["entity"] = entity;
            return View();

        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult EditAsync(UserAccount entity)
        {
            var result = new AjaxResult();
            try
            {
                var count = UserAccountContract.UpdateDirect(d => d.Id == entity.Id, d =>
                  new UserAccount
                  {
                      UserName = entity.UserName,
                      Position = entity.Position,
                      Phone = entity.Phone,
                      UserStare = entity.UserStare,
                      Password = entity.Password,
                      Explain = entity.Explain
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