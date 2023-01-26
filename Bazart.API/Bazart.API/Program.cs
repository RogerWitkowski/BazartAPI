using System.Reflection;
using System.Text.Json.Serialization;
using Bazart.API.Middleware;
using Bazart.API.Repository.IRepository;
using Bazart.API.Repository;
using Bazart.API.Repository.Bazart.API.Repository;
using Bazart.DataAccess.DataAccess;
using Bazart.DataAccess.Seeder;
using Bazart.Models.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Bazart.EmailService.SettingModel;
using Bazart.EmailService.EmailService.IEmailService;
using Bazart.EmailService.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseNLog();

builder.Services.AddDbContext<BazartDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
//!Repository registered area
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<DataGenerator>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.User.AllowedUserNameCharacters = default;

        options.Password.RequiredLength = 10;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredUniqueChars = 2;

        options.User.RequireUniqueEmail = true;

        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

        options.SignIn.RequireConfirmedEmail = true;
    }).AddEntityFrameworkStores<BazartDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddXmlDataContractSerializerFormatters();
//!EmailSendingSettingsModel registered
builder.Services.Configure<EmailSendingSettingsModel>(builder.Configuration.GetSection("MailGun"));
//!EmailSender registered
builder.Services.AddSingleton<IEmailService, EmailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//!CORS policy created
var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins:ReactFrontOrigin").Value;
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendClient", policy => policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(origins: allowedOrigin)
        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//!add CORS policy
app.UseCors(policyName: "FrontendClient");

using (var scope = app.Services.CreateAsyncScope())
{
    var dataGenerator = scope.ServiceProvider.GetService<DataGenerator>();
    dataGenerator.GenerateData().GetAwaiter().GetResult();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();