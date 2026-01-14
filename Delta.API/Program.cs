using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Delta.Application.Interfaces;
using Delta.Application.Interfaces.Utilities;
using Delta.Application.Services;
using Delta.Application.Services.Utilities;
using Delta.Infrastructure.Repositories;
using Delta.Infrastructure.Repositories.Utilities;
using System.Text;
using IUserService = Delta.Application.Interfaces.Utilities.IUserService;
using Delta.Infrastructure.Persistence.EF;

var builder = WebApplication.CreateBuilder(args);

// ---------------------- Controllers ----------------------
builder.Services.AddControllers();

// ---------------------- Swagger ----------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------- DbContext ----------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ---------------------- Dependency Injection ----------------------
// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();


// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<MenuService>();

// ? Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


// JWT Settings from appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// TokenService (Clean Architecture: pass settings from API)
builder.Services.AddScoped<ITokenService>(sp =>
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// ---------------------- Apply Pending Migrations (Optional) ----------------------
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     db.Database.Migrate(); // Applies any pending migrations
// }

// ---------------------- Middleware ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ? Middlewares
app.UseHttpsRedirection();


app.UseCors("AllowAll");
// JWT authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
