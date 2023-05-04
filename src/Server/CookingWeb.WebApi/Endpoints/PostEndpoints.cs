using Carter;
using CookingWeb.Core.Collections;
using CookingWeb.Core.DTO.Post;
using CookingWeb.Core.Entities;
using CookingWeb.Services.Apps.Categories;
using CookingWeb.Services.Apps.Posts;
using CookingWeb.Services.Media;
using CookingWeb.WebApi.Filters;
using CookingWeb.WebApi.Models;
using CookingWeb.WebApi.Models.Post;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace CookingWeb.WebApi.Endpoints
{
    public class PostEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup("/api/posts");

            routeGroupBuilder.MapGet("/", GetPosts)
                .WithName("GetPosts")
                .Produces<ApiResponse<PaginationResult<PostDto>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetPostById)
                .WithName("GetPostById")
                .Produces<ApiResponse<PostDetail>>();

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}", GetPostBySlug)
              .WithName("GetPostBySlug")
              .Produces<ApiResponse<PostDetail>>();

            routeGroupBuilder.MapGet("/toggle-status/{id:int}", ToggleStatus)
                .WithName("TogglePostStatus")
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapGet("/get-filter", GetFilter)
                .WithName("GetPostFilter")
                .Produces<ApiResponse<PostFilterModel>>();

            routeGroupBuilder.MapGet("/get-posts-filter", GetFilteredPosts)
                .WithName("GetFilteredPosts")
                .Produces<ApiResponse<PaginationResult<PostDto>>>();

            routeGroupBuilder.MapPost("/", AddPost)
                .WithName("AddPost")
                .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
                .Produces<ApiResponse<PostDto>>();

            routeGroupBuilder.MapPut("/{id:int}", UpdatePost)
                .WithName("UpdatePost")
                .AddEndpointFilter<ValidatorFilter<PostEditModel>>()
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeletePost)
                .WithName("DeletePost")
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapPost("/{id:int}/picture", SetPostPicture)
              .WithName("SetPostPicture")
              .Accepts<IFormFile>("multipart/form-data")
              .Produces<ApiResponse<string>>();
        }

        private static async Task<IResult> GetPosts(
            [AsParameters] PostFilterModel model,
            IPostRepository postRepository,
            IMapper mapper)
        {
            var query = mapper.Map<PostQuery>(model);
            var posts = await postRepository.GetPagedPostsAsync<PostDto>(query, model,
                posts => posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(posts);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetPostById(int id,
            IPostRepository postRepository,
            IMapper mapper)
        {
            var posts = await postRepository.GetPostById(id, true);
            return posts == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài viết có id {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<PostDetail>(posts)));
        }

        private static async Task<IResult> GetPostBySlug(
            string slug,
            IPostRepository PostRepository,
            IMapper mapper)
        {
            var Posts = await PostRepository.GetPostBySlug(slug, true);
            return Posts == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài viết có slug {slug}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<PostDetail>(Posts)));
        }

        private static async Task<IResult> ToggleStatus(
            int id,
            IPostRepository PostRepository)
        {
            var Post = await PostRepository.GetPostById(id);
            if (Post == null)
            {
                Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài viết có id = {id}"));
            }
            await PostRepository.ToggleStatus(id);
            return Results.Ok(ApiResponse.Success("Đổi trạng thái thành công", HttpStatusCode.NoContent));
        }

        private static async Task<IResult> GetFilter(
            ICategoryRepository categoryRepository)
        {
            var model = new PostFilterModel()
            {
                CategoryList = (await categoryRepository.GetCategoriesAsync())
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return Results.Ok(ApiResponse.Success(model));
        }

        private static async Task<IResult> GetFilteredPosts(
            [AsParameters] PostFilterModel model,
            IMapper mapper,
            IPostRepository PostRepository)
        {
            var PostQuery = mapper.Map<PostQuery>(model);
            var PostsList = await PostRepository.GetPagedPostsAsync(PostQuery, model,
                Posts => Posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(PostsList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddPost(
            PostEditModel model,
            IMapper mapper,
            IPostRepository PostRepository,
            ICategoryRepository categoryRepository,
            IMediaManager mediaManager)
        {
            if (await PostRepository.IsPostSlugExitedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            if (await categoryRepository.GetCategoryById(model.CategoryId) == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Không tìm thấy chủ đề có id = '{model.CategoryId}' "));
            }
            var Post = mapper.Map<Post>(model);
            Post.CreateDate = DateTime.Now;
            await PostRepository.AddOrUpdatePostAsync(Post);
            return Results.Ok(ApiResponse.Success(mapper.Map<PostDto>(Post), HttpStatusCode.Created));
        }

        private static async Task<IResult> UpdatePost(
            int id,
            PostEditModel model,
            IMapper mapper,
            ICategoryRepository categoryRepository,
            IPostRepository PostRepository,
            IMediaManager mediaManager)
        {
            var Post = await PostRepository.GetPostById(id);
            if (Post == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                    $"Không tìm thấy bài viết có id {id}"));
            }
            if (await PostRepository.IsPostSlugExitedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                    $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            if (await categoryRepository.GetCategoryById(model.CategoryId) == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                    $"Không tìm thấy chủ đề có id = {id}"));
            }
            mapper.Map(model, Post);
            Post.Id = id;
            Post.UpdateDate = DateTime.Now;

            return await PostRepository.AddOrUpdatePostAsync(Post)
               ? Results.Ok(ApiResponse.Success($"Thay đổi bài viết có id = {id} thành công"))
               : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài viết có id = {id}"));
        }

        private static async Task<IResult> DeletePost(
            int id,
            IPostRepository PostRepository)
        {
            return await PostRepository.DeletePostByIdAsync(id)
                ? Results.Ok(ApiResponse.Success("Xóa bài viết thành công", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài viết có id = {id}"));
        }

        private static async Task<IResult> SetPostPicture(
            int id,
            IFormFile imageFile,
            IPostRepository PostRepository,
            IMediaManager mediaManager)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(),
                imageFile.FileName, imageFile.ContentType);
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Không lưu được tập tin"));
            }
            await PostRepository.SetImagePostAsync(id, imageUrl);
            return Results.Ok(ApiResponse.Success(imageUrl));
        }
    }
}
