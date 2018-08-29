using System;
using System.ComponentModel.DataAnnotations;

namespace Source.Model.Infrastructure
{
    public abstract class CreatorBase<TKey> : GeneralBase<TKey>, ICreator
            where TKey : IEquatable<TKey>
    {
        [StringLength(50)]
        public string CreatorId { get; set; }
    }
}
