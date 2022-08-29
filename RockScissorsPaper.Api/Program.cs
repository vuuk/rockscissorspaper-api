using RockScissorsPaper.Api.Contracts;
using RockScissorsPaper.Api.Validators;
using RockScissorsPaper.Api.Repositories;
using RockScissorsPaper.Api.BusinessLogic;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IGameStateRepository, GameStateRepository>();
builder.Services.AddScoped<IGameEngine, GameEngine>();
builder.Services.AddScoped<IInputValidator, GameValidator>();

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
