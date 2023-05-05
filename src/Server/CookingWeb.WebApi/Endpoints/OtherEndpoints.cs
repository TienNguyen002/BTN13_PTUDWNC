using Carter;
using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Category;
using CookingWeb.Core.DTO.Others;
using CookingWeb.Services.Apps.Other;
using CookingWeb.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookingWeb.WebApi.Endpoints
{
    public class OtherEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup("/api/others");

            routeGroupBuilder.MapGet("/demands", GetAllDemands)
                .WithName("GetAllDemands")
                .Produces<ApiResponse<PaginationResult<DemandItem>>>();

            routeGroupBuilder.MapGet("/prices", GetAllPrices)
                .WithName("GetAllPrices")
                .Produces<ApiResponse<PaginationResult<PriceItem>>>();

            routeGroupBuilder.MapGet("/sessions", GetAllSessions)
                .WithName("GetAllSessions")
                .Produces<ApiResponse<PaginationResult<NumberOfSessionsItem>>>();
        }

        private static async Task<IResult> GetAllDemands(
            [FromServices] IAppRepository appRepository)
        {
            var demands = await appRepository.GetDemandsAsync();
            return Results.Ok(ApiResponse.Success(demands));
        }

        private static async Task<IResult> GetAllPrices(
            [FromServices] IAppRepository appRepository)
        {
            var prices = await appRepository.GetPricesAsync();
            return Results.Ok(ApiResponse.Success(prices));
        }

        private static async Task<IResult> GetAllSessions(
            [FromServices] IAppRepository appRepository)
        {
            var sessions = await appRepository.GetNumberOfSessionsAsync();
            return Results.Ok(ApiResponse.Success(sessions));
        }
    }
}
