using MyBudget.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

ConfigurationIoC.LoadServices(builder.Services, builder.Configuration);
ConfigurationIoC.LoadDatabase(builder.Services, builder.Configuration);
ConfigurationIoC.LoadMapper(builder.Services);
ConfigurationIoC.LoadSwagger(builder.Services);
ConfigurationIoC.AddAuthentication(builder.Services, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors_policy", builder =>
    {
        builder.AllowAnyOrigin()
               .WithMethods("POST", "GET", "OPTIONS", "PUT", "PATCH", "DELETE")
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors_policy");


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
