namespace SchoolProvider.Database.Entities;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreateDateTime { get; set; }
    public DateTime? LastChangeDateTime { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteDateTime { get; set; }
}