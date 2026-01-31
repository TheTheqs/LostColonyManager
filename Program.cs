using LostColonyManager.Application.Interfaces;
using LostColonyManager.Application.UseCases;
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
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<IStructureRepository, StructureRepository>();
builder.Services.AddScoped<IPlanetStructureRepository, PlanetStructureRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddScoped<RegisterCampaignUseCase>();
builder.Services.AddScoped<RegisterRaceUseCase>();
builder.Services.AddScoped<RegisterPlanetUseCase>();
builder.Services.AddScoped<RegisterStructureUseCase>();
builder.Services.AddScoped<RegisterEventUseCase>();
builder.Services.AddScoped<GetCampaignUseCase>();
builder.Services.AddScoped<GetPlanetUseCase>();
builder.Services.AddScoped<GetRaceUseCase>();
builder.Services.AddScoped<GetEventUseCase>();
builder.Services.AddScoped<GetStructureUseCase>();
builder.Services.AddScoped<ManageAssociationUseCase>();
builder.Services.AddScoped<DeleteRaceUseCase>();
builder.Services.AddScoped<DeletePlanetUseCase>();
builder.Services.AddScoped<DeleteCampaignUseCase>();
builder.Services.AddScoped<GenerateConsequenceUseCase>();
builder.Services.AddScoped<GenerateChoiceUseCase>();
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
