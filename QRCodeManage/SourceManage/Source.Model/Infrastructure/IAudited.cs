using System;

namespace Source.Model.Infrastructure
{
    /// <summary>
    /// 审核模型，在创建者数据模型上，添加审核人Id，审核时间
    /// </summary>
    public interface IAudited : ICreator
    {
        string AuditorId { get; set; }
        DateTime? AuditedTime { get; set; }
    }
}
 