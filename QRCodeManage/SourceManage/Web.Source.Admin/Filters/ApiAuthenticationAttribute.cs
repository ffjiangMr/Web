using System.Linq;
using TomNet.Web.WebApi.Filters;
using Source.Admin.Web.Controllers.Api;

namespace Source.Admin.Web.Filters
{
    public class ApiAuthenticationAttribute : BasicApiAuthenticationAttribute
    {


        //public IUserInfoModelContract UserContract { set; get; }

        //public override bool ValidateToken(string token)
        //{

        //    token = token.Replace(' ', '+');
        //    string tRealitytoken = BaseWebApiController.GetRealityToken(token);//解密
        //    //验证内存中是否存在
        //    if (BaseWebApiController.DicToken != null && BaseWebApiController.DicToken.Count() > 0)
        //    {
        //        if (BaseWebApiController.DicToken.ContainsValue(tRealitytoken))
        //        {
        //            return true;
        //        }
        //    }
        //    //验证数据库是否存在
        //    var userData = UserContract.Entities.Where(d => d.Token == tRealitytoken).FirstOrDefault();
        //    if (userData != null)
        //    {
        //        if(BaseWebApiController.GetUserByToken(tRealitytoken,userData))
        //        {
        //            //BaseWebApiController.GetNewToken(UserContract,userData.Id);
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    else
        //        return false;
        //    //base64的字符串
        //    //字符串会转成一个byte[]
        //    //结构是 uid + uname + token串
        //    //去数据库中验证，合法后，刷新token，返回给客户，并且保存到数据库
         
        //}






    }
}