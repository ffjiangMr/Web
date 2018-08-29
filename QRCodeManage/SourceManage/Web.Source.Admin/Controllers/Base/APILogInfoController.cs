using Source.Admin.Web.Models.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;
using Source.Admin.Web.Common;
using Source.Admin.Web.Filters;
using TomNet.Web.Mvc;
using TomNet.Web.Mvc.UI;
using Source.Core.Contracts.Base;
using Source.Model.DbModels.Base;

namespace Source.Admin.Web.Controllers.Base
{
    public class APILogInfoController : BaseController
    {
      public IAPILogInfoContract APILogInfoContract { get; set; }

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
        /// <param name="pageSize"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult IndexAsync(int pageIndex, int pageSize, string search = "")
        {
            var query = APILogInfoContract.Entities.Where(d => d.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.UserName.Contains(search));
            }
            //数据总数
            var total = query.Count();

            //默认给一个排序的字段
            query = query.OrderByDescending(m => m.Id);

            //分页(假如total == 0，则取消分页查询，提高性能)
            query = total > 0 ? query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                : Enumerable.Empty<APILogInfo>().AsQueryable();// null;

            //此处可以采用匿名对象 GridData<object>
            GridData<object> gridData = new GridData<object>() { Total = total, Rows = query.ToList() };

            //此处采用重写后的jsonResult
            return JsonEx(gridData, JsonRequestBehavior.AllowGet, "yyyy-MM-dd HH:mm:ss");
        }

        #endregion

    }
}