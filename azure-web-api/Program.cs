using azure_web_api.Data;
using Microsoft.Extensions.Azure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAzureClients(azureClientFactoryBuilder =>
{
    azureClientFactoryBuilder.AddSecretClient(
    builder.Configuration.GetSection("KeyVault"));
});
builder.Services.AddScoped<IEngineerService, EngineerService>();
builder.Services.AddSingleton<IKeyVaultManager, KeyVaultManager>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
