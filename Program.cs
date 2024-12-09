using BankApplication.Data;
using BankApplication.Filter;
using BankApplication.Identity;
using BankApplication.Languages;
using BankApplication.Middlewares.exception;
using BankApplication.Middlewares.localization;
using BankApplication.Repository.account;
using BankApplication.Repository.service;
using BankApplication.Service.Account;
using BankApplication.Service.CustomerService;
using BankApplication.Service.LocalizationService;
using BankApplication.ServiceContracts;
using BankApplication.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Reflection;
using System.Text;


// Load configurations
var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder();

configurationBuilder.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables();

IConfiguration configuration = configurationBuilder.Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.Configure(options =>
{
    options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId;
});

builder.Host.UseSerilog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5299", "https://example.com") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//MeadiatR
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

// Localization setup
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddTransient<IJwtService, JwtService>();


builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));

    options.Filters.Add<ValidateModelAttribute>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

})
    .AddDataAnnotationsLocalization()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>

{
    var supportedCultures = new[]
{
    new CultureInfo("en-US"), 
    new CultureInfo("fr-FR"),
    new CultureInfo("es-ES")  
};
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddHttpContextAccessor();

// Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, string>>()
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, string>>();

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError("Authentication failed: {0}", context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options => { });

// Database and dependency injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddSingleton<ILocalizationService, LocalizationService>();


// Swagger
builder.Services.AddSwaggerGen(options =>
{
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.OperationFilter<AddAcceptLanguageHeader>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User API",
        Version = "v1",
        Description = "An API for managing users with localization support."
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,

                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// FluentValidation
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true; 
});

builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();


//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");


app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LocalizationMiddleware>();


app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankApplication v1");
    c.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{}



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
