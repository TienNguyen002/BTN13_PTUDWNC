using CookingWeb.Core.DTO.Others;
using CookingWeb.Core.Entities;
using CookingWeb.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Other
{
    public class AppRepository : IAppRepository
    {
        private readonly WebDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public AppRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        #region Demand
        public async Task<IList<DemandItem>> GetDemandsAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Demand> demands = _context.Set<Demand>();
            return await demands
                .OrderBy(d => d.Name)
                .Select(d => new DemandItem()
                {
                    Id = d.Id,
                    Name = d.Name,
                    UrlSlug = d.UrlSlug,
                    CoursesCount = d.Courses.Count(c => c.Published)
                })
                .OrderByDescending(d => d.CoursesCount)
                .ToListAsync(cancellationToken);
        }

        public async Task<Demand> GetDemandByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Demand>()
                .Include(d => d.Courses)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region Price
        public async Task<IList<PriceItem>> GetPricesAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Price> prices = _context.Set<Price>();
            return await prices
                .OrderBy(p => p.Name)
                .Select(p => new PriceItem()
                { 
                    Id = p.Id,
                    Name = p.Name,
                    UrlSlug = p.UrlSlug,
                    CoursesCount = p.Courses.Count(c => c.Published)
                })
                .OrderByDescending(p =>  p.CoursesCount)
                .ToListAsync(cancellationToken);
        }

        public async Task<Price> GetPriceByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Price>()
                .Include(p => p.Courses)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region NumberOfSessions
        public async Task<IList<NumberOfSessionsItem>> GetNumberOfSessionsAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<NumberOfSessions> sessions = _context.Set<NumberOfSessions>();
            return await sessions
                .OrderBy(n => n.Name)
                .Select(n => new NumberOfSessionsItem()
                {
                    Id = n.Id,
                    Name = n.Name,
                    UrlSlug = n.UrlSlug,
                    CoursesCount = n.Courses.Count(c => c.Published)
                })
                .OrderByDescending(p => p.CoursesCount)
                .ToListAsync(cancellationToken);
        }

        public async Task<NumberOfSessions> GetNumberOfSessionsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<NumberOfSessions>()
                .Include(n => n.Courses)
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        #endregion

        #region Topic
        public async Task<IList<TopicItem>> GetTopicsAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Topic> topics = _context.Set<Topic>();
            return await topics
                .OrderBy(t => t.Name)
                .Select(t => new TopicItem()
                {
                    Id = t.Id,
                    Name = t.Name,
                    UrlSlug = t.UrlSlug,
                    CategoriesCount = t.Categories.Count()
                })
                .OrderByDescending(p => p.CategoriesCount)
                .ToListAsync(cancellationToken);
        }

        public async Task<Topic> GetTopicByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Topic>()
                .Include(t => t.Categories)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        #endregion
    }
}
