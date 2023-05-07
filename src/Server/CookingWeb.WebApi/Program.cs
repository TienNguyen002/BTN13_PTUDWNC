using CookingWeb.WebApi.Mapsters;
using CookingWeb.WebApi.Extensions;
using CookingWeb.WebApi.Mapsters;
using CookingWeb.WebApi.Validations;
using TatBlog.WebApi.Extensions;
using CookingWeb.WebApi.Endpoints;
using Carter;

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

    app.MapCarter();

    app.Run();
}

