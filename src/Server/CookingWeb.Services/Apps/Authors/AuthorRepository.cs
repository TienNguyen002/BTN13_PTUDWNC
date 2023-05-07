using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Author;
using CookingWeb.Core.Entities;
using CookingWeb.Data.Contexts;
using CookingWeb.Services.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CookingWeb.Services.Apps.Authors
{
    public class AuthorRepository : IAuthorRepository  
    {
        private readonly WebDbContext _webDbContext;
        private readonly IMemoryCache _memoryCache;

        public AuthorRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _webDbContext = context;
            _memoryCache = memoryCache;
        }

        public async Task<Author> GetAuthorByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Set<Author>()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Author> GetCachedAuthorByIdAsync(int authorId)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"author.by-id.{authorId}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetAuthorByIdAsync(authorId);
                });
        }

        public async Task<IList<AuthorItem>> GetAuthorsAsync(CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Set<Author>()
                .OrderBy(a => a.FullName)
                .Select(a => new AuthorItem()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    JoinedDate = a.JoinedDate,
                    ImageUrl = a.ImageUrl,
                    UrlSlug = a.UrlSlug,
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CountAuthorAsync(CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Set<Author>().CountAsync(cancellationToken);
        }

        public async Task<bool> IsExistAuthorSlugAsync(int id, string slug, CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Set<Author>().AnyAsync(s => s.Id != id && s.UrlSlug.Equals(slug), cancellationToken);
        }

        public async Task<Author> GetAuthorBySlugAsync(string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Set<Author>()
                .FirstOrDefaultAsync(s => s.UrlSlug == urlSlug, cancellationToken);
        }

        public async Task<Author> GetCachedAuthorBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"author.by-slug.{slug}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetAuthorBySlugAsync(slug, cancellationToken);
                });
        }

        private IQueryable<AuthorItem> AuthorFilter(IAuthorQuery condition)
        {
            var authors = _webDbContext.Set<Author>()
                .WhereIf(!string.IsNullOrWhiteSpace(condition.Keyword), s =>
                    s.FullName.Contains(condition.Keyword))
                .WhereIf(condition.Month != 0, s => s.JoinedDate.Month == condition.Month)
                .WhereIf(condition.Year != 0, s => s.JoinedDate.Year == condition.Year)
                .Select(s => new AuthorItem()
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    ImageUrl = s.ImageUrl,
                    JoinedDate = s.JoinedDate,
                });
            return authors;
        }

        public async Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(IAuthorQuery condition, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            return await AuthorFilter(condition).ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(IPagingParams pagingParams, string name = null,
            CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Set<Author>()
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(name),
                    x => x.FullName.Contains(name))
                .Select(a => new AuthorItem()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    JoinedDate = a.JoinedDate,
                    ImageUrl = a.ImageUrl,
                    UrlSlug = a.UrlSlug,
                })
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<T>> GetPagedAuthorsAsync<T>(Func<IQueryable<Author>, IQueryable<T>> mapper, IPagingParams pagingParams, string name = null,
            CancellationToken cancellationToken = default)
        {
            var authorQuery = _webDbContext.Set<Author>().AsNoTracking();

            if (!string.IsNullOrEmpty(name))
            {
                authorQuery = authorQuery.Where(x => x.FullName.Contains(name));
            }

            return await mapper(authorQuery)
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<bool> AddOrUpdateAuthorAsync(Author author, CancellationToken cancellationToken = default)
        {
            if (author.Id > 0)
                _webDbContext.Update(author);
            else
                _webDbContext.Add(author);

            var result = await _webDbContext.SaveChangesAsync(cancellationToken);

            return result > 0;
        }

        public async Task<bool> DeleteAuthorByIdAsync(int? id, CancellationToken cancellationToken = default)
        {
            var author = await _webDbContext.Set<Author>().FindAsync(id);

            if (author is null) return await Task.FromResult(false);

            _webDbContext.Set<Author>().Remove(author);
            var rowsCount = await _webDbContext.SaveChangesAsync(cancellationToken);

            return rowsCount > 0;
        }

        public async Task<List<AuthorItem>> GetAuthorMostPost(int authorNum, CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Set<Author>()
                .Select(s => new AuthorItem()
                {
                    Id = s.Id,
                    UrlSlug = s.UrlSlug,
                    FullName = s.FullName,
                    ImageUrl = s.ImageUrl,
                    JoinedDate = s.JoinedDate,
                })
                .Take(authorNum)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> SetImageUrlAsync(
                int authorId, string imageUrl,
                CancellationToken cancellationToken = default)
        {
            return await _webDbContext.Authors
                .Where(x => x.Id == authorId)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(a => a.ImageUrl, a => imageUrl),
                    cancellationToken) > 0;
        }
    }
}