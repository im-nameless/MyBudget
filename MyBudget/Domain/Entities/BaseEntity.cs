namespace Domain.Entities;
public class BaseEntity
{
    public Guid Id { get; set; }
    public int AlternateId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
    public bool IsActive { get; set; } = true;
}
