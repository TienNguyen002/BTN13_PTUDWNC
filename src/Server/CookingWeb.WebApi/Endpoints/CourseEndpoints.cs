using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Course;
using CookingWeb.Services.Apps.Courses;
using CookingWeb.WebApi.Models;
using CookingWeb.WebApi.Models.Course;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CookingWeb.WebApi.Endpoints
{
    public static class CourseEndpoints
    {
        public static WebApplication MapCourseEndPoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/courses");

            routeGroupBuilder.MapGet("/", GetCourses)
                .WithName("GetCourses")
                .Produces<ApiResponse<PaginationResult<CourseDto>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetCourseById)
                .WithName("GetCourseById")
                .Produces<ApiResponse<CourseDetail>>();

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}/courses", GetCourseBySlug)
              .WithName("GetCourseBySlug")
              .Produces<ApiResponse<CourseDetail>>();

            routeGroupBuilder.MapGet("/popular/{limit:int}", GetNPopularCourses)
                .WithName("GetNPopularCourses")
                .Produces<ApiResponse<IList<CourseDto>>>();

            routeGroupBuilder.MapGet("/toggle-status/{id:int}", ToggleStatus)
                .WithName("ToggleStatus")
                .Produces<ApiResponse<string>>();

            return app;
        }

        private static async Task<IResult> GetCourses(
            [AsParameters] CourseFilterModel model,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            var query = mapper.Map<CourseQuery>(model);
            var courses = await courseRepository.GetPagedCoursesAsync<CourseDto>(query, model,
                courses => courses.ProjectToType<CourseDto>());
            var paginationResult = new PaginationResult<CourseDto>(courses);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetCourseById(int id,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            var courses = await courseRepository.GetCourseById(id, true);
            return courses == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy khóa học có id {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<CourseDetail>(courses)));
        }

        private static async Task<IResult> GetCourseBySlug(
            [FromRoute] string slug,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            var courses = await courseRepository.GetCourseBySlug(slug, true);
            return courses == null
               ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy khóa học có slug {slug}"))
               : Results.Ok(ApiResponse.Success(mapper.Map<CourseDetail>(courses)));
        }

        private static async Task<IResult> GetNPopularCourses(
            int limit,
            ICourseRepository courseRepository)
        {
            var courses = await courseRepository.GetNPopularCoursesAsync(limit,
                courses => courses.ProjectToType<CourseDto>());
            return Results.Ok(ApiResponse.Success(courses));
        }

        private static async Task<IResult> ToggleStatus(
            int id,
            ICourseRepository courseRepository)
        {
            var course = await courseRepository.GetCourseById(id);
            if(course == null)
            {
                Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy khóa học có id = {id}"));
            }
            await courseRepository.ToggleStatus(id);
            return Results.Ok(ApiResponse.Success("Đổi trạng thái thành công", HttpStatusCode.NoContent));
        }
    }
}
