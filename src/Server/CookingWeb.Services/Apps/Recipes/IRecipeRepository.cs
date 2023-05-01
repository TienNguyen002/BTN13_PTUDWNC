using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Recipe;
using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Recipes
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeById(int id, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<bool> IsRecipeSlugExitedAsync(int id, string slug, CancellationToken cancellationToken = default);

        Task<Recipe> GetRecipeBySlug(string slug, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedRecipesAsync<T>(RecipeQuery query,
            IPagingParams pagingParams,
            Func<IQueryable<Recipe>, IQueryable<T>> mapper,
            CancellationToken cancellationToken = default);

        Task ToggleStatus(int id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateRecipeAsync(Recipe recipe, CancellationToken cancellationToken = default);

        Task<bool> DeleteRecipeByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> SetImageRecipeAsync(int id, string imageUrl, CancellationToken cancellationToken = default);
    }
}
