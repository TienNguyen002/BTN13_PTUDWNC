using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Course;
using CookingWeb.Core.DTO.Post;
using CookingWeb.Core.DTO.Recipe;
using CookingWeb.Core.Entities;
using CookingWeb.Data.Contexts;
using CookingWeb.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly WebDbContext _context;
        private readonly IMemoryCache _memoryCache;
        
        public PostRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Post> GetPostById(int id, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if (!includeDetails)
            {
                return await _context.Set<Post>().FindAsync(id);
            }
            return await _context.Set<Post>()
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsPostSlugExitedAsync(int id, string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>()
                .AnyAsync(p => p.Id != id && p.UrlSlug == slug, cancellationToken);
        }

        public async Task<Post> GetPostBySlug(string slug, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if (!includeDetails)
            {
                return await _context.Set<Post>().Where(r => r.UrlSlug == slug)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            return await _context.Set<Post>()
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.UrlSlug == slug)
                .FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<Post> FindPostByQueryable(PostQuery query)
        {
            IQueryable<Post> postQuery = _context.Set<Post>()
                .Include(p => p.Author)
                .Include(p => p.Category);
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                postQuery = postQuery.Where(p => p.Title.Contains(query.Keyword)
                || p.ShortDescription.Contains(query.Keyword)
                || p.Description.Contains(query.Keyword)
                || p.UrlSlug.Contains(query.Keyword));
            }
            if (query.AuthorId > 0)
            {
                postQuery = postQuery.Where(p => p.AuthorId == query.AuthorId);
            }
            if (query.CategoryId > 0)
            {
                postQuery = postQuery.Where(p => p.CategoryId == query.CategoryId);
            }
            if (query.CreateMonth > 0)
            {
                postQuery = postQuery.Where(p => p.CreateDate.Month == query.CreateMonth);
            }
            if (query.CreateYear > 0)
            {
                postQuery = postQuery.Where(p => p.CreateDate.Year == query.CreateYear);
            }
            if (query.NotPublished)
            {
                postQuery = postQuery.Where(p => p.Published == query.NotPublished);
            }
            if (query.PublishedOnly)
            {
                postQuery = postQuery.Where(p => p.Published == query.PublishedOnly);
            }
            return postQuery;
        }

        public async Task<IPagedList<T>> GetPagedPostsAsync<T>(PostQuery query, 
            IPagingParams pagingParams, 
            Func<IQueryable<Post>, IQueryable<T>> mapper, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<Post> postsFindResultQuery = FindPostByQueryable(query);
            IQueryable<T> result = mapper(postsFindResultQuery);
            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task ToggleStatus(int id, CancellationToken cancellationToken = default)
        {
            await _context.Set<Post>()
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(r => r.SetProperty(p => p.Published, p => !p.Published), cancellationToken);
        }

        public async Task<bool> AddOrUpdatePostAsync(Post post, CancellationToken cancellationToken = default)
        {
            if (post.Id > 0)
            {
                post.UpdateDate = DateTime.Now;
                _context.Update(post);
            }
            else
                _context.Add(post);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeletePostByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var postToDelete = await _context.Set<Post>()
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if (postToDelete == null)
            {
                return false;
            }
            _context.Remove(postToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> SetImagePostAsync(int id, string imageUrl, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>()
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(p => p.SetProperty(p => p.ImageUrl, p => imageUrl)
                                        .SetProperty(p => p.UpdateDate, p => DateTime.Now),
                                        cancellationToken) > 0;
        }
    }
}
