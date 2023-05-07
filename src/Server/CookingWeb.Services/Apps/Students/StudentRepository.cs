using CookingWeb.Core.DTO.Chef;
using CookingWeb.Core.DTO.Student;
using CookingWeb.Core.Entities;
using CookingWeb.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingWeb.Services.Apps.Students
{
    public class StudentRepository : IStudentRepository
    {
        private readonly WebDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public StudentRepository(WebDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }


        public async Task<IList<StudentItem>> GetStudentsAsync(
            bool Email = true,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Student> students = _context.Set<Student>();
            return await students
                .OrderBy(c => c.FullName)
                .Select(c => new StudentItem()
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Mobile = c.Mobile,
                    UrlSlug = c.UrlSlug
                })
                .ToListAsync(cancellationToken);
        }
        public async Task<bool> AddOrUpdateStudentAsync(
           Student student, CancellationToken cancellationToken = default)
        {
            if (student.Id > 0)
            {
                _context.Students.Update(student);
                _memoryCache.Remove($"student.by-id.{student.Id}");
            }
            else
            {
                _context.Students.Add(student);
            }

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        public async Task<bool> IsStudentSlugExistedAsync(
           int Studentid, string Studentslug,
           CancellationToken cancellationToken = default)
        {
            return await _context.Set<Student>()
                .AnyAsync(x => x.Id != Studentid && x.UrlSlug == Studentslug, cancellationToken);
        }
        //Xoa
        public async Task<bool> DeleteStudentsAsync(int id, CancellationToken cancellationToken = default)
        {
            var students = await _context.Set<Student>().FindAsync(id);

            if (students is null) return false;

            _context.Set<Student>().Remove(students);
            var rowsCount = await _context.SaveChangesAsync(cancellationToken);

            return rowsCount > 0;
        }
    }
}

