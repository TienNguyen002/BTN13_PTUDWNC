using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Category;
using CookingWeb.Services.Apps.Categories;
using CookingWeb.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Carter;

namespace CookingWeb.WebApi.Endpoints
{
    public class CategoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup("/api/categories");

            routeGroupBuilder.MapGet("/", GetAllCategories)
                .WithName("GetAllCategories")
                .Produces<ApiResponse<PaginationResult<CategoryItem>>>();
        }

        private static async Task<IResult> GetAllCategories(
            [FromServices] ICategoryRepository categoryRepository)
        {
            var categories = await categoryRepository.GetCategoriesAsync();
            return Results.Ok(ApiResponse.Success(categories));
        }
    }
}
