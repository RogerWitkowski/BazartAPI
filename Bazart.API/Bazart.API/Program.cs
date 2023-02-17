using Bazart.API.Configurations.Extensions.Add;
using Bazart.API.Configurations.Extensions.Use;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseNLog();

//!controllers added
builder.Services.AddControllersCollection();

//!dbContext added
builder.AddBazartDbContext();

//!globalErrorHandlers added
builder.AddGlobalErrorHandlers();

//!Repository registered area
builder.Services.AddRepositories();

//!dataGenerator added
builder.AddDataGenerator();

//!autoMapper added
builder.AddAutoMapper();

//!jwtToken authentication added
builder.AddAuthenticationSettings();

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
//!CORS policy used
app.UseCorsPolicy();

//!globalErrorMiddleware used
app.UseGlobalErrorHandlersMiddleware();

//!dataGenerator used
await app.UseDataGenerator();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();