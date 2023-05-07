using CookingWeb.Core.DTO.Chef;
using CookingWeb.Core.Entities;
using CookingWeb.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Chefs
{
    public class ChefRepository : IChefRepository
    {
        private readonly WebDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public ChefRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IList<ChefItem>> GetChefsAsync(
            bool id = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Chef> chefs = _context.Set<Chef>();
            return await chefs
                .OrderBy(c => c.FullName)
                .Select(c => new ChefItem()
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Description = c.Description,
                    UrlSlug = c.UrlSlug
                })
                .ToListAsync(cancellationToken);
        }

        public Task<Chef> GetChefsBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ChefItem>> GetChefsSlugAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteChefsAsync(int id, CancellationToken cancellationToken = default)
        {
            var chefs = await _context.Set<Chef>().FindAsync(id);

            if (chefs is null) return false;

            _context.Set<Chef>().Remove(chefs);
            var rowsCount = await _context.SaveChangesAsync(cancellationToken);

            return rowsCount > 0;
        }

        public Task<IList<ChefItem>> GetCachedChefsByIdAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsChefSlugExistedAsync(
           int Chefid, string Chefslug,
           CancellationToken cancellationToken = default)
        {
            return await _context.Set<Chef>()
                .AnyAsync(x => x.Id != Chefid && x.UrlSlug == Chefslug, cancellationToken);
        }

        public async Task<bool> AddOrUpdateChefAsync(Chef chef, CancellationToken cancellationToken = default)
        {
            if (chef.Id > 0)
            {
                _context.Chefs.Update(chef);
                _memoryCache.Remove($"chef.by-id.{chef.Id}");
            }
            else
            {
                _context.Chefs.Add(chef);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public Task<IList<ChefItem>> GetChefsAsync(Course course, bool id = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
