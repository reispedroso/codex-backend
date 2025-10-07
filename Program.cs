using DotNetEnv;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using codex_backend.Database;
using codex_backend.Infra.Repositories;
using codex_backend.Application.Handlers;
using codex_backend.Application.Settings;
using codex_backend.Application.Factories;
using codex_backend.Application.Services.Token;
using codex_backend.Application.Common.Middleware;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Application.Authorization.Handlers;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Implementations;
using codex_backend.Application.Authorization.Requirements;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var db = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = tokenSettings!.Issuer,
        ValidateAudience = true,
        ValidAudience = tokenSettings.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key))
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageBookstorePolicy", policy =>
        policy.AddRequirements(new ResourceOwnerRequirement()));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repositories
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookItemRepository, BookItemRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookReviewRepository, BookReviewRepository>();
builder.Services.AddScoped<IBookstoreRepository, BookstoreRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IStorePolicyPricesRepository, StorePolicyPriceRepository>();
builder.Services.AddScoped<IStorePolicyRepository, StorePolicyRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

//Services
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IBookItemService, BookItemService>();
builder.Services.AddScoped<IBookstoreService, BookstoreService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Factories
builder.Services.AddScoped<IReservationFactory, ReservationFactory>();
builder.Services.AddScoped<IRentalFactory, RentalFactory>();
builder.Services.AddScoped<IUserFactory, UserFactory>();

//Handlers
builder.Services.AddScoped<InventoryHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, BookstoreAuthorizationHandler>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");


app.UseAuthentication(); 
// app.UseJwtMiddleware(); // <-- Comentado

app.UseAuthorization();
app.MapControllers();

app.Run();