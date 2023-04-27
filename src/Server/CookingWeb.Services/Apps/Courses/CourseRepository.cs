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
    }
}
