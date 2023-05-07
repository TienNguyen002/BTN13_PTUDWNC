using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Chef;
using CookingWeb.Core.DTO.Student;
using CookingWeb.Core.Entities;
using CookingWeb.Data.Mappings;
using CookingWeb.Services.Apps.Chefs;
using CookingWeb.Services.Apps.Students;
using CookingWeb.WebApi.Extensions;
using CookingWeb.WebApi.Models;
using CookingWeb.WebApi.Models.Chef;
using CookingWeb.WebApi.Models.Student;
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace CookingWeb.WebApi.Endpoints
{
    public static class StudentEndpoints
    {
        public static WebApplication MapStudentEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/students");

            routeGroupBuilder.MapGet("/", GetAllStudent)
                .WithName("GetAllStudents")
                .Produces<ApiResponse<PaginationResult<Student>>>();
            routeGroupBuilder.MapPost("/{id:int}", AddStudent)
               .WithName("AddNewStudent")
               .Produces<ApiResponse<PaginationResult<StudentItem>>>();
            routeGroupBuilder.MapPut("/{id:int}", UpdateStudent)
               .WithName("UpdateAnStudent")
               .Produces<ApiResponse<PaginationResult<StudentItem>>>();
            routeGroupBuilder.MapDelete("/{id:int}", DeleteStudent)
                .WithName("DeleteAnStudent")
                .Produces<ApiResponse<PaginationResult<StudentItem>>>();

            return app;
        }

        private static async Task<IResult> GetAllStudent(
            [FromServices] IStudentRepository studentRepository)
        {
            var student = await studentRepository.GetStudentsAsync();
            return Results.Ok(ApiResponse.Success(student));
        }

        // Them hoc vien
        private static async Task<IResult> AddStudent(
            IMapper mapper,
            IValidator<StudentEditModel> validator,
            StudentEditModel model,
            IStudentRepository studentRepository)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(
                    validationResult.Errors.ToResponse());
            }
            if (await studentRepository.IsStudentSlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Conflict($"Slug '{model.UrlSlug}' đã được sửa dụng");
            }
            var student = mapper.Map<Student>(model);
            await studentRepository.AddOrUpdateStudentAsync(student);

            return Results.CreatedAtRoute(
                "GetStudentById", new { student.Id },
                mapper.Map<StudentItem>(student));
        }
        //Cập nhật hoc vien
        private static async Task<IResult> UpdateStudent(
            int id, StudentEditModel model,
            IValidator<StudentEditModel> validator,
            IMapper mapper,
            IStudentRepository studentRepository)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(
                    validationResult.Errors.ToResponse());
            }
            if (await studentRepository.IsStudentSlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Conflict($"Slug '{model.UrlSlug}' da duoc su dung");
            }
            var student = mapper.Map<Student>(model);
            student.Id = id;

            return await studentRepository.AddOrUpdateStudentAsync(student)
                ? Results.NoContent()
                : Results.NotFound();
        }
        //Xoa hoc vien
        private static async Task<IResult> DeleteStudent(
            int id, IStudentRepository studentRepository)
        {
            return await studentRepository.DeleteStudentsAsync(id)
                ? Results.NoContent()
                : Results.NotFound($"Khong tim thay hoc vien voi id = {id} ");
        }
    }
}
