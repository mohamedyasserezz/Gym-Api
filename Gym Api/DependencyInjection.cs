using FluentValidation;
using FluentValidation.AspNetCore;
using Gym_Api.Common.Settings;
using Gym_Api.Data.Models;
using Gym_Api.Data;
using Gym_Api.Mapping;
using Gym_Api.Survices;
using Gym_Api.Survices.Authentication;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Gym_Api.Repo;
using Gym_Api.Services;

namespace Gym_Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region FluentValidation
            services
                   .AddFluentValidationAutoValidation()
               .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
            #endregion

            #region JWT
            services.AddSingleton<IJwtProvider, JwtProvider>();

            services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();


            var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                       ValidIssuer = jwtSettings?.Issuer,
                       ValidAudience = jwtSettings?.Audience
                   };
               });
            #endregion

            #region CORS
            // var allowedOrgins = configuration.GetSection("AllowedOrgins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyOrigin()    // Allow requests from any origin
                        .AllowAnyHeader()    // Allow any headers
                        .AllowAnyMethod();   // Allow any HTTP methods (GET, POST, etc.)
                });
            });

            #endregion

            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailSender, EmailService>();
            services.AddScoped<INutritionPlanRepository, NutritionPlanRepository>();

            #region Hangfire
            services.AddHangfire(Configuration => Configuration
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            #endregion

            #region auto mapper

            services.AddHttpContextAccessor();

            services.AddTransient<MappingProfile>();

            services.AddAutoMapper(typeof(AssemblyInformation).Assembly);


            #endregion

            #region Identity
            services.AddControllers();

            services
              .AddIdentity<ApplicationUser, ApplicationRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services
                .Configure<IdentityOptions>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;

                });

            #endregion

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IExerciseSurvice, ExerciseSurvice>();
            services.AddScoped<ICoachRepository, CoachRepository>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<INutritionPlanService, NutritionPlanService>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
