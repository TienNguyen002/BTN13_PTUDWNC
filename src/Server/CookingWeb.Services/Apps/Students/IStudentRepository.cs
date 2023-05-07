using CookingWeb.Core.DTO.Category;
using CookingWeb.Core.DTO.Student;
using CookingWeb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Students
{
    public interface IStudentRepository
    {
        Task<IList<StudentItem>> GetStudentsAsync(
            bool Email = true,
           CancellationToken cancellationToken = default);
        Task<bool> DeleteStudentsAsync(
            int id,
            CancellationToken cancellationToken = default);
        Task<bool> IsStudentSlugExistedAsync(
            int id,
            string slug,
            CancellationToken cancellationToken = default);
        Task<bool> AddOrUpdateStudentAsync(
            Student students,
            CancellationToken cancellationToken = default);
    }
}
