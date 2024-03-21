
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

namespace BookHouseAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BookContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<BookContext>().AddDefaultTokenProviders();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthtorService, AuthorService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddHttpContextAccessor();


            Logger? log = new LoggerConfiguration()
                .WriteTo.Console(Serilog.Events.LogEventLevel.Error)
                .WriteTo.File("Logs/myJsonLogs.json")
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
                        new SqlColumn(columnName:"User_Id",SqlDbType.NVarChar)
                    }
                },
                null, null
                )
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();

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

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
