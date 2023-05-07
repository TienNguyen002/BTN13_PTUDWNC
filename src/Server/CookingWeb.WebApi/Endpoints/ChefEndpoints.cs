using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Chef;
using CookingWeb.Core.Entities;
using CookingWeb.Services.Apps.Chefs;
using CookingWeb.WebApi.Extensions;
using CookingWeb.WebApi.Models;
using CookingWeb.WebApi.Models.Chef;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace CookingWeb.WebApi.Endpoints
{
    public static class ChefEndpoints
    {
        public static WebApplication MapChefEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/chefs");

            routeGroupBuilder.MapGet("/", GetAllChefs)
                .WithName("GetAllChefs")
                .Produces<ApiResponse<PaginationResult<ChefItem>>>();
            routeGroupBuilder.MapDelete("/{id:int}", AddChefs)
               .WithName("AddNewChef")
               .Produces<ApiResponse<PaginationResult<ChefItem>>>();
            routeGroupBuilder.MapDelete("/{id:int}", UpdateChefs)
               .WithName("UpdateAnChef")
               .Produces<ApiResponse<PaginationResult<ChefItem>>>();
            routeGroupBuilder.MapDelete("/{id:int}", DeleteChefs)
                .WithName("DeleteAnChef")
                .Produces<ApiResponse<PaginationResult<ChefItem>>>();

            return app;
        }

        private static async Task<IResult> GetAllChefs(
            [FromServices] IChefRepository chefRepository)
        {
            var chefs = await chefRepository.GetChefsAsync();
            return Results.Ok(ApiResponse.Success(chefs));
        }
     
        // Lấy danh sách dau bep theo Id
        //private static async Task<IResult> GetChefsId(
        //    int id,
        //    [AsParameters] PagingModel pagingModel,
        //     IChefRepository chefRepository)
        //{
        //    var course = new Course()
        //    {
        //        ChefId = id,
        //        Published = true
        //    };
        //    var chefsList = await chefRepository.GetChefsAsync(
        //        course,pagingModel, chefs => 
        //        chefs.PojectToType<ChefItem>);
        //    var paginationResult = new PaginationResult<ChefItem>(chefsList);
        //    return Results.Ok(paginationResult);
        //}
        //Them dau bep
        private static async Task<IResult> AddChefs(
            IMapper mapper,
            IValidator<ChefEditModel> validator,
            ChefEditModel model,
            IChefRepository chefRepository)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(
                    validationResult.Errors.ToResponse());
            }  
            if (await chefRepository.IsChefSlugExistedAsync(0,model.UrlSlug))
            {
                return Results.Conflict($"Slug '{model.UrlSlug}' đã được sửa dụng");
            }
            var chefs = mapper.Map<Chef>(model);
            await chefRepository.AddOrUpdateChefAsync(chefs);

            return Results.CreatedAtRoute(
                "GetChefById", new {chefs.Id},
                mapper.Map<ChefItem>(chefs));
        }
        //Them dau bep
        private static async Task<IResult> UpdateChefs(
            int id,ChefEditModel model,
            IValidator<ChefEditModel> validator,
            IMapper mapper,
            IChefRepository chefRepository)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(
                    validationResult.Errors.ToResponse());
            }
            if (await chefRepository.IsChefSlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Conflict($"Slug '{model.UrlSlug}' da duoc su dung");
            }
           var chefs = mapper.Map<Chef>(model);
            chefs.Id = id;

            return await chefRepository.AddOrUpdateChefAsync(chefs)
                ? Results.NoContent()
                : Results.NotFound();
        }
        //Xoa dau bep
        private static async Task<IResult> DeleteChefs(
            int id, IChefRepository chefRepository)
        {
            return await chefRepository.DeleteChefsAsync(id)
                ? Results.NoContent()
                : Results.NotFound($"Khong tim thay chef voi id = {id} ");
        }
    }
}
