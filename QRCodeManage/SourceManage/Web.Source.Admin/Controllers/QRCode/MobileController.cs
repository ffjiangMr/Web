using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Source.Admin.Web.Filters;
using Source.Core.Contracts.Base;
using Source.Core.Contracts.QRCode;
using Source.Model.DbModels.QRCode;
using TomNet.Web.Mvc;
using TomNet.Web.Mvc.UI;

namespace Source.Admin.Web.Controllers.Account
{
    public class MobileController : BaseController
    {
        public IBaseDictionariesInfoContract DictionariesContract { get; set; }
        public IUserAccountContract UserAccountContract { get; set; }
        public IUserNumBoxContract UserNumBoxContract { get; set; }
        public IUserNumberContract UserNumberContract { get; set; }

        #region 页面


        // GET: Dictionaries
        public ActionResult Index(string id = "0")
        {
            FormsAuthentication.SignOut();

            long code = Convert.ToInt64(id);

            var dataall = UserNumberContract.Entities.Where(d => d.IsDeleted == false && d.StartNumber <= code && d.EndNumber >= code).FirstOrDefault();
            if (dataall == null) ViewData["error"] = "当前码无效";
            else
            {
                var udata = UserAccountContract.Entities.Where(d => d.IsDeleted == false && d.Id == dataall.UaId).FirstOrDefault();
                if (udata == null || udata.UserStare != Convert.ToInt32(UserStare.通过))
                    ViewData["error"] = "当前码用户无效";
                else
                {
                    var data = UserNumberContract.Entities.Where(d => d.IsDeleted == false && d.StartNumber <= code && d.EndNumber >= code).FirstOrDefault();
                    //var url = data.ValueURL;
                    ViewData["url"] = data.ValueURL;
                }
            }
            return View();
        }

        #endregion



    }
}