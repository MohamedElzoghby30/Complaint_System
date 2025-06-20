
using ComplaintSystem.Api.Extension;
using ComplaintSystem.Core.Entities;
using ComplaintSystem.Core.Serveice.Contract;
using ComplaintSystem.Repo.Data;
using ComplaintSystem.Service.Services;
using ComplaintSystem.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;

namespace ComplaintSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"));
            });
          

            // config IfileProfider

            var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(webRootPath));
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddJwtIdentity(builder.Configuration)
                            .AddInfrastractureService()
                            .AddSwaggerDocumentation();
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();
            app.DefaultDataSeeding();
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.UseSwaggerGenForDevelopment();
            else
                app.UseSwaggerGen();

            app.UseCors("AllowAll");


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
