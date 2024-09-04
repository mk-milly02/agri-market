using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AgriMarket.API.Data;
using AgriMarket.API.Profiles;
using AgriMarket.API.Repositories.Auth.Interfaces;
using AgriMarket.API.Repositories.Auth.Implementations;
using AgriMarket.API.Repositories.Email;
using AgriMarket.API.Models.Domain.Auth;
using dotenv.net;
using CloudinaryDotNet;
using AgriMarket.API.Repositories.Products.Interfaces;
using AgriMarket.API.Repositories.Products.Implementations;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgriMarket API", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Type=ReferenceType.SecurityScheme,
                Id=JwtBearerDefaults.AuthenticationScheme
            },
            Scheme="Oauth2",
            Name=JwtBearerDefaults.AuthenticationScheme,
            In=ParameterLocation.Header
        },
        new List<string>()
        }
    });
});

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

// Inject AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Inject Identity
builder.Services.AddIdentityCore<User>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<User>>("AgriMarket")
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders().AddApiEndpoints();


// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
    ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")))
});

Cloudinary cloudinary = new(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
cloudinary.Api.Secure = true;

// Inject Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IEmailSender<User>, EmailRepository>();
builder.Services.AddTransient<IProductRepository, ProductsRepository>();
builder.Services.AddSingleton(cloudinary);




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();
app.Run();