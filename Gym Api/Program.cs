
using Gym_Api.Data;
using Gym_Api.Repo;
using Gym_Api.Survices;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Gym_Api.Common.Settings;
using Gym_Api.Survices.Authentication;

namespace Gym_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<ApplicationDbContext>(op =>
			op.UseSqlServer(builder.Configuration.GetConnectionString("myCon")));

			
			
			
			
			
			
			
            
            
            
            


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCommonServices(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
