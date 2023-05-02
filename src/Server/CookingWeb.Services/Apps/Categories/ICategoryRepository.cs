using CookingWeb.Core.DTO.Category;
using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Categories
{
    public interface ICategoryRepository
    {
        Task<IList<CategoryItem>> GetCategoriesAsync(
            bool showOnMenu = true,
            CancellationToken cancellationToken = default);

        Task<Category> GetCategoryById(int id, bool includeDetails = false, CancellationToken cancellationToken = default);
    }
}
