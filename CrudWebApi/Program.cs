using Core.SettingsModels;
using CrudWebApi.Extensions;
using CrudWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureCors();
builder.Services.ConfigureValidators();

builder.Services.Configure<FileServiceSettings>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.ConfigureExceptionHandler();

app.Run();
