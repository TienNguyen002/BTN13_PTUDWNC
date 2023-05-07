using Microsoft.EntityFrameworkCore;
using NLog.Web;
using CookingWeb.Data.Contexts;
using CookingWeb.Services.Apps;
using CookingWeb.Services.Media;
using CookingWeb.Services.Timing;
using CookingWeb.Data.Seeders;
using CookingWeb.Services.Apps.Chefs;
using CookingWeb.Services.Apps.Categories;
using CookingWeb.Services.Apps.Courses;
using CookingWeb.Services.Apps.Other;
using CookingWeb.Services.Apps.Recipes;
using Carter;
using CookingWeb.Services.Apps.Posts;

namespace TatBlog.WebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddCarter();
            builder.Services.AddMemoryCache();

            builder.Services.AddDbContext<WebDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration
                        .GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ITimeProvider, LocalTimeProvider>();
            builder.Services.AddScoped<IMediaManager, LocalFileSystemMediaManager>();
            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IChefRepository, ChefRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IAppRepository, AppRepository>();
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CookingWebApp", policyBuilder =>
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            return builder;
        }

        //Cấu hình việc sử dụng NLog
        public static WebApplicationBuilder ConfigureNLog(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();
            return builder;
        }

        public static WebApplicationBuilder ConfigureSwaggerOpenApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder;
        }

        public static WebApplication SetupRequestPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors("CookingWebApp");

            return app;
        }

        public static IApplicationBuilder UseDataSeeder(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            try
            {
                scope.ServiceProvider
                  .GetRequiredService<IDataSeeder>()
                  .Initialize();
            }
            catch (Exception ex)
            {
                scope.ServiceProvider
                    .GetRequiredService<ILogger<Program>>()
                    .LogError(ex, "Could not insert data into database");
            }
            return app;
        }
    }
}
