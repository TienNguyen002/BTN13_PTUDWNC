using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Post;
using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Posts
{
    public interface IPostRepository
    {
        Task<Post> GetPostById(int id, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<bool> IsPostSlugExitedAsync(int id, string slug, CancellationToken cancellationToken = default);

        Task<Post> GetPostBySlug(string slug, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedPostsAsync<T>(PostQuery query,
            IPagingParams pagingParams,
            Func<IQueryable<Post>, IQueryable<T>> mapper,
            CancellationToken cancellationToken = default);

        Task ToggleStatus(int id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdatePostAsync(Post post, CancellationToken cancellationToken = default);

        Task<bool> DeletePostByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> SetImagePostAsync(int id, string imageUrl, CancellationToken cancellationToken = default);   
    }
}
