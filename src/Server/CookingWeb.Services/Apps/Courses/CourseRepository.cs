using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Course;
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

namespace CookingWeb.Services.Apps.Courses
{
    public class CourseRepository : ICourseRepository
    {
        private readonly WebDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public CourseRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<Course> GetCourseById(int id, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            if(!includeDetails)
            {
                return await _context.Set<Course>().FindAsync(id);
            }
            return await _context.Set<Course>()
                .Include(c => c.Demand)
                .Include(c => c.Price)
                .Include(c => c.NumberOfSessions)
                .Include(c => c.Chef)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsCourseSludExitedAsync(int id, string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Course>()
                .AnyAsync(c => c.Id != id && c.UrlSlug == slug, cancellationToken);
        }

        public async Task<Course> GetCourseBySlug(string slug, bool includeDetails, CancellationToken cancellationToken)
        {
            if (!includeDetails)
            {
                return await _context.Set<Course>().Where(c => c.UrlSlug == slug)
                    .FirstOrDefaultAsync(cancellationToken);
            }
            return await _context.Set<Course>()
                .Include(c => c.Demand)
                .Include(c => c.Price)
                .Include(c => c.NumberOfSessions)
                .Include(c => c.Chef)
                .Where(c => c.UrlSlug == slug)
                .FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<Course> FindCourseByQueryable(CourseQuery query)
        {
            IQueryable<Course> courseQuery = _context.Set<Course>()
                .Include(c => c.Demand)
                .Include(c => c.Price)
                .Include(c => c.NumberOfSessions)
                .Include(c => c.Chef)
                .Where(c => c.Published);
            if (!string.IsNullOrEmpty(query.Keyword))
            {
                courseQuery = courseQuery.Where(c => c.Title.Contains(query.Keyword)
                || c.ShortDescription.Contains(query.Keyword)
                || c.Description.Contains(query.Keyword)
                || c.UrlSlug.Contains(query.Keyword));
            }
            if(query.DemandId > 0)
            {
                courseQuery = courseQuery.Where(c => c.DemandId == query.DemandId);
            }
            if(query.PriceId > 0)
            {
                courseQuery = courseQuery.Where(c => c.PriceId == query.PriceId);
            }
            if(query.NumberOfSessionsId > 0)
            {
                courseQuery = courseQuery.Where(c => c.NumberOfSessionsId == query.NumberOfSessionsId);
            }
            if(query.ChefId > 0)
            {
                courseQuery = courseQuery.Where(c => c.ChefId == query.ChefId);
            }
            if(query.CreateMonth > 0)
            {
                courseQuery = courseQuery.Where(c => c.CreateDate.Month == query.CreateMonth);
            }
            if(query.CreateYear > 0)
            {
                courseQuery = courseQuery.Where(c => c.CreateDate.Year == query.CreateYear);
            }
            return courseQuery;
        }
        public async Task<IPagedList<T>> GetPagedCoursesAsync<T>(CourseQuery query,
            IPagingParams pagingParams,
            Func<IQueryable<Course>, IQueryable<T>> mapper,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Course> coursesFindResultQuery = FindCourseByQueryable(query);
            IQueryable<T> result = mapper(coursesFindResultQuery);
            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IList<T>> GetNPopularCoursesAsync<T>(int n, 
            Func<IQueryable<Course>, IQueryable<T>> mapper, 
            CancellationToken cancellationToken = default)
        {
            var popular = _context.Set<Course>()
                .Include(c => c.Demand)
                .Include(c => c.Price)
                .Include(c => c.NumberOfSessions)
                .Include(c => c.Chef)
                .Where(c => c.Published)
                .OrderByDescending(c => c.RegisterCount)
                .Take(n);
            return await mapper(popular).ToListAsync(cancellationToken);
        }

        public async Task ToggleStatus(int id, CancellationToken cancellationToken = default)
        {
            await _context.Set<Course>()
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.Published, c => !c.Published), cancellationToken);
        }

        public async Task<bool> AddOrUpdateCourseAsync(Course course, CancellationToken cancellationToken = default)
        {
            if (course.Id > 0)
            {
                course.UpdateDate = DateTime.Now;
                _context.Update(course);
            }
            else
                _context.Add(course);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
