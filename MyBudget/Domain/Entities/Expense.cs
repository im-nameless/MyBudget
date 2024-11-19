using System.Text.Json.Serialization;
using static Domain.Enumerators.Enumerators;

namespace Domain.Entities;

public class Expense : BaseEntity
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public ExpenseTypeEnum Type { get; set; }
}
