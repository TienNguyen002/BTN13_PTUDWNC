using CookingWeb.Core.DTO.Category;
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

namespace CookingWeb.Services.Apps.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public CategoryRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IList<CategoryItem>> GetCategoriesAsync(
            bool showOnMenu = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Category> categories = _context.Set<Category>()
                .Where(c => c.ShowOnMenu == true);
            return await categories
                .OrderBy(c => c.Name)
                .Select(c => new CategoryItem()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    UrlSlug = c.UrlSlug,
                    ShowOnMenu = c.ShowOnMenu,
                    PostCount = c.Posts.Count()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<Category> GetCategoryById(int id, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if (!includeDetails)
            {
                return await _context.Set<Category>().FindAsync(id);
            }
            return await _context.Set<Category>()
                .Include(c => c.Topic)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
