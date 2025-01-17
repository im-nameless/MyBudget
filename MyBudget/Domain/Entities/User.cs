using System.Text.Json.Serialization;

namespace Domain.Entities;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required DateTime Birthdate { get; set; }
    public string? Phone { get; set; } = null;
    public List<Income>? Incomes { get; set; } = new List<Income>();
    public List<Expense>? Expenses { get; set; } = new List<Expense>();
}
