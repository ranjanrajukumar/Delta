using Delta.Application.Interfaces;
using Delta.Application.Interfaces.Common;
using Delta.Application.Interfaces.Utilities;
using Delta.Application.Services;
using Delta.Application.Services.Common;
using Delta.Application.Services.Utilities;
using Delta.Infrastructure.Persistence.Dapper;
using Delta.Infrastructure.Persistence.EF;
using Delta.Infrastructure.Repositories;
using Delta.Infrastructure.Repositories.Common;
using Delta.Infrastructure.Repositories.Utilities;
using Delta.Shared.Logging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Text;
using Vidya.API.Middleware;

using IUserService = Delta.Application.Interfaces.Utilities.IUserService;

var builder = WebApplication.CreateBuilder(args);

// ---------------------- GLOBAL LOGGING ----------------------
builder.Host.UseDeltaLogging();

// ---------------------- Controllers ----------------------
builder.Services.AddControllers();

// ---------------------- API Versioning ----------------------
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// ---------------------- Swagger ----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------- DbContext ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Dapper
builder.Services.AddScoped<IDapperContext, DapperContext>();

// ---------------------- Dependency Injection ----------------------

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IMenuFormRightsRepository, MenuFormRightsRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<IMenuFormRightsService, MenuFormRightsService>();
builder.Services.AddScoped<ICommonSearchService, CommonSearchService>();
builder.Services.AddScoped<ICommonSearchRepository, CommonSearchRepository>();

// User Context (Token + Claims)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();

// ---------------------- CORS ----------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// ---------------------- JWT Settings ----------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Token Service
builder.Services.AddScoped<ITokenService>(_ =>
    new TokenService(
        jwtSettings["SecretKey"],
        jwtSettings["Issuer"],
        jwtSettings["Audience"],
        Convert.ToDouble(jwtSettings["ExpiryMinutes"])
    )
);

// ---------------------- JWT Authentication ----------------------
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

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])
        ),

        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// ---------------------- Middleware Pipeline ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseMiddleware<UserContextMiddleware>(); // ✅ Token + Claims
app.UseAuthorization();

app.MapControllers();

app.Run();
