using System;
using System.ComponentModel.DataAnnotations;

namespace Source.Model.Infrastructure
{
    public abstract class AuditedBase<TKey> : CreatorBase<TKey>, IAudited
            where TKey : IEquatable<TKey>
    {
        [StringLength(50)]
        public string AuditorId { get; set; }
        public DateTime? AuditedTime { get; set; }

        public AuditedBase()
        {
            AuditedTime = DateTime.Now;
        }

    }
}
