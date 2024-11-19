using System.Text.Json.Serialization;
using static Domain.Enumerators.Enumerators;

namespace Domain.Entities;

public class Income : BaseEntity
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public IncomeFrequencyEnum  Frequency { get; set; }
}
