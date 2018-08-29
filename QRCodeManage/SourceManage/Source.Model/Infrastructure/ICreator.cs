namespace Source.Model.Infrastructure
{
    /// <summary>
    /// 在常用数模模型上，添加创建者ID
    /// </summary>
    public interface ICreator : IGeneral
    {
        string CreatorId { get; set; }
    }
}
