using CookingWeb.Core.Contracts;
using CookingWeb.Core.DTO.Course;
using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Courses
{
    public interface ICourseRepository
    {
        Task<Course> GetCourseById(int id, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<bool> IsCourseSludExitedAsync(int id, string slug, CancellationToken cancellationToken = default);

        Task<Course> GetCourseBySlug(string slug, bool includeDetails = false, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedCoursesAsync<T>(CourseQuery query,
            IPagingParams pagingParams,
            Func<IQueryable<Course>, IQueryable<T>> mapper,
            CancellationToken cancellationToken = default);

        Task<IList<T>> GetNPopularCoursesAsync<T>(int n, 
            Func<IQueryable<Course>, IQueryable<T>> mapper,
            CancellationToken cancellationToken = default);

        Task ToggleStatus(int id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateCourseAsync(Course course, CancellationToken cancellationToken = default);

        Task<bool> DeleteCourseByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
