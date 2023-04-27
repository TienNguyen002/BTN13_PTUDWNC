using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Course;
using CookingWeb.Services.Apps.Courses;
using CookingWeb.WebApi.Models;
using CookingWeb.WebApi.Models.Course;
using Mapster;
using MapsterMapper;

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
    }
}
