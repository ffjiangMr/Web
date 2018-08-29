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
    public class UserNumBoxController : BaseController
    {
        public IUserAccountContract  UserAccountContract { get; set; }
        public IUserNumBoxContract UserNumBoxContract { get; set; }
        public IUserNumberContract UserNumberContract { get; set; }

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
            var query = from t in (from uc in (UserNumBoxContract.Entities.Where(d => d.IsDeleted == false))
                                   join u in (UserAccountContract.Entities.Where(d => d.IsDeleted == false)) on uc.UaId equals u.Id
                                   join ub in UserNumberContract.Entities.Where(d => d.IsDeleted == false) on uc.Id equals ub.UNumBoxId
                                   into temp
                                   from tt in temp.DefaultIfEmpty()
                                   select new
                                   {
                                       uc.Id,
                                       u.UserName,
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
                            UserName = g.Max(p => p.UserName),
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
                query = query.Where(m => m.UserName.Contains(search));
            }
            //数据总数
            var total = query.Count();

            //默认给一个排序的字段
            query = query.OrderBy(m => m.Id);

            //分页(假如total == 0，则取消分页查询，提高性能)
            query = total > 0 ? query.Skip((pageIndex - 1) * pageSize).Take(pageSize)  : query;

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
                      UserStare=entity.UserStare,
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