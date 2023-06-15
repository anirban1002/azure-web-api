using azure_web_api.Data;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Host.ConfigureAppConfiguration((context, config) =>
//{
//    var settings = config.Build();
//    var keyVaultURL = settings["KeyVaultConfiguration:KeyVaultURL"];
//    var keyVaultClientId = settings["KeyVaultConfiguration:ClientId"];
//    var keyVaultClientSecret = settings["KeyVaultConfiguration:ClientSecret"];
//    config.AddAzureKeyVault(keyVaultURL, keyVaultClientId, keyVaultClientSecret, new DefaultKeyVaultSecretManager());
//});
builder.Services.AddControllers();
//builder.Services.AddAzureClients(azureClientFactoryBuilder =>
//{
//    azureClientFactoryBuilder.AddSecretClient(
//    builder.Configuration.GetSection("KeyVault"));
//});
builder.Services.AddScoped<IEngineerService, EngineerService>();
//builder.Services.AddSingleton<IKeyVaultManager, KeyVaultManager>();
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
