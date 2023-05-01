using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Course;
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

namespace CookingWeb.Services.Apps.Recipes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly WebDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public RecipeRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Recipe> GetRecipeById(int id, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if(!includeDetails)
            {
                return await _context.Set<Recipe>().FindAsync(id);
            }
            return await _context.Set<Recipe>()
                .Include(r => r.Author)
                .Include(r => r.Course)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsRecipeSlugExitedAsync(int id, string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Recipe>()
                .AnyAsync(r => r.Id != id && r.UrlSlug == slug, cancellationToken);
        }

        public async Task<Recipe> GetRecipeBySlug(string slug, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if (!includeDetails)
            {
                return await _context.Set<Recipe>().Where(r => r.UrlSlug == slug)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            return await _context.Set<Recipe>()
                .Include(r => r.Author)
                .Include(r => r.Course)
                .Where(r => r.UrlSlug == slug)
                .FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<Recipe> FindRecipeByQueryable(RecipeQuery query)
        {
            IQueryable<Recipe> recipeQuery = _context.Set<Recipe>()
                .Include(r => r.Author)
                .Include(r => r.Course)
                .Where(r => r.Published);
            if(!string.IsNullOrEmpty(query.Keyword))
            {
                recipeQuery = recipeQuery.Where(r => r.Title.Contains(query.Keyword)
                || r.ShortDesciption.Contains(query.Keyword)
                || r.Description.Contains(query.Keyword)
                || r.Description.Contains(query.Keyword));
            }
            if(query.AuthorId > 0)
            {
                recipeQuery = recipeQuery.Where(r => r.AuthorId == query.AuthorId);
            }
            if(query.CourseId > 0)
            {
                recipeQuery = recipeQuery.Where(r => r.CourseId == query.CourseId);
            }
            if (query.CreateMonth > 0)
            {
                recipeQuery = recipeQuery.Where(c => c.CreateDate.Month == query.CreateMonth);
            }
            if (query.CreateYear > 0)
            {
                recipeQuery = recipeQuery.Where(c => c.CreateDate.Year == query.CreateYear);
            }
            return recipeQuery;
        }

        public async Task<IPagedList<T>> GetPagedRecipesAsync<T>(RecipeQuery query, 
            IPagingParams pagingParams, 
            Func<IQueryable<Recipe>, IQueryable<T>> mapper, 
            CancellationToken cancellationToken = default)
        {
            IQueryable<Recipe> recipesFindResultQuery = FindRecipeByQueryable(query);
            IQueryable<T> result = mapper(recipesFindResultQuery);
            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task ToggleStatus(int id, CancellationToken cancellationToken = default)
        {
            await _context.Set<Recipe>()
                .Where(r => r.Id == id)
                .ExecuteUpdateAsync(r => r.SetProperty(r => r.Published, r => !r.Published), cancellationToken);
        }

        public async Task<bool> AddOrUpdateRecipeAsync(Recipe recipe, CancellationToken cancellationToken = default)
        {
            if (recipe.Id > 0)
            {
                recipe.UpdateDate = DateTime.Now;
                _context.Update(recipe);
            }
            else
                _context.Add(recipe);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> DeleteRecipeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var recipeToDelete = await _context.Set<Recipe>()
                .Include(r => r.Author)
                .Include(r => r.Course)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            if(recipeToDelete == null)
            {
                return false;
            }
            _context.Remove(recipeToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> SetImageRecipeAsync(int id, string imageUrl, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Recipe>()
                .Where(r => r.Id == id)
                .ExecuteUpdateAsync(r => r.SetProperty(r => r.ImageUrl, r => imageUrl)
                                        .SetProperty(r => r.UpdateDate, r => DateTime.Now),
                                        cancellationToken) > 0;
        } 
    }
}
