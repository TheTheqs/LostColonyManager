using LostColonyManager.Application.UseCases;
using LostColonyManager.Domain.Interfaces;
using LostColonyManager.Infra.Data;
using LostColonyManager.Infra.Data.Repositories;
using LostColonyManager.Interface.ExceptionHandling;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Controllers + OpenAPI (Swagger)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Exception Handling (Global)
builder.Services.AddApiExceptionHandling();
#endregion

#region Data (EF Core + PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);
#endregion

#region Dependency Injection (Application / Infra)
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<RegisterRaceUseCase>();
builder.Services.AddScoped<DeleteRaceUseCase>();
#endregion

var app = builder.Build();

#region Development tools
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

#region HTTP Pipeline
app.UseHttpsRedirection();

app.UseApiExceptionHandling();

app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
