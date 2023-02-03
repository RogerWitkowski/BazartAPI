using Bazart.API.Configurations.Extensions.Add;
using Bazart.API.Configurations.Extensions.Use;
using Microsoft.AspNetCore.Cors.Infrastructure;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseNLog();

//!controllers added
builder.Services.AddControllersCollection();

//!dbContext added
builder.AddDbContext();

//!globalErrorHandlers added
builder.AddGlobalErrorHandlers();

//!Repository registered area
builder.Services.AddRepositories();

//!dataGenerator added
builder.AddDataGenerator();

//!autoMapper added
builder.AddAutoMapper();

//!userIdentity added
builder.AddUserIdentity();

//!controllersWithJsonOptions added
builder.AddControllersWithJsonOptions();

//!EmailSendingSettingsModel added
builder.AddEmailSendingSettingsModel();

//!EmailSender added
builder.AddEmailService();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//!CORS policy added
builder.AddCorsPolicy();

var app = builder.Build();
// Configure the HTTP request pipeline.

//!globalErrorMiddleware used
app.UseGlobalErrorHandlersMiddleware();

//!CORS policy used
app.UseCorsPolicy();

//!dataGenerator used
await app.UseDataGenerator();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();