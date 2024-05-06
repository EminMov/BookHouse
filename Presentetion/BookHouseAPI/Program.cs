
using BookHouseAPI.Domain.Entities;
using BookHouseAPI.Persistance.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using static BookHouseAPI.Domain.Entities.AppUser;
using Serilog.Core;
using BookHouseAPI.Application.Abstractions.IUnitOfWork;
using BookHouseAPI.Persistance.Implementetions.UnitOfWork;
using BookHouseAPI.Application.AutoMappers;
using BookHouseAPI.Application.Abstractions.IRepositories;
using BookHouseAPI.Persistance.Implementetions.Repositories;
using BookHouseAPI.Application.Abstractions.Services;
using BookHouseAPI.Persistance.Implementetions.Services;
using System.Text.Json.Serialization;
using Serilog.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using BookHouseAPI.Persistance.Implementations.Services;
using FluentValidation.AspNetCore;
using BookHouseAPI.Infrastructure.Filters;
using BookHouseAPI.Application.DTOs.AuthorDTOs;
using BookHouseAPI.Application.Validators.AuthorValidator;

namespace BookHouseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BookContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<BookContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<IOrderService,  OrderService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserService, UserService> ();
            builder.Services.AddScoped<IAuthoService, AuthoService>();
            builder.Services.AddScoped<ITokenHandler, Persistance.Implementetions.Services.TokenHandler>();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = builder.Configuration["Token:Audience"],

                    ValidIssuer = builder.Configuration["Token:Issuer"],

                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Book House API",
                    Description = "ASP.Net Core 8 Web API"
                });
                swagger.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer Scheme."
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[]{ }
                    }

                });
            });

            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<AuthorAddDTOValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            Logger? log = new LoggerConfiguration()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Error)
                .WriteTo.File("Logs/myJsonLogs.json")//date-time mellim burda demishiki date-time tutsun
                .WriteTo.File("Logs/mylogs.txt")
                .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sinkOptions:
                new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                {
                    TableName = "MySerilog",
                    AutoCreateSqlTable = true
                },

                null, null, LogEventLevel.Warning, null,
                columnOptions: new ColumnOptions
                {
                    AdditionalColumns = new Collection<SqlColumn>
                    {
                        new SqlColumn(columnName:"User_Name",SqlDbType.NVarChar),
                        new SqlColumn(columnName:"Date",SqlDbType.NVarChar)
                    }
                },
                null, null
                )
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();

            LogContext.PushProperty("Date", DateTime.UtcNow.ToString());

            Log.Logger = log;

            builder.Host.UseSerilog(log);

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.ConfigureExceptionHandler();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
                LogContext.PushProperty("User_Name", username);
                await next(context);
            });

            app.MapControllers();

            app.Run();
        }
    }
}
