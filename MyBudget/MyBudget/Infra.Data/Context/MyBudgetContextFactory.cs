using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyBudget.Infra.Data.Context;

public class MyBudgetContextFactory : IDesignTimeDbContextFactory<MyBudgetContext>
{
    public MyBudgetContext CreateDbContext()
    {
        string[] args = Array.Empty<string>();
        return CreateDbContext(args);
    }

    public MyBudgetContext CreateDbContext(string[] args)
    {
        var connection = Environment.GetEnvironmentVariable("DefaultConnection") ?? "Server=.\\SQLExpress01;Database=MyBudgetDb;Trusted_Connection=true;TrustServerCertificate=true";
        var optionsBuilder = new DbContextOptionsBuilder<MyBudgetContext>();
        optionsBuilder.UseSqlServer(connection)
                      .EnableSensitiveDataLogging()
                      .EnableDetailedErrors();

        return new MyBudgetContext(optionsBuilder.Options);
    }

}
