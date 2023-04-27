using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Category;
using CookingWeb.Services.Apps.Categories;
using CookingWeb.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookingWeb.WebApi.Endpoints
{
    public static class CategoryEndpoints
    {
        public static WebApplication MapCategoryEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/categories");

            routeGroupBuilder.MapGet("/", GetAllCategories)
                .WithName("GetAllCategories")
                .Produces<ApiResponse<PaginationResult<CategoryItem>>>();

            return app;
        }

        private static async Task<IResult> GetAllCategories(
            [FromServices] ICategoryRepository categoryRepository)
        {
            var categories = await categoryRepository.GetCategoriesAsync();
            return Results.Ok(ApiResponse.Success(categories));
        }
    }
}
