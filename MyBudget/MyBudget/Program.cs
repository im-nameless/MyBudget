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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
