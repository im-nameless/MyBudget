using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyBudget.Infra.Data.Context
{
    public class MyBudgetContext : DbContext
    {
        public MyBudgetContext(DbContextOptions<MyBudgetContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
