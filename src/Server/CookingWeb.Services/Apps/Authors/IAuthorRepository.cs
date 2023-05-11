using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Author;
using CookingWeb.Core.DTO.Category;
using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Authors;

internal interface IAuthorRepository
{
    Task<Author> GetAuthorByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Author> GetCachedAuthorByIdAsync(int authorId);

    Task<IList<AuthorItem>> GetAuthorsAsync(
        CancellationToken cancellationToken = default);

    Task<int> CountAuthorAsync(CancellationToken cancellationToken = default);
    Task<bool> IsExistAuthorSlugAsync(int id, string slug, CancellationToken cancellationToken = default);

    Task<Author> GetAuthorBySlugAsync(string urlSlug, CancellationToken cancellationToken = default);

    Task<Author> GetCachedAuthorBySlugAsync(
        string slug, CancellationToken cancellationToken = default);

    Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(IAuthorQuery condition, IPagingParams pagingParams, CancellationToken cancellationToken = default);

    Task<IPagedList<AuthorItem>> GetPagedAuthorsAsync(
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default);

    Task<IPagedList<T>> GetPagedAuthorsAsync<T>(
        Func<IQueryable<Author>, IQueryable<T>> mapper,
        IPagingParams pagingParams,
        string name = null,
        CancellationToken cancellationToken = default);

    Task<bool> AddOrUpdateAuthorAsync(Author author, CancellationToken cancellationToken = default);

    Task<bool> DeleteAuthorByIdAsync(int? id, CancellationToken cancellationToken = default);

    Task<List<AuthorItem>> GetAuthorMostPost(int authorNum, CancellationToken cancellationToken = default);

    Task<bool> SetImageUrlAsync(
        int authorId, string imageUrl,
        CancellationToken cancellationToken = default);
}
