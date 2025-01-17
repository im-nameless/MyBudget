using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

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
        var connection = Environment.GetEnvironmentVariable("DefaultConnection") ?? "Server=postgresql://postgres:rdTx5GQW73XOYMIK@chivalrously-intent-squirrel.data-1.use1.tembo.io:5432/postgres?sslmode=verify-full&sslrootcert=ca.crt;TrustServerCertificate=true";
        var optionsBuilder = new DbContextOptionsBuilder<MyBudgetContext>();
        optionsBuilder.UseNpgsql(connection)
                      .EnableSensitiveDataLogging()
                      .EnableDetailedErrors();

        return new MyBudgetContext(optionsBuilder.Options);
    }

}
