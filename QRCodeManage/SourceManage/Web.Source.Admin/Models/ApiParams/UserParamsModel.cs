using Source.Model.DbModels.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Admin.Web.Models.ApiParams
{
    [NotMapped]
    public class UserParamsModel : UserInfoModel
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页内数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int DataCounts { get; set; }

        public string ResPassWord { get; set; }

        public string OldPassWord { get; set; }
    }
}
