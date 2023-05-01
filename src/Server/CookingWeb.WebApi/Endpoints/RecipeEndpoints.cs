using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Course;
using CookingWeb.Core.DTO.Recipe;
using CookingWeb.Core.Entities;
using CookingWeb.Services.Apps.Courses;
using CookingWeb.Services.Apps.Other;
using CookingWeb.Services.Apps.Recipes;
using CookingWeb.Services.Media;
using CookingWeb.WebApi.Filters;
using CookingWeb.WebApi.Models;
using CookingWeb.WebApi.Models.Course;
using CookingWeb.WebApi.Models.Recipe;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace CookingWeb.WebApi.Endpoints
{
    public static class RecipeEndpoints
    {
        public static WebApplication MapRecipeEndPoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/recipes");

            routeGroupBuilder.MapGet("/", GetRecipes)
                .WithName("GetRecipes")
                .Produces<ApiResponse<PaginationResult<RecipeDto>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetRecipeById)
                .WithName("GetRecipeById")
                .Produces<ApiResponse<RecipeDetail>>();

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}/recipes", GetRecipeBySlug)
              .WithName("GetRecipeBySlug")
              .Produces<ApiResponse<RecipeDetail>>();

            routeGroupBuilder.MapGet("/toggle-status/{id:int}", ToggleStatus)
                .WithName("ToggleRecipeStatus")
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapGet("/get-filter", GetFilter)
                .WithName("GetRecipeFilter")
                .Produces<ApiResponse<RecipeFilterModel>>();

            routeGroupBuilder.MapGet("/get-recipes-filter", GetFilteredRecipes)
                .WithName("GetFilteredRecipes")
                .Produces<ApiResponse<PaginationResult<RecipeDto>>>();

            routeGroupBuilder.MapPost("/", AddRecipe)
                .WithName("AddRecipe")
                .AddEndpointFilter<ValidatorFilter<RecipeEditModel>>()
                .Produces<ApiResponse<RecipeDto>>();

            routeGroupBuilder.MapPut("/{id:int}", UpdateRecipe)
                .WithName("UpdateRecipe")
                .AddEndpointFilter<ValidatorFilter<RecipeEditModel>>()
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteRecipe)
                .WithName("DeleteRecipe")
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapPost("/{id:int}/picture", SetRecipePicture)
              .WithName("SetRecipePicture")
              .Accepts<IFormFile>("multipart/form-data")
              .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetRecipes(
            [AsParameters] RecipeFilterModel model,
            IRecipeRepository recipeRepository,
            IMapper mapper)
        {
            var query = mapper.Map<RecipeQuery>(model);
            var recipes = await recipeRepository.GetPagedRecipesAsync<RecipeDto>(query, model,
                recipes => recipes.ProjectToType<RecipeDto>());
            var paginationResult = new PaginationResult<RecipeDto>(recipes);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetRecipeById(int id,
            IRecipeRepository recipeRepository,
            IMapper mapper)
        {
            var recipes = await recipeRepository.GetRecipeById(id, true);
            return recipes == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy công thức có id {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<RecipeDetail>(recipes)));
        }

        private static async Task<IResult> GetRecipeBySlug(
            string slug,
            IRecipeRepository recipeRepository,
            IMapper mapper)
        {
            var recipes = await recipeRepository.GetRecipeBySlug(slug, true);
            return recipes == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy công thức có slug {slug}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<RecipeDetail>(recipes)));
        }

        private static async Task<IResult> ToggleStatus(
            int id,
            IRecipeRepository recipeRepository)
        {
            var recipe = await recipeRepository.GetRecipeById(id);
            if (recipe == null)
            {
                Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy công thức có id = {id}"));
            }
            await recipeRepository.ToggleStatus(id);
            return Results.Ok(ApiResponse.Success("Đổi trạng thái thành công", HttpStatusCode.NoContent));
        }

        private static async Task<IResult> GetFilter(
            ICourseRepository courseRepository)
        {
            var model = new RecipeFilterModel()
            {
                CourseList = (await courseRepository.GetCoursesAsync())
                .Select(c => new SelectListItem()
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                })
            };
            return Results.Ok(ApiResponse.Success(model));
        }

        private static async Task<IResult> GetFilteredRecipes(
            [AsParameters] RecipeFilterModel model,
            IMapper mapper,
            IRecipeRepository recipeRepository)
        {
            var recipeQuery = mapper.Map<RecipeQuery>(model);
            var recipesList = await recipeRepository.GetPagedRecipesAsync(recipeQuery, model,
                recipes => recipes.ProjectToType<RecipeDto>());
            var paginationResult = new PaginationResult<RecipeDto>(recipesList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddRecipe(
            RecipeEditModel model,
            IMapper mapper,
            IRecipeRepository recipeRepository,
            ICourseRepository courseRepository,
            IMediaManager mediaManager)
        {
            if (await recipeRepository.IsRecipeSlugExitedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            if (await courseRepository.GetCourseById(model.CourseId) == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Không tìm thấy khóa học có id = '{model.CourseId}' "));
            }
            var recipe = mapper.Map<Recipe>(model);
            recipe.CreateDate = DateTime.Now;
            await recipeRepository.AddOrUpdateRecipeAsync(recipe);
            return Results.Ok(ApiResponse.Success(mapper.Map<RecipeDto>(recipe), HttpStatusCode.Created));
        }

        private static async Task<IResult> UpdateRecipe(
            int id,
            RecipeEditModel model,
            IMapper mapper,
            ICourseRepository courseRepository,
            IRecipeRepository recipeRepository,
            IMediaManager mediaManager)
        {
            var recipe = await recipeRepository.GetRecipeById(id);
            if (recipe == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                    $"Không tìm thấy công thức có id {id}"));
            }
            if (await recipeRepository.IsRecipeSlugExitedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                    $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            if (await courseRepository.GetCourseById(model.CourseId) == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                    $"Không tìm thấy khóa học có id = {id}"));
            }
            mapper.Map(model, recipe);
            recipe.Id = id;
            recipe.UpdateDate = DateTime.Now;

            return await recipeRepository.AddOrUpdateRecipeAsync(recipe)
               ? Results.Ok(ApiResponse.Success($"Thay đổi công thức có id = {id} thành công"))
               : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy công thức có id = {id}"));
        }

        private static async Task<IResult> DeleteRecipe(
            int id,
            IRecipeRepository recipeRepository)
        {
            return await recipeRepository.DeleteRecipeByIdAsync(id)
                ? Results.Ok(ApiResponse.Success("Xóa công thức thành công", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy công thức có id = {id}"));
        }

        private static async Task<IResult> SetRecipePicture(
            int id,
            IFormFile imageFile,
            IRecipeRepository recipeRepository,
            IMediaManager mediaManager)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(),
                imageFile.FileName, imageFile.ContentType);
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Không lưu được tập tin"));
            }
            await recipeRepository.SetImageRecipeAsync(id, imageUrl);
            return Results.Ok(ApiResponse.Success(imageUrl));
        }
    }
}
