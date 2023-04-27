using CookingWeb.WebApi.Mapsters;
using CookingWeb.WebApi.Extensions;
using CookingWeb.WebApi.Mapsters;
using CookingWeb.WebApi.Validations;
using TatBlog.WebApi.Extensions;
using CookingWeb.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
{
    builder.ConfigureCors()
        .ConfigureNLog()
        .ConfigureServices()
        .ConfigureSwaggerOpenApi()
        .ConfigureMapster()
        .ConfigureFluentValidation();
}

var app = builder.Build();
{
    app.SetupRequestPipeline();
    app.UseDataSeeder();

    app.MapCategoryEndpoints();
    app.MapCourseEndPoints();

    app.Run();
}

