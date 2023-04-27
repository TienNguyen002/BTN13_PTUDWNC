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
        Task<IPagedList<T>> GetPagedCoursesAsync<T>(CourseQuery query,
            IPagingParams pagingParams,
            Func<IQueryable<Course>, IQueryable<T>> mapper,
            CancellationToken cancellationToken = default);
    }
}
