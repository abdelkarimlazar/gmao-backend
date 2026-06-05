using GmaoIntentApi.Data;
using GmaoIntentApi.Options;
using GmaoIntentApi.Repositories;
using GmaoIntentApi.Repositories.Interfaces;
using GmaoIntentApi.Services;
using GmaoIntentApi.Services.Interfaces;
using GmaoIntentApi.Middleware;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

//
// CONNECTION STRING
//

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found."
    );

//
// DATABASE
//

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString);
});

//
// JWT CONFIGURATION
//

var jwtSection = builder.Configuration.GetSection(JwtOptions.SectionName);

builder.Services.Configure<JwtOptions>(jwtSection);

var jwtOptions = jwtSection.Get<JwtOptions>()
    ?? throw new InvalidOperationException(
        "JWT configuration is missing."
    );

//
// CONTROLLERS
//

builder.Services.AddControllers();

//
// CORS
//

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//
// REPOSITORIES
//

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();

builder.Services.AddScoped<IMaintenanceTaskRepository,
    MaintenanceTaskRepository>();

builder.Services.AddScoped<IBreakdownRepository,
    BreakdownRepository>();

builder.Services.AddScoped<IInterventionHistoryRepository,
    InterventionHistoryRepository>();

builder.Services.AddScoped<INotificationRepository,
    NotificationRepository>();

//
// SERVICES
//

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IEquipmentService, EquipmentService>();

builder.Services.AddScoped<IMaintenanceTaskService,
    MaintenanceTaskService>();

builder.Services.AddScoped<IBreakdownService,
    BreakdownService>();

builder.Services.AddScoped<IInterventionHistoryService,
    InterventionHistoryService>();

builder.Services.AddScoped<IDashboardService,
    DashboardService>();

builder.Services.AddScoped<IPasswordHasherService,
    PasswordHasherService>();

builder.Services.AddScoped<ITokenService,
    TokenService>();

builder.Services.AddScoped<IAuthService,
    AuthService>();

builder.Services.AddScoped<INotificationService,
    NotificationService>();

builder.Services.AddScoped<IntentService>();

//
// JWT AUTHENTICATION
//

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = jwtOptions.Issuer,

            ValidAudience = jwtOptions.Audience,

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.Key)
                ),

            ClockSkew = TimeSpan.Zero
        };
});

//
// AUTHORIZATION
//

builder.Services.AddAuthorization();

//
// SWAGGER
//

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GmaoIntentApi",
        Version = "v1",
        Description =
            "REST API for GMAO management with JWT authentication."
    });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter JWT token like: Bearer your_token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

//
// BUILD APP
//

var app = builder.Build();

//
// PIPELINE
//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowReact");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();